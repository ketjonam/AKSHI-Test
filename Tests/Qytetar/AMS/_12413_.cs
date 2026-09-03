using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.AMS;

[Category("AMS")]
[Category("12413")]
public class _12413_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "12413";
    protected override string? ServiceTitle => "ShqyrtimiDosjes";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Aplikim për statusin dhe problematikat e shqyrtimit të dosjes";
    private const string ExpectedAddress =
        "FROSINA PLAKU; Nd. 88; H. 2; Ap. 9; NJËSIA ADMINISTRATIVE NR. 7; NJËSIA BASHKIAKE NR. 7; 1023; TIRANË";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";

    [Test]
    public void ShqyrtimiDosjes()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        AssertDuration("3 minuta kohëzgjatje");

        Log("Assert 3 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert Step 1 Title");
        IWebElement Step1Title = WaitForStepTitle("TË DHËNAT E APLIKANTIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E APLIKANTIT"));

        Log("Assert te dhenat e aplikantit");
        AssertReadonlyInput("firstName", "Katerina");
        AssertReadonlyInput("lastName", "Jançe");
        AssertReadonlyInput("fatherName", "Foti");
        AssertReadonlyInput("nid", Settings.Qytetar.Username);
        AssertReadonlyInput("birthDate", "1993-04-13");
        AssertReadonlyInput("birthPlace", "Korçë");
        AssertReadonlyInput("citizenship", "Shqiptare");
        AssertReadonlyInput("city", "TIRANË");
        AssertReadonlyInput("county", "TIRANË");
        AssertReadonlyInput("postalCode", "000379 - 1885");
        AssertReadonlyInput("email", "katerina.jance@kreatx.com");
        AssertReadonlyInput("mobilePhone", "+355697008820");
        AssertReadonlyInput("street", ExpectedAddress);

        IWebElement genderFemale = wait.Until(ExpectedConditions.ElementExists(By.Id("genderFemale")));
        IWebElement genderMale = wait.Until(ExpectedConditions.ElementExists(By.Id("genderMale")));
        Assert.That(genderFemale.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(genderMale.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(genderFemale.GetAttribute("value"), Is.EqualTo("F"));
        Assert.That(genderMale.GetAttribute("value"), Is.EqualTo("M"));
        Assert.That(genderFemale.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(genderMale.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(genderFemale.Selected, Is.True);
        Assert.That(genderMale.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='genderFemale']")).Text.Trim(),
            Is.EqualTo("Femër"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='genderMale']")).Text.Trim(),
            Is.EqualTo("Mashkull"));

        IWebElement fixedPhone = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("fixedPhone")));
        Assert.That(fixedPhone.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(fixedPhone.GetAttribute("readonly"), Is.Null);
        Assert.That(fixedPhone.GetAttribute("disabled"), Is.Null);

        Log("Assert butonat e navigimit Step 1");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("INFORMACION SPECIFIK MBI APLIKIMIN");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("INFORMACION SPECIFIK MBI APLIKIMIN"));

        Log("Assert kohëzgjatja Step 2");
        AssertDuration("3 minuta kohëzgjatje");

        Log("Assert 3 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert seksioni Kërkoj informacion për");
        Assert.That(driver.FindElement(
            By.XPath("//h5[contains(.,'Kërkoj informacion për')]")).Displayed, Is.True);

        Log("Assert fushat e hapit 2");
        IWebElement fileId = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("fileId")));
        Assert.That(fileId.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement fileNumber = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("fileNumber")));
        Assert.That(fileNumber.GetAttribute("required"), Is.Not.Null);
        Assert.That(fileNumber.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement district = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("district")));
        var districtSelect = new SelectElement(district);
        Assert.That(district.GetAttribute("required"), Is.Not.Null);
        Assert.That(districtSelect.SelectedOption.GetAttribute("value"), Is.EqualTo("-1"));
        Assert.That(districtSelect.Options.Count, Is.EqualTo(13));
        Assert.That(districtSelect.Options.Any(o => o.GetAttribute("value") == "Tiranë"), Is.True);
        Assert.That(districtSelect.Options.Any(o => o.GetAttribute("value") == "Korçë"), Is.True);

        IWebElement sector = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("sector")));
        var sectorSelect = new SelectElement(sector);
        Assert.That(sector.GetAttribute("required"), Is.Not.Null);
        Assert.That(sectorSelect.SelectedOption.GetAttribute("value"), Is.EqualTo("-1"));
        Assert.That(sectorSelect.Options.Count, Is.EqualTo(4));
        Assert.That(sectorSelect.Options[1].GetAttribute("value"), Is.EqualTo("Sektori i Shqyrtimit"));
        Assert.That(sectorSelect.Options[2].GetAttribute("value"), Is.EqualTo("Sektori i Kompensimit"));
        Assert.That(sectorSelect.Options[3].GetAttribute("value"),
            Is.EqualTo("Sektori i Tjetersimit te Oborreve"));

        IWebElement applicantName = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("applicantName")));
        Assert.That(applicantName.GetAttribute("required"), Is.Not.Null);
        Assert.That(applicantName.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement requestDetails = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("requestDetails")));
        Assert.That(requestDetails.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Assert.That(WaitForStepTitle("INFORMACION SPECIFIK MBI APLIKIMIN")
            .Text.Trim().ToUpperInvariant(),
            Is.EqualTo("INFORMACION SPECIFIK MBI APLIKIMIN"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme");
        FillInput(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("fileNumber"))), "1");
        SelectByValue(By.Id("district"), "Tiranë");
        SelectByValue(By.Id("sector"), "Sektori i Shqyrtimit");
        FillInput(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("applicantName"))), "test");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 3");
        AssertDuration("3 minuta kohëzgjatje");

        Log("Assert 3 hapa, te gjithe aktiv");
        AssertSteps(3);

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h5[contains(.,'Dokumenta që ngarkohen nga Aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumenta që ngarkohen nga nëpunësit e administratës publike')]"))
            .Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Për këtë shërbim nuk nevojitet të sigurohet dokumentacion nga nëpunësi i administratës.')]"))
            .Displayed, Is.True);

        Log("Assert document-upload");
        AssertDocumentUpload("akkpDecisionUpload", "Kopje vendimi nga ish AKKP");
        AssertDocumentUpload("legalDocumentsUpload", "Dëshmi Trashëgimie");
        AssertDocumentUpload("otherDocumentsUpload", "Dokumente të tjera");

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Dërgo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Ngarko dokumentet");
        UploadDocument("akkpDecisionUpload", DocumentPath);
        UploadDocument("legalDocumentsUpload", DocumentPath);
        UploadDocument("otherDocumentsUpload", DocumentPath);

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
        Assert.That(perdorBtn.Text.Trim(), Is.EqualTo("Përdor"), "Butoni nuk eshte Përdor");

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
        Assert.That(steps.Count, Is.EqualTo(3));
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
            var titles = d.FindElements(By.CssSelector(
                "h4.px-4.pb-4.text-uppercase, h4.px-4.pb-4, h4.text-uppercase"));
            foreach (var title in titles)
            {
                string actual = title.Text.Trim().ToUpperInvariant();
                if (actual == expectedUpper || actual.StartsWith(expectedUpper))
                    return title;
            }
            return null;
        });
    }

    private void AssertReadonlyInput(string id, string expectedValue)
    {
        IWebElement input = wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(input.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(input.GetAttribute("disabled"), Is.Not.Null);
    }

    private void SelectByValue(By locator, string value)
    {
        IWebElement dropdown = wait.Until(ExpectedConditions.ElementIsVisible(locator));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            dropdown);
        Thread.Sleep(300);
        new SelectElement(dropdown).SelectByValue(value);
        Thread.Sleep(500);
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {
        Assert.That(driver.FindElement(
            By.XPath($"//label[contains(normalize-space(),'{documentTitle}')]")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-12413"));
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
