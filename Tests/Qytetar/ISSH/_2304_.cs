using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("2304")]
public class _2304_ : QytetarNidF602TestBase
{
    protected override string ServiceCode => "2304";
    protected override string? ServiceTitle => "KerkesePerRishqyrtimPensioni";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void KerkesePerRishqyrtimPensioni()
    {
        Log("Assert page header");
        IWebElement headerContainer = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("div.page-header-container")));
        Assert.That(headerContainer.Displayed, Is.True, "Page header nuk eshte visible");

        IWebElement serviceName = wait.Until(ExpectedConditions.ElementIsVisible(
            By.Id("serviceNameBreadcrumb")));
        Assert.That(serviceName.Displayed, Is.True, "Breadcrumb i sherbimit nuk eshte visible");
        Assert.That(serviceName.Text.Trim(), Is.EqualTo("Kërkesë për rishqyrtim përfitimi (pensioni)"),
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

        Log("Assert tre hapa, i pari aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(3));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));
        Assert.That(steps[1].GetAttribute("class"), Does.Not.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Not.Contain("active"));

        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.ealb-header-text")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("KËRKESË PËR RISHQYRTIM PËRFITIMI (PENSIONI)"));

        Log("Assert NID eshte readonly dhe i para-plotesuar");
        IWebElement nidInput = FindInputByLabel("NID");
        Assert.That(nidInput.GetAttribute("value").Trim(), Is.EqualTo(CitizenNid));
        Assert.That(nidInput.GetAttribute("readonly"), Is.Not.Null);

        Log("Assert llojet e pensionit");
        Assert.That(driver.FindElement(By.Id("pleqerie")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id("invaliditet")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id("familjar")).Displayed, Is.True);
        Assert.That(RadioLabel("pleqerie"), Is.EqualTo("Pleqëri"));
        Assert.That(RadioLabel("invaliditet"), Is.EqualTo("Invaliditet"));
        Assert.That(RadioLabel("familjar"), Is.EqualTo("Familjar"));
        Assert.That(driver.FindElement(By.Id("pleqerie")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("invaliditet")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("familjar")).Selected, Is.False);

        Log("Assert checkbox-et e pensionit");
        Assert.That(driver.FindElement(By.CssSelector("label[for='supplementary']")).Text.Trim(),
            Is.EqualTo("Suplementar"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='earlySupplementary']")).Text.Trim(),
            Is.EqualTo("Suplementar i parakohshëm"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='earlyServicePension']")).Text.Trim(),
            Is.EqualTo("Pension i parakohshëm për vjetërsi shërbimi"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='electricityCompensation']")).Text.Trim(),
            Is.EqualTo("Kompesim i energjisë elektrike (VKM 565, VKM 8)"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='dependentCompensation']")).Text.Trim(),
            Is.EqualTo("Kompesim për personat në ngarkim"));
        Assert.That(driver.FindElement(By.Id("supplementary")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("earlySupplementary")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("earlyServicePension")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("electricityCompensation")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("dependentCompensation")).Selected, Is.False);

        Log("Assert deklarimi dhe kartela e pensionit");
        IWebElement deklarim = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//span[contains(.,'nënshkruari/a') and contains(.,'Kadri Kukaj')]")));
        Assert.That(deklarim.Text.Trim(),
            Does.Contain("Unë i/e nënshkruari/a Kadri Kukaj kërkoj të më rishqyrtohet pensioni me Nr."));

        IWebElement kartelaSelect = FindSelectAfterSpan("rishqyrtohet pensioni me Nr.");
        var kartela = new SelectElement(kartelaSelect);
        Assert.That(kartela.SelectedOption.GetAttribute("value"), Is.EqualTo("132655"));
        Assert.That(kartela.SelectedOption.Text.Trim(), Is.EqualTo("Kartele Pleqerie me nr. 132655"));

        Log("Assert DRSSH dhe Agjencia jane disabled");
        IWebElement drsshSelect = FindSelectAfterSpan("dosje të cilën unë e kam në DRSSH-në");
        IWebElement agjenciaSelect = FindSelectAfterSpan("në Agjencinë e Sigurimeve Shoqërore");
        Assert.That(drsshSelect.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(agjenciaSelect.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(new SelectElement(drsshSelect).SelectedOption.Text.Trim(), Is.EqualTo("Drejtoria Shkoder"));
        Assert.That(new SelectElement(agjenciaSelect).SelectedOption.Text.Trim(), Is.EqualTo("Shkoder"));

        Log("Assert fushat e arsyeve jane bosh");
        IWebElement reason1 = FindNumberedInput("1");
        IWebElement reason2 = FindNumberedInput("2");
        IWebElement reason3 = FindNumberedInput("3");
        Assert.That(reason1.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(reason2.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(reason3.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(reason1.GetAttribute("maxlength"), Is.EqualTo("249"));
        Assert.That(reason2.GetAttribute("maxlength"), Is.EqualTo("249"));
        Assert.That(reason3.GetAttribute("maxlength"), Is.EqualTo("249"));

        Log("Assert butonat e navigimit");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        Assert.That(backBtn.Displayed, Is.True, "Butoni Kthehu nuk eshte visible");
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        IWebElement continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Kliko Vazhdo pa zgjedhur llojin e pensionit");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error message per llojin e pensionit");
        IWebElement pensionTypeError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//div[contains(@class,'text-danger') and contains(.,'Zgjidhni llojin e pensionit')]")));
        Assert.That(pensionTypeError.Text.Trim(), Is.EqualTo("Zgjidhni llojin e pensionit"));

        Log("Assert error message per arsyet e rishqyrtimit");
        IWebElement reasonError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//div[contains(@class,'text-danger') and contains(.,'Shkruani të paktën një arsye')]")));
        Assert.That(reasonError.Text.Trim(),
            Is.EqualTo("Shkruani të paktën një arsye për rishqyrtimin e pensionit."));

        Log("Zgjidh llojin e pensionit: Pleqëri");
        SelectRadioById("pleqerie");
        Assert.That(driver.FindElement(By.Id("pleqerie")).Selected, Is.True);

        Log("Ploteso arsyet e rishqyrtimit");
        FillInput(FindNumberedInput("1"), "Arsye test 1");
        FillInput(FindNumberedInput("2"), "Arsye test 2");
        FillInput(FindNumberedInput("3"), "Arsye test 3");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4.ealb-header-text"));
            if (titles.Count == 0)
                return null;
            return titles[0].Text.Trim().ToUpperInvariant() == "ADRESA E KËRKUESIT"
                ? titles[0]
                : null;
        });
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("ADRESA E KËRKUESIT"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

        Log("Assert dy hapa aktiv Step 2");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(3));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Not.Contain("active"));

        Log("Assert label-at dhe fushat e adreses");
        AssertAddressField("bashkiaKomuna", "Bashkia / Komuna", "100");
        AssertAddressField("fshati", "Fshati", "100");
        AssertAddressField("lagjja", "Lagjja", "100");
        AssertAddressField("rruga", "Rruga", "100");
        AssertAddressField("pallatiNr", "Pall. Nr.", "12");
        AssertAddressField("apartamentiNr", "Ap. Nr.", "12");
        AssertAddressField("shkallaNr", "Shk. Nr.", "12");

        Log("Assert Email eshte disabled dhe i para-plotesuar");
        Assert.That(FindFormLabel("Email").Displayed, Is.True);
        IWebElement emailInput = FindInputByName("email");
        Assert.That(emailInput.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(emailInput.GetAttribute("value").Trim(), Is.EqualTo("shkeldiana.gjongecaj@kreatx.com"));

        Log("Assert butonat e navigimit Step 2");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Ploteso Adresa e kerkuesit");
        FillInput(FindInputByName("bashkiaKomuna"), "Shkodër");
        FillInput(FindInputByName("fshati"), "Shkodër");
        FillInput(FindInputByName("lagjja"), "1");
        FillInput(FindInputByName("rruga"), "Test");
        FillInput(FindInputByName("pallatiNr"), "1");
        FillInput(FindInputByName("apartamentiNr"), "2");
        FillInput(FindInputByName("shkallaNr"), "2");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4.ealb-header-text"));
            if (titles.Count == 0)
                return null;
            return titles[0].Text.Trim().ToUpperInvariant() == "DOKUMENTACION"
                ? titles[0]
                : null;
        });
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("DOKUMENTACION"));

        Log("Assert kohëzgjatja Step 3");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

        Log("Assert tre hapa aktiv Step 3");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(3));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Contain("active"));

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//span[normalize-space()='Të ndryshme']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës publike')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//span[contains(.,'Për këtë shërbim nuk nevojitet të sigurohet dokumentacion nga nëpunësi i administratës')]")).Displayed, Is.True);

        Log("Assert document-upload");
        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id("doc1-upload-2304")));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-2304"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("25"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("25"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf,.jpg,.jpeg,.png"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të ngarkuar dokumentin"));

        Log("Assert teksti brenda document-upload");
        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të ngarkuar dokumentin"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='hint']")).Text.Trim(),
            Is.EqualTo("Formatet e lejuara: PDF, JPG, JPEG, PNG. Madhësia maksimale: 25MB."));

        Log("Assert shenimet e aplikantit");
        IWebElement notesLabel = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//label[normalize-space()='Shënime të aplikantit:']")));
        Assert.That(notesLabel.Displayed, Is.True);

        IWebElement notesTextarea = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("textarea.ealb-input")));
        Assert.That(notesTextarea.GetAttribute("maxlength"), Is.EqualTo("255"));
        Assert.That(notesTextarea.GetAttribute("value"), Is.EqualTo(string.Empty));

        IWebElement notesCounter = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//small[contains(@class,'text-muted') and contains(.,'0/255')]")));
        Assert.That(notesCounter.Text.Trim(), Is.EqualTo("0/255"));

        Log("Assert butonat e navigimit Step 3");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.Text.Trim(), Does.Contain("Dërgo"));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Ploteso shenimet e aplikantit");
        FillTextarea(notesTextarea, "Test shenime");

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
        //Assert.That(referenceNumber.Text, Does.Contain("2304-"));
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

    private string RadioLabel(string radioId)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//form//input[@id='{radioId}']/following-sibling::span"))).Text.Trim();
    }

    private IWebElement FindInputByLabel(string labelText)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//form//label[normalize-space()='{labelText}']/following-sibling::input")));
    }

    private IWebElement FindSelectAfterSpan(string spanPart)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//form//span[contains(.,'{spanPart}')]/following::select[1]")));
    }

    private IWebElement FindNumberedInput(string number)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//form//span[normalize-space()='{number}']/following-sibling::input")));
    }

    private IWebElement FindFormLabel(string labelText)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//form//label[normalize-space()='{labelText}']")));
    }

    private void AssertAddressField(string name, string label, string maxlength)
    {
        Assert.That(FindFormLabel(label).Displayed, Is.True);
        IWebElement input = FindInputByName(name);
        Assert.That(input.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(input.GetAttribute("maxlength"), Is.EqualTo(maxlength));
    }

    private IWebElement FindInputByName(string name)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector($"form input[name='{name}']")));
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

    private void FillTextarea(IWebElement textarea, string value)
    {

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            textarea
        );

        Thread.Sleep(400);

        try
        {
            textarea.Click();
            Thread.Sleep(200);
            textarea.Clear();
            textarea.SendKeys(value);
        }
        catch (ElementClickInterceptedException)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].focus(); arguments[0].value = '';",
                textarea
            );
            textarea.SendKeys(value);
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