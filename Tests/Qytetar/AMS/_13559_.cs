using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.AMS;

[Category("AMS")]
[Category("13559")]
public class _13559_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "13559";
    protected override string? ServiceTitle => "RinovimiLicensesAdministratoreveTeFalimentit";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Aplikim për rinovimin e licencës së administratorëve të falimentimit";
    private const string ExpectedAddress =
        "TIRANË,FROSINA PLAKU; Nd. 88; H. 2; Ap. 9; NJËSIA ADMINISTRATIVE NR. 7; NJËSIA BASHKIAKE NR. 7; 1023; TIRANË";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";

    [Test]
    public void RinovimiLicensesAdministratoreveTeFalimentit()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        AssertDuration("6 minuta kohëzgjatje");

        Log("Assert 5 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert Step 1 Title");
        IWebElement Step1Title = WaitForStepTitle("INFORMACION I PËRGJITHSHËM");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("INFORMACION I PËRGJITHSHËM"));

        Log("Assert Te dhenat individuale");
        AssertReadonlyInput("Emri", "Katerina");
        AssertReadonlyInput("Mbiemri", "Jançe");
        AssertReadonlyInput("Emri i babait", "Foti");
        AssertReadonlyInput("NID", Settings.Qytetar.Username);
        AssertReadonlyInput("Datëlindja", "13.04.1993");
        AssertReadonlyInput("Vendlindja", "Korçë");
        AssertReadonlyInput("Shtetësia", "Shqiptare");
        AssertReadonlyInput("Qyteti", "TIRANË");
        AssertReadonlyInput("Rrethi", "TIRANË");
        AssertReadonlyInput("Nr Tel. Cel", "+355697008820");
        AssertReadonlyInput("E-mail", "katerina.jance@kreatx.com");
        AssertReadonlyInput("Adresa", ExpectedAddress);

        IWebElement genderMale = wait.Until(ExpectedConditions.ElementExists(By.Id("gender-m")));
        IWebElement genderFemale = wait.Until(ExpectedConditions.ElementExists(By.Id("gender-f")));
        Assert.That(genderMale.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(genderFemale.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(genderMale.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(genderFemale.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(genderFemale.Selected, Is.True);
        Assert.That(genderMale.Selected, Is.False);

        Log("Assert butonat e navigimit Step 1");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("KOMPANIA NËSE JENI I PUNËSUAR");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("KOMPANIA NËSE JENI I PUNËSUAR"));

        Log("Assert kohëzgjatja Step 2");
        AssertDuration("6 minuta kohëzgjatje");

        Log("Assert 5 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert fushat e kompanise");
        AssertEmptyInput("NIPT");
        AssertEmptyInput("Nr. Telefoni i Zyrës");
        AssertEmptyInput("E-mail i Zyrës");
        AssertEmptyInput("Nr telefoni i punëdhënësit");
        AssertEmptyInput("Punëdhënësi aktual");
        AssertEmptyInput("Organizata apo shoqata profesionale");
        AssertEmptyInput("Adresa e Zyrës");

        IWebElement startDate = FindInputByLabel("Data e fillimit të veprimtarisë", exact: false);
        Assert.That(startDate.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(startDate.GetAttribute("placeholder"), Is.EqualTo("dd.mm.yyyy"));
        Assert.That(startDate.GetAttribute("class"), Does.Contain("flatpickr-input"));

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("DEKLARIME");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("DEKLARIME"));

        Log("Assert kohëzgjatja Step 3");
        AssertDuration("6 minuta kohëzgjatje");

        Log("Assert 5 hapa, tre te paret aktiv");
        AssertSteps(3);

        Log("Assert seksionet e deklarimeve");
        Assert.That(driver.FindElement(
            By.XPath("//h5[contains(.,'Deklarime në lidhje me profesionin')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h5[contains(.,'Deklarime personale')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h5[contains(.,'Deklarime mbi përdorimin e licencës')]")).Displayed, Is.True);

        AssertUnchecked("noBankruptcy",
            "Nuk kam kaluar personalisht në procedura falimenti");
        AssertUnchecked("noDirectorBankruptcy",
            "Nuk kam qenë anëtar i këshillave mbikëqyrëse");

        IWebElement notConvicted = wait.Until(ExpectedConditions.ElementExists(By.Id("notConvicted")));
        IWebElement convicted = wait.Until(ExpectedConditions.ElementExists(By.Id("convicted")));
        Assert.That(notConvicted.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(convicted.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(notConvicted.Selected, Is.False);
        Assert.That(convicted.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='notConvicted']")).Text.Trim(),
            Is.EqualTo("Jam i padënuar penalisht"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='convicted']")).Text.Trim(),
            Is.EqualTo("Jam i dënuar penalisht"));

        IWebElement activeLicense = wait.Until(ExpectedConditions.ElementExists(By.Id("active-license")));
        IWebElement passiveLicense = wait.Until(ExpectedConditions.ElementExists(By.Id("passive-license")));
        Assert.That(activeLicense.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(passiveLicense.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(activeLicense.Selected, Is.False);
        Assert.That(passiveLicense.Selected, Is.False);

        AssertUnchecked("provides-activity-data",
            "Për çdo ndryshim të vendbanimit apo vendit të ushtrimit të veprimtarisë");

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Assert.That(WaitForStepTitle("DEKLARIME").Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DEKLARIME"));
        IWebElement msgErrorDekl = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//*[contains(normalize-space(.),'Përzgjidhni një vlerë për të vazhduar')]")));
        Assert.That(msgErrorDekl.Text.Trim(), Does.Contain("Përzgjidhni një vlerë për të vazhduar"));

        Log("Kryej deklarimet e profesionit");
        ClickCheckbox("noBankruptcy");
        ClickCheckbox("noDirectorBankruptcy");

        Log("Kryej deklarimet personale");
        SafeClick(By.Id("notConvicted"));

        Log("Zgjidh statusin e licenses");
        SafeClick(By.Id("active-license"));

        Log("Kryej deklarimin e pranimit te kushteve");
        ClickCheckbox("provides-activity-data");

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 4 Title");
        IWebElement Step4Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 4");
        AssertDuration("6 minuta kohëzgjatje");

        Log("Assert 5 hapa, kater te paret aktiv");
        AssertSteps(4);

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumenta që ngarkohen nga Aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumenta që ngarkohen nga nëpunësit e administratës publike')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'nuk nevojitet të sigurohet dokumentacion nga nëpunësi i administratës')]")).Displayed, Is.True);

        Log("Assert document-upload");
        AssertDocumentUpload("documentUpload", "provon se ka shlyer detyrimet (Mandatpagesa)");

        Log("Assert butonat e navigimit Step 4");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa ngarkuar dokumentin");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(1000);
        Assert.That(WaitForStepTitle("DOKUMENTACIONI").Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DOKUMENTACIONI"));

        Log("Ngarko dokumentin Mandatpagesa");
        UploadDocument("documentUpload", DocumentPath);

        Log("Kliko Vazhdo Step 4");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 5 Title");
        IWebElement Step5Title = WaitForStepTitle("DEKLARATË MBI USHTRIMIN E VEPRIMTARISË");
        Assert.That(Step5Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DEKLARATË MBI USHTRIMIN E VEPRIMTARISË"));

        Log("Assert kohëzgjatja Step 5");
        AssertDuration("6 minuta kohëzgjatje");

        Log("Assert 5 hapa, te gjithe aktiv");
        AssertSteps(5);

        Log("Assert checkboxet e deklarates");
        AssertUnchecked("meets-criteria",
            "vazhdon të plotësojë kriteret për mbajtjen e licencës");
        AssertUnchecked("provides-activity-data",
            "Të dhënat e aktiviteteve që administratori ka kryer");
        AssertUnchecked("has-training-proof",
            "trajnimet vazhduese profesionale");
        AssertUnchecked("no-disciplinary-proof",
            "nuk ishte objekt i ndonjë mase disiplinore");
        AssertUnchecked("accepts-terms",
            "të dhënat e paraqitura në këtë formular janë të vërteta");
        AssertUnchecked("wants-physical-copy",
            "Përgjigjen e kërkoj edhe si dokument fizik");

        IWebElement activityDetails = wait.Until(ExpectedConditions.ElementExists(By.Id("activity-details")));
        Assert.That(activityDetails.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(activityDetails.GetAttribute("maxlength"), Is.EqualTo("250"));

        Log("Assert butonat e navigimit Step 5");
        AssertNavigationButtons("Dërgo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Ploteso checkbox e dekarates");
        ClickCheckbox("meets-criteria");
        ClickCheckbox("provides-activity-data");
        FillInput(activityDetails, "test");
        ClickCheckbox("has-training-proof");
        ClickCheckbox("no-disciplinary-proof");
        ClickCheckbox("accepts-terms");

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
        Assert.That(steps.Count, Is.EqualTo(5));
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
                "h5.px-4.my-2.text-uppercase, h4.px-4.my-2.text-uppercase, h4.px-4.pb-4, h4.text-uppercase, h5.text-uppercase"));
            foreach (var title in titles)
            {
                string actual = title.Text.Trim().ToUpperInvariant();
                if (actual == expectedUpper || actual.StartsWith(expectedUpper))
                    return title;
            }
            return null;
        });
    }

    private IWebElement FindInputByLabel(string labelText, bool exact = true)
    {
        string labelPred = exact
            ? $"normalize-space()='{labelText}'"
            : $"contains(normalize-space(),'{labelText}')";
        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//label[{labelPred}]/following-sibling::input[contains(@class,'ealb-input')] | //label[{labelPred}]/following-sibling::div//input[contains(@class,'ealb-input')]")));
    }

    private void AssertReadonlyInput(string labelText, string expectedValue)
    {
        IWebElement field = FindInputByLabel(labelText);
        Assert.That(field.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(field.GetAttribute("readonly"), Is.Not.Null);
    }

    private void AssertEmptyInput(string labelText)
    {
        IWebElement field = FindInputByLabel(labelText, exact: false);
        Assert.That(field.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
    }

    private void AssertUnchecked(string id, string labelPart)
    {
        IWebElement checkbox = wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
        Assert.That(checkbox.GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(checkbox.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector($"label[for='{id}']")).Text.Trim(),
            Does.Contain(labelPart));
    }

    private void ClickCheckbox(string id)
    {
        SafeClick(By.CssSelector($"label[for='{id}']"));
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {
        Assert.That(driver.FindElement(
            By.XPath($"//label[contains(normalize-space(),'{documentTitle}')]")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-13559"));
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

        SendFileToUpload(uploadId, filePath);

        var deadline = DateTime.UtcNow + TimeSpan.FromSeconds(60);
        int attempts = 1;
        while (DateTime.UtcNow < deadline)
        {
            string blob = UploadUiText();
            bool failed = blob.Contains("Dështoi ngarkimi", StringComparison.OrdinalIgnoreCase);
            bool processing = blob.Contains("në përpunim", StringComparison.OrdinalIgnoreCase);
            bool success = blob.Contains("Ngarkuar me sukses", StringComparison.OrdinalIgnoreCase);
            if (success && !failed && !processing)
                return;

            if (failed)
            {
                if (attempts >= 3)
                    Assert.Fail("Ngarkimi i dokumentit deshtoi pas 3 provave.");

                attempts++;
                Log($"Ngarkimi deshtoi; po riprovohet (prova {attempts}).");
                SendFileToUpload(uploadId, filePath);
                Thread.Sleep(1500);
                continue;
            }

            Thread.Sleep(400);
        }

        Assert.Fail("Ngarkimi i dokumentit nuk u konfirmua (pritje për 'Ngarkuar me sukses' pa gabim).");
    }

    private void SendFileToUpload(string uploadId, string filePath)
    {
        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            docUpload);
        Thread.Sleep(300);
        ISearchContext shadow = docUpload.GetShadowRoot();
        IWebElement fileInput = shadow.FindElement(By.CssSelector("[data-role='file-input']"));
        fileInput.SendKeys(filePath);
    }

    private string UploadUiText()
    {
        var parts = new List<string>();
        try
        {
            parts.Add(driver.FindElement(By.TagName("body")).Text);
        }
        catch (NoSuchElementException)
        {
        }

        foreach (IWebElement host in driver.FindElements(By.CssSelector("document-upload")))
        {
            object? text = ((IJavaScriptExecutor)driver).ExecuteScript(
                "return arguments[0].shadowRoot ? (arguments[0].shadowRoot.textContent || '') : '';",
                host);
            if (text is not null)
            {
                string shadowText = Convert.ToString(text) ?? string.Empty;
                if (shadowText.Length > 0)
                    parts.Add(shadowText);
            }
        }

        return string.Join("\n", parts);
    }
}
