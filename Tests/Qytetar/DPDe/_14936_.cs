using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.DPDe;

[Category("DPDe")]
[Category("14936")]
public class _14936_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "14936";
    protected override string? ServiceTitle => "CertifikateSipasKonventes";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Aplikim për paisjen e anijes me çertifikatë sipas konventës";
    private const string ExpectedAddress =
        "FROSINA PLAKU; Nd. 88; H. 2; Ap. 9; NJËSIA ADMINISTRATIVE NR. 7; NJËSIA BASHKIAKE NR. 7; 1023; TIRANË";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";

    [Test]
    public void CertifikateSipasKonventes()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert Title Step 1");
        IWebElement Step1Title = WaitForStepTitle("TË DHËNAT E APLIKANTIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E APLIKANTIT"));

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

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

        Log("Assert opsionet e llojit të aplikimit");
        AssertRadioOption("rbFirstTime", "applicationType", "Për herë të parë");
        Assert.That(driver.FindElement(By.Id("rbFirstTime")).Selected, Is.True);
        AssertRadioOption("rbRenewal", "applicationType", "Rinovim");
        Assert.That(driver.FindElement(By.Id("rbRenewal")).Selected, Is.False);

        Log("Assert opsionet e llojit të certifikatës");
        AssertRadioOption("mlcCertificate", "certificationType", "Certifikatë sipas konventës MLC");
        Assert.That(driver.FindElement(By.Id("mlcCertificate")).GetAttribute("value"),
            Is.EqualTo("Certifikate sipas konventes MLC"));
        Assert.That(driver.FindElement(By.Id("mlcCertificate")).Selected, Is.True);

        AssertRadioOption("wrmCertificate", "certificationType", "Certifikatë sipas konventës WRM");
        Assert.That(driver.FindElement(By.Id("wrmCertificate")).GetAttribute("value"),
            Is.EqualTo("Certifikate sipas konventes WRM"));
        Assert.That(driver.FindElement(By.Id("wrmCertificate")).Selected, Is.False);

        AssertRadioOption("marpolCertificate", "certificationType", "Certifikatë sipas konventës MARPOL");
        Assert.That(driver.FindElement(By.Id("marpolCertificate")).GetAttribute("value"),
            Is.EqualTo("Certifikate sipas konventes MARPOL"));
        Assert.That(driver.FindElement(By.Id("marpolCertificate")).Selected, Is.False);

        Log("Assert te dhenat e aplikantit te para-plotesuara dhe disabled");
        AssertDisabledValue("Nid", Settings.Qytetar.Username);
        Assert.That(FindInputByName("nid").GetAttribute("name"), Is.EqualTo("nid"));
        AssertDisabledValue("Emri", "Katerina");
        Assert.That(FindInputByName("emri").GetAttribute("name"), Is.EqualTo("emri"));
        AssertDisabledValue("Mbiemri", "Jançe");
        Assert.That(FindInputByName("mbiemri").GetAttribute("name"), Is.EqualTo("mbiemri"));
        AssertDisabledValue("Gjinia", "Femër");
        Assert.That(FindInputByName("gjinia").GetAttribute("type"), Is.EqualTo("text"));
        AssertDisabledValue("Rrethi", "TIRANË");
        Assert.That(FindInputByName("emRrethi").GetAttribute("name"), Is.EqualTo("emRrethi"));
        AssertDisabledValue("Datëlindja", "13.04.1993");
        Assert.That(FindInputByName("datelindja").GetAttribute("name"), Is.EqualTo("datelindja"));
        AssertDisabledValue("Shtetësia", "Shqiptare");
        Assert.That(FindInputByName("shtetesia").GetAttribute("name"), Is.EqualTo("shtetesia"));
        AssertDisabledValue("Qyteti", "TIRANË");
        Assert.That(FindInputByName("bashkia").GetAttribute("name"), Is.EqualTo("bashkia"));
        AssertDisabledValue("Vendlindja", "Korçë");
        Assert.That(FindInputByName("vendlindja").GetAttribute("name"), Is.EqualTo("vendlindja"));
        AssertDisabledValue("Atësia", "Foti");
        Assert.That(FindInputByName("atesia").GetAttribute("name"), Is.EqualTo("atesia"));
        AssertDisabledValue("Nr. Tel. Cel.", "+355697008820");
        Assert.That(FindInputByName("telCel").GetAttribute("name"), Is.EqualTo("telCel"));
        AssertDisabledValue("Email", "katerina.jance@kreatx.com");
        Assert.That(FindInputByLabel("Email").GetAttribute("type"), Is.EqualTo("email"));
        AssertDisabledValue("Adresa", ExpectedAddress);
        Assert.That(FindInputByName("adresa").TagName, Is.EqualTo("textarea"));

        Log("Assert Kodi Postar dhe Nr.Tel Fiks jane te editueshme");
        IWebElement kodiPostar = FindInputByLabel("Kodi Postar");
        Assert.That(kodiPostar.GetAttribute("name"), Is.EqualTo("postalCode"));
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
        FillInput(FindInputByName("postalCode"), "1023");
        FillInput(FindInputByName("telFiks"), "042200000");
        Assert.That(FindInputByName("postalCode").GetAttribute("value").Trim(), Is.EqualTo("1023"));
        Assert.That(FindInputByName("telFiks").GetAttribute("value").Trim(), Is.EqualTo("042200000"));

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
        IWebElement Step2Title = WaitForStepTitle("TË DHËNAT E MJETIT");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E MJETIT"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert 3 hapa, dy te paret aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(3));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Not.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Contain("no-click"));

        Log("Assert label-at e detyrueshme kane yll");
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Emri i mjetit')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Gjatësia')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Gjerësia')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Material ndërtimi')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Viti i ndërtimit')]")).Text, Does.Contain("*"));

        Log("Assert fushat e mjetit jane boshe dhe te editueshme");
        IWebElement emriMjetit = FindInputByName("emriMjetit");
        Assert.That(emriMjetit.GetAttribute("type"), Is.EqualTo("text"));
        Assert.That(emriMjetit.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(emriMjetit.GetAttribute("disabled"), Is.Null);
        Assert.That(emriMjetit.GetAttribute("readonly"), Is.Null);

        IWebElement gjatesia = FindInputByName("gjatesia");
        Assert.That(gjatesia.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(gjatesia.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(gjatesia.GetAttribute("disabled"), Is.Null);
        Assert.That(gjatesia.GetAttribute("readonly"), Is.Null);

        IWebElement nrZyrtar = FindInputByName("nrZyrtar");
        Assert.That(nrZyrtar.GetAttribute("type"), Is.EqualTo("text"));
        Assert.That(nrZyrtar.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(nrZyrtar.GetAttribute("disabled"), Is.Null);
        Assert.That(nrZyrtar.GetAttribute("readonly"), Is.Null);

        IWebElement gjeresia = FindInputByName("gjeresia");
        Assert.That(gjeresia.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(gjeresia.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(gjeresia.GetAttribute("disabled"), Is.Null);
        Assert.That(gjeresia.GetAttribute("readonly"), Is.Null);

        IWebElement materialNdertimi = FindInputByName("materialNdertimi");
        Assert.That(materialNdertimi.GetAttribute("type"), Is.EqualTo("text"));
        Assert.That(materialNdertimi.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(materialNdertimi.GetAttribute("disabled"), Is.Null);
        Assert.That(materialNdertimi.GetAttribute("readonly"), Is.Null);

        IWebElement grt = FindInputByName("grt");
        Assert.That(grt.GetAttribute("type"), Is.EqualTo("text"));
        Assert.That(grt.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(grt.GetAttribute("disabled"), Is.Null);
        Assert.That(grt.GetAttribute("readonly"), Is.Null);

        IWebElement vitiNdertimit = FindInputByName("vitiNdertimit");
        Assert.That(vitiNdertimit.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(vitiNdertimit.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(vitiNdertimit.GetAttribute("disabled"), Is.Null);
        Assert.That(vitiNdertimit.GetAttribute("readonly"), Is.Null);

        IWebElement nrt = FindInputByName("nrt");
        Assert.That(nrt.GetAttribute("type"), Is.EqualTo("text"));
        Assert.That(nrt.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(nrt.GetAttribute("disabled"), Is.Null);
        Assert.That(nrt.GetAttribute("readonly"), Is.Null);

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
        AssertRequiredError("Emri i mjetit");
        AssertRequiredError("Gjatësia");
        AssertRequiredError("Gjerësia");
        AssertRequiredError("Material ndërtimi");
        AssertRequiredError("Viti i ndërtimit");

        Log("Ploteso te dhenat e mjetit");
        FillInput(FindInputByName("emriMjetit"), "Anija Test");
        FillInput(FindInputByName("gjatesia"), "15");
        FillInput(FindInputByName("nrZyrtar"), "IMO9876543");
        FillInput(FindInputByName("gjeresia"), "5");
        FillInput(FindInputByName("materialNdertimi"), "Çelik");
        FillInput(FindInputByName("grt"), "30");
        FillInput(FindInputByName("vitiNdertimit"), "2015");
        FillInput(FindInputByName("nrt"), "18");

        Assert.That(FindInputByName("emriMjetit").GetAttribute("value").Trim(), Is.EqualTo("Anija Test"));
        Assert.That(FindInputByName("gjatesia").GetAttribute("value").Trim(), Is.EqualTo("15"));
        Assert.That(FindInputByName("nrZyrtar").GetAttribute("value").Trim(), Is.EqualTo("IMO9876543"));
        Assert.That(FindInputByName("gjeresia").GetAttribute("value").Trim(), Is.EqualTo("5"));
        Assert.That(FindInputByName("materialNdertimi").GetAttribute("value").Trim(), Is.EqualTo("Çelik"));
        Assert.That(FindInputByName("grt").GetAttribute("value").Trim(), Is.EqualTo("30"));
        Assert.That(FindInputByName("vitiNdertimit").GetAttribute("value").Trim(), Is.EqualTo("2015"));
        Assert.That(FindInputByName("nrt").GetAttribute("value").Trim(), Is.EqualTo("18"));

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
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert 3 hapa, te gjithe aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(3));
        foreach (var step in steps)
            Assert.That(step.GetAttribute("class"), Does.Contain("active"));

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës')]")).Displayed, Is.True);

        Log("Assert document-upload Certifikatë siguracioni");
        AssertDocumentUpload(
            "certifikate_siguracioni_Upload",
            "Certifikatë siguracioni të lëshuar nga shoqëri të licensuara sigurimi");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Certifikatë siguracioni')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert dokumentet e administrates");
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Raport inspektimi nga KSHF për mjete me gjatësi')]")).Displayed, Is.True);

        Log("Assert checkbox i pranimit eshte i pazgjedhur");
        IWebElement consentCheckbox = wait.Until(ExpectedConditions.ElementExists(By.Id("consentCheckbox")));
        Assert.That(consentCheckbox.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='consentCheckbox']")).Text.Trim(),
            Is.EqualTo("Mbledhja e dokumentacionit shoqërues të mësipërm që më parë ishte detyrimi të dorëzohej në zyrat e shtetit nga vetë aplikanti, tani është detyrë e nëpunësit të administratës ndaj qytetarit. Me klikimin e këtij butoni, ju bini dakord që këto dokumente të sigurohen për ju nga nëpunësi i administratës."));

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

        Log("Ngarko dokumentet e detyrueshme");
        UploadDocument("certifikate_siguracioni_Upload", DocumentPath);

        Log("Zgjidh pranimin e kushteve");
        ClickCheckbox("consentCheckbox");
        Assert.That(driver.FindElement(By.Id("consentCheckbox")).Selected, Is.True);

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
        Assert.That(radio.FindElement(By.XPath("./following-sibling::span")).Text.Trim(),
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

    private IWebElement FindInputByName(string name)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector($"#root form input[name='{name}'], #root form textarea[name='{name}']")));
    }

    private void AssertDisabledValue(string labelPart, string expectedValue)
    {

        IWebElement input = FindInputByLabel(labelPart);
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(input.GetAttribute("disabled"), Is.Not.Null);
    }

    private void AssertRequiredError(string labelPart)
    {

        IWebElement error = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//div[@id='root']//form//label[contains(.,'{labelPart}')]/following::*[contains(@class,'text-danger') or contains(@class,'invalid-feedback')][1]")));
        Assert.That(error.Text.Trim(), Is.EqualTo("Plotësoni fushën për të vazhduar"));
        Assert.That(FindInputByLabel(labelPart).GetAttribute("class"), Does.Contain("is-invalid"));
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
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-14936"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("5"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("50"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf,.jpg,.jpeg,.png"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të ngarkuar dokument"));

        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të ngarkuar dokument"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='hint']")).Text.Trim(),
            Is.EqualTo("Formatet e lejuara: PDF, JPG, JPEG, PNG. Madhësia maksimale: 5MB."));
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