using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("6159")]
public class _6159_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "6159";
    protected override string? ServiceTitle => "AplikimSiDeshmorAtdheu";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    [Test]
    public void AplikimSiDeshmorAtdheu()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert 2 hapa, hapi i pare aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(2));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Not.Contain("active"));

        Log("Assert DRSSH ka opsionet e drejtorive");
        IWebElement drsshSelect = wait.Until(ExpectedConditions.ElementExists(By.Id("drssh")));
        var drssh = new SelectElement(drsshSelect);
        Assert.That(drssh.SelectedOption.GetAttribute("value"), Is.EqualTo("0"));
        Assert.That(drssh.Options.Count, Is.EqualTo(15));
        Assert.That(drssh.Options[1].GetAttribute("value"), Is.EqualTo("01"));
        Assert.That(drssh.Options[1].Text.Trim(), Is.EqualTo("Drejtoria Berat"));
        Assert.That(drssh.Options[11].GetAttribute("value"), Is.EqualTo("11"));
        Assert.That(drssh.Options[11].Text.Trim(), Is.EqualTo("Drejtoria Tirane"));
        Assert.That(drssh.Options[13].Text.Trim(), Is.EqualTo("Dega Tropoje"));
        Assert.That(drssh.Options[14].Text.Trim(), Is.EqualTo("Dega Sarande"));

        Log("Assert ALSSH eshte disabled para zgjedhjes se DRSSH");
        IWebElement alsshSelect = wait.Until(ExpectedConditions.ElementExists(By.Id("agency")));
        Assert.That(alsshSelect.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(new SelectElement(alsshSelect).Options.Count, Is.EqualTo(1));

        Log("Assert emri i nenenshkruesit eshte readonly dhe i para-plotesuar");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(normalize-space(),'nënshkruari')]")).Displayed, Is.True);
        IWebElement emriInput = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[contains(normalize-space(),'nënshkruari')]/following::input[1]")));
        Assert.That(emriInput.GetAttribute("value").Trim(), Is.EqualTo("Ketjona Mema"));
        Assert.That(emriInput.GetAttribute("readonly"), Is.Not.Null);

        Log("Assert dropdown i vendbanimit");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(normalize-space(),'me banim në')]")).Displayed, Is.True);
        IWebElement residenceSelect = FindResidenceSelect();
        var residence = new SelectElement(residenceSelect);
        Assert.That(residence.SelectedOption.GetAttribute("value"), Is.EqualTo("Tirane"));
        Assert.That(residence.Options.Count, Is.GreaterThan(1));
        Assert.That(residence.Options[0].GetAttribute("value"), Is.EqualTo("Tirane"));
        Assert.That(residence.Options[1].GetAttribute("value"), Is.EqualTo("Durres"));

        Log("Assert fusha e deshmorit eshte bosh");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(normalize-space(),'përfitim për dëshmorin')]")).Displayed, Is.True);
        IWebElement martyrName = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("martyrName")));
        Assert.That(martyrName.GetAttribute("value"), Is.EqualTo(string.Empty));

        Log("Kliko Vazhdo pa zgjedhur DRSSH dhe ALSSH");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error message per DRSSH");
        IWebElement drsshError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//label[contains(.,'DRSSH')]/following::div[contains(@class,'text-danger')][1]")));
        Assert.That(drsshError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));

        Log("Assert error message per ALSSH");
        IWebElement alsshError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//label[contains(.,'ALSSH')]/following::div[contains(@class,'text-danger')][1]")));
        Assert.That(alsshError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));

        Log("Zgjidh Drejtoria Tirane");
        SelectDropdownByValue(wait.Until(ExpectedConditions.ElementExists(By.Id("drssh"))), "11");

        Log("Wait qe ALSSH te aktivizohet");
        wait.Until(d =>
        {
            try
            {
                var agency = d.FindElement(By.Id("agency"));
                return agency.GetAttribute("disabled") == null
                    && new SelectElement(agency).Options.Count > 1;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        IWebElement alsshEnabled = driver.FindElement(By.Id("agency"));
        var alsshOptions = new SelectElement(alsshEnabled);
        Assert.That(alsshEnabled.GetAttribute("disabled"), Is.Null);
        Assert.That(alsshOptions.Options.Count, Is.GreaterThan(1));

        Log("Zgjidh ALSSH Kavaje nese ekziston, perndryshe opsionin e pare");
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

        Log("Zgjidh vendbanimin Kavaje");
        SelectDropdownByValue(FindResidenceSelect(), "Kavajë");

        Log("Ploteso emrin e deshmorit");
        FillInput(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("martyrName"))), "Deshmor Testi");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4.text-uppercase"));
            if (titles.Count == 0)
                return null;
            return titles[0].Text.Trim().ToUpperInvariant() == "DOKUMENTACIONI"
                ? titles[0]
                : null;
        });
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert 2 hapa, te gjithe aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(2));
        foreach (var step in steps)
            Assert.That(step.GetAttribute("class"), Does.Contain("active"));

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës publike')]")).Displayed, Is.True);

        Log("Assert document-upload Vertetim shkolle");
        AssertDocumentUpload("6159-vertetimShkolle", "Vërtetim shkolle");

        Log("Assert document-upload Te tjera");
        AssertDocumentUpload("6159-teTjera", "Të tjera");

        Log("Assert dokumentet e administrates publike");
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'Fotokopje e kartës së identitetit')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'Certifikatë e vdekjes së dëshmorit')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'Certifikatë e trungut familjar në çastin e vdekjes')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//li[normalize-space()='Certifikatë e trungut familjar']")).Displayed, Is.True);

        Log("Assert checkbox i pranimit eshte i pazgjedhur");
        IWebElement confirmCheck = wait.Until(ExpectedConditions.ElementExists(By.Id("confirmAdminDocuments")));
        Assert.That(confirmCheck.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='confirmAdminDocuments']")).Text.Trim(),
            Is.EqualTo("Mbledhja e dokumentacionit shoqërues të mësipërm që më parë ishte detyrim të dorëzohej në zyrat e shtetit nga vetë aplikanti, tani është detyrë e nëpunësit të administratës ndaj qytetarit. Me klikimin e këtij butoni, ju bini dakord që këto dokumente të sigurohen për ju nga nëpunësi i administratës."));

        Log("Kliko Dergo pa pranuar kushtet");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(1000);
        Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase"))).Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DOKUMENTACIONI"));

        Log("Zgjidh pranimin e kushteve");
        SafeClick(By.Id("confirmAdminDocuments"));
        Assert.That(driver.FindElement(By.Id("confirmAdminDocuments")).Selected, Is.True);

        Log("Assert butoni Dergo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.Text.Trim(), Does.Contain("Dërgo"));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        //Log("Kliko Dergo");
        //SafeClick(By.CssSelector("button.ealb-btn-continue"));
        //Thread.Sleep(5000);

        //Log("Assert suksesi");
        //IWebElement successTitle = wait.Until(ExpectedConditions.ElementIsVisible(
        //    By.XPath("//h5[contains(.,'APLIKIMI JUAJ')]")));
        //Assert.That(successTitle.Text.Trim().ToUpperInvariant().Replace("Ë", "E"),
        //    Does.Contain("APLIKIMI JUAJ U DERGUA ME SUKSES"));

        //IWebElement referenceNumber = wait.Until(ExpectedConditions.ElementIsVisible(
        //    By.XPath("//h6[contains(.,'Numri referencë i aplikimit')]")));
        //Assert.That(referenceNumber.Text, Does.Contain("6159-"));
        //Assert.That(driver.Url, Does.Contain("/mesazh"));

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

    private IWebElement FindResidenceSelect()
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//div[contains(normalize-space(),'me banim në')]/following::select[1]")));
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {

        Assert.That(driver.FindElement(
            By.XPath($"//span[normalize-space()='{documentTitle}']")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-6159"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("25"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("25"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf,.jpg,.jpeg,.png"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të zgjedhur dokumentin"));

        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të zgjedhur dokumentin"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='hint']")).Text.Trim(),
            Is.EqualTo("Formatet e lejuara: PDF, JPG, JPEG, PNG. Madhësia maksimale: 25MB."));
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
        Assert.That(serviceName.Text.Trim(),
            Is.EqualTo("Aplikim për shpërblim financiar nga statusi \"Dëshmor i Atdheut\""),
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
}