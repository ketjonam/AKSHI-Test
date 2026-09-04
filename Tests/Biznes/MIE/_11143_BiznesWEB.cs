using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Biznes.MIE;

[Category("MIE")]
[Category("11143")]
public class _11143_BiznesWEB : BiznesTestBase
{
    protected override string ServiceCode => "11143";
    protected override string? ServiceTitle => "Mbyllje_Aktiviteti_11143";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Aplikim për mbylljen e aktivitetit të shoqërisë në fushën e studimit e projektimit dhe/ose mbikëqyrjes e kolaudimit";
    private const int TotalSteps = 2;

    [Test]
    public void Mbyllje_Aktiviteti_11143()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert Title Step 1");
        IWebElement Step1Title = WaitForStepTitle("DETAJET E SUBJEKTIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("DETAJET E SUBJEKTIT"));

        Log("Assert kohëzgjatja");
        AssertDuration("2 minuta kohëzgjatje");

        Log("Assert 2 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert te dhenat e subjektit");
        AssertDisabledById("nipt", "M53330201S");
        Assert.That(FindLabel("NIPT").Text, Does.Contain("NIPT"));
        AssertDisabledById("emriSubjektit", "Migen Dërstila");
        Assert.That(FindLabel("Emri i subjektit").Text, Does.Contain("Emri i subjektit"));
        AssertDisabledById("dataRregjistrimit", "30.09.2025");
        Assert.That(FindLabel("Dt. e regjistrimit të subjektit").Text,
            Does.Contain("Dt. e regjistrimit të subjektit"));
        AssertDisabledById("perfaqesuesi", "Migen  Luan  Dërstila");
        Assert.That(FindLabel("Përfaqësuesi ligjor").Text, Does.Contain("Përfaqësuesi ligjor"));
        AssertDisabledById("statusi", "Aktiv");
        Assert.That(FindLabel("Statusi i subjektit").Text, Does.Contain("Statusi i subjektit"));

        IWebElement adresa2 = wait.Until(ExpectedConditions.ElementExists(By.Id("secondAddress")));
        Assert.That(InputValue(adresa2), Is.EqualTo(string.Empty));
        Assert.That(adresa2.GetAttribute("disabled"), Is.Null);
        Assert.That(FindLabel("Adresa 2").Text, Does.Contain("Adresa 2"));

        AssertDisabledById(
            "adresa",
            "Derstile; ; ; ; Gjinar; ; 0000; Elbasan,Elbasan,ELBASAN,Elbasan");
        Assert.That(driver.FindElement(By.XPath("//label[normalize-space()='Adresa']")).Displayed, Is.True);

        IWebElement veprimtaria = wait.Until(ExpectedConditions.ElementExists(By.Id("veprimtaria")));
        Assert.That(InputValue(veprimtaria),
            Is.EqualTo("Tregtia me pakicë me porosi me mail ose nëpërmjet internetit dhe Sherbime të programimit informatik"));
        Assert.That(veprimtaria.GetAttribute("disabled") ?? veprimtaria.GetAttribute("readonly"),
            Is.Not.Null);
        Assert.That(FindLabel("Veprimtaria e subjektit").Text, Does.Contain("Veprimtaria e subjektit"));

        Log("Assert butonat e navigimit Step 1");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("KONTAKTI");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(), Does.Contain("KONTAKTI"));

        Log("Assert kohëzgjatja Step 2");
        AssertDuration("2 minuta kohëzgjatje");

        Log("Assert 2 hapa, te dy aktiv");
        AssertSteps(2);

        Log("Assert te dhenat e kontaktit");
        IWebElement nrTel = wait.Until(ExpectedConditions.ElementExists(By.Name("phone")));
        Assert.That(InputValue(nrTel), Is.EqualTo(string.Empty));
        Assert.That(nrTel.GetAttribute("disabled"), Is.Null);
        Assert.That(FindLabel("Nr. tel").Text, Does.Contain("Nr. tel"));

        IWebElement nrCel = wait.Until(ExpectedConditions.ElementExists(By.Name("mobile")));
        Assert.That(InputValue(nrCel), Is.EqualTo("+355684053531"));
        Assert.That(nrCel.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindLabel("Nr. cel").Text, Does.Contain("Nr. cel"));

        IWebElement email = wait.Until(ExpectedConditions.ElementExists(By.Name("email")));
        Assert.That(email.GetAttribute("type"), Is.EqualTo("email"));
        Assert.That(InputValue(email), Is.EqualTo("migen.derstila@kreatx.com"));
        Assert.That(email.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindLabel("Email").Text, Does.Contain("Email"));

