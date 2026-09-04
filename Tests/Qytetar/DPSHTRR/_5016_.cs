using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.DPSHTRR;

[Category("DPSHTRR")]
[Category("5016")]
public class _5016_ : QytetarNidJ257TestBase
{
    protected override string ServiceCode => "5016";
    protected override string? ServiceTitle => "GjendjaAktiveeMjetit";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName = "Konfirmim për gjendjen aktive të mjetit";
    private const string ExpectedTitle = "Lista e automjeteve në zotërim";
    private const string ExpectedTarga = "AB838HR";

    private static readonly string[] ExpectedVehicleHeaders =
    {
        "Zgjidh",
        "Targa",
        "Shasia",
        "Lloji",
        "Marka",
        "Modeli",
        "Tipi",
        "Ngjyra",
        "Lënda Djegëse",
        "Viti i Prodhimit",
        "Gomat",
        "Dyer",
        "Vende",
        "Nr.Serial i deklaratës së shitjes",
        "Nr.kontratës deklaratës së shitjes"
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

    private static readonly string[] ExpectedStatusHeaders =
    {
        "Kartela",
        "Data e lejes",
        "Statusi i mjetit"
    };

    private static readonly string[] ExpectedStatusValues =
    {
        "ELD00788422",
        "11.10.2022",
        ""
    };

    [Test]
    public void GjendjaAktiveeMjetit()
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
            By.XPath($"//h4[contains(@class,'text-uppercase') and contains(.,'{ExpectedTitle}')]")));
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

        Log("Assert gjendja e mjetit");
        IWebElement gjendjaSection = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[contains(@class,'mt-5') and .//h6[contains(.,'Gjendja e mjetit me targë:')]]")));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            gjendjaSection);

        Log("Assert titulli i seksionit");
        IWebElement titulliSeksionit = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//h6[contains(.,'Gjendja e mjetit me targë:')]")));
        Log("Title text: " + titulliSeksionit.Text.Trim());
        Assert.That(titulliSeksionit.Text.Trim(), Does.Contain("Gjendja e mjetit me targë:"));
        Assert.That(titulliSeksionit.Text.Trim(), Does.Contain(ExpectedTarga));

        Log("Assert tabelen e gjendjes");
        IWebElement statusTable = WaitForTableByHeader("Kartela");
        AssertTableHeaders(statusTable, ExpectedStatusHeaders);
        IWebElement statusRow = WaitForDataRow("Kartela", ExpectedStatusValues[0], columnIndex: 0);
        AssertTableRow(statusRow, ExpectedStatusValues);

        Log("Assert 'Të dhëna të tjera'");
        IWebElement otherDataTitle = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//h6[contains(.,'Të dhëna të tjera')]")));
        Assert.That(otherDataTitle.Text.Trim(), Is.EqualTo("Të dhëna të tjera"));

        Log("Assert 'Gjendje e TVMP'");
        AssertDisabledInput("tvmpStatus", "Mjeti nuk ka detyrime ne taksa");

        Log("Assert 'Gjendja e gjobës KTV'");
        AssertDisabledInput("ktvFine", "Mjeti nuk ka detyrime ne gjoba");

        Log("Assert 'Gjendja e gjobave të kontrollit në rrugë'");
        AssertDisabledInput("roadFine", "Mjeti nuk ka detyrime ne gjoba");

        Log("Assert 'Gjendja e dosjes'");
        AssertDisabledInput("fileStatus", "Mjeti nuk ka bllokime");

        Log("Assert 'Statusi'");
        AssertDisabledInput("vehicleStatus", "");

        Log("Assert butoni 'Shkarko dokumentin e vulosur'");
        IWebElement downloadButton = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//button[contains(.,'Shkarko dokumentin e vulosur')]")));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            downloadButton);
        Log("Download button text: " + downloadButton.Text.Trim());
        Assert.That(downloadButton.Displayed, Is.True, "Butoni 'Shkarko dokumentin e vulosur' nuk eshte visible");
        Assert.That(downloadButton.Text.Trim(), Does.Contain("Shkarko dokumentin e vulosur"));

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

    private void AssertTableRow(IWebElement row, string[] expectedValues)
    {
        Assert.That(row.Displayed, Is.True, "Data row nuk është visible");

        var cells = row.FindElements(By.XPath("./td"));
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
    }

    private void AssertDisabledInput(string id, string expectedValue)
    {
        IWebElement input = wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            input);
        Thread.Sleep(300);
        string actual = input.GetAttribute("value") ?? "";
        Log($"{id} value: '{actual}'");
        Assert.That(actual, Is.EqualTo(expectedValue));
    }
}
