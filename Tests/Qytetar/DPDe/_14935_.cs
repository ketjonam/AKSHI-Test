using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.DPDe;

[Category("DPDe")]
[Category("14935")]
public class _14935_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "14935";
    protected override string? ServiceTitle => "CertifikateEndorsement";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Aplikim për regjistrim dhe pajisje me certifikatë endorsement";
    private const string ExpectedAddress =
        "FROSINA PLAKU; Nd. 88; H. 2; Ap. 9; NJËSIA ADMINISTRATIVE NR. 7; NJËSIA BASHKIAKE NR. 7; 1023; TIRANË";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";

    [Test]
    public void CertifikateEndorsement()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert Title Step 1");
        IWebElement Step1Title = WaitForStepTitle("JU LUTEM ZGJIDHNI SHTETËSINË TUAJ");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("JU LUTEM ZGJIDHNI SHTETËSINË TUAJ"));

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

        Log("Assert 4 hapa, hapi i pare aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(4));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));
        for (int i = 1; i < steps.Count; i++)
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert opsionet e shtetësisë");
        AssertRadioOption("nationalityAlbanian", "nationality", "Shtetas Shqiptar");
        AssertRadioOption("nationalityForeign", "nationality", "Shtetas i huaj");

        Log("Assert butonat e navigimit");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));
        Assert.That(continueBtn.GetAttribute("disabled"), Is.Null);

        Log("Zgjidh Shtetas Shqiptar");
        SelectRadioById("nationalityAlbanian");
        Assert.That(driver.FindElement(By.Id("nationalityAlbanian")).Selected, Is.True);
        Assert.That(driver.FindElement(By.Id("nationalityForeign")).Selected, Is.False);

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Title Step 2");
        IWebElement Step2Title = WaitForStepTitle("TË DHËNAT E APLIKANTIT");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E APLIKANTIT"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

        Log("Assert 4 hapa, dy te paret aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(4));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        for (int i = 2; i < steps.Count; i++)
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert te dhenat e aplikantit te para-plotesuara dhe disabled");
        AssertDisabledValue("Nid", Settings.Qytetar.Username);
        Assert.That(FindInputByName("nid").GetAttribute("id"), Is.EqualTo("nid"));
        AssertDisabledValue("Emri", "Katerina");
        AssertDisabledValue("Mbiemri", "Jançe");
        AssertDisabledValue("Atësia", "Foti");
        AssertDisabledValue("Gjinia", "Femër");
        Assert.That(FindInputByName("gjinia").GetAttribute("type"), Is.EqualTo("text"));
        AssertDisabledValue("Datëlindja", "13.04.1993");
        AssertDisabledValue("Shtetësia", "Shqiptare");
        AssertDisabledValue("Qyteti", "TIRANË");
        AssertDisabledValue("Rrethi", "TIRANË");
        Assert.That(FindInputByName("bashkia").GetAttribute("id"), Is.EqualTo("bashkia"));
        AssertDisabledValue("Vendlindja", "Korçë");
        AssertDisabledValue("Nr. Tel. Cel.", "+355697008820");
        Assert.That(FindInputByName("nrTel").GetAttribute("id"), Is.EqualTo("nrTel"));
        AssertDisabledValue("Email", "katerina.jance@kreatx.com");
        Assert.That(FindInputByLabel("Email").GetAttribute("type"), Is.EqualTo("email"));
        AssertDisabledValue("Adresa", ExpectedAddress);
        Assert.That(FindInputByName("adresa").GetAttribute("type"), Is.EqualTo("text"));

        Log("Assert Nr.Tel Fiks dhe Kodi Postar jane te editueshme");
        IWebElement telFiks = FindInputByLabel("Nr.Tel Fiks");
        Assert.That(telFiks.GetAttribute("name"), Is.EqualTo("nrTelFiks"));
        Assert.That(telFiks.GetAttribute("id"), Is.EqualTo("nrTelFiks"));
        Assert.That(telFiks.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(telFiks.GetAttribute("disabled"), Is.Null);
        Assert.That(telFiks.GetAttribute("readonly"), Is.Null);
        Assert.That(telFiks.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement kodiPostar = FindInputByLabel("Kodi Postar");
        Assert.That(kodiPostar.GetAttribute("name"), Is.EqualTo("kodiPostar"));
        Assert.That(kodiPostar.GetAttribute("disabled"), Is.Null);
        Assert.That(kodiPostar.GetAttribute("readonly"), Is.Null);
        Assert.That(kodiPostar.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Ploteso Kodi Postar");
        FillInput(FindInputByName("kodiPostar"), "1023");
        Assert.That(FindInputByName("kodiPostar").GetAttribute("value").Trim(), Is.EqualTo("1023"));

        Log("Assert butonat e navigimit Step 2");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));
        Assert.That(continueBtn.GetAttribute("disabled"), Is.Null);

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Title Step 3");
        IWebElement Step3Title = WaitForStepTitle("INFORMACION SPECIFIK MBI APLIKIMIN");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("INFORMACION SPECIFIK MBI APLIKIMIN"));

        Log("Assert kohëzgjatja Step 3");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

        Log("Assert 4 hapa, tre te paret aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(4));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[3].GetAttribute("class"), Does.Not.Contain("active"));
        Assert.That(steps[3].GetAttribute("class"), Does.Contain("no-click"));

        Log("Assert label-at e detyrueshme kane yll");
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Gjatësia (cm)')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Ngjyra e syve')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Shenja të veçanta')]")).Text, Does.Contain("*"));

        Log("Assert fushat e informacionit specifik jane boshe dhe te editueshme");
        IWebElement gjatesia = FindInputByName("gjatesia");
        Assert.That(gjatesia.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(gjatesia.GetAttribute("id"), Is.EqualTo("gjatesia"));
        Assert.That(gjatesia.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(gjatesia.GetAttribute("disabled"), Is.Null);
        Assert.That(gjatesia.GetAttribute("readonly"), Is.Null);

        IWebElement ngjyraESyve = FindInputByName("ngjyraESyve");
        Assert.That(ngjyraESyve.GetAttribute("type"), Is.EqualTo("text"));
        Assert.That(ngjyraESyve.GetAttribute("id"), Is.EqualTo("ngjyraESyve"));
        Assert.That(ngjyraESyve.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(ngjyraESyve.GetAttribute("disabled"), Is.Null);
        Assert.That(ngjyraESyve.GetAttribute("readonly"), Is.Null);

        IWebElement shenjaTeVecanta = FindInputByName("shenjaTeVecanta");
        Assert.That(shenjaTeVecanta.GetAttribute("type"), Is.EqualTo("text"));
        Assert.That(shenjaTeVecanta.GetAttribute("id"), Is.EqualTo("shenjaTeVecanta"));
        Assert.That(shenjaTeVecanta.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(shenjaTeVecanta.GetAttribute("disabled"), Is.Null);
        Assert.That(shenjaTeVecanta.GetAttribute("readonly"), Is.Null);

        Log("Assert butonat e navigimit Step 3");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));
        Assert.That(continueBtn.GetAttribute("disabled"), Is.Null);

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error per fushat e detyrueshme");
        AssertRequiredError("Gjatësia (cm)");
        AssertRequiredError("Ngjyra e syve");
        AssertRequiredError("Shenja të veçanta");

        Log("Ploteso informacionin specifik mbi aplikimin");
        FillInput(FindInputByName("gjatesia"), "165");
        FillInput(FindInputByName("ngjyraESyve"), "Kafe");
        FillInput(FindInputByName("shenjaTeVecanta"), "Asnjë");

        Assert.That(FindInputByName("gjatesia").GetAttribute("value").Trim(), Is.EqualTo("165"));
        Assert.That(FindInputByName("ngjyraESyve").GetAttribute("value").Trim(), Is.EqualTo("Kafe"));
        Assert.That(FindInputByName("shenjaTeVecanta").GetAttribute("value").Trim(), Is.EqualTo("Asnjë"));

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Title Step 4");
        IWebElement Step4Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 4");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

        Log("Assert 4 hapa, te gjithe aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(4));
        foreach (var step in steps)
            Assert.That(step.GetAttribute("class"), Does.Contain("active"));

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës')]")).Displayed, Is.True);

        Log("Assert document-upload Mandatpagesa (opsional)");
        AssertDocumentUpload(
            "mandats_pagesaUpload",
            "Mandatpagesa + faturën për regjistrim dhe pajisja me Certifikatë endorsement");
        Assert.That(driver.FindElements(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Mandatpagesa')]//span[normalize-space()='*']")).Count, Is.EqualTo(0));

        Log("Assert linkun e shkarkimit te fatures");
        IWebElement fatureLink = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("a[href='/service/Documents_14935/11154_mbi_15m_fature.pdf']")));
        Assert.That(fatureLink.Text.Trim(), Is.EqualTo("Shkarkoni këtu"));

        Log("Assert document-upload Pasaporte Detare");
        AssertDocumentUpload(
            "pasaporte_detareUpload",
            "Pasaportë Detare");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Pasaportë Detare')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert document-upload Certifikata te kompetences");
        AssertDocumentUpload(
            "certifikata_kompetenceUpload",
            "Certifikatata të kompetencës nga administrata e shtetit");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Certifikatata të kompetencës')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert document-upload Fotografi");
        AssertDocumentUpload(
            "fotografiUpload",
            "Fotografi (4x5 cm për dokument)");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Fotografi')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert nuk nevojitet dokumentacion nga administrata");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'text-muted') and contains(.,'Për këtë shërbim nuk nevojitet të sigurohet dokumentacion nga nëpunësi i administratës.')]")).Displayed, Is.True);
        Assert.That(driver.FindElements(By.Id("agreeCheck")).Count, Is.EqualTo(0));
        Assert.That(driver.FindElements(By.Id("consentCheckbox")).Count, Is.EqualTo(0));

        Log("Assert butonat e navigimit Step 4");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(dergoBtn.Text.Trim(), Does.Contain("Dërgo"));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Kliko Dergo pa ngarkuar dokumentet e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(1000);
        Assert.That(WaitForStepTitle("DOKUMENTACIONI").Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DOKUMENTACIONI"));

        Log("Ngarko dokumentet e detyrueshme");
        UploadDocument("pasaporte_detareUpload", DocumentPath);
        UploadDocument("certifikata_kompetenceUpload", DocumentPath);
        UploadDocument("fotografiUpload", DocumentPath);

        //Log("Kliko Dergo");
        //SafeClick(By.CssSelector("button.ealb-btn-continue"));
        //Thread.Sleep(5000);

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

    private void SelectRadioById(string radioId)
    {

        IWebElement input = wait.Until(ExpectedConditions.ElementExists(By.Id(radioId)));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            input
        );
        Thread.Sleep(300);

        if (!input.Selected)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(@"
                const el = arguments[0];
                el.click();
                el.dispatchEvent(new Event('input', { bubbles: true }));
                el.dispatchEvent(new Event('change', { bubbles: true }));
            ", input);
        }

        wait.Until(d => d.FindElement(By.Id(radioId)).Selected);
        Thread.Sleep(300);
    }

    private void AssertRadioOption(string radioId, string expectedName, string expectedLabel)
    {

        IWebElement radio = wait.Until(ExpectedConditions.ElementExists(By.Id(radioId)));
        Assert.That(radio.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(radio.GetAttribute("name"), Is.EqualTo(expectedName));
        Assert.That(driver.FindElement(By.CssSelector($"label[for='{radioId}']")).Text.Trim(),
            Is.EqualTo(expectedLabel));
    }

    private IWebElement WaitForStepTitle(string expectedUpper)
    {

        return wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h5.text-uppercase, h4.text-uppercase, h4.ealb-header-text"));
            foreach (var title in titles)
            {
                if (title.Text.Trim().ToUpperInvariant() == expectedUpper)
                    return title;
            }
            return null;
        });
    }

    private IWebElement FindInputByLabel(string labelPart)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//div[@id='root']//form//label[contains(.,'{labelPart}')]/following-sibling::*[self::input or self::textarea]")));
    }

    private IWebElement FindInputByName(string name)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector($"#root form input[name='{name}'], #root form textarea[name='{name}']")));
    }

    private void AssertReadOnlyValue(string labelPart, string expectedValue)
    {

        IWebElement input = FindInputByLabel(labelPart);
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(input.GetAttribute("readonly"), Is.Not.Null);
    }

    private void AssertReadOnlyDisabledValue(string labelPart, string expectedValue)
    {

        IWebElement input = FindInputByLabel(labelPart);
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(input.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(input.GetAttribute("disabled"), Is.Not.Null);
    }

    private void AssertDisabledValue(string labelPart, string expectedValue)
    {

        IWebElement input = FindInputByLabel(labelPart);
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(input.GetAttribute("disabled"), Is.Not.Null);
    }

    private void AssertRequiredError(string labelPart)
    {

        IWebElement error = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//div[@id='root']//form//label[contains(.,'{labelPart}')]/following::*[contains(@class,'text-danger') or contains(@class,'invalid-feedback')][1]")));
        Assert.That(error.Text.Trim(), Is.EqualTo("Plotësoni fushën për të vazhduar"));
        Assert.That(FindInputByLabel(labelPart).GetAttribute("class"), Does.Contain("is-invalid"));
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

    private void FillInput(IWebElement input, string value)
    {

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            input
        );
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

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {

        Assert.That(driver.FindElement(
            By.XPath($"//*[contains(normalize-space(),'{documentTitle}')]")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-14935"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("5"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("50"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf,.jpg,.jpeg,.png"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të ngarkuar dokumentin"));

        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të ngarkuar dokumentin"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='hint']")).Text.Trim(),
            Is.EqualTo("Formatet e lejuara: PDF, JPG, JPEG, PNG. Madhësia maksimale: 5MB."));
    }

    private void UploadDocument(string uploadId, string filePath)
    {

        Assert.That(File.Exists(filePath), Is.True, "File nuk ekziston: " + filePath);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            docUpload
        );
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
