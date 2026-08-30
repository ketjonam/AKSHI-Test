using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.MSHMS;

[Category("MSHMS")]
[Category("4435")]
public class _4435_ : QytetarNidF602TestBase
{
    protected override string ServiceCode => "4435";
    protected override string? ServiceTitle => "NdihmaEkonomikeIndivid";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void NdihmaEkonomikeIndivid()
    {
        Log("Assert page header");
        IWebElement headerContainer = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("div.page-header-container")));
        Assert.That(headerContainer.Displayed, Is.True, "Page header nuk eshte visible");

        IWebElement serviceName = wait.Until(ExpectedConditions.ElementIsVisible(
            By.Id("serviceNameBreadcrumb")));
        Assert.That(serviceName.Displayed, Is.True, "Breadcrumb i sherbimit nuk eshte visible");
        Assert.That(serviceName.Text.Trim(), Is.EqualTo("Ndihma ekonomike për individë"),
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

        Log("Assert kohezgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert nje hap aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(1));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));

        Log("Assert Title");
        IWebElement titleElement = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase")));
        Assert.That(titleElement.Text.Trim(), Is.EqualTo("Ndihma ekonomike"),
            "Titulli nuk eshte Ndihma ekonomike");

        Log("Assert Informacioni per aplikant");
        AssertReadonlyField("NID", CitizenNid);
        AssertReadonlyField("Emri", "Kadri");
        AssertReadonlyField("Mbiemri", "Kukaj");
        AssertReadonlyField("Atësia", "Deli");
        AssertReadonlyField("Amësia", "Tale");
        AssertReadonlyField("Gjinia", "M");
        AssertReadonlyField("Datëlindja", "16/04/1956");
        AssertReadonlyField("Vendbanimi", "MALËSI E MADHE");

        Log("Assert mesazhin se nuk ka ndihme ekonomike");
        IWebElement mesazhi = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[contains(@class,'text-center') and normalize-space()='Ju nuk përfitoni ndihmë ekonomike']")));
        Assert.That(mesazhi.Displayed, Is.True, "Mesazhi nuk eshte visible");
        Assert.That(mesazhi.Text.Trim(), Is.EqualTo("Ju nuk përfitoni ndihmë ekonomike"));

        Log("Assert butoni Kthehu");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        Assert.That(backBtn.Displayed, Is.True, "Butoni Kthehu nuk eshte visible");
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));

        Log("TEST PASSED");
    }

    private void AssertReadonlyField(string label, string expectedValue)
    {
        IWebElement input = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//label[contains(@class,'form-label') and normalize-space()='{label}']/following-sibling::input[contains(@class,'ealb-input')]")));
        Assert.That(input.GetAttribute("readonly"), Is.Not.Null, $"Fusha {label} duhet te jete readonly");
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue),
            $"Vlera e fushes {label} nuk eshte e sakte");
    }
}
