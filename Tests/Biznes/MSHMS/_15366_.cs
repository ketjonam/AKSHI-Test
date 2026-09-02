using AKSHI.Test.Core;
using System.Text.RegularExpressions;

namespace AKSHI.Test.Tests.Biznes.MSHMS;

[Category("MSHMS")]
[Category("15366")]
public class _15366_ : BiznesTestBase
{
    protected override string ServiceCode => "15366";
    protected override string? ServiceTitle => "LejeImportiPajisjesh";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName = "Autodeklarimi i importit për pajisje mjekësore";

    [Test]
    public void LejeImportiPajisjesh()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("2 minuta kohëzgjatje"));

        Log("Assert 3 hapa, hapi i pare aktiv");
        AssertActiveSteps(activeCount: 1);

        Log("Assert Step 1 Title");
        IWebElement step1Title = WaitForStepTitle("TË DHËNA PERSONALE");
        Assert.That(NormalizeText(step1Title.Text), Is.EqualTo("TË DHËNA PERSONALE"));

        Log("Assert Statusi i aplikuesit");
        IWebElement applicantStatusSelect = wait.Until(ExpectedConditions.ElementIsVisible(
            By.Id("applicantStatus")));
        var applicantStatus = new SelectElement(applicantStatusSelect);
        Assert.That(applicantStatus.Options.Count, Is.EqualTo(3));
        Assert.That(applicantStatus.Options[0].GetAttribute("value"), Is.EqualTo("AUTHP"));
        Assert.That(applicantStatus.Options[0].Text.Trim(), Is.EqualTo("Perfaqesues"));
        Assert.That(applicantStatus.Options[1].GetAttribute("value"), Is.EqualTo("MANUF"));
        Assert.That(applicantStatus.Options[1].Text.Trim(), Is.EqualTo("Prodhues"));
        Assert.That(applicantStatus.Options[2].GetAttribute("value"), Is.EqualTo("MERCH"));
        Assert.That(applicantStatus.Options[2].Text.Trim(), Is.EqualTo("Tregtues me shumice"));
        Assert.That(applicantStatus.SelectedOption.GetAttribute("value"), Is.EqualTo("AUTHP"));

        Log("Assert te dhenat e subjektit");
        AssertReadonlyByName("nuis", "M53330201S");
        AssertReadonlyByName("subjectName", "Migen Dërstila");
        AssertReadonlyByNameNormalized("contactPerson", "Migen Luan Dërstila");
        AssertReadonlyByName("phone", "+355684053531");
        AssertReadonlyByName("email", "migen.derstila@kreatx.com");
        AssertReadonlyByNameNormalized(
            "address",
            "Derstile; ; ; ; Gjinar; ; 0000; Elbasan,Elbasan,ELBASAN,Elbasan");

        Log("Assert Qyteti/Shteti");
        IWebElement cityCountry = FindInputByName("cityCountry");
        Assert.That(cityCountry.GetAttribute("readonly"), Is.Null);
        Assert.That(cityCountry.GetAttribute("disabled"), Is.Null);
        Assert.That(cityCountry.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 1");
        AssertBackAndContinue("Vazhdo");

        Log("Ploteso Qyteti/Shteti");
        FillByName("cityCountry", "Elbasan");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 title");
        IWebElement step2Title = WaitForStepTitle("TË DHËNAT PËR PAJISJET");
        Assert.That(NormalizeText(step2Title.Text), Does.Contain("TË DHËNAT PËR PAJISJET"));

        Log("Assert 3 hapa, dy te paret aktiv");
        AssertActiveSteps(activeCount: 2);

        Log("Assert kerkimin e pajisjeve");
        IWebElement searchLabel = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//label[normalize-space()='Kërko']")));
        Assert.That(searchLabel.Displayed, Is.True);
        IWebElement searchInput = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//label[normalize-space()='Kërko']/following-sibling::input[contains(@class,'ealb-input')]")));

        Log("Assert kolonat e tabeles se pajisjeve");
        AssertTableHeader("Emri");
        AssertTableHeader("Modeli");
        AssertTableHeader("Prodhuesi");
        AssertTableHeader("Klasa");
        AssertTableHeader("Kategoria");
        AssertTableHeader("Nr. regjistrit");

        Log("Assert rreshtin e pajisjes dhe butonin Zgjidh");
        IWebElement zgjidhBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//table//button[normalize-space()='Zgjidh']")));
        Assert.That(zgjidhBtn.Displayed, Is.True);
        Assert.That(zgjidhBtn.Text.Trim(), Is.EqualTo("Zgjidh"));

        IWebElement deviceRow = zgjidhBtn.FindElement(By.XPath("./ancestor::tr[1]"));
        var deviceCells = deviceRow.FindElements(By.CssSelector("td"));
        Assert.That(deviceCells.Count, Is.GreaterThanOrEqualTo(7));
        Assert.That(deviceCells[0].Text.Trim(), Is.EqualTo("test"));
        Assert.That(deviceCells[1].Text.Trim(), Is.EqualTo("test"));
        Assert.That(deviceCells[2].Text.Trim(), Is.EqualTo("test"));
        Assert.That(deviceCells[3].Text.Trim(), Is.EqualTo("Klasa I"));
        Assert.That(deviceCells[4].Text.Trim(), Is.EqualTo("Tjeter"));
        Assert.That(deviceCells[5].Text.Trim(), Is.EqualTo("22748"));

        Log("Kërko pajisjen test");
        searchInput.Clear();
        searchInput.SendKeys("test");
        Thread.Sleep(1000);
        zgjidhBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//table//button[normalize-space()='Zgjidh']")));
        Assert.That(zgjidhBtn.Displayed, Is.True);

        Log("Assert butonat e navigimit Step 2");
        AssertBackAndContinue("Vazhdo");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement step3Title = WaitForStepTitle("TË DHËNAT E IMPORTIT");
        Assert.That(NormalizeText(step3Title.Text), Is.EqualTo("TË DHËNAT E IMPORTIT"));

        Log("Assert 3 hapa, te gjithe aktiv");
        AssertActiveSteps(activeCount: 3);

        Log("Assert kolonat e tabeles se importit");
        AssertTableHeader("Emri");
        AssertTableHeader("Numri i serisë");
        AssertTableHeader("Data e prodhimit");
        AssertTableHeader("Data e skadencës");
        AssertTableHeader("Sasia");

        Log("Assert mesazhin se nuk ka linja importi");
        IWebElement msgImport = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//table//td[contains(normalize-space(),'Nuk ka linja importi')]")));
        Assert.That(msgImport.Text.Trim(), Is.EqualTo("Nuk ka linja importi"));

        Log("Assert butonat e navigimit Step 3");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(dergoBtn.Text.Trim(), Does.Contain("Dërgo"));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

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
        Assert.That(serviceName.Text.Trim(), Is.EqualTo(ExpectedServiceName),
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
    }

    private IWebElement WaitForStepTitle(string expectedUpper)
    {
        return wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4"));
            foreach (var title in titles)
            {
                try
                {
                    if (NormalizeText(title.Text).Contains(expectedUpper))
                        return title;
                }
                catch (StaleElementReferenceException)
                {
                }
            }
            return null;
        });
    }

    private IWebElement FindInputByName(string name)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector($"input[name='{name}']")));
    }

    private void AssertReadonlyByName(string name, string expectedValue)
    {
        IWebElement input = FindInputByName(name);
        Assert.That(input.GetAttribute("readonly") ?? input.GetAttribute("disabled"), Is.Not.Null,
            $"Fusha {name} duhet te jete readonly");
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue),
            $"Vlera e fushes {name} nuk eshte e sakte");
    }

    private void AssertReadonlyByNameNormalized(string name, string expectedValue)
    {
        IWebElement input = FindInputByName(name);
        Assert.That(input.GetAttribute("readonly") ?? input.GetAttribute("disabled"), Is.Not.Null,
            $"Fusha {name} duhet te jete readonly");
        Assert.That(NormalizeSpaces(input.GetAttribute("value")), Is.EqualTo(expectedValue),
            $"Vlera e fushes {name} nuk eshte e sakte");
    }

    private void FillByName(string name, string value)
    {
        IWebElement input = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector($"input[name='{name}']")));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            input);
        Thread.Sleep(200);
        try
        {
            input.Click();
            input.Clear();
            input.SendKeys(value);
        }
        catch (ElementClickInterceptedException)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].focus(); arguments[0].value = '';",
                input);
            input.SendKeys(value);
        }
        Thread.Sleep(200);
    }

    private void AssertActiveSteps(int activeCount, int totalCount = 3)
    {
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(totalCount));
        for (int i = 0; i < steps.Count; i++)
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
            if (i < activeCount)
                Assert.That(steps[i].GetAttribute("class"), Does.Contain("active"));
            else
                Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
        }
    }

    private void AssertBackAndContinue(string continueText)
    {
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Does.Contain(continueText));
    }

    private void AssertTableHeader(string headerText)
    {
        IWebElement header = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//th[normalize-space()='{headerText}']")));
        Assert.That(header.Displayed, Is.True, $"Kolona '{headerText}' nuk u gjet");
    }

    private static string NormalizeText(string? value)
    {
        return NormalizeSpaces(value).ToUpperInvariant();
    }

    private static string NormalizeSpaces(string? value)
    {
        return Regex.Replace(value ?? string.Empty, @"\s+", " ").Trim();
    }
}
