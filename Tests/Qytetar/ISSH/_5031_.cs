using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("5031")]
public class _5031_ : QytetarNidF602TestBase
{
    protected override string ServiceCode => "5031";
    protected override string? ServiceTitle => "KartelaPleqerise";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    [Test]
    public void KartelaPleqerise()
    {
        OpenNewApplicationFromServicePage("Pensioni i pleqërisë (kartela e pleqërisë)");

        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.px-4.pb-4.text-uppercase")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("KARTELË PLEQËRIE"));

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("1 minut kohëzgjatje"));

        Log("Assert nje hap aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(1));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));

        Log("Assert label-at e karteles");
        AssertLabel("cardCode", "Kodi Kartelës:");
        AssertLabel("firstName", "Emri:");
        AssertLabel("fatherName", "Atësia:");
        AssertLabel("birthDate", "Datëlindja:");
        AssertLabel("personID", "ID e personit:");
        AssertLabel("fileNumber", "Nr Dosjes:");
        AssertLabel("drssh", "Drssh:");
        AssertLabel("pensionStartDate", "Dt Fillimit Pensioni:");
        AssertLabel("assignmentDate", "Dt Caktimi:");
        AssertLabel("paymentCenter", "Qendra Paguese:");
        AssertLabel("lastName", "Mbiemri:");
        AssertLabel("maidenName", "Mbiemri Vajzërisë:");
        AssertLabel("motherName", "Amësia:");
        AssertLabel("gender", "Gjinia:");
        AssertLabel("civilStatus", "Gjendja Civile:");
        AssertLabel("agency", "Agjencia:");
        AssertLabel("requestDate", "Dt Kërkesë:");
        AssertLabel("assignmentNumber", "Nr Caktimi:");
        AssertLabel("lastPaymentDate", "Dt Pagesës Fundit:");
        AssertLabel("paymentCardNumber", "Nr Kartele Pagese:");
        AssertLabel("workSeniority", "Vjetërsia në punë:");
        AssertLabel("evaluatedBase", "Baza Vleresuar (lek):");
        AssertLabel("initialMeasure", "Masa fillestare (lek):");
        AssertLabel("totalIncome", "Totali i të ardhurave (lek):");
        AssertLabel("netSalary", "Paga Neto (lek):");
        AssertLabel("currentMeasure", "Masa Aktuale (lek):");
        AssertLabel("pensionCode", "Kodi i pensionit:");
        AssertLabel("birthRightDate", "Dt Lindje Drejte:");

        Log("Assert te dhenat e karteles se pleqerise");
        AssertReadonlyField("cardCode", "P100070853U");
        AssertReadonlyField("firstName", "KADRI");
        AssertReadonlyField("fatherName", "DELI");
        AssertReadonlyField("birthDate", "16.04.1956");
        AssertReadonlyField("personID", CitizenNid);
        AssertReadonlyField("fileNumber", "132655");
        AssertReadonlyField("drssh", "Drejtoria Shkoder");
        AssertReadonlyField("pensionStartDate", "16.04.2021");
        AssertReadonlyField("assignmentDate", "24.02.2022");
        AssertReadonlyField("paymentCenter", "306 B");
        AssertReadonlyField("lastName", "KUKAJ");
        AssertReadonlyField("maidenName", string.Empty);
        AssertReadonlyField("motherName", "TALE");
        AssertReadonlyField("gender", "Mashkull");
        AssertReadonlyField("civilStatus", "Martuar");
        AssertReadonlyField("agency", "Shkoder");
        AssertReadonlyField("requestDate", "16.02.2022");
        AssertReadonlyField("assignmentNumber", "324");
        AssertReadonlyField("lastPaymentDate", string.Empty);
        AssertReadonlyField("paymentCardNumber", string.Empty);
        AssertReadonlyField("workSeniority", "43");
        AssertReadonlyField("evaluatedBase", "71613");
        AssertReadonlyField("initialMeasure", string.Empty);
        AssertReadonlyField("totalIncome", "55369");
        AssertReadonlyField("netSalary", "0");
        AssertReadonlyField("currentMeasure", "52985");
        AssertReadonlyField("pensionCode", "112101");
        AssertReadonlyField("birthRightDate", string.Empty);

        Log("Assert vjetersia ne pune muaj");
        IWebElement workSeniorityMonths = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//input[@id='workSeniority']/following-sibling::input")));
        Assert.That(workSeniorityMonths.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(workSeniorityMonths.GetAttribute("value").Trim(), Is.EqualTo(".9"));

        IWebElement yearsUnit = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//input[@id='workSeniority']/following-sibling::span[1]")));
        IWebElement monthsUnit = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//input[@id='workSeniority']/following-sibling::span[2]")));
        Assert.That(yearsUnit.Text.Trim(), Is.EqualTo("V"));
        Assert.That(monthsUnit.Text.Trim(), Is.EqualTo("M"));

        Log("Assert butoni Shkarko");
        IWebElement shkarkoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.btn-next")));
        Assert.That(shkarkoBtn.Text.Trim(), Does.Contain("Shkarko"));

        Log("Assert butoni Kthehu");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));

        Log("Kliko butonin Shkarko");
        SafeClick(By.CssSelector("button.btn-next"));
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
}
