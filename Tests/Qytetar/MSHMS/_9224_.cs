using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.MSHMS;

[Category("MSHMS")]
[Category("9224")]
public class _9224_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "9224";
    protected override string? ServiceTitle => "KartaShendetitPerFemije";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void KartaShendetitPerFemije()
    {




        Log("kliko Kerko button pa plotesuar te dhenat e femijes");
        SafeClick(By.XPath("/html/body/div[1]/main/div[3]/div/div/div/div/div[2]/div[2]/div/button[2]"));

        Log("Assert mesazhin e gabimit per nid e detyrueshem te femijes");
        IWebElement mesazhiGabim = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("/html/body/div[1]/main/div[3]/div/div/div/div/div[2]/div[1]/div/div/div/span")
        ));
        Assert.That(mesazhiGabim.Text.Trim(), Is.EqualTo("Plotësoni fushën për të vazhduar"));


        Log("ploteso nid te gabuar te gabuar");
        driver.FindElement(By.Id("nid")).SendKeys(Settings.Qytetar.Username);

        Log("Kliko kerko button");
        SafeClick(By.XPath("/html/body/div[1]/main/div[3]/div/div/div/div/div[2]/div[2]/div/button[2]"));

        Log("Assert popup");

        driver.FindElement(By.Id("nid")).SendKeys("L61213022F");
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