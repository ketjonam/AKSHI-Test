using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.MSHMS;

[Category("MSHMS")]
[Category("4705")]
public class _4705_ : QytetarNidF602TestBase
{
    protected override string ServiceCode => "4705";
    protected override string? ServiceTitle => "ListaESemundjeveKronike";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void ListaESemundjeveKronike()
    {
        Log("Assert page header");
        IWebElement headerContainer = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("div.page-header-container")));
        Assert.That(headerContainer.Displayed, Is.True, "Page header nuk eshte visible");

        IWebElement serviceName = wait.Until(ExpectedConditions.ElementIsVisible(
            By.Id("serviceNameBreadcrumb")));
        Assert.That(serviceName.Displayed, Is.True, "Breadcrumb i sherbimit nuk eshte visible");
        Assert.That(serviceName.Text.Trim(), Is.EqualTo("Lista e sëmundjeve kronike"),
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

        Log("Assert popup E RENDESISHME");
        IWebElement alertModal = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("div.alert-modal-container")));
        Assert.That(alertModal.Displayed, Is.True, "Popup nuk eshte visible");

        IWebElement alertIcon = alertModal.FindElement(By.CssSelector("div.alert-modal-icon-wrapper"));
        Assert.That(alertIcon.Displayed, Is.True, "Ikona e popup nuk eshte visible");

        IWebElement alertTitle = alertModal.FindElement(By.CssSelector("h2.alert-modal-title"));
        Assert.That(alertTitle.Text.Trim(), Is.EqualTo("E RËNDËSISHME!"),
            "Titulli i popup nuk eshte i sakte");

        IWebElement alertDescription = alertModal.FindElement(By.CssSelector("div.alert-modal-description"));
        string alertDescriptionText = string.Join(" ",
            alertDescription.Text.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries));
        Assert.That(alertDescriptionText, Is.EqualTo(
            "Përshëndetje Kadri Kukaj, mjeku juaj i familjes është Elisa Stolaj me numër telefoni 00393401143018 në Qendrën Shëndetësore: Koplik - Qender."),
            "Pershkrimi i popup nuk eshte i sakte");

        IWebElement vazhdoBtn = alertModal.FindElement(
            By.CssSelector("button.alert-modal-button.alert-modal-button--primary"));
        Assert.That(vazhdoBtn.Displayed, Is.True, "Butoni Vazhdo nuk eshte visible");
        Assert.That(vazhdoBtn.Text.Trim(), Is.EqualTo("Vazhdo"), "Butoni i popup nuk eshte Vazhdo");

        Log("Kliko Vazhdo ne popup");
        SafeClick(By.CssSelector("button.alert-modal-button.alert-modal-button--primary"));
        Thread.Sleep(1500);

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

        Log("Assert kohezgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert nje hap aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(1));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));

        Log("Assert Title te dhena personale");
        IWebElement personalTitle = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//h4[contains(@class,'text-uppercase') and normalize-space()='Të dhëna personale të aplikantit']")));
        Assert.That(personalTitle.Displayed, Is.True, "Titulli i te dhenave personale nuk eshte visible");

        Log("Assert te dhenat personale te aplikantit");
        AssertReadonlyField("nid", "NID", CitizenNid);
        AssertReadonlyField("name", "Emri", "Kadri");
        AssertReadonlyField("fatherName", "Atësia", "Deli");
        AssertReadonlyField("surname", "Mbiemri", "Kukaj");

        Log("Assert Title lista e semundjeve");
        IWebElement diseasesTitle = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//h4[contains(@class,'text-uppercase') and normalize-space()='Lista e sëmundjeve']")));
        Assert.That(diseasesTitle.Displayed, Is.True, "Titulli i listes se semundjeve nuk eshte visible");

        Log("Wait qe tabela te ngarkohet");
        WaitForDiseaseTable();

        Log("Assert kolonat e tabeles");
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='#']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Kodi i diagnozës']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Emri i diagnozës']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Dt. fillimit']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Dt. mbarimit']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Shënime']")).Displayed, Is.True);

        Log("Assert rreshtat e semundjeve");
        Assert.That(GetTableRowCount(), Is.EqualTo(2));
        AssertDiseaseRow(0, "1", "401", "Hipertension esencial", "19.12.2020", "-", "N/A");
        AssertDiseaseRow(1, "2", "493", "Astma bronkiale", "19.12.2020", "-", "N/A");

        Log("Assert butoni Kthehu");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        Assert.That(backBtn.Displayed, Is.True, "Butoni Kthehu nuk eshte visible");
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));

        Log("TEST PASSED");
    }

    private void AssertReadonlyField(string id, string expectedLabel, string expectedValue)
    {
        IWebElement label = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector($"label[for='{id}']")));
        Assert.That(label.Text.Trim(), Is.EqualTo(expectedLabel), $"Label per {id} nuk eshte i sakte");

        IWebElement input = wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
        Assert.That(input.GetAttribute("readonly"), Is.Not.Null, $"Fusha {id} duhet te jete readonly");
        Assert.That(input.GetAttribute("disabled"), Is.Not.Null, $"Fusha {id} duhet te jete disabled");
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue),
            $"Vlera e fushes {id} nuk eshte e sakte");
    }

    private void WaitForDiseaseTable()
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

    private int GetTableRowCount()
    {
        return driver.FindElements(By.CssSelector(".custom-data-table table tbody tr")).Count;
    }

    private void AssertDiseaseRow(
        int rowIndex,
        string nr,
        string diagnosisCode,
        string diagnosisName,
        string startDate,
        string endDate,
        string notes)
    {
        var rows = driver.FindElements(By.CssSelector(".custom-data-table table tbody tr"));
        Assert.That(rows.Count, Is.GreaterThan(rowIndex), $"Tabela duhet te kete rreshtin {rowIndex}");

        var cells = rows[rowIndex].FindElements(By.TagName("td"));
        Assert.That(cells.Count, Is.EqualTo(6), $"Row {rowIndex} duhet te kete 6 kolona");
        Assert.That(cells[0].Text.Trim(), Is.EqualTo(nr), $"Row {rowIndex} # nuk perputhet");
        Assert.That(cells[1].Text.Trim(), Is.EqualTo(diagnosisCode), $"Row {rowIndex} Kodi i diagnozes nuk perputhet");
        Assert.That(cells[2].Text.Trim(), Is.EqualTo(diagnosisName), $"Row {rowIndex} Emri i diagnozes nuk perputhet");
        Assert.That(cells[3].Text.Trim(), Is.EqualTo(startDate), $"Row {rowIndex} Dt. fillimit nuk perputhet");
        Assert.That(cells[4].Text.Trim(), Is.EqualTo(endDate), $"Row {rowIndex} Dt. mbarimit nuk perputhet");
        Assert.That(cells[5].Text.Trim(), Is.EqualTo(notes), $"Row {rowIndex} Shenime nuk perputhet");
    }
}
