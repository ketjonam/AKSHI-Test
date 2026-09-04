using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Biznes.MIE;

[Category("MIE")]
[Category("11141")]
public class _11141_BiznesWEB : BiznesTestBase
{
    protected override string ServiceCode => "11141";
    protected override string? ServiceTitle => "Aplikim_i_Ri_Biznes_11141";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Aplikim për reflektimin e ndryshimit të emërtimit dhe / ose selisë, në licencën e shoqërisë në studim e projektim dhe / ose mbikëqyrje e kolaudim";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Signed_TEST_signed.pdf";
    private const int TotalSteps = 3;

    private static readonly (string Id, string Title, bool Required)[] Step3Documents =
    {
        ("fuSubMandatPagesaUpload", "Pagesa e tarifës/tarifave", true),
    };

    [Test]
    public void Aplikim_i_Ri_Biznes_11141()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert Title Step 1");
        IWebElement Step1Title = WaitForStepTitle("DETAJET E SUBJEKTIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("DETAJET E SUBJEKTIT"));

        Log("Assert kohëzgjatja");
        AssertDuration("2 minuta kohëzgjatje");

        Log("Assert 3 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert te dhenat e subjektit");
        AssertDisabledByName("nipt", "M53330201S");
        Assert.That(FindLabel("NIPT").Text, Does.Contain("NIPT"));
        AssertDisabledByLabel("Emri i subjektit", "Migen Dërstila");
        Assert.That(FindLabel("Emri i subjektit").Text, Does.Contain("Emri i subjektit"));
        AssertDisabledByLabel("Dt. e regjistrimit të subjektit", "30.09.2025");
        Assert.That(FindLabel("Dt. e regjistrimit të subjektit").Text,
            Does.Contain("Dt. e regjistrimit të subjektit"));
        AssertDisabledByLabel("Përfaqësuesi ligjor", "Migen  Luan  Dërstila |");
        Assert.That(FindLabel("Përfaqësuesi ligjor").Text, Does.Contain("Përfaqësuesi ligjor"));
        AssertDisabledByLabel("Statusi i subjektit", "Aktiv");
        Assert.That(FindLabel("Statusi i subjektit").Text, Does.Contain("Statusi i subjektit"));

        IWebElement adresa2 = FindInputByLabel("Adresa 2");
        Assert.That(InputValue(adresa2), Is.EqualTo(string.Empty));
        Assert.That(adresa2.GetAttribute("disabled"), Is.Null);
        Assert.That(FindLabel("Adresa 2").Text, Does.Contain("Adresa 2"));

        IWebElement adresa = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//label[normalize-space()='Adresa:']/following-sibling::input[1]")));
        Assert.That(InputValue(adresa),
            Is.EqualTo("Derstile; ; ; ; Gjinar; ; 0000; Elbasan,Elbasan,ELBASAN,Elbasan"));
        Assert.That(adresa.GetAttribute("disabled") ?? adresa.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(driver.FindElement(By.XPath("//label[normalize-space()='Adresa:']")).Displayed, Is.True);

        IWebElement veprimtaria = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//label[contains(.,'Veprimtaria e subjektit')]/following::textarea[1]")));
        Assert.That(InputValue(veprimtaria),
            Is.EqualTo("Tregtia me pakicë me porosi me mail ose nëpërmjet internetit dhe Sherbime të programimit informatik"));
        Assert.That(veprimtaria.GetAttribute("disabled") ?? veprimtaria.GetAttribute("readonly"),
            Is.Not.Null);

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

        Log("Assert 3 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert te dhenat e kontaktit");
        IWebElement nrTel = wait.Until(ExpectedConditions.ElementExists(By.Name("nrTel")));
        Assert.That(InputValue(nrTel), Is.EqualTo(string.Empty));
        Assert.That(nrTel.GetAttribute("disabled"), Is.Null);
        Assert.That(FindLabel("Nr. tel.").Text, Does.Contain("Nr. tel."));

        IWebElement nrCel = wait.Until(ExpectedConditions.ElementExists(By.Name("telCel")));
        Assert.That(InputValue(nrCel), Is.EqualTo("0696674989"));
        Assert.That(nrCel.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(FindLabel("Nr. cel.").Text, Does.Contain("Nr. cel."));

        IWebElement email = wait.Until(ExpectedConditions.ElementExists(By.Name("email")));
        Assert.That(email.GetAttribute("type"), Is.EqualTo("email"));
        Assert.That(InputValue(email), Is.EqualTo("derstilamigen@yahoo.com"));
        Assert.That(email.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(FindLabel("Email").Text, Does.Contain("Email"));

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(), Does.Contain("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 3");
        AssertDuration("2 minuta kohëzgjatje");

        Log("Assert 3 hapa, te gjithe aktiv");
        AssertSteps(3);

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Ekstrakti historik i subjektit të regjistruar në Qendrën Kombëtare të Biznesit')]"))
            .Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Vërtetim nga organet tatimore për shlyerjen nga shoqëria, të të gjitha detyrimeve tatimore')]"))
            .Displayed, Is.True);

        Log("Assert document-upload e aplikantit");
        foreach (var doc in Step3Documents)
            AssertDocumentUpload(doc.Id, doc.Title, doc.Required);

        Log("Assert checkbox i deklarimit");
        IWebElement agreeCheck = wait.Until(ExpectedConditions.ElementExists(By.Id("agreeCheck")));
        Assert.That(agreeCheck.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='agreeCheck']")).Text.Trim(),
            Does.Contain("Mbledhja e dokumentacionit shoqërues të mësipërm"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='agreeCheck']")).Text.Trim(),
            Does.Contain("këto dokumente të sigurohen për ju nga nëpunësi i administratës"));

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Dërgo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Kliko Dergo pa ngarkuar dokumentet");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(1000);
        Assert.That(WaitForStepTitle("DOKUMENTACIONI").Text.Trim().ToUpperInvariant(),
            Does.Contain("DOKUMENTACIONI"));

        Log("Ngarko dokumentet e detyrueshme");
        foreach (var doc in Step3Documents.Where(d => d.Required))
            UploadDocument(doc.Id, DocumentPath);

        Log("Zgjidh deklarimin");
        ClickMuiCheckbox("agreeCheck");
        Assert.That(driver.FindElement(By.Id("agreeCheck")).Selected, Is.True);

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

    private IWebElement FindInputByLabel(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//label[contains(.,'{labelPart}')]/following-sibling::input[1]")));
    }

    private void AssertDisabledByName(string name, string expectedValue)
    {
        IWebElement input = wait.Until(ExpectedConditions.ElementExists(By.Name(name)));
        Assert.That(InputValue(input), Is.EqualTo(expectedValue), $"Vlera e fushes {name} nuk eshte e sakte");
        Assert.That(input.GetAttribute("disabled") ?? input.GetAttribute("readonly"), Is.Not.Null,
            $"Fusha {name} duhet te jete disabled");
    }

    private void AssertDisabledByLabel(string labelPart, string expectedValue)
    {
        IWebElement input = FindInputByLabel(labelPart);
        Assert.That(InputValue(input), Is.EqualTo(expectedValue), $"Vlera e fushes {labelPart} nuk eshte e sakte");
        Assert.That(input.GetAttribute("disabled") ?? input.GetAttribute("readonly"), Is.Not.Null,
            $"Fusha {labelPart} duhet te jete disabled");
    }

    private void ClickMuiCheckbox(string id)
    {
        IWebElement input = wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'}); arguments[0].click();",
            input);
        Thread.Sleep(300);
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle, bool required)
    {
        IWebElement title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//span[contains(@class,'fw-bold') and contains(normalize-space(),'{documentTitle}')]")));
        Assert.That(title.Displayed, Is.True);
        if (required)
            Assert.That(title.Text, Does.Contain("*"));

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-11141"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("20"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("20"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të ngarkuar dokumentin"));

        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të ngarkuar dokumentin"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='hint']")).Text.Trim(),
            Is.EqualTo("Formatet e lejuara: PDF. Madhësia maksimale: 20MB."));
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
