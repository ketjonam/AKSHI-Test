namespace AKSHI.Test.Core;

public sealed class TestSettings
{
    public PortalSettings Portal { get; set; } = new();
    public LoginSelectorSettings Login { get; set; } = new();
    public ServiceSelectorSettings Service { get; set; } = new();
    public AccountSettings Qytetar { get; set; } = new();
    public AccountSettings QytetarJ70903019W { get; set; } = new();
    public AccountSettings QytetarF60416142P { get; set; } = new();
    public AccountSettings Biznes { get; set; } = new();
}

public sealed class PortalSettings
{
    public string BaseUrl { get; set; } = "https://e-albania.al";
    public bool Headless { get; set; }
    public int SlowMoMs { get; set; } = 50;
    public int DefaultTimeoutMs { get; set; } = 30000;
    public int NavigationTimeoutMs { get; set; } = 60000;
}

public sealed class LoginSelectorSettings
{
    public string HyrButtonSelector { get; set; } =
        "a.custom-button[href*='redirectToGgLogin']";
    public string QytetarTabSelector { get; set; } = "#citizen-tab";
    public string BiznesTabSelector { get; set; } =
        "#business-tab, #subject-tab, li[onclick*=\"switchAccountType('business')\"], li[onclick*=\"switchAccountType('subject')\"]";
    public List<string> QytetarEntryTexts { get; set; } = new();
    public List<string> BiznesEntryTexts { get; set; } = new();
    public string UsernameSelector { get; set; } = "#username";
    public string PasswordSelector { get; set; } = "#password";
    public string SubmitSelector { get; set; } = "#kc-login";
    public List<string> SubmitTexts { get; set; } = new();
    public int OtpTimeoutMs { get; set; } = 240000;
}

public sealed class ServiceSelectorSettings
{
    public List<string> SearchPlaceholderTexts { get; set; } = new();
    public List<string> UseServiceTexts { get; set; } = new();
    public List<string> NewApplicationSelectors { get; set; } = new();
    public List<string> NewApplicationTexts { get; set; } = new();
    public List<string> TrackTexts { get; set; } = new();
}

public sealed class AccountSettings
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
