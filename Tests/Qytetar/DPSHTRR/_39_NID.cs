using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.DPSHTRR;

[Category("DPSHTRR")]
[Category("39")]
public class _39_NID : QytetarNidJ257TestBase
{
    protected override string ServiceCode => "39";
    protected override string? ServiceTitle => "AutomjeteteMia";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void AutomjeteteMia()
    {


string titleXpath = "/html/body/div/main/div[3]/div/div/div/div/div[2]/div/div[1]/h5";
        string filterInputXpath = "/html/body/div/main/div[3]/div/div/div/div/div[2]/div/div[2]/div[2]/div/input";

        

        Log("Assert Title");
        IWebElement titleElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(titleXpath)));
        Log("Title text: " + titleElement.Text.Trim());
        Assert.That(titleElement.Displayed, Is.True, "Titulli nuk eshte visible");

        Log("Filter table");
        IWebElement filterInput = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(filterInputXpath)));
        filterInput.SendKeys("test");

        Log("Wait for no-data row");
        IWebElement noDataRow = wait.Until(d =>
        {
            try
            {
                var row = d.FindElement(By.XPath("//table/tbody/tr"));
                return row.Text.Trim().Contains("Nuk dispononi automjete!") ? row : null;
            }
            catch
            {
                return null;
            }
        });

        Log("No-data row text: " + noDataRow.Text.Trim());
        Assert.That(noDataRow.Text.Trim(), Does.Contain("Nuk dispononi automjete!"),
            "Mesazhi 'Nuk dispononi automjete!' nuk u shfaq");

        Log("Clear filter");
        ClearFilterInput(By.XPath(filterInputXpath));

        Log("Wait for real data row to appear");
        IWebElement dataRow = wait.Until(d =>
        {
            try
            {
                var row = d.FindElement(By.XPath("//table/tbody/tr"));
                string rowText = row.Text.Trim();
                Log("Current tbody/tr text: " + rowText);

                if (string.IsNullOrWhiteSpace(rowText))
                    return null;

                if (rowText.Contains("Nuk dispononi automjete!"))
                    return null;

                var cells = row.FindElements(By.XPath("./td"));
                if (cells.Count < 15)
                    return null;

                string nr = cells[0].Text.Trim();
                string targa = cells[1].Text.Trim();

                Log($"Detected row -> Nr: '{nr}', Targa: '{targa}'");

                return (nr == "1" && targa == "AB166DP") ? row : null;
            }
            catch (StaleElementReferenceException)
            {
                return null;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        });

        Assert.That(dataRow, Is.Not.Null, "Data row nuk u ngarkua pas pastrimit te filtrit");

        Log("Gjej header row (tr)");
        IWebElement headerRow = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table/thead/tr")));
        Assert.That(headerRow.Displayed, Is.True, "Header row nuk është visible");

        Log("Merr te gjitha kolonat (th) nga header row");
        var headerCells = headerRow.FindElements(By.XPath("./th"));

        string[] expectedHeaders = new string[]
        {
            "Nr.",
            "Targa",
            "Shasia",
            "Lloji",
            "Marka",
            "Modeli",
            "Tipi",
            "Ngjyra",
            "Lënda Djegëse",
            "Viti",
            "Gomat",
            "Dyer",
            "Vende",
            "Nr. Serial i deklaratës së shitjes",
            "Nr. kontratës deklaratës së shitjes"
        };

        Log($"Numri i header cells: {headerCells.Count}");
        Assert.That(headerCells.Count, Is.EqualTo(expectedHeaders.Length),
            $"Numri i kolonave nuk përputhet. Actual: {headerCells.Count}, Expected: {expectedHeaders.Length}");

        for (int i = 0; i < expectedHeaders.Length; i++)
        {
            string actual = headerCells[i].Text.Trim();
            string expected = expectedHeaders[i];

            Log($"Header[{i}] -> Actual: '{actual}' | Expected: '{expected}'");
            Assert.That(actual, Is.EqualTo(expected), $"Header mismatch në kolonën {i}");
        }

        Log("Gjej data row (tr)");
        dataRow = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table/tbody/tr")));
        Assert.That(dataRow.Displayed, Is.True, "Data row nuk është visible");

        Log("Merr te gjitha kolonat (td) nga data row");
        var cells = dataRow.FindElements(By.XPath("./td"));

        string[] expectedValues = new string[]
        {
            "1",
            "AB166DP",
            "NMTKN56E90R035156",
            "Autoveturë",
            "TOYOTA",
            "AURIS",
            "E90",
            "Gri E Errët",
            "Naftë",
            "2009",
            "205/55R16",
            "5",
            "5",
            "-",
            "-"
        };

        Log($"Numri i kolonave ne rresht: {cells.Count}");
        Assert.That(cells.Count, Is.EqualTo(expectedValues.Length),
            $"Numri i kolonave nuk përputhet. Actual: {cells.Count}, Expected: {expectedValues.Length}");

        for (int i = 0; i < expectedValues.Length; i++)
        {
            string actual = cells[i].Text.Trim();
            string expected = expectedValues[i];

            Log($"Cell[{i}] -> Actual: '{actual}' | Expected: '{expected}'");
            Assert.That(actual, Is.EqualTo(expected), $"Cell mismatch në kolonën {i}");
        }

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