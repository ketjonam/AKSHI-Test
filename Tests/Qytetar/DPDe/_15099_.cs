using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.DPDe;

[Category("DPDe")]
[Category("15099")]
public class _15099_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "15099";
    protected override string? ServiceTitle => "CertifikatePerPilot";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void CertifikatePerPilot()
    {




        Log("Assert Title Step 1");
        IWebElement Step1Title = WaitForStepTitle("TË DHËNAT E APLIKIMIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E APLIKIMIT"));

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 5 hapa, hapi i pare aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(5));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));
        for (int i = 1; i < steps.Count; i++)
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert label-at e detyrueshme kane yll");
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Zgjidh kategorinë')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Zgjidhni llojin e aplikimit')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Zgjidhni portin')]")).Text, Does.Contain("*"));

        Log("Assert dropdown Kategoria");
        Assert.That(FindSelectByLabel("Zgjidh kategorinë").GetAttribute("name"), Is.EqualTo("kategoria"));
        AssertSelectOptions("kategoria", "", "Pilot kategoria A", "Pilot kategoria B");

        Log("Assert dropdown Lloji i aplikimit");
        Assert.That(FindSelectByLabel("Zgjidhni llojin e aplikimit").GetAttribute("name"), Is.EqualTo("llojiAplikimit"));
        AssertSelectOptions("llojiAplikimit", "", "Herë të parë", "Rinovim");

        Log("Assert dropdown Porti");
        Assert.That(FindSelectByLabel("Zgjidhni portin").GetAttribute("name"), Is.EqualTo("porti"));
        AssertSelectOptions("porti", "", "Durrës", "Shëngjin", "Vlorë", "Sarandë");

        Log("Assert butonat e navigimit");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));
        Assert.That(continueBtn.GetAttribute("disabled"), Is.Null);

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error per fushat e detyrueshme");
        AssertRequiredError("Zgjidh kategorinë", "Përzgjidhni një fushë për të vazhduar");
        AssertRequiredError("Zgjidhni llojin e aplikimit", "Përzgjidhni një fushë për të vazhduar");
        AssertRequiredError("Zgjidhni portin", "Përzgjidhni një fushë për të vazhduar");

        Log("Zgjidh te dhenat e aplikimit");
        SelectByValue("kategoria", "Pilot kategoria A");
        SelectByValue("llojiAplikimit", "Herë të parë");
        SelectByValue("porti", "Durrës");

        Assert.That(new SelectElement(FindSelectByName("kategoria")).SelectedOption.GetAttribute("value"),
            Is.EqualTo("Pilot kategoria A"));
        Assert.That(new SelectElement(FindSelectByName("llojiAplikimit")).SelectedOption.GetAttribute("value"),
            Is.EqualTo("Herë të parë"));
        Assert.That(new SelectElement(FindSelectByName("porti")).SelectedOption.GetAttribute("value"),
            Is.EqualTo("Durrës"));

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Title Step 2");
        IWebElement Step2Title = WaitForStepTitle("TË DHËNAT E APLIKANTIT");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E APLIKANTIT"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 5 hapa, dy te paret aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(5));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        for (int i = 2; i < steps.Count; i++)
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert te dhenat e aplikantit te para-plotesuara dhe readonly");
        AssertReadOnlyValue("Nid", Settings.Qytetar.Username);
        Assert.That(FindInputByName("nid").GetAttribute("name"), Is.EqualTo("nid"));
        AssertReadOnlyValue("Datëlindja", "28.07.1995");
        Assert.That(FindInputByName("datelindja").GetAttribute("name"), Is.EqualTo("datelindja"));
        AssertReadOnlyValue("Emri", "Ketjona");
        Assert.That(FindInputByName("emri").GetAttribute("name"), Is.EqualTo("emri"));
        AssertReadOnlyValue("Gjinia", "Femër");
        Assert.That(FindInputByName("gjinia").GetAttribute("type"), Is.EqualTo("text"));
        AssertReadOnlyValue("Mbiemri", "Mema");
        Assert.That(FindInputByName("mbiemri").GetAttribute("name"), Is.EqualTo("mbiemri"));
        AssertReadOnlyValue("Vendlindja", "Kavajë");
        Assert.That(FindInputByName("vendlindja").GetAttribute("name"), Is.EqualTo("vendlindja"));
        AssertReadOnlyValue("Atësia", "Mersin");
        Assert.That(FindInputByName("atesia").GetAttribute("name"), Is.EqualTo("atesia"));
        AssertDisabledValue("Shtetësia", "Shqiptare");
        Assert.That(FindInputByName("shtetesia").GetAttribute("name"), Is.EqualTo("shtetesia"));

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

        Log("Assert kohëzgjatja Step 3");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 5 hapa, tre te paret aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(5));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Contain("active"));
        for (int i = 3; i < steps.Count; i++)
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert te dhenat e kontaktit te para-plotesuara dhe disabled");
        AssertDisabledValue("Qyteti", "KAVAJË");
        Assert.That(FindInputByName("qyteti").GetAttribute("name"), Is.EqualTo("qyteti"));
        AssertDisabledValue("Nr. Tel. Cel", "0676041404");
        Assert.That(FindInputByName("nrTel").GetAttribute("name"), Is.EqualTo("nrTel"));
        AssertDisabledValue("Email", "ketjona.mema@kreatx.com");
        Assert.That(FindInputByLabel("Email").GetAttribute("type"), Is.EqualTo("email"));
        AssertDisabledValue("Adresa",
            "THABIT REXHA 04040156; Nd. 6; H. 2; ; KAVAJË; KAVAJË; 2501; KAVAJË");
        Assert.That(FindInputByName("adresa").GetAttribute("type"), Is.EqualTo("text"));

        Log("Assert Rrethi eshte i editueshem dhe i para-plotesuar");
        IWebElement rrethi = FindInputByLabel("Rrethi");
        Assert.That(rrethi.GetAttribute("name"), Is.EqualTo("bashkia"));
        Assert.That(rrethi.GetAttribute("disabled"), Is.Null);
        Assert.That(rrethi.GetAttribute("readonly"), Is.Null);
        Assert.That(rrethi.GetAttribute("value").Trim(), Is.EqualTo("KAVAJË"));

        Log("Assert Kodi postar dhe Nr. Tel Fiks jane te editueshme");
        IWebElement kodiPostar = FindInputByLabel("Kodi postar");
        Assert.That(kodiPostar.GetAttribute("name"), Is.EqualTo("kodiPostar"));
        Assert.That(kodiPostar.GetAttribute("disabled"), Is.Null);
        Assert.That(kodiPostar.GetAttribute("readonly"), Is.Null);
        Assert.That(kodiPostar.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement telFiks = FindInputByLabel("Nr. Tel Fiks");
        Assert.That(telFiks.GetAttribute("name"), Is.EqualTo("nrTelFiks"));
        Assert.That(telFiks.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(telFiks.GetAttribute("disabled"), Is.Null);
        Assert.That(telFiks.GetAttribute("readonly"), Is.Null);
        Assert.That(telFiks.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Ploteso Kodi postar dhe Nr. Tel Fiks");
        FillInput(FindInputByName("kodiPostar"), "2501");
        FillInput(FindInputByName("nrTelFiks"), "055220000");
        Assert.That(FindInputByName("kodiPostar").GetAttribute("value").Trim(), Is.EqualTo("2501"));
        Assert.That(FindInputByName("nrTelFiks").GetAttribute("value").Trim(), Is.EqualTo("055220000"));

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
        IWebElement Step4Title = WaitForStepTitle("INFORMACION SPECIFIK MBI APLIKIMIN");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("INFORMACION SPECIFIK MBI APLIKIMIN"));

        Log("Assert kohëzgjatja Step 4");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 5 hapa, kater te paret aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(5));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[3].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[4].GetAttribute("class"), Does.Not.Contain("active"));
        Assert.That(steps[4].GetAttribute("class"), Does.Contain("no-click"));

        Log("Assert label-at e detyrueshme kane yll");
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Gjatësia (cm)')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Ngjyra e syve')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Shenja të veçanta')]")).Text, Does.Contain("*"));

        Log("Assert fushat e informacionit specifik jane boshe dhe te editueshme");
        IWebElement gjatesia = FindInputByName("gjatesia");
        Assert.That(gjatesia.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(gjatesia.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(gjatesia.GetAttribute("disabled"), Is.Null);
        Assert.That(gjatesia.GetAttribute("readonly"), Is.Null);

        IWebElement ngjyraESyve = FindInputByName("ngjyraESyve");
        Assert.That(ngjyraESyve.GetAttribute("type"), Is.EqualTo("text"));
        Assert.That(ngjyraESyve.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(ngjyraESyve.GetAttribute("disabled"), Is.Null);
        Assert.That(ngjyraESyve.GetAttribute("readonly"), Is.Null);

        IWebElement shenjaTeVecanta = FindInputByName("shenjaTeVecanta");
        Assert.That(shenjaTeVecanta.GetAttribute("type"), Is.EqualTo("text"));
        Assert.That(shenjaTeVecanta.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(shenjaTeVecanta.GetAttribute("disabled"), Is.Null);
        Assert.That(shenjaTeVecanta.GetAttribute("readonly"), Is.Null);

        Log("Assert butonat e navigimit Step 4");
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
        FillInput(FindInputByName("ngjyraESyve"), "Kafe");
        FillInput(FindInputByName("shenjaTeVecanta"), "Asnjë");

        Assert.That(FindInputByName("gjatesia").GetAttribute("value").Trim(), Is.EqualTo("165"));
        Assert.That(FindInputByName("ngjyraESyve").GetAttribute("value").Trim(), Is.EqualTo("Kafe"));
        Assert.That(FindInputByName("shenjaTeVecanta").GetAttribute("value").Trim(), Is.EqualTo("Asnjë"));

        Log("Kliko Vazhdo Step 4");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Title Step 5");
        IWebElement Step5Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step5Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 5");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 5 hapa, te gjithe aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(5));
        foreach (var step in steps)
            Assert.That(step.GetAttribute("class"), Does.Contain("active"));

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës')]")).Displayed, Is.True);

        Log("Assert document-upload Mandat pagesa (opsional)");
        AssertDocumentUpload(
            "fileMandatPagesaUpload",
            "Mandat pagesa + faturën për regjistrim dhe pajisja me certifikatë për pilot");
        Assert.That(driver.FindElements(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Mandat pagesa')]//span[normalize-space()='*']")).Count, Is.EqualTo(0));

        Log("Assert document-upload Fotografi");
        AssertDocumentUpload(
            "fileFotografiUpload",
            "Fotografi (4x5 cm për dokument)");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Fotografi')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert document-upload Raport mjekoligjor");
        AssertDocumentUpload(
            "fileRaportMjekoligjorUpload",
            "Raport mjekoligjor (shikim, dëgjim)");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Raport mjekoligjor')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert document-upload Eksperiencë pune");
        AssertDocumentUpload(
            "fileEksperiencePuneUpload",
            "Eksperiencë pune");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Eksperiencë pune')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert nuk nevojitet dokumentacion nga administrata");
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(@class,'text-muted') and contains(.,'Për këtë shërbim nuk nevojitet të sigurohet dokumentacion nga nëpunësi i administratës')]")).Displayed, Is.True);
        Assert.That(driver.FindElements(By.Id("agreeCheck")).Count, Is.EqualTo(0));
        Assert.That(driver.FindElements(By.Id("consentCheckbox")).Count, Is.EqualTo(0));

        Log("Assert butonat e navigimit Step 5");
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
        UploadDocument("fileFotografiUpload", documentPath);
        UploadDocument("fileRaportMjekoligjorUpload", documentPath);
        UploadDocument("fileEksperiencePuneUpload", documentPath);

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
            By.XPath($"//div[@id='root']//form//label[contains(.,'{labelPart}')]/following-sibling::*[self::input or self::textarea]")));
    }

    private IWebElement FindInputByName(string name)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector($"#root form input[name='{name}'], #root form textarea[name='{name}']")));
    }

    private IWebElement FindSelectByName(string name)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector($"#root form select[name='{name}']")));
    }

    private void AssertReadOnlyValue(string labelPart, string expectedValue)
    {

        IWebElement input = FindInputByLabel(labelPart);
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(input.GetAttribute("readonly"), Is.Not.Null);
    }

    private void AssertDisabledValue(string labelPart, string expectedValue)
    {

        IWebElement input = FindInputByLabel(labelPart);
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(input.GetAttribute("disabled"), Is.Not.Null);
    }

    private IWebElement FindSelectByLabel(string labelPart)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//div[@id='root']//form//label[contains(.,'{labelPart}')]/following-sibling::select")));
    }

    private void AssertRequiredError(string labelPart, string expectedMessage = "Plotësoni fushën për të vazhduar")
    {

        IWebElement error = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//div[@id='root']//form//label[contains(.,'{labelPart}')]/following::*[contains(@class,'text-danger') or contains(@class,'invalid-feedback')][1]")));
        Assert.That(error.Text.Trim(), Is.EqualTo(expectedMessage));
        IWebElement field = wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//div[@id='root']//form//label[contains(.,'{labelPart}')]/following-sibling::*[self::input or self::textarea or self::select]")));
        Assert.That(field.GetAttribute("class"), Does.Contain("is-invalid"));
    }

    private void AssertSelectOptions(string name, params string[] expectedValues)
    {

        var select = new SelectElement(FindSelectByName(name));
        var actualValues = select.Options.Select(o => o.GetAttribute("value") ?? string.Empty).ToArray();
        var actualTexts = select.Options.Select(o => o.Text.Trim()).ToArray();

        Assert.That(actualValues, Is.EqualTo(expectedValues));
        Assert.That(actualTexts, Is.EqualTo(expectedValues));
        Assert.That(select.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
    }

    private void SelectByValue(string name, string value)
    {

        IWebElement select = FindSelectByName(name);
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            select
        );
        Thread.Sleep(300);

        new SelectElement(select).SelectByValue(value);

        ((IJavaScriptExecutor)driver).ExecuteScript(@"
            const el = arguments[0];
            el.dispatchEvent(new Event('input', { bubbles: true }));
            el.dispatchEvent(new Event('change', { bubbles: true }));
        ", select);

        wait.Until(d => new SelectElement(d.FindElement(
            By.CssSelector($"#root form select[name='{name}']"))).SelectedOption.GetAttribute("value") == value);
        Thread.Sleep(300);
    }

    private void FillInput(IWebElement input, string value)
    {

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            input
        );
        Thread.Sleep(400);

        ((IJavaScriptExecutor)driver).ExecuteScript(@"
            const el = arguments[0];
            const proto = el.tagName === 'TEXTAREA'
                ? window.HTMLTextAreaElement.prototype
                : window.HTMLInputElement.prototype;
            const setter = Object.getOwnPropertyDescriptor(proto, 'value').set;
            setter.call(el, arguments[1]);
            el.dispatchEvent(new Event('input', { bubbles: true }));
            el.dispatchEvent(new Event('change', { bubbles: true }));
        ", input, value);

        Thread.Sleep(300);
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {

        Assert.That(driver.FindElement(
            By.XPath($"//*[contains(normalize-space(),'{documentTitle}')]")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-15099"));
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