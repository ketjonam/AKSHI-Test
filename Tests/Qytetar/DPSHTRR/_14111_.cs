using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.DPSHTRR;

[Category("DPSHTRR")]
[Category("14111")]
public class _14111_ : QytetarNidJ257TestBase
{
    protected override string ServiceCode => "14111";
    protected override string? ServiceTitle => "KonvertimLejesDrejtimit";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void KonvertimLejesDrejtimit()
    {





        Log("Assert Step 1 Title");
        IWebElement step2Title = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/main/div[3]/div/div/div/div/h4")));
        Assert.That(step2Title.Text.Trim(), Is.EqualTo("INFORMACION MBI APLIKANTIN"));
        Thread.Sleep(4000);
        Log("Assert Te dhenat individuale");
        IWebElement NID = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("nid")));
        Assert.That(NID.GetAttribute("value").Trim(), Is.EqualTo(CitizenNid));

        IWebElement Emri = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("emri")));
        Assert.That(Emri.GetAttribute("value").Trim(), Is.EqualTo("Daniela"));

        IWebElement Mbiemri = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("mbiemri")));
        Assert.That(Mbiemri.GetAttribute("value").Trim(), Is.EqualTo("Mema"));

        IWebElement Atesia = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("atesia")));
        Assert.That(Atesia.GetAttribute("value").Trim(), Is.EqualTo("Mersin"));

        IWebElement Datelindja = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("datelindja")));
        Assert.That(Datelindja.GetAttribute("value").Trim(), Is.EqualTo("30/07/1992"));

        Log("Kliko Vazhdo buton");
        SafeClick(By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/button[2]"));

        Thread.Sleep(3000);

        Log("Assert Step2 title");
        IWebElement Step2Title = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/main/div[3]/div/div/div/div/h4")));
        Assert.That(Step2Title.Text.Trim(), Is.EqualTo("DOKUMENTACIONI"));

        Thread.Sleep(500);

        Log("Kliko butonin dergo pa ngarkuar dokumentat e detyrueshmne");
        SafeClick(By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div[3]/button[2]"));

        Log("Assert mesazhin e gabimit per dokumentet e detyrueshme");
        IWebElement mesazhiGabim = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div[1]/div[2]/div/div[2]/div[2]")
        ));
        Assert.That(mesazhiGabim.Text.Trim(), Is.EqualTo("Ju lutem ngarkoni dokumentin e kërkuar"));

        Log("Ngarko dokumentet jo te sakta");
        string LejeDrejtimi = @"C:\Users\Kreatx\Downloads\Kthim Alfis test(1).pdf";
        string CertifikateMjekesore = @"C:\Users\Kreatx\Downloads\15mb.pdf";

        Assert.That(File.Exists(LejeDrejtimi), Is.True, "File Dokumenti nuk ekziston.");
        Assert.That(File.Exists(CertifikateMjekesore), Is.True, "File Certifikate Mjekesore nuk ekziston.");

        IWebElement LejeDrejtimiInputWrong = wait.Until(
          ExpectedConditions.ElementExists(
             By.XPath("//div[contains(.,'Leje drejtimit të huaj të vlefshme')]/following::input[@type='file'][1]"))
        );
        LejeDrejtimiInputWrong.SendKeys(LejeDrejtimi);

        IWebElement CertifikateMjekesoreInputWrong = wait.Until(
          ExpectedConditions.ElementExists(
             By.XPath("//div[contains(.,'Certifikatë mjekësore')]/following::input[@type='file'][1]"))
        );
        CertifikateMjekesoreInputWrong.SendKeys(CertifikateMjekesore);

        Log("Assert uncorrect doc name");
        IWebElement fileDocNameError = wait.Until(
            ExpectedConditions.ElementIsVisible(
                By.XPath("//div[contains(@class,'text-danger') and contains(text(),'Emri i dokumentit është i pavlefshëm')]"))
        );
        Assert.That(fileDocNameError.Displayed, Is.True);
        Assert.That(
            fileDocNameError.Text.Trim(),
            Does.Contain("Emri i dokumentit është i pavlefshëm")
        );

        Log("Assert uncorrect file size");
        IWebElement fileSizeError = wait.Until(
            ExpectedConditions.ElementIsVisible(
                By.XPath("//div[contains(@class,'text-danger') and contains(text(),'Madhësia e dokumentit nuk duhet të jetë më shumë se  5MB')]"))
        );
        Assert.That(fileSizeError.Displayed, Is.True);
        Assert.That(
            fileSizeError.Text.Trim(),
            Does.Contain("Madhësia e dokumentit nuk duhet të jetë më shumë se 5MB")
        );

        Log("Remove uncorrect docs");
        RemoveAllUploadedDocs();
        Thread.Sleep(1500);

        Log("Prit 1 minutë para ngarkimit të dokumenteve të sakta…");
        Thread.Sleep(TimeSpan.FromMinutes(1));

        Log("Ngarko dok e sakte");

        const string signedPdfPath = @"C:\Users\Kreatx\Downloads\Signed_TEST_signed.pdf";
        LejeDrejtimi = signedPdfPath;
        CertifikateMjekesore = signedPdfPath;
        Assert.That(File.Exists(LejeDrejtimi), Is.True, "File Dokumenti nuk ekziston.");
        Assert.That(File.Exists(CertifikateMjekesore), Is.True, "File Certifikate Mjekesore nuk ekziston.");

        IWebElement LejeDrejtimiInput = wait.Until(
         ExpectedConditions.ElementExists(
             By.XPath("//div[contains(.,'Leje drejtimit të huaj të vlefshme, e lëshuar nga shteti përkatës')]/following::input[@type='file'][1]"))
       );
        LejeDrejtimiInput.SendKeys(LejeDrejtimi);

        IWebElement CertifikateMjekesoreInput = wait.Until(
         ExpectedConditions.ElementExists(
             By.XPath("//div[contains(.,'Certifikatë mjekësore')]/following::input[@type='file'][1]"))
        );
        CertifikateMjekesoreInput.SendKeys(CertifikateMjekesore);

        Log("Kliko checkbox e klauzoles deklarative");
        SafeClick(By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div[2]/div/span"));

        Thread.Sleep(2000);

        Log("Kliko Dergo Button");
        ClickDerghoAfterDocumentationReady();

        const string successHeadline = "APLIKIMI JUAJ U DËRGUA ME SUKSES.";

        By successHeadlineBy = By.XPath(
            "//h5[contains(normalize-space(.),'APLIKIMI JUAJ U DËRGUA ME SUKSES')]");
        By alertModalBy = By.CssSelector(".alert-modal-container");

        string outcome = null;
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

            IWebElement referenceLine = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//h6[contains(normalize-space(.),'Numri referencë i aplikimit')]")));
            Assert.That(
                referenceLine.Text.Trim(),
                Does.Contain("Numri referencë i aplikimit është:").IgnoreCase);
            Assert.That(
                referenceLine.Text.Trim(),
                Does.Match("(?i)eALB-\\d+"));

            IWebElement trackBtn = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//button[contains(normalize-space(.),'GJURMO APLIKIMIN')]")));
            Assert.That(trackBtn.Displayed, Is.True);
            Log("Sukses i verifikuar: headline, referenca eALB dhe butoni GJURMO APLIKIMIN.");
        }
        else if (outcome == "alert")
        {
            Log("Aplikimi u dërgua: sistemi u përgjigj dhe u shfaq modal paralajmërimi 'Kujdes'.");
            IWebElement alertModal = driver.FindElement(alertModalBy);
            IWebElement modalTitle = alertModal.FindElement(By.CssSelector("h2.alert-modal-title"));
            IWebElement modalDesc = alertModal.FindElement(By.CssSelector(".alert-modal-description"));
            Assert.That(modalTitle.Text.Trim(), Is.EqualTo("Kujdes"));
            Assert.That(
                modalDesc.Text.Trim(),
                Is.EqualTo("Ekzistojne aplikime te pa perfunduara per kete mjet."));

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
                "Pas 'Dërgo' nuk u shfaq as ekrani i suksesit ('APLIKIMI JUAJ U DËRGUA ME SUKSES.') " +
                "as modal paralajmërimi 'Kujdes' (.alert-modal-container).");
        }

        Log("TEST PASSED");
    }

    private void BlurActiveElement()
    {

        try
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "if(document.activeElement){document.activeElement.blur();}"
            );
        }
        catch (Exception ex)
        {
            Log("BlurActiveElement error: " + ex.Message);
        }
    }

    private void ClearFilterInput(By locator)
    {

        Log("Clear filter input with Ctrl+A + Delete");
        IWebElement input = wait.Until(ExpectedConditions.ElementIsVisible(locator));

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            input
        );

        input.Click();
        Thread.Sleep(300);

        input.SendKeys(Keys.Control + "a");
        Thread.Sleep(200);
        input.SendKeys(Keys.Delete);
        Thread.Sleep(500);

        string currentValue = input.GetAttribute("value") ?? string.Empty;
        Log("Filter value after keyboard clear: '" + currentValue + "'");

        if (!string.IsNullOrEmpty(currentValue))
        {
            Log("Keyboard clear nuk mjaftoi, provoj me JS");
            ((IJavaScriptExecutor)driver).ExecuteScript(@"
                const el = arguments[0];
                el.value = '';
                el.dispatchEvent(new Event('input', { bubbles: true }));
                el.dispatchEvent(new Event('change', { bubbles: true }));
            ", input);

            Thread.Sleep(500);
        }

        BlurActiveElement();
        Thread.Sleep(800);

        input = wait.Until(ExpectedConditions.ElementIsVisible(locator));
        currentValue = input.GetAttribute("value") ?? string.Empty;
        Log("Filter value final: '" + currentValue + "'");
    }

    private void WaitUntilOptionExists(By selectLocator, string optionValue)
    {

        wait.Until(driver =>
        {
            try
            {
                var selectElement = new SelectElement(driver.FindElement(selectLocator));
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
        foreach (var option in select.Options)
        {
            Log($"Option Text = '{option.Text.Trim()}', Value = '{option.GetAttribute("value")}'");
        }

        select.SelectByValue(optionValue);
        Thread.Sleep(1000);
    }

    private void RemoveAllUploadedDocs()
    {

        Log("Hiq dok jo te sakta");

        int safetyCounter = 0;

        while (true)
        {
            var deleteButtons = driver.FindElements(By.CssSelector("button[aria-label='Delete file']"));

            Log("Nr. i butonave Delete file: " + deleteButtons.Count);

            var deleteBtn = deleteButtons.FirstOrDefault(b =>
            {
                try
                {
                    return b.Displayed && b.Enabled;
                }
                catch
                {
                    return false;
                }
            });

            if (deleteBtn == null)
            {
                Log("Nuk ka me dokumente per te hequr");
                break;
            }

            try
            {
                ((IJavaScriptExecutor)driver).ExecuteScript(
                    "arguments[0].scrollIntoView({ block: 'center' });",
                    deleteBtn
                );

                Thread.Sleep(300);

                try
                {
                    deleteBtn.Click();
                }
                catch
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", deleteBtn);
                }

                Log("U hoq nje dokument jo i sakte");
                Thread.Sleep(1000);
            }
            catch (StaleElementReferenceException)
            {
                Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                Log("Gabim gjate heqjes se dokumentit: " + ex.Message);
                break;
            }

            safetyCounter++;
            if (safetyCounter >= 10)
            {
                Log("Ndalo heqjen e dokumenteve per shkak te safetyCounter");
                break;
            }
        }

        Log("Te gjitha dok jo te sakta u hoqen");
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
}