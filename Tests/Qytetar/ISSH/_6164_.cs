using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("6164")]
public class _6164_ : QytetarNidF602TestBase
{
    protected override string ServiceCode => "6164";
    protected override string? ServiceTitle => "PensioniInvaliditetitMCAP";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void PensioniInvaliditetitMCAP()
    {




        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E INVALIDITETIT"));

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert nje hap aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(1));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));

        Log("Wait qe te dhenat e tabeles te ngarkohen");
        WaitForDisabilityTable();

        Log("Assert fusha Kerko");
        IWebElement kerkoLabel = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//label[normalize-space()='Kërko']")));
        Assert.That(kerkoLabel.Displayed, Is.True);
        IWebElement searchInput = FindSearchInput();
        Assert.That(searchInput.GetAttribute("value"), Is.EqualTo(string.Empty));

        Log("Assert kolonat e tabeles");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'rdt_TableCol') and normalize-space()='Lloji']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'rdt_TableCol') and normalize-space()='Nga data']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'rdt_TableCol') and normalize-space()='Deri në datën']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'rdt_TableCol') and normalize-space()='Shuma']")).Displayed, Is.True);

        Log("Wait qe te dhenat e tabeles te ngarkohen");
        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("row-0")));

        var rows = driver.FindElements(By.CssSelector(".rdt_TableRow"));
        Assert.That(rows.Count, Is.EqualTo(8));

        Log("Assert rreshtat e te dhenave te invaliditetit");
        AssertTableRow(0,
            "Shtesë statusi i invalidit sipas VKM nr. 869, datë 18.6.2008",
            "01.01.2025",
            string.Empty,
            "3,616.00");
        AssertTableRow(1,
            "Shperblim 2025 sipas VKM Nr. 20 Dt. 09.01.2025 (10000)",
            "12.03.2025",
            "31.03.2025",
            "10,000.00");
        AssertTableRow(2,
            "Shperblim 2025 sipas VKM Nr. 711 Dt. 26.11.2025 (15000)",
            "12.12.2025",
            "29.12.2025",
            "15,000.00");
        AssertTableRow(3,
            "Shperblim 2026 sipas VKM Nr. 512 Dt. 30.06.2026",
            "20.07.2026",
            "31.07.2026",
            "5,000.00");
        AssertTableRow(4,
            "Shperblim 2024 sipas VKM Nr. 736 Dt. 27.11.2024 (15000)",
            "27.11.2024",
            "30.11.2024",
            "15,000.00");
        AssertTableRow(5,
            "Kompesim mujor deri ne masen 600 leke, VKM 828 dt 30.12.2025 (invalid)",
            "01.01.2026",
            string.Empty,
            "600.00");
        AssertTableRow(6,
            "Pension Invalidi reduktuar",
            "27.06.2024",
            string.Empty,
            "17,736.00");
        AssertTableRow(7,
            "Shtesa kreditore",
            "01.07.2024",
            "30.07.2024",
            "2,158.00");

        Log("Kerko me te dhena jo te sakta");
        FillSearch("test");

        Log("Assert no results");
        wait.Until(d => d.FindElements(By.CssSelector(".rdt_TableRow")).Count == 0);
        Assert.That(driver.FindElements(By.CssSelector(".rdt_TableRow")).Count, Is.EqualTo(0));

        Log("Pastro kerkimin");
        FillSearch(string.Empty);
        wait.Until(d => d.FindElements(By.CssSelector(".rdt_TableRow")).Count == 8);

        Log("Kerko Pension Invalidi reduktuar");
        FillSearch("Pension Invalidi reduktuar");
        wait.Until(d => d.FindElements(By.CssSelector(".rdt_TableRow")).Count == 1);
        AssertTableRow(0,
            "Pension Invalidi reduktuar",
            "27.06.2024",
            string.Empty,
            "17,736.00");

        Log("Pastro kerkimin perseri");
        FillSearch(string.Empty);
        wait.Until(d => d.FindElements(By.CssSelector(".rdt_TableRow")).Count == 8);

        Log("Assert butoni Dergo Mail");
        IWebElement dergoMailBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//button[normalize-space()='Dërgo Mail']")));
        Assert.That(dergoMailBtn.Displayed, Is.True);
        Assert.That(dergoMailBtn.Enabled, Is.True);

        Log("Assert butoni Shkarko");
        IWebElement shkarkoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//button[contains(.,'Shkarko')]")));
        Assert.That(shkarkoBtn.Text.Trim(), Does.Contain("Shkarko"));
        Assert.That(shkarkoBtn.Enabled, Is.True);

        Log("Assert butoni Kthehu");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));

        Log("Kliko butonin Shkarko");
        SafeClick(By.XPath("//button[contains(.,'Shkarko')]"));
        Thread.Sleep(2000);

        Log("TEST PASSED");
    }

    private void WaitForDisabilityTable()
    {

        var dataWait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
        dataWait.Until(d =>
        {
            try
            {
                var emptyMessages = d.FindElements(
                    By.XPath("//div[contains(text(),'Nuk gjëndet informacion')]"));
                bool stillEmpty = emptyMessages.Count > 0 && emptyMessages[0].Displayed;

                var searchLabels = d.FindElements(
                    By.XPath("//label[normalize-space()='Kërko']"));
                bool tableReady = searchLabels.Count > 0 && searchLabels[0].Displayed;

                return tableReady && !stillEmpty;
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
            By.XPath("//label[normalize-space()='Kërko']/following-sibling::input")));
    }

    private void FillSearch(string value)
    {

        IWebElement input = FindSearchInput();

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            input
        );

        input.Click();
        Thread.Sleep(200);
        input.SendKeys(Keys.Control + "a");
        Thread.Sleep(200);
        input.SendKeys(Keys.Delete);
        Thread.Sleep(300);

        if (!string.IsNullOrEmpty(value))
            input.SendKeys(value);

        Thread.Sleep(800);
    }

    private void AssertTableRow(int rowIndex, string lloji, string ngaData, string deriNeDaten, string shuma)
    {

        IWebElement row = wait.Until(ExpectedConditions.ElementIsVisible(By.Id($"row-{rowIndex}")));
        var cells = row.FindElements(By.CssSelector("[role='cell']"));

        Assert.That(cells.Count, Is.EqualTo(4), $"Row {rowIndex} should have 4 columns");
        Assert.That(cells[0].Text.Trim(), Is.EqualTo(lloji), $"Row {rowIndex} Lloji");
        Assert.That(cells[1].Text.Trim(), Is.EqualTo(ngaData), $"Row {rowIndex} Nga data");
        Assert.That(cells[2].Text.Trim(), Is.EqualTo(deriNeDaten), $"Row {rowIndex} Deri në datën");
        Assert.That(cells[3].Text.Trim(), Is.EqualTo(shuma), $"Row {rowIndex} Shuma");
    }
}