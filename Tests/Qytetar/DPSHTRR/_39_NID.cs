using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.DPSHTRR;

[Category("DPSHTRR")]
[Category("39")]
public class _39_NID : QytetarNidJ257TestBase
{
    protected override string ServiceCode => "39";
    protected override string? ServiceTitle => "AutomjeteteMia";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName = "Automjetet e mia";
    private const string ExpectedTitle =
        "Ekstrakt nga Regjistri i Drejtorisë së Përgjithshme të Shërbimeve të Transportit Rrugor";
    private const string ExpectedTarga = "AB838HR";

    private static readonly string[] ExpectedHeaders =
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

    private static readonly string[] ExpectedValues =
    {
        "1",
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
        "-",
        "-"
    };

    [Test]
    public void AutomjeteteMia()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("1 minutë kohëzgjatje"));

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
        WaitForVehicleTable();

        Log("Assert fusha Kerko");
        IWebElement searchInput = FindSearchInput();
        Assert.That(searchInput.Displayed, Is.True);
        Assert.That(searchInput.GetAttribute("placeholder"), Is.EqualTo("Kërko"));
        Assert.That(searchInput.GetAttribute("value"), Is.EqualTo(string.Empty));

        Log("Filter table");
        FillSearch("test");

        Log("Wait for no-data row");
        IWebElement noDataRow = wait.Until(d =>
        {
            try
            {
                var row = d.FindElement(By.CssSelector(".custom-data-table table tbody tr"));
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
        FillSearch(string.Empty);

        Log("Wait for real data row to appear");
        IWebElement dataRow = WaitForDataRow(ExpectedTarga);
        Assert.That(dataRow, Is.Not.Null, "Data row nuk u ngarkua pas pastrimit te filtrit");

        Log("Gjej header row (tr)");
        IWebElement headerRow = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector(".custom-data-table table thead tr")));
        Assert.That(headerRow.Displayed, Is.True, "Header row nuk është visible");

        Log("Merr te gjitha kolonat (th) nga header row");
        var headerCells = headerRow.FindElements(By.XPath("./th"));

        Log($"Numri i header cells: {headerCells.Count}");
        Assert.That(headerCells.Count, Is.EqualTo(ExpectedHeaders.Length),
            $"Numri i kolonave nuk përputhet. Actual: {headerCells.Count}, Expected: {ExpectedHeaders.Length}");

        for (int i = 0; i < ExpectedHeaders.Length; i++)
        {
            string actual = headerCells[i].Text.Trim();
            string expected = ExpectedHeaders[i];

            Log($"Header[{i}] -> Actual: '{actual}' | Expected: '{expected}'");
            Assert.That(actual, Is.EqualTo(expected), $"Header mismatch në kolonën {i}");
        }

        Log("Gjej data row (tr)");
        dataRow = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector(".custom-data-table table tbody tr")));
        Assert.That(dataRow.Displayed, Is.True, "Data row nuk është visible");

        Log("Merr te gjitha kolonat (td) nga data row");
        var cells = dataRow.FindElements(By.XPath("./td"));

        Log($"Numri i kolonave ne rresht: {cells.Count}");
        Assert.That(cells.Count, Is.EqualTo(ExpectedValues.Length),
            $"Numri i kolonave nuk përputhet. Actual: {cells.Count}, Expected: {ExpectedValues.Length}");

        for (int i = 0; i < ExpectedValues.Length; i++)
        {
            string actual = cells[i].Text.Trim();
            string expected = ExpectedValues[i];

            Log($"Cell[{i}] -> Actual: '{actual}' | Expected: '{expected}'");
            Assert.That(actual, Is.EqualTo(expected), $"Cell mismatch në kolonën {i}");
        }

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

    private void WaitForVehicleTable()
    {
        var dataWait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
        dataWait.Until(d =>
        {
            try
            {
                var rows = d.FindElements(By.CssSelector(".custom-data-table table tbody tr"));
                return rows.Count > 0 && rows[0].Displayed;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });
    }

    private IWebElement WaitForDataRow(string expectedTarga)
    {
        return wait.Until(d =>
        {
            try
            {
                var row = d.FindElement(By.CssSelector(".custom-data-table table tbody tr"));
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

                return (nr == "1" && targa == expectedTarga) ? row : null;
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
}
