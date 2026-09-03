using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("6169")]
public class _6169_ : QytetarNidF602TestBase
{
    protected override string ServiceCode => "6169";
    protected override string? ServiceTitle => "PensionFamiljar";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    [Test]
    public void PensionFamiljar()
    {
        Log("Assert page header");
        IWebElement headerContainer = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("div.page-header-container")));
        Assert.That(headerContainer.Displayed, Is.True, "Page header nuk eshte visible");

        IWebElement serviceName = wait.Until(ExpectedConditions.ElementIsVisible(
            By.Id("serviceNameBreadcrumb")));
        Assert.That(serviceName.Displayed, Is.True, "Breadcrumb i sherbimit nuk eshte visible");
        Assert.That(serviceName.Text.Trim(), Is.EqualTo("Pensioni familjar"),
            "Emri i sherbimit nuk eshte i sakte");

        Log("Scroll deri sa butoni Perdor te jete i dukshem");
        By perdorLocator = By.CssSelector("button.use-service-button");
        IWebElement perdorBtn = wait.Until(ExpectedConditions.ElementExists(perdorLocator));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center', inline:'nearest'});",
            perdorBtn);
        Thread.Sleep(500);
        perdorBtn = wait.Until(ExpectedConditions.ElementToBeClickable(perdorLocator));
        Assert.That(perdorBtn.Displayed, Is.True, "Butoni Perdor nuk eshte visible per tu klikuar");

        Log("Kliko butonin Perdor");
        SafeClick(perdorLocator);

        Log("Kliko Aplikim i ri");
        By aplikimIRiLocator = By.XPath(
            "//div[contains(@class,'mbx-content') and @role='button'][.//h6[contains(@class,'mbx-title') and normalize-space()='Aplikim i ri']]");
        IWebElement aplikimIRi = wait.Until(ExpectedConditions.ElementIsVisible(aplikimIRiLocator));
        Assert.That(aplikimIRi.Displayed, Is.True, "Karta Aplikim i ri nuk eshte visible");
        IWebElement aplikimIRiTitle = aplikimIRi.FindElement(By.CssSelector("h6.mbx-title"));
        Assert.That(aplikimIRiTitle.Text.Trim(), Is.EqualTo("Aplikim i ri"),
            "Titulli i kartes nuk eshte Aplikim i ri");
        SafeClick(aplikimIRiLocator);
        Thread.Sleep(1500);

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert nje hap aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(1));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));

        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h5.text-uppercase")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("PUBLIKIMI I TË DHËNAVE"));

        Log("Wait qe te dhenat e tabeles te ngarkohen");
        WaitForPensionTable();

        Log("Assert fusha Kerko");
        IWebElement searchInput = FindSearchInput();
        Assert.That(searchInput.Displayed, Is.True);
        Assert.That(searchInput.GetAttribute("placeholder"), Is.EqualTo("Kërko"));
        Assert.That(searchInput.GetAttribute("value"), Is.EqualTo(string.Empty));

        Log("Assert kolonat e tabeles");
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Lloji']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Nga data']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Deri në datën']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Shuma']")).Displayed, Is.True);

        Log("Assert rreshtat e pensionit familjar");
        Assert.That(GetTableRowCount(), Is.EqualTo(10));
        AssertTableRow(0,
            "Shperblim 2025 sipas VKM Nr. 20 Dt. 09.01.2025 (5000)",
            "12.03.2025",
            "31.03.2025",
            "5,000.00");
        AssertTableRow(1,
            "Shperblim 2025 sipas VKM Nr. 711 Dt. 26.11.2025 (10000)",
            "12.12.2025",
            "29.12.2025",
            "10,000.00");
        AssertTableRow(2,
            "Shperblim 2026 sipas VKM Nr. 512 Dt. 30.06.2026",
            "20.07.2026",
            "31.07.2026",
            "5,000.00");
        AssertTableRow(3,
            "Shperblim 2023 sipas VKM Nr. 654 Dt. 30.11.2023",
            "19.12.2023",
            "21.11.2023",
            "5,000.00");
        AssertTableRow(4,
            "Shperblim 2024 sipas VKM Nr. 736 Dt. 27.11.2024 (10000)",
            "27.11.2024",
            "30.11.2024",
            "10,000.00");
        AssertTableRow(5,
            "Pension pleqerie",
            "01.03.2022",
            string.Empty,
            "52,985.00");
        AssertTableRow(6,
            "Neni 33 Stok",
            "01.03.2022",
            string.Empty,
            "2,384.00");
        AssertTableRow(7,
            "Shtesa kreditore",
            "01.03.2022",
            "31.03.2022",
            "25,495.00");
        AssertTableRow(8,
            "Shperblim 2022 sipas VKM Nr. 752 Dt. 01.12.2022",
            "30.12.2022",
            "30.12.2022",
            "5,000.00");
        AssertTableRow(9,
            "Shperblim i dyte 2022 sipas VKM Nr. 897 Dt. 29.12.2022",
            "29.12.2022",
            "30.12.2022",
            "8,000.00");

        Log("Assert paginimi");
        IWebElement pageNumber = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[contains(@style,'border-radius: 100px')]/div[normalize-space()='1']")));
        Assert.That(pageNumber.Displayed, Is.True);

        var paginationButtons = driver.FindElements(
            By.XPath("//div[contains(@style,'border-radius: 100px')]/ancestor::div[contains(@style,'flex-direction: row')][1]//button"));
        Assert.That(paginationButtons.Count, Is.EqualTo(2));
        Assert.That(paginationButtons[0].GetAttribute("disabled"), Is.Not.Null);
        Assert.That(paginationButtons[1].GetAttribute("disabled"), Is.Null);

        Log("Kerko me te dhena jo te sakta");
        FillSearch("test");

        Log("Assert no results");
        IWebElement noResults = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//table//td[contains(.,'Nuk u gjend asnjë masë')]")));
        Assert.That(noResults.Text.Trim(), Is.EqualTo("Nuk u gjend asnjë masë"));

        Log("Pastro kerkimin");
        FillSearch(string.Empty);
        wait.Until(d => d.FindElements(By.CssSelector("table tbody tr")).Count == 10);

        Log("Kerko me Lloji Shtesa kreditore");
        FillSearch("Shtesa kreditore");
        wait.Until(d => d.FindElements(By.CssSelector("table tbody tr")).Count == 1);
        AssertTableRow(0,
            "Shtesa kreditore",
            "01.03.2022",
            "31.03.2022",
            "25,495.00");

        Log("Pastro kerkimin perseri");
        FillSearch(string.Empty);
        wait.Until(d => d.FindElements(By.CssSelector("table tbody tr")).Count == 10);

        Log("Assert nuk ka buton Vazhdo");
        Assert.That(driver.FindElements(By.CssSelector("button.ealb-btn-continue")).Count, Is.EqualTo(0));

        Log("Assert butoni Shkarko");
        IWebElement shkarkoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//button[contains(.,'Shkarko') or contains(.,'SHKARKO')]")));
        Assert.That(shkarkoBtn.Text.Trim(), Does.Contain("Shkarko").IgnoreCase);
        Assert.That(shkarkoBtn.Enabled, Is.True);

        Log("Assert butoni Kthehu");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        Assert.That(backBtn.Displayed, Is.True, "Butoni Kthehu nuk eshte visible");
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));

        Log("Kliko butonin Shkarko");
        SafeClick(By.XPath("//button[contains(.,'Shkarko') or contains(.,'SHKARKO')]"));
        Thread.Sleep(2000);

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

    private string NormalizeText(string text)
    {
        return string.Join(" ", (text ?? string.Empty)
            .Replace('\u00a0', ' ')
            .Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries));
    }

    private void AssertTableRow(int rowIndex,
        string lloji,
        string ngaData,
        string deriNeDaten,
        string shuma)
    {
        var rows = driver.FindElements(By.CssSelector("table tbody tr"));
        Assert.That(rows.Count, Is.GreaterThan(rowIndex), $"Tabela duhet te kete rreshtin {rowIndex}");

        var cells = rows[rowIndex].FindElements(By.TagName("td"));
        Assert.That(cells.Count, Is.EqualTo(4), $"Row {rowIndex} should have 4 columns");
        Assert.That(NormalizeText(cells[0].Text), Is.EqualTo(lloji), $"Row {rowIndex} Lloji");
        Assert.That(NormalizeText(cells[1].Text), Is.EqualTo(ngaData), $"Row {rowIndex} Nga data");
        Assert.That(NormalizeText(cells[2].Text), Is.EqualTo(deriNeDaten), $"Row {rowIndex} Deri në datën");
        Assert.That(NormalizeText(cells[3].Text), Is.EqualTo(shuma), $"Row {rowIndex} Shuma");
    }
}
