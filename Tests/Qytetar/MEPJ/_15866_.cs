using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.MEPJ;

[Category("MEPJ")]
[Category("15866")]
public class _15866_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "15866";
    protected override string? ServiceTitle => "NdihmeEPergjithshmeKonsullore";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName = "Ndihmë e përgjithshme konsullore";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";
    private const string IdUploadId = "identificationDocUpload";
    private const string LostCitizenUploadId = "lostCitizenDocUpload";
    private const string AdditionalUploadId = "additionalDocUpload";

    [Test]
    public void NdihmeEPergjithshmeKonsullore()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert Title Step 1");
        IWebElement Step1Title = WaitForStepTitle("TË DHËNA PERSONALE TË APLIKANTIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNA PERSONALE TË APLIKANTIT"));

        Log("Assert kohëzgjatja");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 3 hapa, hapi i pare aktiv");
        AssertSteps(1, 3);

        Log("Assert fushat e hapit 1");
        IWebElement applicationType = FindNamed("applicationType");
        Assert.That(new SelectElement(applicationType).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("applicationType").Text, Does.Contain("*"));

        Assert.That(FindNamed("nid").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("nid").Text, Does.Contain("*"));
        Assert.That(FindNamed("name").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("name").Text, Does.Contain("*"));
        Assert.That(FindNamed("surname").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("surname").Text, Does.Contain("*"));

        IWebElement gender = FindNamed("gender");
        var genderSelect = new SelectElement(gender);
        Assert.That(genderSelect.Options.Count, Is.EqualTo(2));
        Assert.That(genderSelect.Options[0].GetAttribute("value"), Is.EqualTo("1"));
        Assert.That(genderSelect.Options[0].Text.Trim(), Is.EqualTo("Mashkull"));
        Assert.That(genderSelect.Options[1].GetAttribute("value"), Is.EqualTo("0"));
        Assert.That(genderSelect.Options[1].Text.Trim(), Is.EqualTo("Femër"));
        Assert.That(genderSelect.SelectedOption.GetAttribute("value"), Is.EqualTo("1"));
        Assert.That(FindLabelFor("gender").Text, Does.Contain("*"));

        Assert.That(FindDateInput("birthdate").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("birthdate").Text, Does.Contain("*"));

        IWebElement email = FindNamed("email");
        Assert.That(email.GetAttribute("type"), Is.EqualTo("email"));
        Assert.That(email.GetAttribute("value").Trim(), Is.EqualTo("katerina.jance@kreatx.com"));
        Assert.That(FindLabelFor("email").Text, Does.Contain("*"));

        Assert.That(FindNamed("birthCity").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("birthCity").Text, Does.Contain("*"));
        Assert.That(new SelectElement(FindNamed("birthCountry")).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("birthCountry").Text, Does.Contain("*"));

        IWebElement citizenship = FindNamed("citizenship");
        Assert.That(citizenship.GetAttribute("value").Trim(), Is.EqualTo("Shqiptare"));
        Assert.That(FindLabelFor("citizenship").Text, Does.Contain("*"));

        Assert.That(FindNamed("secondCitizenship").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("secondCitizenship").Text, Does.Not.Contain("*"));

        Assert.That(FindNamed("phoneNumber").GetAttribute("value").Trim(), Is.EqualTo("+355697008820"));
        Assert.That(FindLabelFor("phoneNumber").Text, Does.Contain("*"));

        Assert.That(new SelectElement(FindNamed("country")).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("country").Text, Does.Contain("*"));
        Assert.That(new SelectElement(FindNamed("consularOffice")).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("consularOffice").Text, Does.Contain("*"));

        Log("Assert butonat e navigimit Step 1");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 1");
        SelectByValue(FindNamed("applicationType"), "2");
        FillInput(FindNamed("nid"), "test");
        FillInput(FindNamed("name"), "test");
        FillInput(FindNamed("surname"), "test");
        SelectByValue(FindNamed("gender"), "0");
        FillDate("birthdate", "01.01.1990", 1990, 1, 1);
        FillInput(FindNamed("birthCity"), "test");
        SelectByValue(FindNamed("birthCountry"), "1");
        SelectByValue(FindNamed("country"), "2");
        SelectFirstWhenEnabled(By.Name("consularOffice"));

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("TË DHËNAT E PERSONIT QË KAM HUMBUR KONTAKT");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNAT E PERSONIT QË KAM HUMBUR KONTAKT"));

        Log("Assert kohëzgjatja Step 2");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 4 hapa, dy te paret aktiv");
        AssertSteps(2, 4);

        Log("Assert fushat e hapit 2");
        Assert.That(FindNamed("documentNumber").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("documentNumber").Text, Does.Contain("*"));
        Assert.That(FindNamed("lostPersonFirstName").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("lostPersonFirstName").Text, Does.Contain("*"));
        Assert.That(FindNamed("lostPersonLastName").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("lostPersonLastName").Text, Does.Contain("*"));
        Assert.That(FindNamed("lostPersonFatherName").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("lostPersonFatherName").Text, Does.Contain("*"));
        Assert.That(FindNamed("lostPersonMotherName").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("lostPersonMotherName").Text, Does.Contain("*"));
        Assert.That(FindDateInput("lostPersonBirthDate").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("lostPersonBirthDate").Text, Does.Contain("*"));
        Assert.That(FindNamed("lostPersonBirthplace").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabelFor("lostPersonBirthplace").Text, Does.Contain("*"));

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 2");
        FillInput(FindNamed("documentNumber"), "test");
        FillInput(FindNamed("lostPersonFirstName"), "test");
        FillInput(FindNamed("lostPersonLastName"), "test");
        FillInput(FindNamed("lostPersonFatherName"), "test");
        FillInput(FindNamed("lostPersonMotherName"), "test");
        FillDate("lostPersonBirthDate", "01.01.1990", 1990, 1, 1);
        FillInput(FindNamed("lostPersonBirthplace"), "test");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("GJENDEM NË SITUATË TË VËSHTIRË");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("GJENDEM NË SITUATË TË VËSHTIRË"));

        Log("Assert kohëzgjatja Step 3");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 4 hapa, tre te paret aktiv");
        AssertSteps(3, 4);

        Log("Assert fushen e pershkrimit");
        IWebElement assistanceReq = FindNamed("assistanceReq");
        Assert.That(assistanceReq.TagName, Is.EqualTo("textarea"));
        Assert.That(assistanceReq.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso pershkrimin");
        FillInput(FindNamed("assistanceReq"), "Test");

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 4 Title");
        IWebElement Step4Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(), Does.StartWith("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 4");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 4 hapa, te gjithe aktiv");
        AssertSteps(4, 4);

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Dokumenta që ngarkohen nga Aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Dokumenta që ngarkohen nga nëpunësi i administratës publike')]"))
            .Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(@class,'text-muted') and contains(.,'Për këtë shërbim nuk nevojitet të sigurohen nga nënpunësit e administratës')]"))
            .Displayed, Is.True);

        Log("Assert document-upload Dokument identifikimi");
        AssertDocumentUpload(IdUploadId, "Dokument identifikimi (pasaportë ose kartë identiteti biometrike)");

        Log("Assert document-upload Dokument identiteti për shtetasit e humbur");
        AssertDocumentUpload(LostCitizenUploadId, "Dokument identiteti për shtetasit e humbur");

        Log("Assert document-upload Dokumente të tjera");
        AssertDocumentUpload(AdditionalUploadId, "Dokumente të tjera (që i vijnë në ndihmë çështjes)");

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

        Log("Ngarko dokumentin e detyrueshem");
        UploadDocument(IdUploadId, DocumentPath);

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
        Assert.That(serviceName.Text.Replace('\u00A0', ' ').Trim(), Is.EqualTo(ExpectedServiceName),
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

    private void AssertSteps(int activeCount, int totalCount)
    {
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(totalCount));
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
            var titles = d.FindElements(By.CssSelector("h4.text-uppercase, h5.text-uppercase"));
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
        return wait.Until(ExpectedConditions.ElementExists(By.Name(name)));
    }

    private IWebElement FindLabelFor(string forId)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector($"label[for='{forId}']")));
    }

    private IWebElement FindDateInput(string fieldId)
    {
        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//label[@for='{fieldId}']/following-sibling::div//input[contains(@class,'flatpickr-input')]")));
    }

    private void SelectByValue(IWebElement select, string value)
    {
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            select);
        Thread.Sleep(300);
        new SelectElement(select).SelectByValue(value);
        Thread.Sleep(800);
    }

    private void SelectFirstWhenEnabled(By locator)
    {
        var selectWait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        selectWait.Until(d =>
        {
            try
            {
                var els = d.FindElements(locator);
                if (els.Count == 0)
                    return false;
                var el = els[0];
                if (!el.Enabled)
                    return false;
                var se = new SelectElement(el);
                return se.Options.Any(o => !string.IsNullOrWhiteSpace(o.GetAttribute("value")));
            }
            catch
            {
                return false;
            }
        });

        IWebElement select = driver.FindElement(locator);
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            select);
        Thread.Sleep(300);

        var dropdown = new SelectElement(select);
        var options = dropdown.Options
            .Where(o => !string.IsNullOrWhiteSpace(o.GetAttribute("value")))
            .ToList();
        Assert.That(options, Is.Not.Empty, "Select nuk ka opsione te disponueshme");
        dropdown.SelectByValue(options[0].GetAttribute("value"));
        Thread.Sleep(1000);
    }

    private void FillDate(string fieldId, string displayDate, int year, int month, int day)
    {
        IWebElement input = FindDateInput(fieldId);

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
                string current = FindDateInput(fieldId).GetAttribute("value") ?? string.Empty;
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

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {
        Assert.That(driver.FindElement(
            By.XPath($"//span[contains(@class,'fw-bold') and contains(normalize-space(),'{documentTitle}')]"))
            .Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-15866"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("15"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("15"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf,.jpg,.jpeg,.png"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të ngarkuar dokumentin"));

        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të ngarkuar dokumentin"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='hint']")).Text.Trim(),
            Is.EqualTo("Formatet e lejuara: PDF, JPG, JPEG, PNG. Madhësia maksimale: 15MB."));
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
