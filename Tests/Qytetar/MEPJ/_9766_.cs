using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.MEPJ;

[Category("MEPJ")]
[Category("9766")]
public class _9766_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "9766";
    protected override string? ServiceTitle => "LejeTransportTrupiHiri";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Aplikim për leje transport trupi / hiri (Laissez Passer for a Corpse)";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";
    private const string DeceasedIdUploadId = "deceasedIdentificationUpload";
    private const string DeathCertificateUploadId = "deathCertificateUpload";
    private const string ApplicantIdUploadId = "applicantIdentificationUpload";
    private const string FuneralAuthUploadId = "funeralAgencyAuthorizationUpload";

    [Test]
    public void LejeTransportTrupiHiri()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert Title Step 1");
        IWebElement Step1Title = WaitForStepTitle("TË DHËNA MBI TË VDEKURIN");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNA MBI TË VDEKURIN"));

        Log("Assert kohëzgjatja");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 4 hapa, hapi i pare aktiv");
        AssertSteps(1, 4);

        Log("Assert fushat e detyrueshme Step 1");
        IWebElement appliesTo = FindById("appliesTo");
        var appliesToSelect = new SelectElement(appliesTo);
        Assert.That(appliesToSelect.SelectedOption.GetAttribute("value"), Is.EqualTo("1"));
        Assert.That(appliesToSelect.Options[0].Text.Trim(), Is.EqualTo("Familjari"));
        Assert.That(appliesToSelect.Options[1].Text.Trim(), Is.EqualTo("Agjencia funerale"));
        Assert.That(FindLabel("Aplikon").Text, Does.Contain("*"));

        IWebElement permissionFor = FindById("permissionFor");
        var permissionSelect = new SelectElement(permissionFor);
        Assert.That(permissionSelect.SelectedOption.GetAttribute("value"), Is.EqualTo("1"));
        Assert.That(permissionSelect.Options[0].Text.Trim(), Is.EqualTo("Transport trupi"));
        Assert.That(permissionSelect.Options[1].Text.Trim(), Is.EqualTo("Transport hiri"));
        Assert.That(FindLabel("Leje për").Text, Does.Contain("*"));

        Assert.That(FindByName("name").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Emri").Text, Does.Contain("*"));
        Assert.That(FindByName("surname").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Mbiemri").Text, Does.Contain("*"));

        IWebElement gender = FindById("gender");
        var genderSelect = new SelectElement(gender);
        Assert.That(genderSelect.SelectedOption.GetAttribute("value"), Is.EqualTo("Mashkull"));
        Assert.That(genderSelect.Options[0].Text.Trim(), Is.EqualTo("Mashkull"));
        Assert.That(genderSelect.Options[1].Text.Trim(), Is.EqualTo("Femër"));
        Assert.That(FindLabel("Gjinia").Text, Does.Contain("*"));

        Assert.That(FindDateInputByLabel("Datëlindja").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Datëlindja").Text, Does.Contain("*"));
        Assert.That(FindByName("placeOfBirth").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Vendlindja").Text, Does.Contain("*"));

        IWebElement citizenship = FindByName("citizenship");
        Assert.That(citizenship.GetAttribute("value").Trim(), Is.EqualTo("Shqiptare"));
        Assert.That(FindLabel("Shtetësia").Text, Does.Contain("*"));

        Assert.That(FindDateInputByLabel("Data e vdekjes").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Data e vdekjes").Text, Does.Contain("*"));
        Assert.That(FindByName("placeOfDeath").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Qyteti i vdekjes").Text, Does.Contain("*"));
        Assert.That(FindByName("causesOfDeath").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Shkaqet e vdekjes").Text, Does.Contain("*"));

        Log("Assert butonat e navigimit Step 1");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 1");
        SelectByValue(appliesTo, "1");
        SelectByValue(permissionFor, "1");
        FillInput(FindByName("name"), "test");
        FillInput(FindByName("surname"), "test");
        SelectByValue(gender, "Mashkull");
        FillDateByLabel("Datëlindja", "01.01.1950", 1950, 1, 1);
        FillInput(FindByName("placeOfBirth"), "test");
        FillDateByLabel("Data e vdekjes", "29.04.2026", 2026, 4, 29);
        FillInput(FindByName("placeOfDeath"), "test");
        FillInput(FindByName("causesOfDeath"), "test");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("ADRESA E APLIKANTIT (NË VENDIN E REZIDENCËS)");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("ADRESA E APLIKANTIT (NË VENDIN E REZIDENCËS)"));

        Log("Assert kohëzgjatja Step 2");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 4 hapa, dy te paret aktiv");
        AssertSteps(2, 4);

        Log("Assert fushat e adreses");
        Assert.That(new SelectElement(FindById("state")).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Shteti").Text, Does.Contain("*"));
        Assert.That(FindByName("city").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Qyteti").Text, Does.Contain("*"));
        Assert.That(FindByName("region").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindByName("streetAndApartment").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Rruga dhe numri i banesës").Text, Does.Contain("*"));
        Assert.That(FindByName("zipCode").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Kodi postar").Text, Does.Contain("*"));
        Assert.That(FindByName("otherInfo").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 2");
        SelectByValue(FindById("state"), "2");
        FillInput(FindByName("city"), "test");
        FillInput(FindByName("streetAndApartment"), "test");
        FillInput(FindByName("zipCode"), "1001");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("KONTAKTI");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(), Does.StartWith("KONTAKTI"));

        Log("Assert kohëzgjatja Step 3");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 4 hapa, tre te paret aktiv");
        AssertSteps(3, 4);

        Log("Assert te dhenat e kontaktit");
        IWebElement email = FindByName("email");
        Assert.That(email.GetAttribute("type"), Is.EqualTo("email"));
        Assert.That(email.GetAttribute("value").Trim(), Is.EqualTo("katerina.jance@kreatx.com"));
        Assert.That(email.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(email.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindLabel("Adresa elektronike").Text, Does.Contain("*"));

        IWebElement phone = FindByName("phoneNumber");
        Assert.That(phone.GetAttribute("type"), Is.EqualTo("tel"));
        Assert.That(phone.GetAttribute("value").Trim(), Is.EqualTo("+355697008820"));
        Assert.That(phone.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(phone.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindLabel("Nr. tel").Text, Does.Contain("*"));

        IWebElement contactCountry = FindById("country");
        Assert.That(new SelectElement(contactCountry).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Shteti").Text, Does.Contain("*"));

        IWebElement consularOffice = FindById("consularOffice");
        Assert.That(new SelectElement(consularOffice).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Zyra konsullore").Text, Does.Contain("*"));

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Përzgjidhni një vlerë për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 3");
        SelectByValue(contactCountry, "Turqia");
        SelectFirstWhenEnabled(By.Name("consularOffice"));

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

        Log("Assert document-upload dokumentet e aplikantit");
        AssertDocumentUpload(DeceasedIdUploadId, "Dokument identifikimi i shtetasit të vdekur", required: true);
        AssertDocumentUpload(DeathCertificateUploadId, "Certifikatë vdekje apo akt vdekje", required: true);
        AssertDocumentUpload(ApplicantIdUploadId, "Dokument identifikimi i aplikuesit", required: true);
        AssertDocumentUpload(FuneralAuthUploadId, "Nëse aplikuesi është agjencia funerale", required: false);

        Log("Assert butonat e navigimit Step 4");
        AssertNavigationButtons("Dërgo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue.with-arrow")));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Kliko Dergo pa ngarkuar dokumentet e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue.with-arrow"));
        Thread.Sleep(1000);
        Assert.That(WaitForStepTitle("DOKUMENTACIONI").Text.Trim().ToUpperInvariant(),
            Does.StartWith("DOKUMENTACIONI"));

        Log("Ngarko dokumentet e detyrueshme");
        UploadDocument(DeceasedIdUploadId, DocumentPath);
        UploadDocument(DeathCertificateUploadId, DocumentPath);
        UploadDocument(ApplicantIdUploadId, DocumentPath);

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

    private IWebElement FindById(string id)
    {
        return wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
    }

    private IWebElement FindByName(string name)
    {
        return wait.Until(ExpectedConditions.ElementExists(By.Name(name)));
    }

    private IWebElement FindLabel(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//form//label[contains(.,'{labelPart}')]")));
    }

    private IWebElement FindDateInputByLabel(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//label[contains(.,'{labelPart}')]/following::input[contains(@class,'flatpickr-input')][1]")));
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

    private void FillDateByLabel(string labelPart, string displayDate, int year, int month, int day)
    {
        FillDate(FindDateInputByLabel(labelPart), displayDate, year, month, day);
    }

    private void FillDate(IWebElement input, string displayDate, int year, int month, int day)
    {
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
            } else if (el._flatpickr) {
                el._flatpickr.setDate(date, true);
                el._flatpickr.close();
            } else {
                const setter = Object.getOwnPropertyDescriptor(window.HTMLInputElement.prototype, 'value').set;
                setter.call(el, display);
                el.dispatchEvent(new Event('input', { bubbles: true }));
                el.dispatchEvent(new Event('change', { bubbles: true }));
            }
        ", input, displayDate, year, month, day);

        wait.Until(_ =>
        {
            try
            {
                string current = input.GetAttribute("value") ?? string.Empty;
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

    private void AssertDocumentUpload(string uploadId, string documentTitle, bool required)
    {
        IWebElement title = driver.FindElement(
            By.XPath($"//span[contains(@class,'fw-bold') and contains(normalize-space(),'{documentTitle}')]"));
        Assert.That(title.Displayed, Is.True);
        if (required)
            Assert.That(title.Text, Does.Contain("*"));
        else
            Assert.That(title.Text.Trim(), Does.Not.EndWith("*"));

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-9766"));
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
