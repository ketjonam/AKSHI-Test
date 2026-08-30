using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Biznes.DPSHTRR_AMS;

[Category("DPSHTRR-AMS")]
[Category("10091")]
[Category("FailCase")]
public class _10091_FailCase_ : BiznesTestBase
{
    protected override string ServiceCode => "10091";
    protected override string? ServiceTitle => "PajisjemeDAPperMakineriteeRenda_FailCase_ReturnsUiMessage";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void PajisjemeDAPperMakineriteeRenda_FailCase_ReturnsUiMessage()
    {



        Thread.Sleep(4000);

        Log("Kliko Vazhdo nga TË DHËNAT E SUBJEKTIT");
        SafeClick(By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/button[2]"));
        Thread.Sleep(4000);

        Log("Ploteso NID kandidati");
        IWebElement NID = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("/html/body/div/main/div[3]/div/div/div/div/form/div/div[1]/div/input")));
        NID.SendKeys("J55728107R");
        NID.SendKeys(Keys.Tab);
        Thread.Sleep(2000);

        Log("Kliko Vazhdo button");
        SafeClick(By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/button[2]"));
        Thread.Sleep(4000);

        Log("Kliko Vazhdo nga leja e drejtimit");
        SafeClick(By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/button[2]"));
        Thread.Sleep(4000);

        Log("Assert LLOJI I DAP");
        IWebElement Step4Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("/html/body/div/main/div[3]/div/div/div/div/h4")));
        Assert.That(Step4Title.Text.Trim(), Is.EqualTo("LLOJI I DAP"));

