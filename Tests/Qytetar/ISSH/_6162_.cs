using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("6162")]
public class _6162_ : QytetarNidF602TestBase
{
    protected override string ServiceCode => "6162";
    protected override string? ServiceTitle => "AplikimMbylljePensioni";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void AplikimMbylljePensioni()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert tre hapa, i pari aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(3));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));
        Assert.That(steps[1].GetAttribute("class"), Does.Not.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("no-click"));
        Assert.That(steps[2].GetAttribute("class"), Does.Not.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Contain("no-click"));

        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.px-4.pb-4.text-uppercase")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("KËRKESË PËR MBYLLJE PENSIONI"));

        Log("Assert titulli Apliko si");
        IWebElement aplikoSi = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//h5[normalize-space()='Apliko si']")));
        Assert.That(aplikoSi.Displayed, Is.True);

        Log("Assert opsionet e radios");
        IWebElement selfRadio = wait.Until(ExpectedConditions.ElementExists(By.Id("self")));
        IWebElement familyRadio = wait.Until(ExpectedConditions.ElementExists(By.Id("family")));

        Assert.That(selfRadio.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(selfRadio.GetAttribute("name"), Is.EqualTo("applicationType"));
        Assert.That(selfRadio.GetAttribute("value"), Is.EqualTo("self"));
        Assert.That(selfRadio.Selected, Is.False);

        Assert.That(familyRadio.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(familyRadio.GetAttribute("name"), Is.EqualTo("applicationType"));
        Assert.That(familyRadio.GetAttribute("value"), Is.EqualTo("family"));
        Assert.That(familyRadio.Selected, Is.False);

        Assert.That(driver.FindElement(By.CssSelector("label[for='self']")).Text.Trim(),
            Is.EqualTo("Vetë Pensionist (kur ka filluar punë)"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='family']")).Text.Trim(),
            Is.EqualTo("Familjari Pensionistit (në rast vdekjeje)"));

        Log("Assert butonat e navigimit");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Kliko Vazhdo pa zgjedhur opsionin e aplikimit");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error message per llojin e aplikimit");
        IWebElement typeError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//div[contains(@class,'text-danger')]")));
        Assert.That(typeError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));

        Log("Zgjidh Vete Pensionist");
        SelectRadioById("self");

        Assert.That(driver.FindElement(By.Id("self")).Selected, Is.True);
        Assert.That(driver.FindElement(By.Id("family")).Selected, Is.False);

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("TË DHËNAT E APLIKIMIT");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("TË DHËNAT E APLIKIMIT"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 3 hapa, dy te paret aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(3));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Not.Contain("active"));

        Log("Assert NID eshte disabled, maxlength 10 dhe i para-plotesuar");
        IWebElement nidInput = FindInputByLabel("NID");
        Assert.That(nidInput.GetAttribute("value").Trim(), Is.EqualTo(CitizenNid));
        Assert.That(nidInput.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(nidInput.GetAttribute("maxlength"), Is.EqualTo("10"));

        Log("Assert DRSSH ka opsionet e drejtorive");
        AssertDrsshDirectorates();

        Log("Assert ALSSH ka vetem opsionin bosh para zgjedhjes se DRSSH");
        AssertAlsshEmpty();

        Log("Assert checkbox-et e llojit te pensionit");
        Assert.That(driver.FindElement(By.CssSelector("label[for='pleqeri']")).Text.Trim(),
            Is.EqualTo("Pleqëri"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='invaliditet']")).Text.Trim(),
            Is.EqualTo("Invaliditet"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='familjar']")).Text.Trim(),
            Is.EqualTo("Familjar"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='suplementar']")).Text.Trim(),
            Is.EqualTo("Suplementar"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='parakohshem']")).Text.Trim(),
            Is.EqualTo("Pension i parakohshëm për vjetërsi shërbimi"));

        Assert.That(driver.FindElement(By.Id("pleqeri")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("invaliditet")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("familjar")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("suplementar")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("parakohshem")).Selected, Is.False);

        Log("Assert emri i aplikantit dhe teksti i mbylljes se dosjes");
        IWebElement applicantName = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//p[contains(@class,'text-muted')]//strong")));
        Assert.That(applicantName.Text.Trim(), Is.EqualTo("Kadri Kukaj"));

        IWebElement closeText = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//p[contains(@class,'text-muted')]")));
        Assert.That(closeText.Text, Does.Contain("Unë i/e nënshkruari/a"));
        Assert.That(closeText.Text, Does.Contain("të mbyllet dosja e pensionit me Nr."));
        Assert.That(closeText.Text, Does.Contain("për shkak se kam rifilluar punë"));

        Log("Assert dropdown i dosjes eshte bosh");
        IWebElement dosjeSelect = FindDosjeSelect();
        Assert.That(new SelectElement(dosjeSelect).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(new SelectElement(dosjeSelect).Options.Count, Is.EqualTo(1));

        Log("Kliko Vazhdo pa zgjedhur DRSSH, ALSSH dhe dosjen");
        AssertDrsshAlsshRequiredErrors();

        Log("Zgjidh Drejtoria Tirane dhe ALSSH");
        SelectDrsshTiraneAndAlssh();

        Log("Zgjidh Pleqeri");
        SafeClick(By.CssSelector("label[for='pleqeri']"));
        Assert.That(driver.FindElement(By.Id("pleqeri")).Selected, Is.True);

        Log("Wait qe dropdown i dosjes te mbushet");
        wait.Until(d =>
        {
            try
            {
                var dosje = d.FindElement(
                    By.XPath("//form//p[contains(.,'dosja e pensionit')]//select"));
                return new SelectElement(dosje).Options.Count > 1;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        dosjeSelect = FindDosjeSelect();
        var dosje = new SelectElement(dosjeSelect);
        Assert.That(dosje.Options.Count, Is.GreaterThan(1));

        Log("Zgjidh dosjen e pare te disponueshme");
        dosje.SelectByIndex(1);
        Thread.Sleep(500);

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        AssertAndFillAddressStep();

        Log("TEST PASSED");
    }

    [Test]
    public void AplikimMbylljePensioniFamiljar()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.px-4.pb-4.text-uppercase")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("KËRKESË PËR MBYLLJE PENSIONI"));

        Log("Zgjidh Familjari Pensionistit");
        SelectRadioById("family");
        Assert.That(driver.FindElement(By.Id("family")).Selected, Is.True);
        Assert.That(driver.FindElement(By.Id("self")).Selected, Is.False);

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert kohëzgjatja Step 2");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 3 hapa, dy te paret aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(3));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Not.Contain("active"));

        Log("Assert Step 2 Title");
        IWebElement Step2Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.px-4.pb-4.text-uppercase")));
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("TË DHËNAT E APLIKIMIT"));

        Log("Assert NID eshte bosh, i editueshem dhe maxlength 10");
        IWebElement nidInput = FindInputByLabel("NID");
        Assert.That(nidInput.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(nidInput.GetAttribute("disabled"), Is.Null);
        Assert.That(nidInput.GetAttribute("readonly"), Is.Null);
        Assert.That(nidInput.GetAttribute("maxlength"), Is.EqualTo("10"));

        Log("Assert DRSSH ka opsionet e drejtorive");
        AssertDrsshDirectorates();

        Log("Assert ALSSH ka vetem opsionin bosh para zgjedhjes se DRSSH");
        AssertAlsshEmpty();

        Log("Assert checkbox-et e llojit te pensionit");
        Assert.That(driver.FindElement(By.CssSelector("label[for='pleqeri']")).Text.Trim(),
            Is.EqualTo("Pleqëri"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='invaliditet']")).Text.Trim(),
            Is.EqualTo("Invaliditet"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='familjar']")).Text.Trim(),
            Is.EqualTo("Familjar"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='suplementar']")).Text.Trim(),
            Is.EqualTo("Suplementar"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='parakohshem']")).Text.Trim(),
            Is.EqualTo("Pension i parakohshëm për vjetërsi shërbimi"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='shpenzimeFunerale']")).Text.Trim(),
            Is.EqualTo("Pagesë për shpenzime funerale"));

        Assert.That(driver.FindElement(By.Id("pleqeri")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("invaliditet")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("familjar")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("suplementar")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("parakohshem")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("shpenzimeFunerale")).Selected, Is.False);

        Log("Assert emri i aplikantit dhe teksti i mbylljes se dosjes");
        IWebElement closeText = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//p[contains(@class,'text-muted')]")));
        Assert.That(closeText.Text, Does.Contain("Unë i/e nënshkruari/a"));
        Assert.That(closeText.FindElement(By.CssSelector("strong")).Text.Trim(),
            Is.EqualTo("Kadri Kukaj"));
        Assert.That(closeText.Text, Does.Contain("të mbyllet dosja e pensionit e të ndjerit/ndjeres"));
        Assert.That(closeText.Text, Does.Contain("dhe nr dosje"));

        Log("Assert NID e te ndjerit eshte disabled dhe bosh");
        IWebElement deceasedNid = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//form//p[contains(@class,'text-muted')]//input")));
        Assert.That(deceasedNid.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(deceasedNid.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Assert dropdown i dosjes eshte bosh");
        IWebElement dosjeSelect = FindDosjeSelect();
        var dosje = new SelectElement(dosjeSelect);
        Assert.That(dosje.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(dosje.Options.Count, Is.EqualTo(1));

        Log("Assert butonat e navigimit Step 2");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Kliko Vazhdo pa zgjedhur DRSSH, ALSSH dhe dosjen");
        AssertDrsshAlsshRequiredErrors();

        Log("Ploteso NID e pensionistit");
        FillInput(FindInputByLabel("NID"), CitizenNid);
        Assert.That(FindInputByLabel("NID").GetAttribute("value").Trim(), Is.EqualTo(CitizenNid));

        Log("Zgjidh Drejtoria Tirane dhe ALSSH");
        SelectDrsshTiraneAndAlssh();

        Log("Zgjidh Pleqeri");
        SafeClick(By.CssSelector("label[for='pleqeri']"));
        Assert.That(driver.FindElement(By.Id("pleqeri")).Selected, Is.True);

        Log("Wait qe dropdown i dosjes te mbushet");
        wait.Until(d =>
        {
            try
            {
                var dosjeEl = d.FindElement(
                    By.XPath("//form//p[contains(.,'dosja e pensionit')]//select"));
                return new SelectElement(dosjeEl).Options.Count > 1;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        dosjeSelect = FindDosjeSelect();
        dosje = new SelectElement(dosjeSelect);
        Assert.That(dosje.Options.Count, Is.GreaterThan(1));

        Log("Zgjidh dosjen e pare te disponueshme");
        dosje.SelectByIndex(1);
        Thread.Sleep(500);

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        AssertAndFillAddressStep();

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        AssertFamilyDocumentationStep();

        Log("TEST PASSED");
    }

    private void SelectRadioById(string radioId)
    {

        SafeClick(By.Id(radioId));
        Thread.Sleep(500);
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

    private void OpenNewApplicationFromServicePage()
    {
        Log("Assert page header");
        IWebElement headerContainer = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("div.page-header-container")));
        Assert.That(headerContainer.Displayed, Is.True, "Page header nuk eshte visible");

        IWebElement serviceName = wait.Until(ExpectedConditions.ElementIsVisible(
            By.Id("serviceNameBreadcrumb")));
        Assert.That(serviceName.Displayed, Is.True, "Breadcrumb i sherbimit nuk eshte visible");
        Assert.That(serviceName.Text.Trim(), Is.EqualTo("Aplikim për mbyllje pensioni"),
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
    }

    private IWebElement WaitForStepTitle(string expectedUpper)
    {

        return wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4.text-uppercase"));
            if (titles.Count == 0)
                return null;
            return titles[0].Text.Trim().ToUpperInvariant() == expectedUpper
                ? titles[0]
                : null;
        });
    }

    private IWebElement FindInputByLabel(string labelPart)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//div[@id='root']//form//label[contains(.,'{labelPart}')]/following-sibling::input")));
    }

    private IWebElement FindSelectByLabel(string labelPart)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//div[@id='root']//form//label[contains(.,'{labelPart}')]/following-sibling::select")));
    }

    private IWebElement FindDosjeSelect()
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//form//p[contains(.,'dosja e pensionit')]//select")));
    }

    private void SelectDropdownByValue(IWebElement select, string value)
    {

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            select
        );

        Thread.Sleep(300);
        new SelectElement(select).SelectByValue(value);
        Thread.Sleep(500);
    }

    private void AssertDrsshDirectorates()
    {

        IWebElement drsshSelect = FindSelectByLabel("DRSSH");
        var drssh = new SelectElement(drsshSelect);
        Assert.That(drssh.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(drssh.Options.Count, Is.EqualTo(15));
        Assert.That(drssh.Options[1].GetAttribute("value"), Is.EqualTo("01"));
        Assert.That(drssh.Options[1].Text.Trim(), Is.EqualTo("Drejtoria Berat"));
        Assert.That(drssh.Options[11].GetAttribute("value"), Is.EqualTo("11"));
        Assert.That(drssh.Options[11].Text.Trim(), Is.EqualTo("Drejtoria Tirane"));
        Assert.That(drssh.Options[13].Text.Trim(), Is.EqualTo("Dega Tropoje"));
        Assert.That(drssh.Options[14].Text.Trim(), Is.EqualTo("Dega Sarande"));
    }

    private void AssertAlsshEmpty()
    {

        IWebElement alsshSelect = FindSelectByLabel("ALSSH");
        Assert.That(new SelectElement(alsshSelect).Options.Count, Is.EqualTo(1));
        Assert.That(new SelectElement(alsshSelect).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
    }

    private void AssertDrsshAlsshRequiredErrors()
    {

        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        IWebElement drsshError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//label[contains(.,'DRSSH')]/following-sibling::span[contains(@class,'text-danger')]")));
        Assert.That(drsshError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));

        IWebElement alsshError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//label[contains(.,'ALSSH')]/following-sibling::span[contains(@class,'text-danger')]")));
        Assert.That(alsshError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));
    }

    private void SelectDrsshTiraneAndAlssh()
    {

        SelectDropdownByValue(FindSelectByLabel("DRSSH"), "11");

        wait.Until(d =>
        {
            try
            {
                var agency = d.FindElement(
                    By.XPath("//form//label[contains(.,'ALSSH')]/following-sibling::select"));
                return new SelectElement(agency).Options.Count > 1;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        IWebElement alsshEnabled = FindSelectByLabel("ALSSH");
        var alsshOptions = new SelectElement(alsshEnabled);
        Assert.That(alsshOptions.Options.Count, Is.GreaterThan(1));

        IWebElement? kavajeOption = null;
        foreach (var option in alsshOptions.Options)
        {
            if (option.Text.IndexOf("Kavaj", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                kavajeOption = option;
                break;
            }
        }

        if (kavajeOption != null)
            alsshOptions.SelectByValue(kavajeOption.GetAttribute("value"));
        else
            alsshOptions.SelectByIndex(1);
        Thread.Sleep(500);
    }

    private void AssertReadonlyDisabledValue(string labelPart, string expectedValue)
    {

        IWebElement input = FindInputByLabel(labelPart);
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(input.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(input.GetAttribute("disabled"), Is.Not.Null);
    }

    private void AssertAndFillAddressStep()
    {

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("ADRESA E KËRKUESIT");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("ADRESA E KËRKUESIT"));

        Log("Assert kohëzgjatja Step 3");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 3 hapa, te tre aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(3));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Contain("active"));

        Log("Assert fushat readonly te adreses");
        AssertReadonlyDisabledValue("Njësia Administrative", "MALËSI E MADHE");
        AssertReadonlyDisabledValue("Fshati", "MALËSI E MADHE");
        AssertReadonlyDisabledValue("Lagjia",
            "PALVAR KOPLIK 03690059; Nd. 69; H. 1; ; QENDËR; BOGIÇ-PALVAR; 4303; MALËSI E MADHE");
        AssertReadonlyDisabledValue("Nr. Tel.", "0676041404");
        AssertReadonlyDisabledValue("Email", "ketjona.mema@kreatx.com");

        Log("Assert fushat e editueshme jane bosh");
        Assert.That(FindInputByLabel("Pall. Nr.").GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindInputByLabel("Shk.Nr.").GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindInputByLabel("Ap.Nr").GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindInputByLabel("Rruga").GetAttribute("value"), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 3");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Ploteso Adresa e kerkuesit");
        FillInput(FindInputByLabel("Pall. Nr."), "1");
        FillInput(FindInputByLabel("Shk.Nr."), "2");
        FillInput(FindInputByLabel("Ap.Nr"), "2");
        FillInput(FindInputByLabel("Rruga"), "Test");
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {

        Assert.That(driver.FindElement(
            By.XPath($"//span[normalize-space()='{documentTitle}']")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-6162"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("25"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("25"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf,.jpg,.jpeg,.png"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të ngarkuar dokumentin"));

        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të ngarkuar dokumentin"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='hint']")).Text.Trim(),
            Is.EqualTo("Formatet e lejuara: PDF, JPG, JPEG, PNG. Madhesia maksimale: 25MB."));
    }

    private void AssertFamilyDocumentationStep()
    {

        Log("Assert Step Dokumentacioni Title");
        IWebElement docsTitle = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(docsTitle.Text.Trim().ToUpperInvariant(), Is.EqualTo("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Dokumentacioni");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 3 hapa, te tre aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(3));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Contain("active"));

        Log("Assert document-upload Fatura e shpenzimeve te varrimit");
        AssertDocumentUpload("6162-fuFatura", "Fatura e shpenzimeve të varrimit");

        Log("Assert document-upload Prokure e posacme");
        AssertDocumentUpload("6162-fuProkure", "Prokurë e posaçme");

        Log("Assert document-upload Deshmi trashegimie/Testament");
        AssertDocumentUpload("6162-fuTestament",
            "Dëshmi trashëgimie/Testament (kur pensionisti ka më tepër se një këst mujor pensioni pa tërhequr)");

        Log("Assert document-upload Te tjera");
        AssertDocumentUpload("6162-fuOthersVdekje", "Të tjera");

        Log("Assert dokumentet e administrates");
        Assert.That(driver.FindElement(
            By.XPath("//h5[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës')]")).Displayed,
            Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//label[contains(.,'Certifikatë vdekje me shënimin: \"Për sigurimet shoqërore\"')]")).Displayed,
            Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//label[contains(.,'Certifikatë vdekje me shënimin: \"Shpenzime varrimi\"')]")).Displayed,
            Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//label[contains(.,'Certifikatë vdekje me shënimin: \"Dokument\"')]")).Displayed,
            Is.True);

        Log("Assert checkbox i pranimit eshte i pazgjedhur");
        IWebElement agreeCheck = wait.Until(ExpectedConditions.ElementExists(By.Id("agreeCheck")));
        Assert.That(agreeCheck.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='agreeCheck']")).Text.Trim(),
            Does.Contain("Me klikimin e këtij butoni, ju bini dakord që këto dokumente të sigurohen për ju nga nëpunësi i administratës."));

        Log("Kliko Apliko pa pranuar kushtet");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(1000);
        Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase"))).Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DOKUMENTACIONI"));

        Log("Zgjidh pranimin e kushteve");
        SafeClick(By.Id("agreeCheck"));
        Assert.That(driver.FindElement(By.Id("agreeCheck")).Selected, Is.True);

        Log("Assert butoni Apliko");
        IWebElement aplikoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(aplikoBtn.Text.Trim(), Does.Contain("Apliko"));
        Assert.That(aplikoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        //Log("Kliko Apliko");
        //SafeClick(By.CssSelector("button.ealb-btn-continue"));
        //Thread.Sleep(5000);

        //Log("Assert suksesi");
        //IWebElement successTitle = wait.Until(ExpectedConditions.ElementIsVisible(
        //    By.XPath("//h5[contains(.,'APLIKIMI JUAJ')]")));
        //Assert.That(successTitle.Text.Trim().ToUpperInvariant().Replace("Ë", "E"),
        //    Does.Contain("APLIKIMI JUAJ U DERGUA ME SUKSES"));

        //IWebElement referenceNumber = wait.Until(ExpectedConditions.ElementIsVisible(
        //    By.XPath("//h6[contains(.,'Numri referencë i aplikimit')]")));
        //Assert.That(referenceNumber.Text, Does.Contain("6162-"));
        //Assert.That(driver.Url, Does.Contain("/mesazh"));
    }
}