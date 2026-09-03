using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("6156")]
public class _6156_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "6156";
    protected override string? ServiceTitle => "AplikimPerPensionPleqerieSuplementarPilot";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    [Test]
    public void AplikimPerPensionPleqerieSuplementarPilot()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("KËRKESË PËR CAKTIM PENSIONI PLEQËRIE"));

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("5 minuta kohëzgjatje"));

        Log("Assert 7 hapa, hapi i pare aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(7));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));

        Log("Assert Nr eshte disabled dhe i para-plotesuar");
        IWebElement nrInput = FindInputByLabel("Nr.");
        Assert.That(nrInput.GetAttribute("value"), Is.Not.Empty);
        Assert.That(nrInput.GetAttribute("disabled"), Is.Not.Null);

        Log("Assert Regj.Date eshte disabled dhe e dites se sotme");
        IWebElement dateInput = FindInputByLabel("Regj.Datë");
        Assert.That(dateInput.GetAttribute("value").Trim(),
            Is.EqualTo(DateTime.Now.ToString("dd.MM.yyyy")));
        Assert.That(dateInput.GetAttribute("disabled"), Is.Not.Null);

        Log("Assert DRSSH ka opsionet e drejtorive");
        IWebElement drsshSelect = FindSelectByLabel("DRSSH");
        var drssh = new SelectElement(drsshSelect);
        Assert.That(drssh.SelectedOption.GetAttribute("value"), Is.EqualTo("zgjidh"));
        Assert.That(drssh.Options.Count, Is.EqualTo(15));
        Assert.That(drssh.Options[1].Text.Trim(), Is.EqualTo("Drejtoria Berat"));
        Assert.That(drssh.Options[11].GetAttribute("value"), Is.EqualTo("11"));
        Assert.That(drssh.Options[11].Text.Trim(), Is.EqualTo("Drejtoria Tirane"));
        Assert.That(drssh.Options[13].Text.Trim(), Is.EqualTo("Dega Tropoje"));
        Assert.That(drssh.Options[14].Text.Trim(), Is.EqualTo("Dega Sarande"));

        Log("Assert ALSSH eshte disabled para zgjedhjes se DRSSH");
        IWebElement alsshSelect = FindSelectByLabel("ALSSH");
        Assert.That(alsshSelect.GetAttribute("disabled"), Is.Not.Null);

        Log("Kliko Vazhdo pa zgjedhur DRSSH dhe ALSSH");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error message per DRSSH");
        IWebElement drsshError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//label[contains(.,'DRSSH')]/following-sibling::div[contains(@class,'text-danger')]")));
        Assert.That(drsshError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));

        Log("Assert error message per ALSSH");
        IWebElement alsshError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//label[contains(.,'ALSSH')]/following-sibling::div[contains(@class,'text-danger')]")));
        Assert.That(alsshError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));

        Log("Zgjidh Drejtoria Tirane");
        SelectDropdownByValue(FindSelectByLabel("DRSSH"), "11");

        Log("Wait qe ALSSH te aktivizohet");
        wait.Until(d =>
        {
            try
            {
                var agency = d.FindElement(
                    By.XPath("//form//label[contains(.,'ALSSH')]/following-sibling::select"));
                return agency.GetAttribute("disabled") == null
                    && new SelectElement(agency).Options.Count > 1;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        IWebElement alsshEnabled = FindSelectByLabel("ALSSH");
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
            return titles[0].Text.Trim().ToUpperInvariant() == "TË DHËNA PERSONALE TË KËRKUESIT"
                ? titles[0]
                : null;
        });
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNA PERSONALE TË KËRKUESIT"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("5 minuta kohëzgjatje"));

        Log("Assert 7 hapa, dy hapat e pare aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(7));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));

        Log("Assert te dhenat personale te para-plotesuara dhe readonly");
        IWebElement nidInput = FindInputByLabel("NID");
        Assert.That(nidInput.GetAttribute("value").Trim(), Is.EqualTo(Settings.Qytetar.Username));
        Assert.That(nidInput.GetAttribute("readonly"), Is.Not.Null);

        IWebElement emerInput = FindInputByLabel("Emër");
        Assert.That(emerInput.GetAttribute("value").Trim(), Is.EqualTo("Ketjona"));
        Assert.That(emerInput.GetAttribute("readonly"), Is.Not.Null);

        IWebElement atesiaInput = FindInputByLabel("Atësia");
        Assert.That(atesiaInput.GetAttribute("value").Trim(), Is.EqualTo("Mersin"));
        Assert.That(atesiaInput.GetAttribute("readonly"), Is.Not.Null);

        IWebElement mbiemerInput = FindInputByLabel("Mbiemër");
        Assert.That(mbiemerInput.GetAttribute("value").Trim(), Is.EqualTo("Mema"));
        Assert.That(mbiemerInput.GetAttribute("readonly"), Is.Not.Null);

        IWebElement datelindjaInput = FindInputByLabel("Datëlindja");
        Assert.That(datelindjaInput.GetAttribute("value").Trim(), Is.EqualTo("28.07.1995"));
        Assert.That(datelindjaInput.GetAttribute("readonly"), Is.Not.Null);

        IWebElement mbiemerParaMarteses = FindInputByLabel("Mbiemër para martesës");
        Assert.That(mbiemerParaMarteses.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(mbiemerParaMarteses.GetAttribute("readonly"), Is.Not.Null);

        Log("Assert seksionet Vendlindja, Adresa dhe Kontakt");
        Assert.That(driver.FindElement(By.XPath("//form//h5[normalize-space()='Vendlindja']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//form//h5[normalize-space()='Adresa']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//form//h5[normalize-space()='Kontakt']")).Displayed, Is.True);

        Log("Assert fushat e vendlindjes jane bosh");
        IWebElement fshati = FindSectionInput("Vendlindja", "Fshati");
        IWebElement qyteti = FindSectionInput("Vendlindja", "Qyteti");
        IWebElement rrethi = FindSectionInput("Vendlindja", "Rrethi");
        IWebElement qarku = FindSectionInput("Vendlindja", "Qarku");
        Assert.That(fshati.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(qyteti.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(rrethi.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(qarku.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(fshati.GetAttribute("maxlength"), Is.EqualTo("100"));
        Assert.That(qyteti.GetAttribute("maxlength"), Is.EqualTo("100"));
        Assert.That(rrethi.GetAttribute("maxlength"), Is.EqualTo("100"));
        Assert.That(qarku.GetAttribute("maxlength"), Is.EqualTo("100"));

        Log("Assert fushat e adreses jane bosh");
        IWebElement lagjia = FindSectionInput("Adresa", "Lagjia");
        IWebElement rruga = FindSectionInput("Adresa", "Rruga");
        IWebElement pallati = FindSectionInput("Adresa", "Pallati");
        IWebElement shkalla = FindSectionInput("Adresa", "Shkalla");
        IWebElement qytetiFshati = FindSectionInput("Adresa", "Qyteti/Fshati");
        IWebElement qarkuRrethi = FindSectionInput("Adresa", "Qarku/Rrethi");
        Assert.That(lagjia.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(rruga.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(pallati.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(shkalla.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(qytetiFshati.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(qarkuRrethi.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(lagjia.GetAttribute("maxlength"), Is.EqualTo("100"));
        Assert.That(rruga.GetAttribute("maxlength"), Is.EqualTo("100"));
        Assert.That(pallati.GetAttribute("maxlength"), Is.EqualTo("12"));
        Assert.That(shkalla.GetAttribute("maxlength"), Is.EqualTo("10"));
        Assert.That(qytetiFshati.GetAttribute("maxlength"), Is.EqualTo("100"));
        Assert.That(qarkuRrethi.GetAttribute("maxlength"), Is.EqualTo("100"));

        Log("Assert fushat e kontaktit");
        IWebElement nrTel = FindSectionInput("Kontakt", "Nr. Tel");
        IWebElement nrCel = FindSectionInput("Kontakt", "Nr. Cel.");
        IWebElement email = FindSectionInput("Kontakt", "Email");
        Assert.That(nrTel.GetAttribute("value").Trim(), Is.EqualTo("0676041404"));
        Assert.That(nrTel.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(nrCel.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(email.GetAttribute("value").Trim(), Is.EqualTo("ketjona.mema@kreatx.com"));
        Assert.That(email.GetAttribute("readonly"), Is.Not.Null);

        Log("Ploteso Vendlindja");
        FillInput(FindSectionInput("Vendlindja", "Fshati"), "Kavajë");
        FillInput(FindSectionInput("Vendlindja", "Qyteti"), "Kavajë");
        FillInput(FindSectionInput("Vendlindja", "Rrethi"), "Kavajë");
        FillInput(FindSectionInput("Vendlindja", "Qarku"), "Tiranë");

        Log("Ploteso Adresa");
        FillInput(FindSectionInput("Adresa", "Lagjia"), "1");
        FillInput(FindSectionInput("Adresa", "Rruga"), "Test");
        FillInput(FindSectionInput("Adresa", "Pallati"), "1");
        FillInput(FindSectionInput("Adresa", "Shkalla"), "2");
        FillInput(FindSectionInput("Adresa", "Qyteti/Fshati"), "Kavajë");
        FillInput(FindSectionInput("Adresa", "Qarku/Rrethi"), "Tiranë");

        Log("Ploteso Nr. Cel.");
        FillInput(FindSectionInput("Kontakt", "Nr. Cel."), "0676041404");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4.text-uppercase"));
            if (titles.Count == 0)
                return null;
            return titles[0].Text.Trim().ToUpperInvariant()
                == "KËRKESA PËR PENSION PLEQËRIE, PENSION SUPLEMENTAR, SHTESA/KOMPENSIME DHE MËNYRA E PAGESËS"
                ? titles[0]
                : null;
        });
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("KËRKESA PËR PENSION PLEQËRIE, PENSION SUPLEMENTAR, SHTESA/KOMPENSIME DHE MËNYRA E PAGESËS"));

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

        Log("Assert teksti i kerkeses per pension");
        Assert.That(driver.FindElement(
            By.XPath("//span[contains(.,'Parashtroj kërkesën për përfitim të pensionit')]")).Displayed, Is.True);

        Log("Assert llojet e pensionit jane te paraqitura dhe te pazgjedhura");
        string[] pensionTypes =
        {
            "neni 31",
            "i reduktuar",
            "NSHF",
            "8097",
            "10142",
            "si pilot",
            "9361",
            "10139",
            "Shtesë mbi pensionin e pleqërisë",
            "150 / 2014",
            "për shkak të profesionit",
            "Pension social",
            "personat në ngarkim"
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

        Log("Assert shtesa mbi pension dhe kompensimet jane te pazgjedhura");
        IWebElement shtesaCheckbox = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//div[contains(@class,'col-12') and contains(text(),'nenit 33')]//input[@type='checkbox']")));
        Assert.That(shtesaCheckbox.Selected, Is.False);

        IWebElement kompensimeParent = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//div[contains(@class,'col-12') and contains(text(),'Kërkoj të përfitoj kompensimet si më poshtë')]//input[@type='checkbox']")));
        Assert.That(kompensimeParent.Selected, Is.False);

        Log("Assert kompensimet e energjise jane disabled para zgjedhjes se prindit");
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
        IWebElement step3Error = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//*[contains(text(),'Përzgjidhni një vlerë për të vazhduar')]")));
        Assert.That(step3Error.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));

        Log("Zgjidh Pension pleqerie (neni 31) dhe Suplementar pleqerie si pilot");
        ClickRowCheckbox("neni 31");
        ClickRowCheckbox("si pilot");
        Assert.That(FindRowCheckbox("neni 31").Selected, Is.True);
        Assert.That(FindRowCheckbox("si pilot").Selected, Is.True);

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
                == "DEKLARATAT E PERIUDHAVE TË SIGURIMIT, MARRËDHËNIEVE FINANCIARE DHE PËRFITIMEVE TË TJERA"
                ? titles[0]
                : null;
        });
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DEKLARATAT E PERIUDHAVE TË SIGURIMIT, MARRËDHËNIEVE FINANCIARE DHE PËRFITIMEVE TË TJERA"));

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

        Log("Assert seksionet A, B1, B2, B3, C, D, E");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'A. Deklaratë për ndryshim të vendbanimit')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'B1. Deklaroj se kam punuar')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//span[contains(.,'ligjit nr. 7/2025')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.CssSelector("label[for='sectionB2']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//span[contains(.,'periudha sigurimi të realizuara në shtetet e huaja')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.CssSelector("label[for='sectionB3']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//span[contains(.,'përfitoj pension apo trajtim të veçantë financiar')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.CssSelector("label[for='hasPension']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//span[contains(.,'nuk përfitoj asnjë lloj tjetër përfitimi')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id("sectionB2")).GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(driver.FindElement(By.Id("sectionB3")).GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(driver.FindElement(By.Id("hasPension")).GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(driver.FindElement(By.Id("noOtherBenefits")).GetAttribute("type"), Is.EqualTo("checkbox"));

        Log("Assert tabelat jane bosh");
        IWebElement emptyVendbanim = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//h6[contains(.,'A. Deklaratë')]/following::table[1]//td[@colspan='5']")));
        Assert.That(emptyVendbanim.Text.Trim(), Is.EqualTo(string.Empty));

        IWebElement emptyPara1994 = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//h6[contains(.,'B1.')]/following::table[1]//td[@colspan='7']")));
        Assert.That(emptyPara1994.Text.Trim(), Is.EqualTo(string.Empty));

        IWebElement emptyPas1994 = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//th[contains(.,'Subjekti Punëdhënës')]/ancestor::table[1]//td[@colspan='7']")));
        Assert.That(emptyPas1994.Text.Trim(), Is.EqualTo(string.Empty));

        Log("Assert checkbox-et B2, B3, D jane te pazgjedhura");
        Assert.That(driver.FindElement(By.Id("sectionB2")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("sectionB3")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("hasPension")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("noOtherBenefits")).Selected, Is.False);

        Log("Assert fushat e pensionit D jane disabled");
        IWebElement pensionName = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//span[contains(.,'përfitoj pension apo trajtim')]/following::input[@type='text'][1]")));
        IWebElement pensionNumber = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//span[normalize-space()='me numër']/following-sibling::input[1]")));
        Assert.That(pensionName.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(pensionNumber.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(pensionNumber.GetAttribute("maxlength"), Is.EqualTo("30"));

        Log("Assert deklarata C ka default KAM");
        IWebElement kamSelect = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//span[contains(.,'Deklaroj nën përgjegjësinë')]/following-sibling::select[1]")));
        Assert.That(new SelectElement(kamSelect).SelectedOption.GetAttribute("value"), Is.EqualTo("KAM"));
        Assert.That(new SelectElement(kamSelect).Options[1].GetAttribute("value"), Is.EqualTo("NUK KAM"));

        Log("Kliko + SHTO VENDBANIM");
        SafeClick(By.XPath("//button[contains(.,'SHTO VENDBANIM')]"));
        WaitForModalTitle("Të dhënat e vendbanimit");

        Log("Kliko ANULLO ne modalin e vendbanimit");
        ClickModalFooter("ANULLO");
        WaitForModalClosed();

        Log("Kliko + SHTO VENDBANIM perseri");
        SafeClick(By.XPath("//button[contains(.,'SHTO VENDBANIM')]"));
        WaitForModalTitle("Të dhënat e vendbanimit");

        Log("Kliko RUAJ pa plotesuar fushat e detyrueshme");
        ClickModalFooter("RUAJ");
        AssertModalFieldInvalid(FindModalSelect("Vendbanimi"));
        AssertModalFieldInvalid(FindModalInput("Nga data"));

        Log("Ploteso vendbanimin");
        SelectDropdownByValue(FindModalSelect("Vendbanimi"), "Kavajë");
        FillModalDate("Nga data", "2020-01-01", "01.01.2020");
        FillModalDate("Deri më datë", "2024-12-31", "31.12.2024");

        Log("Kliko RUAJ vendbanimin");
        ClickModalFooter("RUAJ");
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

        Log("Kliko ANULLO ne modalin para 1994");
        ClickModalFooter("ANULLO");
        WaitForModalClosed();

        Log("Kliko + SHTO PERIUDHE PUNESIMI para 1994 perseri");
        SafeClick(By.XPath("(//button[contains(.,'SHTO PERIUDHË PUNËSIMI')])[1]"));
        WaitForModalTitle("Të dhënat e punësimit para vitit 1994");

        Log("Kliko RUAJ pa plotesuar fushat e detyrueshme para 1994");
        ClickModalFooter("RUAJ");
        AssertModalFieldInvalid(FindModalInput("Ndërmarrja/Institucioni"));
        AssertModalFieldInvalid(FindModalInput("Nga data"));
        AssertModalFieldInvalid(FindModalInput("Lloji i punës"));

        Log("Ploteso punesimin para 1994");
        FillInput(FindModalInput("Ndërmarrja/Institucioni"), "Ndërmarrja Bujqësore");
        Assert.That(FindModalInput("Ndërmarrja/Institucioni").GetAttribute("maxlength"), Is.EqualTo("100"));
        FillModalDate("Nga data", "1990-01-01", "01.01.1990");
        FillModalDate("Deri më datë", "1993-12-31", "31.12.1993");
        FillInput(FindModalInput("Lloji i punës"), "Punëtor");
        Assert.That(FindModalInput("Lloji i punës").GetAttribute("maxlength"), Is.EqualTo("100"));
        var dokumentiPara = new SelectElement(FindModalSelect("Dokumenti që vërteton periudhën"));
        Assert.That(dokumentiPara.Options[0].GetAttribute("value"), Is.EqualTo("Vërtetim"));
        Assert.That(dokumentiPara.Options[1].GetAttribute("value"), Is.EqualTo("Librezë"));
        Assert.That(dokumentiPara.Options[2].GetAttribute("value"), Is.EqualTo("Të Tjera"));
        dokumentiPara.SelectByValue("Librezë");

        Log("Kliko RUAJ punesimin para 1994");
        ClickModalFooter("RUAJ");
        WaitForModalClosed();
        Thread.Sleep(1000);

        Log("Assert rreshti i punesimit para 1994");
        IWebElement para1994Row = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//h6[contains(.,'B1.')]/following::table[1]//tbody/tr")));
        var para1994Cells = para1994Row.FindElements(By.TagName("td"));
        Assert.That(para1994Cells[0].Text.Trim(), Is.EqualTo("1"));
        Assert.That(para1994Cells[1].Text.Trim(), Is.EqualTo("Ndërmarrja Bujqësore"));
        Assert.That(para1994Cells[2].Text.Trim(), Does.Contain("01.01.1990").Or.Contain("1990-01-01"));
        Assert.That(para1994Cells[3].Text.Trim(), Does.Contain("31.12.1993").Or.Contain("1993-12-31"));
        Assert.That(para1994Cells[4].Text.Trim(), Is.EqualTo("Punëtor"));
        Assert.That(para1994Cells[5].Text.Trim(), Does.Contain("Librezë"));

        Log("Kliko + SHTO PERIUDHE PUNESIMI pas 1994");
        SafeClick(By.XPath("(//button[contains(.,'SHTO PERIUDHË PUNËSIMI')])[2]"));
        WaitForModalTitle("Të dhënat e punësimit pas vitit 1994");

        Log("Kliko ANULLO ne modalin pas 1994");
        ClickModalFooter("ANULLO");
        WaitForModalClosed();

        Log("Kliko + SHTO PERIUDHE PUNESIMI pas 1994 perseri");
        SafeClick(By.XPath("(//button[contains(.,'SHTO PERIUDHË PUNËSIMI')])[2]"));
        WaitForModalTitle("Të dhënat e punësimit pas vitit 1994");

        Log("Kliko RUAJ pa plotesuar fushat e detyrueshme pas 1994");
        ClickModalFooter("RUAJ");
        AssertModalFieldInvalid(FindModalInput("Subjekti Punëdhënës"));
        AssertModalFieldInvalid(FindModalInput("Nga data"));
        AssertModalFieldInvalid(FindModalInput("Lloji i punës"));

        Log("Ploteso punesimin pas 1994");
        FillInput(FindModalInput("Subjekti Punëdhënës"), "Ministria e Mbrojtjes");
        Assert.That(FindModalInput("Subjekti Punëdhënës").GetAttribute("maxlength"), Is.EqualTo("100"));
        FillModalDate("Nga data", "2015-01-01", "01.01.2015");
        FillModalDate("Deri më datë", "2020-12-31", "31.12.2020");
        FillInput(FindModalInput("Lloji i punës"), "Pilot");
        new SelectElement(FindModalSelect("Dokumenti që vërteton periudhën")).SelectByValue("Vërtetim");

        Log("Kliko RUAJ punesimin pas 1994");
        ClickModalFooter("RUAJ");
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
        Assert.That(pas1994Cells[4].Text.Trim(), Is.EqualTo("Pilot"));
        Assert.That(pas1994Cells[5].Text.Trim(), Does.Contain("Vërtetim"));

        Log("Ploteso deklaraten C");
        FillInput(wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//span[contains(.,'shkëputur marrëdhëniet financiare me subjektin')]/following-sibling::input[@type='text'][1]"))),
            "Ministria e Mbrojtjes");

        IWebElement cDateInput = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//span[contains(.,'Deklaroj nën përgjegjësinë')]/ancestor::div[contains(@class,'mb-3')][1]//input[@type='date' or (@placeholder='dd.mm.yyyy' and not(@type='hidden'))]")));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            cDateInput
        );
        SetDateValue(cDateInput, "2020-12-31", "31.12.2020");

        Log("Zgjidh E. nuk perfiton perfitim tjeter");
        ClickMuiCheckbox("noOtherBenefits");
        Assert.That(driver.FindElement(By.Id("noOtherBenefits")).Selected, Is.True);

        Log("Kliko Vazhdo Step 4");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 5 Title");
        IWebElement Step5Title = wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4.text-uppercase"));
            if (titles.Count == 0)
                return null;
            return titles[0].Text.Trim().ToUpperInvariant() == "DOKUMENTACIONI"
                ? titles[0]
                : null;
        });
        Assert.That(Step5Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 5");
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

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h5[contains(.,'Për të provuar plotësimin e kushteve')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës publike')]")).Displayed, Is.True);

        Log("Assert document-upload Libreze pune");
        AssertDocumentUpload("6156-workBook", "Librezë pune");

        Log("Assert document-upload Vertetim page");
        AssertDocumentUpload("6156-salaryProof", "Vërtetim page");

        Log("Assert document-upload Diploma");
        AssertDocumentUpload("6156-diploma", "Diploma e shkollës së lartë");

        Log("Assert document-upload Vendim gjykate");
        AssertDocumentUpload("6156-courtDecision", "Vendim gjykate për njohje vjetërsie pune");

        Log("Assert document-upload Te tjera");
        AssertDocumentUpload("6156-othersRegular", "Të tjera");

        Log("Assert dokumentet e administrates publike");
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Vërtetim page') and contains(.,'pas vitit 2012')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Shërbimit të Detyrueshëm Ushtarak')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Certifikatë Martese')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'pagesës së papunësisë')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'dënimit për motive politike')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Dëshmi penaliteti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Pagesën kalimtare si ushtarak')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'pagesën e energjisë elektrike')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'ndryshimit të gjeneraliteteve')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Certifikatë familjare')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Certifikatë lindje e fëmijëve')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Aktverifikime të periudhave të sigurimit')]")).Displayed, Is.True);

        Log("Assert dokumenta shtese per pension suplementar si pilot");
        Assert.That(driver.FindElement(
            By.XPath("//h5[contains(.,'Dokumenta shtesë për pension suplementar si pilot')]")).Displayed, Is.True);
        AssertDocumentUpload("6156-pilotOther", "Të tjera");
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Vërtetim vite shërbimi')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(.,'Vërtetim pagë referuese')]")).Displayed, Is.True);

        Log("Kliko Vazhdo Step 5");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 6 Title");
        IWebElement Step6Title = wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4.text-uppercase"));
            if (titles.Count == 0)
                return null;
            return titles[0].Text.Trim().ToUpperInvariant() == "PRANIMI DHE NËNSHKRIMI I KËRKESËS"
                ? titles[0]
                : null;
        });
        Assert.That(Step6Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("PRANIMI DHE NËNSHKRIMI I KËRKESËS"));

        Log("Assert kohëzgjatja Step 6");
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
            By.XPath("//form//p[contains(.,'Ligjit nr. 9887/2003')]")));
        Assert.That(confirmText.Text.Trim(),
            Is.EqualTo("Pasi u njoha me kushtet ligjore për përfitim, plotësova deklaratën për periudhat e punës dhe ato kontributive, konfirmoj dorëzimin e dokumentacionit shoqërues si më lart dhe nënshkruaj kërkesën në të gjitha faqet e saj, sipas Ligjit nr. 9887/2003 \"për mbrojtjen e të dhënave personale\"."));

        Log("Assert emri i kerkuesit eshte readonly");
        Assert.That(driver.FindElement(
            By.XPath("//div[normalize-space()='Kërkuesi']")).Displayed, Is.True);
        IWebElement kerkuesiInput = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//input[@readonly]")));
        Assert.That(kerkuesiInput.GetAttribute("value").Trim(), Is.EqualTo("Ketjona Mema"));
        Assert.That(kerkuesiInput.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(.,'Emër, Mbiemër, Nënshkrimi')]")).Displayed, Is.True);

        Log("Assert teksti i konfirmimit te nepunësit");
        IWebElement officerText = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//p[contains(.,'ligjit nr. 9128 / 2003')]")));
        Assert.That(officerText.Text.Trim(),
            Is.EqualTo("Pasi vlerësova kushtet ligjore në përputhje me ligjin nr. 7703, datë 11.05.1993, \"Për sigurimet shoqërore në RSH\", të ndryshuar, dhe çdo dispozitë tjetër ligjore dhe nënligjore për sigurimet shoqërore dhe ligjit nr. 7703 / 1993, ligjit nr. 9128 / 2003, kontrollova plotësimin e të dhënave të detyrueshme, dokumentacionin e dorëzuar nga kërkuesi, konfirmoj regjistrimin e kërkesës."));

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
            Is.EqualTo("PRANIMI DHE NËNSHKRIMI I KËRKESËS"));

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
        //Assert.That(referenceNumber.Text, Does.Contain("6156-"));
        //Assert.That(driver.Url, Does.Contain("/mesazh"));

        Log("TEST PASSED");
    }

    private IWebElement FindInputByLabel(string labelText)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//form//label[normalize-space()='{labelText}']/following-sibling::input")));
    }

    private IWebElement FindSelectByLabel(string labelPart)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//form//label[contains(.,'{labelPart}')]/following-sibling::select")));
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

    private IWebElement FindSectionInput(string sectionTitle, string labelText)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//form//h5[normalize-space()='{sectionTitle}']/following-sibling::div[1]//label[normalize-space()='{labelText}']/following-sibling::input")));
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

        Thread.Sleep(300);
    }

    private void FillModalDate(string labelPart, string isoDate, string displayDate)
    {

        SetDateValue(FindModalInput(labelPart), isoDate, displayDate);
    }

    private void ClickMuiCheckbox(string checkboxId)
    {

        SafeClick(By.XPath(
            $"//input[@id='{checkboxId}']/ancestor::span[contains(@class,'MuiCheckbox-root')]"));
        Thread.Sleep(300);
    }

    private void AssertModalFieldInvalid(IWebElement field)
    {

        string cssClass = field.GetAttribute("class") ?? string.Empty;
        Assert.That(cssClass, Does.Contain("error").Or.Contain("is-invalid"));
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {

        Assert.That(driver.FindElement(
            By.XPath($"//span[contains(normalize-space(),'{documentTitle}')]")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-6156"));
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

    private void OpenNewApplicationFromServicePage()
    {
        Log("Assert page header");
        IWebElement headerContainer = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("div.page-header-container")));
        Assert.That(headerContainer.Displayed, Is.True, "Page header nuk eshte visible");

        IWebElement serviceName = wait.Until(ExpectedConditions.ElementIsVisible(
            By.Id("serviceNameBreadcrumb")));
        Assert.That(serviceName.Displayed, Is.True, "Breadcrumb i sherbimit nuk eshte visible");
        Assert.That(serviceName.Text.Trim(),
            Is.EqualTo("Aplikim për pension pleqërie, suplementar pilot, suplementar lundrues i nëndetëseve apo suplementar për akademik"),
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
}