using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.MEPJ;

[Category("MEPJ")]
[Category("9486")]
public class _9486_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "9486";
    protected override string? ServiceTitle => "VertetimCertifikateMartese";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName = "Aplikim për vërtetim certifikatë martese";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";
    private const string IdUploadId = "fileIdUpload";

    [Test]
    public void VertetimCertifikateMartese()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert Title Step 1");
        IWebElement Step1Title = WaitForStepTitle("TË DHËNA PERSONALE TË APLIKANTIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNA PERSONALE TË APLIKANTIT"));

        Log("Assert kohëzgjatja");
        AssertDuration("5 minuta kohëzgjatje");

        Log("Assert 7 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert te dhenat e aplikantit te para-plotesuara");
        AssertDisabledNamed("nid", Settings.Qytetar.Username);
        AssertDisabledNamed("emri", "Katerina");
        AssertDisabledNamed("mbiemri", "Jançe");
        AssertDisabledNamed("atesia", "Foti");
        AssertDisabledNamed("amesia", "Manushaqe");
        AssertDisabledNamed("datelindja", "13.04.1993");
        AssertDisabledNamed("vendlindjaQyteti", "Korçë");

        IWebElement gjinia = FindNamed("gjinia");
        Assert.That(new SelectElement(gjinia).SelectedOption.Text.Trim(), Is.EqualTo("Femër"));
        Assert.That(gjinia.GetAttribute("disabled"), Is.Not.Null);

        Assert.That(FindLabel("Vendlindja/Qyteti").Text, Does.Contain("*"));
        Assert.That(FindLabel("Vendlindja/Shteti").Text, Does.Contain("*"));
        Assert.That(new SelectElement(FindNamed("vendlindjaShteti")).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));

        IWebElement shtetesia = FindNamed("shtetesia");
        Assert.That(shtetesia.GetAttribute("value").Trim(), Is.EqualTo("Shqiptare"));
        Assert.That(shtetesia.GetAttribute("disabled"), Is.Null);
        Assert.That(FindLabel("Shtetësia").Text, Does.Contain("*"));

        IWebElement shtetesiaDyte = FindNamed("shtetesiaDyte");
        Assert.That(shtetesiaDyte.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(shtetesiaDyte.GetAttribute("disabled"), Is.Null);

        Log("Assert butonat e navigimit Step 1");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Përzgjidhni një vlerë për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 1");
        SelectByValue(FindNamed("vendlindjaShteti"), "1");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("ADRESA E APLIKANTIT (NË VENDIN E REZIDENCËS)");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("ADRESA E APLIKANTIT (NË VENDIN E REZIDENCËS)"));

        Log("Assert kohëzgjatja Step 2");
        AssertDuration("5 minuta kohëzgjatje");

        Log("Assert 7 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert fushat e adreses");
        Assert.That(FindLabel("Shteti").Text, Does.Contain("*"));
        Assert.That(FindLabel("Rruga dhe numri i banesës").Text, Does.Contain("*"));
        Assert.That(FindLabel("Qyteti").Text, Does.Contain("*"));
        Assert.That(FindLabel("Kodi postar").Text, Does.Contain("*"));

        Assert.That(new SelectElement(FindNamed("shteti")).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindNamed("rruga").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindNamed("qyteti").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindNamed("kodiPostar").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindNamed("rajoni").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindNamed("adresaTjeter").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 2");
        SelectByValue(FindNamed("shteti"), "2");
        FillInput(FindNamed("rruga"), "test");
        FillInput(FindNamed("qyteti"), "test");
        FillInput(FindNamed("kodiPostar"), "1001");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("KONTAKTI");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(), Does.StartWith("KONTAKTI"));

        Log("Assert kohëzgjatja Step 3");
        AssertDuration("5 minuta kohëzgjatje");

        Log("Assert 7 hapa, tre te paret aktiv");
        AssertSteps(3);

        Log("Assert te dhenat e kontaktit");
        IWebElement email = FindNamed("email");
        Assert.That(email.GetAttribute("type"), Is.EqualTo("email"));
        Assert.That(email.GetAttribute("value").Trim(), Is.EqualTo("katerina.jance@kreatx.com"));
        Assert.That(email.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(FindLabel("Adresa elektronike").Text, Does.Contain("*"));

        IWebElement phoneNumber = FindNamed("phoneNumber");
        Assert.That(phoneNumber.GetAttribute("type"), Is.EqualTo("tel"));
        Assert.That(phoneNumber.GetAttribute("value").Trim(), Is.EqualTo("+355697008820"));
        Assert.That(phoneNumber.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(phoneNumber.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(FindLabel("Nr. tel").Text, Does.Contain("*"));

        Assert.That(FindNamed("fax").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement country = FindNamed("country");
        Assert.That(new SelectElement(country).SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Shteti").Text, Does.Contain("*"));

        IWebElement consularOffice = FindNamed("consularOffice");
        Assert.That(new SelectElement(consularOffice).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Zyra konsullore").Text, Does.Contain("*"));

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Përzgjidhni një vlerë për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 3");
        SelectByValue(FindNamed("country"), "Algjeria");
        SelectFirstWhenEnabled(By.Name("consularOffice"));

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 4 Title");
        IWebElement Step4Title = WaitForStepTitle("TË DHËNAT E BASHKËSHORTIT");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNAT E BASHKËSHORTIT"));

        Log("Assert kohëzgjatja Step 4");
        AssertDuration("5 minuta kohëzgjatje");

        Log("Assert 7 hapa, kater te paret aktiv");
        AssertSteps(4);

        Log("Assert fushat e bashkeshortit");
        Assert.That(FindLabel("Emri").Text, Does.Contain("*"));
        Assert.That(FindLabel("Mbiemri").Text, Does.Contain("*"));
        Assert.That(FindLabel("Datëlindja").Text, Does.Contain("*"));
        Assert.That(FindLabel("Vendlindja").Text, Does.Contain("*"));
        Assert.That(FindNamed("firstName").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindNamed("lastName").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindDateInputByLabel("Datëlindja").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindNamed("birthPlace").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 4");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 4");
        FillInput(FindNamed("firstName"), "test");
        FillInput(FindNamed("lastName"), "test");
        FillDateByLabel("Datëlindja", "01.01.1990", 1990, 1, 1);
        FillInput(FindNamed("birthPlace"), "test");

        Log("Kliko Vazhdo Step 4");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 5 Title");
        IWebElement Step5Title = WaitForStepTitle("TË DHËNAT E BASHKËSHORTES");
        Assert.That(Step5Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNAT E BASHKËSHORTES"));

        Log("Assert kohëzgjatja Step 5");
        AssertDuration("5 minuta kohëzgjatje");

        Log("Assert 7 hapa, pese te paret aktiv");
        AssertSteps(5);

        Log("Assert fushat e bashkeshortes");
        Assert.That(FindLabel("Emri").Text, Does.Contain("*"));
        Assert.That(FindLabel("Mbiemri").Text, Does.Contain("*"));
        Assert.That(FindLabel("Datëlindja").Text, Does.Contain("*"));
        Assert.That(FindLabel("Vendlindja").Text, Does.Contain("*"));
        Assert.That(FindNamed("firstName").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindNamed("lastName").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindDateInputByLabel("Datëlindja").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindNamed("birthPlace").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 5");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 5");
        FillInput(FindNamed("firstName"), "test");
        FillInput(FindNamed("lastName"), "test");
        FillDateByLabel("Datëlindja", "01.01.1990", 1990, 1, 1);
        FillInput(FindNamed("birthPlace"), "test");

        Log("Kliko Vazhdo Step 5");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 6 Title");
        IWebElement Step6Title = WaitForStepTitle("DETAJET E MARTESËS");
        Assert.That(Step6Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("DETAJET E MARTESËS"));

        Log("Assert kohëzgjatja Step 6");
        AssertDuration("5 minuta kohëzgjatje");

        Log("Assert 7 hapa, gjashte te paret aktiv");
        AssertSteps(6);

        Log("Assert fushat e marteses");
        Assert.That(FindLabel("Data e lidhjes së martesës").Text, Does.Contain("*"));
        Assert.That(FindLabel("Vendi i lidhjes së martesës").Text, Does.Contain("*"));
        Assert.That(FindNamed("date").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindNamed("place").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 6");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 6");
        FillDate(FindNamed("date"), "15.06.2015", 2015, 6, 15);
        FillInput(FindNamed("place"), "test");

        Log("Kliko Vazhdo Step 6");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 7 Title");
        IWebElement Step7Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step7Title.Text.Trim().ToUpperInvariant(), Does.StartWith("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 7");
        AssertDuration("5 minuta kohëzgjatje");

        Log("Assert 7 hapa, te gjithe aktiv");
        AssertSteps(7);

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Dokumenta që ngarkohen nga Aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Dokumenta që ngarkohen nga nëpunësi i administratës publike')]"))
            .Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//ol//li[contains(.,'Certifikatë martese')]")).Displayed, Is.True);

        Log("Assert document-upload Dokument identifikimi");
        AssertDocumentUpload(IdUploadId, "Dokument identifikimi (Pasaportë ose kartë identiteti shqiptare)");

        Log("Assert checkbox dokumentacioni");
        IWebElement consentCheckbox = wait.Until(ExpectedConditions.ElementExists(By.Id("agreeCheck")));
        Assert.That(consentCheckbox.GetAttribute("type"), Is.EqualTo("checkbox"));
        IWebElement consentLabel = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("label[for='agreeCheck']")));
        Assert.That(consentLabel.Text.Trim(),
            Does.Contain("Me klikimin e këtij butoni, ju bini dakord"));

        Log("Assert butonat e navigimit Step 7");
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

        Log("Kliko checkboxin e dokumentacionit");
        SafeClick(By.CssSelector("label[for='agreeCheck']"));
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

    private void AssertSteps(int activeCount)
    {
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(7));
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

    private void AssertDisabledNamed(string name, string expectedValue)
    {
        IWebElement field = FindNamed(name);
        Assert.That(field.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(field.GetAttribute("disabled"), Is.Not.Null);
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

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {
        Assert.That(driver.FindElement(
            By.XPath($"//span[contains(@class,'fw-bold') and contains(normalize-space(),'{documentTitle}')]"))
            .Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-9486"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("15"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("15"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf,.jpg,.jpeg,.png"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të ngarkuar dokument"));

        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të ngarkuar dokument"));
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
