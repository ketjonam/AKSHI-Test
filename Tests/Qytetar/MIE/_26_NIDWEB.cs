using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.MIE;

[Category("MIE")]
[Category("26")]
public class _26_NIDWEB : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "26";
    protected override string? ServiceTitle => "_26_Individ";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    [Test]
    public void _26_Individ()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert Step1 Title");
        IWebElement step1Title = wait.Until(
            ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/main/div[3]/div/div/div/div/h5"))
        );
        Assert.That(step1Title.Text, Is.EqualTo("TË DHËNA NGA OPERATORI I SHPËRNDARJES SË ENERGJISË ELEKTRIKE"));

        Log("Click Afisho pa kontrate");
        SafeClick(By.XPath("/html/body/div[1]/main/div[3]/div/div/div/div/form/div/div[2]/div[2]/div[2]/div/button"));

        Log("Assert Kujdes modal");
        IWebElement alertModal = wait.Until(
            ExpectedConditions.ElementIsVisible(By.CssSelector(".alert-modal-container"))
        );
        Assert.That(alertModal.Displayed, Is.True, "Alert modal nuk u shfaq.");

        IWebElement alertTitle = driver.FindElement(By.CssSelector(".alert-modal-title"));
        Assert.That(alertTitle.Text, Is.EqualTo("Kujdes!"));

        IWebElement alertDescription = driver.FindElement(By.CssSelector(".alert-modal-description"));
        Assert.That(alertDescription.Text, Does.Contain("Plotësoni fushën"));

        Log("Close Kujdes modal");
        SafeClick(By.CssSelector(".alert-modal-button--primary"));

        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
            By.CssSelector(".alert-modal-container")));

        Log("Insert contract");
        driver.FindElement(By.Id("contractNumber")).SendKeys("TR1B030402626107");
        SafeClick(By.XPath("/html/body/div[1]/main/div[3]/div/div/div/div/form/div/div[2]/div[2]/div[2]/div/button"));

        Log("Assert table row");
        IWebElement row = wait.Until(
            ExpectedConditions.ElementExists(By.XPath("//table/tbody/tr"))
        );

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            row
        );

        Assert.That(row.Displayed, Is.True, "Rreshti i tabelës nuk u shfaq.");

        Thread.Sleep(2000);

        Log("Click Ruaj kontraten");
        SafeClick(By.CssSelector(".btn.btn-outline-secondary.px-3.py-2"));

        Thread.Sleep(500);

        Log("Fill save contract popup");
        driver.FindElement(By.Id("contractNumber")).SendKeys("189915-1");
        driver.FindElement(By.Id("description")).SendKeys("Test Automation");
        SafeClick(By.XPath("//button[normalize-space()='Ruaj']"));

        Thread.Sleep(2000);

        Log("Assert success modal");
        IWebElement successModal = wait.Until(
            ExpectedConditions.ElementIsVisible(By.CssSelector(".alert-modal-container"))
        );
        Assert.That(successModal.Displayed, Is.True, "Success modal nuk u shfaq.");

        IWebElement successTitle = driver.FindElement(By.CssSelector(".alert-modal-title"));
        Assert.That(successTitle.Text, Is.EqualTo("Sukses"));

        IWebElement successDescription = driver.FindElement(By.CssSelector(".alert-modal-description"));
        Assert.That(successDescription.Text, Is.EqualTo("Shtimi u krye me sukses"));

        Log("Close success modal");
        SafeClick(By.CssSelector(".alert-modal-button--primary"));

        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
            By.CssSelector(".alert-modal-container")));

        Log("Select existing contract");
        SafeClick(By.Id("existingContract"));

        Thread.Sleep(2000);

        Log("Choose existing contract from dropdown");
        SelectElement contractSelect = new SelectElement(
            wait.Until(ExpectedConditions.ElementExists(By.Id("contractSelect")))
        );
        contractSelect.SelectByValue("01234325235");

        Log("Click Afisho for existing contract");
        SafeClick(By.XPath("/html/body/div[1]/main/div[3]/div/div/div/div/form/div/div[2]/div[2]/div[2]/div/button"));

        Log("TEST PASSED");
    }

}