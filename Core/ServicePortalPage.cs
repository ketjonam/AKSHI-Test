using System.Text.RegularExpressions;
using Microsoft.Playwright;

namespace AKSHI.Test.Core;

public sealed class ServicePortalPage
{
    private readonly IPage _page;
    private readonly TestSettings _settings;

    public ServicePortalPage(IPage page, TestSettings settings)
    {
        _page = page;
        _settings = settings;
    }

    public static string ServiceDetailsUrl(string baseUrl, string serviceCode)
    {
        string path = "ServiceDetails";
        return $"{baseUrl.TrimEnd('/')}/{path.Trim('/')}/{Uri.EscapeDataString(serviceCode)}";
    }

    public async Task OpenServiceAsync(ServiceInfo service, bool startService = true)
    {
        string url = ServiceDetailsUrl(_settings.Portal.BaseUrl, service.Code);
        TestContext.Progress.WriteLine(
            $"{DateTime.Now:HH:mm:ss} | Hap sherbimin {service.Code} ne {url}");

        await _page.GotoAsync(url, new PageGotoOptions
        {
            WaitUntil = WaitUntilState.DOMContentLoaded,
            Timeout = _settings.Portal.NavigationTimeoutMs
        });
        await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        AssertNotRedirectedToLogin(url);

        if (!startService)
            return;

        await UseServiceAsync();

        if (service.Mode == ServiceStartMode.Track)
            await ClickTrackAsync();
        else
            await ClickNewApplicationAsync();
    }

    private void AssertNotRedirectedToLogin(string expectedUrl)
    {
        string current = _page.Url;
        bool onAuth = current.Contains("auth", StringComparison.OrdinalIgnoreCase)
            && (current.Contains("login", StringComparison.OrdinalIgnoreCase)
                || current.Contains("openid", StringComparison.OrdinalIgnoreCase)
                || current.Contains("realms", StringComparison.OrdinalIgnoreCase));

        if (onAuth)
        {
            Assert.Fail(
                $"Sesioni i login-it nuk vlen me. Pritesh {expectedUrl}, por u ridrejtuam te {current}. " +
                "Bej login perseri (OTP) para se te nisin testet.");
        }
    }

    private async Task UseServiceAsync()
    {
        foreach (string text in _settings.Service.UseServiceTexts)
        {
            ILocator button = _page.GetByRole(AriaRole.Button, new() { NameRegex = new Regex(Regex.Escape(text), RegexOptions.IgnoreCase) })
                .Or(_page.GetByRole(AriaRole.Link, new() { NameRegex = new Regex(Regex.Escape(text), RegexOptions.IgnoreCase) }));

            if (await button.CountAsync() > 0 && await button.First.IsVisibleAsync())
            {
                TestContext.Progress.WriteLine($"{DateTime.Now:HH:mm:ss} | Kliko '{text}'");
                await button.First.ClickAsync();
                await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
                return;
            }
        }
    }

    private async Task ClickNewApplicationAsync()
    {
        foreach (string selector in _settings.Service.NewApplicationSelectors)
        {
            ILocator bySelector = _page.Locator(selector);
            if (await bySelector.CountAsync() > 0 && await bySelector.First.IsVisibleAsync())
            {
                TestContext.Progress.WriteLine($"{DateTime.Now:HH:mm:ss} | Kliko Aplikim i ri");
                await bySelector.First.ClickAsync();
                await _page.WaitForTimeoutAsync(1500);
                return;
            }
        }

        foreach (string text in _settings.Service.NewApplicationTexts)
        {
            ILocator byText = _page.GetByRole(AriaRole.Button, new() { NameRegex = new Regex(Regex.Escape(text), RegexOptions.IgnoreCase) })
                .Or(_page.GetByText(text, new() { Exact = false }));

            if (await byText.CountAsync() > 0 && await byText.First.IsVisibleAsync())
            {
                TestContext.Progress.WriteLine($"{DateTime.Now:HH:mm:ss} | Kliko '{text}'");
                await byText.First.ClickAsync();
                await _page.WaitForTimeoutAsync(1500);
                return;
            }
        }
    }

    private async Task ClickTrackAsync()
    {
        foreach (string text in _settings.Service.TrackTexts)
        {
            ILocator track = _page.GetByRole(AriaRole.Button, new() { NameRegex = new Regex(Regex.Escape(text), RegexOptions.IgnoreCase) })
                .Or(_page.GetByText(text, new() { Exact = false }));

            if (await track.CountAsync() > 0 && await track.First.IsVisibleAsync())
            {
                TestContext.Progress.WriteLine($"{DateTime.Now:HH:mm:ss} | Kliko '{text}'");
                await track.First.ClickAsync();
                await _page.WaitForTimeoutAsync(1500);
                return;
            }
        }
    }
}
