using Microsoft.Playwright;

namespace AKSHI.Test.Core;

public static class AuthSession
{
    public static async Task EnsureAsync(LoginProfile profile)
    {
        Directory.CreateDirectory(SettingsLoader.AuthStateDirectory);
        string statePath = SettingsLoader.AuthStatePath(profile);
        TestSettings settings = SettingsLoader.Current;
        AccountSettings account = SettingsLoader.AccountFor(profile);

        if (string.IsNullOrWhiteSpace(account.Username) || string.IsNullOrWhiteSpace(account.Password))
        {
            if (profile == LoginProfile.Biznes)
            {
                Assert.Fail(
                    "Kredencialet e biznesit mungojne. Vendos Biznes.Username / Biznes.Password ne appsettings.Local.json.");
            }

            if (profile.IsQytetar())
            {
                Assert.Fail(
                    $"Kredencialet e qytetarit mungojne per {profile} ({account.Username}). " +
                    "Vendos Username / Password ne appsettings.Local.json.");
            }

            TestContext.Progress.WriteLine(
                $"{DateTime.Now:HH:mm:ss} | SKIP login {profile}: kredencialet mungojne.");
            return;
        }

        if (File.Exists(statePath))
            File.Delete(statePath);

        TestContext.Progress.WriteLine(
            $"{DateTime.Now:HH:mm:ss} | Login {profile} me user {account.Username}");

        using IPlaywright playwright = await Playwright.CreateAsync();
        await using IBrowser browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            SlowMo = settings.Portal.SlowMoMs,
            Args = new[] { "--start-maximized" }
        });

        IBrowserContext context = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = ViewportSize.NoViewport,
            IgnoreHTTPSErrors = true
        });

        IPage page = await context.NewPageAsync();
        page.SetDefaultTimeout(settings.Portal.DefaultTimeoutMs);
        page.SetDefaultNavigationTimeout(settings.Portal.NavigationTimeoutMs);

        var login = new LoginPage(page, settings);
        await login.LoginAsync(profile);
        await context.StorageStateAsync(new BrowserContextStorageStateOptions { Path = statePath });
        TestContext.Progress.WriteLine($"{DateTime.Now:HH:mm:ss} | Storage state u ruajt: {statePath}");
    }
}