        Log("Assert checkbox i konfirmimit");
        IWebElement confirmClosure = wait.Until(ExpectedConditions.ElementExists(By.Id("confirmClosure")));
        Assert.That(confirmClosure.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='confirmClosure']")).Text.Trim(),
            Is.EqualTo("Konfirmoj mbylljen e aktivitetit"));

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Dërgo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Kliko Dergo pa konfirmuar mbylljen");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(1000);
        Assert.That(WaitForStepTitle("KONTAKTI").Text.Trim().ToUpperInvariant(),
            Does.Contain("KONTAKTI"));

        Log("Zgjidh konfirmimin e mbylljes");
        ClickMuiCheckbox("confirmClosure");
        Assert.That(driver.FindElement(By.Id("confirmClosure")).Selected, Is.True);

        ClickDergo();
        AssertDergoOutcomeLenient();

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
                try
                {
                    string actual = title.Text.Trim().ToUpperInvariant();
                    if (actual == expectedUpper || actual.StartsWith(expectedUpper) || actual.Contains(expectedUpper))
                        return title;
                }
                catch (StaleElementReferenceException)
                {
                }
            }
            return null;
        });
    }

    private IWebElement FindLabel(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//label[contains(.,'{labelPart}')]")));
    }

    private void AssertDisabledById(string id, string expectedValue)
    {
        IWebElement input = wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
        Assert.That(InputValue(input), Is.EqualTo(expectedValue), $"Vlera e fushes {id} nuk eshte e sakte");
        Assert.That(input.GetAttribute("disabled") ?? input.GetAttribute("readonly"), Is.Not.Null,
            $"Fusha {id} duhet te jete disabled");
    }

    private void ClickMuiCheckbox(string id)
    {
        IWebElement input = wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'}); arguments[0].click();",
            input);
        Thread.Sleep(300);
    }

    private void AssertDergoOutcomeLenient()
    {
        if (!Commands.ShouldExecute("dergo"))
        {
            Log("Dërgo u anashkalua; nuk pretet ekrani i suksesit.");
            return;
        }

        const string successHeadline = "APLIKIMI JUAJ U DËRGUA ME SUKSES";

        By successHeadlineBy = By.XPath(
            "//h5[contains(normalize-space(.),'APLIKIMI JUAJ U DËRGUA ME SUKSES')] | //h5/b[contains(normalize-space(.),'APLIKIMI JUAJ U DËRGUA ME SUKSES')]");
        By alertModalBy = By.CssSelector(".alert-modal-container");

        string? outcome = null;
        try
        {
            outcome = new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(drv =>
            {
                try
                {
                    var successEls = drv.FindElements(successHeadlineBy);
                    if (successEls.Any(e =>
                    {
                        try { return e.Displayed; }
                        catch (StaleElementReferenceException) { return false; }
                    }))
                        return "success";
                }
                catch (StaleElementReferenceException)
                {
                }

                try
                {
                    var alertEls = drv.FindElements(alertModalBy);
                    if (alertEls.Any(e =>
                    {
                        try { return e.Displayed; }
                        catch (StaleElementReferenceException) { return false; }
                    }))
                        return "alert";
                }
                catch (StaleElementReferenceException)
                {
                }

                return null;
            });
        }
        catch (WebDriverTimeoutException)
        {
        }

        if (outcome == "success")
        {
            Log("Pas 'Dërgo' u shfaq ekrani i suksesit.");
            IWebElement headline = wait.Until(ExpectedConditions.ElementIsVisible(successHeadlineBy));
            Assert.That(headline.Text.Trim(), Does.Contain(successHeadline).IgnoreCase);
        }
        else if (outcome == "alert")
        {
            Log("Aplikimi u dërgua: sistemi u përgjigj dhe u shfaq modal paralajmërimi 'Kujdes'.");
            IWebElement alertModal = driver.FindElement(alertModalBy);
            IWebElement modalTitle = alertModal.FindElement(By.CssSelector("h2.alert-modal-title"));
            Assert.That(modalTitle.Text.Trim(), Does.StartWith("Kujdes"));

            IWebElement mbyllBtn = alertModal.FindElement(
                By.CssSelector("button.alert-modal-button--primary"));
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].scrollIntoView({block:'center'});",
                mbyllBtn);
            Thread.Sleep(300);
            try
            {
                mbyllBtn.Click();
            }
            catch (ElementClickInterceptedException)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", mbyllBtn);
            }
        }
        else
        {
            Assert.Fail(
                "Pas 'Dërgo' nuk u shfaq as ekrani i suksesit ('APLIKIMI JUAJ U DËRGUA ME SUKSES') " +
                "as modal paralajmërimi 'Kujdes' (.alert-modal-container).");
        }
    }
}
