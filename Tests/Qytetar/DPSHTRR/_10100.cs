using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.DPSHTRR;

[Category("DPSHTRR")]
[Category("10100")]
public class _10100 : QytetarNidJ257TestBase
{
    protected override string ServiceCode => "10100";
    protected override string? ServiceTitle => "Ndrimm_Pronesie";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Aplikim për ndërrim pronësie mbi mjetin të lejes së qarkullimit dhe targave";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Signed_TEST_signed.pdf";
    private const int TotalSteps = 4;

    private static readonly (string Id, string Title)[] ApplicantDocuments =
    {
        ("AKTSHUpload", "Dokumenti i fitimit të pronësisë (Akt-Shitje)"),
        ("CEROWNUpload", "Certifikatë Pronësie"),
        ("PPUpload", "Prokurë e posaçme"),
        ("RBSDOCUpload", "Konfirmimi nga RBS"),
    };

    [Test]
    public void Ndrimm_Pronesie()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert Title Step 1");
        IWebElement Step1Title = WaitForStepTitle("SEKSIONI PËR NJËSITË");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("SEKSIONI PËR NJËSITË"));

        Log("Assert kohëzgjatja");
        AssertDuration("3 minuta kohëzgjatje");

        Log("Assert 4 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert fushat e njesive");
        IWebElement rajoni = FindByName("ddRajoni");
        Assert.That(new SelectElement(rajoni).SelectedOption.GetAttribute("value"), Is.EqualTo("0"));
        Assert.That(FindLabel("Drejtoria rajonale").Text, Does.Contain("*"));

        IWebElement bashkia = FindByName("ddBashkia");
        Assert.That(bashkia.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindLabel("Bashkia").Text, Does.Contain("*"));

        IWebElement njesiaAdm = FindByName("ddNjesiaAdm");
        Assert.That(njesiaAdm.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindLabel("Njësia administrative").Text, Does.Contain("*"));

        IWebElement nenNjesia = FindByName("ddNenNjesia");
        Assert.That(nenNjesia.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindLabel("Nën njësia administrative").Text, Does.Contain("*"));

        Log("Assert butonat e navigimit Step 1");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Përzgjidhni një vlerë për të vazhduar");

        Log("Zgjidh te dhenat mbi Drejtorine Rajonale");
        SelectByValueSafe(By.Name("ddRajoni"), "11");
        SelectByValueSafe(By.Name("ddBashkia"), "TIR");
        SelectByValueSafe(By.Name("ddNjesiaAdm"), "NJESIADMINNR1");
        SelectByValueSafe(By.Name("ddNenNjesia"), "NJESIABASHKNR1-TIR");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("TË DHËNAT E APLIKANTIT");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNAT E APLIKANTIT"));

        Log("Assert kohëzgjatja Step 2");
        AssertDuration("3 minuta kohëzgjatje");

        Log("Assert 4 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert te dhenat individuale te para-plotesuara");
        AssertDisabledApplicantField("nid", CitizenNid);
        AssertDisabledApplicantField("emri", "Migen");
        AssertDisabledApplicantField("mbiemri", "Dërstila");
        AssertDisabledApplicantField("atesia", "Luan");
        AssertDisabledApplicantField("datelindja", "1997-09-03");
        AssertDisabledApplicantField("vendlindja", "Gjinar, Elbasan");
        AssertDisabledApplicantField("email", "migen.derstila@kreatx.com");
        AssertDisabledApplicantField("nrTel", "+355684053531");

        IWebElement adresa = FindApplicantField("adresa");
        Assert.That(adresa.GetAttribute("value").Trim(),
            Is.EqualTo("Derstile; ; ; ; Elbasan; ; 0000; Elbasan"));
        Assert.That(adresa.GetAttribute("disabled"), Is.Not.Null);

        IWebElement genderMale = wait.Until(ExpectedConditions.ElementExists(By.Id("male")));
        IWebElement genderFemale = wait.Until(ExpectedConditions.ElementExists(By.Id("female")));
        Assert.That(genderMale.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(genderFemale.GetAttribute("disabled"), Is.Not.Null);

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("INFORMACION SPECIFIK MBI APLIKIMIN");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("INFORMACION SPECIFIK MBI APLIKIMIN"));

        Log("Assert kohëzgjatja Step 3");
        AssertDuration("3 minuta kohëzgjatje");

        Log("Assert 4 hapa, tre te paret aktiv");
        AssertSteps(3);

        Log("Assert fushat e informacionit specifik");
        IWebElement vin = FindByName("txtVin");
        Assert.That(vin.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Nr.Shasisë").Text, Does.Contain("*"));

        IWebElement licenceNo = FindByName("txtLicenceNo");
        Assert.That(licenceNo.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Targa").Text, Does.Contain("*"));

        IWebElement vehicleType = FindByName("ddVehicleType");
        var vehicleTypeSelect = new SelectElement(vehicleType);
        Assert.That(vehicleTypeSelect.SelectedOption.GetAttribute("value"), Is.EqualTo("0"));
        Assert.That(FindLabel("Tipi").Text, Does.Contain("*"));
        Assert.That(vehicleTypeSelect.Options.Any(o => o.GetAttribute("value") == "M"), Is.True);
        Assert.That(vehicleTypeSelect.Options.Any(o => o.GetAttribute("value") == "MT"), Is.True);
        Assert.That(vehicleTypeSelect.Options.Any(o => o.GetAttribute("value") == "R"), Is.True);
        Assert.That(vehicleTypeSelect.Options.Any(o => o.GetAttribute("value") == "C"), Is.True);

        IWebElement serviceBundle = FindByName("ddServiceBundle");
        var serviceBundleSelect = new SelectElement(serviceBundle);
        Assert.That(FindLabel("Ndërrim pronësie mbi mjetin").Text, Does.Contain("*"));
        Assert.That(serviceBundleSelect.Options.Any(o => o.GetAttribute("value") == "NP"), Is.True);
        Assert.That(serviceBundleSelect.Options.Any(o => o.GetAttribute("value") == "NPT"), Is.True);

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 3");
        FillInput(vin, "WAUZZZ4G4EN070522");
        FillInput(licenceNo, "AB770PP");
        SelectByValue(vehicleType, "M");
        SelectByValue(serviceBundle, "NP");

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 4 Title");
        IWebElement Step4Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(), Does.StartWith("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 4");
        AssertDuration("3 minuta kohëzgjatje");

        Log("Assert 4 hapa, te gjithe aktiv");
        AssertSteps(4);

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës')]"))
            .Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'Dokumentin e identifikimit të treguesve të mjetit')]"))
            .Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'Lejen e qarkullimit të mjetit')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'Certifikatën e pronësisë së mjetit')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'Vërtetim nga DRSHTRR-ja se shitësi nuk është debitor')]"))
            .Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'Fotokopje e NIPT')]")).Displayed, Is.True);

        Log("Assert document-upload dokumentet e aplikantit");
        foreach (var doc in ApplicantDocuments)
            AssertDocumentUpload(doc.Id, doc.Title);

        Log("Assert checkbox pëlqimi");
        IWebElement consent = wait.Until(ExpectedConditions.ElementExists(By.Id("confirmAdminDocuments")));
        Assert.That(consent.GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='confirmAdminDocuments']")).Text,
            Does.Contain("Me klikimin e këtij butoni, ju bini dakord"));

        Log("Assert butonat e navigimit Step 4");
        AssertNavigationButtons("Dërgo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue.with-arrow")));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Kliko Dergo pa ngarkuar dokumentet");
        SafeClick(By.CssSelector("button.ealb-btn-continue.with-arrow"));
        Thread.Sleep(1000);
        Assert.That(WaitForStepTitle("DOKUMENTACIONI").Text.Trim().ToUpperInvariant(),
            Does.StartWith("DOKUMENTACIONI"));

        Log("Ngarko dokumentet");
        foreach (var doc in ApplicantDocuments)
            UploadDocument(doc.Id, DocumentPath);

        Log("Kliko checkbox pëlqimi");
        IWebElement consentCheck = driver.FindElement(By.Id("confirmAdminDocuments"));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            consentCheck);
        Thread.Sleep(300);
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", consentCheck);

        ClickDergo();
        AssertDergoOutcome();

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
        Assert.That(serviceName.Text.Replace('\u00A0', ' ').Trim(), Is.EqualTo(ExpectedServiceName),
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
        Assert.That(perdorBtn.Text.Trim(), Is.EqualTo("Përdor"), "Butoni nuk eshte Përdor");

        Log("Kliko butonin Perdor");
        SafeClick(perdorLocator);

        Log("Kliko Aplikim i ri");
        By aplikimIRiLocator = By.CssSelector("button[aria-label='Aplikim i ri']");
        IWebElement aplikimIRi = wait.Until(ExpectedConditions.ElementIsVisible(aplikimIRiLocator));
        Assert.That(aplikimIRi.Displayed, Is.True, "Karta Aplikim i ri nuk eshte visible");
        IWebElement aplikimIRiTitle = aplikimIRi.FindElement(By.CssSelector("h6.mbx-title"));
        Assert.That(aplikimIRiTitle.Text.Trim(), Is.EqualTo("Aplikim i ri"),
            "Titulli i kartes nuk eshte Aplikim i ri");
        SafeClick(aplikimIRiLocator);
        Thread.Sleep(1500);
        DismissCookieBannerIfPresent();
    }

    private void AssertDuration(string expected)
    {
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain(expected));
    }

    private void AssertSteps(int activeCount)
    {
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(TotalSteps));
        for (int i = 0; i < steps.Count; i++)
        {
            if (i < activeCount)
                Assert.That(steps[i].GetAttribute("class"), Does.Contain("active"));
            else
                Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }
    }

    private void AssertNavigationButtons(string continueText)
    {
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Does.Contain(continueText));
    }

    private IWebElement WaitForStepTitle(string expectedUpper)
    {
        return wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4.text-uppercase, h5.text-uppercase"));
            foreach (var title in titles)
            {
                string actual = title.Text.Trim().ToUpperInvariant();
                if (actual == expectedUpper || actual.StartsWith(expectedUpper))
                    return title;
            }
            return null;
        });
    }

    private IWebElement FindByName(string name)
    {
        return wait.Until(ExpectedConditions.ElementExists(By.Name(name)));
    }

    private IWebElement FindApplicantField(string key)
    {
        return wait.Until(d =>
        {
            var byName = d.FindElements(By.Name(key));
            if (byName.Count > 0)
                return byName[0];

            var byId = d.FindElements(By.Id(key));
            if (byId.Count > 0)
                return byId[0];

            var labels = d.FindElements(By.CssSelector($"label[for='{key}']"));
            if (labels.Count == 0)
                return null;

            var following = labels[0].FindElements(
                By.XPath("./following-sibling::*[self::input or self::textarea][1]"));
            return following.Count > 0 ? following[0] : null;
        });
    }

    private IWebElement FindLabel(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//label[contains(.,'{labelPart}')]")));
    }

    private void AssertDisabledApplicantField(string key, string expectedValue)
    {
        IWebElement field = FindApplicantField(key);
        Assert.That(field.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(field.GetAttribute("disabled"), Is.Not.Null);
    }

    private void SelectByValue(IWebElement select, string value)
    {
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            select);
        Thread.Sleep(300);
        new SelectElement(select).SelectByValue(value);
        Thread.Sleep(800);
    }

    private void WaitUntilOptionExists(By selectLocator, string optionValue)
    {
        wait.Until(d =>
        {
            try
            {
                var selectElement = new SelectElement(d.FindElement(selectLocator));
                return selectElement.Options.Any(o =>
                    string.Equals(
                        (o.GetAttribute("value") ?? string.Empty).Trim(),
                        optionValue,
                        StringComparison.OrdinalIgnoreCase));
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
            dropdown);
        Thread.Sleep(500);

        var select = new SelectElement(dropdown);
        Log($"Po zgjedh value '{optionValue}' tek {selectLocator}");
        select.SelectByValue(optionValue);
        Thread.Sleep(1000);
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {
        IWebElement title = driver.FindElement(
            By.XPath($"//span[contains(@class,'fw-bold') and contains(normalize-space(),'{documentTitle}')]"));
        Assert.That(title.Displayed, Is.True);
        Assert.That(title.Text.Trim(), Does.Not.EndWith("*"));

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-10100"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("25"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("25"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf,.jpg,.jpeg,.png"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të ngarkuar dokument"));

        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të ngarkuar dokument"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='hint']")).Text.Trim(),
            Is.EqualTo("Formatet e lejuara: PDF, JPG, JPEG, PNG. Madhësia maksimale: 25MB."));
    }

    private void UploadDocument(string uploadId, string filePath)
    {
        Assert.That(File.Exists(filePath), Is.True, "File nuk ekziston: " + filePath);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            docUpload);
        Thread.Sleep(300);

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
}
