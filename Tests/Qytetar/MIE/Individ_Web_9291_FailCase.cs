using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.MIE;

[Category("MIE")]
[Category("9291")]
[Category("FailCase")]
public class Individ_Web_9291_FailCase : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "9291";
    protected override string? ServiceTitle => "Aplikim_i_Ri_9291_FailCase_ReturnsUiMessage";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void Aplikim_i_Ri_9291_FailCase_ReturnsUiMessage()
    {

Log("===== TEST START (FAIL CASE) =====");

                Log("Assert detajet e individit");
                IWebElement detajetIndividit = wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/h4"))
                );
                Assert.That(detajetIndividit.Text.Trim(), Is.EqualTo("DETAJET E INDIVIDIT"));

                IWebElement nid = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("nid")));
                Assert.That(InputValue(nid), Is.EqualTo(Settings.Qytetar.Username));

                IWebElement emri = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("emri")));
                Assert.That(InputValue(emri), Is.EqualTo("Ketjona"));

                IWebElement mbiemri = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("mbiemri")));
                Assert.That(InputValue(mbiemri), Is.EqualTo("Mema"));

                IWebElement atesia = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("atesia")));
                Assert.That(InputValue(atesia), Is.EqualTo("Mersin"));

                IWebElement amesia = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("memesia")));
                Assert.That(InputValue(amesia), Is.EqualTo("Aishe"));

                IWebElement gjinia = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("gjinia")));
                Assert.That(InputValue(gjinia), Is.EqualTo("Femër"));
                Thread.Sleep(500);

                IWebElement statusiCivil = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("gjCiv")));
                Assert.That(InputValue(statusiCivil), Is.EqualTo("Beqare"));

                IWebElement vendlindja = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("vendlindja")));
                Assert.That(InputValue(vendlindja), Is.EqualTo("Kavajë"));

                IWebElement datelindja = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("datelindja")));
                Assert.That(InputValue(datelindja), Is.EqualTo("28.7.1995"));

                IWebElement qarku = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("emQarku")));
                ((IJavaScriptExecutor)driver).ExecuteScript(
"arguments[0].scrollIntoView({block:'center'});",
qarku
);
                Assert.That(InputValue(qarku), Is.EqualTo("TIRANË"));

                IWebElement rrethi = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("emRrethi")));
                Assert.That(InputValue(rrethi), Is.EqualTo("KAVAJË"));

                Log("Click Vazhdo button - Step 1");
                IWebElement vazhdoBtn1 = wait.Until(
                    ExpectedConditions.ElementExists(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div/button[2]"))
                );

                ((IJavaScriptExecutor)driver).ExecuteScript(
                    "arguments[0].scrollIntoView({block:'center'});",
                    vazhdoBtn1
                );

                Thread.Sleep(500);
                wait.Until(ExpectedConditions.ElementToBeClickable(vazhdoBtn1)).Click();

                Log("Assert Kontakti");
                IWebElement kontaktiTitle = wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/h4"))
                );
                Assert.That(kontaktiTitle.Text.Trim(), Is.EqualTo("KONTAKTI"));

                IWebElement email = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("email")));
                Assert.That(InputValue(email), Is.EqualTo("ketjona.mema@kreatx.com"));

                IWebElement phoneNumber = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("phoneNumber")));
                Assert.That(InputValue(phoneNumber), Is.EqualTo("0676041404"));

                Thread.Sleep(500);
                Log("Click Vazhdo button - Step 2");
                IWebElement vazhdoBtn2 = wait.Until(
                    ExpectedConditions.ElementExists(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div/button[2]"))
                );

                ((IJavaScriptExecutor)driver).ExecuteScript(
                    "arguments[0].scrollIntoView({block:'center'});",
                    vazhdoBtn2
                );

                Thread.Sleep(500);
                wait.Until(ExpectedConditions.ElementToBeClickable(vazhdoBtn2)).Click();

                Log("Assert Step 3");
                IWebElement step3Title = wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/h4"))
                );
                Assert.That(step3Title.Text.Trim(), Is.EqualTo("DETAJET E APLIKIMIT"));

                Log("Click 'Vazhdo' button - Step 3");
                driver.FindElement(By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div/button[2]")).Click();

                Log("Assert Dokumentacioni");
                IWebElement step4Title = wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/h4"))
                );
                Assert.That(step4Title.Text.Trim(), Is.EqualTo("DOKUMENTACIONI"));

                Log("STIMULIM FAIL: nuk ngarkohen dokumente (qëllimisht).");
                Thread.Sleep(1000);

                Log("Kliko butonin dergo pa ngarkuar dokumentat e detyrueshme");
                ClickDerghoAfterDocumentationReady(driver);

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

                string uiMessage = CaptureVisibleUiMessageAfterDergo(driver);
                Log("Mesazhi i kapur nga UI (rasti FAIL): " + uiMessage);

                Assert.Fail(
                    "Rasti FAIL (as sukses, as Kujdes). Mesazhi që u shfaq në UI: " + uiMessage);
    }

    private IWebElement FindDerghoButtonInMain(IWebDriver driver)
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

    private void ClickDerghoAfterDocumentationReady(IWebDriver driver)
    {

        var sendWait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
        sendWait.Until(drv =>
        {
            try
            {
                var b = FindDerghoButtonInMain(driver);
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

        IWebElement dergo = FindDerghoButtonInMain(driver);
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center', inline:'nearest'});",
            dergo);
        Thread.Sleep(400);
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", dergo);
        Log("Klikuar butoni 'Dërgo' (JavaScript click pasi u aktivizua).");
    }

    private string CaptureVisibleUiMessageAfterDergo(IWebDriver driver)
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