using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.DPDe;

[Category("DPDe")]
[Category("14929")]
public class _14929_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "14929";
    protected override string? ServiceTitle => "RegjistrimFillestarAnijeImportuar";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Aplikim për regjistrimin fillestar të një anije/ mjeti lundrues të importuar";
    private const string ExpectedAddress =
        "FROSINA PLAKU; Nd. 88; H. 2; Ap. 9; NJËSIA ADMINISTRATIVE NR. 7; NJËSIA BASHKIAKE NR. 7; 1023; TIRANË";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";

    [Test]
    public void RegjistrimFillestarAnijeImportuar()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert Title Step 1");
        IWebElement Step1Title = WaitForStepTitle("JENI PRONAR I ANIJES?");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("JENI PRONAR I ANIJES?"));

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
        IWebElement Step2Title = WaitForStepTitle("TË DHËNAT E APLIKANTIT");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E APLIKANTIT"));

        Log("Assert tooltip i te dhenave te aplikantit");
        IWebElement tooltip = wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector("h4.text-uppercase span[data-bs-toggle='tooltip']")));
        AssertTooltipText(tooltip,
            "Të dhënat e aplikantit plotësohen nga identifikimi juaj në e-Albania");

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

        Log("Assert te dhenat e aplikantit te para-plotesuara dhe disabled");
        AssertDisabledValue("Nid", Settings.Qytetar.Username);
        AssertDisabledValue("Emri", "Katerina");
        AssertDisabledValue("Mbiemri", "Jançe");
        AssertDisabledValue("Atësia", "Foti");
        AssertDisabledValue("Qyteti", "TIRANË");
        AssertDisabledValue("Nr. Tel. Cel", "+355697008820");
        AssertDisabledValue("Email", "katerina.jance@kreatx.com");
        AssertDisabledValue("Datëlindja", "13.04.1993");
        AssertDisabledValue("Vendlindja", "Korçë");
        AssertDisabledValue("Shtetësia", "Shqiptare");
        AssertDisabledValue("Rrethi", "TIRANË");
        AssertDisabledValue("Adresa", ExpectedAddress);

        Log("Assert tooltip i Nr. Tel. Cel");
        IWebElement telCelTooltip = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//form//label[contains(.,'Nr. Tel. Cel')]/following-sibling::span[@data-bs-toggle='tooltip']")));
        AssertTooltipText(telCelTooltip,
            "Numri i celularit merret nga të dhënat e llogarisë që jeni regjistruar në e-Albania. ");

        Log("Assert gjinia eshte disabled dhe e zgjedhur Femër");
        IWebElement gjiniaSelect = WaitForSelectSelectedValue("gjinia", "F");
        Assert.That(gjiniaSelect.GetAttribute("disabled"), Is.Not.Null);
        var gjinia = new SelectElement(gjiniaSelect);
        Assert.That(gjinia.Options.Count, Is.EqualTo(2));
        Assert.That(gjinia.Options[0].GetAttribute("value"), Is.EqualTo("M"));
        Assert.That(gjinia.Options[0].Text.Trim(), Is.EqualTo("Mashkull"));
        Assert.That(gjinia.Options[1].GetAttribute("value"), Is.EqualTo("F"));
        Assert.That(gjinia.Options[1].Text.Trim(), Is.EqualTo("Femër"));
        Assert.That(gjinia.SelectedOption.GetAttribute("value"), Is.EqualTo("F"));
        Assert.That(gjinia.SelectedOption.Text.Trim(), Is.EqualTo("Femër"));

        Log("Assert Kodi Postar dhe Nr. Tel Fiks jane te editueshme");
        IWebElement kodiPostar = FindInputByLabel("Kodi Postar");
        Assert.That(kodiPostar.GetAttribute("name"), Is.EqualTo("postalCode"));
        Assert.That(kodiPostar.GetAttribute("disabled"), Is.Null);
        Assert.That(kodiPostar.GetAttribute("readonly"), Is.Null);
        Assert.That(kodiPostar.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement telFiks = FindInputByLabel("Nr. Tel Fiks");
        Assert.That(telFiks.GetAttribute("name"), Is.EqualTo("phoneNrFiks"));
        Assert.That(telFiks.GetAttribute("disabled"), Is.Null);
        Assert.That(telFiks.GetAttribute("readonly"), Is.Null);
        Assert.That(telFiks.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Ploteso Kodi Postar dhe Nr. Tel Fiks");
        FillInput(kodiPostar, "1023");
        FillInput(telFiks, "042200000");
        Assert.That(FindInputByLabel("Kodi Postar").GetAttribute("value").Trim(), Is.EqualTo("1023"));
        Assert.That(FindInputByLabel("Nr. Tel Fiks").GetAttribute("value").Trim(), Is.EqualTo("042200000"));

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
        IWebElement Step3Title = WaitForStepTitle("TË DHËNAT E MJETIT");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E MJETIT"));

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

        Log("Assert butonat e navigimit Step 3");
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
            "paymentMandateAndInvoiceDocUpload",
            "Mandatpagesa + faturën për regjistrim anije / mjeti lundrues të importuar");
        Assert.That(driver.FindElements(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Mandatpagesa')]//span[normalize-space()='*']")).Count, Is.EqualTo(0));

        Log("Assert document-upload Dokument i përfitimit të pronësisë");
        AssertDocumentUpload(
            "ownershipDocUpload",
            "Dokument i përfitimit të pronësisë");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Dokument i përfitimit të pronësisë')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert document-upload Certifikatë e çregjistrimit");
        AssertDocumentUpload(
            "deregistrationDocUpload",
            "Certifikatë e çregjistrimit nga shteti i origjinës");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Certifikatë e çregjistrimit nga shteti i origjinës')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert document-upload Dokumentet e zhdoganimit");
        AssertDocumentUpload(
            "clearanceDocUpload",
            "Dokumentet e zhdoganimit");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Dokumentet e zhdoganimit')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert document-upload Deklaratë hyrje");
        AssertDocumentUpload(
            "entryDocUpload",
            "Deklaratë hyrje e mjetit lundrues(Policia Kufitare)");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Deklaratë hyrje e mjetit lundrues')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert linket e shkarkimit te fatures");
        AssertInvoiceLink(
            "/service/Documents_14929/11157_fature.pdf",
            "Regjistrimi përfundimtar të motorrit të detit");
        AssertInvoiceLink(
            "/service/Documents_14929/11156_pa_motor_fature.pdf",
            "Regjistrimin e mjeteve të vogla lundruese me gjatësi nën 15 m pa motor");
        AssertInvoiceLink(
            "/service/Documents_14929/11156_me_motor_fature.pdf",
            "Regjistrimin e mjeteve të vogla lundruese me gjatësi nën 15 m me motor");
        AssertInvoiceLink(
            "/service/Documents_14929/11160_mbi_15m_fature.pdf",
            "Regjistrimin e mjeteve lundruese të shërbimit portual me gjatësi mbi 15 m");
        AssertInvoiceLink(
            "/service/Documents_14929/11160_nen_15m_fature.pdf",
            "Regjistrimin e mjeteve lundruese të shërbimit portual me gjatësi nën 15 m");
        AssertInvoiceLink(
            "/service/Documents_14929/14114_fature.pdf",
            "Regjistrimi provizor / përfundimtar të anijeve të transportit");
        AssertInvoiceLink(
            "/service/Documents_14929/14116_mbi_15m_fature.pdf",
            "Regjistrimi provizor / përfundimtar të anijeve të peshkimit me gjatësi mbi 15 m");
        AssertInvoiceLink(
            "/service/Documents_14929/14116_nen_15m_fature.pdf",
            "Regjistrimi provizor / përfundimtar të anijeve të peshkimit me gjatësi nën 15 m");

        Log("Assert dokumentet e administrates");
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Certifikatë Teknike nga shoqëri klasifikuese')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Raport inspektimi nga KSHF për mjete me gjatësi')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Raport inspektimi nga Kapitenët e porteve ose ZRMLD')]")).Displayed, Is.True);

        Log("Assert checkbox i pranimit eshte i pazgjedhur");
        IWebElement agreeCheck = wait.Until(ExpectedConditions.ElementExists(By.Id("agreeCheck")));
        Assert.That(agreeCheck.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='agreeCheck']")).Text.Trim(),
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

        Log("Ngarko dokumentet e detyrueshme");
        UploadDocument("ownershipDocUpload", DocumentPath);
        UploadDocument("deregistrationDocUpload", DocumentPath);
        UploadDocument("clearanceDocUpload", DocumentPath);
        UploadDocument("entryDocUpload", DocumentPath);

        Log("Zgjidh pranimin e kushteve");
        ClickCheckbox("agreeCheck");
        Assert.That(driver.FindElement(By.Id("agreeCheck")).Selected, Is.True);

        //Log("Kliko Dergo");
        //SafeClick(By.CssSelector("button.ealb-btn-continue"));
        //Thread.Sleep(5000);

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

    private IWebElement FindSelectByName(string name)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector($"#root form select[name='{name}']")));
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
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-14929"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("5"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("50"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të ngarkuar dokumentin"));

        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të ngarkuar dokumentin"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='hint']")).Text.Trim(),
            Is.EqualTo("Formatet e lejuara: PDF. Madhësia maksimale: 5MB."));
    }

    private void AssertInvoiceLink(string href, string surroundingText)
    {

        IWebElement link = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector($"a[href='{href}']")));
        Assert.That(link.Text.Trim(), Is.EqualTo("Shkarko faturën"));
        Assert.That(driver.FindElement(
            By.XPath($"//*[contains(.,'{surroundingText}')]")).Displayed, Is.True);
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