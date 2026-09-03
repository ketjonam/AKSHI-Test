using AKSHI.Test.Compat;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace AKSHI.Test.Core;

[TestRunFilter]
public abstract class AkshiTestBase : PageTest
{
    private string _artifactsFolder = string.Empty;

    protected TestSettings Settings => SettingsLoader.Current;
    protected AccountSettings LoggedInAccount => SettingsLoader.AccountFor(Profile);
    protected string CitizenNid => LoggedInAccount.Username;
    protected PlaywrightUi Ui { get; private set; } = null!;
    protected PlaywrightCommands Commands { get; private set; } = null!;
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
        Commands = new PlaywrightCommands(Page, Ui, Settings.Commands);
        Page.SetDefaultTimeout(Settings.Portal.DefaultTimeoutMs);
        Page.SetDefaultNavigationTimeout(Settings.Portal.NavigationTimeoutMs);

        string serviceUrl = ServicePortalPage.ServiceDetailsUrl(Settings.Portal.BaseUrl, ServiceCode);
        Log($"===== TEST START ({Profile}) =====");
        Log($"Test: {testName}");
        Log($"Service: {ServiceCode}");
        Log($"URL: {serviceUrl}");
        Log($"Artifacts: {_artifactsFolder}");
        Commands.IgnoreIfTestSkipped(ServiceCode);

