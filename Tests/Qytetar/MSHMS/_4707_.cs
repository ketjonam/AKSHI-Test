using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.MSHMS;

[Category("MSHMS")]
[Category("4707")]
public class _4707_ : QytetarNidF602TestBase
{
    protected override string ServiceCode => "4707";
    protected override string? ServiceTitle => "ListaBarnaveTeRimbursueshme";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void ListaBarnaveTeRimbursueshme()
    {
        Log("Assert page header");
        IWebElement headerContainer = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("div.page-header-container")));
        Assert.That(headerContainer.Displayed, Is.True, "Page header nuk eshte visible");

        IWebElement serviceName = wait.Until(ExpectedConditions.ElementIsVisible(
            By.Id("serviceNameBreadcrumb")));
        Assert.That(serviceName.Displayed, Is.True, "Breadcrumb i sherbimit nuk eshte visible");
        Assert.That(serviceName.Text.Trim(), Is.EqualTo("Lista e çmimeve të barnave të rimbursueshme"),
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

        Log("Assert popup E RENDESISHME");
        IWebElement alertModal = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("div.alert-modal-container")));
        Assert.That(alertModal.Displayed, Is.True, "Popup nuk eshte visible");

        IWebElement alertIcon = alertModal.FindElement(By.CssSelector("div.alert-modal-icon-wrapper"));
        Assert.That(alertIcon.Displayed, Is.True, "Ikona e popup nuk eshte visible");

        IWebElement alertTitle = alertModal.FindElement(By.CssSelector("h2.alert-modal-title"));
        Assert.That(alertTitle.Text.Trim(), Is.EqualTo("E RËNDËSISHME!"),
            "Titulli i popup nuk eshte i sakte");

        IWebElement alertDescription = alertModal.FindElement(By.CssSelector("div.alert-modal-description"));
        string alertDescriptionText = string.Join(" ",
            alertDescription.Text.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries));
        Assert.That(alertDescriptionText, Is.EqualTo(
            "Përshëndetje Kadri Kukaj, mjeku juaj i familjes është Elisa Stolaj me numër telefoni 00393401143018 në Qendrën Shëndetësore: Koplik - Qender."),
            "Pershkrimi i popup nuk eshte i sakte");

        IWebElement vazhdoBtn = alertModal.FindElement(
            By.CssSelector("button.alert-modal-button.alert-modal-button--primary"));
        Assert.That(vazhdoBtn.Displayed, Is.True, "Butoni Vazhdo nuk eshte visible");
        Assert.That(vazhdoBtn.Text.Trim(), Is.EqualTo("Vazhdo"), "Butoni i popup nuk eshte Vazhdo");

        Log("Kliko Vazhdo ne popup");
        SafeClick(By.CssSelector("button.alert-modal-button.alert-modal-button--primary"));
        Thread.Sleep(1500);

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

        Log("Assert Title");
        IWebElement titleElement = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase")));
        Assert.That(titleElement.Text.Trim(),
            Is.EqualTo("Publikimi dhe konsultimi i çmimeve të listës së barnave të rimbursueshme"),
            "Titulli nuk eshte i sakte");

        Log("Assert kohezgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("1 minutë kohëzgjatje"));

        Log("Assert nje hap aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(1));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));

        Log("Assert mesazhin se lista eshte bosh");
        IWebElement mesazhi = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("p.text-muted.fs-5")));
        Assert.That(mesazhi.Text.Trim(), Is.EqualTo("Lista është bosh"));

        Log("Assert butoni Kthehu");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        Assert.That(backBtn.Displayed, Is.True, "Butoni Kthehu nuk eshte visible");
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));

        Log("TEST PASSED");
    }
}
