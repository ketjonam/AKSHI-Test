using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.DPDe;

[Category("DPDe")]
[Category("11161")]
public class _11161_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "11161";
    protected override string? ServiceTitle => "PasaporteDetareFlotePeshkimi";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Aplikim për regjistrim dhe pajisje me pasaportë detare (flotë peshkimi)";
    private const string ExpectedAddress =
        "FROSINA PLAKU; Nd. 88; H. 2; Ap. 9; NJËSIA ADMINISTRATIVE NR. 7; NJËSIA BASHKIAKE NR. 7; 1023; TIRANË";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";

    [Test]
    public void PasaporteDetareFlotePeshkimi()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert Title Step 1");
        IWebElement Step1Title = WaitForStepTitle("TË DHËNAT E APLIKANTIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNAT E APLIKANTIT"));

        Log("Assert tooltip i te dhenave te aplikantit");
        IWebElement tooltip = wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector("h4.text-uppercase span[data-bs-toggle='tooltip']")));
        AssertTooltipText(tooltip,
            "Të dhënat e aplikantit plotësohen nga identifikimi juaj në e-albania");

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 4 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert te dhenat e aplikantit te para-plotesuara, readonly dhe disabled");
        AssertReadOnlyDisabledValue("Nid", Settings.Qytetar.Username);
        AssertReadOnlyDisabledValue("Emri", "Katerina");
        AssertReadOnlyDisabledValue("Mbiemri", "Jançe");
        AssertReadOnlyDisabledValue("Atësia", "Foti");
        AssertReadOnlyDisabledValue("Datëlindja", "13.04.1993");
        AssertReadOnlyDisabledValue("Vendlindja", "Korçë");
        AssertReadOnlyDisabledValue("Shtetësia", "Shqiptare");

        Log("Assert gjinia eshte disabled dhe e zgjedhur Femër");
        IWebElement gjiniaSelect = WaitForSelectSelectedValue("gjinia", "F");
        Assert.That(gjiniaSelect.GetAttribute("disabled"), Is.Not.Null);
        var gjinia = new SelectElement(gjiniaSelect);
        Assert.That(gjinia.Options.Count, Is.EqualTo(2));
        Assert.That(gjinia.Options[0].GetAttribute("value"), Is.EqualTo("M"));
        Assert.That(gjinia.Options[0].Text.Trim(), Is.EqualTo("Mashkull"));
        Assert.That(gjinia.Options[1].GetAttribute("value"), Is.EqualTo("F"));
        Assert.That(gjinia.Options[1].Text.Trim(), Is.EqualTo("Femër"));
        Assert.That(gjinia.SelectedOption.GetAttribute("value"), Is.EqualTo("F"));
        Assert.That(gjinia.SelectedOption.Text.Trim(), Is.EqualTo("Femër"));

        Log("Assert butonat e navigimit");
        AssertNavigationButtons("Vazhdo");
        IWebElement continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(continueBtn.GetAttribute("disabled"), Is.Null);

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Title Step 2");
        IWebElement Step2Title = WaitForStepTitle("INFORMACIONI I KONTAKTIT TË APLIKANTIT");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("INFORMACIONI I KONTAKTIT TË APLIKANTIT"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 4 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert te dhenat e kontaktit te para-plotesuara, readonly dhe disabled");
        AssertReadOnlyDisabledValue("Qyteti", "TIRANË");
        AssertReadOnlyDisabledValue("Nr. Tel. Cel.", "+355697008820");
        AssertReadOnlyDisabledValue("Email", "katerina.jance@kreatx.com");
        AssertReadOnlyDisabledValue("Adresa", ExpectedAddress);

        Log("Assert tooltip i Nr. Tel. Cel.");
        IWebElement telCelTooltip = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//form//label[contains(.,'Nr. Tel. Cel.')]//span[@data-bs-toggle='tooltip']")));
        AssertTooltipText(telCelTooltip,
            "Numri i celularit merret nga të dhënat e llogarisë që jeni regjistruar në e-Albania. ");

        Log("Assert Email eshte i tipit email, readonly dhe disabled");
        IWebElement emailInput = FindInputByLabel("Email");
        Assert.That(emailInput.GetAttribute("type"), Is.EqualTo("email"));
        Assert.That(emailInput.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(emailInput.GetAttribute("disabled"), Is.Not.Null);

        Log("Assert Rrethi eshte i editueshem dhe i para-plotesuar");
        IWebElement rrethi = FindInputByLabel("Rrethi");
        Assert.That(rrethi.GetAttribute("name"), Is.EqualTo("emRrethi"));
        Assert.That(rrethi.GetAttribute("disabled"), Is.Null);
        Assert.That(rrethi.GetAttribute("readonly"), Is.Null);
        Assert.That(rrethi.GetAttribute("value").Trim(), Is.EqualTo("TIRANË"));

        Log("Assert Kodi Postar dhe Nr. Tel Fiks jane te editueshme");
        IWebElement kodiPostar = FindInputByLabel("Kodi Postar");
        Assert.That(kodiPostar.GetAttribute("name"), Is.EqualTo("kodiPostar"));
        Assert.That(kodiPostar.GetAttribute("disabled"), Is.Null);
        Assert.That(kodiPostar.GetAttribute("readonly"), Is.Null);
        Assert.That(kodiPostar.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement telFiks = FindInputByLabel("Nr. Tel Fiks");
        Assert.That(telFiks.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(telFiks.GetAttribute("name"), Is.EqualTo("telFiks"));
        Assert.That(telFiks.GetAttribute("disabled"), Is.Null);
        Assert.That(telFiks.GetAttribute("readonly"), Is.Null);
        Assert.That(telFiks.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Ploteso Kodi Postar dhe Nr. Tel Fiks");
        FillInput(kodiPostar, "1023");
        FillInput(telFiks, "055220000");
        Assert.That(FindInputByLabel("Kodi Postar").GetAttribute("value").Trim(), Is.EqualTo("1023"));
        Assert.That(FindInputByLabel("Nr. Tel Fiks").GetAttribute("value").Trim(), Is.EqualTo("055220000"));

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
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
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 4 hapa, tre te paret aktiv");
        AssertSteps(3);

        Log("Assert label-at e detyrueshme kane yll");
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Gjatësia (cm)')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Ngjyra e syve')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Shenja të veçanta')]")).Text, Does.Contain("*"));

        Log("Assert fushat e informacionit specifik jane boshe dhe te editueshme");
        IWebElement gjatesia = FindInputByName("gjatesia");
        Assert.That(gjatesia.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(gjatesia.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(gjatesia.GetAttribute("disabled"), Is.Null);
        Assert.That(gjatesia.GetAttribute("readonly"), Is.Null);

        IWebElement ngjyraSyve = FindInputByName("ngjyraSyve");
        Assert.That(ngjyraSyve.GetAttribute("type"), Is.EqualTo("text"));
        Assert.That(ngjyraSyve.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(ngjyraSyve.GetAttribute("disabled"), Is.Null);
        Assert.That(ngjyraSyve.GetAttribute("readonly"), Is.Null);

        IWebElement shenjaTeVecanta = FindInputByName("shenjaTeVecanta");
        Assert.That(shenjaTeVecanta.GetAttribute("type"), Is.EqualTo("text"));
        Assert.That(shenjaTeVecanta.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(shenjaTeVecanta.GetAttribute("disabled"), Is.Null);
        Assert.That(shenjaTeVecanta.GetAttribute("readonly"), Is.Null);

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Vazhdo");
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(continueBtn.GetAttribute("disabled"), Is.Null);

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error per fushat e detyrueshme");
        AssertRequiredError("Gjatësia (cm)");
        AssertRequiredError("Ngjyra e syve");
        AssertRequiredError("Shenja të veçanta");

        Log("Ploteso informacionin specifik mbi aplikimin");
        FillInput(FindInputByName("gjatesia"), "165");
        FillInput(FindInputByName("ngjyraSyve"), "Kafe");
        FillInput(FindInputByName("shenjaTeVecanta"), "Asnjë");

        Assert.That(FindInputByName("gjatesia").GetAttribute("value").Trim(), Is.EqualTo("165"));
        Assert.That(FindInputByName("ngjyraSyve").GetAttribute("value").Trim(), Is.EqualTo("Kafe"));
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
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 4 hapa, te gjithe aktiv");
        AssertSteps(4);

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës')]")).Displayed, Is.True);

        Log("Assert document-upload Mandatpagesa");
        AssertDocumentUpload(
            "fuMandatPagesaUpload",
            "Mandatpagesa + faturën për regjistrim dhe pajisja me pasaportë detare");

        Log("Assert linkun e shkarkimit te fatures");
        IWebElement fatureLink = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("a[href='/service/Documents_11161/11161_mandatpagesa.pdf']")));
        Assert.That(fatureLink.Text.Trim(), Is.EqualTo("Shkarko faturën"));

        Log("Assert document-upload Kontrate punesimi");
        AssertDocumentUpload(
            "fuKontratePunesimiUpload",
            "Kontratë punësimi si detar (origjinale)");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Kontratë punësimi si detar')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert document-upload Raport i aftësisë");
        AssertDocumentUpload(
            "fuRaportMjekoligjorUpload",
            "Raport i aftësisë për punë");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Raport i aftësisë për punë')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert document-upload Analiza e grupit te gjakut");
        AssertDocumentUpload(
            "fuAnalizaGjakuUpload",
            "Dokument për analizën e grupit të gjakut (origjinale)");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Dokument për analizën e grupit të gjakut')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert document-upload Deklarate notimi");
        AssertDocumentUpload(
            "fuDeklarateUpload",
            "Deklaratë mbi aftësinë për të notuar dhe vozitur");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Deklaratë mbi aftësinë për të notuar')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert document-upload Kualifikim per detyren (opsional)");
        AssertDocumentUpload(
            "fuCertifikateTrajnimiUpload",
            "Dokument Kualifikim për detyrën që do të kryejë(nëse ka)");
        Assert.That(driver.FindElements(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Dokument Kualifikim për detyrën')]//span[normalize-space()='*']")).Count, Is.EqualTo(0));

        Log("Assert document-upload Fotografi");
        AssertDocumentUpload(
            "fuFotografiUpload",
            "Fotografi (4x5 cm për dokument)");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Fotografi')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert nuk nevojitet dokumentacion nga administrata");
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(@class,'text-muted') and contains(.,'Për këtë shërbim nuk nevojitet të sigurohet dokumentacion nga nëpunësi i administratës.')]")).Displayed, Is.True);
        Assert.That(driver.FindElements(By.Id("agreeCheck")).Count, Is.EqualTo(0));

        Log("Assert butonat e navigimit Step 4");
        AssertNavigationButtons("Dërgo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Kliko Dergo pa ngarkuar dokumentet e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(1000);
        Assert.That(WaitForStepTitle("DOKUMENTACIONI").Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DOKUMENTACIONI"));

        Log("Ngarko dokumentet e detyrueshme");
        UploadDocument("fuKontratePunesimiUpload", DocumentPath);
        UploadDocument("fuRaportMjekoligjorUpload", DocumentPath);
        UploadDocument("fuAnalizaGjakuUpload", DocumentPath);
        UploadDocument("fuDeklarateUpload", DocumentPath);
        UploadDocument("fuFotografiUpload", DocumentPath);

        ClickDergo();

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

    private void AssertSteps(int activeCount)
    {
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(4));
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
            var titles = d.FindElements(By.CssSelector("h5.text-uppercase, h4.text-uppercase, h4.ealb-header-text"));
            foreach (var title in titles)
            {
                string actual = title.Text.Trim().ToUpperInvariant();
                if (actual == expectedUpper || actual.StartsWith(expectedUpper))
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

    private IWebElement FindSelectByName(string name)
    {
        return wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector($"#root form select[name='{name}']")));
    }

    private IWebElement FindInputByName(string name)
    {
        return wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector($"#root form input[name='{name}']")));
    }

    private void AssertReadOnlyDisabledValue(string labelPart, string expectedValue)
    {
        IWebElement input = FindInputByLabel(labelPart);
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(input.GetAttribute("readonly"), Is.Not.Null);
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

        try
        {
            input.Click();
            Thread.Sleep(200);
            input.Clear();
            input.SendKeys(value);
        }
        catch (ElementClickInterceptedException)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].focus(); arguments[0].value = '';",
                input
            );
            input.SendKeys(value);
        }

        BlurActiveElement();
        Thread.Sleep(300);
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {
        Assert.That(driver.FindElement(
            By.XPath($"//*[contains(normalize-space(),'{documentTitle}')]")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-11161"));
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
