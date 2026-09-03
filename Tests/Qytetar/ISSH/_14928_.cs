using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("14928")]
public class _14928_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "14928";
    protected override string? ServiceTitle => "NjohjeSigurimiPerKB";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    [Test]
    public void NjohjeSigurimiPerKB()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E VENDNDODHJES"));

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("5 minuta kohëzgjatje"));

        Log("Assert 7 hapa, hapi i pare aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(7));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));
        for (int i = 1; i < steps.Count; i++)
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert Drejtoria Rajonale ka opsionet e drejtorive");
        IWebElement drejtoriaSelect = FindSelectByLabel("Drejtoria Rajonale");
        var drejtoria = new SelectElement(drejtoriaSelect);
        Assert.That(drejtoria.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(drejtoria.Options.Count, Is.EqualTo(15));
        Assert.That(drejtoria.Options[1].GetAttribute("value"), Is.EqualTo("01"));
        Assert.That(drejtoria.Options[1].Text.Trim(), Is.EqualTo("Drejtoria Berat"));
        Assert.That(drejtoria.Options[2].GetAttribute("value"), Is.EqualTo("02"));
        Assert.That(drejtoria.Options[2].Text.Trim(), Is.EqualTo("Drejtoria Diber"));
        Assert.That(drejtoria.Options[3].GetAttribute("value"), Is.EqualTo("03"));
        Assert.That(drejtoria.Options[3].Text.Trim(), Is.EqualTo("Drejtoria Durres"));
        Assert.That(drejtoria.Options[4].GetAttribute("value"), Is.EqualTo("04"));
        Assert.That(drejtoria.Options[4].Text.Trim(), Is.EqualTo("Drejtoria Elbasan"));
        Assert.That(drejtoria.Options[5].GetAttribute("value"), Is.EqualTo("05"));
        Assert.That(drejtoria.Options[5].Text.Trim(), Is.EqualTo("Drejtoria Fier"));
        Assert.That(drejtoria.Options[6].GetAttribute("value"), Is.EqualTo("06"));
        Assert.That(drejtoria.Options[6].Text.Trim(), Is.EqualTo("Drejtoria Gjirokaster"));
        Assert.That(drejtoria.Options[7].GetAttribute("value"), Is.EqualTo("07"));
        Assert.That(drejtoria.Options[7].Text.Trim(), Is.EqualTo("Drejtoria Korçe"));
        Assert.That(drejtoria.Options[8].GetAttribute("value"), Is.EqualTo("08"));
        Assert.That(drejtoria.Options[8].Text.Trim(), Is.EqualTo("Drejtoria Kukes"));
        Assert.That(drejtoria.Options[9].GetAttribute("value"), Is.EqualTo("09"));
        Assert.That(drejtoria.Options[9].Text.Trim(), Is.EqualTo("Drejtoria Lezhe"));
        Assert.That(drejtoria.Options[10].GetAttribute("value"), Is.EqualTo("14"));
        Assert.That(drejtoria.Options[10].Text.Trim(), Is.EqualTo("Dega Sarande"));
        Assert.That(drejtoria.Options[11].GetAttribute("value"), Is.EqualTo("10"));
        Assert.That(drejtoria.Options[11].Text.Trim(), Is.EqualTo("Drejtoria Shkoder"));
        Assert.That(drejtoria.Options[12].GetAttribute("value"), Is.EqualTo("11"));
        Assert.That(drejtoria.Options[12].Text.Trim(), Is.EqualTo("Drejtoria Tirane"));
        Assert.That(drejtoria.Options[13].GetAttribute("value"), Is.EqualTo("13"));
        Assert.That(drejtoria.Options[13].Text.Trim(), Is.EqualTo("Dega Tropoje"));
        Assert.That(drejtoria.Options[14].GetAttribute("value"), Is.EqualTo("12"));
        Assert.That(drejtoria.Options[14].Text.Trim(), Is.EqualTo("Drejtoria Vlore"));

        Log("Assert Agjencia eshte disabled para zgjedhjes se drejtorise");
        IWebElement agencySelect = FindSelectByLabel("Agjencia");
        Assert.That(agencySelect.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(new SelectElement(agencySelect).Options.Count, Is.EqualTo(1));
        Assert.That(new SelectElement(agencySelect).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));

        Log("Assert deklarata e kooperatives bujqesore");
        IWebElement declaration = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector(".ealb-declaration-text")));
        Assert.That(declaration.Displayed, Is.True);

        IWebElement applicantName = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector(".ealb-declaration-text strong")));
        Assert.That(applicantName.Text.Trim(), Is.EqualTo("Katerina Jançe"));

        IWebElement firstParagraph = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[contains(@class,'ealb-declaration-text')]/p[1]")));
        Assert.That(firstParagraph.Text, Does.Contain("Unë i nënshkruari"));
        Assert.That(firstParagraph.Text, Does.Contain("deklaroj se kam punuar në kooperativën bujqësore"));
        Assert.That(firstParagraph.Text, Does.Contain("si punonjës bujqësie, me ditë pune"));
        Assert.That(firstParagraph.Text, Does.Contain("nuk kam marrë asnjë njoftim nga ish-kooperativa bujqësore"));
        Assert.That(firstParagraph.Text, Does.Contain("nuk ka dokumentacion që të vërtetoj periudhën e sigurimit"));

        IWebElement secondParagraph = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[contains(@class,'ealb-declaration-text')]/p[2]")));
        Assert.That(secondParagraph.Text.Trim(),
            Is.EqualTo("Në këto kushte, bazuar në ligjin nr. 169/2020 dhe në vendimin e Këshillit të Ministrave 432, datë 15.07.2021, kërkoj që periudha e sigurimit për efekt të përfitimit nga sigurimet shoqërore të përcaktohet në rrugë administrative nga komisioni i ngritur për këtë qëllim. Për këtë, bazuar në dispozitat e ligjit të sipërshënuar dhe të legjislacionit Shqiptar, lidhur me përgjegjësinë personale, deklaroj si më poshtë:"));

        IWebElement cooperativeInput = FindCooperativeInput();
        Assert.That(cooperativeInput.GetAttribute("value"), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Kliko Vazhdo pa zgjedhur drejtorine dhe agjencine");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error message per Drejtoria Rajonale");
        IWebElement drejtoriaError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//label[contains(.,'Drejtoria Rajonale')]/following-sibling::div[contains(@class,'invalid-feedback')]")));
        Assert.That(drejtoriaError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));
        Assert.That(FindSelectByLabel("Drejtoria Rajonale").GetAttribute("class"),
            Does.Contain("is-invalid"));

        Log("Assert error message per Agjencia");
        IWebElement agencyError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//label[contains(.,'Agjencia')]/following-sibling::div[contains(@class,'invalid-feedback')]")));
        Assert.That(agencyError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));
        Assert.That(FindSelectByLabel("Agjencia").GetAttribute("class"),
            Does.Contain("is-invalid"));

        Log("Zgjidh Drejtoria Tirane");
        SelectDropdownByValue(FindSelectByLabel("Drejtoria Rajonale"), "11");

        Log("Wait qe Agjencia te aktivizohet");
        wait.Until(d =>
        {
            try
            {
                var agency = d.FindElement(
                    By.XPath("//form//label[contains(.,'Agjencia')]/following-sibling::select"));
                return agency.GetAttribute("disabled") == null
                    && new SelectElement(agency).Options.Count > 1;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        IWebElement agencyEnabled = FindSelectByLabel("Agjencia");
        var agencyOptions = new SelectElement(agencyEnabled);
        Assert.That(agencyEnabled.GetAttribute("disabled"), Is.Null);
        Assert.That(agencyOptions.Options.Count, Is.GreaterThan(1));

        Log("Zgjidh Agjencia Kavaje nese ekziston, perndryshe opsionin e pare");
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

        Log("Ploteso emrin e kooperatives bujqesore");
        FillInput(FindCooperativeInput(), "Kooperativa Bujqësore Kavajë");
        Assert.That(FindCooperativeInput().GetAttribute("value").Trim(),
            Is.EqualTo("Kooperativa Bujqësore Kavajë"));

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("TË DHËNAT PERSONALE");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT PERSONALE"));

        Log("Assert tooltip i te dhenave personale");
        IWebElement tooltip = wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector("h4.text-uppercase span[data-bs-toggle='tooltip']")));
        Assert.That(tooltip.GetAttribute("title"),
            Is.EqualTo("Të dhënat e aplikantit plotësohen nga identifikimi juaj në e-albania"));

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
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert seksioni Të dhënat personale aktuale");
        Assert.That(driver.FindElement(
            By.XPath("//form//h5[normalize-space()='Të dhënat personale aktuale']")).Displayed, Is.True);

        Log("Assert te dhenat personale te para-plotesuara dhe disabled");
        IWebElement nidInput = FindInputByLabel("NID");
        Assert.That(nidInput.GetAttribute("value").Trim(), Is.EqualTo(Settings.Qytetar.Username));
        Assert.That(nidInput.GetAttribute("disabled"), Is.Not.Null);

        IWebElement emerInput = FindInputByLabel("Emër");
        Assert.That(emerInput.GetAttribute("value").Trim(), Is.EqualTo("Katerina"));
        Assert.That(emerInput.GetAttribute("disabled"), Is.Not.Null);

        IWebElement atesiaInput = FindInputByLabel("Atësia");
        Assert.That(atesiaInput.GetAttribute("value").Trim(), Is.EqualTo("Foti"));
        Assert.That(atesiaInput.GetAttribute("disabled"), Is.Not.Null);

        IWebElement mbiemerInput = FindInputByLabel("Mbiemër");
        Assert.That(mbiemerInput.GetAttribute("value").Trim(), Is.EqualTo("Jançe"));
        Assert.That(mbiemerInput.GetAttribute("disabled"), Is.Not.Null);

        IWebElement mbiemerParaMarteses = FindInputByLabel("Mbiemër para martesës");
        Assert.That(mbiemerParaMarteses.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(mbiemerParaMarteses.GetAttribute("disabled"), Is.Null);

        IWebElement datelindjaInput = FindInputByLabel("Datëlindja");
        Assert.That(datelindjaInput.GetAttribute("value").Trim(), Is.EqualTo("13.04.1993"));
        Assert.That(datelindjaInput.GetAttribute("disabled"), Is.Not.Null);

        Log("Assert seksionet Vendlindja, Adresa dhe Kontakti");
        Assert.That(driver.FindElement(By.XPath("//form//h5[normalize-space()='Vendlindja']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//form//h5[normalize-space()='Adresa']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//form//h5[normalize-space()='Kontakti']")).Displayed, Is.True);

        Log("Assert fushat e vendlindjes");
        IWebElement fshati = FindSectionInput("Vendlindja", "Fshati");
        IWebElement qyteti = FindSectionInput("Vendlindja", "Qyteti");
        IWebElement rrethi = FindSectionInput("Vendlindja", "Rrethi");
        IWebElement qarku = FindSectionInput("Vendlindja", "Qarku");
        Assert.That(fshati.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(qyteti.GetAttribute("value").Trim(), Is.EqualTo("Korçë"));
        Assert.That(rrethi.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(qarku.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(fshati.GetAttribute("maxlength"), Is.EqualTo("100"));
        Assert.That(qyteti.GetAttribute("maxlength"), Is.EqualTo("100"));
        Assert.That(rrethi.GetAttribute("maxlength"), Is.EqualTo("100"));
        Assert.That(qarku.GetAttribute("maxlength"), Is.EqualTo("100"));

        Log("Assert fushat e adreses");
        IWebElement lagjia = FindSectionInput("Adresa", "Lagjia");
        IWebElement rruga = FindSectionInput("Adresa", "Rruga");
        IWebElement pallati = FindSectionInput("Adresa", "Pallati");
        IWebElement shkalla = FindSectionInput("Adresa", "Shkalla");
        IWebElement qytetiFshati = FindSectionInput("Adresa", "Qyteti/Fshati");
        IWebElement adresaRrethi = FindSectionInput("Adresa", "Rrethi");
        Assert.That(lagjia.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(rruga.GetAttribute("value").Trim(),
            Is.EqualTo("FROSINA PLAKU; Nd. 88; H. 2; Ap. 9; NJËSIA ADMINISTRATIVE NR. 7; NJËSIA BASHKIAKE NR. 7; 1023; TIRANË"));
        Assert.That(pallati.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(shkalla.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(qytetiFshati.GetAttribute("value").Trim(), Is.EqualTo("TIRANË"));
        Assert.That(adresaRrethi.GetAttribute("value").Trim(), Is.EqualTo("TIRANË"));
        Assert.That(lagjia.GetAttribute("maxlength"), Is.EqualTo("100"));
        Assert.That(rruga.GetAttribute("maxlength"), Is.EqualTo("100"));
        Assert.That(pallati.GetAttribute("maxlength"), Is.EqualTo("12"));
        Assert.That(shkalla.GetAttribute("maxlength"), Is.EqualTo("12"));
        Assert.That(qytetiFshati.GetAttribute("maxlength"), Is.EqualTo("100"));
        Assert.That(adresaRrethi.GetAttribute("maxlength"), Is.EqualTo("100"));

        Log("Assert fushat e kontaktit");
        IWebElement nrTel1 = FindSectionInput("Kontakti", "Nr.tel 1");
        IWebElement nrTel2 = FindSectionInput("Kontakti", "Nr.tel 2");
        IWebElement email = FindSectionInput("Kontakti", "Email");
        Assert.That(nrTel1.GetAttribute("type"), Is.EqualTo("tel"));
        Assert.That(nrTel1.GetAttribute("value").Trim(), Is.EqualTo("+355697008820"));
        Assert.That(nrTel1.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(nrTel2.GetAttribute("type"), Is.EqualTo("tel"));
        Assert.That(nrTel2.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(email.GetAttribute("type"), Is.EqualTo("email"));
        Assert.That(email.GetAttribute("value").Trim(), Is.EqualTo("katerina.jance@kreatx.com"));
        Assert.That(email.GetAttribute("disabled"), Is.Not.Null);

        Log("Assert butonat e navigimit Step 2");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Ploteso Vendlindja");
        FillInput(FindSectionInput("Vendlindja", "Fshati"), "Kavajë");
        FillInput(FindSectionInput("Vendlindja", "Rrethi"), "Kavajë");
        FillInput(FindSectionInput("Vendlindja", "Qarku"), "Tiranë");

        Log("Ploteso Adresa");
        FillInput(FindSectionInput("Adresa", "Lagjia"), "1");
        FillInput(FindSectionInput("Adresa", "Pallati"), "1");
        FillInput(FindSectionInput("Adresa", "Shkalla"), "2");

        Log("Ploteso Nr.tel 2");
        FillInput(FindSectionInput("Kontakti", "Nr.tel 2"), "0676041404");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle(
            "TË DHËNA LIDHUR ME NDRYSHIMIN E GJENERALITETEVE (NDRYSHIM EMRI, MBIEMRI, EMRI I BABAIT)");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNA LIDHUR ME NDRYSHIMIN E GJENERALITETEVE (NDRYSHIM EMRI, MBIEMRI, EMRI I BABAIT)"));

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
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert kolonat e tabeles se gjeneraliteteve");
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Data e ndryshimit']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Emri']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Atësia']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Mbiemër']")).Displayed, Is.True);

        Log("Assert tabela eshte bosh");
        IWebElement emptyRow = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//form//table//td[@colspan='5']")));
        Assert.That(emptyRow.Text.Trim(), Is.EqualTo(string.Empty));

        Log("Assert butoni + Shto");
        IWebElement shtoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-button-open-modal")));
        Assert.That(shtoBtn.Text.Trim(), Is.EqualTo("+ Shto"));

        Log("Assert butonat e navigimit Step 3");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Kliko + Shto");
        SafeClick(By.CssSelector("button.ealb-button-open-modal"));
        WaitForModalTitle("Të dhënat e vendbanimit");

        Log("Assert fushat e detyrueshme te modalit");
        Assert.That(FindModalInput("Emri").GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindModalInput("Atësia").GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindModalInput("Mbiemër").GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindModalInput("Data e ndryshimit").GetAttribute("value"),
            Is.EqualTo(string.Empty).Or.EqualTo(null));
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'custom-modal-content')]//label[contains(.,'Emri')]//span[contains(@class,'custom-modal-asterisk')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'custom-modal-content')]//label[contains(.,'Atësia')]//span[contains(@class,'custom-modal-asterisk')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'custom-modal-content')]//label[contains(.,'Mbiemër')]//span[contains(@class,'custom-modal-asterisk')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'custom-modal-content')]//label[contains(.,'Data e ndryshimit')]//span[contains(@class,'custom-modal-asterisk')]")).Displayed, Is.True);

        Log("Kliko Anulo ne modal");
        ClickModalFooter("Anulo");
        WaitForModalClosed();

        Log("Kliko + Shto perseri");
        SafeClick(By.CssSelector("button.ealb-button-open-modal"));
        WaitForModalTitle("Të dhënat e vendbanimit");

        Log("Kliko Ruaj pa plotesuar fushat e detyrueshme");
        ClickModalFooter("Ruaj");
        AssertModalFieldInvalid(FindModalInput("Emri"));
        AssertModalFieldInvalid(FindModalInput("Atësia"));
        AssertModalFieldInvalid(FindModalInput("Mbiemër"));
        AssertModalFieldInvalid(FindModalInput("Data e ndryshimit"));

        Log("Ploteso ndryshimin e gjeneraliteteve");
        FillInput(FindModalInput("Emri"), "Katerina");
        FillInput(FindModalInput("Atësia"), "Foti");
        FillInput(FindModalInput("Mbiemër"), "Hoxha");
        FillModalDate("Data e ndryshimit", "2015-01-01", "01.01.2015");

        Log("Kliko Ruaj");
        ClickModalFooter("Ruaj");
        WaitForModalClosed();
        Thread.Sleep(1000);

        Log("Assert rreshti i ndryshimit ne tabele");
        IWebElement changeRow = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//table//tbody/tr[not(.//td[@colspan])]")));
        var changeCells = changeRow.FindElements(By.TagName("td"));
        Assert.That(changeCells.Count, Is.GreaterThanOrEqualTo(4));
        Assert.That(changeCells[0].Text.Trim(), Does.Contain("01.01.2015").Or.Contain("2015-01-01"));
        Assert.That(changeCells[1].Text.Trim(), Is.EqualTo("Katerina"));
        Assert.That(changeCells[2].Text.Trim(), Is.EqualTo("Foti"));
        Assert.That(changeCells[3].Text.Trim(), Is.EqualTo("Hoxha"));

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 4 Title");
        IWebElement Step4Title = WaitForStepTitle(
            "TË DHËNA LIDHUR ME VENDBANIMIN (NDRYSHIMET E VENDBANIMIT (NËSE KA)");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNA LIDHUR ME VENDBANIMIN (NDRYSHIMET E VENDBANIMIT (NËSE KA)"));

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
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert kolonat e tabeles se vendbanimit");
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Vendbanimi/periudha']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Nga']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Deri']")).Displayed, Is.True);

        Log("Assert tabela e vendbanimit eshte bosh");
        IWebElement emptyResidenceRow = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//form//table//td[@colspan='4']")));
        Assert.That(emptyResidenceRow.Text.Trim(), Is.EqualTo(string.Empty));

        Log("Assert butoni + Shto Step 4");
        shtoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-button-open-modal")));
        Assert.That(shtoBtn.Text.Trim(), Is.EqualTo("+ Shto"));

        Log("Assert shenimin e vendbanimit");
        IWebElement shenim = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//p[contains(.,'Shënim')]")));
        Assert.That(shenim.Text.Trim(), Is.EqualTo("Shënim:"));
        Assert.That(driver.FindElement(
            By.XPath("//form//li[contains(.,'në rreshtin \"vendbanimi\" shënohet emri i rrethit, emri i fshatit')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//form//li[contains(.,'në rreshtin \"periudha\", shënohet periudha e banimit fillim mbarim me datë, muaj, vit')]")).Displayed, Is.True);

        Log("Assert butonat e navigimit Step 4");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Kliko + Shto vendbanim");
        SafeClick(By.CssSelector("button.ealb-button-open-modal"));
        WaitForModalTitle("Të dhënat e vendbanimit");

        Log("Assert fushat e modalit te vendbanimit");
        Assert.That(FindModalInput("Vendbanimi/periudha").GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindModalInput("Nga").GetAttribute("value"),
            Is.EqualTo(string.Empty).Or.EqualTo(null));
        Assert.That(FindModalInput("Deri").GetAttribute("value"),
            Is.EqualTo(string.Empty).Or.EqualTo(null));
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'custom-modal-content')]//label[contains(.,'Vendbanimi/periudha')]//span[contains(@class,'custom-modal-asterisk')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'custom-modal-content')]//label[contains(.,'Nga')]//span[contains(@class,'custom-modal-asterisk')]")).Displayed, Is.True);
        Assert.That(driver.FindElements(
            By.XPath("//div[contains(@class,'custom-modal-content')]//label[contains(.,'Deri')]//span[contains(@class,'custom-modal-asterisk')]")).Count,
            Is.EqualTo(0));

        Log("Kliko Anulo ne modalin e vendbanimit");
        ClickModalFooter("Anulo");
        WaitForModalClosed();

        Log("Kliko + Shto vendbanim perseri");
        SafeClick(By.CssSelector("button.ealb-button-open-modal"));
        WaitForModalTitle("Të dhënat e vendbanimit");

        Log("Kliko Ruaj pa plotesuar fushat e detyrueshme te vendbanimit");
        ClickModalFooter("Ruaj");
        AssertModalFieldInvalid(FindModalInput("Vendbanimi/periudha"));
        AssertModalFieldInvalid(FindModalInput("Nga"));

        Log("Ploteso vendbanimin");
        FillInput(FindModalInput("Vendbanimi/periudha"), "Kavajë");
        FillModalDate("Nga", "2020-01-01", "01.01.2020");
        FillModalDate("Deri", "2024-12-31", "31.12.2024");

        Log("Kliko Ruaj vendbanimin");
        ClickModalFooter("Ruaj");
        WaitForModalClosed();
        Thread.Sleep(1000);

        Log("Assert rreshti i vendbanimit ne tabele");
        IWebElement residenceRow = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//table//tbody/tr[not(.//td[@colspan])]")));
        var residenceCells = residenceRow.FindElements(By.TagName("td"));
        Assert.That(residenceCells.Count, Is.GreaterThanOrEqualTo(3));
        Assert.That(residenceCells[0].Text.Trim(), Is.EqualTo("Kavajë"));
        Assert.That(residenceCells[1].Text.Trim(), Does.Contain("01.01.2020").Or.Contain("2020-01-01"));
        Assert.That(residenceCells[2].Text.Trim(), Does.Contain("31.12.2024").Or.Contain("2024-12-31"));

        Log("Kliko Vazhdo Step 4");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 5 Title");
        IWebElement Step5Title = WaitForStepTitle(
            "TË DHËNA LIDHUR ME PERIUDHËN E PUNËS NË ISH-KOOPERATIVË BUJQËSORE");
        Assert.That(Step5Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNA LIDHUR ME PERIUDHËN E PUNËS NË ISH-KOOPERATIVË BUJQËSORE"));

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
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert teksti i ligjit per ish-kooperativen");
        IWebElement lawText = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//small[contains(.,'Ligjin nr. 169/2020')]")));
        Assert.That(lawText.Text.Trim(),
            Is.EqualTo("Bazuar në Ligjin nr. 169/2020, datë 23.12.2020 \"Për njohjen si periudhë sigurimi për efekt përfitimi nga sigurimet shoqërore të kohës së punësimit në ish-kooperativat bujqësore\", deklaroj nën përgjegjësinë time se kam punuar dhe kam qenë banor i territorit në të cilin ka shtrirë aktivitetin e saj ish-kooperativa bujqësore si më poshtë vijon:"));

        Log("Assert kolonat e tabeles se periudhes se punes");
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Nr.']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Ish-kooperativa']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Fshati']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Rrethi']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Sektori']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Brigada']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Data e fillimit të punës']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Data e largimit të punës']")).Displayed, Is.True);

        Log("Assert tabela e periudhes se punes eshte bosh");
        IWebElement emptyWorkRow = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//form//table//td[@colspan='9']")));
        Assert.That(emptyWorkRow.Text.Trim(), Is.EqualTo(string.Empty));

        Log("Assert butoni + Shto Step 5");
        shtoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-button-open-modal")));
        Assert.That(shtoBtn.Text.Trim(), Is.EqualTo("+ Shto"));

        Log("Assert butonat e navigimit Step 5");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Kliko + Shto periudhe pune");
        SafeClick(By.CssSelector("button.ealb-button-open-modal"));
        WaitForModalTitle("Të dhënat e periudhës së punës");

        Log("Assert fushat e detyrueshme te modalit te punes");
        Assert.That(FindModalInput("Ish-kooperativa").GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindModalInput("Fshati").GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindModalInput("Rrethi").GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindModalInput("Sektori").GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindModalInput("Brigada").GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindModalInput("Data e fillimit të punës").GetAttribute("value"),
            Is.EqualTo(string.Empty).Or.EqualTo(null));
        Assert.That(FindModalInput("Data e largimit të punës").GetAttribute("value"),
            Is.EqualTo(string.Empty).Or.EqualTo(null));
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'custom-modal-content')]//label[contains(.,'Ish-kooperativa')]//span[contains(@class,'custom-modal-asterisk')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'custom-modal-content')]//label[contains(.,'Fshati')]//span[contains(@class,'custom-modal-asterisk')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'custom-modal-content')]//label[contains(.,'Rrethi')]//span[contains(@class,'custom-modal-asterisk')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'custom-modal-content')]//label[contains(.,'Sektori')]//span[contains(@class,'custom-modal-asterisk')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'custom-modal-content')]//label[contains(.,'Brigada')]//span[contains(@class,'custom-modal-asterisk')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'custom-modal-content')]//label[contains(.,'Data e fillimit të punës')]//span[contains(@class,'custom-modal-asterisk')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//div[contains(@class,'custom-modal-content')]//label[contains(.,'Data e largimit të punës')]//span[contains(@class,'custom-modal-asterisk')]")).Displayed, Is.True);

        Log("Kliko Anulo ne modalin e periudhes se punes");
        ClickModalFooter("Anulo");
        WaitForModalClosed();

        Log("Kliko + Shto periudhe pune perseri");
        SafeClick(By.CssSelector("button.ealb-button-open-modal"));
        WaitForModalTitle("Të dhënat e periudhës së punës");

        Log("Kliko Ruaj pa plotesuar fushat e detyrueshme te punes");
        ClickModalFooter("Ruaj");
        AssertModalFieldInvalid(FindModalInput("Ish-kooperativa"));
        AssertModalFieldInvalid(FindModalInput("Fshati"));
        AssertModalFieldInvalid(FindModalInput("Rrethi"));
        AssertModalFieldInvalid(FindModalInput("Sektori"));
        AssertModalFieldInvalid(FindModalInput("Brigada"));
        AssertModalFieldInvalid(FindModalInput("Data e fillimit të punës"));
        AssertModalFieldInvalid(FindModalInput("Data e largimit të punës"));

        Log("Ploteso periudhen e punes");
        FillInput(FindModalInput("Ish-kooperativa"), "Kooperativa Bujqësore Kavajë");
        FillInput(FindModalInput("Fshati"), "Kavajë");
        FillInput(FindModalInput("Rrethi"), "Kavajë");
        FillInput(FindModalInput("Sektori"), "1");
        FillInput(FindModalInput("Brigada"), "2");
        FillModalDate("Data e fillimit të punës", "1985-01-01", "01.01.1985");
        FillModalDate("Data e largimit të punës", "1991-12-31", "31.12.1991");

        Log("Kliko Ruaj periudhen e punes");
        ClickModalFooter("Ruaj");
        WaitForModalClosed();
        Thread.Sleep(1000);

        Log("Assert rreshti i periudhes se punes ne tabele");
        IWebElement workRow = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//table//tbody/tr[not(.//td[@colspan])]")));
        var workCells = workRow.FindElements(By.TagName("td"));
        Assert.That(workCells.Count, Is.GreaterThanOrEqualTo(8));
        Assert.That(workCells[0].Text.Trim(), Is.EqualTo(string.Empty));
        Assert.That(workCells[1].Text.Trim(), Is.EqualTo("Kooperativa Bujqësore Kavajë"));
        Assert.That(workCells[2].Text.Trim(), Is.EqualTo("Kavajë"));
        Assert.That(workCells[3].Text.Trim(), Is.EqualTo("Kavajë"));
        Assert.That(workCells[4].Text.Trim(), Is.EqualTo("1"));
        Assert.That(workCells[5].Text.Trim(), Is.EqualTo("2"));
        Assert.That(workCells[6].Text.Trim(), Does.Contain("01.01.1985").Or.Contain("1985-01-01"));
        Assert.That(workCells[7].Text.Trim(), Does.Contain("31.12.1991").Or.Contain("1991-12-31"));

        Log("Kliko Vazhdo Step 5");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 6 Title");
        IWebElement Step6Title = WaitForStepTitle(
            "DEKLARIME PËR ÇËSHTJE DHE TË DHËNA TË TJERA, TË PËRCAKTUARA NË LIGJIN NR. 169/2020");
        Assert.That(Step6Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DEKLARIME PËR ÇËSHTJE DHE TË DHËNA TË TJERA, TË PËRCAKTUARA NË LIGJIN NR. 169/2020"));

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
        Assert.That(steps[6].GetAttribute("class"), Does.Contain("no-click"));

        Log("Assert checkbox-et e deklarimeve jane te pazgjedhura");
        IWebElement prisonSentence = wait.Until(ExpectedConditions.ElementExists(By.Id("prisonSentence")));
        IWebElement disabilityPension = wait.Until(ExpectedConditions.ElementExists(By.Id("disabilityPension")));
        IWebElement militaryService = wait.Until(ExpectedConditions.ElementExists(By.Id("militaryService")));
        IWebElement primaryEducation = wait.Until(ExpectedConditions.ElementExists(By.Id("primaryEducation")));
        IWebElement secondaryEducation = wait.Until(ExpectedConditions.ElementExists(By.Id("secondaryEducation")));

        Assert.That(prisonSentence.GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(disabilityPension.GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(militaryService.GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(primaryEducation.GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(secondaryEducation.GetAttribute("type"), Is.EqualTo("checkbox"));

        Assert.That(prisonSentence.Selected, Is.False);
        Assert.That(disabilityPension.Selected, Is.False);
        Assert.That(militaryService.Selected, Is.False);
        Assert.That(primaryEducation.Selected, Is.False);
        Assert.That(secondaryEducation.Selected, Is.False);

        Log("Assert label-at e deklarimeve");
        Assert.That(driver.FindElement(By.CssSelector("label[for='prisonSentence']")).Text.Trim(),
            Is.EqualTo("Kam qenë duke vuajtur dënimin me heqje lirie."));
        Assert.That(driver.FindElement(By.CssSelector("label[for='disabilityPension']")).Text.Trim(),
            Is.EqualTo("Kam qenë përfitues i pensionit të invaliditetit të plotë."));
        Assert.That(driver.FindElement(By.CssSelector("label[for='militaryService']")).Text.Trim(),
            Is.EqualTo("Kam kryer shërbimin e detyrueshëm ushtarak."));
        Assert.That(driver.FindElement(By.CssSelector("label[for='primaryEducation']")).Text.Trim(),
            Is.EqualTo("Kam frekuentuar arsimin 7/8 vjeçar."));
        Assert.That(driver.FindElement(By.CssSelector("label[for='secondaryEducation']")).Text.Trim(),
            Is.EqualTo("Kam frekuentuar arsimin e mesëm nga data."));

        Log("Assert butonat e navigimit Step 6");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Kliko Vazhdo Step 6");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 7 Title");
        IWebElement Step7Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step7Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 7");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("5 minuta kohëzgjatje"));

        Log("Assert 7 hapa, te gjithe aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(7));
        foreach (var step in steps)
            Assert.That(step.GetAttribute("class"), Does.Contain("active"));

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës')]")).Displayed, Is.True);

        Log("Assert document-upload Deklarate e nenshkruar");
        AssertDocumentUpload("doc1Upload14928", "Deklaratë të nënshkruar (sipas formatit)");

        Log("Assert document-upload Vertetim toke");
        AssertDocumentUpload("doc2Upload14928", "Vërtetim nëse ka përfituar tokë");

        Log("Assert document-upload Libreze pune");
        AssertDocumentUpload("doc3Upload14928", "Kopje të noterizuar e librezës së punës");

        Log("Assert document-upload Deftese shkolle");
        AssertDocumentUpload("doc4Upload14928", "Kopje e dëftesës së shkollës tetëvjeçare ose të mesme");

        Log("Assert document-upload Dokumente te tjere");
        AssertDocumentUpload("doc5Upload14928", "Dokumente të tjerë që zotërohen nga kërkuesi");

        Log("Assert dokumentet e administrates");
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'Certifikatë familjare')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'shërbimit të detyrueshëm ushtarak')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'sistemin DMAIS')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'pension invaliditeti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'Drejtoria e Burgjeve')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'19.07.1991')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'datën e largimit nga territori')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'Certifikatë martese')]")).Displayed, Is.True);

        Log("Assert emri i kerkuesit eshte disabled");
        Assert.That(driver.FindElement(By.XPath("//h6[normalize-space()='Kërkuesi']")).Displayed, Is.True);
        IWebElement kerkuesiInput = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//h6[normalize-space()='Kërkuesi']/following::input[1]")));
        Assert.That(kerkuesiInput.GetAttribute("value").Trim(), Is.EqualTo("Katerina Jançe"));
        Assert.That(kerkuesiInput.GetAttribute("disabled"), Is.Not.Null);

        Log("Assert checkbox i pranimit eshte i pazgjedhur");
        IWebElement agreeCheck = wait.Until(ExpectedConditions.ElementExists(By.Id("agreeCheck")));
        Assert.That(agreeCheck.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='agreeCheck']")).Text.Trim(),
            Is.EqualTo("Mbledhja e dokumentacionit shoqërues të mësipërm që më parë ishte detyrim të dorëzohej në zyrat e shtetit nga vetë aplikanti, tani është detyrë e nëpunësit të administratës ndaj qytetarit. Me klikimin e këtij butoni, ju bini dakord që këto dokumente të sigurohen për ju nga nëpunësi i administratës."));

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
        //Assert.That(referenceNumber.Text, Does.Contain("14928-"));
        //Assert.That(driver.Url, Does.Contain("/mesazh"));

        Log("TEST PASSED");
    }

    private IWebElement FindSelectByLabel(string labelPart)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//form//label[contains(.,'{labelPart}')]/following-sibling::select")));
    }

    private IWebElement FindCooperativeInput()
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector(".ealb-declaration-text input")));
    }

    private IWebElement FindInputByLabel(string labelText)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//form//label[normalize-space()='{labelText}']/following-sibling::input")));
    }

    private IWebElement FindSectionInput(string sectionTitle, string labelText)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//form//h5[normalize-space()='{sectionTitle}']/following-sibling::div[1]//label[normalize-space()='{labelText}']/following-sibling::input")));
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

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            input
        );

        var parts = isoDate.Split('-');
        int year = int.Parse(parts[0]);
        int month = int.Parse(parts[1]);
        int day = int.Parse(parts[2]);

        string type = input.GetAttribute("type") ?? string.Empty;
        bool isDate = type.Equals("date", StringComparison.OrdinalIgnoreCase);
        string valueToSet = isDate ? isoDate : displayDate;

        ((IJavaScriptExecutor)driver).ExecuteScript(@"
            const el = arguments[0];
            const value = arguments[1];
            const year = Number(arguments[2]);
            const month = Number(arguments[3]);
            const day = Number(arguments[4]);
            const date = new Date(year, month - 1, day);
            const group = el.closest('.form-group') || el.parentElement;
            const inputs = [el, ...group.querySelectorAll('input')];
            const fpInput = inputs.find(i => i._flatpickr);
            if (fpInput && fpInput._flatpickr) {
                fpInput._flatpickr.setDate(date, true);
                fpInput._flatpickr.close();
            }
            const setter = Object.getOwnPropertyDescriptor(window.HTMLInputElement.prototype, 'value').set;
            inputs.forEach(i => {
                const nextValue = (i.getAttribute('type') === 'date')
                    ? `${year}-${String(month).padStart(2,'0')}-${String(day).padStart(2,'0')}`
                    : value;
                setter.call(i, nextValue);
                i.dispatchEvent(new Event('input', { bubbles: true }));
                i.dispatchEvent(new Event('change', { bubbles: true }));
            });
        ", input, valueToSet, year, month, day);

        Thread.Sleep(300);
    }

    private void FillModalDate(string labelPart, string isoDate, string displayDate)
    {

        SetDateValue(FindModalInput(labelPart), isoDate, displayDate);
        wait.Until(d =>
        {
            try
            {
                string current = FindModalInput(labelPart).GetAttribute("value") ?? string.Empty;
                return current.Contains(displayDate) || current.Contains(isoDate);
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });
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
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-14928"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("25"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("25"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf,.jpg,.jpeg,.png"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të ngarkuar dokument"));

        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të ngarkuar dokument"));
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
            Is.EqualTo("Aplikim për njohjen si periudhë sigurimi për efekt përfitimi nga sigurimet shoqërore të kohës së punësimit në ish-kooperativat bujqësore sipas ligjit nr. 169/2020 dhe VKM nr.432/2021"),
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