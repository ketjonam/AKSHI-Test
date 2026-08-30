using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("6180")]
public class _6180_ : QytetarNidF602TestBase
{
    protected override string ServiceCode => "6180";
    protected override string? ServiceTitle => "MasatPaterhequraPilot";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void MasatPaterhequraPilot()
    {




        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("MASAT E PATËRHEQURA"));

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
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Skedë']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Periudha']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Nr Dosje']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Shuma']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Statusi']")).Displayed, Is.True);

        Log("Assert rreshtat e pensioneve");
        Assert.That(GetTableRowCount(), Is.EqualTo(5));
        AssertTableRow(0, CitizenNid, "118924588", "Gusht 2026", "P110204493U", "20633", "Pa Konfirmuar");
        AssertTableRow(1, CitizenNid, "118923720", "Gusht 2026", "S110032286U", "7276", "Pa Konfirmuar");
        AssertTableRow(2, CitizenNid, "117782997", "Korrik 2026/2", "P110204493U", "5000", "Pa Konfirmuar");
        AssertTableRow(3, CitizenNid, "117152702", "Korrik 2026", "P110204493U", "20633", "Pa Konfirmuar");
        AssertTableRow(4, CitizenNid, "117150136", "Korrik 2026", "S110032286U", "7276", "Pa Konfirmuar");

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
            By.XPath("//table//td[contains(.,'Asnjë pagesë pensioni nuk u gjet')]")));
        Assert.That(noResults.Text.Trim(), Is.EqualTo("Asnjë pagesë pensioni nuk u gjet."));

        Log("Pastro kerkimin");
        FillSearch(string.Empty);
        wait.Until(d => d.FindElements(By.CssSelector("table tbody tr")).Count == 5);

        Log("Kerko me Skede 118924588");
        FillSearch("118924588");
        wait.Until(d => d.FindElements(By.CssSelector("table tbody tr")).Count == 1);
        AssertTableRow(0, CitizenNid, "118924588", "Gusht 2026", "P110204493U", "20633", "Pa Konfirmuar");

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
        string skede,
        string periudha,
        string nrDosje,
        string shuma,
        string statusi)
    {

        var rows = driver.FindElements(By.CssSelector("table tbody tr"));
        Assert.That(rows.Count, Is.GreaterThan(rowIndex), $"Tabela duhet te kete rreshtin {rowIndex}");

        var cells = rows[rowIndex].FindElements(By.TagName("td"));
        Assert.That(cells.Count, Is.EqualTo(6), $"Row {rowIndex} should have 6 columns");
        Assert.That(cells[0].Text.Trim(), Is.EqualTo(nid), $"Row {rowIndex} NID");
        Assert.That(cells[1].Text.Trim(), Is.EqualTo(skede), $"Row {rowIndex} Skede");
        Assert.That(cells[2].Text.Trim(), Is.EqualTo(periudha), $"Row {rowIndex} Periudha");
        Assert.That(cells[3].Text.Trim(), Is.EqualTo(nrDosje), $"Row {rowIndex} Nr Dosje");
        Assert.That(cells[4].Text.Trim(), Is.EqualTo(shuma), $"Row {rowIndex} Shuma");
        Assert.That(cells[5].Text.Trim(), Is.EqualTo(statusi), $"Row {rowIndex} Statusi");
    }
}