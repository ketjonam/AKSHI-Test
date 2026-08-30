using System.Text.RegularExpressions;
using Microsoft.Playwright;

namespace AKSHI.Test.Core;

public sealed class LoginPage
{
    private readonly IPage _page;
    private readonly TestSettings _settings;

    public LoginPage(IPage page, TestSettings settings)
    {
        _page = page;
        _settings = settings;
    }

    public async Task LoginAsync(LoginProfile profile)
    {
        AccountSettings account = SettingsLoader.AccountFor(profile);

        if (string.IsNullOrWhiteSpace(account.Username) || string.IsNullOrWhiteSpace(account.Password))
        {
            Assert.Ignore(
                $"Kredencialet per {profile} mungojne. Ploteso Qytetar/Biznes ne appsettings.json " +
                "ose appsettings.Local.json, ose variablat e mjedisit AKSHI_QYTETAR_USERNAME / AKSHI_BIZNES_USERNAME.");
        }

        TestContext.Progress.WriteLine($"{DateTime.Now:HH:mm:ss} | Login si {profile}");
        await _page.GotoAsync(_settings.Portal.BaseUrl, new PageGotoOptions
        {
            WaitUntil = WaitUntilState.DOMContentLoaded,
            Timeout = _settings.Portal.NavigationTimeoutMs
        });

        await ClickHyrAsync();
        await ClickAccountTypeAsync(profile);

        ILocator username = _page.Locator(_settings.Login.UsernameSelector).First;
        ILocator password = _page.Locator(_settings.Login.PasswordSelector).First;
        await username.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 30000
        });
        await username.FillAsync(account.Username);
        await password.FillAsync(account.Password);

        await ClickSubmitAsync();
        await WaitForOneTimeCodeAsync();

        TestContext.Progress.WriteLine($"{DateTime.Now:HH:mm:ss} | Login {profile} u krye");
    }

    private async Task ClickHyrAsync()
    {
        ILocator hyr = _page.Locator(_settings.Login.HyrButtonSelector)
            .Or(_page.Locator("a.custom-button", new() { HasText = "Hyr" }))
            .Or(_page.GetByRole(AriaRole.Link, new() { NameRegex = new Regex("^Hyr$", RegexOptions.IgnoreCase) }));

        await hyr.First.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 20000
        });
        TestContext.Progress.WriteLine($"{DateTime.Now:HH:mm:ss} | Kliko Hyr");
        await hyr.First.ClickAsync();
        await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }

    private async Task ClickAccountTypeAsync(LoginProfile profile)
    {
        string selector = profile.IsQytetar()
            ? _settings.Login.QytetarTabSelector
            : _settings.Login.BiznesTabSelector;

        ILocator tab = _page.Locator(selector).First;
        if (profile.IsQytetar())
        {
            tab = tab.Or(_page.Locator("#citizen-tab"))
                .Or(_page.GetByRole(AriaRole.Tab, new() { NameRegex = new Regex("Qytetar", RegexOptions.IgnoreCase) }));
        }
        else
        {
            tab = tab.Or(_page.Locator("#business-tab"))
                .Or(_page.Locator("li[onclick*=\"switchAccountType('business')\"]"))
                .Or(_page.GetByRole(AriaRole.Tab, new() { NameRegex = new Regex("^Biznes$", RegexOptions.IgnoreCase) }));
        }

        await tab.First.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 30000
        });
        TestContext.Progress.WriteLine($"{DateTime.Now:HH:mm:ss} | Zgjidh {profile}");
        await tab.First.ClickAsync();
    }

    private async Task ClickSubmitAsync()
    {
        ILocator submit = _page.Locator(_settings.Login.SubmitSelector)
            .Or(_page.GetByRole(AriaRole.Button, new()
            {
                NameRegex = new Regex("VAZHDONI ME IDENTIFIKIMIN|Vazhdo me Identifikimin|Identifikim", RegexOptions.IgnoreCase)
            }));

        await submit.First.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 15000
        });
        TestContext.Progress.WriteLine($"{DateTime.Now:HH:mm:ss} | Kliko Vazhdoni me Identifikimin");
        await submit.First.ClickAsync();
    }

    private async Task WaitForOneTimeCodeAsync()
    {
        int timeoutMs = Math.Max(_settings.Login.OtpTimeoutMs, 240000);
        TestContext.Progress.WriteLine(
            $"{DateTime.Now:HH:mm:ss} | Prit deri ne {timeoutMs / 1000} sekonda qe te vendosesh kodin OTP ne shfletues");

        DateTime deadline = DateTime.UtcNow.AddMilliseconds(timeoutMs);
        while (DateTime.UtcNow < deadline)
        {
            if (await IsLoggedInAsync())
            {
                TestContext.Progress.WriteLine($"{DateTime.Now:HH:mm:ss} | OTP u pranua, sesioni eshte aktiv");
                return;
            }

            await _page.WaitForTimeoutAsync(2000);
        }

        Assert.Fail(
            "Login nuk perfundoi brenda 4 minutave. Vendos kodin OTP ne shfletues pasi klikohet " +
            "'VAZHDONI ME IDENTIFIKIMIN'.");
    }

    private async Task<bool> IsLoggedInAsync()
    {
        try
        {
            ILocator loginForm = _page.Locator("#kc-login, #username, #password").First;
            bool loginFormVisible = await loginForm.CountAsync() > 0 && await loginForm.IsVisibleAsync();
            if (loginFormVisible)
                return false;

            string url = _page.Url;
            bool stillOnAuth = url.Contains("auth", StringComparison.OrdinalIgnoreCase)
                && (url.Contains("login", StringComparison.OrdinalIgnoreCase)
                    || url.Contains("openid", StringComparison.OrdinalIgnoreCase)
                    || url.Contains("realms", StringComparison.OrdinalIgnoreCase));
            if (stillOnAuth)
                return false;

            Uri baseUri = new(_settings.Portal.BaseUrl);
            return url.Contains(baseUri.Host, StringComparison.OrdinalIgnoreCase);
        }
        catch (PlaywrightException)
        {
            return false;
        }
    }
}
