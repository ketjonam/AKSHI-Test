using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.DPSHTRR;

[Category("DPSHTRR")]
[Category("772")]
public class _772_NID : QytetarNidJ257TestBase
{
    protected override string ServiceCode => "772";
    protected override string? ServiceTitle => "TaksaVjetore";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Pagesa e taksës vjetore të mjeteve të përdoruara/ mjeteve të luksit";
    private const string ExpectedTitle = "Lista e automjeteve në zotërim";
    private const string ExpectedTarga = "AB838HR";

    private static readonly string[] ExpectedVehicleHeaders =
    {
        "Veprime",
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
        "Nr. kontratës së deklaratës së shitjes"
    };

    private static readonly string[] ExpectedVehicleValues =
    {
        "Zgjidh",
        ExpectedTarga,
        "WDD2040071A106224",
        "Autoveturë",
        "DAIMLER CHRYSLER",
        "C 200 CDI",
        "204007",
        "Zezë",
        "Naftë",
        "2007",
        "225/45R17",
        "4",
        "5",
        "",
        ""
    };

    private static readonly string[] ExpectedInvoiceHeaders =
    {
        "Nr fature",
        "Datë Fature",
        "Targa",
        "Pronësia",
        "Viti",
        "TVMP",
        "Gjoba TVMP",
        "TVML",
        "Gjoba TVML",
        "Totali",
        "Seria e Pullës",
        "Pagesë e konfirmuar"
    };

    private static readonly string[] ExpectedInvoiceValues =
    {
        "2500735705",
        "07.11.2025",
        ExpectedTarga,
        "Migen Luan Dërstila",
        "2025",
        "15,036",
        "0",
        "0",
        "0",
        "15,036",
        "AO571612",
        "PO"
    };

    private static readonly string[] ExpectedTaxHeaders =
    {
        "#",
        "Taksë Karburanti",
        "Koeficienti",
        "Vlera",
        "Gjoba",
        "Totali",
        "Veprime"
    };

    private static readonly string[] ExpectedTaxValues =
    {
        "1",
        "TVMP",
        "0.6",
        "16,110",
        "-",
        "16,110"
    };

