using AKSHI.Test.Compat;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace AKSHI.Test.Core;

public abstract class AkshiTestBase : PageTest
{
    private string _artifactsFolder = string.Empty;

    protected TestSettings Settings => SettingsLoader.Current;
    protected AccountSettings LoggedInAccount => SettingsLoader.AccountFor(Profile);
    protected string CitizenNid => LoggedInAccount.Username;
    protected PlaywrightUi Ui { get; private set; } = null!;
    protected PwDriver driver { get; private set; } = null!;
    protected WebDriverWait wait { get; private set; } = null!;
    protected abstract LoginProfile Profile { get; }
    protected abstract string ServiceCode { get; }
    protected virtual string? ServiceTitle => null;
    protected virtual ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected virtual bool StartServiceOnSetup => true;

    public override BrowserNewContextOptions ContextOptions()
    {
        return new BrowserNewContextOptions
        {
            StorageStatePath = File.Exists(SettingsLoader.AuthStatePath(Profile))
                ? SettingsLoader.AuthStatePath(Profile)
                : null,
            ViewportSize = ViewportSize.NoViewport,
            IgnoreHTTPSErrors = true
        };
    }

    [SetUp]
    public async Task AkshiSetUp()
    {
        string runTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string testName = TestContext.CurrentContext.Test.Name;
        _artifactsFolder = Path.Combine(
            TestContext.CurrentContext.WorkDirectory,
            "TestArtifacts",
            $"{testName}_{runTime}");
        Directory.CreateDirectory(_artifactsFolder);

        driver = new PwDriver(Page);
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        Ui = new PlaywrightUi(Page);
        Page.SetDefaultTimeout(Settings.Portal.DefaultTimeoutMs);
        Page.SetDefaultNavigationTimeout(Settings.Portal.NavigationTimeoutMs);

        string serviceUrl = ServicePortalPage.ServiceDetailsUrl(Settings.Portal.BaseUrl, ServiceCode);
        Log($"===== TEST START ({Profile}) =====");
        Log($"Test: {testName}");
        Log($"Service: {ServiceCode}");
        Log($"URL: {serviceUrl}");
        Log($"Artifacts: {_artifactsFolder}");

        if (!File.Exists(SettingsLoader.AuthStatePath(Profile)))
        {
            if (Profile == LoginProfile.Biznes)
            {
                Assert.Fail(
                    "Nuk u gjet sesioni i login-it si biznes. Login-i duhet te kryhet nje here ne fillim " +
                    "(Hyr → Biznes → NIPT/fjalëkalim → OTP) para se te nisin testet.");
            }

            if (Profile.IsQytetar())
            {
                Assert.Fail(
                    $"Nuk u gjet sesioni i login-it si qytetar ({CitizenNid}). Login-i duhet te kryhet nje here " +
                    "ne fillim (Hyr → Qytetar → NID/fjalëkalim → OTP) para se te nisin testet.");
            }

            Assert.Ignore(
                $"Nuk u gjet sesioni i login-it per {Profile}. " +
                "Plotëso kredencialet në appsettings.json ose appsettings.Local.json.");
        }

        ServiceInfo service = ServiceCatalog.Resolve(ServiceCode, ServiceTitle);
        service.StartMode = StartMode.ToString();
        service.LoginProfile = Profile.ToString();

        var portal = new ServicePortalPage(Page, Settings);
        await portal.OpenServiceAsync(service, StartServiceOnSetup);
    }

    [TearDown]
    public async Task AkshiTearDown()
    {
        try
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            Log($"Test status: {status}");

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                await SaveScreenshotAsync("FAILED");
                await SavePageSourceAsync("FAILED");
            }
        }
        catch (Exception ex)
        {
            Log("TearDown error: " + ex.Message);
        }
        finally
        {
            Log("===== TEST END =====");
        }
    }

    protected void Log(string message)
    {
        string logLine = $"{DateTime.Now:HH:mm:ss} | {message}";
        TestContext.Progress.WriteLine(logLine);
        TestContext.Out.WriteLine(logLine);
        Console.WriteLine(logLine);
    }

    protected async Task SaveScreenshotAsync(string name)
    {
        string file = Path.Combine(_artifactsFolder, $"{name}_Screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png");
        await Page.ScreenshotAsync(new PageScreenshotOptions { Path = file, FullPage = true });
        TestContext.AddTestAttachment(file, "Failure Screenshot");
        Log("Screenshot saved: " + file);
    }

    protected async Task SavePageSourceAsync(string name)
    {
        string file = Path.Combine(_artifactsFolder, $"{name}_PageSource_{DateTime.Now:yyyyMMdd_HHmmss}.html");
        await File.WriteAllTextAsync(file, await Page.ContentAsync());
        TestContext.AddTestAttachment(file, "Failure Page Source");
        Log("PageSource saved: " + file);
    }

    protected string artifactsFolder => _artifactsFolder;

    protected void SaveScreenshot(string name) =>
        PwSync.Run(() => SaveScreenshotAsync(name));

    protected void SaveScreenshot(IWebDriver _, string __, string namePrefix) =>
        SaveScreenshot(namePrefix);

    protected void SavePageSource(string name) =>
        PwSync.Run(() => SavePageSourceAsync(name));

    protected static string InputValue(IWebElement element) =>
        element.GetAttribute("value")?.Trim() ?? string.Empty;

    protected static readonly string UploadResourceNotFoundMessage =
        "Burimi i kërkuar nuk u gjet";

    protected void SafeClick(WebEl element)
    {
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            element);
        Thread.Sleep(500);
        try
        {
            element.Click();
        }
        catch (ElementClickInterceptedException)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
        }
    }

    protected void DismissCookieBannerIfPresent()
    {
        var acceptButtons = driver.FindElements(By.CssSelector("#cookieConsent.show button.cookie-btn.accept"));
        if (acceptButtons.Count == 0 || !acceptButtons[0].Displayed)
            return;

        Log("Prano cookies");
        SafeClick(By.CssSelector("#cookieConsent.show button.cookie-btn.accept"));
        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
            By.CssSelector("#cookieConsent.show")));
    }

    protected void SafeClick(By locator)
    {
        WebEl element = wait.Until(ExpectedConditions.ElementExists(locator));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            element);
        Thread.Sleep(500);
        try
        {
            element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            element.Click();
        }
        catch (ElementClickInterceptedException)
        {
            element = driver.FindElement(locator);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
        }
    }

    protected ILocator L(string selector) => Page.Locator(selector);
    protected Task SafeClickAsync(string selector) => Ui.SafeClickAsync(selector);
    protected Task<ILocator> WaitVisibleAsync(string selector) => Ui.WaitVisibleAsync(selector);
}