        Log("STIMULIM FAIL: pa zgjedhur llojin e DAP — provo Vazhdo/Dërgo.");
        try
        {
            ClickDerghoAfterDocumentationReady();
        }
        catch (Exception)
        {
            Log("Nuk ka Dërgo në LLOJI I DAP — kliko Vazhdo pa fusha të plota.");
            SafeClick(By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/button[2]"));
            Thread.Sleep(2000);

            string title = ReadVisibleMainTitle();
            bool onDocs =
                title.IndexOf("DOKUMENTACIONI", StringComparison.OrdinalIgnoreCase) >= 0
                || driver.FindElements(By.CssSelector("main input[type='file']")).Count > 0;

            if (onDocs)
            {
                Log("U arrit DOKUMENTACIONI pa upload — kliko Dërgo.");
                try
                {
                    ClickDerghoAfterDocumentationReady();
                }
                catch (Exception ex)
                {
                    Log("FindDergho dështoi, fallback: " + ex.Message);
                    SafeClick(By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/button[2]"));
                }
            }
        }

        AssertFailWithUiMessage();
    }

    private IWebElement FindDerghoButtonInMain()
    {

        var candidates = driver.FindElements(
            By.XPath("//main//button[contains(normalize-space(.), 'Dërgo') or contains(normalize-space(.), 'Dergo')]"));
        IWebElement pick = candidates.LastOrDefault(e =>
        {
            try
            {
                return e.Displayed;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });
        if (pick == null && candidates.Count > 0)
            pick = candidates[candidates.Count - 1];
        if (pick == null)
            throw new NoSuchElementException("Nuk u gjet butoni 'Dërgo' brenda main.");
        return pick;
    }

    private void ClickDerghoAfterDocumentationReady()
    {

        var sendWait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
        sendWait.Until(drv =>
        {
            try
            {
                var b = FindDerghoButtonInMain();
                return b.Displayed && b.Enabled;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        });

        IWebElement dergo = FindDerghoButtonInMain();
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center', inline:'nearest'});",
            dergo);
        Thread.Sleep(400);
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", dergo);
        Log("Klikuar butoni 'Dërgo' (JavaScript click pasi u aktivizua).");
    }

    private string ReadVisibleMainTitle()
    {

        foreach (var by in new[]
        {
            By.XPath("//main//h4"),
            By.XPath("//main//h5")
        })
        {
            try
            {
                var el = driver.FindElements(by).FirstOrDefault(e =>
                {
                    try { return e.Displayed && !string.IsNullOrWhiteSpace(e.Text); }
                    catch { return false; }
                });
                if (el != null)
                    return el.Text.Trim();
            }
            catch
            {
            }
        }

        return string.Empty;
    }

    private string CaptureVisibleUiMessageAfterDergo()
    {

        Thread.Sleep(1500);

        string[] preferredSelectors =
        {
            ".alert-modal-container",
            ".alert-modal-title",
            ".alert-modal-description",
            ".swal2-title",
            ".swal2-html-container",
            "[role='alert']",
            ".text-danger",
            ".invalid-feedback",
            ".toast-body",
            ".Toastify__toast-body"
        };

        foreach (string css in preferredSelectors)
        {
            try
            {
                foreach (var el in driver.FindElements(By.CssSelector(css)))
                {
                    try
                    {
                        if (!el.Displayed)
                            continue;
                        string t = (el.Text ?? string.Empty).Trim();
                        if (!string.IsNullOrWhiteSpace(t))
                            return t;
                    }
                    catch (StaleElementReferenceException)
                    {
                    }
                }
            }
            catch (WebDriverException)
            {
            }
        }

        object jsResult = ((IJavaScriptExecutor)driver).ExecuteScript(@"
            const root = document.querySelector('#root') || document.querySelector('main') || document.body;
            if (!root) return '';
            const danger = Array.from(root.querySelectorAll('.text-danger, .invalid-feedback, [role=""alert""], .alert'))
                .map(e => (e.innerText || '').trim())
                .filter(Boolean);
            if (danger.length) return danger.join(' | ');
            const headings = Array.from(root.querySelectorAll('h1,h2,h3,h4,h5,h6,p,span'))
                .map(e => (e.innerText || '').trim())
                .filter(t => t.length > 5 && t.length < 300);
            if (headings.length) return headings.slice(0, 8).join(' | ');
            return (root.innerText || '').trim().substring(0, 500);
        ");

        string fromJs = (jsResult?.ToString() ?? string.Empty).Trim();
        if (!string.IsNullOrWhiteSpace(fromJs))
            return fromJs;

        return "(Nuk u gjet asnjë mesazh i dukshëm në UI pas Dërgo.)";
    }

    private void AssertFailWithUiMessage()
    {

        By successHeadlineBy = By.XPath(
            "//h5[contains(normalize-space(.),'APLIKIMI JUAJ U DËRGUA ME SUKSES')]");
        By alertModalBy = By.CssSelector(".alert-modal-container");

        Thread.Sleep(2500);

        bool sawSuccess = false;
        try
        {
            sawSuccess = driver.FindElements(successHeadlineBy).Any(e =>
            {
                try { return e.Displayed; }
                catch (StaleElementReferenceException) { return false; }
            });
        }
        catch (WebDriverException)
        {
        }

        if (sawSuccess)
        {
            Assert.Fail(
                "Stimulimi i FAIL dështoi: u shfaq ekrani i suksesit (APLIKIMI JUAJ U DËRGUA ME SUKSES.) " +
                "ndërsa ky test pret që të mos shfaqet as sukses as Kujdes.");
        }

        try
        {
            var visibleAlert = driver.FindElements(alertModalBy).FirstOrDefault(e =>
            {
                try { return e.Displayed; }
                catch (StaleElementReferenceException) { return false; }
            });

            if (visibleAlert != null)
            {
                string title = visibleAlert.FindElement(By.CssSelector("h2.alert-modal-title")).Text.Trim();
                string desc = visibleAlert.FindElement(By.CssSelector(".alert-modal-description")).Text.Trim();
                string modalMessage = $"[{title}] {desc}";

                if (string.Equals(title, "Kujdes", StringComparison.OrdinalIgnoreCase))
                {
                    Assert.Fail(
                        "Stimulimi i FAIL dështoi: u shfaq modal 'Kujdes'. Mesazhi: " + modalMessage);
                }

                Log("Rasti FAIL — u shfaq modal (jo Kujdes): " + modalMessage);
                Assert.Fail(
                    "Rasti FAIL (as sukses, as Kujdes). Mesazhi që u shfaq në UI: " + modalMessage);
            }
        }
        catch (NoSuchElementException)
        {
        }
        catch (WebDriverException)
        {
        }

        string uiMessage = CaptureVisibleUiMessageAfterDergo();
        Log("Mesazhi i kapur nga UI (rasti FAIL): " + uiMessage);

        Assert.Fail(
            "Rasti FAIL (as sukses, as Kujdes). Mesazhi që u shfaq në UI: " + uiMessage);
    }
}