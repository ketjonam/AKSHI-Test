using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.DPDe;

[Category("DPDe")]
[Category("14934")]
public class _14934_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "14934";
    protected override string? ServiceTitle => "CertifikateKompetence";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void CertifikateKompetence()
    {




        Log("Assert Title Step 1");
        IWebElement Step1Title = WaitForStepTitle("TË DHËNAT E APLIKANTIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E APLIKANTIT"));

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

        Log("Assert 3 hapa, hapi i pare aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(3));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));
        for (int i = 1; i < steps.Count; i++)
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert te dhenat e aplikantit te para-plotesuara dhe readonly");
        AssertReadOnlyValue("Nid", Settings.Qytetar.Username);
        Assert.That(FindInputByName("nid").GetAttribute("id"), Is.EqualTo("nid"));
        AssertReadOnlyValue("Emri", "Ketjona");
        AssertReadOnlyValue("Mbiemri", "Mema");
        AssertReadOnlyValue("Datëlindja", "28.07.1995");
        AssertReadOnlyValue("Gjinia", "Femër");
        Assert.That(FindInputByName("gjinia").GetAttribute("type"), Is.EqualTo("text"));
        AssertReadOnlyValue("Vendlindja", "KAVAJË");
        AssertReadOnlyValue("Atësia", "Mersin");
        AssertReadOnlyValue("Shtetësia", "Shqiptare");
        AssertReadOnlyValue("Qyteti", "KAVAJË");
        AssertReadOnlyValue("Rrethi", "KAVAJË");
        AssertReadOnlyValue("Nr. Tel Cel.", "0676041404");
        Assert.That(FindInputByName("telCel").GetAttribute("name"), Is.EqualTo("telCel"));
        AssertReadOnlyValue("Email", "ketjona.mema@kreatx.com");
        Assert.That(FindInputByLabel("Email").GetAttribute("type"), Is.EqualTo("email"));
        AssertReadOnlyDisabledValue("Adresa",
            "THABIT REXHA 04040156; Nd. 6; H. 2; ; KAVAJË; KAVAJË; 2501; KAVAJË");

        Log("Assert Kodi Postar dhe Nr.Tel Fiks jane te editueshme");
        IWebElement kodiPostar = FindInputByLabel("Kodi Postar");
        Assert.That(kodiPostar.GetAttribute("name"), Is.EqualTo("kodiPostar"));
        Assert.That(kodiPostar.GetAttribute("disabled"), Is.Null);
        Assert.That(kodiPostar.GetAttribute("readonly"), Is.Null);
        Assert.That(kodiPostar.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement telFiks = FindInputByLabel("Nr.Tel Fiks");
        Assert.That(telFiks.GetAttribute("name"), Is.EqualTo("telFiks"));
        Assert.That(telFiks.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(telFiks.GetAttribute("disabled"), Is.Null);
        Assert.That(telFiks.GetAttribute("readonly"), Is.Null);
        Assert.That(telFiks.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Ploteso Kodi Postar dhe Nr.Tel Fiks");
        FillInput(kodiPostar, "2501");
        FillInput(telFiks, "055220000");
        Assert.That(FindInputByLabel("Kodi Postar").GetAttribute("value").Trim(), Is.EqualTo("2501"));
        Assert.That(FindInputByLabel("Nr.Tel Fiks").GetAttribute("value").Trim(), Is.EqualTo("055220000"));

        Log("Assert butonat e navigimit");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));
        Assert.That(continueBtn.GetAttribute("disabled"), Is.Null);

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Title Step 2");
        IWebElement Step2Title = WaitForStepTitle("INFORMACION SPECIFIK MBI APLIKIMIN");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("INFORMACION SPECIFIK MBI APLIKIMIN"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

        Log("Assert 3 hapa, dy te paret aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(3));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Not.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Contain("no-click"));

        Log("Assert label-at e detyrueshme kane yll");
        Assert.That(driver.FindElement(By.XPath("//div[@id='root']//label[contains(.,'Gjatësia (cm)')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//div[@id='root']//label[contains(.,'Ngjyra e syve')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//div[@id='root']//label[contains(.,'Shenja të veçanta')]")).Text, Does.Contain("*"));

        Log("Assert fushat e informacionit specifik jane boshe dhe te editueshme");
        IWebElement gjatesia = FindInputByName("gjatesia");
        Assert.That(gjatesia.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(gjatesia.GetAttribute("id"), Is.EqualTo("gjatesia"));
        Assert.That(gjatesia.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(gjatesia.GetAttribute("disabled"), Is.Null);
        Assert.That(gjatesia.GetAttribute("readonly"), Is.Null);

        IWebElement ngjyraSyve = FindInputByName("ngjyraSyve");
        Assert.That(ngjyraSyve.GetAttribute("type"), Is.EqualTo("text"));
        Assert.That(ngjyraSyve.GetAttribute("id"), Is.EqualTo("ngjyraSyve"));
        Assert.That(ngjyraSyve.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(ngjyraSyve.GetAttribute("disabled"), Is.Null);
        Assert.That(ngjyraSyve.GetAttribute("readonly"), Is.Null);

        IWebElement shenjaTeVecanta = FindInputByName("shenjaTeVecanta");
        Assert.That(shenjaTeVecanta.GetAttribute("type"), Is.EqualTo("text"));
        Assert.That(shenjaTeVecanta.GetAttribute("id"), Is.EqualTo("shenjaTeVecanta"));
        Assert.That(shenjaTeVecanta.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(shenjaTeVecanta.GetAttribute("disabled"), Is.Null);
        Assert.That(shenjaTeVecanta.GetAttribute("readonly"), Is.Null);

        Log("Assert butonat e navigimit Step 2");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));
        Assert.That(continueBtn.GetAttribute("disabled"), Is.Null);

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error per fushat e detyrueshme");
        AssertRequiredError("Gjatësia (cm)");
        AssertRequiredError("Ngjyra e syve");
        AssertRequiredError("Shenja të veçanta");

        Log("Ploteso informacionin specifik mbi aplikimin");
        FillInput(FindInputByName("gjatesia"), "165");
        FillInput(FindInputByName("ngjyraSyve"), "Kafe");
        FillInput(FindInputByName("shenjaTeVecanta"), "Asnjë");

        Assert.That(FindInputByName("gjatesia").GetAttribute("value").Trim(), Is.EqualTo("165"));
        Assert.That(FindInputByName("ngjyraSyve").GetAttribute("value").Trim(), Is.EqualTo("Kafe"));
        Assert.That(FindInputByName("shenjaTeVecanta").GetAttribute("value").Trim(), Is.EqualTo("Asnjë"));

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Title Step 3");
        IWebElement Step3Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 3");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

        Log("Assert 3 hapa, te gjithe aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(3));
        foreach (var step in steps)
            Assert.That(step.GetAttribute("class"), Does.Contain("active"));

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h5[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h5[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës')]")).Displayed, Is.True);

        Log("Assert document-upload Mandatpagesa");
        AssertDocumentUpload(
            "fuCertifikateUpload",
            "Mandatpagesa + faturën për regjistrim dhe pajisja me Certifikata kompetence");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Mandatpagesa')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert linkun e shkarkimit te fatures");
        IWebElement fatureLink = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("a[href='/service/Documents_14934/Deshmi-Kualifikimi-per-Detaret.pdf']")));
        Assert.That(fatureLink.Text.Trim(), Is.EqualTo("Shkarkoni këtu"));

        Log("Assert document-upload Pasaporte Detare");
        AssertDocumentUpload(
            "fuDiplomaUpload",
            "Pasaportë Detare");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Pasaportë Detare')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert document-upload Diploma profesionale");
        AssertDocumentUpload(
            "fuPasaporteUpload",
            "Diploma profesionale e shkollës(për nivelin menaxherial");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Diploma profesionale')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert document-upload Kopje e Certifikatave (opsional)");
        AssertDocumentUpload(
            "fuMandatpagesaUpload",
            "Kopje e Certifikatave e lëshuar nga qëndra trajnimi");
        Assert.That(driver.FindElements(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Kopje e Certifikatave')]//span[normalize-space()='*']")).Count, Is.EqualTo(0));

        Log("Assert document-upload Fotografi");
        AssertDocumentUpload(
            "fuFotoUpload",
            "Fotografi (4x5 cm për dokument)");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Fotografi')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert nuk nevojitet dokumentacion nga administrata");
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(@class,'text-muted') and contains(.,'Për këtë shërbim nuk nevojitet të sigurohet dokumentacion nga nëpunësi i administratës.')]")).Displayed, Is.True);
        Assert.That(driver.FindElements(By.Id("agreeCheck")).Count, Is.EqualTo(0));
        Assert.That(driver.FindElements(By.Id("consentCheckbox")).Count, Is.EqualTo(0));

        Log("Assert butonat e navigimit Step 3");
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
        UploadDocument("fuCertifikateUpload", documentPath);
        UploadDocument("fuDiplomaUpload", documentPath);
        UploadDocument("fuPasaporteUpload", documentPath);
        UploadDocument("fuFotoUpload", documentPath);

        //Log("Kliko Dergo");
        //SafeClick(By.CssSelector("button.ealb-btn-continue"));
        //Thread.Sleep(5000);

        Log("TEST PASSED");
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

    private IWebElement FindInputByLabel(string labelPart)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//div[@id='root']//label[contains(.,'{labelPart}')]/following-sibling::*[self::input or self::textarea]")));
    }

    private IWebElement FindInputByName(string name)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector($"#root input[name='{name}'], #root textarea[name='{name}']")));
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

    private void AssertRequiredError(string labelPart)
    {

        IWebElement error = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//div[@id='root']//label[contains(.,'{labelPart}')]/following::*[contains(@class,'text-danger') or contains(@class,'invalid-feedback')][1]")));
        Assert.That(error.Text.Trim(), Is.EqualTo("Plotësoni fushën për të vazhduar"));
        Assert.That(FindInputByLabel(labelPart).GetAttribute("class"), Does.Contain("is-invalid"));
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

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {

        Assert.That(driver.FindElement(
            By.XPath($"//*[contains(normalize-space(),'{documentTitle}')]")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-14934"));
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