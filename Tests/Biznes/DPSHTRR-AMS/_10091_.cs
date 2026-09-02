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

    [Test]
    public void PajisjemeDAPperMakineriteeRenda()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

        Log("Assert 4 hapa, hapi i pare aktiv");
        AssertActiveSteps(activeCount: 1);

        Log("Assert Step1 title");
        IWebElement Step1Title = WaitForStepTitle("TË DHËNAT E SUBJEKTIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("TË DHËNAT E SUBJEKTIT"));

        Log("Assert te dhenat e SUBJEKTIT");
        IWebElement NrIdentifikimit = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("nipt")));
        Assert.That(NrIdentifikimit.GetAttribute("value").Trim(), Is.EqualTo("M53330201S"));

        IWebElement Emri = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("subjectName")));
        Assert.That(Emri.GetAttribute("value").Trim(), Is.EqualTo("Migen Dërstila"));

        IWebElement Administratori = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("administrator")));
        Assert.That(Administratori.GetAttribute("value").Trim(), Is.EqualTo("Migen  Luan  Dërstila"));

        IWebElement Tel = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("phoneNumber")));
        Assert.That(Tel.GetAttribute("value").Trim(), Is.EqualTo("+355684053531"));

        IWebElement Email = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("email")));
        Assert.That(Email.GetAttribute("value").Trim(), Is.EqualTo("migen.derstila@kreatx.com"));

        IWebElement Adresa = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("address")));
        Assert.That(Adresa.GetAttribute("value").Trim(),
            Is.EqualTo("Derstile; ; ; ; Gjinar; ; 0000; Elbasan,Elbasan,ELBASAN,Elbasan"));

        Log("Assert butonat e navigimit Step 1");
        AssertBackAndContinue("Vazhdo");

        Log("Kliko Vazhdo button");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(4000);

        Log("Assert Step2 title");
        IWebElement Step2Title = WaitForStepTitle("TË DHËNAT E KANDIDATIT");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("TË DHËNAT E KANDIDATIT"));

        Log("Assert 4 hapa, dy te paret aktiv");
        AssertActiveSteps(activeCount: 2);

        Log("Kliko Vazhdo pa plotesuar te dhenat e kandidatit");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert mesazhi per te plotesuar te dhenat e kandidatit");
        IWebElement msgError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//*[normalize-space()='Plotësoni fushën për të vazhduar']")));
        Assert.That(msgError.Text.Trim(), Is.EqualTo("Plotësoni fushën për të vazhduar"));

        Log("Ploteso te dhenat e kandidatit");
        IWebElement NID = FindInputByLabel("Nid");
        NID.SendKeys("J55728107R");
        NID.SendKeys(Keys.Tab);
        Thread.Sleep(2000);

        Log("Assert te dhenat e KANDIDATIT");
        Assert.That(FindInputByLabel("Emri").GetAttribute("value").Trim(), Is.EqualTo("Ketjona"));
        Assert.That(FindInputByLabel("Mbiemri").GetAttribute("value").Trim(), Is.EqualTo("Mema"));
        Assert.That(FindInputByLabel("Atësia").GetAttribute("value").Trim(), Is.EqualTo("Mersin"));
        Assert.That(FindInputByLabel("Datëlindja").GetAttribute("value").Trim(), Is.EqualTo("28.07.1995"));
        Assert.That(FindInputByLabel("Gjinia").GetAttribute("value").Trim(), Is.EqualTo("Femër"));

        Log("Kliko Vazhdo button");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(4000);

        Log("Assert Step3 title");
        IWebElement Step3Title = WaitForStepTitle("TË DHËNAT E LEJES SË DREJTIMIT QË DISPONON");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E LEJES SË DREJTIMIT QË DISPONON"));

        Log("Assert 4 hapa, tre te paret aktiv");
        AssertActiveSteps(activeCount: 3);

        Log("Assert te dhenat e lejes se drejtimit");
        Assert.That(FindInputByLabel("Kategoritë").GetAttribute("value").Trim(), Is.EqualTo("B"));
        Assert.That(FindInputByLabel("Data e lëshimit").GetAttribute("value").Trim(), Is.EqualTo("03.06.2022"));
        Assert.That(FindInputByLabel("Data e vlefshmërisë").GetAttribute("value").Trim(), Is.EqualTo("02.06.2032"));

        Log("Kliko Vazhdo button");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(4000);

        Log("Assert Step4 title");
        IWebElement Step4Title = WaitForStepTitle("LLOJI I DAP");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("LLOJI I DAP"));

        Log("Assert 4 hapa, te gjithe aktiv");
        AssertActiveSteps(activeCount: 4);

        Log("Kliko Dergo buton pa plotesuar llojin e dap");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert mesazhi per te plotesuar llojin e dap");
        IWebElement msgErrorDAP = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//*[normalize-space()='Përzgjidhni një vlerë për të vazhduar']")));
        Assert.That(msgErrorDAP.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));

        Log("Zgjidh llojin e DAP");
        new SelectElement(wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("select[name='llojiDAP']"))))
            .SelectByValue("Automakinist");

        new SelectElement(wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("select[name='drshtrr']"))))
            .SelectByValue("11");

        Log("Kliko Dergo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.Text.Trim(), Does.Contain("Dërgo"));
        ClickDerghoAfterDocumentationReady();
        AssertSuccessOrKujdesAfterDergo();

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
        Assert.That(serviceName.Text.Trim(), Is.EqualTo(ExpectedServiceName),
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
            By.XPath($"//form//label[contains(.,'{labelPart}')]/following-sibling::input")));
    }

    private void AssertActiveSteps(int activeCount, int totalCount = 4)
    {
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(totalCount));
        for (int i = 0; i < steps.Count; i++)
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
            if (i < activeCount)
                Assert.That(steps[i].GetAttribute("class"), Does.Contain("active"));
            else
                Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
        }
    }

    private void AssertBackAndContinue(string continueText)
    {
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Does.Contain(continueText));
    }

    private IWebElement FindDerghoButtonInMain()
    {
        var candidates = driver.FindElements(
            By.XPath("//main//button[contains(normalize-space(.), 'Dërgo') or contains(normalize-space(.), 'Dergo')]"));
        IWebElement pick = candidates.LastOrDefault(e =>
        {
            try
            {
                return e.Displayed;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });
        if (pick == null && candidates.Count > 0)
            pick = candidates[candidates.Count - 1];
        if (pick == null)
            throw new NoSuchElementException("Nuk u gjet butoni 'Dërgo' brenda main.");
        return pick;
    }

    private void ClickDerghoAfterDocumentationReady()
    {
        var sendWait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
        sendWait.Until(drv =>
        {
            try
            {
                var b = FindDerghoButtonInMain();
                return b.Displayed && b.Enabled;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        });

        IWebElement dergo = FindDerghoButtonInMain();
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center', inline:'nearest'});",
            dergo);
        Thread.Sleep(400);
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", dergo);
        Log("Klikuar butoni 'Dërgo' (JavaScript click pasi u aktivizua).");
    }

    private void AssertSuccessOrKujdesAfterDergo()
    {
        const string successHeadline = "APLIKIMI JUAJ U DËRGUA ME SUKSES.";
        const string alertExpectedTitle = "Kujdes";
        const string alertExpectedDescription =
            "Ekzistojne aplikime te pa perfunduara per kete mjet.";

        By successHeadlineBy = By.XPath(
            "//h5[contains(normalize-space(.),'APLIKIMI JUAJ U DËRGUA ME SUKSES')]");
        By alertModalBy = By.CssSelector(".alert-modal-container");

        string outcome = null;
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

            var refEls = driver.FindElements(
                By.XPath("//h6[contains(normalize-space(.),'Numri referencë i aplikimit')]"));
            var trackEls = driver.FindElements(
                By.XPath("//button[contains(normalize-space(.),'GJURMO APLIKIMIN')]"));
            bool hasRef = refEls.Any(e =>
            {
                try { return e.Displayed; }
                catch (StaleElementReferenceException) { return false; }
            });
            bool hasTrack = trackEls.Any(e =>
            {
                try { return e.Displayed; }
                catch (StaleElementReferenceException) { return false; }
            });

            if (hasRef && hasTrack)
            {
                IWebElement referenceLine = refEls.First(e =>
                {
                    try { return e.Displayed; }
                    catch (StaleElementReferenceException) { return false; }
                });
                Assert.That(
                    referenceLine.Text.Trim(),
                    Does.Contain("Numri referencë i aplikimit është:").IgnoreCase);
                Assert.That(
                    referenceLine.Text.Trim(),
                    Does.Match("(?i)eALB-\\d+"));

                IWebElement trackBtn = trackEls.First(e =>
                {
                    try { return e.Displayed; }
                    catch (StaleElementReferenceException) { return false; }
                });
                Assert.That(trackBtn.Displayed, Is.True);
                Log("Sukses i verifikuar: headline, referenca eALB dhe butoni GJURMO APLIKIMIN.");
            }
            else
            {
                Log("Sukses i verifikuar: headline (eALB/GJURMO nuk u gjetën).");
            }
        }
        else if (outcome == "alert")
        {
            Log("Aplikimi u dërgua: sistemi u përgjigj dhe u shfaq modal paralajmërimi 'Kujdes'.");
            IWebElement alertModal = driver.FindElement(alertModalBy);
            IWebElement modalTitle = alertModal.FindElement(By.CssSelector("h2.alert-modal-title"));
            Assert.That(modalTitle.Text.Trim(), Is.EqualTo(alertExpectedTitle));

            var descEls = alertModal.FindElements(By.CssSelector(".alert-modal-description"));
            if (descEls.Count > 0)
            {
                Assert.That(descEls[0].Text.Trim(), Is.EqualTo(alertExpectedDescription));
            }

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
                "Pas 'Dërgo' nuk u shfaq as ekrani i suksesit ('APLIKIMI JUAJ U DËRGUA ME SUKSES.') " +
                "as modal paralajmërimi 'Kujdes' (.alert-modal-container).");
        }
    }
}
