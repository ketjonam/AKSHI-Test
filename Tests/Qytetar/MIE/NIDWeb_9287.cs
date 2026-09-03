using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.MIE;

[Category("MIE")]
[Category("9287")]
public class NIDWeb_9287 : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "9287";
    protected override string? ServiceTitle => "Aplikim_i_Ri_9287";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Licencimi i individëve për herë të parë (shkalla e dytë), për ushtrimin e aktivitetit në fushën e vlerësimit të pasurive të paluajtshme (online)";

    [Test]
    public void Aplikim_i_Ri_9287()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

        Log("Assert 4 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert Title");
        IWebElement Step1Title = WaitForStepTitle("DETAJET E INDIVIDIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("DETAJET E INDIVIDIT"));

        Log("Assert te dhenat e individit te para-plotesuara");
        AssertDisabledInput("nid", Settings.Qytetar.Username);
        AssertDisabledInput("emri", "Katerina");
        AssertDisabledInput("mbiemri", "Jançe");
        AssertDisabledInput("atesia", "Foti");
        AssertDisabledInput("memesia", "Manushaqe");
        AssertDisabledInput("gjinia", "Femër");
        AssertDisabledInput("gjCiv", "E Martuar");
        AssertDisabledInput("vendlindja", "Korçë");
        AssertDisabledInput("datelindja", "13.04.1993");
        AssertDisabledInput("emQarku", "TIRANË");
        AssertDisabledInput("emRrethi", "TIRANË");
        AssertDisabledInput("emriNjqv", "Shkolla \"Gustav Mayer\"");

        IWebElement rruga = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("rruga")));
        Assert.That(rruga.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(rruga.GetAttribute("value").Trim(),
            Is.EqualTo("FROSINA PLAKU; Nd. 88; H. 2; Ap. 9; NJËSIA ADMINISTRATIVE NR. 7; NJËSIA BASHKIAKE NR. 7; 1023; TIRANË"));

        Log("Assert butonat e navigimit Step 1");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("KONTAKTI");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("KONTAKTI"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

        Log("Assert 4 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert fushat e kontaktit");
        IWebElement nrTel = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("nrTel")));
        Assert.That(nrTel.GetAttribute("disabled"), Is.Null);
        Assert.That(nrTel.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        AssertDisabledInput("nrCel", "+355697008820");

        IWebElement email = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("email")));
        Assert.That(email.GetAttribute("type"), Is.EqualTo("email"));
        Assert.That(email.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(email.GetAttribute("value").Trim(), Is.EqualTo("katerina.jance@kreatx.com"));

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("DETAJET E APLIKIMIT");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("DETAJET E APLIKIMIT"));

        Log("Assert kohëzgjatja Step 3");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

        Log("Assert 4 hapa, tre te paret aktiv");
        AssertSteps(3);

        Log("Assert opsionet e licences");
        IWebElement licenseSelect = FindFieldAfterLabel("Licencë për", "select");
        var license = new SelectElement(licenseSelect);
        Assert.That(license.Options.Count, Is.EqualTo(3));
        Assert.That(license.Options[0].GetAttribute("value"), Is.EqualTo("NDERTES_TOKE_TRUALL"));
        Assert.That(license.Options[0].Text.Trim(), Is.EqualTo("Për ndërtesat dhe tokë truall"));
        Assert.That(license.Options[1].GetAttribute("value"), Is.EqualTo("TOKE_BUJQESORE_PYJE_LIVADH"));
        Assert.That(license.Options[1].Text.Trim(),
            Is.EqualTo("Për tokë buqësore, tokë pyjore, kullotë, livadh dhe toke të pafrytshme"));
        Assert.That(license.Options[2].GetAttribute("value"), Is.EqualTo("LINJA_TEKNOLOGJIKE"));
        Assert.That(license.Options[2].Text.Trim(), Is.EqualTo("Linja teknologjike, makineri e pajisje"));
        Assert.That(license.SelectedOption.GetAttribute("value"), Is.EqualTo("NDERTES_TOKE_TRUALL"));

        Log("Assert Shënim dhe Adresa");
        IWebElement shenim = FindFieldAfterLabel("Shënim", "textarea");
        Assert.That(shenim.GetAttribute("maxlength"), Is.EqualTo("200"));
        Assert.That(shenim.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement adresa = FindFieldAfterLabel("Adresa", "textarea");
        Assert.That(adresa.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 4 Title");
        IWebElement Step4Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 4");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

        Log("Assert 4 hapa, te gjithe aktiv");
        AssertSteps(4);

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);

        Log("Assert document-upload Diplomë universitare");
        AssertDocumentUpload(
            "fileDiplomeUpload",
            "Diplomë universitare, e skanuar, origjinale ose e noterizuar");

        Log("Assert document-upload Certifikatë");
        AssertDocumentUpload(
            "fileCertifikateUpload",
            "Certifikatë për kryerjen e programeve të studimit të vazhduar");

        Log("Assert document-upload Vetëdeklarim");
        AssertDocumentUpload(
            "fileVetdeklarimUpload",
            "Vetëdeklarim, ku individi te deklarojë");

        string documentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";

        Log("Ngarko Diplomë universitare");
        UploadDocument("fileDiplomeUpload", documentPath);

        Log("Ngarko Certifikatë");
        UploadDocument("fileCertifikateUpload", documentPath);

        Log("Ngarko Vetëdeklarim");
        UploadDocument("fileVetdeklarimUpload", documentPath);

        Log("Assert checkbox i pranimit eshte i pazgjedhur");
        IWebElement agreeCheck = wait.Until(ExpectedConditions.ElementExists(By.Id("agreeCheck")));
        Assert.That(agreeCheck.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='agreeCheck']")).Text.Trim(),
            Does.Contain("Me klikimin e këtij butoni, ju bini dakord që këto dokumente të sigurohen për ju nga nëpunësi i administratës."));

        Log("Kliko Dergo pa pranuar kushtet");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(1000);
        Assert.That(WaitForStepTitle("DOKUMENTACIONI").Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DOKUMENTACIONI"));

        Log("Zgjidh pranimin e kushteve");
        SafeClick(By.Id("agreeCheck"));
        Assert.That(driver.FindElement(By.Id("agreeCheck")).Selected, Is.True);

        Log("Assert butoni Dergo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.Text.Trim(), Does.Contain("Dërgo"));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        //Log("Kliko Dergo");
        //SafeClick(By.CssSelector("button.ealb-btn-continue"));
        //Thread.Sleep(5000);

        //Log("Assert suksesi");
        //IWebElement successTitle = wait.Until(ExpectedConditions.ElementIsVisible(
        //    By.XPath("//h5[contains(.,'APLIKIMI JUAJ')]")));
        //Assert.That(successTitle.Text.Trim().ToUpperInvariant().Replace("Ë", "E"),
        //    Does.Contain("APLIKIMI JUAJ U DERGUA ME SUKSES"));

        //IWebElement referenceNumber = wait.Until(ExpectedConditions.ElementIsVisible(
        //    By.XPath("//h6[contains(.,'Numri referencë i aplikimit')]")));
        //Assert.That(referenceNumber.Text, Does.Contain("9287-"));
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
            var titles = d.FindElements(By.CssSelector(
                "h5.px-4.my-2.text-uppercase, h4.px-4.pb-4, h4.text-uppercase"));
            foreach (var title in titles)
            {
                if (title.Text.Trim().ToUpperInvariant() == expectedUpper)
                    return title;
            }
            return null;
        });
    }

    private void AssertDisabledInput(string name, string expectedValue)
    {
        IWebElement input = wait.Until(ExpectedConditions.ElementIsVisible(By.Name(name)));
        Assert.That(input.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
    }

    private IWebElement FindFieldAfterLabel(string labelPart, string tagName)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//form//label[contains(.,'{labelPart}')]/following-sibling::{tagName}")));
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {
        Assert.That(driver.FindElement(
            By.XPath($"//span[contains(normalize-space(),'{documentTitle}')]")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-9287"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("15"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("50"));
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
}
