using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.AMS;

[Category("AMS")]
[Category("9635")]
public class _9635_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "9635";
    protected override string? ServiceTitle => "KerkesePerLenieShtetesie";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName = "Kërkesë për lënie shtetësie";
    private const string ApplicationTypeOver18 =
        "Kërkesë për heqjen dorë nga shtetësia shqiptare për personat mbi 18 vjeç";
    private const string ApplicationType14To18 =
        "Kërkesë për heqjen dorë nga shtetësia shqiptare për personat nga 14 - 18 vjeç";
    private const string ApplicationTypeUnder14 =
        "Kërkesë për heqjen dorë nga shtetësia shqiptare për personat nën 14 vjeç";
    private const string AlbaniaOption = "SHQIPËRI (AL) - AL";
    private const string ExpectedAddress =
        "FROSINA PLAKU; Nd. 88; H. 2; Ap. 9; NJËSIA ADMINISTRATIVE NR. 7; NJËSIA BASHKIAKE NR. 7; 1023; TIRANË";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";

    [Test]
    public void KerkesePerLenieShtetesie()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("5 minuta kohëzgjatje"));

        Log("Assert 4 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert Step 1 Title");
        IWebElement Step1Title = WaitForStepTitle("TIPI I APLIKIMIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TIPI I APLIKIMIT"));

        Log("Assert fusha Tipi i Aplikimit");
        IWebElement tipiLabel = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//label[contains(.,'Tipi i Aplikimit')]")));
        Assert.That(tipiLabel.Text, Does.Contain("Tipi i Aplikimit"));
        Assert.That(tipiLabel.Text, Does.Contain("*"));
        Assert.That(tipiLabel.GetAttribute("class"), Does.Contain("required-name"));

        IWebElement tipiSelect = FindSelectByLabel("Tipi i Aplikimit");
        var tipi = new SelectElement(tipiSelect);
        Assert.That(tipi.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(tipi.Options.Count, Is.EqualTo(4));
        Assert.That(tipi.Options[1].GetAttribute("value"), Is.EqualTo(ApplicationTypeOver18));
        Assert.That(tipi.Options[1].Text.Trim(), Is.EqualTo(ApplicationTypeOver18));
        Assert.That(tipi.Options[2].GetAttribute("value"), Is.EqualTo(ApplicationType14To18));
        Assert.That(tipi.Options[2].Text.Trim(), Is.EqualTo(ApplicationType14To18));
        Assert.That(tipi.Options[3].GetAttribute("value"), Is.EqualTo(ApplicationTypeUnder14));
        Assert.That(tipi.Options[3].Text.Trim(), Is.EqualTo(ApplicationTypeUnder14));

        Log("Assert butonat e navigimit Step 1");
        AssertNavigationButtons("Vazhdo");

        Log("Zgjidh Tipi i Aplikimit");
        SelectDropdownByValue(tipiSelect, ApplicationTypeOver18);

        Log("Assert fusha Aplikuesi");
        IWebElement aplikuesiLabel = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//label[contains(.,'Aplikuesi')]")));
        Assert.That(aplikuesiLabel.Text, Does.Contain("Aplikuesi"));
        Assert.That(aplikuesiLabel.Text, Does.Contain("*"));
        Assert.That(aplikuesiLabel.GetAttribute("class"), Does.Contain("required-name"));

        IWebElement aplikuesiSelect = FindSelectByLabel("Aplikuesi");
        var aplikuesi = new SelectElement(aplikuesiSelect);
        Assert.That(aplikuesi.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(aplikuesi.Options.Count, Is.EqualTo(3));
        Assert.That(aplikuesi.Options[1].GetAttribute("value"), Is.EqualTo("Vetë personi"));
        Assert.That(aplikuesi.Options[1].Text.Trim(), Is.EqualTo("Vetë personi"));
        Assert.That(aplikuesi.Options[2].GetAttribute("value"), Is.EqualTo("Autorizuar me prokurë"));
        Assert.That(aplikuesi.Options[2].Text.Trim(), Is.EqualTo("Autorizuar me prokurë"));

        Log("Kliko Vazhdo pa zgjedhur Aplikuesi");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error message per Aplikuesi");
        aplikuesiSelect = FindSelectByLabel("Aplikuesi");
        Assert.That(aplikuesiSelect.GetAttribute("class"), Does.Contain("is-invalid"));
        IWebElement aplikuesiError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//label[contains(.,'Aplikuesi')]/following-sibling::div[contains(@class,'error-message')]")));
        Assert.That(aplikuesiError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));
        Assert.That(WaitForStepTitle("TIPI I APLIKIMIT").Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TIPI I APLIKIMIT"));

        Log("Zgjidh Aplikuesi Vetë personi");
        SelectDropdownByValue(FindSelectByLabel("Aplikuesi"), "Vetë personi");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("TË DHËNAT E APLIKANTIT");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNAT E APLIKANTIT"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("5 minuta kohëzgjatje"));

        Log("Assert 4 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert te dhenat e aplikantit te para-plotesuara");
        AssertReadonlyField("Nid", Settings.Qytetar.Username);
        AssertReadonlyField("Emri", "Katerina");
        AssertReadonlyField("Mbiemri", "Jançe");
        AssertReadonlyField("Atësia", "Foti");
        AssertReadonlyField("Amësia", "Manushaqe");
        AssertReadonlyField("Datëlindja", "13.04.1993");
        AssertReadonlyField("Gjinia", "Femër");

        IWebElement vendiLindjes = FindNamed("vendlindja");
        Assert.That(vendiLindjes.GetAttribute("value").Trim(), Is.EqualTo("Korçë"));
        Assert.That(vendiLindjes.GetAttribute("readonly"), Is.Null);

        IWebElement shtetiLindjes = FindNamed("vendlindjaShteti");
        Assert.That(new SelectElement(shtetiLindjes).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("vendlindjaShteti").Text, Does.Contain("*"));

        IWebElement shtetesia = FindNamed("shtetesia");
        Assert.That(new SelectElement(shtetesia).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("shtetesia").Text, Does.Contain("*"));

        IWebElement gjuhaMeme = FindNamed("gjuhaMeme");
        Assert.That(gjuhaMeme.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement gjendjaCivile = FindNamed("gjendjaCivile");
        var gjendja = new SelectElement(gjendjaCivile);
        Assert.That(gjendja.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(gjendja.Options[1].GetAttribute("value"), Is.EqualTo("Beqar"));
        Assert.That(gjendja.Options[2].GetAttribute("value"), Is.EqualTo("Martuar"));
        Assert.That(gjendja.Options[3].GetAttribute("value"), Is.EqualTo("Divorcuar"));
        Assert.That(gjendja.Options[4].GetAttribute("value"), Is.EqualTo("I/e ve"));
        Assert.That(gjendja.Options[5].GetAttribute("value"), Is.EqualTo("Partner civil"));
        Assert.That(FindLabelFor("gjendjaCivile").Text, Does.Contain("*"));

        IWebElement profesioni = FindNamed("profesioni");
        Assert.That(profesioni.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Assert.That(WaitForStepTitle("TË DHËNAT E APLIKANTIT").Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNAT E APLIKANTIT"));

        Log("Ploteso fushat e detyrueshme");
        SelectDropdownByValue(FindNamed("vendlindjaShteti"), AlbaniaOption);
        SelectDropdownByValue(FindNamed("shtetesia"), AlbaniaOption);
        FillInput(FindNamed("gjuhaMeme"), "Shqip");
        SelectDropdownByValue(FindNamed("gjendjaCivile"), "Martuar");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("INFORMACIONI I KONTAKTIT TË APLIKANTIT");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("INFORMACIONI I KONTAKTIT TË APLIKANTIT"));

        Log("Assert kohëzgjatja Step 3");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("5 minuta kohëzgjatje"));

        Log("Assert 4 hapa, tre te paret aktiv");
        AssertSteps(3);

        Log("Assert te dhenat e kontaktit");
        IWebElement qytetFshat = FindNamed("qytetFshat");
        Assert.That(qytetFshat.GetAttribute("value").Trim(), Is.EqualTo("TIRANË, TIRANË"));
        Assert.That(qytetFshat.GetAttribute("readonly"), Is.Not.Null);

        IWebElement email = FindNamed("email");
        Assert.That(email.GetAttribute("type"), Is.EqualTo("email"));
        Assert.That(email.GetAttribute("value").Trim(), Is.EqualTo("katerina.jance@kreatx.com"));
        Assert.That(email.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(FindLabelFor("email").Text, Does.Contain("*"));

        IWebElement telefonFiks = FindNamed("telefonFiks");
        Assert.That(telefonFiks.GetAttribute("type"), Is.EqualTo("tel"));
        Assert.That(telefonFiks.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(telefonFiks.GetAttribute("readonly"), Is.Null);

        IWebElement telefonCelular = FindNamed("telefonCelular");
        Assert.That(telefonCelular.GetAttribute("type"), Is.EqualTo("tel"));
        Assert.That(telefonCelular.GetAttribute("value").Trim(), Is.EqualTo("+355697008820"));
        Assert.That(telefonCelular.GetAttribute("readonly"), Is.Not.Null);

        IWebElement bashkia = FindNamed("bashkia");
        var bashkiaSelect = new SelectElement(bashkia);
        Assert.That(bashkiaSelect.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(bashkiaSelect.Options.Any(o => o.GetAttribute("value") == "Tiranë"), Is.True);
        Assert.That(FindLabelFor("bashkia").Text, Does.Contain("*"));

        IWebElement njesiAdministrative = FindNamed("njesiAdministrative");
        Assert.That(new SelectElement(njesiAdministrative).Options.Count, Is.EqualTo(1));
        Assert.That(FindLabelFor("njesiAdministrative").Text, Does.Contain("*"));

        IWebElement komisariati = FindNamed("komisariati");
        Assert.That(new SelectElement(komisariati).Options.Count, Is.EqualTo(1));
        Assert.That(FindLabelFor("komisariati").Text, Does.Contain("*"));

        IWebElement rruga = FindNamed("rruga");
        Assert.That(rruga.GetAttribute("value").Trim(), Is.EqualTo(ExpectedAddress));
        Assert.That(rruga.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(rruga.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindLabelFor("rruga").Text, Does.Contain("*"));

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa zgjedhur Bashkine dhe Komisariatin");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Assert.That(WaitForStepTitle("INFORMACIONI I KONTAKTIT TË APLIKANTIT")
            .Text.Trim().ToUpperInvariant(),
            Does.StartWith("INFORMACIONI I KONTAKTIT TË APLIKANTIT"));

        Log("Zgjidh Bashkia Tiranë");
        SelectDropdownByValue(FindNamed("bashkia"), "Tiranë");

        Log("Wait qe Njësia administrative te mbushen opsionet");
        wait.Until(d =>
        {
            try
            {
                return new SelectElement(d.FindElement(By.CssSelector("select[name='njesiAdministrative']")))
                    .Options.Count > 1;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        Log("Zgjidh Njësia administrative");
        SelectFirstAvailableOption(FindNamed("njesiAdministrative"));

        Log("Wait qe Komisariati te mbushen opsionet");
        wait.Until(d =>
        {
            try
            {
                return new SelectElement(d.FindElement(By.CssSelector("select[name='komisariati']")))
                    .Options.Count > 1;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        Log("Zgjidh Komisariati");
        SelectFirstAvailableOption(FindNamed("komisariati"));

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 4 Title");
        IWebElement Step4Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(), Does.StartWith("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 4");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("5 minuta kohëzgjatje"));

        Log("Assert 4 hapa, te gjithe aktiv");
        AssertSteps(4);

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h5[contains(.,'Dokumenta që ngarkohen nga Aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h5[contains(.,'Dokumenta që ngarkohen nga nëpunësit e administratës publike')]"))
            .Displayed, Is.True);

        Log("Assert document-upload");
        AssertDocumentUpload("doc_0Upload", "Kopje të dokumentit të identifikimit");
        AssertDocumentUpload("doc_1Upload",
            "Kërkesë e shtetasit të huaj drejtuar Presidentit të Republikës");
        AssertDocumentUpload("doc_2Upload",
            "Premtimi nga autoriteti kompetent i vendit të shtetësisë tjetër, ose dokumenti në origjinal ku konfirmohet shtetësia tjetër");
        AssertDocumentUpload("doc_3Upload",
            "Dokumenti që vërteton rezidencën, vendbanimin e tij në shtetin nga i cili është premtuar shtetësia tjetër");
        AssertDocumentUpload("doc_4Upload", "Vërtetim i lëshuar nga Gjykata e rrethit");
        AssertDocumentUpload("doc_5Upload", "Vërtetim i lëshuar nga Prokuroria e rrethit");
        AssertDocumentUpload("doc_6Upload", "Fotografi e aplikantit");

        Log("Assert dokumentet e administrates");
        Assert.That(driver.FindElement(By.XPath(
            "//p[contains(.,'Certifikata elektronike e gjendjes gjyqësore e Rupublikës së Shqipërisë')]"))
            .Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//p[contains(.,'Certifikata e lindjes')]"))
            .Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//p[contains(.,'Certifikata familjare')]"))
            .Displayed, Is.True);

        Log("Assert checkboxet e autorizimit");
        IWebElement authorizeEmployee = wait.Until(ExpectedConditions.ElementExists(
            By.Id("authorizeEmployee")));
        IWebElement agreeToDocCollection = wait.Until(ExpectedConditions.ElementExists(
            By.Id("agreeToDocCollection")));
        Assert.That(authorizeEmployee.GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(agreeToDocCollection.GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(authorizeEmployee.Selected, Is.False);
        Assert.That(agreeToDocCollection.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='authorizeEmployee']")).Text.Trim(),
            Does.Contain("Autorizoj nëpunësin e administratës"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='agreeToDocCollection']")).Text.Trim(),
            Does.Contain("Mbledhja e dokumentacionit shoqërues"));

        Log("Assert butonat e navigimit Step 4");
        AssertNavigationButtons("Dërgo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Kliko Dergo pa ngarkuar dokumentin");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(1000);
        Assert.That(WaitForStepTitle("DOKUMENTACIONI").Text.Trim().ToUpperInvariant(),
            Does.StartWith("DOKUMENTACIONI"));

        Log("Ngarko dokumentet");
        UploadDocument("doc_0Upload", DocumentPath);
        UploadDocument("doc_1Upload", DocumentPath);
        UploadDocument("doc_2Upload", DocumentPath);
        UploadDocument("doc_3Upload", DocumentPath);
        UploadDocument("doc_4Upload", DocumentPath);
        UploadDocument("doc_5Upload", DocumentPath);
        UploadDocument("doc_6Upload", DocumentPath);

        Log("Kliko checkboxet e autorizimit");
        SafeClick(By.CssSelector("label[for='authorizeEmployee']"));
        SafeClick(By.CssSelector("label[for='agreeToDocCollection']"));
        Thread.Sleep(500);

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

    private IWebElement FindSelectByLabel(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//form//label[contains(.,'{labelPart}')]/following-sibling::select")));
    }

    private IWebElement FindNamed(string name)
    {
        return wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector($"[name='{name}']")));
    }

    private IWebElement FindLabelFor(string name)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//*[@name='{name}']/preceding-sibling::label")));
    }

    private void AssertReadonlyField(string labelText, string expectedValue)
    {
        IWebElement field = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//form//label[normalize-space()='{labelText}']/following-sibling::input")));
        Assert.That(field.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(field.GetAttribute("readonly"), Is.Not.Null);
    }

    private void SelectDropdownByValue(IWebElement select, string value)
    {
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            select);
        Thread.Sleep(300);
        new SelectElement(select).SelectByValue(value);
        Thread.Sleep(500);
    }

    private void SelectFirstAvailableOption(IWebElement select)
    {
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            select);
        Thread.Sleep(300);

        var dropdown = new SelectElement(select);
        var option = dropdown.Options.FirstOrDefault(o =>
            !string.IsNullOrWhiteSpace(o.GetAttribute("value")));
        Assert.That(option, Is.Not.Null, "Komisariati nuk ka opsione te disponueshme");
        dropdown.SelectByValue(option!.GetAttribute("value"));
        Thread.Sleep(500);
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {
        Assert.That(driver.FindElement(
            By.XPath($"//label[contains(normalize-space(),'{documentTitle}')]")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-9635"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("5"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("50"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf,.jpg,.jpeg,.png"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të ngarkuar dokument"));

        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të ngarkuar dokument"));
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
