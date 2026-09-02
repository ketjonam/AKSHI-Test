using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Biznes.ISSH;

[Category("ISSH")]
[Category("10059")]
public class _10059_ : BiznesTestBase
{
    protected override string ServiceCode => "10059";
    protected override string? ServiceTitle => "ShperblimLindjeGrandBiznes";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Kërkesë për shpërblim lindje dhe grant për fëmijë të lindur për personat e punësuar";

    private static readonly string[] AlbanianMonths =
    {
        "", "Janar", "Shkurt", "Mars", "Prill", "Maj", "Qershor",
        "Korrik", "Gusht", "Shtator", "Tetor", "Nëntor", "Dhjetor"
    };

    private static string ExpectedRewardPeriodValue => DateTime.Now.ToString("yyyyMM") + "01";
    private static string ExpectedRewardPeriodText =>
        $"{AlbanianMonths[DateTime.Now.Month]} {DateTime.Now.Year}";

    [Test]
    public void ShperblimLindjeGrandBiznes()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 6 hapa, hapi i pare aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(6));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));
        for (int i = 1; i < steps.Count; i++)
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert DRSSH ka opsionet e drejtorive");
        IWebElement drsshSelect = FindSelectByLabel("DRSSH");
        var drssh = new SelectElement(drsshSelect);
        Assert.That(drssh.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(drssh.Options.Count, Is.EqualTo(15));
        Assert.That(drssh.Options[1].GetAttribute("value"), Is.EqualTo("01"));
        Assert.That(drssh.Options[1].Text.Trim(), Is.EqualTo("Drejtoria Berat"));
        Assert.That(drssh.Options[2].GetAttribute("value"), Is.EqualTo("02"));
        Assert.That(drssh.Options[2].Text.Trim(), Is.EqualTo("Drejtoria Diber"));
        Assert.That(drssh.Options[3].GetAttribute("value"), Is.EqualTo("03"));
        Assert.That(drssh.Options[3].Text.Trim(), Is.EqualTo("Drejtoria Durres"));
        Assert.That(drssh.Options[4].GetAttribute("value"), Is.EqualTo("04"));
        Assert.That(drssh.Options[4].Text.Trim(), Is.EqualTo("Drejtoria Elbasan"));
        Assert.That(drssh.Options[5].GetAttribute("value"), Is.EqualTo("05"));
        Assert.That(drssh.Options[5].Text.Trim(), Is.EqualTo("Drejtoria Fier"));
        Assert.That(drssh.Options[6].GetAttribute("value"), Is.EqualTo("06"));
        Assert.That(drssh.Options[6].Text.Trim(), Is.EqualTo("Drejtoria Gjirokaster"));
        Assert.That(drssh.Options[7].GetAttribute("value"), Is.EqualTo("07"));
        Assert.That(drssh.Options[7].Text.Trim(), Is.EqualTo("Drejtoria Korçe"));
        Assert.That(drssh.Options[8].GetAttribute("value"), Is.EqualTo("08"));
        Assert.That(drssh.Options[8].Text.Trim(), Is.EqualTo("Drejtoria Kukes"));
        Assert.That(drssh.Options[9].GetAttribute("value"), Is.EqualTo("09"));
        Assert.That(drssh.Options[9].Text.Trim(), Is.EqualTo("Drejtoria Lezhe"));
        Assert.That(drssh.Options[10].GetAttribute("value"), Is.EqualTo("10"));
        Assert.That(drssh.Options[10].Text.Trim(), Is.EqualTo("Drejtoria Shkoder"));
        Assert.That(drssh.Options[11].GetAttribute("value"), Is.EqualTo("11"));
        Assert.That(drssh.Options[11].Text.Trim(), Is.EqualTo("Drejtoria Tirane"));
        Assert.That(drssh.Options[12].GetAttribute("value"), Is.EqualTo("12"));
        Assert.That(drssh.Options[12].Text.Trim(), Is.EqualTo("Drejtoria Vlore"));
        Assert.That(drssh.Options[13].GetAttribute("value"), Is.EqualTo("13"));
        Assert.That(drssh.Options[13].Text.Trim(), Is.EqualTo("Dega Tropoje"));
        Assert.That(drssh.Options[14].GetAttribute("value"), Is.EqualTo("14"));
        Assert.That(drssh.Options[14].Text.Trim(), Is.EqualTo("Dega Sarande"));

        Log("Assert AGJENCIA eshte disabled para zgjedhjes se DRSSH");
        IWebElement agencySelect = FindSelectByLabel("AGJENCIA");
        Assert.That(agencySelect.Enabled, Is.False);
        Assert.That(new SelectElement(agencySelect).Options.Count, Is.EqualTo(1));
        Assert.That(new SelectElement(agencySelect).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Kliko Vazhdo pa zgjedhur DRSSH dhe AGJENCIA");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error message per DRSSH");
        IWebElement drsshError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[@id='root']//form//label[contains(.,'DRSSH')]/following::*[contains(@class,'text-danger')][1]")));
        Assert.That(drsshError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));

        Log("Assert error message per AGJENCIA");
        IWebElement agencyError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[@id='root']//form//label[contains(.,'AGJENCIA')]/following::*[contains(@class,'text-danger')][1]")));
        Assert.That(agencyError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));

        Log("Zgjidh Drejtoria Tirane");
        SelectDropdownByValue(FindSelectByLabel("DRSSH"), "11");

        Log("Wait qe AGJENCIA te aktivizohet");
        wait.Until(d =>
        {
            try
            {
                var agency = d.FindElement(
                    By.XPath("//div[@id='root']//form//label[contains(.,'AGJENCIA')]/following-sibling::select"));
                return agency.Enabled && new SelectElement(agency).Options.Count > 1;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        IWebElement agencyEnabled = FindSelectByLabel("AGJENCIA");
        var agencyOptions = new SelectElement(agencyEnabled);
        Assert.That(agencyEnabled.Enabled, Is.True);
        Assert.That(agencyOptions.Options.Count, Is.GreaterThan(1));

        Log("Zgjidh agjencine e pare te disponueshme");
        IWebElement? kavajeOption = null;
        foreach (var option in agencyOptions.Options)
        {
            if (option.Text.IndexOf("Kavaj", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                kavajeOption = option;
                break;
            }
        }

        if (kavajeOption != null)
            agencyOptions.SelectByValue(kavajeOption.GetAttribute("value"));
        else
            agencyOptions.SelectByIndex(1);
        Thread.Sleep(500);

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("INFORMACION MBI SUBJEKTIN");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("INFORMACION MBI SUBJEKTIN"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 6 hapa, dy te paret aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(6));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        for (int i = 2; i < steps.Count; i++)
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert label-at e informacionit te subjektit");
        Assert.That(driver.FindElement(
            By.XPath("//div[@id='root']//form//label[normalize-space()='NIPT']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[@id='root']//form//label[normalize-space()='Emri i subjektit']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[@id='root']//form//label[normalize-space()='Email']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[@id='root']//form//label[normalize-space()='Përfaqësuesi ligjor']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[@id='root']//form//label[normalize-space()='Statusi']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[@id='root']//form//label[normalize-space()='Nr. tel']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[@id='root']//form//label[normalize-space()='Adresa']")).Displayed, Is.True);

        Log("Assert te dhenat e subjektit te para-plotesuara");
        AssertReadonlyValue("NIPT", "M53330201S");
        AssertReadonlyValue("Emri i subjektit", "Migen Dërstila");
        AssertReadonlyValue("Email", "migen.derstila@kreatx.com");
        AssertReadonlyValue("Përfaqësuesi ligjor", "Migen  Luan  Dërstila |");
        AssertReadonlyValue("Statusi", "Aktiv");
        AssertReadonlyValue("Nr. tel", "+355684053531");

        IWebElement adresa = FindInputByLabel("Adresa");
        Assert.That(adresa.GetAttribute("value").Trim(),
            Is.EqualTo("Derstile; ; ; ; Gjinar; ; 0000; Elbasan,Elbasan,ELBASAN,Elbasan"));
        Assert.That(adresa.GetAttribute("disabled"), Is.Not.Null);

        Log("Assert butonat e navigimit Step 2");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("INFORMACION MBI PËRFITUESIN");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("INFORMACION MBI PËRFITUESIN"));

        Log("Assert kohëzgjatja Step 3");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 6 hapa, tre te paret aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(6));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Contain("active"));
        for (int i = 3; i < steps.Count; i++)
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert NID eshte i editueshem dhe bosh");
        IWebElement nidInput = FindInputByLabel("NID");
        Assert.That(nidInput.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(IsReadonly(nidInput), Is.False);
        Assert.That(nidInput.Enabled, Is.True);

        Log("Assert fushat e gjendjes civile jane readonly dhe bosh");
        IWebElement emri = FindInputByLabel("Emri");
        Assert.That(emri.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(emri.GetAttribute("readonly"), Is.Not.Null);

        IWebElement mbiemri = FindInputByLabel("Mbiemri");
        Assert.That(mbiemri.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(mbiemri.GetAttribute("readonly"), Is.Not.Null);

        IWebElement atesia = FindInputByLabel("Atësia");
        Assert.That(atesia.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(atesia.GetAttribute("readonly"), Is.Not.Null);

        IWebElement datelindja = FindInputByLabel("Datëlindja");
        Assert.That(datelindja.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(datelindja.GetAttribute("readonly"), Is.Not.Null);

        Log("Assert gjinia Mashkull/Femer eshte disabled dhe e pazgjedhur");
        IWebElement maleRadio = wait.Until(ExpectedConditions.ElementExists(By.Id("genderMale")));
        IWebElement femaleRadio = wait.Until(ExpectedConditions.ElementExists(By.Id("genderFemale")));
        Assert.That(maleRadio.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(maleRadio.GetAttribute("name"), Is.EqualTo("gender"));
        Assert.That(maleRadio.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(maleRadio.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(maleRadio.Selected, Is.False);
        Assert.That(femaleRadio.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(femaleRadio.GetAttribute("name"), Is.EqualTo("gender"));
        Assert.That(femaleRadio.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(femaleRadio.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(femaleRadio.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='genderMale'] span")).Text.Trim(),
            Is.EqualTo("Mashkull"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='genderFemale'] span")).Text.Trim(),
            Is.EqualTo("Femër"));

        Log("Assert Nr. Cel, Email dhe Numri i kontrates se punes jane bosh");
        IWebElement nrCel = FindInputByLabel("Nr. Cel");
        IWebElement emailPerfituesi = FindInputByLabel("Email *");
        IWebElement nrKontrate = FindInputByLabel("Numri i kontratës së punës");
        Assert.That(nrCel.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(IsReadonly(nrCel), Is.False);
        Assert.That(emailPerfituesi.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(IsReadonly(emailPerfituesi), Is.False);
        Assert.That(nrKontrate.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(IsReadonly(nrKontrate), Is.False);

        Log("Assert Adresa eshte disabled dhe bosh");
        IWebElement adresaPerfituesi = FindInputByLabel("Adresa");
        Assert.That(adresaPerfituesi.TagName.ToLowerInvariant(), Is.EqualTo("textarea"));
        Assert.That(adresaPerfituesi.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(adresaPerfituesi.GetAttribute("disabled"), Is.Not.Null);

        Log("Assert butonat e navigimit Step 3");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error message per Nr. Cel");
        IWebElement nrCelError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[@id='root']//form//label[contains(.,'Nr. Cel')]/following::*[contains(@class,'text-danger')][1]")));
        Assert.That(nrCelError.Text.Trim(), Is.EqualTo("Plotësoni fushën për të vazhduar"));

        Log("Assert error message per Email");
        IWebElement emailError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[@id='root']//form//label[contains(.,'Email')]/following::*[contains(@class,'text-danger')][1]")));
        Assert.That(emailError.Text.Trim(), Is.EqualTo("Plotësoni fushën për të vazhduar"));

        Log("Ploteso NID e perfituesit");
        FillInput(FindInputByLabel("NID"), "J55728107R");

        Log("Wait qe te dhenat e gjendjes civile te ngarkohen");
        wait.Until(d =>
        {
            try
            {
                var name = d.FindElement(
                    By.XPath("//div[@id='root']//form//label[normalize-space()='Emri']/following-sibling::input"));
                return name.GetAttribute("value").Trim() == "Ketjona";
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        Log("Assert te dhenat e gjendjes civile te para-plotesuara");
        Assert.That(FindInputByLabel("NID").GetAttribute("value").Trim(), Is.EqualTo("J55728107R"));
        Assert.That(FindInputByLabel("Emri").GetAttribute("value").Trim(), Is.EqualTo("Ketjona"));
        Assert.That(FindInputByLabel("Mbiemri").GetAttribute("value").Trim(), Is.EqualTo("Mema"));
        Assert.That(FindInputByLabel("Atësia").GetAttribute("value").Trim(), Is.EqualTo("Mersin"));
        Assert.That(FindInputByLabel("Datëlindja").GetAttribute("value").Trim(), Is.EqualTo("28.07.1995"));
        Assert.That(driver.FindElement(By.Id("genderFemale")).Selected, Is.True);
        Assert.That(driver.FindElement(By.Id("genderMale")).Selected, Is.False);
        Assert.That(FindInputByLabel("Adresa").GetAttribute("value").Trim(), Is.Not.Empty);

        Log("Ploteso Nr. Cel, Email dhe Numri i kontrates se punes");
        FillInput(FindInputByLabel("Nr. Cel"), "0676041404");
        FillInput(FindInputByLabel("Email *"), "test@test.com");
        FillInput(FindInputByLabel("Numri i kontratës së punës"), "1");

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 4 Title");
        IWebElement Step4Title = WaitForStepTitle("INFORMACION MBI FËMIJËT");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("INFORMACION MBI FËMIJËT"));

        Log("Assert kohëzgjatja Step 4");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 6 hapa, kater te paret aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(6));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[3].GetAttribute("class"), Does.Contain("active"));
        for (int i = 4; i < steps.Count; i++)
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert Shperblimi kerkohet");
        IWebElement shperblimiLabel = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[@id='root']//form//label[normalize-space()='Shpërblimi kërkohet:']")));
        Assert.That(shperblimiLabel.Displayed, Is.True);

        IWebElement beforeBirth = wait.Until(ExpectedConditions.ElementExists(By.Id("beforeBirth")));
        IWebElement afterBirth = wait.Until(ExpectedConditions.ElementExists(By.Id("afterBirth")));

        Assert.That(beforeBirth.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(beforeBirth.GetAttribute("name"), Is.EqualTo("shperblimi"));
        Assert.That(beforeBirth.Selected, Is.True);

        Assert.That(afterBirth.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(afterBirth.GetAttribute("name"), Is.EqualTo("shperblimi"));
        Assert.That(afterBirth.Selected, Is.False);

        Assert.That(driver.FindElement(By.CssSelector("label[for='beforeBirth']")).Text.Trim(),
            Is.EqualTo("Para lindjes"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='afterBirth']")).Text.Trim(),
            Is.EqualTo("Pas lindjes"));

        Log("Assert butonat e navigimit Step 4");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Kliko Vazhdo Step 4 me Para lindjes");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 5 Title");
        IWebElement Step5Title = WaitForStepTitle("INFORMACION MBI MËNYRËN E PAGESËS");
        Assert.That(Step5Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("INFORMACION MBI MËNYRËN E PAGESËS"));

        Log("Assert kohëzgjatja Step 5");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 6 hapa, pese te paret aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(6));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[3].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[4].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[5].GetAttribute("class"), Does.Not.Contain("active"));
        Assert.That(steps[5].GetAttribute("class"), Does.Contain("no-click"));

        Log("Wait qe periudha e shperblimit te ngarkohet");
        wait.Until(d =>
        {
            try
            {
                var select = d.FindElement(By.CssSelector("#root form select[name='selectedPeriod']"));
                var options = new SelectElement(select).Options;
                return options.Count > 1
                    && options[1].GetAttribute("value") == ExpectedRewardPeriodValue;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        Log("Assert Periudha e shperblimit eshte disabled dhe e para-zgjedhur "
            + ExpectedRewardPeriodText);
        IWebElement periodSelect = FindSelectByName("selectedPeriod");
        var period = new SelectElement(periodSelect);
        Assert.That(periodSelect.Enabled, Is.False);
        Assert.That(period.Options.Count, Is.EqualTo(2));
        Assert.That(period.Options[1].GetAttribute("value"), Is.EqualTo(ExpectedRewardPeriodValue));
        Assert.That(period.Options[1].Text.Trim(), Is.EqualTo(ExpectedRewardPeriodText));

        string selectedPeriod = InputValue(periodSelect);
        if (string.IsNullOrEmpty(selectedPeriod))
            selectedPeriod = period.Options[1].GetAttribute("value");
        Assert.That(selectedPeriod, Is.EqualTo(ExpectedRewardPeriodValue));
        Assert.That(driver.FindElement(By.CssSelector("label[for='selectedPeriod']")).Text.Trim(),
            Is.EqualTo("Periudha e shpërblimit"));

        Log("Assert menyra e pageses eshte e pazgjedhur dhe fushat disabled");
        IWebElement paymentLabel = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[@id='root']//form//label[contains(.,'Kërkoj që të ardhurat t’i tërheq pranë')]")));
        Assert.That(paymentLabel.Text.Trim(),
            Is.EqualTo("1. Kërkoj që të ardhurat t’i tërheq pranë:"));

        IWebElement postalRadio = wait.Until(ExpectedConditions.ElementExists(By.Id("postalPayment")));
        IWebElement bankRadio = wait.Until(ExpectedConditions.ElementExists(By.Id("bankPayment")));
        Assert.That(postalRadio.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(postalRadio.GetAttribute("name"), Is.EqualTo("paymentMethod"));
        Assert.That(postalRadio.Selected, Is.False);
        Assert.That(bankRadio.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(bankRadio.GetAttribute("name"), Is.EqualTo("paymentMethod"));
        Assert.That(bankRadio.Selected, Is.False);

        Assert.That(driver.FindElement(By.CssSelector("label[for='postalPayment']")).Text.Trim(),
            Is.EqualTo("Qendrës Paguese"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='bankPayment']")).Text.Trim(),
            Is.EqualTo("Bankës"));
        Assert.That(driver.FindElement(
            By.XPath("//div[@id='root']//form//span[normalize-space()='të postës shqiptare.']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[@id='root']//form//span[normalize-space()='me numër llogarie']")).Displayed, Is.True);

        IWebElement postalSelect = FindPaymentSelect("postalPayment");
        IWebElement bankSelect = FindPaymentSelect("bankPayment");
        IWebElement bankAccount = FindInputByName("accountNumber");
        Assert.That(postalSelect.Enabled, Is.False);
        Assert.That(new SelectElement(postalSelect).Options.Count, Is.EqualTo(1));
        Assert.That(bankSelect.Enabled, Is.False);
        Assert.That(new SelectElement(bankSelect).Options.Count, Is.EqualTo(1));
        Assert.That(bankAccount.Enabled, Is.False);
        Assert.That(bankAccount.GetAttribute("value"), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 5");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Kliko Vazhdo pa zgjedhur menyren e pageses");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error message per menyren e pageses");
        IWebElement paymentError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[@id='root']//*[contains(text(),'Zgjidhni të paktën një lloj pagese')]")));
        Assert.That(paymentError.Text.Trim(), Is.EqualTo("Zgjidhni të paktën një lloj pagese"));

        Log("Zgjidh terheqjen ne Qendren Paguese");
        SelectRadioById("postalPayment");
        Assert.That(driver.FindElement(By.Id("postalPayment")).Selected, Is.True);
        Assert.That(driver.FindElement(By.Id("bankPayment")).Selected, Is.False);

        Log("Wait qe dega e postes te aktivizohet");
        wait.Until(d =>
        {
            try
            {
                var postSelect = d.FindElement(
                    By.XPath("//input[@id='postalPayment']/ancestor::div[contains(@class,'row')][1]//select"));
                return postSelect.Enabled && new SelectElement(postSelect).Options.Count > 1;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        postalSelect = FindPaymentSelect("postalPayment");
        var postalOptions = new SelectElement(postalSelect);
        Assert.That(postalSelect.Enabled, Is.True);
        Assert.That(FindPaymentSelect("bankPayment").Enabled, Is.False);
        Assert.That(FindInputByName("accountNumber").Enabled, Is.False);

        Log("Zgjidh posten Kavaje nese ekziston, perndryshe opsionin e pare");
        IWebElement? postalKavaje = null;
        foreach (var option in postalOptions.Options)
        {
            if (option.Text.IndexOf("Kavaj", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                postalKavaje = option;
                break;
            }
        }

        if (postalKavaje != null)
            postalOptions.SelectByValue(postalKavaje.GetAttribute("value"));
        else
            postalOptions.SelectByIndex(1);
        Thread.Sleep(500);

        Log("Kliko Vazhdo Step 5");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 6 Title");
        IWebElement Step6Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step6Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 6");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 6 hapa, te gjithe aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(6));
        foreach (var step in steps)
        {
            Assert.That(step.GetAttribute("class"), Does.Contain("active"));
            Assert.That(step.GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumenta që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumenta që ngarkohen nga nëpunësi i administratës publike')]")).Displayed,
            Is.True);

        Log("Assert document-upload Raport mjekesor per barrelindje");
        AssertDocumentUpload("fuRaportBarrelindjeUpload", "Raport mjekësor për barrëlindje");

        Log("Assert document-upload Te tjera");
        AssertDocumentUpload("fuOthersUpload", "Të tjera");

        Log("Assert dokumentet e administrates publike");
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Vërtetim i derdhjes së kontributit si i/e vetëpunësuar')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Listë pagesa e përfitimeve afatshkurtra')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Certifikatë familjare')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Certifikatë lindjeje/shpërblimi')]")).Displayed, Is.True);

        Log("Ngarko Raport mjekesor per barrelindje");
        UploadDocument(
            "fuRaportBarrelindjeUpload",
            @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf");

        Log("Assert checkbox i pranimit eshte i pazgjedhur");
        IWebElement agreeCheck = wait.Until(ExpectedConditions.ElementExists(By.Id("agreeCheck")));
        Assert.That(agreeCheck.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='agreeCheck']")).Text.Trim(),
            Does.Contain("Me klikimin e këtij butoni, ju bini dakord që këto dokumente të sigurohen për ju nga nëpunësi i administratës."));

        Log("Kliko Dergo pa pranuar kushtet");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(1000);
        Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase"))).Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DOKUMENTACIONI"));

        Log("Zgjidh pranimin e kushteve");
        SafeClick(By.Id("agreeCheck"));
        Assert.That(driver.FindElement(By.Id("agreeCheck")).Selected, Is.True);

        Log("Assert butoni Dergo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.Text.Trim(), Does.Contain("Dërgo"));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        //Log("Kliko Dergo");
        //SafeClick(By.CssSelector("button.ealb-btn-continue"));
        //Thread.Sleep(5000);

        //Log("Assert suksesi");
        //IWebElement successTitle = wait.Until(ExpectedConditions.ElementIsVisible(
        //    By.XPath("//h5[contains(.,'APLIKIMI JUAJ')]")));
        //Assert.That(successTitle.Text.Trim().ToUpperInvariant().Replace("Ë", "E"),
        //    Does.Contain("APLIKIMI JUAJ U DERGUA ME SUKSES"));

        //IWebElement referenceNumber = wait.Until(ExpectedConditions.ElementIsVisible(
        //    By.XPath("//h6[contains(.,'Numri referencë i aplikimit')]")));
        //Assert.That(referenceNumber.Text, Does.Contain("10059-"));
        //Assert.That(driver.Url, Does.Contain("/mesazh"));

        Log("TEST PASSED");
    }

    private bool IsReadonly(IWebElement element)
    {
        object? result = ((IJavaScriptExecutor)driver).ExecuteScript(
            "return arguments[0].readOnly === true || arguments[0].hasAttribute('readonly');",
            element);
        return Equals(result, true);
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
        DismissCookieBannerIfPresent();
    }

    private IWebElement FindSelectByLabel(string labelPart)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//div[@id='root']//form//label[contains(.,'{labelPart}')]/following-sibling::select")));
    }

    private IWebElement FindInputByLabel(string labelPart)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//div[@id='root']//form//label[contains(.,'{labelPart}')]/following-sibling::*[self::input or self::textarea]")));
    }

    private IWebElement WaitForStepTitle(string expectedUpper)
    {

        return wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4.text-uppercase, h4.ealb-header-text"));
            foreach (var title in titles)
            {
                if (title.Text.Trim().ToUpperInvariant() == expectedUpper)
                    return title;
            }
            return null;
        });
    }

    private void AssertReadonlyValue(string labelPart, string expectedValue)
    {

        IWebElement input = FindInputByLabel(labelPart);
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(input.GetAttribute("readonly") ?? input.GetAttribute("disabled"), Is.Not.Null);
    }

    private void SelectDropdownByValue(IWebElement select, string value)
    {

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            select
        );

        Thread.Sleep(300);
        new SelectElement(select).SelectByValue(value);
        Thread.Sleep(500);
    }

    private void BlurActiveElement()
    {

        try
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "if(document.activeElement){document.activeElement.blur();}"
            );
        }
        catch (Exception ex)
        {
            Log("BlurActiveElement error: " + ex.Message);
        }
    }

    private void FillInput(IWebElement input, string value)
    {

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            input
        );

        Thread.Sleep(400);

        try
        {
            input.Click();
            Thread.Sleep(200);
            input.Clear();
            input.SendKeys(value);
        }
        catch (ElementClickInterceptedException)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].focus(); arguments[0].value = '';",
                input
            );
            input.SendKeys(value);
        }

        BlurActiveElement();
        Thread.Sleep(300);
    }

    private void SelectRadioById(string radioId)
    {

        SafeClick(By.Id(radioId));
        Thread.Sleep(500);
    }

    private IWebElement FindSelectByName(string name)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector($"#root form select[name='{name}']")));
    }

    private IWebElement FindInputByName(string name)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector($"#root form input[name='{name}']")));
    }

    private IWebElement FindPaymentSelect(string radioId)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//input[@id='{radioId}']/ancestor::div[contains(@class,'row')][1]//select")));
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {

        Assert.That(driver.FindElement(
            By.XPath($"//span[contains(normalize-space(),'{documentTitle}')]")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-10059"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("25"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("25"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf,.jpg,.jpeg,.png"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të ngarkuar dokumentin"));

        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të ngarkuar dokumentin"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='hint']")).Text.Trim(),
            Is.EqualTo("Formatet e lejuara: PDF, JPG, JPEG, PNG. Madhësia maksimale: 25MB."));
    }

    private void UploadDocument(string uploadId, string filePath)
    {

        Assert.That(File.Exists(filePath), Is.True, "File nuk ekziston: " + filePath);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        ISearchContext shadow = docUpload.GetShadowRoot();
        IWebElement fileInput = shadow.FindElement(By.CssSelector("[data-role='file-input']"));
        fileInput.SendKeys(filePath);

        var uploadWait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
        uploadWait.Until(d =>
        {
            try
            {
                var root = d.FindElement(By.Id(uploadId)).GetShadowRoot();
                var fileRow = root.FindElement(By.CssSelector("[data-role='single-file']"));
                string cssClass = fileRow.GetAttribute("class") ?? string.Empty;
                string fileName = root.FindElement(By.CssSelector("[data-role='sf-name']")).Text.Trim();
                return cssClass.Contains("completed") || fileName.Length > 0;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });
    }
}