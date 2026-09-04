using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Biznes.DPSHTRR_AMS;

[Category("DPSHTRR-AMS")]
[Category("10091")]
public class _10091_ : BiznesTestBase
{
    protected override string ServiceCode => "10091";
    protected override string? ServiceTitle => "PajisjemeDAPperMakineriteeRenda";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Aplikim për pajisje me Dëshmi Aftësie Profesionale për makineritë e rënda";
    private const int TotalSteps = 4;

    [Test]
    public void PajisjemeDAPperMakineriteeRenda()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert Title Step 1");
        IWebElement Step1Title = WaitForStepTitle("TË DHËNAT E SUBJEKTIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNAT E SUBJEKTIT"));

        Log("Assert kohëzgjatja");
        AssertDuration("3 minuta kohëzgjatje");

        Log("Assert 4 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert te dhenat e SUBJEKTIT");
        AssertDisabledById("nipt", "M53330201S");
        Assert.That(FindLabel("NIPT").Displayed, Is.True);

        AssertDisabledById("subjectName", "Migen Dërstila");
        Assert.That(FindLabel("Emri i subjektit").Displayed, Is.True);

        AssertDisabledById("administrator", "Migen  Luan  Dërstila");
        Assert.That(FindLabel("Përfaqësuesi ligjor").Displayed, Is.True);

        AssertDisabledById("phoneNumber", "+355684053531");
        Assert.That(FindLabel("Nr Cel").Displayed, Is.True);

        AssertDisabledById("email", "migen.derstila@kreatx.com");
        Assert.That(FindLabel("Email").Displayed, Is.True);

        IWebElement adresa = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("address")));
        Assert.That(adresa.GetAttribute("value").Trim(),
            Is.EqualTo("Derstile; ; ; ; Gjinar; ; 0000; Elbasan,Elbasan,ELBASAN,Elbasan"));
        Assert.That(adresa.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindLabel("Adresa").Displayed, Is.True);

        Log("Assert butonat e navigimit Step 1");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(4000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("TË DHËNAT E KANDIDATIT");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNAT E KANDIDATIT"));

        Log("Assert kohëzgjatja Step 2");
        AssertDuration("3 minuta kohëzgjatje");

        Log("Assert 4 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert fushat e kandidatit");
        IWebElement nid = FindInputByLabel("Nid");
        Assert.That(nid.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Nid").Text, Does.Contain("*"));

        AssertDisabledByLabel("Emri", string.Empty);
        AssertDisabledByLabel("Mbiemri", string.Empty);
        AssertDisabledByLabel("Atësia", string.Empty);
        AssertDisabledByLabel("Datëlindja", string.Empty);
        AssertDisabledByLabel("Gjinia", string.Empty);
        AssertDisabledByLabel("Adresa", string.Empty);

        IWebElement nrCel = FindInputByLabel("Nr Cel");
        Assert.That(nrCel.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(nrCel.GetAttribute("disabled"), Is.Null);

        IWebElement emailKandidati = FindInputByLabel("Email");
        Assert.That(emailKandidati.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(emailKandidati.GetAttribute("disabled"), Is.Null);

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar te dhenat e kandidatit");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso te dhenat e kandidatit");
        nid = FindInputByLabel("Nid");
        nid.Clear();
        nid.SendKeys("J55728107R");
        nid.SendKeys(Keys.Tab);
        WaitUntilInputByLabelEquals("Emri", "Ketjona");

        Log("Assert te dhenat e KANDIDATIT");
        AssertDisabledByLabel("Emri", "Ketjona");
        AssertDisabledByLabel("Mbiemri", "Mema");
        AssertDisabledByLabel("Atësia", "Mersin");
        AssertDisabledByLabel("Datëlindja", "28.07.1995");
        AssertDisabledByLabel("Gjinia", "Femër");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(4000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("TË DHËNAT E LEJES SË DREJTIMIT QË DISPONON");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNAT E LEJES SË DREJTIMIT QË DISPONON"));

        Log("Assert kohëzgjatja Step 3");
        AssertDuration("3 minuta kohëzgjatje");

        Log("Assert 4 hapa, tre te paret aktiv");
        AssertSteps(3);

        Log("Assert te dhenat e lejes se drejtimit");
        AssertDisabledByLabel("Kategoritë", "B");
        AssertDisabledByLabel("Data e lëshimit", "03.06.2022");
        AssertDisabledByLabel("Data e vlefshmërisë", "02.06.2032");

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(4000);

        Log("Assert Step 4 Title");
        IWebElement Step4Title = WaitForStepTitle("LLOJI I DAP");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("LLOJI I DAP"));

        Log("Assert kohëzgjatja Step 4");
        AssertDuration("3 minuta kohëzgjatje");

        Log("Assert 4 hapa, te gjithe aktiv");
        AssertSteps(4);

        Log("Assert fushat e llojit te DAP");
        IWebElement llojiDap = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("select[name='llojiDAP']")));
        var llojiDapSelect = new SelectElement(llojiDap);
        Assert.That(llojiDapSelect.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Lloji i DAP").Text, Does.Contain("*"));
        Assert.That(llojiDapSelect.Options.Any(o => o.GetAttribute("value") == "Automakinist"), Is.True);
        Assert.That(llojiDapSelect.Options.Any(o => o.GetAttribute("value") == "Buldozerist"), Is.True);
        Assert.That(llojiDapSelect.Options.Any(o => o.GetAttribute("value") == "Ekskavtorist me Gome"), Is.True);
        Assert.That(llojiDapSelect.Options.Any(o => o.GetAttribute("value") == "Ekskavatorist me Zinxhir"), Is.True);

        IWebElement drshtrr = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("select[name='drshtrr']")));
        var drshtrrSelect = new SelectElement(drshtrr);
        Assert.That(drshtrrSelect.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("DRSHTRR").Text, Does.Contain("*"));
        Assert.That(drshtrrSelect.Options.Any(o => o.GetAttribute("value") == "11"), Is.True);
        Assert.That(drshtrrSelect.Options.Any(o => o.Text.Trim() == "Tiranë"), Is.True);

        Log("Assert butonat e navigimit Step 4");
        AssertNavigationButtons("Dërgo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue.with-arrow")));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Kliko Dergo pa plotesuar llojin e dap");
        SafeClick(By.CssSelector("button.ealb-btn-continue.with-arrow"));
        AssertFieldError("Përzgjidhni një vlerë për të vazhduar");

        Log("Zgjidh llojin e DAP");
        SelectByValueSafe(By.CssSelector("select[name='llojiDAP']"), "Automakinist");
        SelectByValueSafe(By.CssSelector("select[name='drshtrr']"), "11");

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
            var titles = d.FindElements(By.CssSelector("h4.text-uppercase, h5.text-uppercase"));
            foreach (var title in titles)
            {
                string actual = title.Text.Trim().ToUpperInvariant();
                if (actual == expectedUpper || actual.StartsWith(expectedUpper))
                    return title;
            }
            return null;
        });
    }

    private IWebElement FindLabel(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//form//label[contains(.,'{labelPart}')]")));
    }

    private IWebElement FindInputByLabel(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//form//label[contains(.,'{labelPart}')]/following-sibling::*[self::input or self::textarea]")));
    }

    private void AssertDisabledById(string id, string expectedValue)
    {
        IWebElement field = wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
        Assert.That(field.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(field.GetAttribute("disabled"), Is.Not.Null);
    }

    private void AssertDisabledByLabel(string labelPart, string expectedValue)
    {
        IWebElement field = FindInputByLabel(labelPart);
        Assert.That(field.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(field.GetAttribute("disabled"), Is.Not.Null);
    }

    private void WaitUntilInputByLabelEquals(string labelPart, string expectedValue)
    {
        var lookupWait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        lookupWait.Until(d =>
        {
            try
            {
                var fields = d.FindElements(By.XPath(
                    $"//form//label[contains(.,'{labelPart}')]/following-sibling::*[self::input or self::textarea]"));
                return fields.Count > 0
                    && string.Equals(fields[0].GetAttribute("value")?.Trim(), expectedValue, StringComparison.Ordinal);
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });
    }

    private void WaitUntilOptionExists(By selectLocator, string optionValue)
    {
        wait.Until(d =>
        {
            try
            {
                var selectElement = new SelectElement(d.FindElement(selectLocator));
                return selectElement.Options.Any(o =>
                    string.Equals(
                        (o.GetAttribute("value") ?? string.Empty).Trim(),
                        optionValue,
                        StringComparison.OrdinalIgnoreCase));
            }
            catch
            {
                return false;
            }
        });
    }

    private void SelectByValueSafe(By selectLocator, string optionValue)
    {
        WaitUntilOptionExists(selectLocator, optionValue);

        IWebElement dropdown = wait.Until(ExpectedConditions.ElementIsVisible(selectLocator));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            dropdown);
        Thread.Sleep(500);

        var select = new SelectElement(dropdown);
        Log($"Po zgjedh value '{optionValue}' tek {selectLocator}");
        select.SelectByValue(optionValue);
        Thread.Sleep(1000);
    }
}
