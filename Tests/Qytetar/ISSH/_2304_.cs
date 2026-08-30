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




        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.ealb-header-text")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("KËRKESË PËR RISHQYRTIM PËRFITIMI (PENSIONI)"));

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

        Log("Assert NID eshte readonly dhe i para-plotesuar");
        IWebElement nidInput = FindInputByLabel("NID");
        Assert.That(nidInput.GetAttribute("value").Trim(), Is.EqualTo(CitizenNid));
        Assert.That(nidInput.GetAttribute("readonly"), Is.Not.Null);

        Log("Assert llojet e pensionit");
        Assert.That(driver.FindElement(By.Id("pleqerie")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id("invaliditet")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id("familjar")).Displayed, Is.True);

        Log("Assert checkbox-et e pensionit");
        Assert.That(driver.FindElement(By.CssSelector("label[for='supplementary']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.CssSelector("label[for='earlySupplementary']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.CssSelector("label[for='earlyServicePension']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.CssSelector("label[for='electricityCompensation']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.CssSelector("label[for='dependentCompensation']")).Displayed, Is.True);

        Log("Assert kartela e pensionit");
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

        Log("Assert Email eshte disabled dhe i para-plotesuar");
        IWebElement emailInput = FindInputByName("email");
        Assert.That(emailInput.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(emailInput.GetAttribute("value").Trim(), Is.EqualTo("ketjona.mema@kreatx.com"));

        Log("Assert fushat e adreses jane bosh");
        Assert.That(FindInputByName("bashkiaKomuna").GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindInputByName("fshati").GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindInputByName("lagjja").GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindInputByName("rruga").GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindInputByName("pallatiNr").GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindInputByName("apartamentiNr").GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindInputByName("shkallaNr").GetAttribute("value"), Is.EqualTo(string.Empty));

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
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf,.jpg,.jpeg,.png"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të ngarkuar dokumentin"));

        Log("Assert teksti brenda document-upload");
        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të ngarkuar dokumentin"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='hint']")).Text.Trim(),
            Is.EqualTo("Formatet e lejuara: PDF, JPG, JPEG, PNG. Madhesia maksimale: 25MB."));

        Log("Assert shenimet e aplikantit");
        IWebElement notesLabel = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//label[contains(.,'Shënime të aplikantit')]")));
        Assert.That(notesLabel.Displayed, Is.True);

        IWebElement notesTextarea = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("textarea.ealb-input")));
        Assert.That(notesTextarea.GetAttribute("maxlength"), Is.EqualTo("255"));
        Assert.That(notesTextarea.GetAttribute("value"), Is.EqualTo(string.Empty));

        IWebElement notesCounter = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//small[contains(@class,'text-muted') and contains(.,'0/255')]")));
        Assert.That(notesCounter.Text.Trim(), Is.EqualTo("0/255"));

        Log("Assert butoni Dergo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.Text.Trim(), Does.Contain("Dërgo"));

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