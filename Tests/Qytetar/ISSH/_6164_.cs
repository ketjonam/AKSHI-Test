using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("6164")]
public class _6164_ : QytetarNidF602TestBase
{
    protected override string ServiceCode => "6164";
    protected override string? ServiceTitle => "PensioniInvaliditetitMCAP";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    [Test]
    public void PensioniInvaliditetitMCAP()
    {
        OpenNewApplicationFromServicePage("Pensioni i invaliditetit (KMCAP)");

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert nje hap aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(1));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));

        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.px-4.pb-2.text-uppercase")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E INVALIDITETIT"));

        Log("Assert mesazhi i bosh");
        IWebElement emptyMessage = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[contains(@class,'text-center') and normalize-space()='Nuk gjëndet informacion per kategorinë e kërkuar!']")));
        Assert.That(emptyMessage.Displayed, Is.True, "Mesazhi nuk eshte visible");
        Assert.That(emptyMessage.Text.Trim(),
            Is.EqualTo("Nuk gjëndet informacion per kategorinë e kërkuar!"));

        Log("Assert nuk ka buton Vazhdo");
        Assert.That(driver.FindElements(By.CssSelector("button.ealb-btn-continue")).Count, Is.EqualTo(0));

        Log("Assert butoni Kthehu");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        Assert.That(backBtn.Displayed, Is.True, "Butoni Kthehu nuk eshte visible");
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));

        Log("Kliko butonin Kthehu");
        SafeClick(By.CssSelector("button.ealb-btn-back"));
        Thread.Sleep(2000);

        Log("TEST PASSED");
    }
}
