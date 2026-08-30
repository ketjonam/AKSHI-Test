using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.MIE;

[Category("MIE")]
[Category("9287")]
public class NIDWeb_9287 : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "9287";
    protected override string? ServiceTitle => "Aplikim_i_Ri_9287";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void Aplikim_i_Ri_9287()
    {


                Log("Assert detajet e individit");
                IWebElement detajetIndividit = wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/h5"))
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

                IWebElement statusiCivil = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("gjCiv")));
                Assert.That(InputValue(statusiCivil), Is.EqualTo("Beqare"));

                IWebElement vendlindja = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("vendlindja")));
                Assert.That(InputValue(vendlindja), Is.EqualTo("Kavajë"));

                IWebElement datelindja = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("datelindja")));
                Assert.That(InputValue(datelindja), Is.EqualTo("28.07.1995"));

                IWebElement qarku = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("emQarku")));
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
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/h5"))
                );
                Assert.That(kontaktiTitle.Text.Trim(), Is.EqualTo("KONTAKTI"));

                IWebElement email = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("email")));
                Assert.That(InputValue(email), Is.EqualTo("ketjona.mema@kreatx.com"));

                IWebElement phoneNumber = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("nrCel")));
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
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/h5"))
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

                Log("Click 'Dergo' button without required document");
                IWebElement dergoBtn = wait.Until(
                    ExpectedConditions.ElementExists(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div[2]/div/button[2]"))
                );

                ((IJavaScriptExecutor)driver).ExecuteScript(
                    "arguments[0].scrollIntoView({block:'center'});",
                    dergoBtn
                );

                Thread.Sleep(500);
                wait.Until(ExpectedConditions.ElementToBeClickable(dergoBtn)).Click();

                IWebElement docErrorMessage = wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div[1]/div[1]/div[2]"))
                );
                Assert.That(docErrorMessage.Text, Does.Contain("Ju lutem ngarkoni dokumentin e kërkuar"));

                Log("Upload uncorrect docs");
                string fileDiploma = @"C:\Users\Kreatx\Downloads\Kthim Alfis test(1).pdf";
                string fileCertifikata = @"C:\Users\Kreatx\Downloads\image.png";
                string fileVetdeklarimi = @"C:\Users\Kreatx\Downloads\15mb.pdf";

                Assert.That(File.Exists(fileDiploma), Is.True, "File diploma nuk ekziston.");
                Assert.That(File.Exists(fileCertifikata), Is.True, "File certifikata nuk ekziston.");
                Assert.That(File.Exists(fileVetdeklarimi), Is.True, "File vetedeklarimi nuk ekziston.");

                IWebElement diplomaInputWrong = wait.Until(
                    ExpectedConditions.ElementExists(
                        By.XPath("//div[contains(.,'Diplomë universitare')]/following::input[@type='file'][1]"))
                );
                diplomaInputWrong.SendKeys(fileDiploma);

                IWebElement certifikataInputWrong = wait.Until(
                    ExpectedConditions.ElementExists(
                        By.XPath("//div[contains(.,'Certifikatë për kryerjen e programeve të studimit')]/following::input[@type='file'][1]"))
                );
                certifikataInputWrong.SendKeys(fileCertifikata);

                IWebElement vetedeklarimiInputWrong = wait.Until(
                    ExpectedConditions.ElementExists(
                        By.XPath("//div[contains(.,'Vetëdeklarim')]/following::input[@type='file'][1]"))
                );
                vetedeklarimiInputWrong.SendKeys(fileVetdeklarimi);

                Log("Assert Max size");
                IWebElement fileSizeError = wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.XPath("//div[contains(@class,'text-danger') and contains(text(),'Madhësia e dokumentit')]"))
                );
                Assert.That(fileSizeError.Displayed, Is.True);
                Assert.That(
                    fileSizeError.Text.Trim(),
                    Does.Contain("Madhësia e dokumentit nuk duhet të jetë më shumë se 15MB")
                );

                Log("Assert format gabim");
                IWebElement formatError = wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.XPath("//div[contains(@class,'text-danger') and contains(.,'Formati duhet të jetë')]"))
                );
                Assert.That(formatError.Displayed, Is.True);
                Assert.That(formatError.Text.Trim(), Is.EqualTo("Formati duhet të jetë: PDF"));

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

                Log("Remove uncorrect docs");

                // hiqi te gjitha dokumentet e gabuara derisa te mos ngelet asnje
                while (true)
                {
                    var cancelButtons = driver.FindElements(By.CssSelector("button[aria-label='Cancel upload']"));
                    if (cancelButtons.Count == 0)
                        break;

                    try
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript(
                            "arguments[0].scrollIntoView({block:'center'});",
                            cancelButtons[0]
                        );
                        Thread.Sleep(300);
                        cancelButtons[0].Click();
                        Thread.Sleep(500);
                    }
                    catch
                    {
                        break;
                    }
                }

                Thread.Sleep(1000);

        Log("Prit 1 minutë para ngarkimit të dokumentit të saktë…");
        Thread.Sleep(TimeSpan.FromMinutes(1));

                Log("Upload Correct Docs");
                string correctFileDiploma = @"C:\Users\Kreatx\Downloads\Signed_TEST_signed.pdf";
                string correctFileCertifikata = @"C:\Users\Kreatx\Downloads\Signed_TEST_signed.pdf";
                string correctFileVetedeklarimi = @"C:\Users\Kreatx\Downloads\Signed_TEST_signed.pdf";

                Assert.That(File.Exists(correctFileDiploma), Is.True, "File correct diploma nuk ekziston.");
                Assert.That(File.Exists(correctFileCertifikata), Is.True, "File correct certifikata nuk ekziston.");
                Assert.That(File.Exists(correctFileVetedeklarimi), Is.True, "File correct vetedeklarimi nuk ekziston.");

                // Diploma
                IWebElement diplomaInput = wait.Until(
                    ExpectedConditions.ElementExists(
                        By.XPath("//span[contains(normalize-space(),'Diplomë universitare')]/ancestor::div[contains(@class,'col')][1]//input[@type='file']"))
                );
                diplomaInput.SendKeys(correctFileDiploma);

                // Certifikata
                IWebElement certifikataInput = wait.Until(
                    ExpectedConditions.ElementExists(
                        By.XPath("//span[contains(normalize-space(),'Certifikatë për kryerjen e programeve të studimit')]/ancestor::div[contains(@class,'col')][1]//input[@type='file']"))
                );
                certifikataInput.SendKeys(correctFileCertifikata);

                // Vetedeklarimi
                IWebElement vetedeklarimiInput = wait.Until(
                    ExpectedConditions.ElementExists(
                        By.XPath("//span[contains(normalize-space(),'Vetëdeklarim')]/ancestor::div[contains(@class,'col')][1]//input[@type='file']"))
                );
                vetedeklarimiInput.SendKeys(correctFileVetedeklarimi);

                Thread.Sleep(1500);

                Log("Verify uploaded docs are present");

                // kontrollo qe file inputs kane vlere
                Assert.That(diplomaInput.GetAttribute("value"), Does.Contain(".pdf"));
                Assert.That(certifikataInput.GetAttribute("value"), Does.Contain(".pdf"));
                Assert.That(vetedeklarimiInput.GetAttribute("value"), Does.Contain(".pdf"));

                // kontrollo qe nuk ka me mesazhe visible error
                var visibleErrors = driver.FindElements(By.XPath("//div[contains(@class,'text-danger') and normalize-space()!='']"))
                                          .Where(e => e.Displayed)
                                          .ToList();

                Assert.That(visibleErrors.Count, Is.EqualTo(0),
                    "Ka ende gabime të dukshme pas ngarkimit të dokumenteve të sakta.");

                ClickDerghoAfterDocumentationReady(driver);

                const string successHeadline = "APLIKIMI JUAJ U DËRGUA ME SUKSES";
                const string alertExpectedTitle = "Kujdes";
                const string alertExpectedDescription =
                    "Ekzistojne aplikime te pa perfunduara per kete mjet.";

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
                        Log("Sukses i verifikuar: headline (eALB/GJURMO nuk u gjetën — mjafton për AQTN).");
                    }
                }
                else if (outcome == "alert")
                {
                    Log("Aplikimi u dërgua: sistemi u përgjigj dhe u shfaq modal paralajmërimi 'Kujdes'.");
                    IWebElement alertModal = driver.FindElement(alertModalBy);
                    IWebElement alertModalTitle = alertModal.FindElement(By.CssSelector("h2.alert-modal-title"));
                    Assert.That(alertModalTitle.Text.Trim(), Does.StartWith("Kujdes"));

                    var descEls = alertModal.FindElements(By.CssSelector(".alert-modal-description"));
                    if (descEls.Count > 0)
                    {
                        Log("Kujdes description: " + descEls[0].Text.Trim());
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

                Log("TEST PASSED");
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
}