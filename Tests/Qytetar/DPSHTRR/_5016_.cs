using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.DPSHTRR;

[Category("DPSHTRR")]
[Category("5016")]
public class _5016_ : QytetarNidJ257TestBase
{
    protected override string ServiceCode => "5016";
    protected override string? ServiceTitle => "GjendjaAktiveeMjetit";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void GjendjaAktiveeMjetit()
    {


string titleXpath = "/html/body/div/main/div[3]/div/div/div/div/h4";

        

        Log("Assert Title");
        IWebElement titleElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(titleXpath)));
        Log("Title text: " + titleElement.Text.Trim());
        Assert.That(titleElement.Displayed, Is.True, "Titulli nuk eshte visible");


        Log("Zgjidh automjetin");
        driver.FindElement(By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div[1]/div/div[1]/div/table/tbody/tr/td[1]/button")).Click();

        Log("Assert gjendja e mjetit");

        IWebElement gjendjaSection = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[@class='mt-5' and .//h6[contains(.,'Gjendja e mjetit me targë:')]]")
        ));

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            gjendjaSection
        );

        Thread.Sleep(500);

        Log("Assert titulli i seksionit");

        IWebElement TitulliSeksionit = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[@class='mt-5' and .//h6[contains(.,'Gjendja e mjetit me targë:')]]//h6[contains(.,'Gjendja e mjetit me targë:')]")
        ));

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            TitulliSeksionit
        );

        Thread.Sleep(500);

        Log("Title text: " + TitulliSeksionit.Text.Trim());
        Assert.That(TitulliSeksionit.Text.Trim(), Does.Contain("Gjendja e mjetit me targë:"));
        Assert.That(TitulliSeksionit.Text.Trim(), Does.Contain("AB166DP"));

        Log("Assert header row");
        IWebElement headerRow = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[@class='mt-5' and .//h6[contains(.,'Gjendja e mjetit me targë:')]]//table/thead/tr")
        ));

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            headerRow
        );

        Thread.Sleep(500);

        var headerCells = headerRow.FindElements(By.XPath("./th"));

        string[] expectedHeaders = new string[]
        {
    "Kartela",
    "Data e lejes",
    "Statusi i mjetit"
        };

        Log($"Numri i header cells: {headerCells.Count}");
        Assert.That(headerCells.Count, Is.EqualTo(expectedHeaders.Length),
            $"Numri i header-ave nuk përputhet. Actual: {headerCells.Count}, Expected: {expectedHeaders.Length}");

        for (int i = 0; i < expectedHeaders.Length; i++)
        {
            string actual = headerCells[i].Text.Trim();
            string expected = expectedHeaders[i];

            Log($"Header[{i}] -> Actual: '{actual}' | Expected: '{expected}'");
            Assert.That(actual, Is.EqualTo(expected), $"Header mismatch në kolonën {i}");
        }

        Log("Assert data row");
        IWebElement dataRow = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[@class='mt-5' and .//h6[contains(.,'Gjendja e mjetit me targë:')]]//table/tbody/tr")
        ));

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            dataRow
        );

        Thread.Sleep(500);

        var dataCells = dataRow.FindElements(By.XPath("./td"));

        string[] expectedRow = new string[]
        {
    "DRD00857221",
    "17.06.2021",
    ""
        };

        Log($"Numri i data cells: {dataCells.Count}");
        Assert.That(dataCells.Count, Is.EqualTo(expectedRow.Length),
            $"Numri i kolonave nuk përputhet. Actual: {dataCells.Count}, Expected: {expectedRow.Length}");

        for (int i = 0; i < expectedRow.Length; i++)
        {
            string actual = dataCells[i].Text.Trim();
            string expected = expectedRow[i];

            Log($"Cell[{i}] -> Actual: '{actual}' | Expected: '{expected}'");
            Assert.That(actual, Is.EqualTo(expected), $"Cell mismatch në kolonën {i}");
        }

        Log("Assert 'Gjendje e TVMP'");
        IWebElement tvmpStatus = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("tvmpStatus")));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            tvmpStatus
        );
        Thread.Sleep(300);
        Log("tvmpStatus value: " + (tvmpStatus.GetAttribute("value") ?? ""));
        Assert.That(tvmpStatus.GetAttribute("value") ?? "", Is.EqualTo("Mjeti nuk ka detyrime ne taksa"));

        Log("Assert 'Gjendja e gjobës KTV'");
        IWebElement ktvFine = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("ktvFine")));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            ktvFine
        );
        Thread.Sleep(300);
        Log("ktvFine value: " + (ktvFine.GetAttribute("value") ?? ""));
        Assert.That(ktvFine.GetAttribute("value") ?? "", Is.EqualTo("Mjeti nuk ka detyrime ne gjoba"));

        Log("Assert 'Gjendja e gjobave të kontrollit në rrugë'");
        IWebElement roadFine = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("roadFine")));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            roadFine
        );
        Thread.Sleep(300);
        Log("roadFine value: " + (roadFine.GetAttribute("value") ?? ""));
        Assert.That(roadFine.GetAttribute("value") ?? "", Is.EqualTo("Mjeti nuk ka detyrime ne gjoba"));

        Log("Assert 'Gjendja e dosjes'");
        IWebElement fileStatus = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("fileStatus")));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            fileStatus
        );
        Thread.Sleep(300);
        Log("fileStatus value: " + (fileStatus.GetAttribute("value") ?? ""));
        Assert.That(fileStatus.GetAttribute("value") ?? "", Is.EqualTo("Mjeti nuk ka bllokime"));

        Log("Assert 'Statusi'");
        IWebElement vehicleStatus = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("vehicleStatus")));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            vehicleStatus
        );
        Thread.Sleep(300);
        Log("vehicleStatus value: '" + (vehicleStatus.GetAttribute("value") ?? "") + "'");
        Assert.That(vehicleStatus.GetAttribute("value") ?? "", Is.EqualTo(""));

        Log("Assert butoni 'Shkarko dokumentin e vulosur'");
        IWebElement downloadButton = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[@class='mt-5' and .//h6[contains(.,'Gjendja e mjetit me targë:')]]//button[contains(.,'Shkarko dokumentin e vulosur')]")
        ));

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            downloadButton
        );

        Thread.Sleep(500);

        Log("Download button text: " + downloadButton.Text.Trim());
        Assert.That(downloadButton.Displayed, Is.True, "Butoni 'Shkarko dokumentin e vulosur' nuk eshte visible");
        Assert.That(downloadButton.Text.Trim(), Does.Contain("Shkarko dokumentin e vulosur"));

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