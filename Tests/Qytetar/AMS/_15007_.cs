using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.AMS;

[Category("AMS")]
[Category("15007")]
public class _15007_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "15007";
    protected override string? ServiceTitle => "PeshkimClodhesArgetues";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Aplikim për t’u pajisur me autorizim për peshkim çlodhës-argëtues";
    private const string ExpectedAddress =
        "FROSINA PLAKU; Nd. 88; H. 2; Ap. 9; NJËSIA ADMINISTRATIVE NR. 7; NJËSIA BASHKIAKE NR. 7; 1023; TIRANË, TIRANË, TIRANË";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";

    [Test]
    public void PeshkimClodhesArgetues()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 4 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert Step 1 Title");
        IWebElement Step1Title = WaitForStepTitle("MËNYRA E IDENTIFIKIMIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("MËNYRA E IDENTIFIKIMIT"));

        Log("Assert menyra e identifikimit");
        IWebElement ealbania = wait.Until(ExpectedConditions.ElementExists(By.Id("ealbania")));
        IWebElement nidRadio = wait.Until(ExpectedConditions.ElementExists(By.Id("nid")));
        Assert.That(ealbania.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(ealbania.GetAttribute("name"), Is.EqualTo("applicationType"));
        Assert.That(ealbania.GetAttribute("value"), Is.EqualTo("ealbania"));
        Assert.That(ealbania.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='ealbania']")).Text.Trim(),
            Is.EqualTo("e-Albania"));
        Assert.That(nidRadio.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(nidRadio.GetAttribute("name"), Is.EqualTo("applicationType"));
        Assert.That(nidRadio.GetAttribute("value"), Is.EqualTo("nid"));
        Assert.That(nidRadio.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='nid']")).Text.Trim(),
            Is.EqualTo("Passport/ID Card"));

        Log("Assert butonat e navigimit Step 1");
        AssertNavigationButtons("Vazhdo");
        IWebElement continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(continueBtn.GetAttribute("disabled"), Is.Not.Null);

        Log("Zgjidh llojin e identifikimit e-Albania");
        SafeClick(By.CssSelector("label[for='ealbania']"));
        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button.ealb-btn-continue")));

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("TË DHËNAT E APLIKANTIT");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E APLIKANTIT"));

        Log("Assert kohëzgjatja Step 2");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 4 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert te dhenat e aplikantit");
        AssertDisabledNamed("nid", Settings.Qytetar.Username);
        AssertDisabledNamed("name", "Katerina");
        AssertDisabledNamed("fatherName", "Foti");
        AssertDisabledNamed("surname", "Jançe");
        AssertDisabledNamed("birthplace", "Korçë");
        AssertDisabledNamed("nationality", "Shqipëri");
        AssertDisabledNamed("phone", "+355697008820");
        AssertDisabledNamed("email", "katerina.jance@kreatx.com");

        IWebElement birthday = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//label[contains(normalize-space(),'Datëlindja')]/following::input[contains(@class,'ealb-input')][1]")));
        Assert.That(birthday.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(birthday.GetAttribute("placeholder"), Is.EqualTo("dd.mm.yyyy"));
        Assert.That(birthday.GetAttribute("value").Trim(), Is.EqualTo("13.04.1993"));

        IWebElement genderMale = wait.Until(ExpectedConditions.ElementExists(By.Id("male")));
        IWebElement genderFemale = wait.Until(ExpectedConditions.ElementExists(By.Id("female")));
        Assert.That(genderMale.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(genderFemale.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(genderMale.GetAttribute("value"), Is.EqualTo("M"));
        Assert.That(genderFemale.GetAttribute("value"), Is.EqualTo("F"));
        Assert.That(genderMale.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(genderFemale.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(genderFemale.Selected, Is.True);
        Assert.That(genderMale.Selected, Is.False);

        IWebElement nipt = FindNamed("nipt");
        Assert.That(nipt.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(nipt.GetAttribute("disabled"), Is.Null);

        IWebElement companyName = FindNamed("companyName");
        Assert.That(companyName.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(companyName.GetAttribute("disabled"), Is.Null);

        IWebElement address = FindNamed("address");
        Assert.That(address.GetAttribute("value").Trim(), Is.EqualTo(ExpectedAddress));
        Assert.That(address.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(address.GetAttribute("readonly"), Is.Not.Null);

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("TË DHËNAT E APLIKIMIT");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E APLIKIMIT"));

        Log("Assert kohëzgjatja Step 3");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 4 hapa, tre te paret aktiv");
        AssertSteps(3);

        Log("Assert tipi i aplikimit");
        IWebElement applicationType = FindNamed("applicationType");
        var appTypeSelect = new SelectElement(applicationType);
        Assert.That(appTypeSelect.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(appTypeSelect.Options.Count, Is.EqualTo(4));
        Assert.That(appTypeSelect.Options[1].GetAttribute("value"), Is.EqualTo("commercial"));
        Assert.That(appTypeSelect.Options[1].Text.Trim(),
            Is.EqualTo("PESHKIMI TURISTIK I ANIJEVE TË PESHKIMIT ME FLAMUR SHQIPTAR"));
        Assert.That(appTypeSelect.Options[2].GetAttribute("value"), Is.EqualTo("sport"));
        Assert.That(appTypeSelect.Options[2].Text.Trim(), Is.EqualTo("PESHKIMI SPORTIV"));
        Assert.That(appTypeSelect.Options[3].GetAttribute("value"), Is.EqualTo("tourist"));
        Assert.That(appTypeSelect.Options[3].Text.Trim(),
            Is.EqualTo("PESHKIMI TURISTIK I MJETEVE LUNDRUESE KËNAQËSIE ME FLAMUR SHQIPTAR OSE TË HUAJ"));

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Assert.That(WaitForStepTitle("TË DHËNAT E APLIKIMIT")
            .Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E APLIKIMIT"));
        AssertFieldError("Përzgjidhni një vlerë për të vazhduar");

        Log("Zgjidh Tipin e Aplikimit");
        applicationType = FindNamed("applicationType");
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            applicationType);
        Thread.Sleep(300);
        new SelectElement(applicationType).SelectByValue("tourist");
        Thread.Sleep(1500);

        var vesselFields = driver.FindElements(By.CssSelector("[name='vesselName']"));
        if (vesselFields.Count > 0)
        {
            Log("Ploteso detajet e mjetit lundrues");
            FillInput(FindNamed("vesselName"), "Test");
            FillInput(FindNamed("vesselLength"), "1");
            FillInput(FindNamed("grossTonnage"), "1");
            FillInput(FindNamed("enginePower"), "1");
            FillInput(FindNamed("maxPassengers"), "1");
            FillInput(FindNamed("licenseNumber"), "Test");
            FillInput(FindNamed("registrationCertNumber"), "test");
            FillInput(FindNamed("activityZone"), "test");
            FillInput(FindNamed("fishingGear"), "test");
            FillInput(FindNamed("touristFishingActivity"), "test");
            DateTime start = DateTime.Today.AddDays(7);
            DateTime end = start.AddMonths(1);
            FillDateByLabel("Data e fillimit", start.ToString("dd.MM.yyyy"), start.Year, start.Month, start.Day);
            FillDateByLabel("Data e përfundimit", end.ToString("dd.MM.yyyy"), end.Year, end.Month, end.Day);
        }

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 4 Title");
        IWebElement Step4Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(), Does.StartWith("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 4");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 4 hapa, te gjithe aktiv");
        AssertSteps(4);

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h5[contains(.,'Dokumenta që ngarkohen nga Aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h5[contains(.,'Dokumenta që ngarkohen nga nëpunësit e administratës publike')]"))
            .Displayed, Is.True);
        Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//h6[contains(.,'nuk nevojitet të sigurohet dokumentacion nga nëpunësi')]")))
            .Displayed, Is.True);

        Log("Assert document-upload");
        AssertDocumentUpload("dokregjistriAnijesUpload",
            "Kopje të librit të anijes ose regjistrit detar");
        AssertDocumentUpload("dokCertifikataMjetitUpload",
            "Kopje të Certifikatës së Regjistrimit të mjetit lundrues");
        AssertDocumentUpload("dokDeshmieMjetitUpload",
            "Kopje të Dëshmisë së Aftësisë për drejtimin e mjetit lundrues");
        AssertDocumentUpload("dokCertLundrimiUpload",
            "Kopje të certifikatës së lundrimit të sigurt");
        AssertDocumentUpload("dokMandatPagesaUpload", "Paguaj");

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

        Log("Ngarko dokumentet e detyrueshme");
        UploadDocument("dokregjistriAnijesUpload", DocumentPath);
        UploadDocument("dokMandatPagesaUpload", DocumentPath);

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

    private void AssertDuration(string expected)
    {
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain(expected));
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
                "h5.px-4.my-2.text-uppercase, h5.px-4.my-4.text-uppercase, h4.required-name.text-uppercase, h4.text-uppercase, h5.text-uppercase"));
            foreach (var title in titles)
            {
                string actual = title.Text.Trim().ToUpperInvariant();
                if (actual == expectedUpper || actual.StartsWith(expectedUpper))
                    return title;
            }
            return null;
        });
    }

    private IWebElement FindNamed(string name)
    {
        return wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector($"[name='{name}']")));
    }

    private void FillDateByLabel(string labelText, string displayDate, int year, int month, int day)
    {
        IWebElement input = wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//label[contains(normalize-space(),'{labelText}')]/following-sibling::div//input[contains(@class,'ealb-input')]")));

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
                string current = FindDateInput(labelText).GetAttribute("value") ?? string.Empty;
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

    private IWebElement FindDateInput(string labelText)
    {
        return driver.FindElement(
            By.XPath($"//label[contains(normalize-space(),'{labelText}')]/following-sibling::div//input[contains(@class,'ealb-input')]"));
    }

    private void AssertDisabledNamed(string name, string expectedValue)
    {
        IWebElement field = FindNamed(name);
        Assert.That(field.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(field.GetAttribute("disabled"), Is.Not.Null);
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {
        Assert.That(driver.FindElement(
            By.XPath($"//span[contains(@class,'form-label') and contains(normalize-space(),'{documentTitle}')]"))
            .Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-15007"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("5"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("52428800"));
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
