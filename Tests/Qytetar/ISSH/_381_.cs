using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("381")]
public class _381_ : QytetarNidF602TestBase
{
    protected override string ServiceCode => "381";
    protected override string? ServiceTitle => "PensioniIM";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    [Test]
    public void PensioniIM()
    {
        OpenNewApplicationFromServicePage("Pensioni im");

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
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("EKSTRAKTI NGA INSTITUTI I SIGURIMEVE SHOQËRORE"));

        Log("Assert label-at e pensionistit");
        AssertLabel("nid", "Numri Personal:");
        AssertLabel("firstName", "Emri:");
        AssertLabel("fatherName", "Atësia:");
        AssertLabel("lastName", "Mbiemri:");

        Log("Assert te dhenat e pensionistit");
        AssertReadonlyField("nid", CitizenNid);
        AssertReadonlyField("firstName", "KADRI");
        AssertReadonlyField("fatherName", "DELI");
        AssertReadonlyField("lastName", "KUKAJ");

        Log("Wait qe te dhenat e tabeles te ngarkohen");
        WaitForPensionTable();

        Log("Assert kolonat e tabeles");
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'rdt_TableCol') and normalize-space()='#']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'rdt_TableCol') and normalize-space()='Lloji i pensionit']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'rdt_TableCol') and normalize-space()='Numri i pensionit']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'rdt_TableCol') and normalize-space()='DRSSH']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'rdt_TableCol') and normalize-space()='E ardhura bruto']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'rdt_TableCol') and normalize-space()='Qendra paguese']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'rdt_TableCol') and normalize-space()='Dt e fillimit']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'rdt_TableCol') and normalize-space()='Vjetërsia']")).Displayed, Is.True);

        var rows = driver.FindElements(By.CssSelector(".rdt_TableRow"));
        Assert.That(rows.Count, Is.EqualTo(1));

        Log("Assert rreshtin e pensionit");
        AssertTableRow(0, "1", "Pleqerie Urban", "132655", "Shkoder", "55,369", "FI Bank (SH)", "12.02.2022", "43.8vjet");

        Log("Assert butoni Shkarko");
        var shkarkoButtons = driver.FindElements(By.CssSelector("button.btn-next"));
        Assert.That(shkarkoButtons.Count, Is.EqualTo(1));
        Assert.That(shkarkoButtons[0].Text.Trim(), Does.Contain("Shkarko"));

        Log("Kliko butonin shkarko");
        SafeClick(By.CssSelector("#row-0 button.btn-next"));
        Thread.Sleep(2000);

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

    private IWebElement FindFieldById(string id)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
    }

    private void AssertReadonlyField(string id, string expectedValue)
    {
        IWebElement input = FindFieldById(id);
        Assert.That(input.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
    }

    private void AssertLabel(string forId, string expectedLabel)
    {
        IWebElement label = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector($"label[for='{forId}']")));
        Assert.That(label.Text.Trim(), Is.EqualTo(expectedLabel));
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

    private void AssertTableRow(int rowIndex,
        string nr,
        string lloji,
        string nrPensionit,
        string drssh,
        string eArdhuraBruto,
        string qendraPaguese,
        string dtFillimit,
        string vjetersia)
    {
        IWebElement row = wait.Until(ExpectedConditions.ElementIsVisible(By.Id($"row-{rowIndex}")));
        var cells = row.FindElements(By.CssSelector("[role='cell']"));

        Assert.That(cells.Count, Is.EqualTo(9), $"Row {rowIndex} should have 9 columns");
        Assert.That(cells[0].Text.Trim(), Is.EqualTo(nr), $"Row {rowIndex} #");
        Assert.That(cells[1].Text.Trim(), Is.EqualTo(lloji), $"Row {rowIndex} Lloji i pensionit");
        Assert.That(cells[2].Text.Trim(), Is.EqualTo(nrPensionit), $"Row {rowIndex} Numri i pensionit");
        Assert.That(cells[3].Text.Trim(), Is.EqualTo(drssh), $"Row {rowIndex} DRSSH");
        Assert.That(cells[4].Text.Trim(), Is.EqualTo(eArdhuraBruto), $"Row {rowIndex} E ardhura bruto");
        Assert.That(cells[5].Text.Trim(), Is.EqualTo(qendraPaguese), $"Row {rowIndex} Qendra paguese");
        Assert.That(cells[6].Text.Trim(), Is.EqualTo(dtFillimit), $"Row {rowIndex} Dt e fillimit");
        Assert.That(cells[7].Text.Trim(), Is.EqualTo(vjetersia), $"Row {rowIndex} Vjetërsia");
        Assert.That(cells[8].Text.Trim(), Does.Contain("Shkarko"), $"Row {rowIndex} Shkarko");
    }
}
