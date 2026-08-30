using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("368")]
public class _368_ : QytetarNidF602TestBase
{
    protected override string ServiceCode => "368";
    protected override string? ServiceTitle => "PensionetePaterhequra";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void PensionetePaterhequra()
    {




        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("PENSIONET"));

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
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Emri']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Qyteti']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Nr. Fature']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Lloji Pensionit']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Shuma']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Data për tërheqje']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Statusi']")).Displayed, Is.True);

        Log("Assert rreshtat e pensioneve te paterhequra");
        Assert.That(GetTableRowCount(), Is.EqualTo(5));
        AssertTableRow(0, "MERSIN MUSTAFA MEMA", "KAVAJË", "118924588", "Pleqeri e plote", "20633", "01/08/2026", "Konfirmuar");
        AssertTableRow(1, "MERSIN MUSTAFA MEMA", "KAVAJË", "118923720", "Ligji 10142, date 15.05.2009, i ndryshuar (Ushtaraku ligji i ri)", "7276", "01/08/2026", "Konfirmuar");
        AssertTableRow(2, "MERSIN MUSTAFA MEMA", "KAVAJË", "117782997", "Pleqeri e plote", "20633", "02/07/2026", "Konfirmuar");
        AssertTableRow(3, "MERSIN MUSTAFA MEMA", "KAVAJË", "117152702", "Pleqeri e plote", "20633", "01/07/2026", "Konfirmuar");
        AssertTableRow(4, "MERSIN MUSTAFA MEMA", "KAVAJË", "117150136", "Ligji 10142, date 15.05.2009, i ndryshuar (Ushtaraku ligji i ri)", "7276", "01/07/2026", "Konfirmuar");

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

        Log("Kerko me Nr. Fature 118924588");
        FillSearch("118924588");
        wait.Until(d => d.FindElements(By.CssSelector("table tbody tr")).Count == 1);
        AssertTableRow(0, "MERSIN MUSTAFA MEMA", "KAVAJË", "118924588", "Pleqeri e plote", "20633", "01/08/2026", "Konfirmuar");

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
        string emri,
        string qyteti,
        string nrFature,
        string llojiPensionit,
        string shuma,
        string dataPerTerheqje,
        string statusi)
    {

        var rows = driver.FindElements(By.CssSelector("table tbody tr"));
        Assert.That(rows.Count, Is.GreaterThan(rowIndex), $"Tabela duhet te kete rreshtin {rowIndex}");

        var cells = rows[rowIndex].FindElements(By.TagName("td"));
        Assert.That(cells.Count, Is.EqualTo(7), $"Row {rowIndex} should have 7 columns");
        Assert.That(cells[0].Text.Trim(), Is.EqualTo(emri), $"Row {rowIndex} Emri");
        Assert.That(cells[1].Text.Trim(), Is.EqualTo(qyteti), $"Row {rowIndex} Qyteti");
        Assert.That(cells[2].Text.Trim(), Is.EqualTo(nrFature), $"Row {rowIndex} Nr. Fature");
        Assert.That(cells[3].Text.Trim(), Is.EqualTo(llojiPensionit), $"Row {rowIndex} Lloji Pensionit");
        Assert.That(cells[4].Text.Trim(), Is.EqualTo(shuma), $"Row {rowIndex} Shuma");
        Assert.That(cells[5].Text.Trim(), Is.EqualTo(dataPerTerheqje), $"Row {rowIndex} Data për tërheqje");
        Assert.That(cells[6].Text.Trim(), Is.EqualTo(statusi), $"Row {rowIndex} Statusi");
    }
}