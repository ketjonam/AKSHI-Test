using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("6166")]
public class _6166_ : QytetarNidF602TestBase
{
    protected override string ServiceCode => "6166";
    protected override string? ServiceTitle => "PensionPleqerie";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    [Test]
    public void PensionPleqerie()
    {
        Log("Assert page header");
        IWebElement headerContainer = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("div.page-header-container")));
        Assert.That(headerContainer.Displayed, Is.True, "Page header nuk eshte visible");

        IWebElement serviceName = wait.Until(ExpectedConditions.ElementIsVisible(
            By.Id("serviceNameBreadcrumb")));
        Assert.That(serviceName.Displayed, Is.True, "Breadcrumb i sherbimit nuk eshte visible");
        Assert.That(serviceName.Text.Trim(), Is.EqualTo("Pensioni i pleqërisë"),
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
            By.CssSelector("h4.px-4.pb-4.text-uppercase")));
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
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'rdt_TableCol') and normalize-space()='Emri']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'rdt_TableCol') and normalize-space()='Atësia']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'rdt_TableCol') and normalize-space()='Mbiemri']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'rdt_TableCol') and normalize-space()='Nr. Dosje']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'rdt_TableCol') and normalize-space()='Agjencia']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'rdt_TableCol') and normalize-space()='Njësia']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'rdt_TableCol') and normalize-space()='Lloji']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'rdt_TableCol') and normalize-space()='Shuma']")).Displayed, Is.True);

        Log("Wait qe te dhenat e tabeles te ngarkohen");
        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("row-0")));

        var rows = driver.FindElements(By.CssSelector(".rdt_TableRow"));
        Assert.That(rows.Count, Is.EqualTo(1));

        Log("Assert rreshtin e pensionit te pleqerise");
        AssertTableRow(0,
            "KADRI",
            "DELI",
            "KUKAJ",
            "132655",
            "Shkoder",
            "FI Bank (SH)",
            "Pleqerie Urban",
            "55 369,00");

        Log("Assert paginimi");
        IWebElement pageNumber = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[contains(@style,'border-radius: 100px')]/div[normalize-space()='1']")));
        Assert.That(pageNumber.Displayed, Is.True);

        var paginationButtons = driver.FindElements(
            By.XPath("//div[contains(@style,'border-radius: 100px')]/ancestor::div[contains(@style,'flex-direction: row')][1]//button"));
        Assert.That(paginationButtons.Count, Is.EqualTo(2));
        Assert.That(paginationButtons[0].GetAttribute("disabled"), Is.Not.Null);
        Assert.That(paginationButtons[1].GetAttribute("disabled"), Is.Not.Null);

        Log("Kerko me te dhena jo te sakta");
        FillSearch("test");

        Log("Assert no results");
        wait.Until(d => d.FindElements(By.XPath("//div[@id='row-0']//div[normalize-space()='KADRI']")).Count == 0);
        Assert.That(driver.FindElements(By.XPath("//div[@id='row-0']//div[normalize-space()='KADRI']")).Count, Is.EqualTo(0));

        Log("Pastro kerkimin");
        FillSearch(string.Empty);
        wait.Until(d => d.FindElements(By.CssSelector(".rdt_TableRow")).Count == 1);

        Log("Kerko me Nr. Dosje 132655");
        FillSearch("132655");
        wait.Until(d => d.FindElements(By.CssSelector(".rdt_TableRow")).Count == 1);
        AssertTableRow(0,
            "KADRI",
            "DELI",
            "KUKAJ",
            "132655",
            "Shkoder",
            "FI Bank (SH)",
            "Pleqerie Urban",
            "55 369,00");

        Log("Pastro kerkimin perseri");
        FillSearch(string.Empty);
        wait.Until(d => d.FindElements(By.CssSelector(".rdt_TableRow")).Count == 1);

        Log("Assert nuk ka buton Vazhdo");
        Assert.That(driver.FindElements(By.CssSelector("button.ealb-btn-continue")).Count, Is.EqualTo(0));

        Log("Assert butoni Kthehu");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        Assert.That(backBtn.Displayed, Is.True, "Butoni Kthehu nuk eshte visible");
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
                var rows = d.FindElements(By.CssSelector(".rdt_TableRow"));
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

    private string NormalizeText(string text)
    {
        return string.Join(" ", (text ?? string.Empty)
            .Replace('\u00a0', ' ')
            .Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries));
    }

    private void AssertTableRow(int rowIndex,
        string emri,
        string atesia,
        string mbiemri,
        string nrDosje,
        string agjencia,
        string njesia,
        string lloji,
        string shuma)
    {
        IWebElement row = wait.Until(ExpectedConditions.ElementIsVisible(By.Id($"row-{rowIndex}")));
        var cells = row.FindElements(By.CssSelector("[role='cell']"));

        Assert.That(cells.Count, Is.EqualTo(8), $"Row {rowIndex} should have 8 columns");
        Assert.That(NormalizeText(cells[0].Text), Is.EqualTo(emri), $"Row {rowIndex} Emri");
        Assert.That(NormalizeText(cells[1].Text), Is.EqualTo(atesia), $"Row {rowIndex} Atësia");
        Assert.That(NormalizeText(cells[2].Text), Is.EqualTo(mbiemri), $"Row {rowIndex} Mbiemri");
        Assert.That(NormalizeText(cells[3].Text), Is.EqualTo(nrDosje), $"Row {rowIndex} Nr. Dosje");
        Assert.That(NormalizeText(cells[4].Text), Is.EqualTo(agjencia), $"Row {rowIndex} Agjencia");
        Assert.That(NormalizeText(cells[5].Text), Is.EqualTo(njesia), $"Row {rowIndex} Njësia");
        Assert.That(NormalizeText(cells[6].Text), Is.EqualTo(lloji), $"Row {rowIndex} Lloji");
        Assert.That(NormalizeText(cells[7].Text), Is.EqualTo(shuma), $"Row {rowIndex} Shuma");
    }
}
