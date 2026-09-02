using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.MIE;

[Category("MIE")]
[Category("365")]
public class IndividWeb365 : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "365";
    protected override string? ServiceTitle => "NIDWeb365";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName = "Faturat e Ujësjellës Kanalizime Tiranë, UKT";
    private const string ContractNumber = "189915-1";
    private const string WarningMessage =
        "Plotësoni fushën e kodit të klientit ose përzgjidh një kontratë dhe klikoni butonin 'Afisho'";
    private const string ExistingContractWarningMessage = "Ju lutemi përzgjidhni një kontratë";

    [Test]
    public void NIDWeb365()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert nje hap aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(1));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));

        Log("Assert Step 1 Title");
        IWebElement step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.px-4.pb-4.text-uppercase")));
        Assert.That(step1Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("TË DHËNA NGA UKT"));

        Log("Assert mesazhin e kujdesit ne forme");
        IWebElement formWarning = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector(".ealb-bg-form-section h6")));
        Assert.That(formWarning.Text.Trim(), Does.Contain(WarningMessage));

        Log("Assert opsionet e kontrates");
        IWebElement newContract = wait.Until(ExpectedConditions.ElementExists(By.Id("newContract")));
        IWebElement existingContract = wait.Until(ExpectedConditions.ElementExists(By.Id("existingContract")));
        Assert.That(newContract.Selected, Is.True, "Kontrate e re duhet te jete e perzgjedhur");
        Assert.That(existingContract.Selected, Is.False);
        Assert.That(
            driver.FindElement(By.XPath("//input[@id='newContract']/following-sibling::span")).Text.Trim(),
            Is.EqualTo("Kontratë e re"));
        Assert.That(
            driver.FindElement(By.XPath("//input[@id='existingContract']/following-sibling::span")).Text.Trim(),
            Is.EqualTo("Kontratë ekzistuese"));

        Log("Assert fushen e kodit te klientit");
        IWebElement contractLabel = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("label[for='contractNumber']")));
        Assert.That(contractLabel.Text.Trim(), Is.EqualTo("Kodi i klientit"));
        IWebElement contractInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("contractNumber")));
        Assert.That(InputValue(contractInput), Is.EqualTo(string.Empty));

        Log("Assert butonin Afisho");
        IWebElement afishoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//button[normalize-space()='Afisho']")));
        Assert.That(afishoBtn.Displayed, Is.True);

        Log("Click Afisho pa kontrate");
        SafeClick(By.XPath("//button[normalize-space()='Afisho']"));

        Log("Assert Kujdes modal");
        AssertKujdesModal(WarningMessage);

        Log("Close Kujdes modal");
        CloseKujdesModal();

        Log("Zgjidh Kontrate ekzistuese");
        SafeClick(By.Id("existingContract"));
        existingContract = wait.Until(ExpectedConditions.ElementExists(By.Id("existingContract")));
        Assert.That(existingContract.Selected, Is.True, "Kontrate ekzistuese duhet te jete e perzgjedhur");

        Log("Assert dropdown e kontratave ekzistuese");
        IWebElement contractSelectEl = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("contractSelect")));
        Assert.That(contractSelectEl.GetAttribute("class"), Does.Contain("ealb-input"));
        Assert.That(contractSelectEl.GetAttribute("class"), Does.Contain("form-select"));
        var contractSelect = new SelectElement(contractSelectEl);
        Assert.That(contractSelect.Options.Count, Is.EqualTo(1));
        Assert.That(contractSelect.Options[0].GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(contractSelect.Options[0].GetAttribute("disabled"), Is.Not.Null);
        Assert.That(contractSelect.Options[0].Text.Trim(), Is.EqualTo("Nuk ka asnjë kontratë"));

        Log("Click Afisho pa kontrate ekzistuese");
        SafeClick(By.XPath("//button[normalize-space()='Afisho']"));
        AssertKujdesModal(ExistingContractWarningMessage);

        Log("Close Kujdes modal");
        CloseKujdesModal();

        Log("Kthehu te Kontrate e re");
        SafeClick(By.Id("newContract"));

        Log("Insert contract");
        contractInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("contractNumber")));
        contractInput.Clear();
        contractInput.SendKeys(ContractNumber);
        SafeClick(By.XPath("//button[normalize-space()='Afisho']"));

        Log("Assert butonin Ruaj kontraten");
        IWebElement ruajKontratenBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//button[normalize-space()='Ruaj kontratën']")));
        Assert.That(ruajKontratenBtn.Displayed, Is.True, "Butoni Ruaj kontratën nuk u shfaq.");
        Assert.That(ruajKontratenBtn.Text.Trim(), Is.EqualTo("Ruaj kontratën"));

        Log("Click Ruaj kontraten");
        SafeClick(By.XPath("//button[normalize-space()='Ruaj kontratën']"));

        Log("Assert modalin Ruaj kontraten");
        IWebElement saveModal = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector(".modal-content")));
        Assert.That(saveModal.Displayed, Is.True, "Modali Ruaj kontratën nuk u shfaq.");
        Assert.That(
            saveModal.FindElement(By.CssSelector(".modal-title")).Text.Trim(),
            Is.EqualTo("Ruaj kontratën"));
        Assert.That(
            saveModal.FindElement(By.CssSelector("label[for='contractNumber']")).Text.Trim(),
            Does.Contain("Kontrata"));
        Assert.That(
            saveModal.FindElement(By.Id("contractNumber")).GetAttribute("value")?.Trim(),
            Is.EqualTo(ContractNumber));
        Assert.That(
            saveModal.FindElement(By.CssSelector("label[for='description']")).Text.Trim(),
            Does.Contain("Përshkrimi"));
        Assert.That(
            saveModal.FindElement(By.Id("description")).GetAttribute("value")?.Trim(),
            Is.EqualTo(string.Empty));
        Assert.That(
            saveModal.FindElement(By.XPath(".//button[normalize-space()='Mbyll']")).Displayed,
            Is.True);
        Assert.That(
            saveModal.FindElement(By.XPath(".//button[normalize-space()='Ruaj']")).Displayed,
            Is.True);

        Log("Fill save contract popup");
        IWebElement descriptionInput = saveModal.FindElement(By.Id("description"));
        descriptionInput.Clear();
        descriptionInput.SendKeys("Test Automation");
        SafeClick(By.XPath("//div[contains(@class,'modal-content')]//button[normalize-space()='Ruaj']"));

        Log("Assert success modal");
        IWebElement successModal = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector(".alert-modal-container")));
        Assert.That(successModal.Displayed, Is.True, "Success modal nuk u shfaq.");
        Assert.That(
            successModal.FindElement(By.CssSelector(".alert-modal-title")).Text.Trim(),
            Is.EqualTo("Sukses"));
        Assert.That(
            successModal.FindElement(By.CssSelector(".alert-modal-description")).Text.Trim(),
            Is.EqualTo("Shtimi u krye me sukses"));

        Log("Close success modal");
        SafeClick(By.CssSelector(".alert-modal-button--primary"));
        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
            By.CssSelector(".alert-modal-container")));

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
        DismissCookieBannerIfPresent();
    }

    private void AssertKujdesModal(string expectedDescription)
    {
        IWebElement alertModal = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector(".alert-modal-container")));
        Assert.That(alertModal.Displayed, Is.True, "Alert modal nuk u shfaq.");
        Assert.That(
            alertModal.FindElement(By.CssSelector(".alert-modal-title")).Text.Trim(),
            Is.EqualTo("Kujdes!"));
        Assert.That(
            alertModal.FindElement(By.CssSelector(".alert-modal-description")).Text.Trim(),
            Is.EqualTo(expectedDescription));
        Assert.That(
            alertModal.FindElement(By.CssSelector(".alert-modal-button--primary")).Text.Trim(),
            Is.EqualTo("Mbyll"));
    }

    private void CloseKujdesModal()
    {
        SafeClick(By.CssSelector(".alert-modal-button--primary"));
        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
            By.CssSelector(".alert-modal-container")));
    }
}
