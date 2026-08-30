using Microsoft.Extensions.Configuration;

namespace AKSHI.Test.Core;

public static class SettingsLoader
{
    private static readonly Lazy<TestSettings> Cached = new(Load);

    public static TestSettings Current => Cached.Value;

    public static string OutputDirectory =>
        TestContext.CurrentContext.WorkDirectory;

    public static string AuthStateDirectory =>
        Path.Combine(OutputDirectory, "AuthState");

    public static string AuthStatePath(LoginProfile profile) =>
        Path.Combine(AuthStateDirectory, $"{profile.ToString().ToLowerInvariant()}.json");

    public static AccountSettings AccountFor(LoginProfile profile) =>
        profile switch
        {
            LoginProfile.Qytetar => Current.Qytetar,
            LoginProfile.QytetarJ70903019W => Current.QytetarJ70903019W,
            LoginProfile.QytetarF60416142P => Current.QytetarF60416142P,
            LoginProfile.Biznes => Current.Biznes,
            _ => throw new ArgumentOutOfRangeException(nameof(profile), profile, null)
        };

    private static TestSettings Load()
    {
        string basePath = AppContext.BaseDirectory;

        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables("AKSHI_")
            .Build();

        var settings = new TestSettings();
        configuration.Bind(settings);

        if (!settings.Portal.Headless)
            Environment.SetEnvironmentVariable("HEADED", "1");

        string? envUser = Environment.GetEnvironmentVariable("AKSHI_QYTETAR_USERNAME");
        string? envPass = Environment.GetEnvironmentVariable("AKSHI_QYTETAR_PASSWORD");
        if (!string.IsNullOrWhiteSpace(envUser))
            settings.Qytetar.Username = envUser;
        if (!string.IsNullOrWhiteSpace(envPass))
            settings.Qytetar.Password = envPass;

        envUser = Environment.GetEnvironmentVariable("AKSHI_BIZNES_USERNAME");
        envPass = Environment.GetEnvironmentVariable("AKSHI_BIZNES_PASSWORD");
        if (!string.IsNullOrWhiteSpace(envUser))
            settings.Biznes.Username = envUser;
        if (!string.IsNullOrWhiteSpace(envPass))
            settings.Biznes.Password = envPass;

        return settings;
    }
}
