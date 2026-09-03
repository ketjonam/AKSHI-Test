using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.AQTN;

[Category("AQTN")]
[Category("12427")]
public class _12427_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "12427";
    protected override string? ServiceTitle => "MarrjeDosjePronesie";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName = "Marrje dosje pronësie";

    [Test]
    public void MarrjeDosjePronesie()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert 3 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert Step 1 Title");
        IWebElement step1Title = WaitForStepTitle("INFORMACION MBI APLIKUESIN");
        Assert.That(step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("INFORMACION MBI APLIKUESIN"));

        Log("Assert Te dhenat individuale");
        AssertReadonlyInput("nid", Settings.Qytetar.Username);
        AssertReadonlyInput("emri", "Katerina");
        AssertReadonlyInput("mbiemri", "Jançe");
        AssertReadonlyInput("atesia", "Foti");
        AssertReadonlyInput("email", "katerina.jance@kreatx.com");
        AssertReadonlyInput("telCel", "+355697008820");
        AssertReadonlyInput("datelindja", "13.04.1993");
        AssertReadonlyInput("vendlindja", "Korçë");
        AssertReadonlyInput("rrethi", "TIRANË");

        IWebElement genderMale = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("genderMale")));
        Assert.That(genderMale.GetAttribute("value").Trim(), Is.EqualTo("M"));
        Assert.That(genderMale.GetAttribute("disabled"), Is.Not.Null);

        IWebElement genderFemale = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("genderFemale")));
        Assert.That(genderFemale.GetAttribute("value").Trim(), Is.EqualTo("F"));
        Assert.That(genderFemale.GetAttribute("disabled"), Is.Not.Null);

        IWebElement qyteti = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("qyteti")));
        Assert.That(qyteti.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement shtetesia = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("shtetesia")));
        Assert.That(shtetesia.GetAttribute("value").Trim(), Is.EqualTo("Shqiptare"));

        IWebElement kodiPostar = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("kodiPostar")));
        Assert.That(kodiPostar.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(kodiPostar.GetAttribute("maxlength"), Is.EqualTo("10"));

        IWebElement telFiks = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("telFiks")));
        Assert.That(telFiks.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement adresa = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("adresa")));
        Assert.That(adresa.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(adresa.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(adresa.GetAttribute("value").Trim(),
            Is.EqualTo("FROSINA PLAKU; Nd. 88; H. 2; Ap. 9; NJËSIA ADMINISTRATIVE NR. 7; NJËSIA BASHKIAKE NR. 7; 1023; TIRANË"));

        Log("Assert butonat e navigimit Step 1");
        AssertNavigationButtons("Vazhdo");

        Log("Ploteso Qyteti dhe Kodi postar");
        FillInput(qyteti, "TEST");
        FillInput(kodiPostar, "1024");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement step2Title = WaitForStepTitle("INFORMACION SPECIFIK MBI APLIKIMIN");
        Assert.That(step2Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("INFORMACION SPECIFIK MBI APLIKIMIN"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert 3 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert fushat e hapit 2");
        IWebElement emriSpecifik = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("emriSpecifik")));
        Assert.That(emriSpecifik.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(emriSpecifik.GetAttribute("maxlength"), Is.EqualTo("40"));

        IWebElement mbiemriSpecifik = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("mbiemriSpecifik")));
        Assert.That(mbiemriSpecifik.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(mbiemriSpecifik.GetAttribute("maxlength"), Is.EqualTo("40"));

        IWebElement vitiLejesNdertimit = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("vitiLejesNdertimit")));
        Assert.That(vitiLejesNdertimit.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(vitiLejesNdertimit.GetAttribute("type"), Is.EqualTo("number"));

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme");
        FillInput(emriSpecifik, "test");
        FillInput(mbiemriSpecifik, "test");
        FillInput(vitiLejesNdertimit, "1");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement step3Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(step3Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("DOKUMENTACIONI"));

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

        string documentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";

        Log("Ngarko Vendndodhja në hartë");
        UploadDocument("fileHartaUpload", documentPath);

        Log("Assert butoni Dergo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.Text.Trim(), Does.Contain("Dërgo"));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        ClickDergo();

        //Log("Assert suksesi");
        //IWebElement successTitle = wait.Until(ExpectedConditions.ElementIsVisible(
        //    By.XPath("//h5[contains(.,'APLIKIMI JUAJ')]")));
        //Assert.That(successTitle.Text.Trim().ToUpperInvariant().Replace("Ë", "E"),
        //    Does.Contain("APLIKIMI JUAJ U DERGUA ME SUKSES"));

        //IWebElement referenceNumber = wait.Until(ExpectedConditions.ElementIsVisible(
        //    By.XPath("//h6[contains(.,'Numri referencë i aplikimit')]")));
        //Assert.That(referenceNumber.Text, Does.Contain("12427-"));
        //Assert.That(driver.Url, Does.Contain("/mesazh"));

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
                "h4.px-4.pb-4.text-uppercase, h4.text-uppercase"));
            foreach (var title in titles)
            {
                if (title.Text.Trim().ToUpperInvariant() == expectedUpper)
                    return title;
            }
            return null;
        });
    }

    private void AssertReadonlyInput(string id, string expectedValue)
    {
        IWebElement input = wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
        Assert.That(input.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {
        Assert.That(driver.FindElement(
            By.XPath($"//label[contains(.,'{documentTitle}')]")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-12427"));
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
