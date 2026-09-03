using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.AMS;

[Category("AMS")]
[Category("15389")]
public class _15389_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "15389";
    protected override string? ServiceTitle => "PyjetdheKullotat";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Aplikim për lëshimin e certifikatës së ekspertit për pyjet dhe kullotat";
    private const string ExpectedAddress =
        "TIRANË,FROSINA PLAKU; Nd. 88; H. 2; Ap. 9; NJËSIA ADMINISTRATIVE NR. 7; NJËSIA BASHKIAKE NR. 7; 1023; TIRANË";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";

    [Test]
    public void PyjetdheKullotat()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 3 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert Step 1 Title");
        IWebElement Step1Title = WaitForStepTitle("TË DHËNAT E APLIKANTIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNAT E APLIKANTIT"));

        Log("Assert te dhenat e aplikantit");
        AssertReadonlyInput("NID", Settings.Qytetar.Username);
        AssertReadonlyInput("Emri", "Katerina");
        AssertReadonlyInput("Atësia", "Foti");
        AssertReadonlyInput("Mbiemri", "Jançe");
        AssertReadonlyInput("Nr. Cel", "+355697008820");
        AssertReadonlyInput("Email", "katerina.jance@kreatx.com");
        AssertReadonlyInput("Vendlindja", "Korçë");
        AssertReadonlyInput("Datëlindja", "13.04.1993");
        AssertReadonlyInput("Adresa", ExpectedAddress);

        IWebElement genderMale = wait.Until(ExpectedConditions.ElementExists(By.Id("genderMale")));
        IWebElement genderFemale = wait.Until(ExpectedConditions.ElementExists(By.Id("genderFemale")));
        Assert.That(genderMale.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(genderFemale.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(genderMale.GetAttribute("value"), Is.EqualTo("M"));
        Assert.That(genderFemale.GetAttribute("value"), Is.EqualTo("F"));
        Assert.That(genderMale.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(genderFemale.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(genderFemale.Selected, Is.True);
        Assert.That(genderMale.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='genderMale']")).Text.Trim(),
            Is.EqualTo("Mashkull"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='genderFemale']")).Text.Trim(),
            Is.EqualTo("Femër"));

        Log("Assert seksionet e hapit 1");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Të dhënat personale')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Kualifikimi')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Përvoja e punës dhe kohëzgjatja e saj')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Aftësi personale dhe kompetencat')]")).Displayed, Is.True);

        Log("Assert butonat e navigimit Step 1");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko + SHTO KUALIFIKIM");
        SafeClick(By.XPath("//button[contains(.,'+ SHTO KUALIFIKIM')]"));

        Log("Assert kualifikimi modal");
        IWebElement kualifikimModal = WaitForModal("+ SHTO KUALIFIKIM");
        Assert.That(kualifikimModal.Displayed, Is.True);

        Log("Ploteso fushat e detyrueshme te kualifikimit");
        FillModalDate("Data nga", "14.04.2026");
        FillModalDate("Data deri", "14.04.2026");
        FillInput(FindModalInput("Titulli i kualifikimit të arritur"), "test");
        FillInput(FindModalInput("Temat kryesore"), "test");
        FillInput(FindModalInput("Emri dhe lloji i subjektit"), "test");

        Log("Kliko Ruaj kualifikim");
        SafeClick(By.CssSelector("button.ealb-add-button-modal"));
        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
            By.CssSelector(".custom-modal-content")));
        Thread.Sleep(1000);

        Log("Kliko + SHTO PËRVOJË PUNE");
        SafeClick(By.XPath("//button[contains(.,'+ SHTO PËRVOJË PUNE')]"));

        Log("Assert pervoja modal");
        IWebElement pervojaModal = WaitForModal("+ SHTO PËRVOJË PUNE");
        Assert.That(pervojaModal.Displayed, Is.True);

        Log("Ploteso fushat e detyrueshme te pervojes se punes");
        FillModalDate("Data nga", "14.04.2026");
        FillModalDate("Data deri", "15.04.2026");
        FillInput(FindModalInput("Roli ose pozicioni i punës"), "test");
        FillInput(FindModalInput("Aktivitetet kryesore dhe përgjegjësitë"), "test");
        FillInput(FindModalInput("Emri dhe adresa e punëdhësit"), "test");
        FillInput(FindModalInput("Lloji i biznesit ose sektori"), "test");

        Log("Kliko Ruaj pervoje pune");
        SafeClick(By.CssSelector("button.ealb-add-button-modal"));
        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
            By.CssSelector(".custom-modal-content")));
        Thread.Sleep(1000);

        Log("Ploteso gjuhen meme");
        IWebElement gjuhaMeme = FindInputByLabel("Gjuha mëmë");
        Assert.That(gjuhaMeme.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        FillInput(gjuhaMeme, "Shqip");

        Log("Kliko + SHTO GJUHË");
        SafeClick(By.XPath("//button[contains(.,'+ SHTO GJUHË')]"));

        Log("Assert gjuha modal");
        IWebElement gjuhaModal = WaitForModal("+ SHTO GJUHË");
        Assert.That(gjuhaModal.Displayed, Is.True);

        Log("Ploteso fushat e detyrueshme te gjuhes");
        FillInput(FindModalInput("Gjuhë të tjera"), "test");
        FillInput(FindModalInput("Niveli"), "A1");

        Log("Kliko Ruaj gjuhe");
        SafeClick(By.CssSelector("button.ealb-add-button-modal"));
        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
            By.CssSelector(".custom-modal-content")));
        Thread.Sleep(1000);

        Log("Ploteso aftesite e detyrueshme");
        FillInput(FindTextareaByLabel("Aftësi organizative dhe kompetenca"), "test");
        FillInput(FindTextareaByLabel("Aftësi teknike dhe kompetenca"), "test");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("TË DHËNAT E APLIKIMIT");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E APLIKIMIT"));

        Log("Assert kohëzgjatja Step 2");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 3 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert checkboxet e certifikimit");
        AssertUnchecked("chb1",
            "Supervizimin e aktiviteteve të ndryshme në fushën e pyjeve dhe kullotave");
        AssertUnchecked("chb2",
            "Hartimin e metodikave, udhëzuesve/manualeve");
        AssertUnchecked("chb3",
            "Hartimin e projekteve të tilla si, projekte komplekse");
        AssertUnchecked("chb4",
            "Zbatimin e projekteve në fushën e pyjeve");

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko checkboxet e certifikimit");
        ClickCheckbox("chb1");
        ClickCheckbox("chb2");
        ClickCheckbox("chb3");
        ClickCheckbox("chb4");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 3");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 3 hapa, te gjithe aktiv");
        AssertSteps(3);

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h5[contains(.,'Dokumenta që ngarkohen nga Aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h5[contains(.,'Dokumenta që ngarkohen nga nëpunësit e administratës publike')]"))
            .Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Dëshmia e Penalitetit')]")).Displayed, Is.True);

        Log("Assert document-upload");
        AssertDocumentUpload("diplomaFileUpload", "Tituj/diploma");
        AssertDocumentUpload("workbookFileUpload", "Libreza e punës (e skanuar)");
        AssertDocumentUpload("paymentFileUpload", "Mandat pagesa");

        Log("Assert checkboxet e autorizimit");
        AssertUnchecked("authorizeEmployee",
            "Autorizoj nëpunësin e administratës të aksesojë direkt të dhënat e mia nga Gjendja Civile.");
        AssertUnchecked("agreeToDocCollection",
            "Mbledhja e dokumentacionit shoqërues");
        AssertUnchecked("declaration1",
            "Deklaroj se angazhohem të njoh e të zbatoj ligjet");
        AssertUnchecked("declaration2",
            "Deklaroj vërtetësinë e dokumenteve provuese");
        AssertUnchecked("declaration3",
            "Deklaroj se nuk jam i punësuar në rolin e drejtuesit teknik");

        Log("Assert butonat e navigimit Step 3");
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
        UploadDocument("diplomaFileUpload", DocumentPath);
        UploadDocument("workbookFileUpload", DocumentPath);
        UploadDocument("paymentFileUpload", DocumentPath);

        Log("Kliko checkboxet e autorizimit");
        ClickCheckbox("authorizeEmployee");
        ClickCheckbox("agreeToDocCollection");
        ClickCheckbox("declaration1");
        ClickCheckbox("declaration2");
        ClickCheckbox("declaration3");
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
                "h4.px-4.pb-4.text-uppercase, h4.px-4.pb-4, h4.text-uppercase"));
            foreach (var title in titles)
            {
                string actual = title.Text.Trim().ToUpperInvariant();
                if (actual == expectedUpper || actual.StartsWith(expectedUpper))
                    return title;
            }
            return null;
        });
    }

    private IWebElement FindInputByLabel(string labelText)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//label[contains(normalize-space(),'{labelText}')]/following-sibling::input[contains(@class,'ealb-input')][1]")));
    }

    private IWebElement FindTextareaByLabel(string labelText)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//label[contains(normalize-space(),'{labelText}')]/following-sibling::textarea[contains(@class,'ealb-input')][1]")));
    }

    private void AssertReadonlyInput(string labelText, string expectedValue)
    {
        IWebElement field = FindInputByLabel(labelText);
        Assert.That(field.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(field.GetAttribute("readonly"), Is.Not.Null);
    }

    private IWebElement WaitForModal(string expectedTitle)
    {
        IWebElement modal = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector(".custom-modal-content")));
        IWebElement title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector(".custom-modal-title")));
        Assert.That(title.Text.Trim(), Is.EqualTo(expectedTitle));
        return modal;
    }

    private IWebElement FindModalInput(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//div[contains(@class,'custom-modal-content')]//label[contains(.,'{labelPart}')]/following::input[contains(@class,'custom-modal-input')][1]")));
    }

    private void FillModalDate(string labelPart, string displayDate)
    {
        string[] parts = displayDate.Split('.');
        int day = int.Parse(parts[0]);
        int month = int.Parse(parts[1]);
        int year = int.Parse(parts[2]);

        IWebElement input = FindModalInput(labelPart);
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
                string current = FindModalInput(labelPart).GetAttribute("value") ?? string.Empty;
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

    private void AssertUnchecked(string id, string labelPart)
    {
        IWebElement checkbox = wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
        Assert.That(checkbox.GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(checkbox.Selected, Is.False);
        IWebElement label = driver.FindElement(By.CssSelector($"label[for='{id}']"));
        Assert.That(label.Text.Trim(), Does.Contain(labelPart));
    }

    private void ClickCheckbox(string id)
    {
        IWebElement checkbox = wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            checkbox);
        Thread.Sleep(300);
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", checkbox);
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {
        Assert.That(driver.FindElement(
            By.XPath($"//span[contains(@class,'form-label') and contains(normalize-space(),'{documentTitle}')]"))
            .Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-15389"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("5"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("52428800"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf,.jpg,.jpeg,.png"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Klikoni për të ngarkuar dokumentin"));

        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Klikoni për të ngarkuar dokumentin"));
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
