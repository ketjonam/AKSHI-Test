namespace AKSHI.Test.Core;

public static class QytetarLoginPlanner
{
    public static IReadOnlyList<LoginProfile> Resolve()
    {
        string haystack = string.Join(" ", Environment.GetCommandLineArgs())
            + " "
            + (Environment.GetEnvironmentVariable("AKSHI_QYTETAR_LOGIN") ?? string.Empty);

        bool hasJ354 = ContainsAny(haystack, "J35413056V", "NidJ55728107R");
        bool hasJ709 = ContainsAny(haystack, "J70903019W", "NidJ25730113W");
        bool hasF604 = ContainsAny(haystack, "F60416142P", "NidF60214024S", "NidG35511058E");

        var profiles = new List<LoginProfile>();
        if (hasJ354)
            profiles.Add(LoginProfile.Qytetar);
        if (hasJ709)
            profiles.Add(LoginProfile.QytetarJ70903019W);
        if (hasF604)
            profiles.Add(LoginProfile.QytetarF60416142P);

        if (profiles.Count > 0)
            return profiles;

        profiles.Add(LoginProfile.Qytetar);
        if (HasCredentials(LoginProfile.QytetarJ70903019W))
            profiles.Add(LoginProfile.QytetarJ70903019W);
        if (HasCredentials(LoginProfile.QytetarF60416142P))
            profiles.Add(LoginProfile.QytetarF60416142P);
        return profiles;
    }

    private static bool HasCredentials(LoginProfile profile)
    {
        AccountSettings account = SettingsLoader.AccountFor(profile);
        return !string.IsNullOrWhiteSpace(account.Username)
            && !string.IsNullOrWhiteSpace(account.Password);
    }

    private static bool ContainsAny(string haystack, params string[] tokens) =>
        tokens.Any(token => haystack.Contains(token, StringComparison.OrdinalIgnoreCase));
}
