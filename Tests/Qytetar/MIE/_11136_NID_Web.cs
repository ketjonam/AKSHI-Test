using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.MIE;

[Category("MIE")]
[Category("11136")]
public class _11136_NID_Web : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "11136";
    protected override string? ServiceTitle => "Aplikim_i_Ri_NID_11136";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Aplikim për shtesa në kategoritë e licencës individuale në studim e projektim dhe/ose në mbikëqyrje e kolaudim";

    [Test]
    public void Aplikim_i_Ri_NID_11136()
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
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(), Does.StartWith("DETAJET E INDIVIDIT"));

        Log("Assert te dhenat e individit te para-plotesuara");
        AssertDisabledFieldByLabel("NID:", Settings.Qytetar.Username);
        AssertDisabledFieldByLabel("Emri:", "Katerina");
        AssertDisabledFieldByLabel("Mbiemri:", "Jançe");
        AssertDisabledFieldByLabel("Atësia:", "Foti");
        AssertDisabledFieldByLabel("Datëlindja:", "13.04.1993");
        AssertDisabledFieldByLabel("Vendlindja:", "Korçë");
        AssertDisabledFieldByLabel("Rrethi:", "TIRANË");
        AssertDisabledFieldByLabel("Qarku:", "TIRANË");
        AssertDisabledFieldByLabel("Amësia:", "Manushaqe");
        AssertDisabledFieldByLabel("Emri NJQV:", "Shkolla \"Gustav Mayer\"");
        AssertDisabledFieldByLabel("Statusi civil:", "E Martuar");

        IWebElement gjinia = FindFieldByLabel("Gjinia:");
        Assert.That(gjinia.GetAttribute("disabled"), Is.Not.Null);
        var gjiniaSelect = new SelectElement(gjinia);
        Assert.That(gjiniaSelect.SelectedOption.GetAttribute("value"), Is.EqualTo("F"));
        Assert.That(gjiniaSelect.SelectedOption.Text.Trim(), Is.EqualTo("Femër"));

        IWebElement adresa2 = FindFieldByLabel("Adresa 2:");
        Assert.That(adresa2.GetAttribute("disabled"), Is.Null);
        Assert.That(adresa2.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement rruga = FindFieldByLabel("Rruga:");
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
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(), Does.StartWith("KONTAKTI"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

        Log("Assert 4 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert fushat e kontaktit");
        IWebElement nrTel = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("nrTel")));
        Assert.That(nrTel.GetAttribute("disabled"), Is.Null);
        Assert.That(nrTel.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement nrCel = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("nrCel")));
        Assert.That(nrCel.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(nrCel.GetAttribute("value").Trim(), Is.EqualTo("+355697008820"));

        IWebElement email = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("email")));
        Assert.That(email.GetAttribute("type"), Is.EqualTo("email"));
        Assert.That(email.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(email.GetAttribute("value").Trim(), Is.EqualTo("katerina.jance@kreatx.com"));

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("KATEGORITË");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(), Does.StartWith("KATEGORITË"));

        Log("Assert kohëzgjatja Step 3");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

        Log("Assert 4 hapa, tre te paret aktiv");
        AssertSteps(3);

        Log("Assert pemen e kategorive");
        IWebElement categoryTree = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("ul[aria-label='category-tree']")));
        Assert.That(categoryTree.Displayed, Is.True);

        AssertDisabledCategory("LICENCE MBIKEQYRJE DHE KOLAUDIM I PUNIMEVE TE ZBATIMIT NE NDERTIM");
        AssertDisabledCategory("LICENCE PROJEKTIMI NE NDERTIM");

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 4 Title");
        IWebElement Step4Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(), Does.StartWith("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 4");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

        Log("Assert 4 hapa, te gjithe aktiv");
        AssertSteps(4);

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Licencën që disponon, në të cilën kërkon shtesat')]")).Displayed, Is.True);

        Log("Assert document-upload CV");
        AssertDocumentUpload(
            "fuCVUpload",
            "CV e individit, ku pasqyrohet veprimtaria profesionale e tij e kryer nga marrja e diplomës");

        Log("Assert document-upload Dokumentacion teknik");
        AssertDocumentUpload(
            "fuDokJustifikuesUpload",
            "Dokumentacion teknik justifikues për veprimtarinë e kryer");

        Log("Assert document-upload Vetëdeklarim");
        AssertDocumentUpload(
            "fuVetdeklarimUpload",
            "Vetëdeklarim i individit");
        Assert.That(driver.FindElement(By.CssSelector("a[href='/service/VETDEKLARIM_6_1.pdf']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.CssSelector("a[href='/service/VETDEKLARIM_6_2.pdf']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.CssSelector("a[href='/service/VETDEKLARIM_6_1.pdf']")).Text.Trim(),
            Is.EqualTo("[Shkarko]"));
        Assert.That(driver.FindElement(By.CssSelector("a[href='/service/VETDEKLARIM_6_2.pdf']")).Text.Trim(),
            Is.EqualTo("[Shkarko]"));

        Log("Assert document-upload Pagesa e tarifës");
        AssertDocumentUpload(
            "fuMandatPagesaUpload",
            "Pagesa e tarifës/tarifave");

        string documentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";

        Log("Ngarko CV");
        UploadDocument("fuCVUpload", documentPath);

        Log("Ngarko Dokumentacion teknik");
        UploadDocument("fuDokJustifikuesUpload", documentPath);

        Log("Ngarko Vetëdeklarim");
        UploadDocument("fuVetdeklarimUpload", documentPath);

        Log("Ngarko Pagesa e tarifës");
        UploadDocument("fuMandatPagesaUpload", documentPath);

        Log("Assert checkbox i pranimit eshte i pazgjedhur");
        IWebElement agreeCheck = wait.Until(ExpectedConditions.ElementExists(By.Id("agreeCheck")));
        Assert.That(agreeCheck.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='agreeCheck']")).Text.Trim(),
            Does.Contain("Me klikimin e këtij butoni, ju bini dakord që këto dokumente të sigurohen për ju nga nëpunësi i administratës."));

        Log("Kliko Dergo pa pranuar kushtet");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(1000);
        Assert.That(WaitForStepTitle("DOKUMENTACIONI").Text.Trim().ToUpperInvariant(),
            Does.StartWith("DOKUMENTACIONI"));

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
        //Assert.That(referenceNumber.Text, Does.Contain("11136-"));
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
                string actual = title.Text.Trim().ToUpperInvariant();
                if (actual == expectedUpper || actual.StartsWith(expectedUpper))
                    return title;
            }
            return null;
        });
    }

    private IWebElement FindFieldByLabel(string labelText)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//label[contains(normalize-space(),'{labelText}')]/following-sibling::*[self::input or self::select][1]")));
    }

    private void AssertDisabledFieldByLabel(string labelText, string expectedValue)
    {
        IWebElement field = FindFieldByLabel(labelText);
        Assert.That(field.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(field.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
    }

    private void AssertDisabledCategory(string categoryText)
    {
        IWebElement categoryLi = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//ul[@aria-label='category-tree']//li[@role='treeitem'][.//span[contains(normalize-space(),'{categoryText}')]]")));
        Assert.That(categoryLi.Displayed, Is.True, $"Kategoria nuk u gjet: {categoryText}");

        IWebElement checkbox = categoryLi.FindElement(By.XPath(".//input[@type='checkbox']"));
        Assert.That(checkbox.GetAttribute("disabled"), Is.Not.Null,
            $"Checkbox i kategorise duhet te jete disabled: {categoryText}");
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {
        Assert.That(driver.FindElement(
            By.XPath($"//span[contains(normalize-space(),'{documentTitle}')]")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-11136"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("20"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("20"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të ngarkuar dokumentin"));

        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të ngarkuar dokumentin"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='hint']")).Text.Trim(),
            Is.EqualTo("Formatet e lejuara: PDF. Madhësia maksimale: 20MB."));
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
