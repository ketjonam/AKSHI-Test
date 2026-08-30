using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("6161")]
public class _6161_ : QytetarNidF602TestBase
{
    protected override string ServiceCode => "6161";
    protected override string? ServiceTitle => "AplikimPerRikomisionimKMCAP";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void AplikimPerRikomisionimKMCAP()
    {




        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("KËRKESË PËR RIVLERËSIM INVALIDITETI NGA KMCAP-I"));

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert Nr eshte readonly dhe i para-plotesuar");
        IWebElement nrInput = FindInputByLabel("Nr.");
        Assert.That(nrInput.GetAttribute("value"), Is.Not.Empty);
        Assert.That(nrInput.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(nrInput.GetAttribute("disabled"), Is.Not.Null);

        Log("Assert Regj.Date eshte readonly dhe e dites se sotme");
        IWebElement dateInput = FindInputByLabel("Regj.Datë");
        Assert.That(dateInput.GetAttribute("value").Trim(),
            Is.EqualTo(DateTime.Now.ToString("dd.MM.yyyy")));
        Assert.That(dateInput.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(dateInput.GetAttribute("disabled"), Is.Not.Null);

        Log("Assert NID eshte readonly dhe i para-plotesuar");
        IWebElement nidInput = FindInputByLabel("NID");
        Assert.That(nidInput.GetAttribute("value").Trim(), Is.EqualTo(CitizenNid));
        Assert.That(nidInput.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(nidInput.GetAttribute("disabled"), Is.Not.Null);

        Log("Assert ALSSH eshte readonly para zgjedhjes se DRSSH");
        IWebElement alsshSelect = FindSelectByLabel("ALSSH");
        Assert.That(alsshSelect.GetAttribute("readonly"), Is.Not.Null);

        Log("Assert checkbox-et Invaliditet dhe Suplementar");
        Assert.That(driver.FindElement(By.CssSelector("label[for='invaliditet']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.CssSelector("label[for='suplementar']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id("invaliditet")).Selected, Is.True);
        Assert.That(driver.FindElement(By.Id("suplementar")).Selected, Is.False);

        Log("Assert emri i aplikantit");
        IWebElement emriInput = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//span[contains(.,'nënshkruari')]/following-sibling::input")));
        Assert.That(emriInput.GetAttribute("value").Trim(), Is.EqualTo("Aishe Mema"));
        Assert.That(emriInput.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(emriInput.GetAttribute("disabled"), Is.Not.Null);

        Log("Assert dropdown i dosjes");
        IWebElement dosjeSelect = FindSelectAfterSpan("Nr. dosje");
        var dosje = new SelectElement(dosjeSelect);
        Assert.That(dosje.SelectedOption.GetAttribute("value"), Is.EqualTo("zgjidh"));

        IWebElement? dosjeOption = null;
        foreach (var option in dosje.Options)
        {
            if (option.GetAttribute("value") == "I0001393")
            {
                dosjeOption = option;
                break;
            }
        }
        Assert.That(dosjeOption, Is.Not.Null);
        Assert.That(dosjeOption!.Text.Trim(), Does.Contain("Invaliditet i plote i reduktuar"));

        Log("Assert arsyet e para-plotesuara");
        IWebElement arsye1 = FindNumberedInput("1.");
        Assert.That(arsye1.GetAttribute("value").Trim(),
            Is.EqualTo("Për vazhdimin e pensionit të invaliditetit/pension suplementar invaliditeti"));
        Assert.That(arsye1.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(arsye1.GetAttribute("disabled"), Is.Not.Null);

        IWebElement arsye2 = FindNumberedInput("2.");
        Assert.That(arsye2.GetAttribute("value").Trim(),
            Is.EqualTo("Për shtesë për përkujdesje (konform nenit 38 të ligjit 7703/1993, të ndryshuar)"));
        Assert.That(arsye2.GetAttribute("readonly"), Is.Not.Null);

        IWebElement arsye3 = FindNumberedInput("3.");
        Assert.That(arsye3.GetAttribute("value").Trim(),
            Is.EqualTo("Për shtesë për fëmijët në ngarkim (konform nenit 38 të ligjit 7703/1993, të ndryshuar)"));
        Assert.That(arsye3.GetAttribute("readonly"), Is.Not.Null);

        Assert.That(FindNumberedInput("4.").GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindNumberedInput("5.").GetAttribute("value"), Is.EqualTo(string.Empty));

        Log("Assert checkbox-et e kompensimeve");
        Assert.That(driver.FindElement(By.CssSelector("label[for='personatNgarkim']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.CssSelector("label[for='energjiElektrike']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id("personatNgarkim")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("energjiElektrike")).Selected, Is.False);

        Log("Kliko Vazhdo pa zgjedhur DRSSH, ALSSH dhe dosjen");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error message per DRSSH");
        IWebElement drsshError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//label[contains(.,'DRSSH')]/following-sibling::div[contains(@class,'text-danger')]")));
        Assert.That(drsshError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));

        Log("Assert error message per ALSSH");
        IWebElement alsshError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//label[contains(.,'ALSSH')]/following-sibling::div[contains(@class,'text-danger')]")));
        Assert.That(alsshError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));

        Log("Zgjidh Drejtoria Tirane");
        IWebElement drsshSelect = FindSelectByLabel("DRSSH");
        SelectDropdownByValue(drsshSelect, "11");

        Log("Wait qe ALSSH te aktivizohet");
        wait.Until(d =>
        {
            try
            {
                var agency = d.FindElement(
                    By.XPath("//form//label[contains(.,'ALSSH')]/following-sibling::select"));
                return agency.GetAttribute("readonly") == null
                    && agency.GetAttribute("disabled") == null
                    && new SelectElement(agency).Options.Count > 1;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        IWebElement alsshEnabled = FindSelectByLabel("ALSSH");
        var alsshOptions = new SelectElement(alsshEnabled);
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

        Log("Zgjidh dosjen I0001393");
        SelectDropdownByValue(FindSelectAfterSpan("Nr. dosje"), "I0001393");

        Log("Zgjidh arsyen 1");
        ClickReasonCheckbox("1.");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4.text-uppercase"));
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
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert Njesia Administrative eshte readonly dhe e para-plotesuar");
        IWebElement njesiaAdministrative = wait.Until(ExpectedConditions.ElementExists(
            By.Id("njesiaAdministrative")));
        Assert.That(njesiaAdministrative.GetAttribute("value").Trim(), Is.EqualTo("KAVAJË"));
        Assert.That(njesiaAdministrative.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(njesiaAdministrative.GetAttribute("disabled"), Is.Not.Null);

        Log("Assert fushat e adreses jane bosh");
        Assert.That(driver.FindElement(By.Id("fshat")).GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(driver.FindElement(By.Id("lagjia")).GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(driver.FindElement(By.Id("pallati")).GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(driver.FindElement(By.Id("shkNr")).GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(driver.FindElement(By.Id("rruga")).GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(driver.FindElement(By.Id("apartamenti")).GetAttribute("value"), Is.EqualTo(string.Empty));

        Log("Assert maxlength per Pall. Nr dhe Ap. Nr");
        Assert.That(driver.FindElement(By.Id("pallati")).GetAttribute("maxlength"), Is.EqualTo("10"));
        Assert.That(driver.FindElement(By.Id("apartamenti")).GetAttribute("maxlength"), Is.EqualTo("10"));

        Log("Ploteso Adresa e kerkuesit");
        FillInput(driver.FindElement(By.Id("fshat")), "Kavajë");
        FillInput(driver.FindElement(By.Id("lagjia")), "1");
        FillInput(driver.FindElement(By.Id("pallati")), "1");
        FillInput(driver.FindElement(By.Id("shkNr")), "2");
        FillInput(driver.FindElement(By.Id("rruga")), "Test");
        FillInput(driver.FindElement(By.Id("apartamenti")), "2");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4.text-uppercase"));
            if (titles.Count == 0)
                return null;
            return titles[0].Text.Trim().ToUpperInvariant() == "DOKUMENTACIONI"
                ? titles[0]
                : null;
        });
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 3");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës publike')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Për këtë shërbim nuk nevojitet të sigurohet dokumentacion nga nëpunësi i administratës')]")).Displayed, Is.True);

        Log("Assert document-upload Epikrize");
        AssertDocumentUpload("6161-fuEpikrize", "Epikrizë përcjellëse");

        Log("Assert document-upload Analiza");
        AssertDocumentUpload("6161-fuAnaliza", "Analizat dhe ekzaminimet e kryera gjatë kësaj periudhe");

        Log("Assert document-upload Vertetim");
        AssertDocumentUpload("6161-fuVertetim", "Vërtetim për llojin e punës (për invaliditet të pjesshëm)");

        Log("Assert document-upload Flete drejtimi");
        AssertDocumentUpload("6161-fuDrejtimi", "Fletë drejtimi e re");

        Log("Assert document-upload Te tjera");
        AssertDocumentUpload("6161-fuOthers", "Të tjera");

        Log("Assert qyteti default eshte Tirane");
        IWebElement citySelect = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//select[option[@value='Tiranë']]")));
        Assert.That(new SelectElement(citySelect).SelectedOption.GetAttribute("value"),
            Is.EqualTo("Tiranë"));

        Log("Zgjidh Kavaje");
        SelectDropdownByValue(citySelect, "Kavajë");

        Log("Assert data eshte readonly dhe e dites se sotme");
        IWebElement dateField = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//label[contains(.,'më datë')]/following-sibling::input")));
        Assert.That(dateField.GetAttribute("value").Trim(),
            Is.EqualTo(DateTime.Now.ToString("dd.MM.yyyy")));
        Assert.That(dateField.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(dateField.GetAttribute("disabled"), Is.Not.Null);

        Log("Assert teksti i konfirmimit");
        IWebElement confirmText = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//p[contains(.,'Ligjit nr. 9887/2003')]")));
        Assert.That(confirmText.Text.Trim(),
            Is.EqualTo("Pasi u njoha me kushtet ligjore për përfitim, konfirmoj dorëzimin e dokumentacionit shoqërues si më lart dhe nënshkruaj kërkesën, sipas Ligjit nr. 9887/2003 \"për mbrojtjen e të dhënave personale\""));

        Log("Assert butoni Dergo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.Text.Trim(), Does.Contain("Dërgo"));

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
        //Assert.That(referenceNumber.Text, Does.Contain("6161-"));
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

    private IWebElement FindSelectByLabel(string labelPart)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//form//label[contains(.,'{labelPart}')]/following-sibling::select")));
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

    private void ClickReasonCheckbox(string number)
    {

        SafeClick(By.XPath(
            $"//form//span[normalize-space()='{number}']/ancestor::li[1]//span[contains(@class,'MuiCheckbox-root')]"));
        Thread.Sleep(300);
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {

        Assert.That(driver.FindElement(
            By.XPath($"//span[normalize-space()='{documentTitle}']")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-6161"));
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
}