        if (!File.Exists(SettingsLoader.AuthStatePath(Profile)))
        {
            if (Profile.IsQytetar())
            {
                await AuthSession.LoginInCurrentPageAsync(Page, Profile);
            }
            else if (Profile == LoginProfile.Biznes)
            {
                Assert.Fail(
                    "Nuk u gjet sesioni i login-it si biznes. Login-i duhet te kryhet nje here ne fillim " +
                    "(Hyr → Biznes → NIPT/fjalëkalim → OTP) para se te nisin testet.");
            }
            else
            {
                Assert.Ignore(
                    $"Nuk u gjet sesioni i login-it per {Profile}. " +
                    "Plotëso kredencialet në appsettings.json ose appsettings.Local.json.");
            }
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

    protected static string GetTooltipText(IWebElement tooltip)
    {
        string? text = tooltip.GetAttribute("title");
        if (string.IsNullOrWhiteSpace(text))
            text = tooltip.GetAttribute("data-bs-original-title");
        if (string.IsNullOrWhiteSpace(text))
            text = tooltip.GetAttribute("data-bs-title");
        if (string.IsNullOrWhiteSpace(text))
            text = tooltip.GetAttribute("aria-label");
        return (text ?? string.Empty).Trim();
    }

    protected static void AssertTooltipText(IWebElement tooltip, string expected) =>
        Assert.That(NormalizeTooltipText(GetTooltipText(tooltip)), Is.EqualTo(NormalizeTooltipText(expected)));

    protected IWebElement WaitForSelectSelectedValue(string name, string expectedValue)
    {
        return wait.Until(d =>
        {
            try
            {
                IWebElement select = d.FindElement(By.CssSelector($"#root form select[name='{name}']"));
                string value = new SelectElement(select).SelectedOption.GetAttribute("value") ?? string.Empty;
                return value.Equals(expectedValue, StringComparison.OrdinalIgnoreCase) ? select : null;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
            catch (StaleElementReferenceException)
            {
                return null;
            }
        });
    }

    private static string NormalizeTooltipText(string text) =>
        text.Replace('ë', 'e').Replace('Ë', 'E')
            .Replace("e-Albania", "e-albania", StringComparison.OrdinalIgnoreCase)
            .Trim();

    protected void FillInput(IWebElement input, string value)
    {
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            input);
        Thread.Sleep(400);

        ((IJavaScriptExecutor)driver).ExecuteScript(@"
            const el = arguments[0];
            const proto = el.tagName === 'TEXTAREA'
                ? window.HTMLTextAreaElement.prototype
                : window.HTMLInputElement.prototype;
            const setter = Object.getOwnPropertyDescriptor(proto, 'value').set;
            setter.call(el, arguments[1]);
            el.dispatchEvent(new Event('input', { bubbles: true }));
            el.dispatchEvent(new Event('change', { bubbles: true }));
        ", input, value);

        Thread.Sleep(300);
    }

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

    protected void AssertFieldError(string expected)
    {
        IWebElement error = wait.Until(d =>
        {
            foreach (string selector in new[]
                     { ".invalid-feedback", ".text-danger.small", ".text-danger.mt-1", ".text-danger" })
            {
                foreach (var el in d.FindElements(By.CssSelector(selector)))
                {
                    if (el.Displayed && el.Text.Trim() == expected)
                        return el;
                }
            }

            return null;
        });
        Assert.That(error.Text.Trim(), Is.EqualTo(expected));
    }

    protected void ClickDergo()
    {
        Commands.Execute("dergo", () =>
        {
            Log("Kliko Dergo");
            SafeClick(By.CssSelector("button.ealb-btn-continue"));
            Thread.Sleep(5000);
        });
    }

    protected void AssertDergoOutcome(
        string successHeadline = "APLIKIMI JUAJ U DËRGUA ME SUKSES",
        string alertExpectedTitle = "Kujdes",
        string alertExpectedDescription = "Ekzistojne aplikime te pa perfunduara per kete mjet.")
    {
        if (!Commands.ShouldExecute("dergo"))
        {
            Log("Dërgo u anashkalua; nuk pretet ekrani i suksesit.");
            return;
        }

        By successHeadlineBy = By.XPath(
            "//h5[contains(normalize-space(.),'APLIKIMI JUAJ U DËRGUA ME SUKSES')] | //h5/b[contains(normalize-space(.),'APLIKIMI JUAJ U DËRGUA ME SUKSES')]");
        By alertModalBy = By.CssSelector(".alert-modal-container");

        string? outcome = null;
        try
        {
            outcome = new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(drv =>
            {
                try
                {
                    var successEls = drv.FindElements(successHeadlineBy);
                    if (successEls.Any(e =>
                    {
                        try { return e.Displayed; }
                        catch (StaleElementReferenceException) { return false; }
                    }))
                        return "success";
                }
                catch (StaleElementReferenceException)
                {
                }

                try
                {
                    var alertEls = drv.FindElements(alertModalBy);
                    if (alertEls.Any(e =>
                    {
                        try { return e.Displayed; }
                        catch (StaleElementReferenceException) { return false; }
                    }))
                        return "alert";
                }
                catch (StaleElementReferenceException)
                {
                }

                return null;
            });
        }
        catch (WebDriverTimeoutException)
        {
        }

        if (outcome == "success")
        {
            Log("Pas 'Dërgo' u shfaq ekrani i suksesit.");
            IWebElement headline = wait.Until(ExpectedConditions.ElementIsVisible(successHeadlineBy));
            Assert.That(headline.Text.Trim(), Does.Contain(successHeadline).IgnoreCase);

            var refEls = driver.FindElements(
                By.XPath("//h6[contains(normalize-space(.),'Numri referencë i aplikimit')]"));
            var trackEls = driver.FindElements(
                By.XPath("//button[contains(normalize-space(.),'GJURMO APLIKIMIN')]"));
            bool hasRef = refEls.Any(e =>
            {
                try { return e.Displayed; }
                catch (StaleElementReferenceException) { return false; }
            });
            bool hasTrack = trackEls.Any(e =>
            {
                try { return e.Displayed; }
                catch (StaleElementReferenceException) { return false; }
            });

            if (hasRef && hasTrack)
            {
                IWebElement referenceLine = refEls.First(e =>
                {
                    try { return e.Displayed; }
                    catch (StaleElementReferenceException) { return false; }
                });
                Assert.That(
                    referenceLine.Text.Trim(),
                    Does.Contain("Numri referencë i aplikimit është:").IgnoreCase);
                Assert.That(
                    referenceLine.Text.Trim(),
                    Does.Match("(?i)eALB-\\d+"));

                IWebElement trackBtn = trackEls.First(e =>
                {
                    try { return e.Displayed; }
                    catch (StaleElementReferenceException) { return false; }
                });
                Assert.That(trackBtn.Displayed, Is.True);
                Log("Sukses i verifikuar: headline, referenca eALB dhe butoni GJURMO APLIKIMIN.");
            }
            else
            {
                Log("Sukses i verifikuar: headline (eALB/GJURMO nuk u gjetën).");
            }
        }
        else if (outcome == "alert")
        {
            Log("Aplikimi u dërgua: sistemi u përgjigj dhe u shfaq modal paralajmërimi 'Kujdes'.");
            IWebElement alertModal = driver.FindElement(alertModalBy);
            IWebElement modalTitle = alertModal.FindElement(By.CssSelector("h2.alert-modal-title"));
            Assert.That(modalTitle.Text.Trim(), Is.EqualTo(alertExpectedTitle));

            var descEls = alertModal.FindElements(By.CssSelector(".alert-modal-description"));
            if (descEls.Count > 0)
            {
                Assert.That(descEls[0].Text.Trim(), Is.EqualTo(alertExpectedDescription));
            }

            IWebElement mbyllBtn = alertModal.FindElement(
                By.CssSelector("button.alert-modal-button--primary"));
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].scrollIntoView({block:'center'});",
                mbyllBtn);
            Thread.Sleep(300);
            try
            {
                mbyllBtn.Click();
            }
            catch (ElementClickInterceptedException)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", mbyllBtn);
            }
        }
        else
        {
            Assert.Fail(
                "Pas 'Dërgo' nuk u shfaq as ekrani i suksesit ('APLIKIMI JUAJ U DËRGUA ME SUKSES') " +
                "as modal paralajmërimi 'Kujdes' (.alert-modal-container).");
        }
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
