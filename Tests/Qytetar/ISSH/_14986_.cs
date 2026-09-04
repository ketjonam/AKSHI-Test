using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("14986")]
public class _14986_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "14986";
    protected override string? ServiceTitle => "RimbursimShpenzimeshKarburanti";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    [Test]
    public void RimbursimShpenzimeshKarburanti()
    {
        OpenNewApplicationFromServicePage(
            "Kërkesë për rimbursim të shpenzimeve për blerjen e karburanteve dhe vajrave lubrifikantë");

        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT E VENDNDODHJES"));

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 5 hapa, hapi i pare aktiv");
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(5));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("no-click"));
        for (int i = 1; i < steps.Count; i++)
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert label-at e vendndodhjes");
        IWebElement drejtoriaLabel = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//label[contains(.,'DREJTORIA RAJONALE')]")));
        Assert.That(drejtoriaLabel.Text, Does.Contain("DREJTORIA RAJONALE"));
        Assert.That(drejtoriaLabel.Text, Does.Contain("*"));

        IWebElement agencyLabel = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//label[contains(.,'AGJENCIA')]")));
        Assert.That(agencyLabel.Text, Does.Contain("AGJENCIA"));
        Assert.That(agencyLabel.Text, Does.Contain("*"));

        Log("Assert Drejtoria Rajonale ka opsionet e drejtorive");
        IWebElement drsshSelect = wait.Until(ExpectedConditions.ElementExists(By.Id("drssh")));
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

        Log("Assert Agjencia eshte disabled para zgjedhjes se drejtorise");
        IWebElement agencySelect = wait.Until(ExpectedConditions.ElementExists(By.Id("agency")));
        Assert.That(agencySelect.GetAttribute("disabled"), Is.Not.Null);
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

        Log("Kliko Vazhdo pa zgjedhur drejtorine dhe agjencine");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error message per drejtorine");
        IWebElement drsshError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//select[@id='drssh']/following-sibling::div[contains(@class,'text-danger')]")));
        Assert.That(drsshError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));
        Assert.That(driver.FindElement(By.Id("drssh")).GetAttribute("class"),
            Does.Contain("is-invalid"));

        Log("Assert error message per agjencine");
        IWebElement agencyError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//select[@id='agency']/following-sibling::div[contains(@class,'text-danger')]")));
        Assert.That(agencyError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));
        Assert.That(driver.FindElement(By.Id("agency")).GetAttribute("class"),
            Does.Contain("is-invalid"));

        Log("Zgjidh Drejtoria Tirane");
        SelectDropdownByValue(wait.Until(ExpectedConditions.ElementExists(By.Id("drssh"))), "11");

        Log("Wait qe Agjencia te aktivizohet");
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

        IWebElement agencyEnabled = driver.FindElement(By.Id("agency"));
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

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("TË DHËNAT PERSONALE AKTUALE");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT PERSONALE AKTUALE"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 5 hapa, dy te paret aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(5));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        for (int i = 2; i < steps.Count; i++)
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert label-at e te dhenave personale");
        AssertLabel("nid", "NID");
        AssertLabel("dateOfBirth", "Datëlindja");
        AssertLabel("firstName", "Emër");
        AssertLabel("fatherName", "Atësia");
        AssertLabel("lastName", "Mbiemër");
        AssertLabel("maidenName", "Mbiemër para martesës");

        Log("Assert te dhenat personale te para-plotesuara");
        AssertReadonlyField("nid", Settings.Qytetar.Username);
        Assert.That(FindFieldById("nid").GetAttribute("maxlength"), Is.EqualTo("10"));
        AssertReadonlyField("dateOfBirth", "13.04.1993");
        AssertReadonlyField("firstName", "Katerina");
        AssertReadonlyField("fatherName", "Foti");
        AssertReadonlyField("lastName", "Jançe");
        AssertEditableField("maidenName", string.Empty);

        Log("Assert seksioni Vendlindja");
        Assert.That(driver.FindElement(
            By.XPath("//h4[normalize-space()='Vendlindja']")).Displayed, Is.True);
        AssertLabel("vendlindjaFshati", "Fshati");
        AssertLabel("vendlindjaQyteti", "Qyteti");
        AssertLabel("vendlindjaRrethi", "Rrethi");
        AssertLabel("vendlindjaQarku", "Qarku");
        AssertEditableField("vendlindjaFshati", string.Empty);
        AssertEditableField("vendlindjaQyteti", "Korçë");
        AssertEditableField("vendlindjaRrethi", string.Empty);
        AssertEditableField("vendlindjaQarku", string.Empty);

        Log("Assert seksioni Adresa");
        Assert.That(driver.FindElement(
            By.XPath("//h4[normalize-space()='Adresa']")).Displayed, Is.True);
        AssertLabel("adresaLagja", "Lagjia");
        AssertLabel("adresaPallati", "Pallati");
        AssertLabel("adresaShkalla", "Shkalla");
        AssertLabel("city", "Qyteti/Fshati");
        AssertLabel("district", "Rrethi");
        AssertLabel("address", "Rruga");
        AssertEditableField("adresaLagja", string.Empty);
        AssertEditableField("adresaPallati", string.Empty);
        AssertEditableField("adresaShkalla", string.Empty);
        AssertEditableField("city", "TIRANË");
        AssertEditableField("district", "TIRANË");

        IWebElement rrugaInput = FindFieldById("address");
        Assert.That(rrugaInput.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(rrugaInput.GetAttribute("value").Trim(),
            Is.EqualTo("FROSINA PLAKU; Nd. 88; H. 2; Ap. 9; NJËSIA ADMINISTRATIVE NR. 7; NJËSIA BASHKIAKE NR. 7; 1023; TIRANË"));

        Log("Assert seksioni Të dhënat e kontaktit");
        Assert.That(driver.FindElement(
            By.XPath("//h4[normalize-space()='Të dhënat e kontaktit']")).Displayed, Is.True);
        AssertLabel("phoneNumber", "Nr. tel 1");
        AssertLabel("phoneNumber2", "Nr. tel 2");
        AssertLabel("email", "Email");
        AssertReadonlyField("phoneNumber", "+355697008820");
        AssertEditableField("phoneNumber2", string.Empty);
        AssertReadonlyField("email", "katerina.jance@kreatx.com");
        Assert.That(FindFieldById("email").GetAttribute("type"), Is.EqualTo("email"));

        Log("Assert seksioni Të dhënat e lejes së drejtimit");
        Assert.That(driver.FindElement(
            By.XPath("//h4[normalize-space()='Të dhënat e lejes së drejtimit.']")).Displayed, Is.True);
        AssertLabel("nrLejedrejtimit", "Nr. lejes së drejtimit");
        AssertLabel("kategoria", "Kategoria");
        AssertLabel("dtLeshimit", "Data e lëshimit");
        AssertLabel("afatiVlefshmerise", "Afati i vlefshmërisë");
        AssertDisabledField("nrLejedrejtimit", string.Empty);
        AssertDisabledField("kategoria", string.Empty);
        AssertDisabledField("dtLeshimit", string.Empty);
        AssertDisabledField("afatiVlefshmerise", string.Empty);

        Log("Assert butonat e navigimit Step 2");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Ploteso Vendlindja");
        FillInput(FindFieldById("vendlindjaFshati"), "Korçë");
        FillInput(FindFieldById("vendlindjaRrethi"), "Korçë");
        FillInput(FindFieldById("vendlindjaQarku"), "Korçë");

        Log("Ploteso Adresa");
        FillInput(FindFieldById("adresaLagja"), "1");
        FillInput(FindFieldById("adresaPallati"), "1");
        FillInput(FindFieldById("adresaShkalla"), "2");

        Log("Ploteso Nr. tel 2");
        FillInput(FindFieldById("phoneNumber2"), "0697008820");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("KËRKESA PËR PËRFITIM");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("KËRKESA PËR PËRFITIM"));

        Log("Assert kohëzgjatja Step 3");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 5 hapa, tre te paret aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(5));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Contain("active"));
        for (int i = 3; i < steps.Count; i++)
        {
            Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert teksti i kerkeses per perfitim");
        IWebElement kerkesaText = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//b[contains(.,'Parashtroj kërkesën për përfitim')]")));
        Assert.That(kerkesaText.Text.Trim(), Is.EqualTo("Parashtroj kërkesën për përfitim:"));

        Log("Assert kolonat e tabeles se perfitimit");
        Assert.That(driver.FindElement(
            By.XPath("//th[normalize-space()='LLOJI I PËRFITIMIT']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//th[normalize-space()='PO/JO']")).Displayed, Is.True);

        Log("Assert rreshti i rimburimit");
        IWebElement rimburimRow = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//td[contains(.,'Rimbursim të shpenzimeve për blerjen e karburanteve dhe vajrave lubrifikantë')]")));
        Assert.That(rimburimRow.Text.Trim(),
            Is.EqualTo("Rimbursim të shpenzimeve për blerjen e karburanteve dhe vajrave lubrifikantë"));

        Log("Assert opsionet PO/JO, PO i zgjedhur");
        IWebElement rimbursimPo = wait.Until(ExpectedConditions.ElementExists(By.Id("rimbursimPo")));
        IWebElement rimbursimJo = wait.Until(ExpectedConditions.ElementExists(By.Id("rimbursimJo")));
        Assert.That(rimbursimPo.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(rimbursimPo.GetAttribute("name"), Is.EqualTo("rimbursim"));
        Assert.That(rimbursimPo.GetAttribute("value"), Is.EqualTo("rimbursimPo"));
        Assert.That(rimbursimPo.Selected, Is.True);
        Assert.That(rimbursimJo.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(rimbursimJo.GetAttribute("name"), Is.EqualTo("rimbursim"));
        Assert.That(rimbursimJo.GetAttribute("value"), Is.EqualTo("rimbursimJo"));
        Assert.That(rimbursimJo.Selected, Is.False);
        Assert.That(driver.FindElement(
            By.XPath("//input[@id='rimbursimPo']/following-sibling::span")).Text.Trim(),
            Is.EqualTo("PO"));
        Assert.That(driver.FindElement(
            By.XPath("//input[@id='rimbursimJo']/following-sibling::span")).Text.Trim(),
            Is.EqualTo("JO"));

        Log("Assert butonat e navigimit Step 3");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Kliko Vazhdo Step 3 me PO te zgjedhur");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 4 Title");
        IWebElement Step4Title = WaitForStepTitle("DEKLARATA");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("DEKLARATA"));

        Log("Assert kohëzgjatja Step 4");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 5 hapa, kater te paret aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(5));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[3].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[4].GetAttribute("class"), Does.Not.Contain("active"));
        Assert.That(steps[4].GetAttribute("class"), Does.Contain("no-click"));

        Log("Assert teksti i deklarates");
        IWebElement deklarataText = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//p[contains(.,'Deklaroj se përfitoj pension')]")));
        Assert.That(deklarataText.Text.Trim(),
            Is.EqualTo("Deklaroj se përfitoj pension apo trajtim të veçantë financiar me numër:"));

        Log("Assert rreshtat e ligjeve jane te pazgjedhura dhe numrat disabled");
        AssertDeclarationRow("ligji7703", ", nga Ligji nr. 7703, datë 11.05.1993, i ndryshuar");
        AssertDeclarationRow("ligji10142", ", nga Ligji nr. 10 142, datë 15.05.2009, i ndryshuar");
        AssertDeclarationRow("ligji29_2019", ", nga Ligji nr. 29/2019, datë 23.5.2019");

        Log("Assert butonat e navigimit Step 4");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Zgjidh Ligji 7703");
        ClickCheckbox("ligji7703");

        Log("Wait qe numri i pensionit te aktivizohet");
        wait.Until(d =>
        {
            try
            {
                var numberInput = d.FindElement(
                    By.XPath("//input[@id='ligji7703']/preceding-sibling::input[@type='text']"));
                return numberInput.GetAttribute("disabled") == null;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        IWebElement pensionNumber = FindPensionNumberInput("ligji7703");
        Assert.That(pensionNumber.GetAttribute("disabled"), Is.Null);
        FillInput(pensionNumber, "123456");
        Assert.That(FindPensionNumberInput("ligji7703").GetAttribute("value").Trim(),
            Is.EqualTo("123456"));

        Log("Kliko Vazhdo Step 4");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 5 Title");
        IWebElement Step5Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step5Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 5");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 5 hapa, te gjithe aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(5));
        foreach (var step in steps)
        {
            Assert.That(step.GetAttribute("class"), Does.Contain("active"));
            Assert.That(step.GetAttribute("class"), Does.Contain("no-click"));
        }

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës')]")).Displayed, Is.True);

        Log("Assert document-upload Akti i blerjes");
        AssertDocumentUpload(
            "aktiBlerjes-upload-14986",
            "Akti i blerjes apo i dhurimit të mjetit sipas legjislacionit në fuqi të shteteve përkatëse",
            ".pdf",
            "Formatet e lejuara: PDF. Madhësia maksimale: 25MB.");

        Log("Assert document-upload Dokumenti i cregjistrimit");
        AssertDocumentUpload(
            "dokCrregjistrimi-upload-14986",
            "Dokumenti i çregjistrimit dhe nxjerrjes jashtë qarkullimit në vendin e origjinës",
            ".pdf",
            "Formatet e lejuara: PDF. Madhësia maksimale: 25MB.");

        Log("Assert document-upload Dokumente te tjera te mjetit");
        AssertDocumentUpload("dokTjera-upload-14986", "Dokumente të tjera të mjetit");

        Log("Assert document-upload Prokure e posacme");
        AssertDocumentUpload(
            "prokure-upload-14986",
            "Prokurë e posaçme e hartuar sipas kërkesave të Kodit Civil");

        Log("Assert dokumentet e administrates");
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'Vërtetim nga Shërbimi Social Shtetëror (Drejtoria Rajonale)')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'Certifikata e pronësisë së mjetit (lëshuar nga autoriteti kompetent')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//li[contains(.,'Leje e qarkullimit të mjetit')]")).Displayed, Is.True);

        Log("Assert emri i kerkuesit eshte readonly");
        Assert.That(driver.FindElement(By.XPath("//h5[normalize-space()='Kërkuesi']")).Displayed, Is.True);
        IWebElement kerkuesiInput = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//h5[normalize-space()='Kërkuesi']/following::input[1]")));
        Assert.That(kerkuesiInput.GetAttribute("value").Trim(), Is.EqualTo("Katerina Jançe"));
        Assert.That(kerkuesiInput.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(driver.FindElement(
            By.XPath("//small[contains(.,'Emër, Mbiemër, Nënshkrimi')]")).Displayed, Is.True);

        string documentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";

        Log("Ngarko Akti i blerjes");
        UploadDocument("aktiBlerjes-upload-14986", documentPath);

        Log("Ngarko Dokumenti i cregjistrimit");
        UploadDocument("dokCrregjistrimi-upload-14986", documentPath);

        Log("Assert checkbox i pranimit eshte i pazgjedhur");
        IWebElement acceptTerms = wait.Until(ExpectedConditions.ElementExists(By.Id("acceptTerms")));
        Assert.That(acceptTerms.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='acceptTerms']")).Text.Trim(),
            Does.Contain("Në mbështetje të Ligjit nr. 9887, datë 10.03.2003"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='acceptTerms']")).Text.Trim(),
            Does.Contain("Për mbrojtjen e të dhënave personale"));

        Log("Kliko Dergo pa pranuar kushtet");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(1000);
        Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase"))).Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DOKUMENTACIONI"));

        Log("Zgjidh pranimin e kushteve");
        SafeClick(By.Id("acceptTerms"));
        Assert.That(driver.FindElement(By.Id("acceptTerms")).Selected, Is.True);

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
        //Assert.That(referenceNumber.Text, Does.Contain("14986-"));
        //Assert.That(driver.Url, Does.Contain("/mesazh"));

        Log("TEST PASSED");
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

    private IWebElement FindFieldById(string id)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
    }

    private void AssertLabel(string forId, string expectedLabel)
    {

        IWebElement label = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector($"label[for='{forId}']")));
        Assert.That(label.Text.Trim(), Is.EqualTo(expectedLabel));
    }

    private void AssertReadonlyField(string id, string expectedValue)
    {

        IWebElement input = FindFieldById(id);
        Assert.That(input.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(input.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
    }

    private void AssertEditableField(string id, string expectedValue)
    {

        IWebElement input = FindFieldById(id);
        Assert.That(input.GetAttribute("disabled"), Is.Null);
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
    }

    private void AssertDisabledField(string id, string expectedValue)
    {

        IWebElement input = FindFieldById(id);
        Assert.That(input.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
    }

    private IWebElement FindPensionNumberInput(string checkboxId)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//input[@id='{checkboxId}']/preceding-sibling::input[@type='text']")));
    }

    private void AssertDeclarationRow(string checkboxId, string lawText)
    {

        IWebElement checkbox = wait.Until(ExpectedConditions.ElementExists(By.Id(checkboxId)));
        Assert.That(checkbox.GetAttribute("type"), Is.EqualTo("checkbox"));
        Assert.That(checkbox.Selected, Is.False);

        IWebElement law = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//input[@id='{checkboxId}']/preceding-sibling::span")));
        Assert.That(law.Text.Trim(), Is.EqualTo(lawText));

        IWebElement numberInput = FindPensionNumberInput(checkboxId);
        Assert.That(numberInput.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(numberInput.GetAttribute("placeholder"), Is.EqualTo("Numri i pensionit"));
        Assert.That(numberInput.GetAttribute("value"), Is.EqualTo(string.Empty));
    }

    private void ClickCheckbox(string checkboxId)
    {

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

    private void AssertDocumentUpload(string uploadId,
        string documentTitle,
        string fileTypes = ".pdf,.jpg,.jpeg,.png",
        string hint = "Formatet e lejuara: PDF, JPG, JPEG, PNG. Madhësia maksimale: 25MB.")
    {

        Assert.That(driver.FindElement(
            By.XPath($"//span[contains(normalize-space(),'{documentTitle}')]")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-14986"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("25"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("25"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(fileTypes));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të ngarkuar dokumentin"));

        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të ngarkuar dokumentin"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='hint']")).Text.Trim(),
            Is.EqualTo(hint));
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
}
