using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Biznes.MIE;

[Category("MIE")]
[Category("365")]
public class BiznesWeb365 : BiznesTestBase
{
    protected override string ServiceCode => "365";
    protected override string? ServiceTitle => "NIPTWeb365";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void NIPTWeb365()
    {

        

        Log("Click Aplikimi i Ri");
        SafeClick(By.XPath("//button[@aria-label='Aplikim i ri']"));

        Log("Click Afisho pa kontrate");
        SafeClick(By.XPath("/html/body/div/main/div[3]/div/div/div/div/form/div/div[2]/div[2]/div[2]/div/button"));

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
        driver.FindElement(By.Id("contractNumber")).SendKeys("189915-1");
        SafeClick(By.XPath("/html/body/div/main/div[3]/div/div/div/div/form/div/div[2]/div[2]/div[2]/div/button"));

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

        Log("Assert error message");
        IWebElement errorMessage = wait.Until(
            ExpectedConditions.ElementIsVisible(By.CssSelector("p.text-danger"))
        );

        Assert.That(errorMessage.Displayed, Is.True, "Mesazhi i gabimit nuk u shfaq.");
        Assert.That(
            errorMessage.Text,
            Is.EqualTo("Kodi i klientit nuk ekziston. Ju lutemi vendosni kodin e saktë të klientit")
        );

        Log("TEST PASSED");
    }

}