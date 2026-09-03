using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.AMS;

[Category("AMS")]
[Category("9636")]
public class _9636_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "9636";
    protected override string? ServiceTitle => "KerkesePeRifitimShtetesie";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName = "Kërkesë për rifitim shtetësie";
    private const string ApplicationTypeNoOtherCitizenship =
        "Ish shtetasi shqiptar, që nuk ka fituar shtetësi tjetër";
    private const string ApplicationTypeHasOtherCitizenship =
        "Ish shtetasi shqiptar, që ka fituar shtetësi tjetër";
    private const string AndorraOption = "ANDORRA (AD) - AN";
    private const string UaeOption = "EMIRATET E BASHKUARA ARABE (AE) - TC";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";

    private static readonly string[] ApplicantDocuments =
    {
        "Kopje e dokumentit të identifikimit",
        "Kërkesa e shtetasit të huaj drejtuar Presidentit të Republikës",
        "Deklarate noteriale",
        "Vërtetimi i marrë nga njësia administrative ku është banor",
        "Certifikatë lindje ose vdekje e prindërve të aplikantit",
        "Fotografi e aplikantit"
    };

    [Test]
    public void KerkesePeRifitimShtetesie()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 3 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert Step 1 Title");
        IWebElement Step1Title = WaitForStepTitle("TË DHËNAT E APLIKANTIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNAT E APLIKANTIT"));

        Log("Assert fushat e hapit 1");
        AssertEmptyRequired("nrPasaportes", "Numri i pasaportës");
        AssertEmptyRequired("nrDokUdhetimit", "Nr. i Dok të udhëtimit");
        AssertEmptyRequired("emri", "Emri");
        AssertEmptyRequired("mbiemri", "Mbiemri");
        AssertEmptyRequired("atesia", "Atësia");
        AssertEmptyRequired("amesia", "Amësia");

        IWebElement gjinia = FindNamed("gjinia");
        var gjiniaSelect = new SelectElement(gjinia);
        Assert.That(gjiniaSelect.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("gjinia").Text, Does.Contain("*"));
        Assert.That(gjiniaSelect.Options.Any(o => o.GetAttribute("value") == "Mashkull"), Is.True);
        Assert.That(gjiniaSelect.Options.Any(o => o.GetAttribute("value") == "Femer"), Is.True);

        IWebElement datelindja = FindDateOfBirthInput();
        Assert.That(datelindja.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(datelindja.GetAttribute("placeholder"), Is.EqualTo("dd.mm.yyyy"));

        IWebElement gjendjaCivile = FindNamed("gjendjaCivile");
        var gjendja = new SelectElement(gjendjaCivile);
        Assert.That(gjendja.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("gjendjaCivile").Text, Does.Contain("*"));
        Assert.That(gjendja.Options[1].GetAttribute("value"), Is.EqualTo("Martuar"));
        Assert.That(gjendja.Options[2].GetAttribute("value"), Is.EqualTo("Beqar"));
        Assert.That(gjendja.Options[3].GetAttribute("value"), Is.EqualTo("Divorcuar"));
        Assert.That(gjendja.Options[4].GetAttribute("value"), Is.EqualTo("I/e ve"));
        Assert.That(gjendja.Options[5].GetAttribute("value"), Is.EqualTo("Partner civil"));

        IWebElement shtetiLindjes = FindNamed("shtetiLindjes");
        Assert.That(new SelectElement(shtetiLindjes).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("shtetiLindjes").Text, Does.Contain("*"));
        Assert.That(new SelectElement(shtetiLindjes).Options.Any(o =>
            o.GetAttribute("value") == "SHQIPËRI (AL) - AL"), Is.True);

        IWebElement shtetesiaAktuale = FindNamed("shtetesiaAktuale");
        Assert.That(new SelectElement(shtetesiaAktuale).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("shtetesiaAktuale").Text, Does.Contain("*"));

        Assert.That(FindNamed("vendlindja").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindNamed("gjuhaMeme").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindNamed("profesioni").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 1");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Assert.That(WaitForStepTitle("TË DHËNAT E APLIKANTIT").Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNAT E APLIKANTIT"));
        Assert.That(FindNamed("nrPasaportes").GetAttribute("class"), Does.Contain("is-invalid"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme");
        FillInput(FindNamed("nrPasaportes"), "test");
        FillInput(FindNamed("nrDokUdhetimit"), "test");
        FillInput(FindNamed("emri"), "Katerina");
        FillInput(FindNamed("mbiemri"), "Jançe");
        FillInput(FindNamed("atesia"), "Foti");
        FillInput(FindNamed("amesia"), "Manushaqe");
        SelectDropdownByValue(FindNamed("gjinia"), "Femer");
        FillDateOfBirth("13.04.1993");
        SelectDropdownByValue(FindNamed("gjendjaCivile"), "Beqar");
        SelectDropdownByValue(FindNamed("shtetiLindjes"), AndorraOption);
        SelectDropdownByValue(FindNamed("shtetesiaAktuale"), UaeOption);
        FillInput(FindNamed("vendlindja"), "Korçë");
        FillInput(FindNamed("gjuhaMeme"), "Shqip");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("INFORMACIONI I KONTAKTIT TË APLIKANTIT");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("INFORMACIONI I KONTAKTIT TË APLIKANTIT"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 3 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert te dhenat e kontaktit");
        IWebElement email = FindNamed("email");
        Assert.That(email.GetAttribute("type"), Is.EqualTo("email"));
        Assert.That(email.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("email").Text, Does.Contain("*"));

        IWebElement telCel = FindNamed("telCel");
        Assert.That(telCel.GetAttribute("type"), Is.EqualTo("tel"));
        Assert.That(telCel.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("telCel").Text, Does.Contain("*"));

        IWebElement telFiks = FindNamed("telFiks");
        Assert.That(telFiks.GetAttribute("type"), Is.EqualTo("tel"));
        Assert.That(telFiks.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement fshatiQyteti = FindNamed("fshatiQyteti");
        Assert.That(fshatiQyteti.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("fshatiQyteti").Text, Does.Contain("*"));

        IWebElement bashkia = FindNamed("bashkia");
        var bashkiaSelect = new SelectElement(bashkia);
        Assert.That(bashkiaSelect.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(bashkiaSelect.Options.Any(o => o.GetAttribute("value") == "Tiranë"), Is.True);
        Assert.That(FindLabelFor("bashkia").Text, Does.Contain("*"));

        IWebElement njesiaAdministrative = FindNamed("njesiaAdministrative");
        Assert.That(njesiaAdministrative.GetAttribute("disabled"), Is.Not.Null);

        IWebElement komisariati = FindNamed("komisariati");
        Assert.That(new SelectElement(komisariati).Options.Count, Is.EqualTo(1));
        Assert.That(FindLabelFor("komisariati").Text, Does.Contain("*"));

        IWebElement adresa = FindNamed("adresa");
        Assert.That(adresa.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("adresa").Text, Does.Contain("*"));

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Assert.That(WaitForStepTitle("INFORMACIONI I KONTAKTIT TË APLIKANTIT")
            .Text.Trim().ToUpperInvariant(),
            Does.StartWith("INFORMACIONI I KONTAKTIT TË APLIKANTIT"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme");
        FillInput(FindNamed("email"), "katerina.jance@kreatx.com");
        FillInput(FindNamed("telCel"), "0697008820");
        FillInput(FindNamed("fshatiQyteti"), "Tiranë");
        FillInput(FindNamed("adresa"), "test");

        Log("Zgjidh Bashkia Tiranë");
        SelectDropdownByValue(FindNamed("bashkia"), "Tiranë");

        Log("Wait qe Njësia administrative / Komisariati te mbushen");
        wait.Until(d =>
        {
            try
            {
                var njesia = d.FindElement(By.CssSelector("select[name='njesiaAdministrative']"));
                var kom = d.FindElement(By.CssSelector("select[name='komisariati']"));
                bool njesiaReady = njesia.Enabled && new SelectElement(njesia).Options.Count > 1;
                bool komReady = new SelectElement(kom).Options.Count > 1;
                return njesiaReady || komReady;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        njesiaAdministrative = FindNamed("njesiaAdministrative");
        if (njesiaAdministrative.Enabled && new SelectElement(njesiaAdministrative).Options.Count > 1)
        {
            Log("Zgjidh Njësia administrative");
            SelectFirstAvailableOption(njesiaAdministrative);
        }

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

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(), Does.StartWith("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 3");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 3 hapa, te gjithe aktiv");
        AssertSteps(3);

        Log("Assert fusha Tipi i aplikimit");
        IWebElement tipiLabel = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//label[contains(.,'Tipi i aplikimit')]")));
        Assert.That(tipiLabel.Text, Does.Contain("Tipi i aplikimit"));
        Assert.That(tipiLabel.Text, Does.Contain("*"));
        Assert.That(tipiLabel.GetAttribute("class"), Does.Contain("required-name"));

        IWebElement tipiSelect = FindSelectByLabel("Tipi i aplikimit");
        var tipi = new SelectElement(tipiSelect);
        Assert.That(tipi.SelectedOption.GetAttribute("value"), Is.EqualTo("0"));
        Assert.That(tipi.Options.Count, Is.EqualTo(3));
        Assert.That(tipi.Options[1].GetAttribute("value"), Is.EqualTo("1"));
        Assert.That(tipi.Options[1].Text.Trim(), Is.EqualTo(ApplicationTypeNoOtherCitizenship));
        Assert.That(tipi.Options[2].GetAttribute("value"), Is.EqualTo("2"));
        Assert.That(tipi.Options[2].Text.Trim(), Is.EqualTo(ApplicationTypeHasOtherCitizenship));

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Dërgo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Kliko Dergo pa zgjedhur tipin e aplikimit");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(1000);
        tipiSelect = FindSelectByLabel("Tipi i aplikimit");
        Assert.That(tipiSelect.GetAttribute("class"), Does.Contain("is-invalid"));
        AssertFieldError("Përzgjidhni një vlerë për të vazhduar");
        Assert.That(WaitForStepTitle("DOKUMENTACIONI").Text.Trim().ToUpperInvariant(),
            Does.StartWith("DOKUMENTACIONI"));

        Log("Zgjidh Tipi i aplikimit");
        SelectDropdownByValue(FindSelectByLabel("Tipi i aplikimit"), "1");

        Log("Prit dokumentet pas zgjedhjes se tipit");
        wait.Until(d => d.FindElements(By.CssSelector("[id$='Upload']")).Count > 0);
        Thread.Sleep(1000);

        Log("Assert document-upload");
        foreach (string documentTitle in ApplicantDocuments)
            AssertDocumentUpload(documentTitle);

        Log("Kliko Dergo pa ngarkuar dokumentin");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(1000);
        Assert.That(WaitForStepTitle("DOKUMENTACIONI").Text.Trim().ToUpperInvariant(),
            Does.StartWith("DOKUMENTACIONI"));

        Log("Ngarko dokumentet");
        foreach (string documentTitle in ApplicantDocuments)
            UploadDocument(documentTitle, DocumentPath);

        Log("Kliko checkboxet e autorizimit nese shfaqen");
        ClickAuthorizationCheckboxesIfPresent();

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
            By.XPath($"//label[contains(.,'{labelPart}')]/following-sibling::select")));
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

    private void AssertEmptyRequired(string name, string labelPart)
    {
        IWebElement field = FindNamed(name);
        Assert.That(field.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        IWebElement label = FindLabelFor(name);
        Assert.That(label.Text, Does.Contain(labelPart));
        Assert.That(label.Text, Does.Contain("*"));
        Assert.That(label.GetAttribute("class"), Does.Contain("required-name"));
    }

    private IWebElement FindDateOfBirthInput()
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//label[contains(.,'Datëlindja')]/following::input[@placeholder='dd.mm.yyyy'][1]")));
    }

    private void FillDateOfBirth(string displayDate)
    {
        string[] parts = displayDate.Split('.');
        int day = int.Parse(parts[0]);
        int month = int.Parse(parts[1]);
        int year = int.Parse(parts[2]);

        IWebElement input = FindDateOfBirthInput();
        ((IJavaScriptExecutor)driver).ExecuteScript(@"
            const el = arguments[0];
            const display = arguments[1];
            const year = Number(arguments[2]);
            const month = Number(arguments[3]);
            const day = Number(arguments[4]);
            const date = new Date(year, month - 1, day);
            el.scrollIntoView({block:'center'});
            const wrap = el.closest('.flatpickr-wrapper') || el.parentElement;
            const inputs = [el, ...wrap.querySelectorAll('input')];
            const fpInput = inputs.find(i => i._flatpickr);
            if (fpInput && fpInput._flatpickr) {
                fpInput._flatpickr.setDate(date, true);
                fpInput._flatpickr.close();
            } else {
                const setter = Object.getOwnPropertyDescriptor(window.HTMLInputElement.prototype, 'value').set;
                setter.call(el, display);
                el.dispatchEvent(new Event('input', { bubbles: true }));
                el.dispatchEvent(new Event('change', { bubbles: true }));
            }
        ", input, displayDate, year, month, day);

        wait.Until(d =>
        {
            try
            {
                string current = FindDateOfBirthInput().GetAttribute("value") ?? string.Empty;
                return current.Length > 0 &&
                       (current.Contains(displayDate) || current.Contains(year.ToString()));
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        ((IJavaScriptExecutor)driver).ExecuteScript(@"
            document.querySelectorAll('.flatpickr-calendar.open').forEach(el => {
                el.classList.remove('open');
                el.style.display = 'none';
            });
            if (document.activeElement) document.activeElement.blur();
        ");
        Thread.Sleep(300);
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
        Assert.That(option, Is.Not.Null, "Dropdown nuk ka opsione te disponueshme");
        dropdown.SelectByValue(option!.GetAttribute("value"));
        Thread.Sleep(500);
    }

    private IWebElement FindUploadByTitle(string documentTitle)
    {
        return wait.Until(d =>
        {
            var custom = d.FindElements(By.XPath(
                $"//label[contains(normalize-space(),'{documentTitle}')]/following::document-upload[1]"));
            if (custom.Count > 0)
                return custom[0];

            var byId = d.FindElements(By.XPath(
                $"//label[contains(normalize-space(),'{documentTitle}')]/following::*[contains(@id,'Upload')][1]"));
            return byId.Count > 0 ? byId[0] : null;
        });
    }

    private void AssertDocumentUpload(string documentTitle)
    {
        Assert.That(driver.FindElement(
            By.XPath($"//label[contains(normalize-space(),'{documentTitle}')]")).Displayed, Is.True);

        IWebElement docUpload = FindUploadByTitle(documentTitle);
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-9636"));
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

    private void UploadDocument(string documentTitle, string filePath)
    {
        Assert.That(File.Exists(filePath), Is.True, "File nuk ekziston: " + filePath);

        IWebElement docUpload = FindUploadByTitle(documentTitle);
        string uploadId = docUpload.GetAttribute("id");
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

    private void ClickAuthorizationCheckboxesIfPresent()
    {
        var authorize = driver.FindElements(By.CssSelector("label[for='authorizeEmployee']"));
        if (authorize.Count > 0 && authorize[0].Displayed)
            SafeClick(By.CssSelector("label[for='authorizeEmployee']"));

        var agree = driver.FindElements(By.CssSelector("label[for='agreeToDocCollection']"));
        if (agree.Count > 0 && agree[0].Displayed)
            SafeClick(By.CssSelector("label[for='agreeToDocCollection']"));

        Thread.Sleep(500);
    }
}
