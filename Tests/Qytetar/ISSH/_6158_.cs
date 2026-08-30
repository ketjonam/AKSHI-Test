using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("6158")]
public class _6158_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "6158";
    protected override string? ServiceTitle => "AplikimPerPensionFamiljar";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void AplikimPerPensionFamiljar()
    {




        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("KËRKESE PËR CAKTIM PENSIONI FAMILJAR"));

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("5 minuta kohëzgjatje"));

        Log("Assert 7 hapa, hapi i pare aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(7));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        for (int i = 1; i < steps.Count; i++)
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));

        Log("Assert Nr eshte readonly dhe i para-plotesuar");
        IWebElement nrInput = FindInputByLabel("Nr.");
        Assert.That(nrInput.GetAttribute("value"), Is.Not.Empty);
        Assert.That(nrInput.GetAttribute("readonly"), Is.Not.Null);

        Log("Assert Regj.Date eshte readonly dhe e dites se sotme");
        IWebElement dateInput = FindInputByLabel("Regj.Datë");
        Assert.That(dateInput.GetAttribute("value").Trim(),
            Is.EqualTo(DateTime.Now.ToString("dd/MM/yyyy")));
        Assert.That(dateInput.GetAttribute("readonly"), Is.Not.Null);

        Log("Assert DRSSH ka opsionet e drejtorive");
        IWebElement drsshSelect = wait.Until(ExpectedConditions.ElementExists(By.Id("drssh")));
        var drssh = new SelectElement(drsshSelect);
        Assert.That(drssh.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(drssh.Options.Count, Is.EqualTo(15));
        Assert.That(drssh.Options[1].GetAttribute("value"), Is.EqualTo("01"));
        Assert.That(drssh.Options[1].Text.Trim(), Is.EqualTo("Drejtoria Berat"));
        Assert.That(drssh.Options[11].GetAttribute("value"), Is.EqualTo("11"));
        Assert.That(drssh.Options[11].Text.Trim(), Is.EqualTo("Drejtoria Tirane"));
        Assert.That(drssh.Options[13].Text.Trim(), Is.EqualTo("Dega Tropoje"));
        Assert.That(drssh.Options[14].Text.Trim(), Is.EqualTo("Dega Sarande"));

        Log("Assert ALSSH eshte disabled para zgjedhjes se DRSSH");
        IWebElement alsshSelect = wait.Until(ExpectedConditions.ElementExists(By.Id("agency")));
        Assert.That(alsshSelect.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(new SelectElement(alsshSelect).Options.Count, Is.EqualTo(1));

        Log("Kliko Vazhdo pa zgjedhur DRSSH dhe ALSSH");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error message per DRSSH");
        IWebElement drsshError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//label[contains(.,'DRSSH')]/following::span[contains(@class,'text-danger')][1]")));
        Assert.That(drsshError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));

        Log("Assert error message per ALSSH");
        IWebElement alsshError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//label[contains(.,'ALSSH')]/following::span[contains(@class,'text-danger')][1]")));
        Assert.That(alsshError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));

        Log("Zgjidh Drejtoria Tirane");
        SelectDropdownByValue(wait.Until(ExpectedConditions.ElementExists(By.Id("drssh"))), "11");

        Log("Wait qe ALSSH te aktivizohet");
        wait.Until(d =>
        {
            try
            {
                var agency = d.FindElement(By.Id("agency"));
                return agency.GetAttribute("disabled") == null
                    && new SelectElement(agency).Options.Count > 1;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        IWebElement alsshEnabled = driver.FindElement(By.Id("agency"));
        var alsshOptions = new SelectElement(alsshEnabled);
        Assert.That(alsshEnabled.GetAttribute("disabled"), Is.Null);
        Assert.That(alsshOptions.Options.Count, Is.GreaterThan(1));

        Log("Zgjidh ALSSH Kavaje nese ekziston, perndryshe opsionin e pare");
        IWebElement? kavajeOption = null;
        foreach (var option in alsshOptions.Options)
        {
            if (option.Text.IndexOf("Kavaj", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                kavajeOption = option;
                break;
            }
        }

        if (kavajeOption != null)
            alsshOptions.SelectByValue(kavajeOption.GetAttribute("value"));
        else
            alsshOptions.SelectByIndex(1);
        Thread.Sleep(500);

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4.text-uppercase"));
            if (titles.Count == 0)
                return null;
            return titles[0].Text.Trim().ToUpperInvariant()
                == "TË DHËNA PERSONALE TË KËRKUESIT/KËRKUESVE"
                ? titles[0]
                : null;
        });
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNA PERSONALE TË KËRKUESIT/KËRKUESVE"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("5 minuta kohëzgjatje"));

        Log("Assert 7 hapa, dy hapat e pare aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(7));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        for (int i = 2; i < steps.Count; i++)
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));

        Log("Assert kolonat e tabeles se kerkuesve");
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='NID']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Emër']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Atësi']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Mbiemër']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Datëlindja']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Lidhja me të ndjerin']")).Displayed, Is.True);

        Log("Assert tabela e kerkuesve eshte bosh");
        Assert.That(driver.FindElements(By.CssSelector("table tbody tr")).Count, Is.EqualTo(0));

        Log("Assert butoni SHTO KERKUES");
        IWebElement shtoKerkuesBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//button[contains(.,'SHTO KËRKUES')]")));
        Assert.That(shtoKerkuesBtn.Displayed, Is.True);

        Log("Assert seksioni Adresa");
        Assert.That(driver.FindElement(
            By.XPath("//h4[contains(@class,'text-uppercase') and contains(.,'Adresa')]")).Displayed, Is.True);

        Log("Assert fushat e adreses");
        IWebElement lagjia = FindInputByLabel("Lagjia");
        IWebElement pallati = FindInputByLabel("Pallati");
        IWebElement shkalla = FindInputByLabel("Shkalla");
        IWebElement qyteti = FindInputByLabel("Qyteti");
        IWebElement fshati = FindInputByLabel("Fshati");
        IWebElement qarku = FindInputByLabel("Qarku");
        IWebElement rrethi = FindInputByLabel("Rrethi");
        IWebElement nrTel = FindInputByLabel("Nr.tel:");
        IWebElement nrTel2 = FindInputByLabel("Nr.tel 2");
        IWebElement email = FindInputByLabel("Email");
        IWebElement rruga = FindTextareaByLabel("Rruga");

        Assert.That(lagjia.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(lagjia.GetAttribute("maxlength"), Is.EqualTo("100"));
        Assert.That(pallati.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(pallati.GetAttribute("maxlength"), Is.EqualTo("12"));
        Assert.That(shkalla.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(shkalla.GetAttribute("maxlength"), Is.EqualTo("12"));
        Assert.That(qyteti.GetAttribute("value").Trim(), Is.EqualTo("KAVAJË"));
        Assert.That(qyteti.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(fshati.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(fshati.GetAttribute("maxlength"), Is.EqualTo("100"));
        Assert.That(qarku.GetAttribute("value").Trim(), Is.EqualTo("TIRANË"));
        Assert.That(qarku.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(rrethi.GetAttribute("value").Trim(), Is.EqualTo("KAVAJË"));
        Assert.That(rrethi.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(nrTel.GetAttribute("value").Trim(), Is.EqualTo("0676041404"));
        Assert.That(nrTel.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(nrTel2.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(email.GetAttribute("value").Trim(), Is.EqualTo("ketjona.mema@kreatx.com"));
        Assert.That(email.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(rruga.GetAttribute("value").Trim(), Does.Contain("KAVAJË"));
        Assert.That(rruga.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(rruga.GetAttribute("disabled"), Is.Not.Null);

        Log("Kliko Vazhdo pa shtuar kerkues");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(1000);
        Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase"))).Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNA PERSONALE TË KËRKUESIT/KËRKUESVE"));

        Log("Kliko + SHTO KERKUES");
        SafeClick(By.XPath("//button[contains(.,'SHTO KËRKUES')]"));
        WaitForModalTitle("Shto kërkues");

        Log("Assert fushat e modalit jane bosh dhe te disabled pervec NID");
        IWebElement modalNid = FindModalInput("NID");
        IWebElement modalEmer = FindModalInput("Emër");
        IWebElement modalAtesi = FindModalInput("Atësi");
        IWebElement modalMbiemer = FindModalInput("Mbiemër");
        IWebElement modalDatelindja = FindModalInput("Datëlindja");
        IWebElement modalLidhja = FindModalSelect("Lidhja me të ndjerin");
        Assert.That(modalNid.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(modalEmer.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(modalAtesi.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(modalMbiemer.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(modalDatelindja.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(modalLidhja.GetAttribute("disabled"), Is.Not.Null);

        Log("Assert opsionet e lidhjes me te ndjerin");
        var lidhjaOptions = new SelectElement(modalLidhja);
        Assert.That(lidhjaOptions.Options.Count, Is.EqualTo(12));
        Assert.That(lidhjaOptions.Options[0].GetAttribute("value"), Is.EqualTo("I biri"));
        Assert.That(lidhjaOptions.Options[1].GetAttribute("value"), Is.EqualTo("E bija"));
        Assert.That(lidhjaOptions.Options[2].GetAttribute("value"), Is.EqualTo("I veu"));
        Assert.That(lidhjaOptions.Options[3].GetAttribute("value"), Is.EqualTo("E veja"));
        Assert.That(lidhjaOptions.Options[11].GetAttribute("value"), Is.EqualTo("Njerka"));

        Log("Kliko Anullo ne modal");
        ClickModalFooter("Anullo");
        WaitForModalClosed();

        Log("Kliko + SHTO KERKUES perseri");
        SafeClick(By.XPath("//button[contains(.,'SHTO KËRKUES')]"));
        WaitForModalTitle("Shto kërkues");

        Log("Kliko Ruaj pa plotesuar NID");
        ClickModalFooter("Ruaj");
        AssertModalFieldInvalid(FindModalInput("NID"));

        Log("Ploteso NID te kerkuesit");
        FillInput(FindModalInput("NID"), Settings.Qytetar.Username);

        Log("Wait qe te dhenat personale te ngarkohen dhe lidhja te aktivizohet");
        wait.Until(d =>
        {
            try
            {
                var emer = d.FindElement(
                    By.XPath("//div[contains(@class,'custom-modal-content')]//label[contains(.,'Emër')]/following::input[not(@type='hidden')][1]"));
                var lidhja = d.FindElement(
                    By.XPath("//div[contains(@class,'custom-modal-content')]//label[contains(.,'Lidhja me të ndjerin')]/following::select[1]"));
                return !string.IsNullOrWhiteSpace(emer.GetAttribute("value"))
                    && lidhja.GetAttribute("disabled") == null;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        Log("Assert te dhenat personale te para-plotesuara");
        Assert.That(FindModalInput("NID").GetAttribute("value").Trim(), Is.EqualTo(Settings.Qytetar.Username));
        Assert.That(FindModalInput("Emër").GetAttribute("value").Trim(), Is.EqualTo("Ketjona"));
        Assert.That(FindModalInput("Atësi").GetAttribute("value").Trim(), Is.EqualTo("Mersin"));
        Assert.That(FindModalInput("Mbiemër").GetAttribute("value").Trim(), Is.EqualTo("Mema"));
        Assert.That(FindModalInput("Datëlindja").GetAttribute("value").Trim(),
            Does.Contain("28.07.1995").Or.Contain("28/07/1995"));

        Log("Zgjidh lidhjen me te ndjerin E bija");
        SelectDropdownByValue(FindModalSelect("Lidhja me të ndjerin"), "E bija");

        Log("Kliko Ruaj kerkuesin");
        ClickModalFooter("Ruaj");
        WaitForModalClosed();
        Thread.Sleep(1000);

        Log("Assert rreshti i kerkuesit ne tabele");
        IWebElement kerkuesRow = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("table tbody tr")));
        var kerkuesCells = kerkuesRow.FindElements(By.TagName("td"));
        Assert.That(kerkuesCells[0].Text.Trim(), Is.EqualTo(Settings.Qytetar.Username));
        Assert.That(kerkuesCells[1].Text.Trim(), Is.EqualTo("Ketjona"));
        Assert.That(kerkuesCells[2].Text.Trim(), Is.EqualTo("Mersin"));
        Assert.That(kerkuesCells[3].Text.Trim(), Is.EqualTo("Mema"));
        Assert.That(kerkuesCells[4].Text.Trim(), Does.Contain("28.07.1995").Or.Contain("28/07/1995"));
        Assert.That(kerkuesCells[5].Text.Trim(), Is.EqualTo("E bija"));

        Log("Ploteso Adresa");
        FillInput(FindInputByLabel("Lagjia"), "1");
        FillInput(FindInputByLabel("Pallati"), "1");
        FillInput(FindInputByLabel("Shkalla"), "2");
        FillInput(FindInputByLabel("Fshati"), "Kavajë");
        FillInput(FindInputByLabel("Nr.tel 2"), "0676041404");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4.text-uppercase"));
            if (titles.Count == 0)
                return null;
            return titles[0].Text.Trim().ToUpperInvariant() == "TË DHËNA PERSONALE TË TË NDJERIT"
                ? titles[0]
                : null;
        });
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNA PERSONALE TË TË NDJERIT"));

        Log("Assert kohëzgjatja Step 3");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("5 minuta kohëzgjatje"));

        Log("Assert 7 hapa, tre hapat e pare aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(7));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Contain("active"));
        for (int i = 3; i < steps.Count; i++)
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));

        Log("Assert fushat e te ndjerit jane bosh");
        IWebElement deceasedNid = FindInputAfterLabel("NID");
        IWebElement deceasedEmer = FindInputAfterLabel("Emër");
        IWebElement deceasedMbiemer = FindInputAfterLabel("Mbiemër");
        IWebElement deceasedDeathDate = FindInputAfterLabel("Datë vdekjeje");
        Assert.That(deceasedNid.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(deceasedNid.GetAttribute("maxlength"), Is.EqualTo("10"));
        Assert.That(deceasedEmer.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(deceasedMbiemer.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(deceasedDeathDate.GetAttribute("value"), Is.EqualTo(string.Empty));

        Log("Vendos NID J35413056V per te ndjerin");
        SetReactInputValue(FindInputAfterLabel("NID"), Settings.Qytetar.Username);

        Log("Assert popup Gabim nga Gjendja Civile");
        IWebElement alertTitle = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h2.alert-modal-title")));
        Assert.That(alertTitle.Text.Trim(), Is.EqualTo("Gabim"));

        IWebElement alertMsg = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector(".alert-modal-description")));
        Assert.That(alertMsg.Text.Trim(),
            Is.EqualTo("Nuk u arrit të merren të dhënat nga Gjendja Civile. Ju lutemi provoni përsëri më vonë."));

        Log("Kliko OK ne popup");
        DismissAlertModal();

        Log("Vendos NID F90520294G per te ndjerin");
        SetReactInputValue(FindInputAfterLabel("NID"), "F90520294G");
        Assert.That(FindInputAfterLabel("NID").GetAttribute("value").Trim(), Is.EqualTo("F90520294G"));

        Log("Wait qe emri dhe mbiemri i te ndjerit te ngarkohen ose popup Gabim");
        WebDriverWait waitLookup = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        waitLookup.Until(d =>
        {
            try
            {
                var alerts = d.FindElements(By.CssSelector("h2.alert-modal-title"));
                if (alerts.Count > 0 && alerts[0].Displayed)
                    return true;

                var emer = d.FindElement(
                    By.XPath("//div[@id='root']//label[contains(.,'Emër')]/following::input[not(@type='hidden')][1]"));
                var mbiemer = d.FindElement(
                    By.XPath("//div[@id='root']//label[contains(.,'Mbiemër')]/following::input[not(@type='hidden')][1]"));
                return !string.IsNullOrWhiteSpace(emer.GetAttribute("value"))
                    && !string.IsNullOrWhiteSpace(mbiemer.GetAttribute("value"));
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        var gabimPopup = driver.FindElements(By.CssSelector("h2.alert-modal-title"));
        if (gabimPopup.Count > 0 && gabimPopup[0].Displayed)
        {
            Log("Popup Gabim perseri, kliko OK dhe ploteso fushat manualisht");
            DismissAlertModal();
            FillInput(FindInputAfterLabel("Emër"), "Test");
            FillInput(FindInputAfterLabel("Mbiemër"), "Test");
        }
        else
        {
            Assert.That(FindInputAfterLabel("NID").GetAttribute("value").Trim(), Is.EqualTo("F90520294G"));
            Assert.That(FindInputAfterLabel("Emër").GetAttribute("value").Trim(), Is.Not.Empty);
            Assert.That(FindInputAfterLabel("Mbiemër").GetAttribute("value").Trim(), Is.Not.Empty);
        }

        Log("Ploteso Date vdekjeje nese eshte bosh");
        IWebElement deathDate = FindInputAfterLabel("Datë vdekjeje");
        if (string.IsNullOrWhiteSpace(deathDate.GetAttribute("value")))
            SetDateValue(deathDate, "2020-01-01", "01.01.2020");

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 4 Title");
        IWebElement Step4Title = wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4.text-uppercase"));
            if (titles.Count == 0)
                return null;
            return titles[0].Text.Trim().ToUpperInvariant()
                == "KËRKESA PËR PENSION PLEQËRIE, PENSION SUPLEMENTAR, SHTESA/KOMPENSIME DHE MËNYRA E PAGESËS"
                ? titles[0]
                : null;
        });
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("KËRKESA PËR PENSION PLEQËRIE, PENSION SUPLEMENTAR, SHTESA/KOMPENSIME DHE MËNYRA E PAGESËS"));

        Log("Assert kohëzgjatja Step 4");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("5 minuta kohëzgjatje"));

        Log("Assert 7 hapa, kater hapat e pare aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(7));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[3].GetAttribute("class"), Does.Contain("active"));
        for (int i = 4; i < steps.Count; i++)
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));

        Log("Assert teksti i kerkeses per pension");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Parashtroj kërkesën për përfitim të pensionit')]")).Displayed, Is.True);

        Log("Assert llojet e pensionit jane te paraqitura dhe te pazgjedhura");
        string[] pensionTypes =
        {
            "nenit 40",
            "8097",
            "si ushtarak",
            "për shkak të detyrës"
        };

        foreach (string pensionType in pensionTypes)
        {
            IWebElement rowLabel = wait.Until(ExpectedConditions.ElementExists(
                By.XPath($"//td[contains(.,'{pensionType}')]")));
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].scrollIntoView({block:'center'});",
                rowLabel
            );
            Assert.That(rowLabel.Displayed, Is.True);
            Assert.That(FindRowCheckbox(pensionType).Selected, Is.False);
        }

        Log("Assert kompensimet jane te pazgjedhura dhe te disabled");
        IWebElement kompensimeParent = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//h6[contains(.,'kompensimet si më poshtë')]/following::input[@type='checkbox'][1]")));
        Assert.That(kompensimeParent.Selected, Is.False);

        IWebElement vkm565 = FindRowCheckbox("VKM nr.565");
        IWebElement vkm8 = FindRowCheckbox("VKM nr.8");
        IWebElement teArdhuraTeTjera = FindRowCheckbox("të ardhurat të tjera");
        Assert.That(vkm565.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(vkm8.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(teArdhuraTeTjera.GetAttribute("disabled"), Is.Not.Null);

        Log("Assert menyra e pageses eshte e pazgjedhur dhe dropdown-et disabled");
        IWebElement postalRadio = wait.Until(ExpectedConditions.ElementExists(By.Id("postalPayment")));
        IWebElement bankRadio = wait.Until(ExpectedConditions.ElementExists(By.Id("bankPayment")));
        Assert.That(postalRadio.Selected, Is.False);
        Assert.That(bankRadio.Selected, Is.False);
        Assert.That(FindPaymentSelect("postalPayment").GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindPaymentSelect("bankPayment").GetAttribute("disabled"), Is.Not.Null);

        IWebElement bankAccount = FindBankAccountInput();
        Assert.That(bankAccount.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(bankAccount.GetAttribute("maxlength"), Is.EqualTo("30"));
        Assert.That(bankAccount.GetAttribute("value"), Is.EqualTo(string.Empty));

        Log("Kliko Vazhdo pa zgjedhur llojin e pensionit dhe menyren e pageses");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error messages per fushat e detyrueshme");
        IWebElement step4Error = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//*[contains(text(),'Përzgjidhni një vlerë për të vazhduar')]")));
        Assert.That(step4Error.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));

        Log("Zgjidh Pension familjar");
        ClickRowCheckbox("nenit 40");
        Assert.That(FindRowCheckbox("nenit 40").Selected, Is.True);

        Log("Zgjidh terheqjen ne Qendren Paguese");
        SelectRadioById("postalPayment");

        wait.Until(d =>
        {
            try
            {
                var postSelect = d.FindElement(
                    By.XPath("//input[@id='postalPayment']/ancestor::div[contains(@class,'row')][1]//select"));
                return postSelect.GetAttribute("disabled") == null
                    && new SelectElement(postSelect).Options.Count > 1;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        IWebElement postalSelect = FindPaymentSelect("postalPayment");
        var postalOptions = new SelectElement(postalSelect);
        Assert.That(postalSelect.GetAttribute("disabled"), Is.Null);
        Assert.That(FindPaymentSelect("bankPayment").GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindBankAccountInput().GetAttribute("disabled"), Is.Not.Null);

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

        Log("Kliko Vazhdo Step 4");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 5 Title");
        IWebElement Step5Title = wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4.text-uppercase"));
            if (titles.Count == 0)
                return null;
            return titles[0].Text.Trim().ToUpperInvariant()
                == "DEKLARATAT E PERIUDHAVE TË SIGURIMIT, MARRËDHËNIEVE FINANCIARE DHE PËRFITIMEVE TË TJERA"
                ? titles[0]
                : null;
        });
        Assert.That(Step5Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DEKLARATAT E PERIUDHAVE TË SIGURIMIT, MARRËDHËNIEVE FINANCIARE DHE PËRFITIMEVE TË TJERA"));

        Log("Assert kohëzgjatja Step 5");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("5 minuta kohëzgjatje"));

        Log("Assert 7 hapa, pese hapat e pare aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(7));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[3].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[4].GetAttribute("class"), Does.Contain("active"));
        for (int i = 5; i < steps.Count; i++)
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));

        Log("Assert seksionet A, B.1, B.2, B.3, C, D");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'A. Deklaratë për ndryshim të vendbanimit')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'B.1 Deklaroj se kam punuar')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.CssSelector("label[for='b2Declaration']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//label[contains(.,'ligjit nr. 7/2025')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.CssSelector("label[for='b3Declaration']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//label[contains(.,'periudha sigurimi të realizuara në shtetet e huaja')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.CssSelector("label[for='hasReceivedBenefits']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//label[contains(.,'ka përfituar pension apo trajtim të veçantë financiar')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.CssSelector("label[for='noOtherBenefits']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//label[contains(.,'nuk përfitoj asnjë lloj tjetër përfitimi')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id("b2Declaration")).GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(driver.FindElement(By.Id("b3Declaration")).GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(driver.FindElement(By.Id("hasReceivedBenefits")).GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(driver.FindElement(By.Id("noOtherBenefits")).GetAttribute("type"), Is.EqualTo("checkbox"));

        Log("Assert tabelat jane bosh");
        Assert.That(driver.FindElements(
            By.XPath("//h6[contains(.,'A. Deklaratë')]/following::table[1]//tbody/tr")).Count, Is.EqualTo(0));
        Assert.That(driver.FindElements(
            By.XPath("//h6[contains(.,'B.1')]/following::table[1]//tbody/tr")).Count, Is.EqualTo(0));
        Assert.That(driver.FindElements(
            By.XPath("//th[contains(.,'Subjekti Punëdhënës')]/ancestor::table[1]//tbody/tr")).Count, Is.EqualTo(0));

        Log("Assert checkbox-et B.2, B.3, C, D jane te pazgjedhura");
        Assert.That(driver.FindElement(By.Id("b2Declaration")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("b3Declaration")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("hasReceivedBenefits")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("noOtherBenefits")).Selected, Is.False);

        Log("Assert shenimet e aplikantit jane bosh");
        IWebElement shenime = wait.Until(ExpectedConditions.ElementExists(By.Id("applicantNotes")));
        Assert.That(shenime.GetAttribute("value"), Is.EqualTo(string.Empty));

        Log("Kliko + SHTO VENDBANIM");
        SafeClick(By.XPath("//button[contains(.,'SHTO VENDBANIM')]"));
        WaitForModalTitle("Të dhënat e vendbanimit");

        Log("Kliko Anullo ne modalin e vendbanimit");
        ClickModalFooter("Anullo");
        WaitForModalClosed();

        Log("Kliko + SHTO VENDBANIM perseri");
        SafeClick(By.XPath("//button[contains(.,'SHTO VENDBANIM')]"));
        WaitForModalTitle("Të dhënat e vendbanimit");

        Log("Kliko Ruaj pa plotesuar fushat e detyrueshme");
        ClickModalFooter("Ruaj");
        AssertModalFieldInvalid(FindModalInput("Nga data"));

        Log("Ploteso vendbanimin");
        SelectDropdownByValue(FindModalSelect("Vendbanimi"), "Kavajë");
        FillModalDate("Nga data", "2020-01-01", "01.01.2020");
        FillModalDate("Deri më datë", "2024-12-31", "31.12.2024");

        Log("Kliko Ruaj vendbanimin");
        ClickModalFooter("Ruaj");
        WaitForModalClosed();
        Thread.Sleep(1000);

        Log("Assert rreshti i vendbanimit ne tabele");
        IWebElement vendbanimRow = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//h6[contains(.,'A. Deklaratë')]/following::table[1]//tbody/tr")));
        var vendbanimCells = vendbanimRow.FindElements(By.TagName("td"));
        Assert.That(vendbanimCells[0].Text.Trim(), Is.EqualTo("1"));
        Assert.That(vendbanimCells[1].Text.Trim(), Is.EqualTo("Kavajë"));
        Assert.That(vendbanimCells[2].Text.Trim(), Does.Contain("01.01.2020").Or.Contain("2020-01-01"));
        Assert.That(vendbanimCells[3].Text.Trim(), Does.Contain("31.12.2024").Or.Contain("2024-12-31"));

        Log("Kliko + SHTO PERIUDHE PUNESIMI para 1994");
        SafeClick(By.XPath("(//button[contains(.,'SHTO PERIUDHË PUNËSIMI')])[1]"));
        WaitForModalTitle("Të dhënat e punësimit para vitit 1994");

        Log("Kliko Anullo ne modalin para 1994");
        ClickModalFooter("Anullo");
        WaitForModalClosed();

        Log("Kliko + SHTO PERIUDHE PUNESIMI para 1994 perseri");
        SafeClick(By.XPath("(//button[contains(.,'SHTO PERIUDHË PUNËSIMI')])[1]"));
        WaitForModalTitle("Të dhënat e punësimit para vitit 1994");

        Log("Kliko Ruaj pa plotesuar fushat e detyrueshme para 1994");
        ClickModalFooter("Ruaj");
        AssertModalFieldInvalid(FindModalInput("Ndërmarrja/Institucioni"));
        AssertModalFieldInvalid(FindModalInput("Nga data"));
        AssertModalFieldInvalid(FindModalInput("Lloji punës"));

        Log("Ploteso punesimin para 1994");
        FillInput(FindModalInput("Ndërmarrja/Institucioni"), "Ndërmarrja Bujqësore");
        FillModalDate("Nga data", "1990-01-01", "01.01.1990");
        FillModalDate("Deri më datë", "1993-12-31", "31.12.1993");
        FillInput(FindModalInput("Lloji punës"), "Punëtor");
        Assert.That(FindModalInput("Lloji punës").GetAttribute("maxlength"), Is.EqualTo("100"));
        var dokumentiPara = new SelectElement(FindModalSelect("Dokumenti që vërteton periudhen"));
        Assert.That(dokumentiPara.Options[0].GetAttribute("value"), Is.EqualTo("Vërtetim"));
        Assert.That(dokumentiPara.Options[1].GetAttribute("value"), Is.EqualTo("Librezë"));
        Assert.That(dokumentiPara.Options[2].GetAttribute("value"), Is.EqualTo("Të tjera"));
        dokumentiPara.SelectByValue("Librezë");

        Log("Kliko Ruaj punesimin para 1994");
        ClickModalFooter("Ruaj");
        WaitForModalClosed();
        Thread.Sleep(1000);

        Log("Assert rreshti i punesimit para 1994");
        IWebElement para1994Row = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//h6[contains(.,'B.1')]/following::table[1]//tbody/tr")));
        var para1994Cells = para1994Row.FindElements(By.TagName("td"));
        Assert.That(para1994Cells[0].Text.Trim(), Is.EqualTo("1"));
        Assert.That(para1994Cells[1].Text.Trim(), Is.EqualTo("Ndërmarrja Bujqësore"));
        Assert.That(para1994Cells[2].Text.Trim(), Does.Contain("01.01.1990").Or.Contain("1990-01-01"));
        Assert.That(para1994Cells[3].Text.Trim(), Does.Contain("31.12.1993").Or.Contain("1993-12-31"));
        Assert.That(para1994Cells[4].Text.Trim(), Is.EqualTo("Punëtor"));
        Assert.That(para1994Cells[5].Text.Trim(), Does.Contain("Librezë"));

        Log("Kliko + SHTO PERIUDHE PUNESIMI pas 1994");
        SafeClick(By.XPath("(//button[contains(.,'SHTO PERIUDHË PUNËSIMI')])[2]"));
        wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".custom-modal-content")));
        Assert.That(FindModalInput("Subjekti Punëdhënës").Displayed, Is.True);

        Log("Kliko Anullo ne modalin pas 1994");
        ClickModalFooter("Anullo");
        WaitForModalClosed();

        Log("Kliko + SHTO PERIUDHE PUNESIMI pas 1994 perseri");
        SafeClick(By.XPath("(//button[contains(.,'SHTO PERIUDHË PUNËSIMI')])[2]"));
        wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[contains(@class,'custom-modal-content')]//label[contains(.,'Subjekti Punëdhënës')]")));

        Log("Kliko Ruaj pa plotesuar fushat e detyrueshme pas 1994");
        ClickModalFooter("Ruaj");
        AssertModalFieldInvalid(FindModalInput("Subjekti Punëdhënës"));
        AssertModalFieldInvalid(FindModalInput("Nga data"));
        AssertModalFieldInvalid(FindModalInput("Lloji punës"));

        Log("Ploteso punesimin pas 1994");
        FillInput(FindModalInput("Subjekti Punëdhënës"), "Ministria e Mbrojtjes");
        FillModalDate("Nga data", "2015-01-01", "01.01.2015");
        FillModalDate("Deri më datë", "2020-12-31", "31.12.2020");
        FillInput(FindModalInput("Lloji punës"), "Punonjës");
        Assert.That(FindModalInput("Lloji punës").GetAttribute("maxlength"), Is.EqualTo("100"));
        new SelectElement(FindModalSelect("Dokumenti që vërteton periudhen")).SelectByValue("Vërtetim");

        Log("Kliko Ruaj punesimin pas 1994");
        ClickModalFooter("Ruaj");
        WaitForModalClosed();
        Thread.Sleep(1000);

        Log("Assert rreshti i punesimit pas 1994");
        IWebElement pas1994Row = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//th[contains(.,'Subjekti Punëdhënës')]/ancestor::table[1]//tbody/tr")));
        var pas1994Cells = pas1994Row.FindElements(By.TagName("td"));
        Assert.That(pas1994Cells[0].Text.Trim(), Is.EqualTo("1"));
        Assert.That(pas1994Cells[1].Text.Trim(), Is.EqualTo("Ministria e Mbrojtjes"));
        Assert.That(pas1994Cells[2].Text.Trim(), Does.Contain("01.01.2015").Or.Contain("2015-01-01"));
        Assert.That(pas1994Cells[3].Text.Trim(), Does.Contain("31.12.2020").Or.Contain("2020-12-31"));
        Assert.That(pas1994Cells[4].Text.Trim(), Is.EqualTo("Punonjës"));
        Assert.That(pas1994Cells[5].Text.Trim(), Does.Contain("Vërtetim"));

        Log("Zgjidh D. nuk perfiton perfitim tjeter");
        ClickMuiCheckbox("noOtherBenefits");
        Assert.That(driver.FindElement(By.Id("noOtherBenefits")).Selected, Is.True);

        Log("Kliko Vazhdo Step 5");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 6 Title");
        IWebElement Step6Title = wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4.text-uppercase"));
            if (titles.Count == 0)
                return null;
            return titles[0].Text.Trim().ToUpperInvariant() == "DOKUMENTACIONI"
                ? titles[0]
                : null;
        });
        Assert.That(Step6Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 6");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("5 minuta kohëzgjatje"));

        Log("Assert 7 hapa, gjashte hapat e pare aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(7));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[3].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[4].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[5].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[6].GetAttribute("class"), Does.Not.Contain("active"));

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h5[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h5[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës publike')]")).Displayed, Is.True);

        Log("Assert document-upload Libreze pune");
        AssertDocumentUpload("6158-librezePune", "Librezë pune");

        Log("Assert document-upload Vertetim page 1994-2012");
        AssertDocumentUpload("6158-fuVertetimPage", "1994 deri në vitin 2012");

        Log("Assert document-upload Vendim gjykate");
        AssertDocumentUpload("6158-fuVendimGjykate", "Vendim gjykate për njohje vjetërsie");

        Log("Assert document-upload Diploma");
        AssertDocumentUpload("6158-fuFADiploma", "Diploma e shkollës së lartë");

        Log("Assert document-upload Vendim KMCAP");
        AssertDocumentUpload("6158-fuVendimKmcap", "Vendim i KMCAP");

        Log("Assert document-upload Vertetim nga Prokuroria");
        AssertDocumentUpload("6158-fuVertetimProkuroria", "Vërtetim nga organet e prokurorise");

        Log("Assert document-upload Vertetim nga shkolla");
        AssertDocumentUpload("6158-fuFAVertetimShkolle", "Vërtetim nga shkolla kur fëmija");

        Log("Assert document-upload Te tjera");
        AssertDocumentUpload("6158-fuOthers", "Të tjera");

        Log("Assert dokumentet e administrates publike");
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'që nga viti 2012 deri në momentin e lënies së punës')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Shërbimit të Detyrueshëm Ushtarak')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Certifikatë martese')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'pagesës së papunësisë')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'dënimit për motive politike')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Dëshmi penaliteti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Pagesën kalimtare')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'pagesën e energjisë elektrike')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'ndryshimit të gjeneraliteteve')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Certifikatë familjare')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Aktverifikime të periudhave të punës e të sigurimit')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Certifikatë vdekjeje')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'humbjen e jetës së pilotit në përmbushje të detyrës')]")).Displayed, Is.True);

        Log("Kliko Vazhdo Step 6");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 7 Title");
        IWebElement Step7Title = wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4.text-uppercase"));
            if (titles.Count == 0)
                return null;
            return titles[0].Text.Trim().ToUpperInvariant()
                == "REGJISTRIMI DHE PRANIMI I KËRKESËS DHE DOKUMENTACIONIT SHOQËRUES"
                ? titles[0]
                : null;
        });
        Assert.That(Step7Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("REGJISTRIMI DHE PRANIMI I KËRKESËS DHE DOKUMENTACIONIT SHOQËRUES"));

        Log("Assert kohëzgjatja Step 7");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("5 minuta kohëzgjatje"));

        Log("Assert 7 hapa, te gjithe aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(7));
        foreach (var step in steps)
            Assert.That(step.GetAttribute("class"), Does.Contain("active"));

        Log("Assert teksti i konfirmimit te kerkuesit");
        IWebElement confirmText = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//span[contains(.,'nënshkruaj kërkesën në të gjitha faqet')]")));
        Assert.That(confirmText.Text.Trim(),
            Is.EqualTo("Pasi u njoha me kushtet ligjore për përfitim, plotësova deklaratën për periudhat e punës dhe ato kontributive,konfirmoj dorëzimin e dokumentacionit shoqërues si më lart dhe nënshkruaj kërkesën në të gjitha faqet e saj."));

        Log("Assert emri i kerkuesit eshte disabled");
        Assert.That(driver.FindElement(
            By.XPath("//span[contains(.,'Kërkuesi')]")).Displayed, Is.True);
        IWebElement kerkuesiInput = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//span[contains(.,'Kërkuesi')]/following::input[1]")));
        Assert.That(kerkuesiInput.GetAttribute("value").Trim(), Is.EqualTo("Ketjona"));
        Assert.That(kerkuesiInput.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(driver.FindElement(
            By.XPath("//span[contains(.,'Emër') and contains(.,'Nënshkrimi')]")).Displayed, Is.True);

        Log("Assert teksti i konfirmimit te nepunësit");
        IWebElement officerText = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//span[contains(.,'nenit 40')]")));
        Assert.That(officerText.Text.Trim(),
            Does.Contain("ligjin nr. 7703"));
        Assert.That(officerText.Text.Trim(),
            Does.Contain("nenit 40, të Ligjit nr. 7703"));
        Assert.That(officerText.Text.Trim(),
            Does.Contain("konfirmoj regjistrimin e kërkesës"));

        Log("Assert checkbox i pranimit eshte i pazgjedhur");
        IWebElement agreeCheck = wait.Until(ExpectedConditions.ElementExists(By.Id("agreeCheck")));
        Assert.That(agreeCheck.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='agreeCheck']")).Text.Trim(),
            Does.Contain("Me klikimin e këtij butoni, ju bini dakord që këto dokumente të sigurohen për ju nga nëpunësi i administratës."));

        Log("Kliko Apliko pa pranuar kushtet");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(1000);
        Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase"))).Text.Trim().ToUpperInvariant(),
            Is.EqualTo("REGJISTRIMI DHE PRANIMI I KËRKESËS DHE DOKUMENTACIONIT SHOQËRUES"));

        Log("Zgjidh pranimin e kushteve");
        SafeClick(By.Id("agreeCheck"));
        Assert.That(driver.FindElement(By.Id("agreeCheck")).Selected, Is.True);

        Log("Assert butoni Apliko");
        IWebElement aplikoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(aplikoBtn.Text.Trim(), Does.Contain("Apliko"));
        Assert.That(aplikoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        //Log("Kliko Apliko");
        //SafeClick(By.CssSelector("button.ealb-btn-continue"));
        //Thread.Sleep(5000);

        //Log("Assert suksesi");
        //IWebElement successTitle = wait.Until(ExpectedConditions.ElementIsVisible(
        //    By.XPath("//h5[contains(.,'APLIKIMI JUAJ')]")));
        //Assert.That(successTitle.Text.Trim().ToUpperInvariant().Replace("Ë", "E"),
        //    Does.Contain("APLIKIMI JUAJ U DERGUA ME SUKSES"));

        //IWebElement referenceNumber = wait.Until(ExpectedConditions.ElementIsVisible(
        //    By.XPath("//h6[contains(.,'Numri referencë i aplikimit')]")));
        //Assert.That(referenceNumber.Text, Does.Contain("6158-"));
        //Assert.That(driver.Url, Does.Contain("/mesazh"));

        Log("TEST PASSED");
    }

    private IWebElement FindInputByLabel(string labelText)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//div[@id='root']//label[contains(normalize-space(),'{labelText}')]/following-sibling::input")));
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

    private void SetReactInputValue(IWebElement input, string value)
    {

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            input
        );
        Thread.Sleep(200);

        ((IJavaScriptExecutor)driver).ExecuteScript(@"
            const el = arguments[0];
            const proto = el.tagName === 'TEXTAREA'
                ? window.HTMLTextAreaElement.prototype
                : window.HTMLInputElement.prototype;
            const setter = Object.getOwnPropertyDescriptor(proto, 'value').set;
            el.focus();
            setter.call(el, arguments[1]);
            el.dispatchEvent(new Event('input', { bubbles: true }));
            el.dispatchEvent(new Event('change', { bubbles: true }));
            el.blur();
        ", input, value);
        Thread.Sleep(500);
    }

    private void DismissAlertModal()
    {

        SafeClick(By.CssSelector("button.alert-modal-button--primary"));
        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
            By.CssSelector(".alert-modal-overlay")));
        Thread.Sleep(400);
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

    private IWebElement FindModalInput(string labelPart)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//div[contains(@class,'custom-modal-content')]//label[contains(.,'{labelPart}')]/following::input[not(@type='hidden')][1]")));
    }

    private IWebElement FindModalSelect(string labelPart)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//div[contains(@class,'custom-modal-content')]//label[contains(.,'{labelPart}')]/following::select[1]")));
    }

    private void WaitForModalTitle(string title)
    {

        IWebElement modalTitle = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector(".custom-modal-title")));
        Assert.That(modalTitle.Text.Trim(), Is.EqualTo(title));
    }

    private void ClickModalFooter(string buttonText)
    {

        SafeClick(By.XPath(
            $"//div[contains(@class,'custom-modal-content')]//button[contains(.,'{buttonText}')]"));
        Thread.Sleep(500);
    }

    private void WaitForModalClosed()
    {

        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
            By.CssSelector(".custom-modal-content")));
    }

    private void AssertModalFieldInvalid(IWebElement field)
    {

        string cssClass = field.GetAttribute("class") ?? string.Empty;
        if (cssClass.Contains("error") || cssClass.Contains("is-invalid"))
            return;

        var formGroup = field.FindElements(
            By.XPath("./ancestor::div[contains(@class,'form-group')][1]"));
        if (formGroup.Count > 0)
        {
            bool labelInvalid = formGroup[0].FindElements(
                By.XPath(".//label[contains(@class,'text-danger') or contains(@class,'error')]")).Count > 0;
            bool errorText = formGroup[0].FindElements(
                By.XPath(".//*[contains(@class,'text-danger') or contains(@class,'custom-modal-error')][normalize-space()]")).Count > 0;
            if (labelInvalid || errorText)
                return;
        }

        Assert.Fail($"Expected invalid modal field. class='{cssClass}'");
    }

    private IWebElement FindTextareaByLabel(string labelPart)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//div[@id='root']//label[contains(normalize-space(),'{labelPart}')]/following-sibling::textarea")));
    }

    private IWebElement FindInputAfterLabel(string labelPart)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//div[@id='root']//label[contains(.,'{labelPart}')]/following::input[not(@type='hidden')][1]")));
    }

    private void CloseDatePicker()
    {

        try
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(@"
                document.querySelectorAll('.flatpickr-calendar.open').forEach(el => {
                    el.classList.remove('open');
                    el.style.display = 'none';
                });
                if (document.activeElement) { document.activeElement.blur(); }
            ");
        }
        catch (Exception ex)
        {
            Log("CloseDatePicker error: " + ex.Message);
        }
    }

    private void SetDateValue(IWebElement input, string isoDate, string displayDate)
    {

        string type = input.GetAttribute("type") ?? string.Empty;
        if (type.Equals("date", StringComparison.OrdinalIgnoreCase))
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(@"
                const el = arguments[0];
                el.value = arguments[1];
                el.dispatchEvent(new Event('input', { bubbles: true }));
                el.dispatchEvent(new Event('change', { bubbles: true }));
            ", input, isoDate);

            var hidden = input.FindElements(By.XPath("./preceding-sibling::input[@type='hidden'][1]"));
            if (hidden.Count > 0)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript(@"
                    const el = arguments[0];
                    el.value = arguments[1];
                    el.dispatchEvent(new Event('input', { bubbles: true }));
                    el.dispatchEvent(new Event('change', { bubbles: true }));
                ", hidden[0], displayDate);
            }
        }
        else
        {
            FillInput(input, displayDate);
        }

        CloseDatePicker();
        Thread.Sleep(300);
    }

    private void FillModalDate(string labelPart, string isoDate, string displayDate)
    {

        SetDateValue(FindModalInput(labelPart), isoDate, displayDate);
    }

    private void ClickMuiCheckbox(string checkboxId)
    {

        CloseDatePicker();

        IWebElement input = wait.Until(ExpectedConditions.ElementExists(By.Id(checkboxId)));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            input
        );
        Thread.Sleep(300);

        if (!input.Selected)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(@"
                const el = arguments[0];
                el.click();
                el.dispatchEvent(new Event('input', { bubbles: true }));
                el.dispatchEvent(new Event('change', { bubbles: true }));
            ", input);
        }

        wait.Until(d => d.FindElement(By.Id(checkboxId)).Selected);
        Thread.Sleep(300);
    }

    private void SelectRadioById(string radioId)
    {

        SafeClick(By.Id(radioId));
        Thread.Sleep(500);
    }

    private IWebElement FindRowCheckbox(string cellPart)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//td[contains(.,'{cellPart}')]/following-sibling::td//input[@type='checkbox']")));
    }

    private void ClickRowCheckbox(string cellPart)
    {

        SafeClick(By.XPath(
            $"//td[contains(.,'{cellPart}')]/following-sibling::td//span[contains(@class,'MuiCheckbox-root')]"));
        Thread.Sleep(300);
    }

    private IWebElement FindPaymentSelect(string radioId)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//input[@id='{radioId}']/ancestor::div[contains(@class,'row')][1]//select")));
    }

    private IWebElement FindBankAccountInput()
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//input[@id='bankPayment']/ancestor::div[contains(@class,'row')][1]//input[@type='text']")));
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {

        Assert.That(driver.FindElement(
            By.XPath($"//span[contains(normalize-space(),'{documentTitle}')]")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-6158"));
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
            Is.EqualTo("Formatet e lejuara: PDF, JPG, JPEG, PNG. Madhesia maksimale: 25MB."));
    }
}