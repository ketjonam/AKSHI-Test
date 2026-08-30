using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.MSHMS;

[Category("MSHMS")]
[Category("4435")]
public class _4701_ : QytetarNidF602TestBase
{
    protected override string ServiceCode => "4435";
    protected override string? ServiceTitle => "KontrolliMjekesorBaze";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void KontrolliMjekesorBaze()
    {
        Log("Assert page header");
        IWebElement headerContainer = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("div.page-header-container")));
        Assert.That(headerContainer.Displayed, Is.True, "Page header nuk eshte visible");

        IWebElement serviceName = wait.Until(ExpectedConditions.ElementIsVisible(
            By.Id("serviceNameBreadcrumb")));
        Assert.That(serviceName.Displayed, Is.True, "Breadcrumb i sherbimit nuk eshte visible");
        Assert.That(serviceName.Text.Trim(), Is.EqualTo("Kontrolli mjekësor bazë"),
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

        Log("Assert Title");
        IWebElement titleElement = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase")));
        Assert.That(titleElement.Text.Trim(), Is.EqualTo("Intervistat"), "Titulli nuk eshte Intervistat");

        Log("Wait qe tabela te ngarkohet");
        WaitForInterviewTable();

        Log("Assert fusha Kerko");
        IWebElement searchInput = FindSearchInput();
        Assert.That(searchInput.Displayed, Is.True);
        Assert.That(searchInput.GetAttribute("placeholder"), Is.EqualTo("Kërko"));

        Log("Assert kolonat e tabeles");
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Nr']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='NID']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Emri']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Tipi i vizitës']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Data e vizitës']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Barkodi']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='QSH']")).Displayed, Is.True);

        Log("Assert rreshtat e intervistave");
        Assert.That(GetTableRowCount(), Is.EqualTo(7));
        AssertInterviewRow(0, "1", CitizenNid, "Kadri Deli Kukaj", "VIzite Rutine", "24.01.2026", "F0004007128I", "QSH Koplik Shkoder");
        AssertInterviewRow(1, "2", CitizenNid, "Kadri Deli Kukaj", "VIzite Rutine", "02.03.2024", "F0003138623I", "QSH Koplik Shkoder");
        AssertInterviewRow(2, "3", CitizenNid, "Kadri Deli Kukaj", "VIzite Rutine", "05.08.2023", "F0002860499I", "QSH Koplik Shkoder");
        AssertInterviewRow(3, "4", CitizenNid, "Kadri Deli Kukaj", "VIzite Rutine", "11.10.2022", "F0002458374I", "QSH Koplik Shkoder");
        AssertInterviewRow(4, "5", CitizenNid, "Kadri Deli Kukaj", "VIzite Rutine", "31.08.2019", "F0001666730I", "QSH Koplik Shkoder");
        AssertInterviewRow(5, "6", CitizenNid, "Kadri Deli Kukaj", "VIzite Rutine", "07.04.2018", "F0001027408I", "QSH Koplik Shkoder");
        AssertInterviewRow(6, "7", CitizenNid, "Kadri Deli Kukaj", "VIzite Rutine", "05.09.2015", "F0000134655I", "QSH Koplik Shkoder");

        Log("kerko per intervist");
        FillSearch("intervist");

        Log("Assert mesazhin se nuk ka rezultate");
        IWebElement mesazhi = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//table//td[contains(normalize-space(),'Nuk ka rezultate')]")));
        Assert.That(mesazhi.Text.Trim(), Is.EqualTo("Nuk ka rezultate"));

        Log("Clear filter input");
        FillSearch(string.Empty);
        wait.Until(d => GetTableRowCount() == 7);

        Log("Shkruaj barkodin ne filter");
        FillSearch("F0004007128I");
        wait.Until(d => GetTableRowCount() == 1);
        AssertInterviewRow(0, "1", CitizenNid, "Kadri Deli Kukaj", "VIzite Rutine", "24.01.2026", "F0004007128I", "QSH Koplik Shkoder");

        Log("Clear filter input");
        FillSearch(string.Empty);
        wait.Until(d => GetTableRowCount() == 7);

        Log("Kliko butonin Shkarko");
        By shkarkoLocator = By.XPath(
            "//div[contains(@class,'custom-data-table')]//tbody/tr[1]//button[contains(normalize-space(),'Shkarko')]");
        IWebElement shkarkoBtn = wait.Until(ExpectedConditions.ElementToBeClickable(shkarkoLocator));
        Assert.That(shkarkoBtn.Displayed, Is.True, "Butoni Shkarko nuk eshte visible");
        Assert.That(shkarkoBtn.Enabled, Is.True, "Butoni Shkarko nuk eshte i aktivizuar");
        SafeClick(shkarkoLocator);
        Thread.Sleep(2000);

        Log("Kliko butonin Shfaq per intervisten 07.04.2018");
        By shfaqLocator = By.XPath(
            "//div[contains(@class,'custom-data-table')]//tbody/tr[.//td[normalize-space()='F0001027408I']]//button[normalize-space()='Shfaq']");
        IWebElement shfaqBtn = wait.Until(ExpectedConditions.ElementToBeClickable(shfaqLocator));
        Assert.That(shfaqBtn.Displayed, Is.True, "Butoni Shfaq nuk eshte visible");
        Assert.That(shfaqBtn.Enabled, Is.True, "Butoni Shfaq nuk eshte i aktivizuar");
        SafeClick(shfaqLocator);

        Log("Prit deri 200 sekonda per Ekzaminimet");
        var shfaqWait = new WebDriverWait(driver, TimeSpan.FromSeconds(200));
        IWebElement examinations = shfaqWait.Until(ExpectedConditions.ElementIsVisible(By.Id("divExaminations")));
        Assert.That(examinations.Displayed, Is.True, "Seksioni Ekzaminimet nuk eshte visible");

        IWebElement examinationsTitle = examinations.FindElement(By.CssSelector("h4.text-uppercase"));
        Assert.That(examinationsTitle.Text.Trim(), Is.EqualTo("Ekzaminimet"),
            "Titulli nuk eshte Ekzaminimet");

        Log("Assert kolonat e ekzaminimeve");
        Assert.That(examinations.FindElement(By.XPath(".//th[normalize-space()='Nr']")).Displayed, Is.True);
        Assert.That(examinations.FindElement(By.XPath(".//th[normalize-space()='Tipi i ekzaminimit']")).Displayed, Is.True);
        Assert.That(examinations.FindElement(By.XPath(".//th[normalize-space()='Data e ekzaminimit']")).Displayed, Is.True);
        Assert.That(examinations.FindElement(By.XPath(".//th[normalize-space()='Data e pranimit']")).Displayed, Is.True);
        Assert.That(examinations.FindElement(By.XPath(".//th[normalize-space()='Statusi']")).Displayed, Is.True);

        Log("Assert rreshtat e ekzaminimeve");
        Assert.That(GetExaminationRowCount(), Is.EqualTo(5));
        AssertExaminationRow(0, "1", "Urine Komplet", "07.04.2018", "07.04.2018", "Analize e perfunduar");
        AssertExaminationRow(1, "2", "Gjak Komplet", "07.04.2018", "07.04.2018", "Analize e perfunduar");
        AssertExaminationRow(2, "3", "Fece Gjak Okult", "07.04.2018", "07.04.2018", "Analize e perfunduar");
        AssertExaminationRow(3, "4", "EKG", "07.04.2018", "07.04.2018", "Analize e perfunduar");
        AssertExaminationRow(4, "5", "Test Biokimik (Serum)", "07.04.2018", "07.04.2018", "Analize e perfunduar");

        Log("TEST PASSED");
    }

    private void WaitForInterviewTable()
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
            input);
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
        return driver.FindElements(By.CssSelector(".custom-data-table table tbody tr")).Count;
    }

    private void AssertInterviewRow(
        int rowIndex,
        string nr,
        string nid,
        string emri,
        string tipiVizitës,
        string dataVizitës,
        string barkodi,
        string qsh)
    {
        var rows = driver.FindElements(By.CssSelector(".custom-data-table table tbody tr"));
        Assert.That(rows.Count, Is.GreaterThan(rowIndex), $"Tabela duhet te kete rreshtin {rowIndex}");

        var cells = rows[rowIndex].FindElements(By.TagName("td"));
        Assert.That(cells.Count, Is.EqualTo(8), $"Row {rowIndex} duhet te kete 8 kolona");
        Assert.That(cells[0].Text.Trim(), Is.EqualTo(nr), $"Row {rowIndex} Nr nuk perputhet");
        Assert.That(cells[1].Text.Trim(), Is.EqualTo(nid), $"Row {rowIndex} NID nuk perputhet");
        Assert.That(cells[2].Text.Trim(), Is.EqualTo(emri), $"Row {rowIndex} Emri nuk perputhet");
        Assert.That(cells[3].Text.Trim(), Is.EqualTo(tipiVizitës), $"Row {rowIndex} Tipi i vizites nuk perputhet");
        Assert.That(cells[4].Text.Trim(), Is.EqualTo(dataVizitës), $"Row {rowIndex} Data e vizites nuk perputhet");
        Assert.That(cells[5].Text.Trim(), Is.EqualTo(barkodi), $"Row {rowIndex} Barkodi nuk perputhet");
        Assert.That(cells[6].Text.Trim(), Is.EqualTo(qsh), $"Row {rowIndex} QSH nuk perputhet");

        IWebElement shfaqBtn = rows[rowIndex].FindElement(By.XPath(".//button[normalize-space()='Shfaq']"));
        IWebElement shkarkoBtn = rows[rowIndex].FindElement(By.XPath(".//button[contains(normalize-space(),'Shkarko')]"));
        Assert.That(shfaqBtn.Displayed, Is.True, $"Row {rowIndex} butoni Shfaq nuk eshte visible");
        Assert.That(shkarkoBtn.Displayed, Is.True, $"Row {rowIndex} butoni Shkarko nuk eshte visible");
    }

    private int GetExaminationRowCount()
    {
        return driver.FindElements(By.CssSelector("#divExaminations table tbody tr")).Count;
    }

    private void AssertExaminationRow(
        int rowIndex,
        string nr,
        string tipiEkzaminimit,
        string dataEkzaminimit,
        string dataPranimit,
        string statusi)
    {
        var rows = driver.FindElements(By.CssSelector("#divExaminations table tbody tr"));
        Assert.That(rows.Count, Is.GreaterThan(rowIndex), $"Tabela e ekzaminimeve duhet te kete rreshtin {rowIndex}");

        var cells = rows[rowIndex].FindElements(By.TagName("td"));
        Assert.That(cells.Count, Is.EqualTo(6), $"Row {rowIndex} duhet te kete 6 kolona");
        Assert.That(cells[0].Text.Trim(), Is.EqualTo(nr), $"Row {rowIndex} Nr nuk perputhet");
        Assert.That(cells[1].Text.Trim(), Is.EqualTo(tipiEkzaminimit), $"Row {rowIndex} Tipi i ekzaminimit nuk perputhet");
        Assert.That(cells[2].Text.Trim(), Is.EqualTo(dataEkzaminimit), $"Row {rowIndex} Data e ekzaminimit nuk perputhet");
        Assert.That(cells[3].Text.Trim(), Is.EqualTo(dataPranimit), $"Row {rowIndex} Data e pranimit nuk perputhet");
        Assert.That(cells[4].Text.Trim(), Is.EqualTo(statusi), $"Row {rowIndex} Statusi nuk perputhet");

        IWebElement shkarkoBtn = rows[rowIndex].FindElement(By.XPath(".//button[contains(normalize-space(),'Shkarko')]"));
        Assert.That(shkarkoBtn.Displayed, Is.True, $"Row {rowIndex} butoni Shkarko nuk eshte visible");
    }
}