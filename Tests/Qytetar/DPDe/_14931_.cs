using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.DPDe;

[Category("DPDe")]
[Category("14931")]
public class _14931_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "14931";
    protected override string? ServiceTitle => "RegjistrimiteDhenaveTeNdryshuaraAnije";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void RegjistrimiteDhenaveTeNdryshuaraAnije()
    {




        Log("Assert Title Step 1");
        IWebElement Step1Title = WaitForStepTitle("JENI PRONAR I ANIJES?");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("JENI PRONAR I ANIJES?"));

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

        Log("Assert opsionet e pronësisë së anijes");
        AssertRadioOption("ownerYes", "ownerYes", "Po");
        AssertRadioOption("ownerNo", "ownerNo", "Jo");

        Log("Assert butonat e navigimit");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));
        Assert.That(continueBtn.GetAttribute("disabled"), Is.Not.Null);

        Log("Zgjidh Po");
        SelectRadioById("ownerYes");
        Assert.That(driver.FindElement(By.Id("ownerYes")).Selected, Is.True);
        Assert.That(driver.FindElement(By.Id("ownerNo")).Selected, Is.False);

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
        Log("Headings after step 1: " + GetVisibleHeadings());
        IWebElement Step2Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//h4[contains(.,'llojin e ndryshimit') or contains(.,'Llojin e ndryshimit') or contains(.,'NDRYSHIMIT')]")));
        Assert.That(NormalizeTitle(Step2Title), Does.Contain("NDRYSHIMIT"));

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

        Log("Assert opsionet e llojit të ndryshimit");
        AssertRadioByLabel("Ndryshim Pronësie", "changeType");
        AssertRadioByLabel("Ndryshim Emri", "changeType");
        AssertRadioByLabel("Shtyrje afati të vlefshmërisë", "changeType");
        AssertRadioByLabel("Ndryshim të Dhënash", "changeType");
        AssertRadioByLabel("Ndryshim Destinacioni", "changeType");

        Log("Assert butonat e navigimit Step 2");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));
        Assert.That(continueBtn.GetAttribute("disabled"), Is.Not.Null);

        Log("Zgjidh Ndryshim të Dhënash");
        SelectRadioByLabel("Ndryshim të Dhënash");
        Assert.That(FindRadioByLabel("Ndryshim të Dhënash").Selected, Is.True);
        Assert.That(FindRadioByLabel("Ndryshim Pronësie").Selected, Is.False);
        Assert.That(FindRadioByLabel("Ndryshim Emri").Selected, Is.False);
        Assert.That(FindRadioByLabel("Shtyrje afati të vlefshmërisë").Selected, Is.False);
        Assert.That(FindRadioByLabel("Ndryshim Destinacioni").Selected, Is.False);

        Log("Assert Vazhdo behet i aktivizuar Step 2");
        continueBtn = wait.Until(d =>
        {
            var btn = d.FindElement(By.CssSelector("button.ealb-btn-continue"));
            return btn.GetAttribute("disabled") == null ? btn : null;
        });
        Assert.That(continueBtn.GetAttribute("disabled"), Is.Null);

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Title Step 3");
        IWebElement Step3Title = WaitForStepTitle("TË DHËNAT E APLIKANTIT");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E APLIKANTIT"));

        Log("Assert tooltip i te dhenave te aplikantit");
        IWebElement tooltip = wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector("h4.text-uppercase span[data-bs-toggle='tooltip']")));
        Assert.That(tooltip.GetAttribute("title"),
            Is.EqualTo("Të dhënat e aplikantit plotësohen nga identifikimi juaj në e-Albania"));

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

        Log("Assert te dhenat e aplikantit te para-plotesuara dhe disabled");
        AssertDisabledValue("Nid", Settings.Qytetar.Username);
        AssertDisabledValue("Emri", "Ketjona");
        AssertDisabledValue("Mbiemri", "Mema");
        AssertDisabledValue("Atësia", "Mersin");
        AssertDisabledValue("Qyteti", "KAVAJË");
        AssertDisabledValue("Nr. Tel. Cel.", "0676041404");
        AssertDisabledValue("Email", "ketjona.mema@kreatx.com");
        AssertDisabledValue("Datëlindja", "28.07.1995");
        AssertDisabledValue("Gjinia", "Femër");
        AssertDisabledValue("Vendlindja", "Kavajë");
        AssertDisabledValue("Shtetësia", "Shqiptare");
        AssertDisabledValue("Rrethi", "KAVAJË");
        AssertDisabledValue("Adresa",
            "THABIT REXHA 04040156; Nd. 6; H. 2; ; KAVAJË; KAVAJË; 2501; KAVAJË");

        Log("Assert Kodi Postar dhe Nr. Tel Fiks jane te editueshme");
        IWebElement kodiPostar = FindInputByLabel("Kodi Postar");
        Assert.That(kodiPostar.GetAttribute("name"), Is.EqualTo("kodiPostar"));
        Assert.That(kodiPostar.GetAttribute("disabled"), Is.Null);
        Assert.That(kodiPostar.GetAttribute("readonly"), Is.Null);
        Assert.That(kodiPostar.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement telFiks = FindInputByLabel("Nr. Tel Fiks");
        Assert.That(telFiks.GetAttribute("name"), Is.EqualTo("phoneNrFiks"));
        Assert.That(telFiks.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(telFiks.GetAttribute("disabled"), Is.Null);
        Assert.That(telFiks.GetAttribute("readonly"), Is.Null);
        Assert.That(telFiks.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Ploteso Kodi Postar dhe Nr. Tel Fiks");
        FillInput(kodiPostar, "2501");
        FillInput(telFiks, "055220000");
        Assert.That(FindInputByLabel("Kodi Postar").GetAttribute("value").Trim(), Is.EqualTo("2501"));
        Assert.That(FindInputByLabel("Nr. Tel Fiks").GetAttribute("value").Trim(), Is.EqualTo("055220000"));

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
        IWebElement Step4Title = WaitForStepTitle("TË DHËNAT E MJETIT");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E MJETIT"));

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
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Emri i mjetit')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Gjatësia')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Gjerësia')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Material ndërtimi')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Viti i ndërtimit')]")).Text, Does.Contain("*"));

        Log("Assert fushat e mjetit jane boshe dhe te editueshme");
        IWebElement emriMjetit = FindInputByName("shipName");
        Assert.That(emriMjetit.GetAttribute("type"), Is.EqualTo("text"));
        Assert.That(emriMjetit.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(emriMjetit.GetAttribute("disabled"), Is.Null);
        Assert.That(emriMjetit.GetAttribute("readonly"), Is.Null);

        IWebElement gjatesia = FindInputByName("length");
        Assert.That(gjatesia.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(gjatesia.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(gjatesia.GetAttribute("disabled"), Is.Null);
        Assert.That(gjatesia.GetAttribute("readonly"), Is.Null);

        IWebElement nrImo = FindInputByName("imoNr");
        Assert.That(nrImo.GetAttribute("type"), Is.EqualTo("text"));
        Assert.That(nrImo.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(nrImo.GetAttribute("disabled"), Is.Null);
        Assert.That(nrImo.GetAttribute("readonly"), Is.Null);

        IWebElement gjeresia = FindInputByName("width");
        Assert.That(gjeresia.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(gjeresia.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(gjeresia.GetAttribute("disabled"), Is.Null);
        Assert.That(gjeresia.GetAttribute("readonly"), Is.Null);

        IWebElement materialNdertimi = FindInputByName("constrMaterial");
        Assert.That(materialNdertimi.GetAttribute("type"), Is.EqualTo("text"));
        Assert.That(materialNdertimi.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(materialNdertimi.GetAttribute("disabled"), Is.Null);
        Assert.That(materialNdertimi.GetAttribute("readonly"), Is.Null);

        IWebElement grt = FindInputByName("grt");
        Assert.That(grt.GetAttribute("type"), Is.EqualTo("text"));
        Assert.That(grt.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(grt.GetAttribute("disabled"), Is.Null);
        Assert.That(grt.GetAttribute("readonly"), Is.Null);

        IWebElement vitiNdertimit = FindInputByName("constrYear");
        Assert.That(vitiNdertimit.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(vitiNdertimit.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(vitiNdertimit.GetAttribute("disabled"), Is.Null);
        Assert.That(vitiNdertimit.GetAttribute("readonly"), Is.Null);

        IWebElement nrt = FindInputByName("nrt");
        Assert.That(nrt.GetAttribute("type"), Is.EqualTo("text"));
        Assert.That(nrt.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(nrt.GetAttribute("disabled"), Is.Null);
        Assert.That(nrt.GetAttribute("readonly"), Is.Null);

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
        AssertRequiredError("Emri i mjetit");
        AssertRequiredError("Gjatësia");
        AssertRequiredError("Gjerësia");
        AssertRequiredError("Material ndërtimi");
        AssertRequiredError("Viti i ndërtimit");

        Log("Ploteso te dhenat e mjetit");
        FillInput(FindInputByName("shipName"), "Anija Test");
        FillInput(FindInputByName("length"), "15");
        FillInput(FindInputByName("imoNr"), "IMO9876543");
        FillInput(FindInputByName("width"), "5");
        FillInput(FindInputByName("constrMaterial"), "Çelik");
        FillInput(FindInputByName("grt"), "30");
        FillInput(FindInputByName("constrYear"), "2015");
        FillInput(FindInputByName("nrt"), "18");

        Assert.That(FindInputByName("shipName").GetAttribute("value").Trim(), Is.EqualTo("Anija Test"));
        Assert.That(FindInputByName("length").GetAttribute("value").Trim(), Is.EqualTo("15"));
        Assert.That(FindInputByName("imoNr").GetAttribute("value").Trim(), Is.EqualTo("IMO9876543"));
        Assert.That(FindInputByName("width").GetAttribute("value").Trim(), Is.EqualTo("5"));
        Assert.That(FindInputByName("constrMaterial").GetAttribute("value").Trim(), Is.EqualTo("Çelik"));
        Assert.That(FindInputByName("grt").GetAttribute("value").Trim(), Is.EqualTo("30"));
        Assert.That(FindInputByName("constrYear").GetAttribute("value").Trim(), Is.EqualTo("2015"));
        Assert.That(FindInputByName("nrt").GetAttribute("value").Trim(), Is.EqualTo("18"));

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

        Log("Assert document-upload Mandatpagesa");
        AssertDocumentUpload(
            "mandatePaymentUpload",
            "Mandatpagesa + faturën për regjistrim të dhënave të anije / mjeti lundrues");
        Assert.That(driver.FindElements(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Mandatpagesa')]//span[normalize-space()='*']")).Count, Is.EqualTo(0));

        Log("Assert document-upload Certifikatën origjinale të regjistrimit");
        AssertDocumentUpload(
            "originalRegistrationCertUpload",
            "Certifikatën origjinale të regjistrimit");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Certifikatën origjinale të regjistrimit')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert document-upload Certifikatë siguracioni");
        AssertDocumentUpload(
            "insuranceCertUpload",
            "Certifikatë siguracioni të lëshuar nga shoqëri të licensuara sigurimi");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Certifikatë siguracioni')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert nuk shfaqet dokumenti i pronësisë");
        Assert.That(driver.FindElements(By.Id("ownershipChangeDocUpload")).Count, Is.EqualTo(0));
        Assert.That(driver.FindElements(
            By.XPath("//*[contains(normalize-space(),'Dokumenti i përfitimit të pronësisë')]")).Count, Is.EqualTo(0));

        Log("Assert linkun e shkarkimit te fatures");
        AssertInvoiceLink(
            "/service/Documents_14931/14931_Ndryshim_te_dhenash.pdf",
            "Mandatpagesa");

        Log("Assert dokumentet e administrates");
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Certifikatë Teknike nga shoqëri klasifikuese')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Raport inspektimi nga KSHF për mjete me gjatësi')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Raport inspektimi nga Kapitenët e porteve ose ZRMLD')]")).Displayed, Is.True);

        Log("Assert checkbox i pranimit eshte i pazgjedhur");
        IWebElement consentCheckbox = wait.Until(ExpectedConditions.ElementExists(By.Id("consentCheckbox")));
        Assert.That(consentCheckbox.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='consentCheckbox']")).Text.Trim(),
            Is.EqualTo("Mbledhja e dokumentacionit shoqërues të mësipërm që më parë ishte detyrim të dorëzohej në zyrat e shtetit nga vetë aplikanti, tani është detyrë e nëpunësit të administratës ndaj qytetarit. Me klikimin e këtij butoni, ju bini dakord që këto dokumente të sigurohen për ju nga nëpunësi i administratës."));

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
        UploadDocument("originalRegistrationCertUpload", documentPath);
        UploadDocument("insuranceCertUpload", documentPath);

        Log("Zgjidh pranimin e kushteve");
        ClickCheckbox("consentCheckbox");
        Assert.That(driver.FindElement(By.Id("consentCheckbox")).Selected, Is.True);

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

    private void AssertRadioOption(string radioId, string expectedName, string expectedLabel)
    {

        IWebElement radio = wait.Until(ExpectedConditions.ElementExists(By.Id(radioId)));
        Assert.That(radio.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(radio.GetAttribute("name"), Is.EqualTo(expectedName));
        Assert.That(radio.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector($"label[for='{radioId}']")).Text.Trim(),
            Is.EqualTo(expectedLabel));
    }

    private IWebElement FindRadioByLabel(string labelText)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//form//label[.//span[normalize-space()='{labelText}']]//input[@type='radio']")));
    }

    private void AssertRadioByLabel(string labelText, string expectedName)
    {

        IWebElement radio = FindRadioByLabel(labelText);
        Assert.That(radio.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(radio.GetAttribute("name"), Is.EqualTo(expectedName));
        Assert.That(radio.Selected, Is.False);
        Assert.That(radio.FindElement(By.XPath("./ancestor::label[1]")).Text.Trim(),
            Is.EqualTo(labelText));
    }

    private void SelectRadioByLabel(string labelText)
    {

        IWebElement input = FindRadioByLabel(labelText);
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

        wait.Until(d => FindRadioByLabel(labelText).Selected);
        Thread.Sleep(300);
    }

    private string NormalizeTitle(string raw)
    {

        if (string.IsNullOrWhiteSpace(raw))
            return string.Empty;

        return Regex.Replace(raw, @"[^\p{L}\p{N}?]+", " ").Trim().ToUpperInvariant();
    }

    private string NormalizeTitle(IWebElement title)
    {

        string raw = title.Text;
        if (string.IsNullOrWhiteSpace(raw))
            raw = title.GetDomProperty("textContent") ?? string.Empty;
        if (string.IsNullOrWhiteSpace(raw))
        {
            raw = ((IJavaScriptExecutor)driver).ExecuteScript(
                "return arguments[0].textContent || '';", title)?.ToString() ?? string.Empty;
        }

        return NormalizeTitle(raw);
    }

    private string GetVisibleHeadings()
    {

        try
        {
            object result = ((IJavaScriptExecutor)driver).ExecuteScript(@"
                return Array.from(document.querySelectorAll('h4,h5'))
                    .map(e => (e.textContent || '').replace(/\s+/g, ' ').trim())
                    .join(' | ');
            ");
            return result?.ToString() ?? string.Empty;
        }
        catch (Exception ex)
        {
            return "error: " + ex.Message;
        }
    }

    private IWebElement WaitForStepTitle(string expectedUpper)
    {

        string want = NormalizeTitle(expectedUpper);

        return wait.Until(d =>
        {
            try
            {
                var js = (IJavaScriptExecutor)d;
                var found = js.ExecuteScript(@"
                    const want = arguments[0];
                    const normalize = (s) => (s || '')
                        .replace(/[^A-Za-z0-9\u00C0-\u024F?]+/g, ' ')
                        .replace(/\s+/g, ' ')
                        .trim()
                        .toLocaleUpperCase('en-US');
                    for (const n of document.querySelectorAll('h4, h5')) {
                        const got = normalize(n.textContent);
                        if (got === want || got.includes(want) || want.includes(got))
                            return n;
                    }
                    return null;
                ", want) as IWebElement;

                if (found != null)
                    return found;

                foreach (var title in d.FindElements(By.CssSelector("h4, h5")))
                {
                    string text = NormalizeTitle(title);
                    if (!string.IsNullOrEmpty(text) &&
                        (text == want || text.Contains(want) || want.Contains(text)))
                    {
                        return title;
                    }
                }

                return null;
            }
            catch (StaleElementReferenceException)
            {
                return null;
            }
            catch (Exception ex)
            {
                Log("WaitForStepTitle error: " + ex.Message);
                return null;
            }
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
            By.CssSelector($"#root form input[name='{name}']")));
    }

    private void AssertRequiredError(string labelPart)
    {

        IWebElement error = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//div[@id='root']//form//label[contains(.,'{labelPart}')]/following::*[contains(@class,'text-danger') or contains(@class,'invalid-feedback')][1]")));
        Assert.That(error.Text.Trim(), Is.EqualTo("Plotësoni fushën për të vazhduar"));
        Assert.That(FindInputByLabel(labelPart).GetAttribute("class"), Does.Contain("is-invalid"));
    }

    private void AssertDisabledValue(string labelPart, string expectedValue)
    {

        IWebElement input = FindInputByLabel(labelPart);
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
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
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-14931"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("5"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("50"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të ngarkuar dokumentin e kërkuar"));

        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të ngarkuar dokumentin e kërkuar"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='hint']")).Text.Trim(),
            Is.EqualTo("Formatet e lejuara: PDF. Madhesia maksimale: 5MB."));
    }

    private void AssertInvoiceLink(string href, string surroundingText)
    {

        IWebElement link = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector($"a[href='{href}']")));
        Assert.That(link.Text.Trim(), Is.EqualTo("Shkarkoni këtu"));
        Assert.That(link.FindElement(By.XPath($"./ancestor::*[contains(.,'{surroundingText}')][1]")).Displayed, Is.True);
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