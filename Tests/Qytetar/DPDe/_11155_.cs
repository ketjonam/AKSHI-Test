using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.DPDe;

[Category("DPDe")]
[Category("11155")]
public class _11155_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "11155";
    protected override string? ServiceTitle => "LejeDrejtimiPerMjetetLundruese";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void LejeDrejtimiPerMjetetLundruese()
    {




        Log("Assert Title Step 1");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//h4[normalize-space()='Aplikim për']")));
        Assert.That(Step1Title.Text.Trim(), Is.EqualTo("Aplikim për"));

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 4 hapa, hapi i pare aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(4));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));
        for (int i = 1; i < steps.Count; i++)
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert opsionet e arsyes se aplikimit");
        AssertRadioOption("firstTime", "firstTime", "Herë të parë");
        AssertRadioOption("categoryChange", "categoryChange", "Ndryshim të kategorisë");
        AssertRadioOption("sailingLicenceRenew", "sailingLicenceRenew", "Rinovim leje drejtimi");
        AssertRadioOption("personalDataChange", "personalDataChange", "Ndryshim të dhënash personale");
        AssertRadioOption("loss", "loss", "Humbje");
        AssertRadioOption("damage", "damage", "Dëmtim");

        Log("Assert butonat e navigimit");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));
        Assert.That(continueBtn.GetAttribute("disabled"), Is.Not.Null);

        Log("Zgjidh Herë të parë");
        SelectRadioById("firstTime");
        Assert.That(driver.FindElement(By.Id("firstTime")).Selected, Is.True);
        Assert.That(driver.FindElement(By.Id("categoryChange")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("sailingLicenceRenew")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("personalDataChange")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("loss")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("damage")).Selected, Is.False);

        Log("Assert Vazhdo behet i aktivizuar");
        continueBtn = wait.Until(d =>
        {
            var btn = d.FindElement(By.CssSelector("button.ealb-btn-continue"));
            return btn.GetAttribute("disabled") == null ? btn : null;
        });
        Assert.That(continueBtn.GetAttribute("disabled"), Is.Null);

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Title Step 2");
        IWebElement Step2Title = WaitForStepTitle("TË DHËNAT E APLIKANTIT");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E APLIKANTIT"));

        Log("Assert tooltip i te dhenave te aplikantit");
        IWebElement tooltip = wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector("h4.text-uppercase span[data-bs-toggle='tooltip']")));
        Assert.That(tooltip.GetAttribute("title"),
            Is.EqualTo("Të dhënat e aplikantit plotësohen nga identifikimi juaj në e-albania"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 4 hapa, dy te paret aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(4));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        for (int i = 2; i < steps.Count; i++)
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert te dhenat e aplikantit te para-plotesuara dhe readonly");
        AssertReadOnlyValue("Nid", Settings.Qytetar.Username);
        AssertReadOnlyValue("Emri", "Ketjona");
        AssertReadOnlyValue("Mbiemri", "Mema");
        AssertReadOnlyValue("Atësia", "Mersin");
        AssertReadOnlyValue("Datëlindja", "28.07.1995");
        AssertReadOnlyValue("Gjinia", "Femër");
        AssertReadOnlyValue("Vendlindja", "Kavajë");
        AssertReadOnlyValue("Shtetësia", "Shqiptare");

        Log("Assert butonat e navigimit Step 2");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));
        Assert.That(continueBtn.GetAttribute("disabled"), Is.Null);

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Title Step 3");
        IWebElement Step3Title = WaitForStepTitle("INFORMACIONI I KONTAKTIT TË APLIKANTIT");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("INFORMACIONI I KONTAKTIT TË APLIKANTIT"));

        Log("Assert tooltip i informacionit te kontaktit");
        IWebElement contactTooltip = wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector("h4.text-uppercase span[data-bs-toggle='tooltip']")));
        Assert.That(contactTooltip.GetAttribute("title"),
            Is.EqualTo("Informacioni i dhënë duhet të jetë i saktë pasi ju do të kontaktoheni nëpërmjet adresës elektronike dhe numrit të telefonit"));

        Log("Assert kohëzgjatja Step 3");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 4 hapa, tre te paret aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(4));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[3].GetAttribute("class"), Does.Not.Contain("active"));
        Assert.That(steps[3].GetAttribute("class"), Does.Contain("no-click"));

        Log("Assert te dhenat e kontaktit te para-plotesuara");
        AssertReadOnlyValue("Qyteti", "KAVAJË");
        AssertReadOnlyValue("Rrethi", "KAVAJË");
        AssertReadOnlyDisabledValue("Nr. Tel. Cel.", "0676041404");
        AssertReadOnlyDisabledValue("Email", "ketjona.mema@kreatx.com");
        AssertReadOnlyDisabledValue("Adresa",
            "THABIT REXHA 04040156; Nd. 6; H. 2; ; KAVAJË; KAVAJË; 2501; KAVAJË");

        Log("Assert tooltip i Nr. Tel. Cel.");
        IWebElement telCelTooltip = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//form//label[contains(.,'Nr. Tel. Cel.')]//span[@data-bs-toggle='tooltip']")));
        Assert.That(telCelTooltip.GetAttribute("title"),
            Is.EqualTo("Numri i celularit merret nga të dhënat e llogarisë që jeni regjistruar në e-Albania. "));

        Log("Assert Email eshte i tipit text, readonly dhe disabled");
        IWebElement emailInput = FindInputByLabel("Email");
        Assert.That(emailInput.GetAttribute("type"), Is.EqualTo("text"));
        Assert.That(emailInput.GetAttribute("name"), Is.EqualTo("email"));

        Log("Assert Kodi Postar dhe Nr.Tel Fiks jane te editueshme");
        IWebElement kodiPostar = FindInputByLabel("Kodi Postar");
        Assert.That(kodiPostar.GetAttribute("name"), Is.EqualTo("postalCodeId"));
        Assert.That(kodiPostar.GetAttribute("disabled"), Is.Null);
        Assert.That(kodiPostar.GetAttribute("readonly"), Is.Null);
        Assert.That(kodiPostar.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement telFiks = FindInputByLabel("Nr.Tel Fiks");
        Assert.That(telFiks.GetAttribute("type"), Is.EqualTo("text"));
        Assert.That(telFiks.GetAttribute("name"), Is.EqualTo("nrTel"));
        Assert.That(telFiks.GetAttribute("disabled"), Is.Null);
        Assert.That(telFiks.GetAttribute("readonly"), Is.Null);
        Assert.That(telFiks.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Ploteso Kodi Postar dhe Nr.Tel Fiks");
        FillInput(kodiPostar, "2501");
        FillInput(telFiks, "055220000");
        Assert.That(FindInputByLabel("Kodi Postar").GetAttribute("value").Trim(), Is.EqualTo("2501"));
        Assert.That(FindInputByLabel("Nr.Tel Fiks").GetAttribute("value").Trim(), Is.EqualTo("055220000"));

        Log("Assert butonat e navigimit Step 3");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));
        Assert.That(continueBtn.GetAttribute("disabled"), Is.Null);

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Title Step 4");
        IWebElement Step4Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 4");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 4 hapa, te gjithe aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(4));
        foreach (var step in steps)
            Assert.That(step.GetAttribute("class"), Does.Contain("active"));

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës')]")).Displayed, Is.True);

        Log("Assert document-upload Mandatpagesa");
        AssertDocumentUpload(
            "fuMandatPagesaUpload",
            "Mandatpagesa + faturën për pajisja me leje drejtimi për mjete lundruese.");

        Log("Assert linkun e shkarkimit te fatures");
        IWebElement fatureLink = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("a[href='/service/Documents_11155/11155_fature.pdf']")));
        Assert.That(fatureLink.Text.Trim(), Is.EqualTo("Shkarkoni këtu"));
        Assert.That(fatureLink.GetAttribute("download"), Is.EqualTo("11155_fature.pdf"));

        Log("Assert document-upload Fotografi");
        AssertDocumentUpload(
            "fuFotografiUpload",
            "Fotografi (4x5 cm për dokument)");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Fotografi')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert document-upload Raport i aftësisë");
        AssertDocumentUpload(
            "fuRaportAftesieUpload",
            "Raport i aftësisë për punë");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Raport i aftësisë për punë')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert dokumentet e administrates");
        Assert.That(driver.FindElement(
            By.XPath("//*[contains(.,'Certifikatat e lëshuara nga qëndra trajnimit dhe e firmosur nga personi i autorizuar nga DPD')]")).Displayed, Is.True);

        Log("Assert checkbox i pranimit eshte i pazgjedhur");
        IWebElement agreementCheckbox = wait.Until(ExpectedConditions.ElementExists(By.Id("agreementCheckbox")));
        Assert.That(agreementCheckbox.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='agreementCheckbox']")).Text.Trim(),
            Is.EqualTo("Mbledhja e dokumentacionit shoqërues të mësipërm që më parë ishte detyrim të dorëzohej në zyrat e shtetit nga vetë aplikanti, tani është detyrë e nëpunësit të administratës ndaj qytetarit. Me klikimin e këtij butoni, ju bini dakord që këto dokumente të sigurohen për ju nga nëpunësi i administratës."));

        Log("Assert butonat e navigimit Step 4");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(dergoBtn.Text.Trim(), Does.Contain("Dërgo"));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Kliko Dergo pa ngarkuar dokumentet e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(1000);
        Assert.That(WaitForStepTitle("DOKUMENTACIONI").Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DOKUMENTACIONI"));

        string documentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";

        Log("Ngarko dokumentet e detyrueshme");
        UploadDocument("fuFotografiUpload", documentPath);
        UploadDocument("fuRaportAftesieUpload", documentPath);

        Log("Zgjidh pranimin e kushteve");
        ClickCheckbox("agreementCheckbox");
        Assert.That(driver.FindElement(By.Id("agreementCheckbox")).Selected, Is.True);

        //Log("Kliko Dergo");
        //SafeClick(By.CssSelector("button.ealb-btn-continue"));
        //Thread.Sleep(5000);

        Log("TEST PASSED");
    }

    private void SelectRadioById(string radioId)
    {

        IWebElement input = wait.Until(ExpectedConditions.ElementExists(By.Id(radioId)));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            input
        );
        Thread.Sleep(300);

        if (!input.Selected)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(@"
                const el = arguments[0];
                el.click();
                el.dispatchEvent(new Event('input', { bubbles: true }));
                el.dispatchEvent(new Event('change', { bubbles: true }));
            ", input);
        }

        wait.Until(d => d.FindElement(By.Id(radioId)).Selected);
        Thread.Sleep(300);
    }

    private void AssertRadioOption(string radioId, string expectedValue, string expectedLabel)
    {

        IWebElement radio = wait.Until(ExpectedConditions.ElementExists(By.Id(radioId)));
        Assert.That(radio.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(radio.GetAttribute("name"), Is.EqualTo("arsyeAplikimi"));
        Assert.That(radio.GetAttribute("value"), Is.EqualTo(expectedValue));
        Assert.That(radio.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector($"label[for='{radioId}']")).Text.Trim(),
            Is.EqualTo(expectedLabel));
    }

    private IWebElement FindInputByLabel(string labelPart)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//div[@id='root']//form//label[contains(.,'{labelPart}')]/following-sibling::*[self::input or self::textarea]")));
    }

    private IWebElement WaitForStepTitle(string expectedUpper)
    {

        return wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h5.text-uppercase, h4.text-uppercase, h4.ealb-header-text"));
            foreach (var title in titles)
            {
                if (title.Text.Trim().ToUpperInvariant() == expectedUpper)
                    return title;
            }
            return null;
        });
    }

    private void AssertReadOnlyValue(string labelPart, string expectedValue)
    {

        IWebElement input = FindInputByLabel(labelPart);
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(input.GetAttribute("readonly"), Is.Not.Null);
    }

    private void AssertReadOnlyDisabledValue(string labelPart, string expectedValue)
    {

        IWebElement input = FindInputByLabel(labelPart);
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(input.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(input.GetAttribute("disabled"), Is.Not.Null);
    }

    private void BlurActiveElement()
    {

        try
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "if(document.activeElement){document.activeElement.blur();}"
            );
        }
        catch (Exception ex)
        {
            Log("BlurActiveElement error: " + ex.Message);
        }
    }

    private void FillInput(IWebElement input, string value)
    {

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            input
        );

        Thread.Sleep(400);

        try
        {
            input.Click();
            Thread.Sleep(200);
            input.Clear();
            input.SendKeys(value);
        }
        catch (ElementClickInterceptedException)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].focus(); arguments[0].value = '';",
                input
            );
            input.SendKeys(value);
        }

        BlurActiveElement();
        Thread.Sleep(300);
    }

    private void ClickCheckbox(string checkboxId)
    {

        IWebElement input = wait.Until(ExpectedConditions.ElementExists(By.Id(checkboxId)));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            input
        );
        Thread.Sleep(300);

        if (!input.Selected)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(@"
                const el = arguments[0];
                el.click();
                el.dispatchEvent(new Event('input', { bubbles: true }));
                el.dispatchEvent(new Event('change', { bubbles: true }));
            ", input);
        }

        wait.Until(d => d.FindElement(By.Id(checkboxId)).Selected);
        Thread.Sleep(300);
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {

        Assert.That(driver.FindElement(
            By.XPath($"//*[contains(normalize-space(),'{documentTitle}')]")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-11155"));
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
            Is.EqualTo("Formatet e lejuara: PDF, JPG, JPEG, PNG. Madhesia maksimale: 5MB."));
    }

    private void UploadDocument(string uploadId, string filePath)
    {

        Assert.That(File.Exists(filePath), Is.True, "File nuk ekziston: " + filePath);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            docUpload
        );
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