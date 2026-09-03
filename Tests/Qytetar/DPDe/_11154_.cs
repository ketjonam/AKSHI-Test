using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.DPDe;

[Category("DPDe")]
[Category("11154")]
public class _11154_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "11154";
    protected override string? ServiceTitle => "RegjitrimAnijeKontrateBareboat";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Aplikim për regjistrim të anijeve me kontratë bareboat";
    private const string ExpectedAddress =
        "FROSINA PLAKU; Nd. 88; H. 2; Ap. 9; NJËSIA ADMINISTRATIVE NR. 7; NJËSIA BASHKIAKE NR. 7; 1023; TIRANË";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";

    [Test]
    public void RegjitrimAnijeKontrateBareboat()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert Title Step 1");
        IWebElement Step1Title = WaitForStepTitle("TË DHËNAT E APLIKANTIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNAT E APLIKANTIT"));

        Log("Assert tooltip i te dhenave te aplikantit");
        IWebElement tooltip = wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector("h5.text-uppercase span[data-bs-toggle='tooltip']")));
        AssertTooltipText(tooltip,
            "Të dhënat e aplikantit plotësohen nga identifikimi juaj ne e-albania");

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 4 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert te dhenat e aplikantit te para-plotesuara dhe disabled");
        AssertDisabledValue("Nid", Settings.Qytetar.Username);
        AssertDisabledValue("Emri", "Katerina");
        AssertDisabledValue("Mbiemri", "Jançe");
        AssertDisabledValue("Atësia", "Foti");
        AssertDisabledValue("Datëlindja", "13.04.1993");
        AssertDisabledValue("Vendlindja", "Korçë");
        AssertDisabledValue("Shtetësia", "Shqiptare");

        Log("Assert gjinia eshte disabled dhe e zgjedhur Femër");
        IWebElement gjiniaSelect = WaitForSelectSelectedValue("gjinia", "1");
        Assert.That(gjiniaSelect.GetAttribute("disabled"), Is.Not.Null);
        var gjinia = new SelectElement(gjiniaSelect);
        Assert.That(gjinia.SelectedOption.GetAttribute("value"), Is.EqualTo("1"));
        Assert.That(gjinia.SelectedOption.Text.Trim(), Is.EqualTo("Femër"));

        Log("Assert butonat e navigimit");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Title Step 2");
        IWebElement Step2Title = WaitForStepTitle("INFORMACIONI I KONTAKTIT TË APLIKANTIT");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("INFORMACIONI I KONTAKTIT TË APLIKANTIT"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 4 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert te dhenat e kontaktit te para-plotesuara dhe disabled");
        AssertDisabledValue("Qyteti", "TIRANË");
        AssertDisabledValue("Rrethi", "TIRANË");
        AssertDisabledValue("Nr. Tel. Cel.", "+355697008820");
        AssertDisabledValue("Email", "katerina.jance@kreatx.com");
        AssertDisabledValue("Adresa", ExpectedAddress);

        Log("Assert tooltip i Nr. Tel. Cel.");
        IWebElement telCelTooltip = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//form//label[contains(.,'Nr. Tel. Cel.')]//span[@data-bs-toggle='tooltip']")));
        AssertTooltipText(telCelTooltip,
            "Numri i celularit merret nga të dhënat e llogarisë që jeni regjistruar në e-Albania");

        Log("Assert Email eshte i tipit email dhe disabled");
        IWebElement emailInput = FindInputByLabel("Email");
        Assert.That(emailInput.GetAttribute("type"), Is.EqualTo("email"));
        Assert.That(emailInput.GetAttribute("disabled"), Is.Not.Null);

        Log("Assert Kodi Postar dhe Nr.Tel Fiks jane te editueshme");
        IWebElement kodiPostar = FindInputByLabel("Kodi Postar");
        Assert.That(kodiPostar.GetAttribute("disabled"), Is.Null);
        Assert.That(kodiPostar.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement telFiks = FindInputByLabel("Nr.Tel Fiks");
        Assert.That(telFiks.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(telFiks.GetAttribute("disabled"), Is.Null);
        Assert.That(telFiks.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Ploteso Kodi Postar dhe Nr.Tel Fiks");
        FillInput(kodiPostar, "1023");
        FillInput(telFiks, "055220000");
        Assert.That(FindInputByLabel("Kodi Postar").GetAttribute("value").Trim(), Is.EqualTo("1023"));
        Assert.That(FindInputByLabel("Nr.Tel Fiks").GetAttribute("value").Trim(), Is.EqualTo("055220000"));

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

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
        AssertSteps(3);

        Log("Assert label-at e detyrueshme kane yll");
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Emri i mjetit')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Gjatësia')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Gjerësia')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Material ndërtimi')]")).Text, Does.Contain("*"));
        Assert.That(driver.FindElement(By.XPath("//form//label[contains(.,'Viti i ndërtimit')]")).Text, Does.Contain("*"));

        Log("Assert fushat e mjetit jane boshe dhe te editueshme");
        Assert.That(FindInputByName("emriMjetit").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindInputByName("emriMjetit").GetAttribute("disabled"), Is.Null);

        IWebElement gjatesia = FindInputByName("gjatesia");
        Assert.That(gjatesia.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(gjatesia.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(gjatesia.GetAttribute("disabled"), Is.Null);

        Assert.That(FindInputByName("nrZyrtar").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindInputByName("nrZyrtar").GetAttribute("disabled"), Is.Null);

        IWebElement gjeresia = FindInputByName("gjeresia");
        Assert.That(gjeresia.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(gjeresia.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(gjeresia.GetAttribute("disabled"), Is.Null);

        Assert.That(FindInputByName("materialNdertimi").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindInputByName("materialNdertimi").GetAttribute("disabled"), Is.Null);
        Assert.That(FindInputByName("grt").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindInputByName("grt").GetAttribute("disabled"), Is.Null);

        IWebElement vitiNdertimit = FindInputByName("vitiNdertimit");
        Assert.That(vitiNdertimit.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(vitiNdertimit.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(vitiNdertimit.GetAttribute("disabled"), Is.Null);

        Assert.That(FindInputByName("nrt").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindInputByName("nrt").GetAttribute("disabled"), Is.Null);

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Vazhdo");

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
        AssertSteps(4);

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës')]")).Displayed, Is.True);

        Log("Assert document-upload Mandatpagesa");
        AssertDocumentUpload(
            "mandat_pagesa_Upload",
            "Mandatpagesa + faturën për regjistrim anije bareboat me gjatësi mbi 15m");

        Log("Assert linket e shkarkimit te fatures");
        IWebElement mbi15Link = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("a[href='/service/Documents_11154/11154_mbi_15m_fature.pdf']")));
        Assert.That(mbi15Link.Text.Trim(), Is.EqualTo("Shkarkoni këtu"));
        IWebElement nen15Link = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("a[href='/service/Documents_11154/11154_nen_15m_fature.pdf']")));
        Assert.That(nen15Link.Text.Trim(), Is.EqualTo("Shkarkoni këtu"));
        Assert.That(driver.FindElement(
            By.XPath("//*[contains(.,'ose për regjistrim anije bareboat me gjatësi nën 15m')]")).Displayed, Is.True);

        Log("Assert document-upload Kontrata noteriale");
        AssertDocumentUpload(
            "kontrata_qirramarrjes_Upload",
            "Kontrata noteriale e Qiramarrjes (Bareboat)");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Kontrata noteriale e Qiramarrjes')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert document-upload Certifikate crregjistrimi");
        AssertDocumentUpload(
            "certifikate_regjistrimi_Upload",
            "Certifikatë e crregjistrimi provizor nga regjistri i mëparshëm");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Certifikatë e crregjistrimi provizor')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert document-upload Deklarate hyrje");
        AssertDocumentUpload(
            "deklarate_hyrje_Upload",
            "Deklaratë hyrje e mjetit lundrues(Policia Kufitare)");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Deklaratë hyrje e mjetit lundrues')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert document-upload Certifikate siguracioni");
        AssertDocumentUpload(
            "certifikate_siguracioni_Upload",
            "Certifikatë siguracioni të lëshuar nga shoqëri të licensuara sigurimi");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'fw-bold') and contains(.,'Certifikatë siguracioni të lëshuar')]//span[normalize-space()='*']")).Displayed, Is.True);

        Log("Assert dokumentet e administrates");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(.,'Certifikatë e sigurisë nga shoqëri klasifikuese')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(.,'Raport inspektimi nga KSHF për mjete me gjatësi')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(.,'Raport inspektimi nga Kapitenët e porteve ose ZRMLD')]")).Displayed, Is.True);

        Log("Assert checkbox i pranimit eshte i pazgjedhur");
        IWebElement consentCheckbox = wait.Until(ExpectedConditions.ElementExists(By.Id("consentCheckbox")));
        Assert.That(consentCheckbox.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='consentCheckbox']")).Text.Trim(),
            Is.EqualTo("Mbledhja e dokumentacionit shoqërues të mësipërm që më parë ishte detyrim të dorëzohej në zyrat e shtetit nga vetë aplikanti, tani është detyrë e nëpunësit të administratës ndaj qytetarit. Me klikimin e këtij butoni, ju bini dakord që këto dokumente të sigurohen për ju nga nëpunësi i administratës."));

        Log("Assert butonat e navigimit Step 4");
        AssertNavigationButtons("Dërgo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Kliko Dergo pa ngarkuar dokumentet e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(1000);
        Assert.That(WaitForStepTitle("DOKUMENTACIONI").Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DOKUMENTACIONI"));

        Log("Ngarko dokumentet e detyrueshme");
        UploadDocument("kontrata_qirramarrjes_Upload", DocumentPath);
        UploadDocument("certifikate_regjistrimi_Upload", DocumentPath);
        UploadDocument("deklarate_hyrje_Upload", DocumentPath);
        UploadDocument("certifikate_siguracioni_Upload", DocumentPath);

        Log("Zgjidh pranimin e kushteve");
        ClickCheckbox("consentCheckbox");
        Assert.That(driver.FindElement(By.Id("consentCheckbox")).Selected, Is.True);

        ClickDergo();

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

    private void AssertSteps(int activeCount)
    {
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(4));
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

    private IWebElement WaitForStepTitle(string expectedUpper)
    {
        return wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h5.text-uppercase, h4.text-uppercase, h4.ealb-header-text"));
            foreach (var title in titles)
            {
                string actual = title.Text.Trim().ToUpperInvariant();
                if (actual == expectedUpper || actual.StartsWith(expectedUpper))
                    return title;
            }
            return null;
        });
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
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-11154"));
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
