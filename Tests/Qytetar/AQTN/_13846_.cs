using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.AQTN;

[Category("AQTN")]
[Category("13846")]
public class _13846_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "13846";
    protected override string? ServiceTitle => "FleteProjektiTip";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName = "Dhënie e një flete projekti tip";
    private const string ExpectedAddress =
        "FROSINA PLAKU; Nd. 88; H. 2; Ap. 9; NJËSIA ADMINISTRATIVE NR. 7; NJËSIA BASHKIAKE NR. 7; 1023; TIRANË";
    private const string HartaUploadId = "fileHarta-compat";

    [Test]
    public void FleteProjektiTip()
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
        AssertReadOnlyInputByLabel("NID", Settings.Qytetar.Username);
        AssertReadOnlyInputByLabel("Emri", "Katerina");
        AssertReadOnlyInputByLabel("Mbiemri", "Jançe");
        AssertReadOnlyInputByLabel("Atësia", "Foti");
        AssertReadOnlyInputByLabel("E-mail", "katerina.jance@kreatx.com");
        AssertReadOnlyInputByLabel("Nr. Tel. Celular", "+355697008820");
        AssertReadOnlyInputByLabel("Datëlindja", "13.04.1993");
        AssertReadOnlyInputByLabel("Vendlindja", "Korçë");
        AssertReadOnlyInputByLabel("Rrethi", "TIRANË");

        Log("Assert gjinia");
        IWebElement genderMale = wait.Until(ExpectedConditions.ElementExists(By.Id("genderMaleChk")));
        IWebElement genderFemale = wait.Until(ExpectedConditions.ElementExists(By.Id("genderFemaleChk")));
        Assert.That(genderMale.GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(genderMale.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(driver.FindElement(By.CssSelector("label[for='genderMaleChk']")).Text.Trim(),
            Is.EqualTo("Mashkull"));
        Assert.That(genderFemale.GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(genderFemale.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(driver.FindElement(By.CssSelector("label[for='genderFemaleChk']")).Text.Trim(),
            Is.EqualTo("Femër"));
        Assert.That(genderFemale.Selected, Is.True);

        Log("Assert fushat e editueshme");
        IWebElement qyteti = InputByLabel("Qyteti");
        Assert.That(qyteti.GetAttribute("readonly"), Is.Null);
        Assert.That(qyteti.GetAttribute("disabled"), Is.Null);
        Assert.That(qyteti.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement shtetesia = InputByLabel("Shtetësia");
        Assert.That(shtetesia.GetAttribute("readonly"), Is.Null);
        Assert.That(shtetesia.GetAttribute("disabled"), Is.Null);
        Assert.That(shtetesia.GetAttribute("value").Trim(), Is.EqualTo("Shqiptare"));
        Assert.That(FindLabel("Shtetësia").Text, Does.Contain("*"));

        IWebElement kodiPostar = InputByLabel("Kodi postar");
        Assert.That(kodiPostar.GetAttribute("readonly"), Is.Null);
        Assert.That(kodiPostar.GetAttribute("disabled"), Is.Null);
        Assert.That(kodiPostar.GetAttribute("maxlength"), Is.EqualTo("10"));
        Assert.That(kodiPostar.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement telFiks = InputByLabel("Nr. Tel. Fiks");
        Assert.That(telFiks.GetAttribute("readonly"), Is.Null);
        Assert.That(telFiks.GetAttribute("disabled"), Is.Null);
        Assert.That(telFiks.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement adresa = InputByLabel("Adresa");
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
        IWebElement emertimiDokumentit = InputByLabel("Emërtimi i dokumentit arkivor");
        Assert.That(emertimiDokumentit.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Emërtimi i dokumentit arkivor").Text, Does.Contain("*"));

        IWebElement vitiProjektit = InputByLabel("Viti i projektit");
        Assert.That(vitiProjektit.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(vitiProjektit.GetAttribute("min"), Is.EqualTo("1"));
        Assert.That(vitiProjektit.GetAttribute("max"), Is.EqualTo("9999"));
        Assert.That(vitiProjektit.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Viti i projektit").Text, Does.Not.Contain("*"));

        IWebElement autoriProjektit = InputByLabel("Autori i projektit");
        Assert.That(autoriProjektit.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement nrFleteve = InputByLabel("Numri i fletëve");
        Assert.That(nrFleteve.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(nrFleteve.GetAttribute("min"), Is.EqualTo("1"));
        Assert.That(nrFleteve.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Assert specifikimi i dokumentit arkivor");
        Assert.That(FindLabel("Specifikimi i dokumentit arkivor").Text, Does.Contain("*"));
        AssertSpecifikimi("Arkitekture", "Arkitekturë");
        AssertSpecifikimi("Konstruksion", "Konstruksion");
        AssertSpecifikimi("Hidrosanitare", "Hidrosanitare");
        AssertSpecifikimi("Elektrike", "Elektrike");
        AssertSpecifikimi("TeTjera", "Të tjera");

        IWebElement specifikimTextarea = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("form textarea.ealb-input")));
        Assert.That(specifikimTextarea.GetAttribute("readonly"), Is.Not.Null);

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme");
        emertimiDokumentit = InputByLabel("Emërtimi i dokumentit arkivor");
        FillInput(emertimiDokumentit, "test");
        Assert.That(emertimiDokumentit.GetAttribute("value").Trim(), Is.EqualTo("test"));

        IWebElement hidrosanitare = wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("input[name='specifikimiDokArk'][value='Hidrosanitare']")));
        hidrosanitare.Click();
        Assert.That(hidrosanitare.Selected, Is.True);

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
        AssertDocumentUpload(HartaUploadId, "Vendndodhja në hartë");

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
        UploadDocument(HartaUploadId, documentPath);

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

    private IWebElement FindLabel(string labelText)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//label[contains(normalize-space(.), '{labelText}')]")));
    }

    private IWebElement InputByLabel(string labelText)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//label[contains(normalize-space(.), '{labelText}')]/following-sibling::input[1]")));
    }

    private void AssertReadOnlyInputByLabel(string labelText, string expectedValue)
    {
        IWebElement input = InputByLabel(labelText);
        Assert.That(input.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
    }

    private void AssertSpecifikimi(string value, string labelText)
    {
        IWebElement checkbox = wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector($"input[name='specifikimiDokArk'][value='{value}']")));
        Assert.That(checkbox.GetAttribute("type"), Is.EqualTo("checkbox"));
        IWebElement label = checkbox.FindElement(By.XPath("./ancestor::label"));
        Assert.That(label.Text.Trim(), Is.EqualTo(labelText));
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {
        Assert.That(driver.FindElement(
            By.XPath($"//label[contains(normalize-space(),'{documentTitle}')]")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-13846"));
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
