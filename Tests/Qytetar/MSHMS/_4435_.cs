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


string titleXpath = "/html/body/div[1]/main/div[3]/div/div/div/div/div/h4";
        

        Log("Assert Title");
        IWebElement titleElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(titleXpath)));
        Log("Title text: " + titleElement.Text.Trim());
        Assert.That(titleElement.Displayed, Is.True, "Titulli nuk eshte visible");

        Log("Assert Informacioni per aplikant");
        IWebElement NID = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("/html/body/div[1]/main/div[3]/div/div/div/div/div/form/div/div/div[1]/input")
        )); 
        Assert.That(NID.GetAttribute("value").Trim(), Is.EqualTo(CitizenNid));    
        IWebElement Emri = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("/html/body/div[1]/main/div[3]/div/div/div/div/div/form/div/div/div[2]/input")
        ));
        Assert.That(Emri.GetAttribute("value").Trim(), Is.EqualTo("Aishe"));
        IWebElement Mbiemri = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("/html/body/div[1]/main/div[3]/div/div/div/div/div/form/div/div/div[3]/input")
        )); Assert.That(Mbiemri.GetAttribute("value").Trim(), Is.EqualTo("Mema"));
        IWebElement Atesia = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("/html/body/div[1]/main/div[3]/div/div/div/div/div/form/div/div/div[4]/input")
        )); Assert.That(Atesia.GetAttribute("value").Trim(), Is.EqualTo("Alush"));
        IWebElement Amesia = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("/html/body/div[1]/main/div[3]/div/div/div/div/div/form/div/div/div[5]/input")
        )); Assert.That(Amesia.GetAttribute("value").Trim(), Is.EqualTo("Hatixhe"));
        IWebElement Gjinia = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("/html/body/div[1]/main/div[3]/div/div/div/div/div/form/div/div/div[6]/input")
        )); Assert.That(new SelectElement(Gjinia).SelectedOption.Text.Trim(), Is.EqualTo("F"));
        IWebElement Datelindja = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("/html/body/div[1]/main/div[3]/div/div/div/div/div/form/div/div/div[7]/input")
        )); Assert.That(Datelindja.GetAttribute("value").Trim(), Is.EqualTo("11/05/1963"));
        IWebElement Vendbanimi = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("/html/body/div[1]/main/div[3]/div/div/div/div/div/form/div/div/div[8]/input")
        )); Assert.That(Vendbanimi.GetAttribute("value").Trim(), Is.EqualTo("KAVAJË"));

        Log("Assert mesazhin se nuk ka ndihme ekonomike");
        IWebElement mesazhi = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("/html/body/div[1]/main/div[3]/div/div/div/div/div/div[2]")
        ));
        Assert.That(mesazhi.Text.Trim(), Is.EqualTo("Ju nuk përfitoni ndihmë ekonomike"));

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

    private void ClearFilterInput(By locator)
    {

        Log("Clear filter input with Ctrl+A + Delete");
        IWebElement input = wait.Until(ExpectedConditions.ElementIsVisible(locator));

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            input
        );

        input.Click();
        Thread.Sleep(300);

        input.SendKeys(Keys.Control + "a");
        Thread.Sleep(200);
        input.SendKeys(Keys.Delete);
        Thread.Sleep(500);

        string currentValue = input.GetAttribute("value") ?? string.Empty;
        Log("Filter value after keyboard clear: '" + currentValue + "'");

        if (!string.IsNullOrEmpty(currentValue))
        {
            Log("Keyboard clear nuk mjaftoi, provoj me JS");
            ((IJavaScriptExecutor)driver).ExecuteScript(@"
                const el = arguments[0];
                el.value = '';
                el.dispatchEvent(new Event('input', { bubbles: true }));
                el.dispatchEvent(new Event('change', { bubbles: true }));
            ", input);

            Thread.Sleep(500);
        }

        BlurActiveElement();
        Thread.Sleep(800);

        input = wait.Until(ExpectedConditions.ElementIsVisible(locator));
        currentValue = input.GetAttribute("value") ?? string.Empty;
        Log("Filter value final: '" + currentValue + "'");
    }
}