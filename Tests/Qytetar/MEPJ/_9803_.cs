using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.MEPJ;

[Category("MEPJ")]
[Category("9803")]
public class _9803_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "9803";
    protected override string? ServiceTitle => "AplikimPerVertetimCertifikateFamiljare";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName = "Aplikim për vërtetim certifikatë familjare";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";
    private const string IdentifikimiUploadId = "dokID_vcfUpload";
    private const int TotalSteps = 5;

    [Test]
    public void AplikimPerVertetimCertifikateFamiljare()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert Title Step 1");
        IWebElement Step1Title = WaitForStepTitle("TË DHËNAT PERSONALE TË APLIKANTIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNAT PERSONALE TË APLIKANTIT"));

        Log("Assert kohëzgjatja");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 5 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert te dhenat e aplikantit te para-plotesuara");
        AssertReadonlyById("nid", Settings.Qytetar.Username);
        AssertReadonlyById("emri", "Katerina");
        AssertReadonlyById("mbiemri", "Jançe");
        AssertReadonlyById("gjinia", "Femër", expectDisabled: true);
        AssertReadonlyById("datelindja", "13.04.1993");
        AssertReadonlyById("atesia", "Foti");
        AssertReadonlyById("amesia", "Manushaqe");

        IWebElement vendlindjaQyteti = FindById("vendlindjaQyteti");
        Assert.That(vendlindjaQyteti.GetAttribute("value").Trim(), Is.EqualTo("Korçë"));
        Assert.That(vendlindjaQyteti.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(vendlindjaQyteti.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindLabel("Vendlindja/Qyteti").Text, Does.Contain("*"));

        IWebElement vendlindjaShteti = FindById("vendlindjaShteti");
        Assert.That(new SelectElement(vendlindjaShteti).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Vendlindja/Shteti").Text, Does.Contain("*"));

        IWebElement shtetesia = FindById("shtetesia");
        Assert.That(shtetesia.GetAttribute("value").Trim(), Is.EqualTo("Shqiptare"));
        Assert.That(FindLabel("Shtetësia").Text, Does.Contain("*"));

        IWebElement shtetesiaDyte = FindById("shtetesiaDyte");
        Assert.That(shtetesiaDyte.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 1");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Përzgjidhni një vlerë për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 1");
        SelectByValue(vendlindjaShteti, "1");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("ADRESA E APLIKANTIT (NË VENDIN E REZIDENCËS)");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("ADRESA E APLIKANTIT (NË VENDIN E REZIDENCËS)"));

        Log("Assert kohëzgjatja Step 2");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 5 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert fushat e adreses");
        Assert.That(new SelectElement(FindById("vendlindjaShteti")).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Shteti").Text, Does.Contain("*"));
        Assert.That(FindByName("street").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Rruga dhe numri i banesës").Text, Does.Contain("*"));
        Assert.That(FindByName("city").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Qyteti").Text, Does.Contain("*"));
        Assert.That(FindByName("postalCode").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Kodi postar").Text, Does.Contain("*"));
        Assert.That(FindByName("region").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindByName("otherAddressDetails").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 2");
        SelectByValue(FindById("vendlindjaShteti"), "2");
        FillInput(FindByName("street"), "test");
        FillInput(FindByName("city"), "test");
        FillInput(FindByName("postalCode"), "1001");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("KONTAKTI");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(), Does.StartWith("KONTAKTI"));

        Log("Assert kohëzgjatja Step 3");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 5 hapa, tre te paret aktiv");
        AssertSteps(3);

        Log("Assert te dhenat e kontaktit");
        IWebElement email = FindById("email");
        Assert.That(email.GetAttribute("type"), Is.EqualTo("email"));
        Assert.That(email.GetAttribute("value").Trim(), Is.EqualTo("katerina.jance@kreatx.com"));
        Assert.That(email.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(email.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindLabel("Adresa elektronike (Email)").Text, Does.Contain("*"));

        IWebElement tel = FindByName("phoneNumber");
        Assert.That(tel.GetAttribute("value").Trim(), Is.EqualTo("+355697008820"));
        Assert.That(tel.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(tel.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindLabel("Nr. tel").Text, Does.Contain("*"));

        IWebElement contactCountry = FindById("vendlindjaShteti");
        Assert.That(new SelectElement(contactCountry).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Shteti").Text, Does.Contain("*"));

        IWebElement consularOffice = FindById("konsullataShteti");
        Assert.That(new SelectElement(consularOffice).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Zyra konsullore").Text, Does.Contain("*"));

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Përzgjidhni një vlerë për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 3");
        SelectByValue(contactCountry, "2");
        SelectFirstWhenEnabled(By.Id("konsullataShteti"));

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 4 Title");
        IWebElement Step4Title = WaitForStepTitle("PËRBËRJA FAMILJARE");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(), Does.StartWith("PËRBËRJA FAMILJARE"));

        Log("Assert kohëzgjatja Step 4");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 5 hapa, kater te paret aktiv");
        AssertSteps(4);

        Log("Assert tabelen e perberjes familjare");
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Nr.']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Emri']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Atësia']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Mbiemri']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Datëlindja']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//button[contains(.,'SHTO')]")).Displayed, Is.True);

        Log("Assert butonat e navigimit Step 4");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa shtuar pjesetar te familjes");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        IWebElement familyError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//*[contains(normalize-space(.),'Ju duhet të shtoni të paktën një pjesëtar të familjes për të vazhduar.')]")));
        Assert.That(familyError.Text.Trim(),
            Is.EqualTo("Ju duhet të shtoni të paktën një pjesëtar të familjes për të vazhduar."));

        Log("Kliko butonin Shto familjar");
        SafeClick(By.XPath("//button[contains(.,'SHTO')]"));

        Log("Assert titullin e modalit shto familjar");
        IWebElement modalTitle = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h2.custom-modal-title")));
        Assert.That(modalTitle.Text.Trim(), Is.EqualTo("Shto pjesëtar të familjes"));

        Log("Ploteso te dhenat e familjarit");
        FillInput(FindModalInput("Emri"), "test");
        FillInput(FindModalInput("Mbiemri"), "test");
        FillInput(FindModalInput("Atësia"), "test");
        FillInput(FindModalInput("Amësia"), "test");
        FillDate(FindModalDateInput("Datëlindja"), "10.04.1990", 1990, 4, 10);
        SelectByValue(FindModalSelect("Vendl. Shteti"), "1");
        FillInput(FindModalInput("Vendl. Qyteti"), "test");
        FillInput(FindModalInput("Lidhja me kryefamiljarin"), "test");
        SelectByValue(FindModalSelect("Gjinia"), "f");
        SelectByValue(FindModalSelect("Gjendja civile"), "1");
        FillInput(FindModalInput("Shtetësia"), "test");

        Log("Kliko Ruaj");
        SafeClick(By.CssSelector("button.ealb-add-button-modal"));
        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
            By.CssSelector("div.custom-modal-content")));

        Log("Assert se familjari u shtua ne listen e familjareve");
        IWebElement addedFamilyMember = wait.Until(d =>
        {
            var rows = d.FindElements(By.CssSelector("table tbody tr"));
            return rows.FirstOrDefault(r =>
            {
                try { return r.Displayed && r.Text.Contains("test"); }
                catch (StaleElementReferenceException) { return false; }
            });
        });
        Assert.That(addedFamilyMember.Text.Trim(), Does.Contain("test"));

        Log("Kliko Vazhdo Step 4");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 5 Title");
        IWebElement Step5Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step5Title.Text.Trim().ToUpperInvariant(), Does.StartWith("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 5");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 5 hapa, te gjithe aktiv");
        AssertSteps(5);

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës')]"))
            .Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Certifikatë familjare')]")).Displayed, Is.True);

        Log("Assert document-upload dokumentet e aplikantit");
        AssertDocumentUpload(IdentifikimiUploadId,
            "Dokument identifikimi (Pasaportë ose kartë identiteti shqiptare)", required: false);

        Log("Assert checkbox pëlqimi");
        IWebElement agreeCheck = wait.Until(ExpectedConditions.ElementExists(By.Id("agreementCheckbox")));
        Assert.That(agreeCheck.GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='agreementCheckbox']")).Text,
            Does.Contain("Me klikimin e këtij butoni, ju bini dakord"));

        Log("Assert butonat e navigimit Step 5");
        AssertNavigationButtons("Dërgo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue.with-arrow")));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Kliko Dergo pa ngarkuar dokumentet");
        SafeClick(By.CssSelector("button.ealb-btn-continue.with-arrow"));
        Thread.Sleep(1000);
        Assert.That(WaitForStepTitle("DOKUMENTACIONI").Text.Trim().ToUpperInvariant(),
            Does.StartWith("DOKUMENTACIONI"));

        Log("Ngarko dokumentin");
        UploadDocument(IdentifikimiUploadId, DocumentPath);

        Log("Kliko checkbox pëlqimi");
        IWebElement agree = driver.FindElement(By.Id("agreementCheckbox"));
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", agree);

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
        Assert.That(steps.Count, Is.EqualTo(TotalSteps));
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
            By.XPath($"//label[contains(.,'{labelPart}')]")));
    }

    private void AssertReadonlyById(string id, string expectedValue, bool expectDisabled = false)
    {
        IWebElement field = FindById(id);
        Assert.That(field.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(field.GetAttribute("readonly"), Is.Not.Null);
        if (expectDisabled)
            Assert.That(field.GetAttribute("disabled"), Is.Not.Null);
    }

    private IWebElement FindModalInput(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//div[contains(@class,'custom-modal-content')]//label[contains(.,'{labelPart}')]/following::input[not(@type='hidden')][1]")));
    }

    private IWebElement FindModalSelect(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//div[contains(@class,'custom-modal-content')]//label[contains(.,'{labelPart}')]/following::select[1]")));
    }

    private IWebElement FindModalDateInput(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//div[contains(@class,'custom-modal-content')]//label[contains(.,'{labelPart}')]/following::input[contains(@class,'flatpickr-input')][1]")));
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
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-9803"));
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
