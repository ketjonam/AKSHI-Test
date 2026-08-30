using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.DPSHTRR;

[Category("DPSHTRR")]
[Category("10098")]
[Category("FailCase")]
public class _10098_FailCase_ : QytetarNidJ257TestBase
{
    protected override string ServiceCode => "10098";
    protected override string? ServiceTitle => "Aplikim_Per_Nderrim_LejeQarkullimi_FailCase_ReturnsUiMessage";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void Aplikim_Per_Nderrim_LejeQarkullimi_FailCase_ReturnsUiMessage()
    {


string titleXpath = "/html/body/div/main/div[3]/div/div/div/div/h4";

        

        Log("Assert Title");
        IWebElement titleElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(titleXpath)));
        Assert.That(titleElement.Displayed, Is.True, "Titulli nuk eshte visible");

        Log("Zgjidh te dhenat mbi Drejtorine Rajonale");
        SelectByValueSafe(By.Name("rajoni"), "11");
        SelectByValueSafe(By.Name("bashkia"), "TIR");
        SelectByValueSafe(By.Name("njesiaAdm"), "NJESIADMINNR1");
        SelectByValueSafe(By.Name("nenNjesia"), "NJESIABASHKNR1-TIR");

        Log("Kliko Vazhdo");
        SafeClick(By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/button[2]"));
        Thread.Sleep(4000);

        Log("Assert Step 2 Title");
        IWebElement step2Title = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/main/div[3]/div/div/div/div/h4")));
        Assert.That(step2Title.Text.Trim(), Is.EqualTo("TË DHËNAT E APLIKANTIT"));
        Thread.Sleep(4000);

        Log("Assert Te dhenat individuale");
        Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Name("nid"))).GetAttribute("value").Trim(), Is.EqualTo(CitizenNid));
        Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Name("emri"))).GetAttribute("value").Trim(), Is.EqualTo("Daniela"));
        Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Name("mbiemri"))).GetAttribute("value").Trim(), Is.EqualTo("Mema"));
        Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Name("atesia"))).GetAttribute("value").Trim(), Is.EqualTo("Mersin"));
        Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Name("datelindja"))).GetAttribute("value").Trim(), Is.EqualTo("1992-07-30"));
        Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Name("vendlindja"))).GetAttribute("value").Trim(), Is.EqualTo("Kavajë"));
        Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Name("email"))).GetAttribute("value").Trim(), Is.EqualTo("ketjona.mema@kreatx.com"));
        Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Name("telNo"))).GetAttribute("value").Trim(), Is.EqualTo("0676041404"));

        Log("Kliko Vazhdo buton");
        SafeClick(By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/button[2]"));

        Log("Assert Step3 title");
        IWebElement Step3Title = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/main/div[3]/div/div/div/div/h4")));
        Assert.That(Step3Title.Text.Trim(), Is.EqualTo("INFORMACION SPECIFIK MBI APLIKIMIN"));

        Log("Plotëso fushat e kërkuara dhe vazhdo");
        string uniqueVin = "FAIL98" + DateTime.Now.ToString("yyyyMMddHHmmss");
        string uniqueLicence = "LIC98" + DateTime.Now.ToString("HHmmss");
        Log($"VIN unik për stimulim FAIL: {uniqueVin}, licenceNo: {uniqueLicence}");
        driver.FindElement(By.Name("vin")).SendKeys(uniqueVin);
        driver.FindElement(By.Name("licenceNo")).SendKeys(uniqueLicence);
        SelectByValueSafe(By.Name("vehicleType"), "M");
        SafeClick(By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/button[2]"));

        Log("Assert Step4 title");
        IWebElement Step4Title = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/main/div[3]/div/div/div/div/h4")));
        Assert.That(Step4Title.Text.Trim(), Is.EqualTo("DOKUMENTACIONI"));

        Log("STIMULIM FAIL: nuk ngarkohen dokumente (qëllimisht), që të mos shfaqet as sukses as Kujdes.");
        Thread.Sleep(3000);

        Log("Kliko CHECKBOX (pa dokumente të ngarkuara)");
        IWebElement checkbox = wait.Until(ExpectedConditions.ElementExists(By.Id("consentCheckbox")));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            checkbox);
        Thread.Sleep(500);
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", checkbox);
        Thread.Sleep(800);

        Log("Kliko Dergo Button");
        ClickDerghoAfterDocumentationReady();

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

            if (visibleAlert is not null)
            {
                string title = visibleAlert.FindElement(By.CssSelector("h2.alert-modal-title")).Text.Trim();
                string desc = visibleAlert.FindElement(By.CssSelector(".alert-modal-description")).Text.Trim();
                string modalMessage = $"[{title}] {desc}";

                if (string.Equals(title, "Kujdes", StringComparison.OrdinalIgnoreCase))
                {
                    Assert.Fail(
                        "Stimulimi i FAIL dështoi: u shfaq modal 'Kujdes' (aplikime ekzistuese). " +
                        $"Mesazhi: {modalMessage}");
                }

                Log("Rasti FAIL — u shfaq modal (p.sh. Gabim, jo Kujdes): " + modalMessage);
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

    private void WaitUntilOptionExists(By selectLocator, string optionValue)
    {

        wait.Until(drv =>
        {
            try
            {
                var selectElement = new SelectElement(drv.FindElement(selectLocator));
                return selectElement.Options.Any(o =>
                    string.Equals(
                        (o.GetAttribute("value") ?? string.Empty).Trim(),
                        optionValue,
                        StringComparison.OrdinalIgnoreCase
                    ));
            }
            catch
            {
                return false;
            }
        });
    }

    private void SelectByValueSafe(By selectLocator, string optionValue)
    {

        WaitUntilOptionExists(selectLocator, optionValue);

        IWebElement dropdown = wait.Until(ExpectedConditions.ElementIsVisible(selectLocator));

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            dropdown
        );

        Thread.Sleep(500);

        var select = new SelectElement(dropdown);
        Log($"Po zgjedh value '{optionValue}' tek {selectLocator}");
        select.SelectByValue(optionValue);
        Thread.Sleep(1000);
    }

    private IWebElement FindDerghoButtonInMain()
    {

        var candidates = driver.FindElements(
            By.XPath("//main//button[contains(normalize-space(.), 'Dërgo') or contains(normalize-space(.), 'Dergo')]"));
        IWebElement? pick = candidates.LastOrDefault(e =>
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
        if (pick is null && candidates.Count > 0)
            pick = candidates[^1];
        if (pick is null)
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

        object? jsResult = ((IJavaScriptExecutor)driver).ExecuteScript(@"
            const parts = [];
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
}