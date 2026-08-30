using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("6175")]
public class _6175_ : QytetarNidF602TestBase
{
    protected override string ServiceCode => "6175";
    protected override string? ServiceTitle => "MasatPaterhequraPensioniVdekjes";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void MasatPaterhequraPensioniVdekjes()
    {




        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("PUBLIKIMI I MASAVE"));

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
        WaitForPensionTable();

        Log("Assert fusha Kerko");
        IWebElement searchInput = FindSearchInput();
        Assert.That(searchInput.Displayed, Is.True);
        Assert.That(searchInput.GetAttribute("placeholder"), Is.EqualTo("Kërko"));
        Assert.That(searchInput.GetAttribute("value"), Is.EqualTo(string.Empty));

        Log("Assert kolonat e tabeles");
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='NID']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Periudha']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Statusi']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Shuma']")).Displayed, Is.True);

        Log("Assert rreshtat e masave");
        Assert.That(GetTableRowCount(), Is.EqualTo(5));
        AssertTableRow(0, CitizenNid, "Gusht 2026", "Pa Konfirmuar", "20633");
        AssertTableRow(1, CitizenNid, "Gusht 2026", "Pa Konfirmuar", "7276");
        AssertTableRow(2, CitizenNid, "Korrik 2026/2", "Pa Konfirmuar", "5000");
        AssertTableRow(3, CitizenNid, "Korrik 2026", "Pa Konfirmuar", "20633");
        AssertTableRow(4, CitizenNid, "Korrik 2026", "Pa Konfirmuar", "7276");

        Log("Assert paginimi");
        IWebElement pageNumber = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[contains(@style,'border-radius: 100px')]/div[normalize-space()='1']")));
        Assert.That(pageNumber.Displayed, Is.True);

        var paginationButtons = driver.FindElements(
            By.CssSelector("button.MuiButton-textPrimary"));
        Assert.That(paginationButtons.Count, Is.GreaterThanOrEqualTo(2));
        Assert.That(paginationButtons[0].GetAttribute("disabled"), Is.Not.Null);
        Assert.That(paginationButtons[paginationButtons.Count - 1].GetAttribute("disabled"), Is.Not.Null);

        Log("Kerko me te dhena jo te sakta");
        FillSearch("test");

        Log("Assert no results");
        IWebElement noResults = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//table//td[contains(.,'Nuk u gjend asnjë masë')]")));
        Assert.That(noResults.Text.Trim(), Is.EqualTo("Nuk u gjend asnjë masë"));

        Log("Pastro kerkimin");
        FillSearch(string.Empty);
        wait.Until(d => d.FindElements(By.CssSelector("table tbody tr")).Count == 5);

        Log("Kerko me Periudha Korrik 2026/2");
        FillSearch("Korrik 2026/2");
        wait.Until(d => d.FindElements(By.CssSelector("table tbody tr")).Count == 1);
        AssertTableRow(0, CitizenNid, "Korrik 2026/2", "Pa Konfirmuar", "5000");

        Log("Pastro kerkimin perseri");
        FillSearch(string.Empty);
        wait.Until(d => d.FindElements(By.CssSelector("table tbody tr")).Count == 5);

        Log("Assert nuk ka buton Vazhdo");
        Assert.That(driver.FindElements(By.CssSelector("button.ealb-btn-continue")).Count, Is.EqualTo(0));

        Log("Assert butoni Kthehu");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));

        Log("Kliko butonin Kthehu");
        SafeClick(By.CssSelector("button.ealb-btn-back"));
        Thread.Sleep(2000);

        Log("TEST PASSED");
    }

    private void WaitForPensionTable()
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

    private void AssertTableRow(int rowIndex,
        string nid,
        string periudha,
        string statusi,
        string shuma)
    {

        var rows = driver.FindElements(By.CssSelector("table tbody tr"));
        Assert.That(rows.Count, Is.GreaterThan(rowIndex), $"Tabela duhet te kete rreshtin {rowIndex}");

        var cells = rows[rowIndex].FindElements(By.TagName("td"));
        Assert.That(cells.Count, Is.EqualTo(4), $"Row {rowIndex} should have 4 columns");
        Assert.That(cells[0].Text.Trim(), Is.EqualTo(nid), $"Row {rowIndex} NID");
        Assert.That(cells[1].Text.Trim(), Is.EqualTo(periudha), $"Row {rowIndex} Periudha");
        Assert.That(cells[2].Text.Trim(), Is.EqualTo(statusi), $"Row {rowIndex} Statusi");
        Assert.That(cells[3].Text.Trim(), Is.EqualTo(shuma), $"Row {rowIndex} Shuma");
    }
}