namespace AKSHI.Test.Core;

public enum LoginProfile
{
    /// <summary>Qytetar J35413056V (testet që në origjinal përdornin J55728107R).</summary>
    Qytetar,
    /// <summary>Qytetar J70903019W (testet që në origjinal përdornin J25730113W).</summary>
    QytetarJ70903019W,
    /// <summary>Qytetar F60416142P (testet që në origjinal përdornin G35511058E, F60214024S ose F60416142P).</summary>
    QytetarF60416142P,
    Biznes
}

public static class LoginProfiles
{
    public static bool IsQytetar(this LoginProfile profile) =>
        profile != LoginProfile.Biznes;
}

public enum ServiceStartMode
{
    NewApplication,
    Track
}