    [Test]
    public void TaksaVjetore()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("1 minut kohëzgjatje"));

        Log("Assert nje hap aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(1));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));

        Log("Assert Title");
        IWebElement titleElement = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//div[contains(@class,'text-uppercase')]//h5[contains(.,'{ExpectedTitle}')]")));
        Assert.That(titleElement.Displayed, Is.True, "Titulli nuk eshte visible");
        Assert.That(titleElement.Text.Trim(), Is.EqualTo(ExpectedTitle), "Titulli nuk eshte i sakte");

        Log("Wait qe te dhenat e tabeles te ngarkohen");
        IWebElement vehicleTable = WaitForTableByHeader("Targa");
        AssertTableHeaders(vehicleTable, ExpectedVehicleHeaders);

        Log("Assert rreshtin e automjetit");
        IWebElement vehicleRow = WaitForDataRow("Targa", ExpectedTarga, columnIndex: 1);
        AssertTableRow(vehicleRow, ExpectedVehicleValues);

        Log("Assert butoni Kthehu");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        Assert.That(backBtn.Displayed, Is.True, "Butoni Kthehu nuk eshte visible");
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));

        Log("Zgjidh automjetin");
        SafeClick(By.XPath("//table[.//th[normalize-space()='Targa']]//button[normalize-space()='Zgjidh']"));

        Log("Assert faturen");
        IWebElement invoiceTable = WaitForTableByHeader("Nr fature");
        AssertTableHeaders(invoiceTable, ExpectedInvoiceHeaders);
        IWebElement invoiceRow = WaitForDataRow("Nr fature", ExpectedInvoiceValues[0], columnIndex: 0);
        AssertTableRow(invoiceRow, ExpectedInvoiceValues);

        Log("Assert butoni Shkarkoni pullën e taksës");
        IWebElement downloadStampBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//button[contains(.,'Shkarkoni pullën e taksës')]")));
        Assert.That(downloadStampBtn.Displayed, Is.True);
        Assert.That(downloadStampBtn.Text.Trim(), Does.Contain("Shkarkoni pullën e taksës"));

        Log("Assert Zgjidh Vitin");
        IWebElement yearLabel = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//label[contains(.,'Zgjidh Vitin')]")));
        Assert.That(yearLabel.Text.Trim(), Is.EqualTo("Zgjidh Vitin:"));

        IWebElement yearSelectEl = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("unpaidYearSelect")));
        var yearSelect = new SelectElement(yearSelectEl);
        Assert.That(yearSelect.SelectedOption.GetAttribute("value"), Is.EqualTo("2026"));
        Assert.That(yearSelect.SelectedOption.Text.Trim(), Is.EqualTo("2026"));

        Log("Click 'llogarit'");
        SafeClick(By.XPath("//button[normalize-space()='Llogarit']"));

        Log("Assert Taksa");
        IWebElement taxTable = WaitForTableByHeader("Taksë Karburanti");
        AssertTableHeaders(taxTable, ExpectedTaxHeaders);
        IWebElement taxRow = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//table[.//th[normalize-space()='Taksë Karburanti']]/tbody/tr")));
        AssertTableRow(taxRow, ExpectedTaxValues, extraActionCells: 1);

        IWebElement payBtn = taxRow.FindElement(By.CssSelector("button[title='Paguaj']"));
        Assert.That(payBtn.Displayed, Is.True, "Butoni Paguaj nuk eshte visible");

        Log("TEST PASSED");
    }

    private void OpenNewApplicationFromServicePage()
    {
        Log("Assert page header");
        IWebElement headerContainer = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("div.page-header-container")));
        Assert.That(headerContainer.Displayed, Is.True, "Page header nuk eshte visible");

        IWebElement serviceName = wait.Until(ExpectedConditions.ElementIsVisible(
            By.Id("serviceNameBreadcrumb")));
        Assert.That(serviceName.Displayed, Is.True, "Breadcrumb i sherbimit nuk eshte visible");
        Assert.That(serviceName.Text.Replace('\u00A0', ' ').Trim(), Is.EqualTo(ExpectedServiceName),
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
        Assert.That(perdorBtn.Text.Trim(), Is.EqualTo("Përdor"), "Butoni nuk eshte Përdor");

        Log("Kliko butonin Perdor");
        SafeClick(perdorLocator);

        Log("Kliko Aplikim i ri");
        By aplikimIRiLocator = By.CssSelector("button[aria-label='Aplikim i ri']");
        IWebElement aplikimIRi = wait.Until(ExpectedConditions.ElementIsVisible(aplikimIRiLocator));
        Assert.That(aplikimIRi.Displayed, Is.True, "Karta Aplikim i ri nuk eshte visible");
        IWebElement aplikimIRiTitle = aplikimIRi.FindElement(By.CssSelector("h6.mbx-title"));
        Assert.That(aplikimIRiTitle.Text.Trim(), Is.EqualTo("Aplikim i ri"),
            "Titulli i kartes nuk eshte Aplikim i ri");
        SafeClick(aplikimIRiLocator);
        Thread.Sleep(1500);
        DismissCookieBannerIfPresent();
    }

    private IWebElement WaitForTableByHeader(string headerText)
    {
        var dataWait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
        return dataWait.Until(d =>
        {
            try
            {
                var table = d.FindElement(By.XPath($"//table[.//th[normalize-space()='{headerText}']]"));
                var rows = table.FindElements(By.CssSelector("tbody tr"));
                return rows.Count > 0 && rows[0].Displayed ? table : null;
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
    }

    private IWebElement WaitForDataRow(string headerText, string expectedValue, int columnIndex)
    {
        return wait.Until(d =>
        {
            try
            {
                var table = d.FindElement(By.XPath($"//table[.//th[normalize-space()='{headerText}']]"));
                var row = table.FindElement(By.CssSelector("tbody tr"));
                string rowText = row.Text.Trim();
                Log("Current tbody/tr text: " + rowText);

                if (string.IsNullOrWhiteSpace(rowText))
                    return null;

                var cells = row.FindElements(By.XPath("./td"));
                if (cells.Count <= columnIndex)
                    return null;

                string actual = cells[columnIndex].Text.Trim();
                Log($"Detected row cell[{columnIndex}]: '{actual}'");
                return actual == expectedValue ? row : null;
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
    }

    private void AssertTableHeaders(IWebElement table, string[] expectedHeaders)
    {
        IWebElement headerRow = table.FindElement(By.CssSelector("thead tr"));
        Assert.That(headerRow.Displayed, Is.True, "Header row nuk është visible");

        var headerCells = headerRow.FindElements(By.XPath("./th"));
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
    }

    private void AssertTableRow(IWebElement row, string[] expectedValues, int extraActionCells = 0)
    {
        Assert.That(row.Displayed, Is.True, "Data row nuk është visible");

        var cells = row.FindElements(By.XPath("./td"));
        Log($"Numri i kolonave ne rresht: {cells.Count}");
        Assert.That(cells.Count, Is.EqualTo(expectedValues.Length + extraActionCells),
            $"Numri i kolonave nuk përputhet. Actual: {cells.Count}, Expected: {expectedValues.Length + extraActionCells}");

        for (int i = 0; i < expectedValues.Length; i++)
        {
            string actual = cells[i].Text.Trim();
            string expected = expectedValues[i];
            Log($"Cell[{i}] -> Actual: '{actual}' | Expected: '{expected}'");
            Assert.That(actual, Is.EqualTo(expected), $"Cell mismatch në kolonën {i}");
        }
    }
}
