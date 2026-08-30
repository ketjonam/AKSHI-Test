using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.DPSHTRR;

[Category("DPSHTRR")]
[Category("772")]
public class _772_NID : QytetarNidJ257TestBase
{
    protected override string ServiceCode => "772";
    protected override string? ServiceTitle => "TaksaVjetore";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void TaksaVjetore()
    {


string titleXpath = "/html/body/div/main/div[3]/div/div/div/div[2]/div[1]/div/div/h5";

        

        Log("Assert Title");
        IWebElement titleElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(titleXpath)));
        Log("Title text: " + titleElement.Text.Trim());
        Assert.That(titleElement.Displayed, Is.True, "Titulli nuk eshte visible");


        Log("Zgjidh automjetin");
       driver.FindElement(By.XPath("/html/body/div/main/div[3]/div/div/div/div[2]/div[2]/div/table/tbody/tr/td[1]/button")).Click();

        Log("Assert faturen");
        IWebElement faturaElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/main/div[3]/div/div/div/div[2]/div[4]/div/table/tbody/tr[1]/td[1]")));
        Assert.That(faturaElement.Text.Trim(), Is.EqualTo("2500880811"));

        Log("Click 'llogarit'");
        IWebElement llogaritBtn = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("/html/body/div/main/div[3]/div/div/div/div[2]/div[7]/div/button")
        ));

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            llogaritBtn
        );

        Thread.Sleep(500);

        try
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("/html/body/div/main/div[3]/div/div/div/div[2]/div[7]/div/button")
            )).Click();
        }
        catch (ElementClickInterceptedException)
        {
            Log("Klikimi normal i 'llogarit' u bllokua, po provoj me JS click");
            llogaritBtn = driver.FindElement(By.XPath("/html/body/div/main/div[3]/div/div/div/div[2]/div[7]/div/button"));

            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].click();",
                llogaritBtn
            );
        }

        Thread.Sleep(1000);

        Log("Assert Taksa");
        IWebElement TaksaRow = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("/html/body/div/main/div[3]/div/div/div/div[2]/div[9]/div/table/tbody/tr")
        ));

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            TaksaRow
        );

        Thread.Sleep(500);

        Log("TaksaRow text: " + TaksaRow.Text.Trim());
        Assert.That(TaksaRow.Displayed, Is.True, "Taksa row nuk eshte visible");


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