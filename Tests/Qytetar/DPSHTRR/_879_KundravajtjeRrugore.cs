using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.DPSHTRR;

[Category("DPSHTRR")]
[Category("879")]
public class _879_KundravajtjeRrugore : QytetarNidJ257TestBase
{
    protected override string ServiceCode => "879";
    protected override string? ServiceTitle => "KundravajtjeRrugore";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void KundravajtjeRrugore()
    {


string titleXpath = "/html/body/div/main/div[3]/div/div/div/div/div[2]/div[1]/div/div[1]/h5";

        

        Log("Assert Title");
        IWebElement titleElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(titleXpath)));
        Log("Title text: " + titleElement.Text.Trim());
        Assert.That(titleElement.Displayed, Is.True, "Titulli nuk eshte visible");


        Log("Zgjidh automjetin");
        driver.FindElement(By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div[1]/div/div[2]/div/div/div/div/div[2]/div/div[1]/button")).Click();

        Log("Assert gjobat");

        IWebElement gjobatAlert = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[@role='alert' and contains(normalize-space(.), 'Nuk ka gjoba për automjetin me targë')]")
        ));

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            gjobatAlert
        );

        Thread.Sleep(500);

        Log("Gjobat text: " + gjobatAlert.Text.Trim());

        Assert.That(
            gjobatAlert.Text.Trim(),
            Is.EqualTo("Nuk ka gjoba për automjetin me targë 'AB166DP'!")
        );


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