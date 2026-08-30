using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("5034")]
public class _5034_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "5034";
    protected override string? ServiceTitle => "KontributetSigurimeveShoqerore";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void KontributetSigurimeveShoqerore()
    {




        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.ealb-header-text")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("LISTA E KONTRIBUTEVE"));

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("1 minut kohëzgjatje"));

        Log("Assert nje hap aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(1));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));

        Log("Wait qe te dhenat e tabeles te ngarkohen");
        WaitForContributionsTable();

        Log("Assert fusha Kerko");
        IWebElement searchInput = FindSearchInput();
        Assert.That(searchInput.Displayed, Is.True);
        Assert.That(searchInput.GetAttribute("placeholder"), Is.EqualTo("Kërko"));
        Assert.That(searchInput.GetAttribute("value"), Is.EqualTo(string.Empty));

        Log("Assert kolonat e tabeles");
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Emri']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Mbiemri']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Paga Bruto']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Paga Neto']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Muaji']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Viti']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Skema']")).Displayed, Is.True);

        Log("Assert rreshtat e kontributeve");
        Assert.That(GetTableRowCount(), Is.EqualTo(10));
        AssertTableContains(
            "KETJONA|MEMA|230824|0|7|2026|Urban",
            "KETJONA|MEMA|185064|0|7|2026|Urban",
            "KETJONA|MEMA|185064|0|6|2026|Urban",
            "KETJONA|MEMA|230823|0|6|2026|Urban",
            "KETJONA|MEMA|209849|0|5|2026|Urban",
            "KETJONA|MEMA|230823|0|5|2026|Urban",
            "KETJONA|MEMA|185064|0|4|2026|Urban",
            "KETJONA|MEMA|230823|0|4|2026|Urban",
            "KETJONA|MEMA|230823|0|3|2026|Urban",
            "KETJONA|MEMA|185064|0|3|2026|Urban"
        );

        Log("Assert paginimi");
        IWebElement pageNumber = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[contains(@style,'border-radius: 100px')]/div[normalize-space()='1']")));
        Assert.That(pageNumber.Displayed, Is.True);

        var paginationButtons = driver.FindElements(
            By.CssSelector("button.MuiButton-textPrimary"));
        Assert.That(paginationButtons.Count, Is.GreaterThanOrEqualTo(2));
        Assert.That(paginationButtons[0].GetAttribute("disabled"), Is.Not.Null);
        Assert.That(paginationButtons[paginationButtons.Count - 1].Enabled, Is.True);

        Log("Kerko me te dhena jo te sakta");
        FillSearch("test");

        Log("Assert no results");
        IWebElement noResults = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//table//td[contains(.,'Nuk u gjetën të dhëna')]")));
        Assert.That(noResults.Text.Trim(), Is.EqualTo("Nuk u gjetën të dhëna"));

        Log("Pastro kerkimin");
        FillSearch(string.Empty);
        wait.Until(d => d.FindElements(By.CssSelector("table tbody tr")).Count == 10);

        Log("Kerko me Paga Bruto 230824");
        FillSearch("230824");
        wait.Until(d => d.FindElements(By.CssSelector("table tbody tr")).Count == 1);
        AssertTableRow(0, "KETJONA", "MEMA", "230824", "0", "7", "2026", "Urban");

        Log("Pastro kerkimin perseri");
        FillSearch(string.Empty);
        wait.Until(d => d.FindElements(By.CssSelector("table tbody tr")).Count == 10);

        Log("Assert butoni Printo");
        IWebElement printoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//button[contains(.,'Printo')]")));
        Assert.That(printoBtn.Displayed, Is.True);
        Assert.That(printoBtn.Enabled, Is.True);

        Log("Assert butoni Shkarko PDF");
        IWebElement shkarkoPdfBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//button[contains(.,'Shkarko PDF')]")));
        Assert.That(shkarkoPdfBtn.Text.Trim(), Does.Contain("Shkarko PDF"));
        Assert.That(shkarkoPdfBtn.Enabled, Is.True);

        Log("Assert nuk ka buton Vazhdo");
        Assert.That(driver.FindElements(By.CssSelector("button.ealb-btn-continue")).Count, Is.EqualTo(0));

        Log("Assert butoni Kthehu");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));

        Log("Kliko butonin Shkarko PDF");
        SafeClick(By.XPath("//button[contains(.,'Shkarko PDF')]"));
        Thread.Sleep(2000);

        Log("TEST PASSED");
    }

    private void WaitForContributionsTable()
    {

        var dataWait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
        dataWait.Until(d =>
        {
            try
            {
                var rows = d.FindElements(By.CssSelector("table tbody tr"));
                return rows.Count > 0 && rows[0].Displayed;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });
    }

    private IWebElement FindSearchInput()
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("input[placeholder='Kërko']")));
    }

    private void FillSearch(string value)
    {

        IWebElement input = FindSearchInput();

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            input
        );
        Thread.Sleep(200);

        ((IJavaScriptExecutor)driver).ExecuteScript(@"
            const el = arguments[0];
            const setter = Object.getOwnPropertyDescriptor(
                window.HTMLInputElement.prototype, 'value').set;
            el.focus();
            setter.call(el, arguments[1]);
            el.dispatchEvent(new Event('input', { bubbles: true }));
            el.dispatchEvent(new Event('change', { bubbles: true }));
            el.blur();
        ", input, value ?? string.Empty);

        Thread.Sleep(800);
    }

    private int GetTableRowCount()
    {

        return driver.FindElements(By.CssSelector("table tbody tr")).Count;
    }

    private string[] GetTableRowValues()
    {

        return driver.FindElements(By.CssSelector("table tbody tr"))
            .Select(row => string.Join("|",
                row.FindElements(By.TagName("td")).Select(c => c.Text.Trim())))
            .ToArray();
    }

    private void AssertTableContains(params string[] expectedRows)
    {

        Assert.That(GetTableRowValues(), Is.EquivalentTo(expectedRows));
    }

    private void AssertTableRow(int rowIndex,
        string emri,
        string mbiemri,
        string pagaBruto,
        string pagaNeto,
        string muaji,
        string viti,
        string skema)
    {

        var rows = driver.FindElements(By.CssSelector("table tbody tr"));
        Assert.That(rows.Count, Is.GreaterThan(rowIndex), $"Tabela duhet te kete rreshtin {rowIndex}");

        var cells = rows[rowIndex].FindElements(By.TagName("td"));
        Assert.That(cells.Count, Is.EqualTo(7), $"Row {rowIndex} should have 7 columns");
        Assert.That(cells[0].Text.Trim(), Is.EqualTo(emri), $"Row {rowIndex} Emri");
        Assert.That(cells[1].Text.Trim(), Is.EqualTo(mbiemri), $"Row {rowIndex} Mbiemri");
        Assert.That(cells[2].Text.Trim(), Is.EqualTo(pagaBruto), $"Row {rowIndex} Paga Bruto");
        Assert.That(cells[3].Text.Trim(), Is.EqualTo(pagaNeto), $"Row {rowIndex} Paga Neto");
        Assert.That(cells[4].Text.Trim(), Is.EqualTo(muaji), $"Row {rowIndex} Muaji");
        Assert.That(cells[5].Text.Trim(), Is.EqualTo(viti), $"Row {rowIndex} Viti");
        Assert.That(cells[6].Text.Trim(), Is.EqualTo(skema), $"Row {rowIndex} Skema");
    }
}