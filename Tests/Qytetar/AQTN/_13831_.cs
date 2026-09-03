using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.AQTN;

[Category("AQTN")]
[Category("13831")]
public class _13831_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "13831";
    protected override string? ServiceTitle => "PikeKordinate";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName = "Dhënie e një pike koordinate";
    private const string ExpectedAddress =
        "FROSINA PLAKU; Nd. 88; H. 2; Ap. 9; NJËSIA ADMINISTRATIVE NR. 7; NJËSIA BASHKIAKE NR. 7; 1023; TIRANË";

    [Test]
    public void PikeKordinate()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert 3 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert Step 1 Title");
        IWebElement Step1Title = WaitForStepTitle("INFORMACION MBI APLIKANTIN");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("INFORMACION MBI APLIKANTIN"));

        Log("Assert te dhenat e aplikantit te para-plotesuara");
        AssertReadOnlyInput("nid", Settings.Qytetar.Username);
        AssertReadOnlyInput("emri", "Katerina");
        AssertReadOnlyInput("mbiemri", "Jançe");
        AssertReadOnlyInput("atesia", "Foti");
        AssertReadOnlyInput("email", "katerina.jance@kreatx.com");
        Assert.That(driver.FindElement(By.Id("email")).GetAttribute("type"), Is.EqualTo("email"));
        AssertReadOnlyInput("telCel", "+355697008820");
        AssertReadOnlyInput("datelindja", "13.04.1993");
        AssertReadOnlyInput("vendlindja", "Korçë");
        AssertReadOnlyInput("rrethi", "TIRANË");

        Log("Assert gjinia");
        IWebElement genderMale = wait.Until(ExpectedConditions.ElementExists(By.Id("genderMale")));
        IWebElement genderFemale = wait.Until(ExpectedConditions.ElementExists(By.Id("genderFemale")));
        Assert.That(genderMale.GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(genderMale.GetAttribute("value"), Is.EqualTo("M"));
        Assert.That(genderMale.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(driver.FindElement(By.CssSelector("label[for='genderMale']")).Text.Trim(),
            Is.EqualTo("Mashkull"));
        Assert.That(genderFemale.GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(genderFemale.GetAttribute("value"), Is.EqualTo("F"));
        Assert.That(genderFemale.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(driver.FindElement(By.CssSelector("label[for='genderFemale']")).Text.Trim(),
            Is.EqualTo("Femër"));
        Assert.That(genderFemale.Selected, Is.True);

        Log("Assert fushat e editueshme");
        IWebElement qyteti = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("qyteti")));
        Assert.That(qyteti.GetAttribute("readonly"), Is.Null);
        Assert.That(qyteti.GetAttribute("disabled"), Is.Null);
        Assert.That(qyteti.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement shtetesia = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("shtetesia")));
        Assert.That(shtetesia.GetAttribute("readonly"), Is.Null);
        Assert.That(shtetesia.GetAttribute("disabled"), Is.Null);
        Assert.That(shtetesia.GetAttribute("value").Trim(), Is.EqualTo("Shqiptare"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='shtetesia']")).Text,
            Does.Contain("*"));

        IWebElement kodiPostar = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("kodiPostar")));
        Assert.That(kodiPostar.GetAttribute("readonly"), Is.Null);
        Assert.That(kodiPostar.GetAttribute("disabled"), Is.Null);
        Assert.That(kodiPostar.GetAttribute("maxlength"), Is.EqualTo("10"));
        Assert.That(kodiPostar.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement telFiks = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("telFiks")));
        Assert.That(telFiks.GetAttribute("readonly"), Is.Null);
        Assert.That(telFiks.GetAttribute("disabled"), Is.Null);
        Assert.That(telFiks.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement adresa = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("adresa")));
        Assert.That(adresa.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(adresa.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(adresa.GetAttribute("value").Trim(), Is.EqualTo(ExpectedAddress));

        Log("Assert butonat e navigimit Step 1");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("INFORMACION SPECIFIK MBI APLIKIMIN");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("INFORMACION SPECIFIK MBI APLIKIMIN"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert 3 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert fushat e informacionit specifik");
        IWebElement vitiHartes = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("vitiHartes")));
        Assert.That(vitiHartes.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(vitiHartes.GetAttribute("maxlength"), Is.EqualTo("30"));
        Assert.That(vitiHartes.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(driver.FindElement(By.CssSelector("label[for='vitiHartes']")).Text,
            Does.Contain("*"));

        IWebElement specifikimi = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("specifikimi")));
        Assert.That(specifikimi.GetAttribute("maxlength"), Is.EqualTo("255"));
        Assert.That(specifikimi.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(driver.FindElement(By.CssSelector("label[for='specifikimi']")).Text,
            Does.Contain("*"));

        IWebElement qytetiHartes = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("qytetiHartes")));
        Assert.That(qytetiHartes.GetAttribute("maxlength"), Is.EqualTo("80"));
        Assert.That(qytetiHartes.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(driver.FindElement(By.CssSelector("label[for='qytetiHartes']")).Text,
            Does.Contain("*"));

        IWebElement rugaZona = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("rugaZona")));
        Assert.That(rugaZona.GetAttribute("maxlength"), Is.EqualTo("80"));
        Assert.That(rugaZona.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(driver.FindElement(By.CssSelector("label[for='rugaZona']")).Text,
            Does.Contain("*"));

        IWebElement emertimiHartes = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("emertimiHartes")));
        Assert.That(emertimiHartes.GetAttribute("maxlength"), Is.EqualTo("120"));
        Assert.That(emertimiHartes.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(driver.FindElement(By.CssSelector("label[for='emertimiHartes']")).Text,
            Does.Not.Contain("*"));

        IWebElement teTjera = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("teTjera")));
        Assert.That(teTjera.GetAttribute("maxlength"), Is.EqualTo("1000"));
        Assert.That(teTjera.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(driver.FindElement(By.CssSelector("label[for='teTjera']")).Text,
            Does.Not.Contain("*"));

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme");
        vitiHartes = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("vitiHartes")));
        FillInput(vitiHartes, "1");
        Assert.That(vitiHartes.GetAttribute("value").Trim(), Is.EqualTo("1"));

        specifikimi = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("specifikimi")));
        FillInput(specifikimi, "1");
        Assert.That(specifikimi.GetAttribute("value").Trim(), Is.EqualTo("1"));

        qytetiHartes = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("qytetiHartes")));
        FillInput(qytetiHartes, "test");
        Assert.That(qytetiHartes.GetAttribute("value").Trim(), Is.EqualTo("test"));

        rugaZona = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("rugaZona")));
        FillInput(rugaZona, "test");
        Assert.That(rugaZona.GetAttribute("value").Trim(), Is.EqualTo("test"));

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(), Does.StartWith("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 3");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert 3 hapa, te gjithe aktiv");
        AssertSteps(3);

        Log("Assert seksioni i dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//span[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);

        Log("Assert document-upload Vendndodhja në hartë");
        AssertDocumentUpload("fileHartaUpload", "Vendndodhja në hartë");

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Dërgo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Kliko Dergo pa ngarkuar dokumentin");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(1000);
        Assert.That(WaitForStepTitle("DOKUMENTACIONI").Text.Trim().ToUpperInvariant(),
            Does.StartWith("DOKUMENTACIONI"));

        string documentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";

        Log("Ngarko Vendndodhja në hartë");
        UploadDocument("fileHartaUpload", documentPath);

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
                "h5.px-4.my-2.text-uppercase, h4.px-4.pb-4, h4.text-uppercase"));
            foreach (var title in titles)
            {
                string actual = title.Text.Trim().ToUpperInvariant();
                if (actual == expectedUpper || actual.StartsWith(expectedUpper))
                    return title;
            }
            return null;
        });
    }

    private void AssertReadOnlyInput(string id, string expectedValue)
    {
        IWebElement input = wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
        Assert.That(input.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {
        Assert.That(driver.FindElement(
            By.XPath($"//label[contains(normalize-space(),'{documentTitle}')]")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-13831"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("5"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("50"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf,.jpeg,.jpg,.png,.gif"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të ngarkuar dokumentin"));

        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të ngarkuar dokumentin"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='hint']")).Text.Trim(),
            Is.EqualTo("Formatet e lejuara: PDF, JPEG, JPG, PNG, GIF. Madhësia maksimale: 5MB."));
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
