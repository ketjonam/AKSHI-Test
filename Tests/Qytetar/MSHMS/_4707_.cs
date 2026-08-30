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


string titleXpath = "/html/body/div[1]/main/div[3]/div/div/div/div/h4";
        

        Log("Assert Title");
        IWebElement titleElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(titleXpath)));
        Log("Title text: " + titleElement.Text.Trim());
        Assert.That(titleElement.Displayed, Is.True, "Titulli nuk eshte visible");

        Log("Assert mesazhin se nuk ka rezultate");
        IWebElement mesazhi = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("/html/body/div[1]/main/div[3]/div/div/div/div/div[2]/div/div/p")
        ));
        Assert.That(mesazhi.Text.Trim(), Is.EqualTo("Lista është bosh"));

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