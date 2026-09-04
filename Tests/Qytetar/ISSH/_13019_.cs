using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("13019")]
public class _13019_ : QytetarNidF602TestBase
{
    protected override string ServiceCode => "13019";
    protected override string? ServiceTitle => "KonfirmimKreditimiLlogarie";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    [Test]
    public void KonfirmimKreditimiLlogarie()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("PËRZGJIDHNI NËSE JENI VETË DEKLARUESI, OSE PËRFAQËSUESI I AUTORIZUAR PËR KRYERJEN E DEKLARATËS"));

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert 3 hapa, hapi i pare aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(3));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));
        for (int i = 1; i < steps.Count; i++)
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert radio Deklaruesi eshte i zgjedhur");
        IWebElement selfRadio = wait.Until(ExpectedConditions.ElementExists(By.Id("applicantSelf")));
        IWebElement otherRadio = wait.Until(ExpectedConditions.ElementExists(By.Id("applicantOther")));
        Assert.That(selfRadio.GetAttribute("value"), Is.EqualTo("V"));
        Assert.That(selfRadio.Selected, Is.True);
        Assert.That(otherRadio.GetAttribute("value"), Is.EqualTo("T"));
        Assert.That(otherRadio.Selected, Is.False);

        Log("Assert label-at e radio");
        Assert.That(driver.FindElement(By.CssSelector("label[for='applicantSelf']")).Text.Trim(),
            Is.EqualTo("Deklaruesi"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='applicantOther']")).Text.Trim(),
            Is.EqualTo("Përfaqësuesi i autorizuar"));

        Log("Assert butonat e navigimit");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Kliko Vazhdo");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert popup Mesazh per NID pa pension");
        IWebElement alertModal = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector(".alert-modal-container")));
        Assert.That(alertModal.Displayed, Is.True);
        Assert.That(alertModal.FindElement(By.CssSelector(".alert-modal-icon-wrapper")).Displayed, Is.True);

        IWebElement alertTitle = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h2.alert-modal-title")));
        Assert.That(alertTitle.Text.Trim(), Is.EqualTo("Mesazh"));

        IWebElement alertMsg = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector(".alert-modal-description")));
        Assert.That(alertMsg.Text.Trim(),
            Is.EqualTo("Ju nuk mund të deklaroni konfirmimin e kreditimit të llogarisë dhe rregullshmërinë e pagesave, pasi nuk bëni pjesë në kategorinë e qytetarëve që përfitojnë pension"));

        IWebElement okBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.alert-modal-button--primary")));
        Assert.That(okBtn.Text.Trim(), Is.EqualTo("OK"));

        Log("Kliko OK ne popup");
        SafeClick(By.CssSelector("button.alert-modal-button--primary"));
        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
            By.CssSelector(".alert-modal-overlay")));

        Log("TEST PASSED");
    }

    [Test]
    public void KonfirmimKreditimiLlogariePersonAutorizuar()
    {




        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("PËRZGJIDHNI NËSE JENI VETË DEKLARUESI, OSE PËRFAQËSUESI I AUTORIZUAR PËR KRYERJEN E DEKLARATËS"));

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert 3 hapa, hapi i pare aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(3));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));
        for (int i = 1; i < steps.Count; i++)
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert radio Deklaruesi eshte i zgjedhur fillimisht");
        IWebElement selfRadio = wait.Until(ExpectedConditions.ElementExists(By.Id("applicantSelf")));
        IWebElement otherRadio = wait.Until(ExpectedConditions.ElementExists(By.Id("applicantOther")));
        Assert.That(selfRadio.GetAttribute("value"), Is.EqualTo("V"));
        Assert.That(selfRadio.Selected, Is.True);
        Assert.That(otherRadio.GetAttribute("value"), Is.EqualTo("T"));
        Assert.That(otherRadio.Selected, Is.False);

        Log("Assert label-at e radio");
        Assert.That(driver.FindElement(By.CssSelector("label[for='applicantSelf']")).Text.Trim(),
            Is.EqualTo("Deklaruesi"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='applicantOther']")).Text.Trim(),
            Is.EqualTo("Përfaqësuesi i autorizuar"));

        Log("Zgjidh Perfaqesuesi i autorizuar");
        SelectRadioById("applicantOther");
        Assert.That(driver.FindElement(By.Id("applicantOther")).Selected, Is.True);
        Assert.That(driver.FindElement(By.Id("applicantSelf")).Selected, Is.False);

        Log("Assert butonat e navigimit");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("DETAJET E PERSONIT");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("DETAJET E PERSONIT"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert 4 hapa, dy te paret aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(4));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Not.Contain("active"));
        Assert.That(steps[3].GetAttribute("class"), Does.Not.Contain("active"));
        foreach (var step in steps)
            Assert.That(step.GetAttribute("class"), Does.Contain("no-click"));

        Log("Assert fushat e detajeve te personit");
        IWebElement nidInput = FindInputByLabel("Nid");
        IWebElement emriInput = FindInputByLabel("Emri");
        IWebElement mbiemriInput = FindInputByLabel("Mbiemri");
        IWebElement bankaInput = FindInputByLabel("Banka");
        IWebElement periudhaNgaInput = FindInputByLabel("Periudha nga");
        IWebElement periudhaDeriInput = FindInputByLabel("Periudha deri");
        IWebElement adresaInput = FindInputByLabel("Adresa");

        Assert.That(nidInput.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(nidInput.GetAttribute("readonly"), Is.Null);

        Assert.That(emriInput.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(emriInput.GetAttribute("readonly"), Is.Not.Null);

        Assert.That(mbiemriInput.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(mbiemriInput.GetAttribute("readonly"), Is.Not.Null);

        Assert.That(bankaInput.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(bankaInput.GetAttribute("readonly"), Is.Not.Null);

        Assert.That(periudhaNgaInput.GetAttribute("value").Trim(), Is.EqualTo("01.09.2026"));
        Assert.That(periudhaNgaInput.GetAttribute("readonly"), Is.Not.Null);

        Assert.That(periudhaDeriInput.GetAttribute("value").Trim(), Is.EqualTo("31.03.2027"));
        Assert.That(periudhaDeriInput.GetAttribute("readonly"), Is.Not.Null);

        Assert.That(adresaInput.GetAttribute("value"), Is.EqualTo(string.Empty));

        Log("Assert dropdown Llogaria");
        var llogaria = new SelectElement(FindSelectByLabel("Llogaria"));
        Assert.That(llogaria.Options.Count, Is.EqualTo(2));
        Assert.That(llogaria.Options[0].GetAttribute("value"), Is.EqualTo("1"));
        Assert.That(llogaria.Options[0].Text.Trim(), Is.EqualTo("Kaluar rregullisht"));
        Assert.That(llogaria.Options[1].GetAttribute("value"), Is.EqualTo("2"));
        Assert.That(llogaria.Options[1].Text.Trim(), Is.EqualTo("Kaluar jo rregullisht"));

        Log("Assert dropdown Verejtje mbi pagesat");
        var verejtje = new SelectElement(FindSelectByLabel("Vërejtje mbi pagesat"));
        Assert.That(verejtje.Options.Count, Is.EqualTo(2));
        Assert.That(verejtje.Options[0].GetAttribute("value"), Is.EqualTo("1"));
        Assert.That(verejtje.Options[0].Text.Trim(), Is.EqualTo("Jo"));
        Assert.That(verejtje.Options[1].GetAttribute("value"), Is.EqualTo("2"));
        Assert.That(verejtje.Options[1].Text.Trim(), Is.EqualTo("Po"));

        Log("Assert butonat e navigimit Step 2");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Vendos NID F60214024S");
        SetReactInputValue(FindInputByLabel("Nid"), "F60214024S");

        Log("Assert popup Mesazh per deklarate te meparshme");
        AssertAlertModal(
            "Mesazh",
            "Ju keni bërë një deklaratë në e-Albania. Ju lutemi deklaratën e rradhës duhet ta bëni pranë sportelit fizik të bankës tuaj");

        Log("Kliko OK ne popup");
        DismissAlertModal();

        Log("Pastro NID dhe vendos F60416142P");
        SetReactInputValue(FindInputByLabel("Nid"), string.Empty);
        Assert.That(FindInputByLabel("Nid").GetAttribute("value"), Is.EqualTo(string.Empty));
        SetReactInputValue(FindInputByLabel("Nid"), "F60416142P");

        Log("Assert popup Mesazh per deklarate te meparshme");
        AssertAlertModal(
            "Mesazh",
            "Ju keni bërë një deklaratë në e-Albania. Ju lutemi deklaratën e rradhës duhet ta bëni pranë sportelit fizik të bankës tuaj");

        Log("Kliko OK ne popup");
        DismissAlertModal();

        Log("TEST PASSED");
    }

    private void SelectRadioById(string radioId)
    {

        SafeClick(By.Id(radioId));
        Thread.Sleep(500);
    }

    private IWebElement FindInputByLabel(string labelPart)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//div[@id='root']//form//label[contains(.,'{labelPart}')]/following-sibling::*[self::input or self::textarea]")));
    }

    private IWebElement FindSelectByLabel(string labelPart)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//div[@id='root']//form//label[contains(.,'{labelPart}')]/following-sibling::select")));
    }

    private IWebElement WaitForStepTitle(string expectedUpper)
    {

        return wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4.text-uppercase, h4.ealb-header-text"));
            foreach (var title in titles)
            {
                if (title.Text.Trim().ToUpperInvariant() == expectedUpper)
                    return title;
            }
            return null;
        });
    }

    private void SetReactInputValue(IWebElement input, string value)
    {

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            input
        );
        Thread.Sleep(200);

        ((IJavaScriptExecutor)driver).ExecuteScript(@"
            const el = arguments[0];
            const proto = el.tagName === 'TEXTAREA'
                ? window.HTMLTextAreaElement.prototype
                : window.HTMLInputElement.prototype;
            const setter = Object.getOwnPropertyDescriptor(proto, 'value').set;
            el.focus();
            setter.call(el, arguments[1]);
            el.dispatchEvent(new Event('input', { bubbles: true }));
            el.dispatchEvent(new Event('change', { bubbles: true }));
            el.blur();
        ", input, value);
        Thread.Sleep(500);
    }

    private void AssertAlertModal(string expectedTitle, string expectedDescription)
    {

        IWebElement alertModal = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector(".alert-modal-container")));
        Assert.That(alertModal.Displayed, Is.True);
        Assert.That(alertModal.FindElement(By.CssSelector(".alert-modal-icon-wrapper")).Displayed, Is.True);

        IWebElement alertTitle = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h2.alert-modal-title")));
        Assert.That(alertTitle.Text.Trim(), Is.EqualTo(expectedTitle));

        IWebElement alertMsg = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector(".alert-modal-description")));
        Assert.That(alertMsg.Text.Trim(), Is.EqualTo(expectedDescription));

        IWebElement okBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.alert-modal-button--primary")));
        Assert.That(okBtn.Text.Trim(), Is.EqualTo("OK"));
    }

    private void DismissAlertModal()
    {

        SafeClick(By.CssSelector("button.alert-modal-button--primary"));
        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
            By.CssSelector(".alert-modal-overlay")));
        Thread.Sleep(400);
    }
}