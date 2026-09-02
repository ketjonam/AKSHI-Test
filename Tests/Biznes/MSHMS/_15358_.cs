using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Biznes.MSHMS;

[Category("MSHMS")]
[Category("15358")]
public class _15358_ : BiznesTestBase
{
    protected override string ServiceCode => "15358";
    protected override string? ServiceTitle => "RilidhjeKontratemeInstitutetShendetesoreJoPublike";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Aplikim për lidhje/rilidhje kontrate me Institucionet Shëndetësore Jo Publike";
    private const string SignedPdf = @"C:\Users\Kreatx\Downloads\Signed_TEST_signed.pdf";

    [Test]
    public void RilidhjeKontratemeInstitutetShendetesoreJoPublike()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

        Log("Assert 3 hapa, hapi i pare aktiv");
        AssertActiveSteps(activeCount: 1);

        Log("Assert Step 1 Title");
        IWebElement step1Title = WaitForStepTitle("TË DHËNAT E SUBJEKTIT");
        Assert.That(step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E SUBJEKTIT"));

        Log("Assert alert per licencen QKB");
        IWebElement licenseAlert = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("div.alert.alert-danger[role='alert']")));
        Assert.That(licenseAlert.Text.Trim(), Does.Contain(
            "Për institucionin tuaj shëndetësor jo publik nuk u gjetën të dhëna lidhur me licencën e lëshuar nga QKB"));

        Log("Assert seksioni Subjekti");
        Assert.That(driver.FindElement(
            By.XPath("//h4[contains(@class,'text-uppercase') and contains(.,'Subjekti')]")).Displayed, Is.True);

        Log("Assert te dhenat e subjektit");
        AssertReadonlyByName("nipt", "M53330201S");
        AssertReadonlyByName("companyName", "Migen Dërstila");
        Assert.That(FindInputByName("secondaryNipt").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        AssertReadonlyByName("licenseNumber", string.Empty);
        AssertReadonlyByName("licenceDate", string.Empty);
        Assert.That(FindInputByName("bankName").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindInputByName("accountNumber").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        AssertReadonlyByName("phone", "+355684053531");
        AssertReadonlyByName("email", "migen.derstila@kreatx.com");
        AssertReadonlyByName("address", "Derstile; ; ; ; Gjinar; ; 0000; Elbasan");

        Log("Assert Ka detyrime në organet tatimore");
        Assert.That(driver.FindElement(
            By.XPath("//label[contains(@class,'form-label') and contains(.,'Ka detyrime në organet tatimore')]"))
            .Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id("hasTaxObligationsYes")).GetAttribute("value"), Is.EqualTo("true"));
        Assert.That(driver.FindElement(By.Id("hasTaxObligationsNo")).GetAttribute("value"), Is.EqualTo("false"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='hasTaxObligationsYes']")).Text.Trim(),
            Is.EqualTo("Po"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='hasTaxObligationsNo']")).Text.Trim(),
            Is.EqualTo("Jo"));

        Log("Assert seksioni Pronari/Administratori");
        Assert.That(driver.FindElement(
            By.XPath("//h4[contains(@class,'text-uppercase') and contains(.,'Pronari/Administratori')]"))
            .Displayed, Is.True);
        AssertReadonlyByName("administratorNid", "J70903019W");
        AssertReadonlyByName("administratorName", "Migen Dërstila");

        Log("Assert butonat e navigimit Step 1");
        AssertBackAndContinue("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error message per fushat e detyrueshme");
        IWebElement msgErrorRequired = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//label[contains(.,'Banka')]/following::*[contains(@class,'invalid-feedback') or contains(@class,'text-danger')][1]")));
        Assert.That(msgErrorRequired.Text.Trim(), Is.EqualTo("Plotësoni fushën për të vazhduar"));

        Log("Ploteso fushat e detyrueshme");
        FillByName("bankName", "test");
        FillByName("accountNumber", "test123");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 title");
        IWebElement step2Title = WaitForStepTitle("TË DHËNAT E APLIKIMIT");
        Assert.That(step2Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E APLIKIMIT"));

        Log("Assert 3 hapa, dy te paret aktiv");
        AssertActiveSteps(activeCount: 2);

        Log("Assert Lista e sherbimeve");
        Assert.That(driver.FindElement(
            By.XPath("//h5[contains(.,'1. Lista e shërbimeve që ofrohen')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//strong[normalize-space()='PO/JO']")).Displayed, Is.True);
        AssertCheckboxExists("dialysisService", "a. Shërbim dialize");
        AssertCheckboxExists("fistulaService", "b. Shërbim të vendosjes së fistules së thjeshtë/grafit të thjeshtë");
        AssertCheckboxExists("cardiologyService", "c. Shërbim i kardiologjisë");
        AssertCheckboxExists("cardiacSurgeryService", "d. Shërbim i kardiokirurgjisë");
        AssertCheckboxExists("kidneyTransplantService", "e. Shërbim i transplantit të veshkës dhe flakjes akute");
        AssertCheckboxExists("cochlearImplantService", "f. Shërbimi i implantit koklear");

        Log("Assert pyetjet 2-6");
        AssertCheckboxExists("hasComputerInfrastructure",
            "2. Institucioni është i pajisur me infrastrukturën e nevojshme kompjuterike");
        AssertCheckboxExists("provides24HourService",
            "3. Institucioni siguron dhe garanton shërbim të urgjencës 24 orë");
        AssertCheckboxExists("specialistsEmployedInPublicInstitutions",
            "4. Mjekët specialistë, të cilët do të kryejnë procedurat e paketave shëndetësore");
        AssertCheckboxExists("specialistsBrokeMinistryAgreement",
            "5. Mjekët specialistë, të cilët do të kryejnë procedurat e paketave shëndetësore");
        AssertCheckboxExists("institutionInBankruptcy",
            "6. Institucioni shëndetësor jopublik është në proces falimentimi dhe likuidimi");

        Log("Assert opsionet e drejtorise");
        IWebElement directorateSelect = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("select[name='selectedDirectorate']")));
        var directorate = new SelectElement(directorateSelect);
        Assert.That(directorate.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(directorate.Options.Count, Is.EqualTo(13));
        Assert.That(directorate.Options[1].GetAttribute("value"), Is.EqualTo("DRF Durres"));
        Assert.That(directorate.Options[2].GetAttribute("value"), Is.EqualTo("DRF Tirane"));
        Assert.That(directorate.Options[3].GetAttribute("value"), Is.EqualTo("DRF Elbasan"));
        Assert.That(directorate.Options[4].GetAttribute("value"), Is.EqualTo("DRF Korce"));
        Assert.That(directorate.Options[5].GetAttribute("value"), Is.EqualTo("DRF Vlore"));
        Assert.That(directorate.Options[6].GetAttribute("value"), Is.EqualTo("DRF Shkoder"));
        Assert.That(directorate.Options[7].GetAttribute("value"), Is.EqualTo("DRF Fier"));
        Assert.That(directorate.Options[8].GetAttribute("value"), Is.EqualTo("DRF Gjirokaster"));
        Assert.That(directorate.Options[9].GetAttribute("value"), Is.EqualTo("DRF Lezhe"));
        Assert.That(directorate.Options[10].GetAttribute("value"), Is.EqualTo("DRF Berat"));
        Assert.That(directorate.Options[11].GetAttribute("value"), Is.EqualTo("DRF Diber"));
        Assert.That(directorate.Options[12].GetAttribute("value"), Is.EqualTo("DRF Kukes"));

        Log("Assert butonat e navigimit Step 2");
        AssertBackAndContinue("Vazhdo");

        Log("Kliko Vazhdo pa zgjedhur drejtorine");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        IWebElement msgErrorApl = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//label[contains(.,'Përzgjidhni drejtorinë')]/following::*[contains(@class,'invalid-feedback') or contains(@class,'text-danger')][1]")));
        Assert.That(msgErrorApl.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));

        Log("Zgjidh DRF Tirane");
        new SelectElement(wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("select[name='selectedDirectorate']")))).SelectByValue("DRF Tirane");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement step3Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(step3Title.Text.Trim().ToUpperInvariant(), Does.Contain("DOKUMENTACIONI"));

        Log("Assert 3 hapa, te gjithe aktiv");
        AssertActiveSteps(activeCount: 3);

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumentet që ngarkoh')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumentet që sigurohen nga')]")).Displayed, Is.True);

        Log("Assert document-upload");
        AssertDocumentUpload("vertetim_bankarUpload",
            "Vërtetim të lëshuar nga banka e nivelit të dytë për llogarinë bankare");
        AssertDocumentUpload("license_laboratorUpload",
            "Kopje të licensës për laboratorin kliniko-biokimik e mikrobiologjik ose marrëveshje me një laborator për kryerjen e analizave");
        AssertDocumentUpload("license_drejtues_teknikUpload",
            "Kopje së licensës së drejtuesit teknik të laboratorit");
        AssertDocumentUpload("license_mjekUpload",
            "Kopje të licensës së ushtrimit të profesionit për mjekët e punësuar me kohë të plotë, si dhe kartës së identitetit");
        AssertDocumentUpload("kontrata_mjek_specialistUpload",
            "Kontratat e punës të mjekëve specialistë të kontraktuar në shërbimet mjekësore sipas shërbimit që ofrohen nga spitali");

        Log("Assert dokumentet e administrates");
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'Licenca e ushtrimit të aktivitetit të lëshuar nga Qendra Kombëtare e Biznesit')]"))
            .Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'Certifikata e Regjistrimit NUIS/NIPT')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'Vërtetim nga Drejtoria Tatimore')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'Kopje e Certifikatës së pronësisë dhe lejes së qarkullimit për autoambulancën')]"))
            .Displayed, Is.True);

        Log("Assert checkbox i deklarimit");
        IWebElement agreeCheck = wait.Until(ExpectedConditions.ElementExists(By.Id("agreeCheck")));
        Assert.That(agreeCheck.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='agreeCheck']")).Text.Trim(),
            Does.Contain("Deklaroj nën përgjegjësinë time të plotë se:"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='agreeCheck']")).Text.Trim(),
            Does.Contain("Të gjitha të dhënat e mësipërme janë të vërteta"));

        Log("Assert butoni Dergo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.Text.Trim(), Does.Contain("Dërgo"));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Ngarko dokumentet e detyrueshme");
        UploadDocument("vertetim_bankarUpload", SignedPdf);
        UploadDocument("license_laboratorUpload", SignedPdf);
        UploadDocument("license_drejtues_teknikUpload", SignedPdf);
        UploadDocument("license_mjekUpload", SignedPdf);
        UploadDocument("kontrata_mjek_specialistUpload", SignedPdf);

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

    private IWebElement FindInputByName(string name)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector($"input[name='{name}']")));
    }

    private void AssertReadonlyByName(string name, string expectedValue)
    {
        IWebElement input = FindInputByName(name);
        Assert.That(input.GetAttribute("readonly") ?? input.GetAttribute("disabled"), Is.Not.Null,
            $"Fusha {name} duhet te jete readonly");
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue),
            $"Vlera e fushes {name} nuk eshte e sakte");
    }

    private void FillByName(string name, string value)
    {
        IWebElement input = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector($"input[name='{name}']")));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            input);
        Thread.Sleep(200);
        try
        {
            input.Click();
            input.Clear();
            input.SendKeys(value);
        }
        catch (ElementClickInterceptedException)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].focus(); arguments[0].value = '';",
                input);
            input.SendKeys(value);
        }
        Thread.Sleep(200);
    }

    private void AssertActiveSteps(int activeCount, int totalCount = 3)
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

    private void AssertCheckboxExists(string id, string labelPart)
    {
        Assert.That(driver.FindElement(
            By.XPath($"//label[contains(.,'{labelPart}')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id(id)).GetAttribute("type"), Is.EqualTo("checkbox"));
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
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-15358"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("5"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("5"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf,.jpg,.jpeg,.png,.txt"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të ngarkuar dokumentin"));

        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të ngarkuar dokumentin"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='hint']")).Text.Trim(),
            Is.EqualTo("Formatet e lejuara: PDF, JPG, JPEG, PNG, TXT. Madhësia maksimale: 5MB."));
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
}
