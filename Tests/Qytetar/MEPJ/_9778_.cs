using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.MEPJ;

[Category("MEPJ")]
[Category("9778")]
public class _9778_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "9778";
    protected override string? ServiceTitle => "LidhjeMarteseMeShtetasTeHuaj";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName = "Aplikim për lidhje martese me shtetas të huaj";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";
    private const string IdentifikimiUploadId = "dokumentIdentiteti_doc_lmhUpload";
    private const string LejeQendrimiUploadId = "lejeQendrimi_doc_lmhUpload";
    private const string IdDeshmitareveUploadId = "dokumentidentitetiDeshmitare_lmhUpload";
    private const string DokumentIdHuajUploadId = "dokumentIDHuaj_doc_lmhUpload";
    private const int TotalSteps = 7;

    [Test]
    public void LidhjeMarteseMeShtetasTeHuaj()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert Title Step 1");
        IWebElement Step1Title = WaitForStepTitle("TË DHËNA PERSONALE TË APLIKANTIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNA PERSONALE TË APLIKANTIT"));

        Log("Assert kohëzgjatja");
        AssertDuration("5 minuta kohëzgjatje");

        Log("Assert 7 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert te dhenat e aplikantit te para-plotesuara");
        AssertDisabledName("nid", Settings.Qytetar.Username);
        AssertDisabledName("firstName", "Katerina");
        AssertDisabledName("lastName", "Jançe");
        AssertDisabledName("gender", "Femër", readonlyExpected: true);
        AssertDisabledByLabel("Datëlindja", "13.04.1993");
        AssertDisabledName("fatherName", "Foti");
        AssertDisabledName("motherName", "Manushaqe");

        IWebElement vendlindjaQyteti = FindByName("birthCity");
        Assert.That(vendlindjaQyteti.GetAttribute("value").Trim(), Is.EqualTo("Korçë"));
        Assert.That(vendlindjaQyteti.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(vendlindjaQyteti.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindLabel("Vendlindja/Qyteti").Text, Does.Contain("*"));

        IWebElement vendlindjaShteti = FindByName("birthCountry");
        Assert.That(new SelectElement(vendlindjaShteti).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Vendlindja/Shteti").Text, Does.Contain("*"));

        IWebElement shtetesia = FindByName("citizenship");
        Assert.That(shtetesia.GetAttribute("value").Trim(), Is.EqualTo("Shqiptare"));
        Assert.That(FindLabel("Shtetësia").Text, Does.Contain("*"));

        IWebElement shtetesiaDyte = FindByName("secondCitizenship");
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
        AssertDuration("5 minuta kohëzgjatje");

        Log("Assert 7 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert fushat e adreses");
        Assert.That(new SelectElement(FindByName("bashkiaCode")).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Shteti").Text, Does.Contain("*"));
        Assert.That(FindByName("cityCode").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Qyteti").Text, Does.Contain("*"));
        Assert.That(FindByName("streetAndNumber").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Rruga dhe numri i banesës").Text, Does.Contain("*"));
        Assert.That(FindByName("postalCodeId").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Kodi postar").Text, Does.Contain("*"));
        Assert.That(FindByName("admUnit").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindByName("additionalAddressInfo").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Përzgjidhni një vlerë për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 2");
        SelectByValue(FindByName("bashkiaCode"), "2");
        FillInput(FindByName("cityCode"), "test");
        FillInput(FindByName("streetAndNumber"), "test");
        FillInput(FindByName("postalCodeId"), "1001");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("KONTAKTI");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(), Does.StartWith("KONTAKTI"));

        Log("Assert kohëzgjatja Step 3");
        AssertDuration("5 minuta kohëzgjatje");

        Log("Assert 7 hapa, tre te paret aktiv");
        AssertSteps(3);

        Log("Assert te dhenat e kontaktit");
        IWebElement email = FindByName("email");
        Assert.That(email.GetAttribute("type"), Is.EqualTo("email"));
        Assert.That(email.GetAttribute("value").Trim(), Is.EqualTo("katerina.jance@kreatx.com"));
        Assert.That(email.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindLabel("Adresa elektronike (Email)").Text, Does.Contain("*"));

        IWebElement tel = FindByName("phoneNumber");
        Assert.That(tel.GetAttribute("value").Trim(), Is.EqualTo("+355697008820"));
        Assert.That(tel.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(tel.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindLabel("Nr. tel").Text, Does.Contain("*"));

        IWebElement contactCountry = FindByName("country");
        Assert.That(new SelectElement(contactCountry).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Shteti").Text, Does.Contain("*"));

        IWebElement consularOffice = FindByName("consularOffice");
        Assert.That(new SelectElement(consularOffice).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Zyra konsullore").Text, Does.Contain("*"));

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Përzgjidhni një vlerë për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 3");
        SelectByValue(contactCountry, "Emiratet e Bashkuara Arabe");
        SelectFirstWhenEnabled(By.Name("consularOffice"));

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 4 Title");
        IWebElement Step4Title = WaitForStepTitle("TË DHËNAT E BASHKËSHORTIT");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNAT E BASHKËSHORTIT"));

        Log("Assert kohëzgjatja Step 4");
        AssertDuration("5 minuta kohëzgjatje");

        Log("Assert 7 hapa, kater te paret aktiv");
        AssertSteps(4);

        Log("Assert fushat e bashkeshortit");
        AssertSpouseFields("husband");

        Log("Assert butonat e navigimit Step 4");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 4");
        FillSpouseFields("husband", "test", "10.04.1990", 1990, 4, 10);

        Log("Kliko Vazhdo Step 4");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 5 Title");
        IWebElement Step5Title = WaitForStepTitle("TË DHËNAT E BASHKËSHORTES");
        Assert.That(Step5Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNAT E BASHKËSHORTES"));

        Log("Assert kohëzgjatja Step 5");
        AssertDuration("5 minuta kohëzgjatje");

        Log("Assert 7 hapa, pese te paret aktiv");
        AssertSteps(5);

        Log("Assert fushat e bashkeshortes");
        AssertSpouseFields("wife");

        Log("Assert butonat e navigimit Step 5");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 5");
        FillSpouseFields("wife", Settings.Qytetar.Username, "10.04.1995", 1995, 4, 10);

        Log("Kliko Vazhdo Step 5");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 6 Title");
        IWebElement Step6Title = WaitForStepTitle("DËSHMITARËT");
        Assert.That(Step6Title.Text.Trim().ToUpperInvariant(), Does.StartWith("DËSHMITARËT"));

        Log("Assert kohëzgjatja Step 6");
        AssertDuration("5 minuta kohëzgjatje");

        Log("Assert 7 hapa, gjashte te paret aktiv");
        AssertSteps(6);

        Log("Assert fushat e deshmitareve");
        Assert.That(FindByName("witness1FirstName").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Dëshmitari 1 Emri").Text, Does.Contain("*"));
        Assert.That(FindByName("witness1LastName").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Dëshmitari 1 Mbiemri").Text, Does.Contain("*"));
        Assert.That(FindByName("witness1Nid").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("NID i dëshmitarit të parë").Text, Does.Contain("*"));
        Assert.That(FindByName("witness2FirstName").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Dëshmitari 2 Emri").Text, Does.Contain("*"));
        Assert.That(FindByName("witness2LastName").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Dëshmitari 2 Mbiemri").Text, Does.Contain("*"));
        Assert.That(FindByName("witness2Nid").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("NID i dëshmitarit të dytë").Text, Does.Contain("*"));

        Log("Assert butonat e navigimit Step 6");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 6");
        FillInput(FindByName("witness1FirstName"), "test");
        FillInput(FindByName("witness1LastName"), "test");
        FillInput(FindByName("witness1Nid"), "test");
        FillInput(FindByName("witness2FirstName"), "test");
        FillInput(FindByName("witness2LastName"), "test");
        FillInput(FindByName("witness2Nid"), "test");

        Log("Kliko Vazhdo Step 6");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 7 Title");
        IWebElement Step7Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step7Title.Text.Trim().ToUpperInvariant(), Does.StartWith("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 7");
        AssertDuration("5 minuta kohëzgjatje");

        Log("Assert 7 hapa, te gjithe aktiv");
        AssertSteps(7);

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Dokumenta që ngarkohen nga Aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Dokumenta që ngarkohen nga nëpunësi i administratës publike')]"))
            .Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//ol//li[contains(.,'Certifikatë martese')]")).Displayed, Is.True);

        Log("Assert document-upload dokumentet e aplikantit");
        AssertDocumentUpload(IdentifikimiUploadId,
            "Dokument identifikimi (Pasaportë ose kartë identiteti shqiptare)", required: true);
        AssertDocumentUpload(LejeQendrimiUploadId,
            "Leje qëndrimi ose konfermë e qëndrimit të ligjshëm në vendin pritës, për secilin shtetas",
            required: true);
        AssertDocumentUpload(IdDeshmitareveUploadId,
            "Dokument i vlefshëm identifikimi për secilin dëshmitar", required: true);
        AssertDocumentUpload(DokumentIdHuajUploadId,
            "Dokument për shtetasin/en e huaj që lejon martesën (i legalizuar ose apostiluar)",
            required: true);

        Log("Assert checkbox pëlqimi");
        IWebElement agreeCheck = wait.Until(ExpectedConditions.ElementExists(By.Id("agreeCheck")));
        Assert.That(agreeCheck.GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='agreeCheck']")).Text,
            Does.Contain("Me klikimin e këtij butoni, ju bini dakord"));

        Log("Assert butonat e navigimit Step 7");
        AssertNavigationButtons("Dërgo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue.with-arrow")));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Kliko Dergo pa ngarkuar dokumentet e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue.with-arrow"));
        Thread.Sleep(1000);
        Assert.That(WaitForStepTitle("DOKUMENTACIONI").Text.Trim().ToUpperInvariant(),
            Does.StartWith("DOKUMENTACIONI"));

        Log("Ngarko dokumentet e detyrueshme");
        UploadDocument(IdentifikimiUploadId, DocumentPath);
        UploadDocument(LejeQendrimiUploadId, DocumentPath);
        UploadDocument(IdDeshmitareveUploadId, DocumentPath);
        UploadDocument(DokumentIdHuajUploadId, DocumentPath);

        Log("Kliko checkbox pëlqimi");
        IWebElement agree = driver.FindElement(By.Id("agreeCheck"));
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
            var titles = d.FindElements(By.CssSelector("h4, h5"));
            foreach (var title in titles)
            {
                string actual = title.Text.Trim().ToUpperInvariant();
                if (actual == expectedUpper || actual.StartsWith(expectedUpper))
                    return title;
            }
            return null;
        });
    }

    private IWebElement FindByName(string name)
    {
        return wait.Until(ExpectedConditions.ElementExists(By.Name(name)));
    }

    private IWebElement FindLabel(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//form//label[contains(.,'{labelPart}')]")));
    }

    private IWebElement FindInputByLabel(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//form//label[contains(.,'{labelPart}')]/following-sibling::input")));
    }

    private void AssertDisabledName(string name, string expectedValue, bool readonlyExpected = false)
    {
        IWebElement field = FindByName(name);
        Assert.That(field.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(field.GetAttribute("disabled"), Is.Not.Null);
        if (readonlyExpected)
            Assert.That(field.GetAttribute("readonly"), Is.Not.Null);
    }

    private void AssertDisabledByLabel(string labelPart, string expectedValue)
    {
        IWebElement field = FindInputByLabel(labelPart);
        Assert.That(field.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(field.GetAttribute("disabled"), Is.Not.Null);
    }

    private IWebElement FindDateInputByLabel(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//label[contains(.,'{labelPart}')]/following::input[contains(@class,'flatpickr-input')][1]")));
    }

    private void AssertSpouseFields(string prefix)
    {
        Assert.That(FindByName($"{prefix}FirstName").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Emri").Text, Does.Contain("*"));
        Assert.That(FindByName($"{prefix}LastName").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Mbiemri").Text, Does.Contain("*"));
        Assert.That(FindByName($"{prefix}Nid").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("NID").Text, Does.Contain("*"));
        Assert.That(FindByName($"{prefix}FatherName").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Atësia").Text, Does.Contain("*"));
        Assert.That(FindByName($"{prefix}MotherName").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Amësia").Text, Does.Contain("*"));
        Assert.That(FindDateInputByLabel("Datëlindja").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindByName($"{prefix}PlaceOfBirth").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Vendlindja").Text, Does.Contain("*"));

        IWebElement civilStatus = FindByName($"{prefix}CivilStatus");
        var civilSelect = new SelectElement(civilStatus);
        Assert.That(civilSelect.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(civilSelect.Options[1].Text.Trim(), Is.EqualTo("Beqar"));
        Assert.That(civilSelect.Options[2].Text.Trim(), Is.EqualTo("I ve"));
        Assert.That(civilSelect.Options[3].Text.Trim(), Is.EqualTo("I divorcuar"));
        Assert.That(FindLabel("Gjendja Civile").Text, Does.Contain("*"));

        Assert.That(FindByName($"{prefix}Nationality").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Kombësia").Text, Does.Contain("*"));
        Assert.That(FindByName($"{prefix}Citizenship").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Shtetësia").Text, Does.Contain("*"));
        Assert.That(FindByName($"{prefix}Residence").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Vendqëndrimi").Text, Does.Contain("*"));
        Assert.That(FindByName($"{prefix}Domicile").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Vendbanimi").Text, Does.Contain("*"));
    }

    private void FillSpouseFields(string prefix, string nid, string birthDate, int year, int month, int day)
    {
        FillInput(FindByName($"{prefix}FirstName"), "test");
        FillInput(FindByName($"{prefix}LastName"), "test");
        FillInput(FindByName($"{prefix}Nid"), nid);
        FillInput(FindByName($"{prefix}FatherName"), "test");
        FillInput(FindByName($"{prefix}MotherName"), "test");
        FillDateByLabel("Datëlindja", birthDate, year, month, day);
        FillInput(FindByName($"{prefix}PlaceOfBirth"), "test");
        SelectByValue(FindByName($"{prefix}CivilStatus"), "1");
        FillInput(FindByName($"{prefix}Nationality"), "test");
        FillInput(FindByName($"{prefix}Citizenship"), "test");
        FillInput(FindByName($"{prefix}Residence"), "test");
        FillInput(FindByName($"{prefix}Domicile"), "test");
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

    private void FillDateByLabel(string labelPart, string displayDate, int year, int month, int day)
    {
        FillDate(FindDateInputByLabel(labelPart), displayDate, year, month, day);
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
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-9778"));
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
