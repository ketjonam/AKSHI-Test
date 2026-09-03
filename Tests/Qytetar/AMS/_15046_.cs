using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.AMS;

[Category("AMS")]
[Category("15046")]
public class _15046_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "15046";
    protected override string? ServiceTitle => "LejeKalimiKufitar";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName = "Leje Kalimi Kufitar Lokal";
    private const string ExpectedAddress =
        "FROSINA PLAKU; Nd. 88; H. 2; Ap. 9; NJËSIA ADMINISTRATIVE NR. 7; NJËSIA BASHKIAKE NR. 7; 1023; TIRANË";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";

    [Test]
    public void LejeKalimiKufitar()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 6 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert fusha e tipit te aplikimit Step 1");
        IWebElement applicationTypeLabel = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("label[for='applicationType']")));
        Assert.That(applicationTypeLabel.Text, Does.Contain("Përzgjidhni tipin e aplikimit"));
        Assert.That(applicationTypeLabel.Text, Does.Contain("*"));

        IWebElement applicationType = wait.Until(ExpectedConditions.ElementIsVisible(
            By.Id("applicationType")));
        var applicationTypeSelect = new SelectElement(applicationType);
        Assert.That(applicationTypeSelect.Options.Count, Is.EqualTo(2));
        Assert.That(applicationTypeSelect.Options[0].GetAttribute("value"),
            Is.EqualTo("Aplikim për veten"));
        Assert.That(applicationTypeSelect.Options[1].GetAttribute("value"),
            Is.EqualTo("Personi i autorizuar"));
        Assert.That(applicationTypeSelect.SelectedOption.GetAttribute("value"),
            Is.EqualTo("Aplikim për veten"));

        Log("Assert butonat e navigimit Step 1");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("TË DHËNAT E APLIKANTIT");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E APLIKANTIT"));

        Log("Assert kohëzgjatja Step 2");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 6 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert te dhenat e aplikantit");
        AssertReadonlyField("NID", Settings.Qytetar.Username);
        AssertReadonlyField("Emri", "Katerina");
        AssertReadonlyField("Mbiemri", "Jançe");
        AssertReadonlyField("Atësia", "Foti");
        AssertReadonlyField("Nr. Tel. Cel.", "+355697008820");
        AssertReadonlyField("Datëlindja", "13.04.1993");
        AssertReadonlyField("Gjinia", "Femër");
        AssertReadonlyField("Email", "katerina.jance@kreatx.com");
        AssertReadonlyField("Vendbanimi", ExpectedAddress);

        IWebElement email = FindControlByLabel("Email");
        Assert.That(email.GetAttribute("type"), Is.EqualTo("email"));

        IWebElement shtetiLindjes = FindControlByLabel("Shteti i lindjes");
        Assert.That(shtetiLindjes.GetAttribute("readonly"), Is.Null);
        Assert.That(shtetiLindjes.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement shtetesia = FindControlByLabel("Shtetësia");
        Assert.That(shtetesia.GetAttribute("readonly"), Is.Null);
        Assert.That(shtetesia.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Ploteso shtetin e lindjes dhe shtetesine");
        FillInput(shtetiLindjes, "Shqipëri");
        FillInput(shtetesia, "Shqiptare");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("TË DHËNAT E APLIKIMIT");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E APLIKIMIT"));

        Log("Assert kohëzgjatja Step 3");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 6 hapa, tre te paret aktiv");
        AssertSteps(3);

        Log("Assert fushat e te dhenave te aplikimit");
        IWebElement tipiAplikimit = FindControlByLabel("Tipi i aplikimit");
        var tipiSelect = new SelectElement(tipiAplikimit);
        Assert.That(tipiSelect.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(tipiSelect.Options.Count, Is.EqualTo(3));
        Assert.That(tipiSelect.Options[1].GetAttribute("value"),
            Is.EqualTo("Leje për herë të parë / A first local border traffic permit"));
        Assert.That(tipiSelect.Options[1].Text.Trim(), Is.EqualTo("Leje për herë të parë"));
        Assert.That(tipiSelect.Options[2].GetAttribute("value"),
            Is.EqualTo("Rinovim leje / Renewed local border traffic permit"));
        Assert.That(tipiSelect.Options[2].Text.Trim(), Is.EqualTo("Rinovim leje"));

        IWebElement motivi = FindControlByLabel("Motivi i kalimit lokal të kufirit");
        var motiviSelect = new SelectElement(motivi);
        Assert.That(motiviSelect.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(motiviSelect.Options.Any(o => o.GetAttribute("value") == "Social"), Is.True);
        Assert.That(motiviSelect.Options.Any(o => o.GetAttribute("value") == "Kulturor"), Is.True);
        Assert.That(motiviSelect.Options.Any(o => o.GetAttribute("value") == "Familjar"), Is.True);
        Assert.That(motiviSelect.Options.Any(o => o.GetAttribute("value") == "Arsimor"), Is.True);
        Assert.That(motiviSelect.Options.Any(o => o.GetAttribute("value") == "Shëndetësor"), Is.True);
        Assert.That(motiviSelect.Options.Any(o => o.GetAttribute("value") == "Ekonomik"), Is.True);

        IWebElement zonaBashkia = FindControlByLabel("Zona kufitare - Bashkia");
        var zonaBashkiaSelect = new SelectElement(zonaBashkia);
        Assert.That(zonaBashkiaSelect.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(zonaBashkiaSelect.Options.Any(o => o.GetAttribute("value") == "KUKËS"), Is.True);
        Assert.That(zonaBashkiaSelect.Options.Any(o => o.GetAttribute("value") == "KRUMË"), Is.True);
        Assert.That(zonaBashkiaSelect.Options.Any(o => o.GetAttribute("value") == "BAJRAM CURRI"), Is.True);

        IWebElement zonaQyteti = FindControlByLabel("Zona kufitare - Qyteti/Fshati");
        Assert.That(zonaQyteti.GetAttribute("disabled"), Is.Not.Null);

        IWebElement menyraKalimit = FindControlByLabel("Mënyra e kalimit");
        var menyraSelect = new SelectElement(menyraKalimit);
        Assert.That(menyraSelect.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(menyraSelect.Options.Any(o => o.GetAttribute("value") == "Këmbësor"), Is.True);
        Assert.That(menyraSelect.Options.Any(o => o.GetAttribute("value") == "Me automjet"), Is.True);

        IWebElement ndalimHyrje = FindControlByLabel("A keni ndalim hyrje në territorin e RSH?");
        Assert.That(ndalimHyrje.TagName.ToLowerInvariant(), Is.EqualTo("textarea"));
        Assert.That(ndalimHyrje.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Assert.That(WaitForStepTitle("TË DHËNAT E APLIKIMIT").Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E APLIKIMIT"));
        AssertFieldError("Përzgjidhni një vlerë për të vazhduar");

        Log("Ploteso te dhenat e aplikimit");
        SelectByValue(FindControlByLabel("Tipi i aplikimit"),
            "Rinovim leje / Renewed local border traffic permit");
        SelectByValue(FindControlByLabel("Motivi i kalimit lokal të kufirit"), "Kulturor");
        SelectByValue(FindControlByLabel("Zona kufitare - Bashkia"), "BAJRAM CURRI");
        SelectFirstWhenEnabled("Zona kufitare - Qyteti/Fshati", preferredText: "Viçidol");
        SelectByValue(FindControlByLabel("Mënyra e kalimit"), "Këmbësor");
        FillInput(FindControlByLabel("A keni ndalim hyrje në territorin e RSH?"), "JO");

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 4 Title");
        IWebElement Step4Title = WaitForStepTitle("ADRESA E PLOTË E APLIKANTIT");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("ADRESA E PLOTË E APLIKANTIT"));

        Log("Assert kohëzgjatja Step 4");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 6 hapa, kater te paret aktiv");
        AssertSteps(4);

        Log("Assert fushat e adreses");
        IWebElement bashkia = FindControlByLabel("Bashkia", exact: true);
        var bashkiaSelect = new SelectElement(bashkia);
        Assert.That(bashkiaSelect.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(bashkiaSelect.Options.Any(o => o.GetAttribute("value") == "50" && o.Text.Trim() == "TIRANË"),
            Is.True);

        AssertDisabledSelect("Njësia administrative");
        AssertDisabledSelect("Fshati/Qyteti");
        AssertDisabledSelect("Rruga");
        AssertDisabledSelect("Numri i ndërtesës");
        AssertDisabledSelect("Hyrja");
        AssertDisabledSelect("Apartamenti");

        IWebElement kodiPostar = FindControlByLabel("Kodi postar");
        Assert.That(kodiPostar.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(kodiPostar.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 4");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Assert.That(WaitForStepTitle("ADRESA E PLOTË E APLIKANTIT").Text.Trim().ToUpperInvariant(),
            Is.EqualTo("ADRESA E PLOTË E APLIKANTIT"));
        AssertFieldError("Përzgjidhni një vlerë për të vazhduar");

        Log("Ploteso te dhenat e adreses se aplikantit");
        SelectByValue(FindControlByLabel("Bashkia", exact: true), "50");
        SelectFirstWhenEnabled("Njësia administrative");
        SelectFirstWhenEnabled("Fshati/Qyteti");
        SelectFirstWhenEnabled("Rruga");
        SelectFirstWhenEnabled("Numri i ndërtesës");
        SelectFirstWhenEnabled("Hyrja");
        SelectFirstWhenEnabled("Apartamenti");

        Log("Kliko Vazhdo Step 4");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 5 Title");
        IWebElement Step5Title = WaitForStepTitle("AFATI I LEJES");
        Assert.That(Step5Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("AFATI I LEJES"));

        Log("Assert kohëzgjatja Step 5");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 6 hapa, pese te paret aktiv");
        AssertSteps(5);

        Log("Assert afati i lejes");
        IWebElement afati = FindControlByLabel("Unë aplikoj për leje kalimi me afat");
        Assert.That(afati.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(afati.GetAttribute("class"), Does.Contain("disabled-style"));
        var afatiSelect = new SelectElement(afati);
        Assert.That(afatiSelect.SelectedOption.GetAttribute("value"), Is.EqualTo("5"));
        Assert.That(afatiSelect.SelectedOption.Text.Trim(), Is.EqualTo("5 vjeçare"));

        Log("Assert butonat e navigimit Step 5");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 5");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 6 Title");
        IWebElement Step6Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step6Title.Text.Trim().ToUpperInvariant(), Does.StartWith("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 6");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 6 hapa, te gjithe aktiv");
        AssertSteps(6);

        Log("Assert seksioni i dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//span[contains(.,'Dokumenta që ngarkohen nga Aplikanti')]")).Displayed, Is.True);

        Log("Assert document-upload");
        AssertDocumentUpload("fuIdCardUpload",
            "Fotokopja e dokumentit të vlefshëm të udhëtimit (pasaportë, kartë identiteti)");
        AssertDocumentUpload("fuPhotoUpload",
            "Një fotografi (47mmX36mm), të bërë jo më përpara se 6 muaj nga data e aplikimit");
        AssertDocumentUpload("fuStatementUpload",
            "Deklarata individuale mbi motivin e kalimit dhe qëndrimit në Zonën Kufitare, Shqipëri");
        AssertDocumentUpload("fuDeclarationUpload", "Deklaratë/autorizimi");
        AssertDocumentUpload("fuOtherDocsUpload", "Dokumente të tjera");

        Log("Assert checkboxet");
        AssertUnchecked("authorizeEmployee",
            "Autorizoj nëpunësin e administratës të aksesojë direkt të dhënat e mia nga Gjendja Civile.");
        AssertUnchecked("declarationCheckbox",
            "Unë po aplikoj për një leje të trafikut kufitar lokal");

        Log("Assert butonat e navigimit Step 6");
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
        UploadDocument("fuIdCardUpload", DocumentPath);
        UploadDocument("fuPhotoUpload", DocumentPath);
        UploadDocument("fuStatementUpload", DocumentPath);

        Log("Kliko checkboxet");
        ClickCheckbox("authorizeEmployee");
        ClickCheckbox("declarationCheckbox");
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

    private void AssertDuration(string expected)
    {
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain(expected));
    }

    private void AssertSteps(int activeCount)
    {
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(6));
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
                "h4.px-4.pb-4.text-uppercase, h4.px-4.pb-1.text-uppercase, h4.px-4.pb-4, h4.text-uppercase"));
            foreach (var title in titles)
            {
                string actual = title.Text.Trim().ToUpperInvariant();
                if (actual == expectedUpper || actual.StartsWith(expectedUpper))
                    return title;
            }
            return null;
        });
    }

    private static By ControlByLabel(string labelPart, bool exact = false)
    {
        string labelPred = exact
            ? $"starts-with(normalize-space(),'{labelPart}')"
            : $"contains(normalize-space(),'{labelPart}')";
        return By.XPath(
            $"//form//label[{labelPred}]/following-sibling::*[self::input or self::select or self::textarea][1]");
    }

    private IWebElement FindControlByLabel(string labelPart, bool exact = false)
    {
        return wait.Until(ExpectedConditions.ElementExists(ControlByLabel(labelPart, exact)));
    }

    private void AssertReadonlyField(string labelPart, string expectedValue)
    {
        IWebElement field = FindControlByLabel(labelPart);
        Assert.That(field.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(field.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
    }

    private void AssertDisabledSelect(string labelPart)
    {
        IWebElement field = FindControlByLabel(labelPart);
        Assert.That(field.GetAttribute("disabled"), Is.Not.Null);
        var select = new SelectElement(field);
        Assert.That(select.Options.Count, Is.EqualTo(1));
        Assert.That(select.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
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

    private void SelectFirstWhenEnabled(string labelPart, string? preferredText = null)
    {
        By locator = ControlByLabel(labelPart);
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
        Assert.That(options, Is.Not.Empty, $"Select '{labelPart}' nuk ka opsione te disponueshme");

        var preferred = preferredText == null
            ? null
            : options.FirstOrDefault(o =>
                o.Text.Trim().Equals(preferredText, StringComparison.OrdinalIgnoreCase)
                || o.GetAttribute("value").Trim().Equals(preferredText, StringComparison.OrdinalIgnoreCase));
        string value = (preferred ?? options[0]).GetAttribute("value");
        dropdown.SelectByValue(value);
        Thread.Sleep(1000);
    }

    private void AssertUnchecked(string id, string labelPart)
    {
        IWebElement checkbox = wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
        Assert.That(checkbox.GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(checkbox.Selected, Is.False);
        IWebElement label = driver.FindElement(
            By.XPath($"//input[@id='{id}']/ancestor::div[contains(@class,'d-flex')][1]/label"));
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
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-15046"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("5"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("52428800"));
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
