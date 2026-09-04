using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.DPSHTRR;

[Category("DPSHTRR")]
[Category("879")]
public class _879_KundravajtjeRrugore : QytetarNidJ257TestBase
{
    protected override string ServiceCode => "879";
    protected override string? ServiceTitle => "KundravajtjeRrugore";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName = "Kundërvajtjet rrugore";
    private const string ExpectedTitle = "Lista e automjeteve në zotërim";
    private const string ExpectedTarga = "AB838HR";

    private static readonly string[] ExpectedVehicleHeaders =
    {
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
        "-",
        "-"
    };

    [Test]
    public void KundravajtjeRrugore()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("3 minuta kohëzgjatje"));

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
        IWebElement vehicleRow = WaitForVehicleRow(ExpectedTarga);
        AssertVehicleHeaders();
        AssertTableRow(vehicleRow, ExpectedVehicleValues);

        Log("Assert butoni Kthehu");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        Assert.That(backBtn.Displayed, Is.True, "Butoni Kthehu nuk eshte visible");
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));

        Log("Zgjidh automjetin");
        SafeClick(By.XPath("//div[contains(@class,'rdt_TableRow')]//button[normalize-space()='Zgjidh']"));

        Log("Assert gjobat");
        IWebElement gjobatAlert = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[@role='alert' and contains(normalize-space(.), \"Nuk ka gjoba për automjetin me targë\")]")));

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            gjobatAlert);

        Log("Gjobat text: " + gjobatAlert.Text.Trim());
        Assert.That(
            gjobatAlert.Text.Trim(),
            Is.EqualTo($"Nuk ka gjoba për automjetin me targë '{ExpectedTarga}'!"));

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

    private void AssertVehicleHeaders()
    {
        foreach (string header in ExpectedVehicleHeaders)
        {
            IWebElement headerCell = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath($"//div[contains(@class,'rdt_TableHead')]//div[normalize-space()='{header}']")));
            Assert.That(headerCell.Displayed, Is.True, $"Header '{header}' nuk eshte visible");
        }
    }

    private IWebElement WaitForVehicleRow(string expectedTarga)
    {
        var dataWait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
        return dataWait.Until(d =>
        {
            try
            {
                var row = d.FindElement(By.Id("row-0"));
                var cells = row.FindElements(By.CssSelector("[role='cell']"));
                if (cells.Count < 2)
                    return null;

                string targa = cells[1].Text.Trim();
                Log($"Detected vehicle row targa: '{targa}'");
                return targa == expectedTarga ? row : null;
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

    private void AssertTableRow(IWebElement row, string[] expectedValues)
    {
        Assert.That(row.Displayed, Is.True, "Data row nuk është visible");

        var cells = row.FindElements(By.CssSelector("[role='cell']"));
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
}
