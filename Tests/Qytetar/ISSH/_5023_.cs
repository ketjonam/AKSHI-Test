using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("5023")]
public class _5023_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "5023";
    protected override string? ServiceTitle => "AplikimPensioniFunksioneKushtetuese";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;

    [Test]
    public void AplikimPensioniFunksioneKushtetuese()
    {




        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.px-4.text-uppercase")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("TË DHËNAT MBI GJENDJEN CIVILE"));

        Log("Assert pershkrimi i kerkeses");
        IWebElement description = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//h4[contains(@class,'text-uppercase')]/following-sibling::div[contains(@class,'pb-4')]")));
        Assert.That(description.Text.Trim(),
            Is.EqualTo("Plotësohet nga përfituesi: KËRKESË PËR PËRFITIM SHTETËROR SUPLEMENTAR SIPAS LIGJIT NR. 8097 DATË 21.03.1996, I NDRYSHUAR"));

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

        Log("Assert label-at e gjendjes civile");
        AssertLabel("regionalOffice", "Drejtuar Drejtorisë Rajonale të Sigurimeve Shoqërore:*");
        AssertLabel("agency", "Agjencia e Sigurimeve Shoqërore:*");
        AssertLabel("name", "Emri:");
        AssertLabel("fathersName", "Atësia:");
        AssertLabel("surname", "Mbiemri:");
        AssertLabel("birthDate", "Datëlindja:");
        AssertLabel("street", "Rruga:");
        AssertLabel("birthPlace", "Vendlindja B/K:");
        AssertLabel("residence", "Vendbanimi B/K:");
        AssertLabel("idNumber", "Nr. letërnjoftimit:");
        AssertLabel("district", "Rrethi:");
        AssertLabel("phone", "Nr Telef.:");
        AssertLabel("buildingNo", "Pallati:");
        AssertLabel("apartmentNo", "Apart.Nr:");
        AssertLabel("qark", "Qarku:");
        AssertLabel("emriNjqv", "Q/F:");
        AssertLabel("address", "Adresa/Lagje:");

        Log("Assert te dhenat e gjendjes civile te para-plotesuara");
        AssertReadonlyField("name", "Ketjona");
        AssertReadonlyField("fathersName", "Mersin");
        AssertReadonlyField("surname", "Mema");
        AssertReadonlyField("birthDate", "28.07.1995");
        AssertReadonlyField("street",
            "THABIT REXHA 04040156; Nd. 6; H. 2; ; KAVAJË; KAVAJË; 2501; KAVAJË");
        AssertReadonlyField("birthPlace", "Kavajë");
        AssertReadonlyField("residence", "KAVAJË");
        AssertReadonlyField("idNumber", Settings.Qytetar.Username);
        AssertReadonlyField("district", "KAVAJË");
        AssertReadonlyField("phone", string.Empty);
        AssertReadonlyField("buildingNo", string.Empty);
        AssertReadonlyField("apartmentNo", string.Empty);
        AssertReadonlyField("qark", "TIRANË");
        AssertReadonlyField("emriNjqv", "Shkolla \"3 Deshmoret\"");
        AssertReadonlyField("address",
            "THABIT REXHA 04040156; Nd. 6; H. 2; ; KAVAJË; KAVAJË; 2501; KAVAJË");

        Log("Assert Drejtoria Rajonale ka opsionet e drejtorive");
        IWebElement regionalOfficeSelect = wait.Until(ExpectedConditions.ElementExists(
            By.Id("regionalOffice")));
        var regionalOffice = new SelectElement(regionalOfficeSelect);
        Assert.That(regionalOffice.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(regionalOffice.Options.Count, Is.EqualTo(15));
        Assert.That(regionalOffice.Options[1].GetAttribute("value"), Is.EqualTo("01"));
        Assert.That(regionalOffice.Options[1].Text.Trim(), Is.EqualTo("Drejtoria Berat"));
        Assert.That(regionalOffice.Options[11].GetAttribute("value"), Is.EqualTo("11"));
        Assert.That(regionalOffice.Options[11].Text.Trim(), Is.EqualTo("Drejtoria Tirane"));
        Assert.That(regionalOffice.Options[13].Text.Trim(), Is.EqualTo("Dega Tropoje"));
        Assert.That(regionalOffice.Options[14].Text.Trim(), Is.EqualTo("Dega Sarande"));

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
        IWebElement regionalOfficeError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//select[@id='regionalOffice']/following-sibling::div[contains(@class,'invalid-feedback')]")));
        Assert.That(regionalOfficeError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));
        Assert.That(driver.FindElement(By.Id("regionalOffice")).GetAttribute("class"),
            Does.Contain("is-invalid"));

        Log("Assert error message per agjencine");
        IWebElement agencyError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//select[@id='agency']/following-sibling::div[contains(@class,'invalid-feedback')]")));
        Assert.That(agencyError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));
        Assert.That(driver.FindElement(By.Id("agency")).GetAttribute("class"),
            Does.Contain("is-invalid"));

        Log("Zgjidh Drejtoria Tirane");
        SelectDropdownByValue(wait.Until(ExpectedConditions.ElementExists(By.Id("regionalOffice"))), "11");

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
        IWebElement Step2Title = WaitForHeaderTitle("KËRKESË");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("KËRKESË"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 5 hapa, dy te pare aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(5));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Not.Contain("active"));
        Assert.That(steps[3].GetAttribute("class"), Does.Not.Contain("active"));
        Assert.That(steps[4].GetAttribute("class"), Does.Not.Contain("active"));

        Log("Assert teksti i ligjit");
        IWebElement ligjiText = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//p[contains(.,'ligjit 8097')]")));
        Assert.That(ligjiText.Text.Trim(),
            Is.EqualTo("Për rrjedhojë, se kam plotësuar kushtet sipas ligjit 8097 datë 21.03.1996, i ndryshuar"));

        Log("Assert titulli Kerkoj");
        IWebElement kerkojTitle = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h5.ealb-kerkoj-text")));
        Assert.That(kerkojTitle.Text.Trim().ToUpperInvariant(), Is.EqualTo("KËRKOJ"));

        Log("Assert opsionet e tipit te pensionit");
        Assert.That(driver.FindElement(By.CssSelector("label[for='transitionalPayment']")).Text.Trim(),
            Is.EqualTo("1. Pagesë kalimtare"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='supplementaryStatePension']")).Text.Trim(),
            Is.EqualTo("2. Pension shtetëror suplementar"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='supplementaryStateFamilyPension']")).Text.Trim(),
            Is.EqualTo("3. Pension shtetëror suplementar familjar"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='supplementaryStateDisabilityPension']")).Text.Trim(),
            Is.EqualTo("4. Pension shtetëror suplementar invaliditeti"));

        Assert.That(driver.FindElement(By.Id("transitionalPayment")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("supplementaryStatePension")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("supplementaryStateFamilyPension")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("supplementaryStateDisabilityPension")).Selected, Is.False);

        Log("Assert titulli i terheqjes se kistit");
        IWebElement withdrawalTitle = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//h5[contains(.,'Tërheqjen e këstit mujor')]")));
        Assert.That(withdrawalTitle.Text.Trim(),
            Is.EqualTo("Tërheqjen e këstit mujor të përfitimit suplementar, e kërkoj"));

        Log("Assert fushat e postes dhe bankes jane bosh");
        IWebElement postBranch = FindInlineInputAfterSpan("Në Postën Shqiptare, filiali");
        IWebElement postOffice = FindInlineInputAfterSpan("zyra");
        IWebElement bankName = FindInlineInputAfterSpan("Në Bankën");
        IWebElement bankBranch = FindInlineInputAfterSpan("Dega");
        IWebElement bankAddress = FindInlineInputAfterSpan("adresa");

        Assert.That(postBranch.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(postOffice.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(bankName.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(bankBranch.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(bankAddress.GetAttribute("value"), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 2");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Kliko Vazhdo pa zgjedhur llojin e pensionit");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error message per llojin e pensionit");
        IWebElement pensionTypeError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//*[contains(@class,'invalid-feedback') or contains(@class,'text-danger')][contains(.,'Përzgjidhni')]")));
        Assert.That(pensionTypeError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));

        Log("Zgjidh Pension shteteror suplementar");
        SelectRadioById("supplementaryStatePension");
        Assert.That(driver.FindElement(By.Id("supplementaryStatePension")).Selected, Is.True);
        Assert.That(driver.FindElement(By.Id("transitionalPayment")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("supplementaryStateFamilyPension")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("supplementaryStateDisabilityPension")).Selected, Is.False);

        Log("Ploteso terheqjen ne Posten Shqiptare");
        FillInput(FindInlineInputAfterSpan("Në Postën Shqiptare, filiali"), "Kavajë");
        FillInput(FindInlineInputAfterSpan("zyra"), "1");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForHeaderTitle("DEKLARATAT");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("DEKLARATAT"));

        Log("Assert kohëzgjatja Step 3");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 5 hapa, tre te pare aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(5));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[3].GetAttribute("class"), Does.Not.Contain("active"));
        Assert.That(steps[4].GetAttribute("class"), Does.Not.Contain("active"));

        Log("Assert titulli Deklaroj se");
        IWebElement deklarojTitle = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//h5[normalize-space()='Deklaroj se']")));
        Assert.That(deklarojTitle.Text.Trim(), Is.EqualTo("Deklaroj se"));

        Log("Assert opsionet e deklaratave");
        Assert.That(FindCheckboxLabel("terminatedEmployment").Text.Trim(),
            Is.EqualTo("Kam ndërprerë marrëdhëniet e punës me subjektin"));
        Assert.That(FindCheckboxLabel("continuedEmployment").Text.Trim(),
            Is.EqualTo("Vazhdoj marrëdhëniet e punës në subjektin"));
        Assert.That(FindCheckboxLabel("receivingPension").Text.Trim(),
            Is.EqualTo("Marr pension (lloji i pensionit)"));
        Assert.That(FindCheckboxLabel("notReceivingPension").Text.Trim(),
            Is.EqualTo("Nuk marr asnjë lloj pensioni nga Sigurimet Shoqërore"));

        Assert.That(driver.FindElement(By.Id("terminatedEmployment")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("continuedEmployment")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("receivingPension")).Selected, Is.False);
        Assert.That(driver.FindElement(By.Id("notReceivingPension")).Selected, Is.False);

        Log("Assert fushat e deklaratave jane bosh");
        Assert.That(FindRowTextInput("terminatedEmployment", 1).GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindRowTextInput("continuedEmployment", 1).GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindRowTextInput("continuedEmployment", 2).GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindRowTextInput("receivingPension", 1).GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindRowTextInput("receivingPension", 2).GetAttribute("value"), Is.EqualTo(string.Empty));

        Log("Assert emri i perfaqesuesit");
        AssertLabel("representativeName", "(emër, mbiemër,firma)");
        IWebElement representativeName = FindFieldById("representativeName");
        Assert.That(representativeName.GetAttribute("value"), Is.EqualTo(string.Empty));

        Log("Assert teksti i njoftimit");
        IWebElement notifyText = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//p[contains(.,'Do të njoftoj për çdo ndryshim të gjendjes sociale')]")));
        Assert.That(notifyText.Text.Trim(), Does.Contain(
            "Do të njoftoj për çdo ndryshim të gjendjes sociale e cila sjell shtesë ose pakësim të të ardhurave mujore"));

        Log("Assert fushat e perfaqesuesit jane bosh");
        Assert.That(FindInlineInputAfterSpanContains("përfaqësues").GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindInlineInputAfterSpanContains("relacioni familjar").GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindInlineInputAfterSpanContains("nr. pasaporte").GetAttribute("value"),
            Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 3");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Zgjidh Kam nderprere marredheniet e punes");
        ClickMuiCheckbox("terminatedEmployment");
        Assert.That(driver.FindElement(By.Id("terminatedEmployment")).Selected, Is.True);
        FillInput(FindRowTextInput("terminatedEmployment", 1), "Ministria e Mbrojtjes");
        SetDateValue(FindDateInputAfterSpan("në datën"), "2020-12-31", "31.12.2020");

        Log("Zgjidh Nuk marr asnje lloj pensioni");
        ClickMuiCheckbox("notReceivingPension");
        Assert.That(driver.FindElement(By.Id("notReceivingPension")).Selected, Is.True);

        Log("Ploteso emrin e perfaqesuesit");
        FillInput(FindFieldById("representativeName"), "Ketjona Mema");

        Log("Ploteso te dhenat e perfaqesuesit");
        FillInput(FindInlineInputAfterSpanContains("përfaqësues"), "Test Test");
        FillInput(FindInlineInputAfterSpanContains("relacioni familjar"), "Bashkëshort");
        FillInput(FindInlineInputAfterSpanContains("nr. pasaporte"), "A12345678");

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 4 Title");
        IWebElement Step4Title = WaitForUppercaseTitle(
            "DEKLARIMET E SIPËRSHËNUARA I VËRTETOJ EDHE ME KËTO DOKUMENTA");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DEKLARIMET E SIPËRSHËNUARA I VËRTETOJ EDHE ME KËTO DOKUMENTA"));

        Log("Assert kohëzgjatja Step 4");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 5 hapa, kater te pare aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(5));
        Assert.That(steps[0].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[1].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[2].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[3].GetAttribute("class"), Does.Contain("active"));
        Assert.That(steps[4].GetAttribute("class"), Does.Not.Contain("active"));

        Log("Assert header-at e tabeles");
        Assert.That(driver.FindElement(By.XPath("//th[normalize-space()='Nr.']")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//th[contains(.,'Përshkrimi i dokumenteve që dorëzohen')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//th[contains(.,'Shenja')]")).Displayed, Is.True);

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//td[contains(.,'DOKUMENTA QË NGARKOHEN NGA APLIKANTI')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//td[contains(.,'DOKUMENTA QË NGARKOHEN NGA NËPUNËSI I ADMINISTRATËS PUBLIKE')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//td[contains(.,'DOKUMENTA QË PËRGATITEN NGA STRUKTURAT E SIGURIMEVE SHOQËRORE')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//td[normalize-space()='TË TJERA']")).Displayed, Is.True);

        Log("Assert dokumentet e aplikantit");
        Assert.That(driver.FindElement(
            By.XPath("//td[contains(.,'FOTOKOPJE TË LETËRNJOFTIMIT ELEKTRONIK')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//td[contains(.,'CERTIFIKATË FAMILJARE')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//td[contains(.,'DEKLARATË INDIVIDUALE PËR GJENDJEN E PUNËSIMIT')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//td[contains(.,'CERTIFIKATË VDEKJE')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//td[contains(.,'VËRTETIM SHKOLLE')]")).Displayed, Is.True);

        Log("Assert dokumentet e administrates publike");
        Assert.That(driver.FindElement(
            By.XPath("//td[contains(.,'VËRTETIM I PAGËS REFERUESE')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//td[contains(.,'VËRTETIM PËR PERIUDHËN E SHËRBIMIT NË FUNKSION')]")).Displayed, Is.True);

        Log("Assert dokumentet e sigurimeve shoqerore");
        Assert.That(driver.FindElement(
            By.XPath("//td[contains(.,'VËRTETIM NËSE PERSONI TRAJTOHET ME PËRFITIM NGA LIGJI NR.7703')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//td[contains(.,'VËRTETIM PËR DERDHJE TË KONTRIBUTIT SUPLEMENTAR')]")).Displayed, Is.True);

        Log("Assert 8 checkbox-e");
        var documentCheckboxes = driver.FindElements(By.CssSelector("table tbody input[type='checkbox']"));
        Assert.That(documentCheckboxes.Count, Is.EqualTo(8));
        foreach (var checkbox in documentCheckboxes)
            Assert.That(checkbox.Selected, Is.False);

        Log("Assert fushat e te tjerave jane bosh");
        var extraDocInputs = driver.FindElements(By.CssSelector("table tbody input.MuiInputBase-input"));
        Assert.That(extraDocInputs.Count, Is.EqualTo(5));
        foreach (var extraInput in extraDocInputs)
            Assert.That(extraInput.GetAttribute("value"), Is.EqualTo(string.Empty));

        Log("Assert shenimi");
        IWebElement note = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//p[contains(.,'Selektoni shenjën')]")));
        Assert.That(note.Text.Trim(),
            Is.EqualTo("Shënim: Selektoni shenjën ✓ për dokumentat që dorëzohen."));

        Log("Assert butonat e navigimit Step 4");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Zgjidh dokumente");
        ClickDocumentCheckbox("FOTOKOPJE TË LETËRNJOFTIMIT");
        ClickDocumentCheckbox("DEKLARATË INDIVIDUALE PËR GJENDJEN E PUNËSIMIT");
        ClickDocumentCheckbox("VËRTETIM I PAGËS REFERUESE");
        ClickDocumentCheckbox("VËRTETIM NËSE PERSONI TRAJTOHET ME PËRFITIM");

        Assert.That(FindDocumentCheckbox("FOTOKOPJE TË LETËRNJOFTIMIT").Selected, Is.True);
        Assert.That(FindDocumentCheckbox("DEKLARATË INDIVIDUALE PËR GJENDJEN E PUNËSIMIT").Selected, Is.True);
        Assert.That(FindDocumentCheckbox("VËRTETIM I PAGËS REFERUESE").Selected, Is.True);
        Assert.That(FindDocumentCheckbox("VËRTETIM NËSE PERSONI TRAJTOHET ME PËRFITIM").Selected, Is.True);

        Log("Kliko Vazhdo Step 4");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 5 Title");
        IWebElement Step5Title = WaitForUppercaseTitle("NËNSHKRIMET");
        Assert.That(Step5Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("NËNSHKRIMET"));

        Log("Assert kohëzgjatja Step 5");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 5 hapa, te gjithe aktiv");
        steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(5));
        foreach (var step in steps)
            Assert.That(step.GetAttribute("class"), Does.Contain("active"));

        Log("Assert label-at e nenshkrimeve");
        AssertLabel("registrationNumber", "Nr. Regjistrit");
        AssertLabel("registrationDate", "Data e regjistrimit");
        AssertLabel("applicant", "KËRKUESI");
        AssertLabel("inspector", "INSPEKTORI I SIGURIMEVE SHOQËRORE");

        Log("Assert Nr. Regjistrit eshte readonly, disabled dhe bosh");
        AssertReadonlyField("registrationNumber", string.Empty);

        Log("Assert Data e regjistrimit eshte e dites se sotme");
        IWebElement registrationDateInput = wait.Until(ExpectedConditions.ElementExists(
            By.XPath("//label[@for='registrationDate']/following::input[@placeholder='dd.mm.yyyy' or contains(@class,'flatpickr-input')][not(@type='hidden')][1]")));
        Assert.That(registrationDateInput.GetAttribute("value").Trim(),
            Is.EqualTo(DateTime.Now.ToString("dd.MM.yyyy")));

        Log("Assert tekstet ndihmese");
        IWebElement filledByIssh = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//p[contains(.,'Plotesohet nga sigurimet shoqërore')]")));
        Assert.That(filledByIssh.Text.Trim(),
            Is.EqualTo("Plotesohet nga sigurimet shoqërore pas marrjes në dorëzim."));

        IWebElement deadlineText = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//p[contains(.,'afati i lidhjes së pensionit')]")));
        Assert.That(deadlineText.Text.Trim(),
            Is.EqualTo("Nga kjo datë afati i lidhjes së pensionit është deri në ditën e fundit të muajit të ardhshëm."));

        Log("Assert kerkuesi dhe inspektori");
        AssertReadonlyField("applicant", "Ketjona Mersin Mema");
        AssertReadonlyField("inspector", string.Empty);

        var nameHints = driver.FindElements(By.XPath("//form//small[normalize-space()='EMËR ATËSI MBIEMËR']"));
        Assert.That(nameHints.Count, Is.EqualTo(2));

        Log("Assert butonat e navigimit Step 5");
        backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
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
        //Assert.That(referenceNumber.Text, Does.Contain("5023-"));
        //Assert.That(driver.Url, Does.Contain("/mesazh"));

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
        Assert.That(input.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(input.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
    }

    private void AssertLabel(string forId, string expectedLabel)
    {

        IWebElement label = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector($"label[for='{forId}']")));
        Assert.That(label.Text.Trim(), Is.EqualTo(expectedLabel));
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

    private IWebElement FindInlineInputAfterSpan(string spanText)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//form//span[normalize-space()='{spanText}']/following-sibling::input[1]")));
    }

    private IWebElement FindInlineInputAfterSpanContains(string spanPart)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//form//span[contains(.,'{spanPart}')]/following-sibling::input[1]")));
    }

    private IWebElement FindRowTextInput(string checkboxId, int index)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"(//input[@id='{checkboxId}']/ancestor::div[contains(@class,'flex-wrap')][1]//input[@type='text'])[{index}]")));
    }

    private IWebElement FindDateInputAfterSpan(string spanText)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//form//span[normalize-space()='{spanText}']/following-sibling::div[1]//input[@placeholder='dd.mm.yyyy' or @type='date'][not(@type='hidden')]")));
    }

    private IWebElement FindCheckboxLabel(string checkboxId)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//input[@id='{checkboxId}']/ancestor::span[contains(@class,'MuiCheckbox-root')]/following-sibling::span")));
    }

    private void ClickMuiCheckbox(string checkboxId)
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

    private IWebElement WaitForHeaderTitle(string expectedUpper)
    {

        return wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4.ealb-header-text"));
            if (titles.Count == 0)
                return null;
            return titles[0].Text.Trim().ToUpperInvariant() == expectedUpper
                ? titles[0]
                : null;
        });
    }

    private IWebElement WaitForUppercaseTitle(string expectedUpper)
    {

        return wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4.text-uppercase"));
            if (titles.Count == 0)
                return null;
            return titles[0].Text.Trim().ToUpperInvariant() == expectedUpper
                ? titles[0]
                : null;
        });
    }

    private IWebElement FindDocumentCheckbox(string descriptionPart)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//td[contains(.,'{descriptionPart}')]/following-sibling::td//input[@type='checkbox']")));
    }

    private void ClickDocumentCheckbox(string descriptionPart)
    {

        SafeClick(By.XPath(
            $"//td[contains(.,'{descriptionPart}')]/following-sibling::td//span[contains(@class,'MuiCheckbox-root')]"));
        Thread.Sleep(300);
    }
}