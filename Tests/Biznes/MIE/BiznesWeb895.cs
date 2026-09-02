using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Biznes.MIE;

[Category("MIE")]
[Category("895")]
public class BiznesWeb895 : BiznesTestBase
{
    protected override string ServiceCode => "895";
    protected override string? ServiceTitle => "NIPTWeb";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Kalimi i shkallës së vlerësimit të licencës së shoqërisë, nga e dytë në të parë (ndryshim shkalle vlerësimi për licencat e shoqërive) në fushën e vlerësimit të pasurive të paluajtshme(online)";
    private const string SignedPdf = @"C:\Users\Kreatx\Downloads\Signed_TEST_signed.pdf";

    [Test]
    public void NIPTWeb()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

        Log("Assert 4 hapa, hapi i pare aktiv");
        AssertActiveSteps(activeCount: 1);

        Log("Assert Step 1 Title");
        IWebElement step1Title = WaitForStepTitle("DETAJET E SUBJEKTIT");
        Assert.That(step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DETAJET E SUBJEKTIT"));

        Log("Assert te dhenat e subjektit");
        AssertLabel("nipt", "NIPT:");
        AssertReadonlyById("nipt", "M53330201S");
        AssertLabel("subjectName", "Emri i subjektit:");
        AssertReadonlyById("subjectName", "Migen Dërstila");
        AssertLabel("registrationDate", "Dt. e regjistrimit:");
        AssertReadonlyById("registrationDate", "30.09.2025");
        AssertLabel("subjectActivity", "Veprimtaria e subjektit:");
        AssertReadonlyById(
            "subjectActivity",
            "Tregtia me pakicë me porosi me mail ose nëpërmjet internetit dhe Sherbime të programimit informatik");
        AssertLabel("administrator", "Administratori:");
        AssertReadonlyById("administrator", "Migen  Luan  Dërstila |");
        AssertLabel("subjectStatus", "Statusi i subjektit:");
        AssertReadonlyById("subjectStatus", "Aktiv");
        AssertLabel("address", "Adresa:");
        AssertReadonlyById(
            "address",
            "Derstile; ; ; ; Gjinar; ; 0000; Elbasan,Elbasan,ELBASAN,Elbasan");

        Log("Assert butonat e navigimit Step 1");
        AssertBackAndContinue("Vazhdo");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 title");
        IWebElement step2Title = WaitForStepTitle("KONTAKTI");
        Assert.That(step2Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("KONTAKTI"));

        Log("Assert 4 hapa, dy te paret aktiv");
        AssertActiveSteps(activeCount: 2);

        Log("Assert te dhenat e kontaktit");
        AssertLabel("nrTel", "Nr. tel.:");
        IWebElement nrTel = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("nrTel")));
        Assert.That((nrTel.GetAttribute("value") ?? string.Empty).Trim(), Is.EqualTo(string.Empty));
        Assert.That(nrTel.GetAttribute("disabled"), Is.Null);

        AssertLabel("nrCel", "Nr. cel.:");
        AssertReadonlyById("nrCel", "+355684053531");
        AssertLabel("email", "Email:");
        AssertReadonlyById("email", "migen.derstila@kreatx.com");

        Log("Assert butonat e navigimit Step 2");
        AssertBackAndContinue("Vazhdo");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 title");
        IWebElement step3Title = WaitForStepTitle("DETAJET E APLIKIMIT");
        Assert.That(step3Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DETAJET E APLIKIMIT"));

        Log("Assert 4 hapa, tre te paret aktiv");
        AssertActiveSteps(activeCount: 3);

        Log("Assert fushat e detajeve te aplikimit");
        AssertLabel("licensePer", "Licencë për");
        var license = new SelectElement(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("licensePer"))));
        Assert.That(license.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(license.Options.Count, Is.EqualTo(4));
        Assert.That(license.Options[0].Text.Trim(), Is.EqualTo("Zgjidhni..."));
        Assert.That(license.Options[1].GetAttribute("value"), Is.EqualTo("NDERTES_TOKE_TRUALL"));
        Assert.That(license.Options[1].Text.Trim(), Is.EqualTo("Për ndërtesat dhe tokë truall"));
        Assert.That(license.Options[2].GetAttribute("value"), Is.EqualTo("TOKE_BUJQESORE_PYJE_LIVADH"));
        Assert.That(license.Options[2].Text.Trim(),
            Is.EqualTo("Për tokë buqësore, tokë pyjore, kullotë, livadh dhe toke të pafrytshme"));
        Assert.That(license.Options[3].GetAttribute("value"), Is.EqualTo("LINJA_TEKNOLOGJIKE"));
        Assert.That(license.Options[3].Text.Trim(), Is.EqualTo("Linja teknologjike, makineri e pajisje"));

        AssertLabel("sherim", "Shënim:");
        IWebElement sherim = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("sherim")));
        Assert.That(sherim.GetAttribute("maxlength"), Is.EqualTo("200"));
        Assert.That((sherim.GetAttribute("value") ?? string.Empty).Trim(), Is.EqualTo(string.Empty));

        AssertLabel("adresa", "Adresa:");
        IWebElement adresa = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("adresa")));
        Assert.That((adresa.GetAttribute("value") ?? string.Empty).Trim(), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 3");
        AssertBackAndContinue("Vazhdo");

        Log("Zgjidh licencen");
        license.SelectByValue("NDERTES_TOKE_TRUALL");

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 4 Title");
        IWebElement step4Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(step4Title.Text.Trim().ToUpperInvariant(), Does.Contain("DOKUMENTACIONI"));

        Log("Assert 4 hapa, te gjithe aktiv");
        AssertActiveSteps(activeCount: 4);

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës')]")).Displayed, Is.True);

        Log("Assert document-upload");
        AssertDocumentUpload(
            "fileKerkesaUpload",
            "Kërkesë e përfaqësuesit ligjor");
        AssertDocumentUpload(
            "fileKontrataUpload",
            "Kontratat midis përfaqësuesit ligjor dhe drejtuesve teknike");
        AssertDocumentUpload(
            "fileVetedeklarimiUpload",
            "Vetëdeklarimi i vlerësuesit/ve të shoqërisë");

        Log("Assert dokumentet e administrates");
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Ekstrakti i regjistrit tregtar për të dhënat e subjektit')]"))
            .Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Vërtetim, nga organet tatimore, për shlyerjen nga shoqëria të të gjitha detyrimeve tatimore')]"))
            .Displayed, Is.True);

        Log("Assert checkbox i deklarimit");
        IWebElement agreeCheck = wait.Until(ExpectedConditions.ElementExists(By.Id("agreeCheck")));
        Assert.That(agreeCheck.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='agreeCheck']")).Text.Trim(),
            Does.Contain("Mbledhja e dokumentacionit shoqërues të mësipërm"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='agreeCheck']")).Text.Trim(),
            Does.Contain("këto dokumente të sigurohen për ju nga nëpunësi i administratës"));

        Log("Assert butoni Dergo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.Text.Trim(), Does.Contain("Dërgo"));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Kliko Dergo pa ngarkuar dokumentin");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        IWebElement msgErrorDoc = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//*[contains(@class,'invalid-feedback') or contains(@class,'text-danger') or contains(@class,'ds-comp__validation')][normalize-space()!='']")));
        Assert.That(msgErrorDoc.Text.Trim(),
            Does.Contain("Plotësoni").IgnoreCase
                .Or.Contain("ngarkoni").IgnoreCase);

        Log("Ngarko dokumentet e detyrueshme");
        UploadDocument("fileKerkesaUpload", SignedPdf);
        UploadDocument("fileKontrataUpload", SignedPdf);

        Log("Zgjidh deklarimin");
        ClickMuiCheckbox("agreeCheck");
        Assert.That(driver.FindElement(By.Id("agreeCheck")).Selected, Is.True);

        Log("Kliko Dergo");
        ClickDerghoAfterDocumentationReady();
        AssertSuccessOrKujdesAfterDergo();

        Log("TEST PASSED");
    }

    private void OpenNewApplicationFromServicePage()
    {
        Log("Assert page header");
        IWebElement headerContainer = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("div.page-header-container")));
        Assert.That(headerContainer.Displayed, Is.True, "Page header nuk eshte visible");

        IWebElement serviceName = wait.Until(ExpectedConditions.ElementIsVisible(
            By.Id("serviceNameBreadcrumb")));
        Assert.That(serviceName.Displayed, Is.True, "Breadcrumb i sherbimit nuk eshte visible");
        Assert.That(serviceName.Text.Trim(), Is.EqualTo(ExpectedServiceName),
            "Emri i sherbimit nuk eshte i sakte");

        Log("Scroll deri sa butoni Perdor te jete i dukshem");
        By perdorLocator = By.CssSelector("button.use-service-button");
        IWebElement perdorBtn = wait.Until(ExpectedConditions.ElementExists(perdorLocator));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center', inline:'nearest'});",
            perdorBtn);
        Thread.Sleep(500);
        perdorBtn = wait.Until(ExpectedConditions.ElementToBeClickable(perdorLocator));
        Assert.That(perdorBtn.Displayed, Is.True, "Butoni Perdor nuk eshte visible per tu klikuar");

        Log("Kliko butonin Perdor");
        SafeClick(perdorLocator);

        Log("Kliko Aplikim i ri");
        By aplikimIRiLocator = By.XPath(
            "//div[contains(@class,'mbx-content') and @role='button'][.//h6[contains(@class,'mbx-title') and normalize-space()='Aplikim i ri']]");
        IWebElement aplikimIRi = wait.Until(ExpectedConditions.ElementIsVisible(aplikimIRiLocator));
        Assert.That(aplikimIRi.Displayed, Is.True, "Karta Aplikim i ri nuk eshte visible");
        IWebElement aplikimIRiTitle = aplikimIRi.FindElement(By.CssSelector("h6.mbx-title"));
        Assert.That(aplikimIRiTitle.Text.Trim(), Is.EqualTo("Aplikim i ri"),
            "Titulli i kartes nuk eshte Aplikim i ri");
        SafeClick(aplikimIRiLocator);
        Thread.Sleep(1500);
    }

    private IWebElement WaitForStepTitle(string expectedUpper)
    {
        return wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4"));
            foreach (var title in titles)
            {
                try
                {
                    if (title.Text.Trim().ToUpperInvariant().Contains(expectedUpper))
                        return title;
                }
                catch (StaleElementReferenceException)
                {
                }
            }
            return null;
        });
    }

    private void AssertLabel(string forId, string expectedText)
    {
        IWebElement label = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector($"label[for='{forId}']")));
        Assert.That(label.Text.Trim(), Does.Contain(expectedText.TrimEnd(':')));
    }

    private void AssertReadonlyById(string id, string expectedValue)
    {
        IWebElement input = wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
        Assert.That(input.GetAttribute("readonly") ?? input.GetAttribute("disabled"), Is.Not.Null,
            $"Fusha {id} duhet te jete readonly");
        Assert.That((input.GetAttribute("value") ?? string.Empty).Trim(), Is.EqualTo(expectedValue),
            $"Vlera e fushes {id} nuk eshte e sakte");
    }

    private void AssertActiveSteps(int activeCount, int totalCount = 4)
    {
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(totalCount));
        for (int i = 0; i < steps.Count; i++)
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
            if (i < activeCount)
                Assert.That(steps[i].GetAttribute("class"), Does.Contain("active"));
            else
                Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
        }
    }

    private void AssertBackAndContinue(string continueText)
    {
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Does.Contain(continueText));
    }

    private void ClickMuiCheckbox(string id)
    {
        IWebElement input = wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'}); arguments[0].click();",
            input);
        Thread.Sleep(300);
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {
        Assert.That(driver.FindElement(
            By.XPath($"//span[contains(normalize-space(),'{documentTitle}')]")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-895"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("15"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("15"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të ngarkuar dokumentin"));

        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të ngarkuar dokumentin"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='hint']")).Text.Trim(),
            Is.EqualTo("Formatet e lejuara: PDF. Madhësia maksimale: 15MB."));
    }

    private void UploadDocument(string uploadId, string filePath)
    {
        Assert.That(File.Exists(filePath), Is.True, "File nuk ekziston: " + filePath);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        ISearchContext shadow = docUpload.GetShadowRoot();
        IWebElement fileInput = shadow.FindElement(By.CssSelector("[data-role='file-input']"));
        fileInput.SendKeys(filePath);

        var uploadWait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
        uploadWait.Until(d =>
        {
            try
            {
                var root = d.FindElement(By.Id(uploadId)).GetShadowRoot();
                var fileRow = root.FindElement(By.CssSelector("[data-role='single-file']"));
                string cssClass = fileRow.GetAttribute("class") ?? string.Empty;
                string fileName = root.FindElement(By.CssSelector("[data-role='sf-name']")).Text.Trim();
                return cssClass.Contains("completed") || fileName.Length > 0;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });
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

    private void AssertSuccessOrKujdesAfterDergo()
    {
        const string successHeadline = "APLIKIMI JUAJ U DËRGUA ME SUKSES";

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
            Assert.That(modalTitle.Text.Trim(), Does.StartWith("Kujdes"));

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
    }
}
