using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("2306")]
public class _2306_ : QytetarNidF602TestBase
{
    protected override string ServiceCode => "2306";
    protected override string? ServiceTitle => "KerkeseTransferimDosjePensioni";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void KerkeseTransferimDosjePensioni()
    {
        Log("Assert page header");
        IWebElement headerContainer = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("div.page-header-container")));
        Assert.That(headerContainer.Displayed, Is.True, "Page header nuk eshte visible");

        IWebElement serviceName = wait.Until(ExpectedConditions.ElementIsVisible(
            By.Id("serviceNameBreadcrumb")));
        Assert.That(serviceName.Displayed, Is.True, "Breadcrumb i sherbimit nuk eshte visible");
        Assert.That(serviceName.Text.Trim(), Is.EqualTo("Kërkesë për transferim dosjeje përfitimi (pensioni)"),
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

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

        Log("Assert nje hap aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(1));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));

        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.ealb-header-text")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("KËRKESË PËR TRANSFERIM DOSJEJE PËRFITIMI (PENSIONI)"));

        Log("Assert NID eshte readonly dhe i para-plotesuar");
        IWebElement nidInput = FindInputByLabel("NID");
        Assert.That(nidInput.GetAttribute("value").Trim(), Is.EqualTo(CitizenNid));
        Assert.That(nidInput.GetAttribute("readonly"), Is.Not.Null);

        Log("Assert llojet e pensionit");
        Assert.That(driver.FindElement(By.CssSelector("label[for='pleqeri']")).Text.Trim(),
            Is.EqualTo("Pleqëri"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='invaliditet']")).Text.Trim(),
            Is.EqualTo("Invaliditet"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='familjar']")).Text.Trim(),
            Is.EqualTo("Familjar"));

        Assert.That(driver.FindElement(By.Id("pleqeri")).GetAttribute("value"), Is.EqualTo("pleqeri"));
        Assert.That(driver.FindElement(By.Id("invaliditet")).GetAttribute("value"), Is.EqualTo("invaliditet"));
        Assert.That(driver.FindElement(By.Id("familjar")).GetAttribute("value"), Is.EqualTo("familjar"));

        Assert.That(driver.FindElement(By.Id("pleqeri")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("invaliditet")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("familjar")).Selected, Is.False);

        Log("Assert opsionet suplementare");
        Assert.That(driver.FindElement(By.CssSelector("label[for='suplementar']")).Text.Trim(),
            Is.EqualTo("Suplementar"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='suplementarParakohshem']")).Text.Trim(),
            Is.EqualTo("Pension i parakohshëm për vjetërsi shërbimi"));

        Assert.That(driver.FindElement(By.Id("suplementar")).GetAttribute("value"),
            Is.EqualTo("suplementar"));
        Assert.That(driver.FindElement(By.Id("suplementarParakohshem")).GetAttribute("value"),
            Is.EqualTo("suplementar_i_parakohshem"));

        Assert.That(driver.FindElement(By.Id("suplementar")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("suplementarParakohshem")).Selected, Is.False);

        Log("Assert kartela e pensionit");
        IWebElement kartelaSelect = FindSelectByName("selectedPension");
        var kartela = new SelectElement(kartelaSelect);
        Assert.That(kartela.SelectedOption.GetAttribute("value"), Is.EqualTo("132655"));
        Assert.That(kartela.SelectedOption.Text.Trim(), Is.EqualTo("Kartele Pleqerie me nr. 132655"));

        Log("Assert te dhenat e dosjes se tanishme");
        Assert.That(FindBoldAfterSpan("nënshkruari/a").Text.Trim(), Is.EqualTo("Kadri Kukaj"));
        Assert.That(FindBoldAfterSpan("DRSSH-në").Text.Trim(), Is.EqualTo("Shkoder"));
        Assert.That(FindBoldAfterSpan("Agjencinë e Sigurimeve Shoqërore").Text.Trim(),
            Is.EqualTo("Shkoder"));
        Assert.That(FindBoldAfterSpan("Qendrën paguese").Text.Trim(), Is.EqualTo("FI Bank (SH)"));
        Assert.That(FindBoldAfterSpan("Bashkisë / Komunës").Text.Trim(), Is.EqualTo("SHKODËR"));

        Log("Assert fushat e adreses se re jane bosh");
        IWebElement targetMunicipality = FindInputByName("targetMunicipality");
        IWebElement neighborhood = FindInputByName("neighborhood");
        IWebElement street = FindInputByName("street");
        IWebElement buildingNo = FindInputByName("buildingNo");
        IWebElement apartmentNo = FindInputByName("apartmentNo");
        IWebElement entranceNo = FindInputByName("entranceNo");

        Assert.That(targetMunicipality.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(neighborhood.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(street.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(buildingNo.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(apartmentNo.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(entranceNo.GetAttribute("value"), Is.EqualTo(string.Empty));

        Assert.That(targetMunicipality.GetAttribute("maxlength"), Is.EqualTo("100"));
        Assert.That(neighborhood.GetAttribute("maxlength"), Is.EqualTo("100"));
        Assert.That(street.GetAttribute("maxlength"), Is.EqualTo("100"));
        Assert.That(buildingNo.GetAttribute("maxlength"), Is.EqualTo("12"));
        Assert.That(apartmentNo.GetAttribute("maxlength"), Is.EqualTo("12"));
        Assert.That(entranceNo.GetAttribute("maxlength"), Is.EqualTo("12"));

        Log("Assert DRSSH destinacion ka opsionet e drejtorive");
        IWebElement drsshSelect = FindSelectByName("drsshDestination");
        var drssh = new SelectElement(drsshSelect);
        Assert.That(drssh.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(drssh.Options.Count, Is.EqualTo(15));
        Assert.That(drssh.Options[1].GetAttribute("value"), Is.EqualTo("01"));
        Assert.That(drssh.Options[1].Text.Trim(), Is.EqualTo("Drejtoria Berat"));
        Assert.That(drssh.Options[10].GetAttribute("value"), Is.EqualTo("14"));
        Assert.That(drssh.Options[10].Text.Trim(), Is.EqualTo("Dega Sarande"));
        Assert.That(drssh.Options[11].GetAttribute("value"), Is.EqualTo("10"));
        Assert.That(drssh.Options[11].Text.Trim(), Is.EqualTo("Drejtoria Shkoder"));
        Assert.That(drssh.Options[12].GetAttribute("value"), Is.EqualTo("11"));
        Assert.That(drssh.Options[12].Text.Trim(), Is.EqualTo("Drejtoria Tirane"));
        Assert.That(drssh.Options[13].Text.Trim(), Is.EqualTo("Dega Tropoje"));
        Assert.That(drssh.Options[14].Text.Trim(), Is.EqualTo("Drejtoria Vlore"));

        Log("Assert Agjencia destinacion eshte disabled para zgjedhjes se DRSSH");
        IWebElement agencySelect = FindSelectByName("agencyDestination");
        Assert.That(agencySelect.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(new SelectElement(agencySelect).Options.Count, Is.EqualTo(1));
        Assert.That(new SelectElement(agencySelect).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));

        Log("Assert menyra e pageses");
        IWebElement paymentHint = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//div[contains(@class,'text-muted') and contains(.,'Zgjidhni DRSSH dhe ALSSH')]")));
        Assert.That(paymentHint.Text.Trim(),
            Is.EqualTo("Zgjidhni DRSSH dhe ALSSH për të zgjedhur mënyrën e pagesës."));

        Assert.That(driver.FindElement(By.CssSelector("label[for='qendraPaguese']")).Text.Trim(),
            Is.EqualTo("Qëndrës Paguese"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='bankAccount']")).Text.Trim(),
            Is.EqualTo("Bankës"));
        Assert.That(driver.FindElement(By.Id("qendraPaguese")).GetAttribute("value"),
            Is.EqualTo("qendraPaguese"));
        Assert.That(driver.FindElement(By.Id("bankAccount")).GetAttribute("value"),
            Is.EqualTo("bank"));
        Assert.That(driver.FindElement(By.Id("qendraPaguese")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("bankAccount")).Selected, Is.False);

        Log("Assert data dhe vendi");
        IWebElement dateLocation = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//span[contains(.,'më datë')]")));
        Assert.That(dateLocation.Text, Does.Contain("Shkoder"));
        Assert.That(dateLocation.Text, Does.Contain(DateTime.Now.ToString("dd.MM.yyyy")));

        Log("Assert butonat e navigimit");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(dergoBtn.Text.Trim(), Does.Contain("Dërgo"));

        Log("Kliko Dergo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error messages per DRSSH, Agjenci dhe menyren e pageses");
        var requiredFieldErrors = wait.Until(d =>
        {
            var items = d.FindElements(
                By.XPath("//form//*[normalize-space()='Plotësoni fushën për të vazhduar']"));
            return items.Count >= 2 ? items : null;
        });
        Assert.That(requiredFieldErrors.Count, Is.GreaterThanOrEqualTo(2));
        Assert.That(requiredFieldErrors[0].Text.Trim(), Is.EqualTo("Plotësoni fushën për të vazhduar"));
        Assert.That(requiredFieldErrors[1].Text.Trim(), Is.EqualTo("Plotësoni fushën për të vazhduar"));

        IWebElement paymentError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//small[contains(@class,'text-danger') and contains(.,'Zgjidh një menyrë pagese')]")));
        Assert.That(paymentError.Text.Trim(), Is.EqualTo("Zgjidh një menyrë pagese."));

        Log("Zgjidh llojin e pensionit: Pleqëri");
        SelectRadioById("pleqeri");
        Assert.That(driver.FindElement(By.Id("pleqeri")).Selected, Is.True);
        Assert.That(driver.FindElement(By.Id("invaliditet")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("familjar")).Selected, Is.False);

        Log("Ploteso adresen e re");
        FillInput(FindInputByName("targetMunicipality"), "Tiranë");
        FillInput(FindInputByName("neighborhood"), "1");
        FillInput(FindInputByName("street"), "Test");
        FillInput(FindInputByName("buildingNo"), "1");
        FillInput(FindInputByName("apartmentNo"), "2");
        FillInput(FindInputByName("entranceNo"), "2");

        Log("Zgjidh Drejtoria Tirane si DRSSH destinacion");
        SelectDropdownByValue(FindSelectByName("drsshDestination"), "11");

        Log("Wait qe Agjencia destinacion te aktivizohet");
        wait.Until(d =>
        {
            try
            {
                var agency = d.FindElement(By.CssSelector("form select[name='agencyDestination']"));
                return agency.GetAttribute("disabled") == null
                    && new SelectElement(agency).Options.Count > 1;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        IWebElement agencyEnabled = FindSelectByName("agencyDestination");
        var agencyOptions = new SelectElement(agencyEnabled);
        Assert.That(agencyEnabled.GetAttribute("disabled"), Is.Null);
        Assert.That(agencyOptions.Options.Count, Is.GreaterThan(1));

        Log("Zgjidh agjencine e pare te disponueshme");
        agencyOptions.SelectByIndex(1);
        Thread.Sleep(1000);

        Log("Zgjidh menyren e pageses: Qëndrës Paguese");
        SelectRadioById("qendraPaguese");
        Assert.That(driver.FindElement(By.Id("qendraPaguese")).Selected, Is.True);
        Assert.That(driver.FindElement(By.Id("bankAccount")).Selected, Is.False);

        Log("Assert fushat e plotesuara");
        Assert.That(FindInputByName("targetMunicipality").GetAttribute("value").Trim(),
            Is.EqualTo("Tiranë"));
        Assert.That(FindInputByName("neighborhood").GetAttribute("value").Trim(), Is.EqualTo("1"));
        Assert.That(FindInputByName("street").GetAttribute("value").Trim(), Is.EqualTo("Test"));
        Assert.That(FindInputByName("buildingNo").GetAttribute("value").Trim(), Is.EqualTo("1"));
        Assert.That(FindInputByName("apartmentNo").GetAttribute("value").Trim(), Is.EqualTo("2"));
        Assert.That(FindInputByName("entranceNo").GetAttribute("value").Trim(), Is.EqualTo("2"));
        Assert.That(new SelectElement(FindSelectByName("drsshDestination")).SelectedOption
            .GetAttribute("value"), Is.EqualTo("11"));

        //Log("Kliko Dergo");
        //SafeClick(By.CssSelector("button.ealb-btn-continue"));
        //Thread.Sleep(5000);

        Log("TEST PASSED");
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

    private IWebElement FindInputByLabel(string labelText)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//form//label[normalize-space()='{labelText}']/following-sibling::input")));
    }

    private IWebElement FindInputByName(string name)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector($"form input[name='{name}']")));
    }

    private IWebElement FindSelectByName(string name)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector($"form select[name='{name}']")));
    }

    private IWebElement FindBoldAfterSpan(string spanPart)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//form//span[contains(.,'{spanPart}')]/following-sibling::span[contains(@class,'fw-bold')][1]")));
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

    private void SelectRadioById(string radioId)
    {

        SafeClick(By.Id(radioId));
        Thread.Sleep(500);
    }
}