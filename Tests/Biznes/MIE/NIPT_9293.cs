using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Biznes.MIE;

[Category("MIE")]
[Category("9293")]
public class NIPT_9293 : BiznesTestBase
{
    protected override string ServiceCode => "9293";
    protected override string? ServiceTitle => "Aplikim_i_Ri_Biznes_9293";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Licencimi i shoqërive për herë të parë në fushën e vlerësimit të pasurive të paluajtshme (online)";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Signed_TEST_signed.pdf";
    private const int TotalSteps = 4;

    private static readonly (string Id, string Title, bool Required)[] Step4Documents =
    {
        ("fileKontrataPunesUpload",
            "Kontrata e punës midis përfaqësuesit ligjor dhe vlerësuesit/ve të rinj që punësohen pranë saj", true),
        ("fileVetedeklarimiUpload",
            "Vetëdeklarim të drejtuesit/drejtuesve ligjorë dhe të vlerësuesit/vlerësuesve të shoqërisë", true),
    };

    [Test]
    public void Aplikim_i_Ri_Biznes_9293()
    {
        OpenNewApplicationFromServicePage(ExpectedServiceName);

        Log("Assert Title Step 1");
        IWebElement Step1Title = WaitForStepTitle("DETAJET E SUBJEKTIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("DETAJET E SUBJEKTIT"));

        Log("Assert kohëzgjatja");
        AssertDuration("3 minuta kohëzgjatje");

        Log("Assert 4 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert te dhenat e subjektit");
        AssertDisabledById("nipt", "M53330201S");
        Assert.That(FindLabel("NIPT").Text, Does.Contain("NIPT"));
        AssertDisabledById("emri", "Migen Dërstila");
        Assert.That(FindLabel("Emri i subjektit").Text, Does.Contain("Emri i subjektit"));
        AssertDisabledById("registrationDate", "30.09.2025");
        Assert.That(FindLabel("Dt. e rregjistrimit").Text, Does.Contain("Dt. e rregjistrimit"));
        AssertDisabledById("administrator", "Migen  Luan  Dërstila |");
        Assert.That(FindLabel("Administratori").Text, Does.Contain("Administratori"));
        AssertDisabledById("status", "Aktiv");
        Assert.That(FindLabel("Statusi i subjektit").Text, Does.Contain("Statusi i subjektit"));

        AssertDisabledById(
            "adresa",
            "Derstile; ; ; ; Gjinar; ; 0000; Elbasan,Elbasan,ELBASAN,Elbasan");
        Assert.That(driver.FindElement(By.XPath("//label[normalize-space()='Adresa:']")).Displayed, Is.True);

        IWebElement veprimtaria = wait.Until(ExpectedConditions.ElementExists(By.Id("activity")));
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
        AssertDuration("3 minuta kohëzgjatje");

        Log("Assert 4 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert te dhenat e kontaktit");
        IWebElement nrTel = wait.Until(ExpectedConditions.ElementExists(By.Name("nrTel")));
        Assert.That(InputValue(nrTel), Is.EqualTo(string.Empty));
        Assert.That(nrTel.GetAttribute("disabled"), Is.Null);
        Assert.That(FindLabel("Nr. tel.").Text, Does.Contain("Nr. tel."));

        IWebElement nrCel = wait.Until(ExpectedConditions.ElementExists(By.Name("nrCel")));
        Assert.That(InputValue(nrCel), Is.EqualTo("+355684053531"));
        Assert.That(nrCel.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindLabel("Nr. cel.").Text, Does.Contain("Nr. cel."));

        IWebElement email = wait.Until(ExpectedConditions.ElementExists(By.Name("email")));
        Assert.That(email.GetAttribute("type"), Is.EqualTo("email"));
        Assert.That(InputValue(email), Is.EqualTo("migen.derstila@kreatx.com"));
        Assert.That(email.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindLabel("Email").Text, Does.Contain("Email"));

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("DETAJET E APLIKIMIT");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(), Does.Contain("DETAJET E APLIKIMIT"));

        Log("Assert kohëzgjatja Step 3");
        AssertDuration("3 minuta kohëzgjatje");

        Log("Assert 4 hapa, tre te paret aktiv");
        AssertSteps(3);

        Log("Assert fushat e detajeve te aplikimit");
        IWebElement licensePerLabel = FindLabel("Licencë për");
        Assert.That(licensePerLabel.Text, Does.Contain("Licencë për"));
        Assert.That(licensePerLabel.Text, Does.Contain("*"));

        IWebElement licensePer = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("licensePer")));
        var licenseSelect = new SelectElement(licensePer);
        Assert.That(InputValue(licensePer), Is.EqualTo(string.Empty));
        Assert.That(licenseSelect.Options.Select(o => o.GetAttribute("value")).ToList(),
            Is.EquivalentTo(new[]
            {
                "",
                "NDERTES_TOKE_TRUALL",
                "TOKE_BUJQESORE_PYJE_LIVADH",
                "LINJA_TEKNOLOGJIKE",
            }));
        Assert.That(
            licenseSelect.Options.Any(o => o.Text.Trim() == "Për ndërtesat dhe tokë truall"),
            Is.True);
        Assert.That(
            licenseSelect.Options.Any(o =>
                o.Text.Trim() == "Për tokë buqësore, tokë pyjore, kullotë, livadh dhe toke të pafrytshme"),
            Is.True);
        Assert.That(
            licenseSelect.Options.Any(o => o.Text.Trim() == "Linja teknologjike, makineri e pajisje"),
            Is.True);

        IWebElement shenim = wait.Until(ExpectedConditions.ElementExists(By.Id("shenim")));
        Assert.That(InputValue(shenim), Is.EqualTo(string.Empty));
        Assert.That(shenim.GetAttribute("disabled"), Is.Null);
        Assert.That(FindLabel("Shënim").Text, Does.Contain("Shënim"));

        IWebElement adresaAplikimi = wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector("textarea#adresa")));
        Assert.That(InputValue(adresaAplikimi), Is.EqualTo(string.Empty));
        Assert.That(adresaAplikimi.GetAttribute("disabled"), Is.Null);
        Assert.That(driver.FindElement(By.CssSelector("label[for='adresa']")).Text, Does.Contain("Adresa"));

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa zgjedhur licencen");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(1000);
        Assert.That(WaitForStepTitle("DETAJET E APLIKIMIT").Text.Trim().ToUpperInvariant(),
            Does.Contain("DETAJET E APLIKIMIT"));

        Log("Zgjidh licencen");
        licenseSelect.SelectByValue("LINJA_TEKNOLOGJIKE");

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 4 Title");
        IWebElement Step4Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(), Does.Contain("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 4");
        AssertDuration("3 minuta kohëzgjatje");

        Log("Assert 4 hapa, te gjithe aktiv");
        AssertSteps(4);

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Ekstrakti i regjistrit tregtar për të dhënat e subjektit')]"))
            .Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Vërtetimi nga organet tatimore, për shlyerjen, nga shoqëria, të të gjitha detyrimeve tatimore')]"))
            .Displayed, Is.True);

        Log("Assert document-upload e aplikantit");
        foreach (var doc in Step4Documents)
            AssertDocumentUpload(doc.Id, doc.Title, doc.Required);

        Log("Assert checkbox i deklarimit");
        IWebElement agreeCheck = wait.Until(ExpectedConditions.ElementExists(By.Id("agreeCheck")));
        Assert.That(agreeCheck.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='agreeCheck']")).Text.Trim(),
            Does.Contain("Mbledhja e dokumentacionit shoqërues të mësipërm"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='agreeCheck']")).Text.Trim(),
            Does.Contain("këto dokumente të sigurohen për ju nga nëpunësi i administratës"));

        Log("Assert butonat e navigimit Step 4");
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
        foreach (var doc in Step4Documents.Where(d => d.Required))
            UploadDocument(doc.Id, DocumentPath);

        Log("Zgjidh deklarimin");
        ClickMuiCheckbox("agreeCheck");
        Assert.That(driver.FindElement(By.Id("agreeCheck")).Selected, Is.True);

        ClickDergo();
        AssertDergoOutcomeLenient();

        Log("TEST PASSED");
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

    private void AssertDocumentUpload(string uploadId, string documentTitle, bool required)
    {
        IWebElement title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//span[contains(@class,'fw-bold') and contains(normalize-space(),'{documentTitle}')]")));
        Assert.That(title.Displayed, Is.True);
        if (required)
            Assert.That(title.Text, Does.Contain("*"));

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-9293"));
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
