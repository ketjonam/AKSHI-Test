using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.MIE;

[Category("MIE")]
[Category("11133")]
public class _11133_NID_Web : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "11133";
    protected override string? ServiceTitle => "Aplikim_i_Ri_NID_11133";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Aplikim për rinovimin e kategorive të licencës individuale në studim e projektim dhe/ose në mbikëqyrje e kolaudim";

    [Test]
    public void Aplikim_i_Ri_NID_11133()
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
        Assert.That(perdorBtn.Text.Trim(), Is.EqualTo("Përdor"), "Butoni nuk eshte Përdor");

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

        Log("Assert popup Kujdes");
        IWebElement alertModal = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("div.alert-modal-container")));
        Assert.That(alertModal.Displayed, Is.True, "Popup nuk eshte visible");

        IWebElement alertIcon = alertModal.FindElement(By.CssSelector("div.alert-modal-icon-wrapper"));
        Assert.That(alertIcon.Displayed, Is.True, "Ikona e popup nuk eshte visible");

        IWebElement alertTitle = alertModal.FindElement(By.CssSelector("h2.alert-modal-title"));
        Assert.That(alertTitle.Text.Trim(), Is.EqualTo("Kujdes!"),
            "Titulli i popup nuk eshte i sakte");

        IWebElement alertDescription = alertModal.FindElement(By.CssSelector("div.alert-modal-description"));
        Assert.That(alertDescription.Text.Trim(),
            Is.EqualTo("Ju nuk keni asnjë licencë dhe nuk mund të aplikoni për rinovim ose dublikatë"),
            "Pershkrimi i popup nuk eshte i sakte");

        IWebElement mbyllBtn = alertModal.FindElement(
            By.CssSelector("button.alert-modal-button.alert-modal-button--primary"));
        Assert.That(mbyllBtn.Displayed, Is.True, "Butoni Mbyll nuk eshte visible");
        Assert.That(mbyllBtn.Text.Trim(), Is.EqualTo("Mbyll"), "Butoni i popup nuk eshte Mbyll");

        Log("Kliko Mbyll ne popup");
        SafeClick(By.CssSelector("button.alert-modal-button.alert-modal-button--primary"));
        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
            By.CssSelector(".alert-modal-container")));

        Log("TEST PASSED");
    }
}
