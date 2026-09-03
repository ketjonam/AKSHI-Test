using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.ISSH;

[Category("ISSH")]
[Category("2308")]
public class _2308_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "2308";
    protected override string? ServiceTitle => "AplikimPerPensionTeParakoheshemUshtarak";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    [Test]
    public void AplikimPerPensionTeParakoheshemUshtarak()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 5 hapa, i pari aktiv");
        AssertSteps(1);

        Log("Assert Title");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h5.ealb-header-text")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("TË DHËNA MBI GJENDJEN CIVILE"));

        Log("Assert butonat e navigimit");
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Is.EqualTo("Vazhdo"));

        Log("Assert Agjencia eshte disabled para zgjedhjes se drejtorise");
        IWebElement agjenciaSelect = FindSelectByLabel("Agjencia e Sigurimeve Shoqërore");
        Assert.That(agjenciaSelect.GetAttribute("disabled"), Is.Not.Null);

        Log("Assert Drejtoria ka opsionet e drejtorive");
        IWebElement drejtoriaSelect = FindSelectByLabel("Drejtuar");
        var drejtoria = new SelectElement(drejtoriaSelect);
        Assert.That(drejtoria.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(drejtoria.Options.Count, Is.EqualTo(15));
        Assert.That(drejtoria.Options[1].GetAttribute("value"), Is.EqualTo("01"));
        Assert.That(drejtoria.Options[1].Text.Trim(), Is.EqualTo("Drejtoria Berat"));
        Assert.That(drejtoria.Options[11].GetAttribute("value"), Is.EqualTo("11"));
        Assert.That(drejtoria.Options[11].Text.Trim(), Is.EqualTo("Drejtoria Tirane"));
        Assert.That(drejtoria.Options[13].Text.Trim(), Is.EqualTo("Dega Tropoje"));
        Assert.That(drejtoria.Options[14].Text.Trim(), Is.EqualTo("Dega Sarande"));

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error message per fushat e detyrueshme");
        IWebElement drejtuarError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//label[contains(.,'Drejtuar')]/following-sibling::div[contains(@class,'text-danger')]")));
        Assert.That(drejtuarError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));

        IWebElement agjenciaError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//label[contains(.,'Agjencia e Sigurimeve Shoqërore')]/following-sibling::div[contains(@class,'text-danger')]")));
        Assert.That(agjenciaError.Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));

        Log("Assert te dhenat e gjendjes civile te para-plotesuara");
        Assert.That(FindInputByLabel("NID").GetAttribute("value").Trim(), Is.EqualTo(Settings.Qytetar.Username));
        Assert.That(FindInputByLabel("Emri").GetAttribute("value").Trim(), Is.EqualTo("Ketjona"));
        Assert.That(FindInputByLabel("Atësia").GetAttribute("value").Trim(), Is.EqualTo("Mersin"));
        Assert.That(FindInputByLabel("Mbiemri").GetAttribute("value").Trim(), Is.EqualTo("Mema"));
        Assert.That(FindInputByLabel("Datëlindja").GetAttribute("value").Trim(), Is.EqualTo("28.07.1995"));

        Log("Assert te dhenat e vendlindjes");
        Assert.That(FindSectionInput("Vendlindja", "Bashki/Komuna").GetAttribute("value").Trim(), Is.EqualTo("KAVAJË"));
        Assert.That(FindSectionInput("Vendlindja", "Qarku").GetAttribute("value").Trim(), Is.EqualTo("TIRANË"));
        Assert.That(FindSectionInput("Vendlindja", "Qytet/Fshat").GetAttribute("value").Trim(), Is.EqualTo("Kavajë"));

        Log("Assert adresa e rruges eshte readonly");
        IWebElement rruga = FindSectionInput("Adresa", "Rruga");
        Assert.That(rruga.GetAttribute("value").Trim(),
            Is.EqualTo("THABIT REXHA 04040156; Nd. 6; H. 2; ; KAVAJË; KAVAJË; 2501; KAVAJË"));
        Assert.That(rruga.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(rruga.GetAttribute("disabled"), Is.Not.Null);

        Log("Zgjidh Drejtoria Tirane");
        drejtoriaSelect = FindSelectByLabel("Drejtuar");
        SelectDropdownByValue(drejtoriaSelect, "11");

        Log("Wait qe Agjencia te aktivizohet");
        wait.Until(d =>
        {
            try
            {
                var agency = d.FindElement(
                    By.XPath("//form//label[contains(.,'Agjencia e Sigurimeve Shoqërore')]/following-sibling::select"));
                return agency.GetAttribute("disabled") == null
                    && new SelectElement(agency).Options.Count > 1;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        IWebElement agjenciaEnabled = FindSelectByLabel("Agjencia e Sigurimeve Shoqërore");
        var agjenciaOptions = new SelectElement(agjenciaEnabled);
        Assert.That(agjenciaOptions.Options.Count, Is.GreaterThan(1));

        Log("Zgjidh Agjencia Kavaje");
        SelectDropdownByValue(agjenciaEnabled, "32");

        Log("Ploteso fushat e editueshme te gjendjes civile");
        FillInput(FindInputByLabel("Mbiemri i vajzërisë"), "Test");
        FillInput(FindInputByLabel("Nr. Tel"), "0676041404");

        Log("Ploteso Vendbanimi");
        FillInput(FindSectionInput("Vendbanimi", "Bashki/Komuna"), "Kavajë");
        FillInput(FindSectionInput("Vendbanimi", "Qarku"), "Tiranë");
        FillInput(FindSectionInput("Vendbanimi", "Qytet/Fshat"), "Kavajë");
        FillInput(FindSectionInput("Vendbanimi", "Nga viti"), "2015");
        FillInput(FindSectionInput("Vendbanimi", "deri në vitin"), "2024");

        Log("Ploteso Vendbanimi aktual");
        FillInput(FindSectionInput("Vendbanimi aktual", "Bashki/Komuna"), "Kavajë");
        FillInput(FindSectionInput("Vendbanimi aktual", "Qarku"), "Tiranë");
        FillInput(FindSectionInput("Vendbanimi aktual", "Qytet/Fshat"), "Kavajë");
        FillInput(FindSectionInput("Vendbanimi aktual", "Nga viti"), "2024");

        Log("Ploteso Adresa");
        FillInput(FindSectionInput("Adresa", "Lagjja"), "1");
        FillInput(FindSectionInput("Adresa", "Pallati Nr."), "6");
        FillInput(FindSectionInput("Adresa", "Ap Nr."), "2");
        FillInput(FindSectionInput("Adresa", "Shkalla Nr."), "2");

        Log("Kliko Vazhdo");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//h5[contains(@class,'ealb-section-title')]")));
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("KËRKESA"));

        Log("Assert 5 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert teksti i ligjit dhe ndihmesa");
        IWebElement ligjiText = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//p[contains(.,'ligjit nr. 10142')]")));
        Assert.That(ligjiText.Text.Trim(),
            Is.EqualTo("Për rrjedhojë, se kam plotësuar kushtet sipas ligjit nr. 10142 datë 15.05.2009"));

        IWebElement hintText = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//form//p/i")));
        Assert.That(hintText.Text.Trim(), Is.EqualTo("Zgjidh pikat ku mendohet se përfiton."));

        Log("Assert opsionet e arsyes se kerkeses");
        Assert.That(driver.FindElement(By.Id("reason-ageReason")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id("reason-healthReasons")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id("reason-familyReasons")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id("reason-yearsOnServiceReason")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id("reason-agePensionReason")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.CssSelector("label[for='reason-ageReason']")).Text.Trim(),
            Is.EqualTo("1. Moshën"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='reason-healthReasons']")).Text.Trim(),
            Is.EqualTo("2. Për shkaqe shëndetësore"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='reason-familyReasons']")).Text.Trim(),
            Is.EqualTo("3. Për shkaqe familjare"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='reason-yearsOnServiceReason']")).Text.Trim(),
            Is.EqualTo("4. Vitet në shërbim"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='reason-agePensionReason']")).Text.Trim(),
            Is.EqualTo("5. Jam në pension pleqërie"));

        Log("Assert opsionet e tipit te kerkeses");
        Assert.That(driver.FindElement(By.Id("requestType-serviceSeniorityType")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id("requestType-invaliditySupplementType")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id("requestType-workInvaliditySupplementType")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id("requestType-workFamilySupplementType")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id("requestType-familySupplementType")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id("requestType-oldageSupplementType")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.CssSelector("label[for='requestType-serviceSeniorityType']")).Text.Trim(),
            Is.EqualTo("1. Pension të parakohshëm për vjetërsi shërbimi"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='requestType-invaliditySupplementType']")).Text.Trim(),
            Is.EqualTo("2. Pension suplementar invaliditeti"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='requestType-workInvaliditySupplementType']")).Text.Trim(),
            Is.EqualTo("3. Pension suplementar invaliditeti për shkak detyre"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='requestType-workFamilySupplementType']")).Text.Trim(),
            Is.EqualTo("4. Pension suplementar familjar në detyrë dhe për shkak të saj"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='requestType-familySupplementType']")).Text.Trim(),
            Is.EqualTo("5. Pension suplementar familjar"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='requestType-oldageSupplementType']")).Text.Trim(),
            Is.EqualTo("6. Pension suplementar pleqërie"));

        Log("Assert opsionet e terheqjes");
        Assert.That(driver.FindElement(By.CssSelector("label[for='paymentMethod-withdrawFromPost']")).Text.Trim(),
            Is.EqualTo("Në postën shqiptare, filiali"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='paymentMethod-withdrawFromBank']")).Text.Trim(),
            Is.EqualTo("Në bankën"));

        Log("Assert dropdown-et e pageses jane disabled para zgjedhjes");
        IWebElement postSelect = FindPaymentSelect("paymentMethod-withdrawFromPost");
        IWebElement bankSelect = FindPaymentSelect("paymentMethod-withdrawFromBank");
        Assert.That(postSelect.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(bankSelect.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindLabeledInput("Dega").GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindLabeledInput("Adresa").GetAttribute("disabled"), Is.Not.Null);

        Log("Kliko Vazhdo pa zgjedhur opsionet e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error messages per tre grupet e radios");
        var step2Errors = wait.Until(d =>
        {
            var items = d.FindElements(By.CssSelector("form div.text-danger.mb-3"));
            return items.Count >= 3 ? items : null;
        });
        Assert.That(step2Errors.Count, Is.EqualTo(3));
        Assert.That(step2Errors[0].Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));
        Assert.That(step2Errors[1].Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));
        Assert.That(step2Errors[2].Text.Trim(), Is.EqualTo("Përzgjidhni një vlerë për të vazhduar"));

        Log("Zgjidh arsyen: Vitet ne sherbim");
        SelectRadioById("reason-yearsOnServiceReason");

        Log("Zgjidh tipin: Pension te parakoheshem per vjetersi sherbimi");
        SelectRadioById("requestType-serviceSeniorityType");

        Log("Zgjidh terheqjen ne Posten Shqiptare");
        SelectRadioById("paymentMethod-withdrawFromPost");

        postSelect = FindPaymentSelect("paymentMethod-withdrawFromPost");
        bankSelect = FindPaymentSelect("paymentMethod-withdrawFromBank");
        Assert.That(postSelect.GetAttribute("disabled"), Is.Null);
        Assert.That(bankSelect.GetAttribute("disabled"), Is.Not.Null);

        Log("Zgjidh filiali A/Kavaje (KJ)");
        SelectDropdownByValue(postSelect, "56");

        Log("Ndrysho terheqjen ne banke");
        SelectRadioById("paymentMethod-withdrawFromBank");

        postSelect = FindPaymentSelect("paymentMethod-withdrawFromPost");
        bankSelect = FindPaymentSelect("paymentMethod-withdrawFromBank");
        Assert.That(postSelect.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(bankSelect.GetAttribute("disabled"), Is.Null);
        Assert.That(FindLabeledInput("Dega").GetAttribute("disabled"), Is.Null);
        Assert.That(FindLabeledInput("Adresa").GetAttribute("disabled"), Is.Null);

        Log("Zgjidh Credins (KJ)");
        SelectDropdownByValue(bankSelect, "57");

        Log("Kliko Vazhdo pa plotesuar Dega dhe Adresa");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error per Dega dhe Adresa");
        var bankFieldErrors = wait.Until(d =>
        {
            var items = d.FindElements(By.XPath("//form//small[contains(.,'Plotësoni fushën për të vazhduar')]"));
            return items.Count >= 2 ? items : null;
        });
        Assert.That(bankFieldErrors.Count, Is.EqualTo(2));
        Assert.That(bankFieldErrors[0].Text.Trim(), Is.EqualTo("Plotësoni fushën për të vazhduar"));
        Assert.That(bankFieldErrors[1].Text.Trim(), Is.EqualTo("Plotësoni fushën për të vazhduar"));

        Log("Ploteso Dega dhe Adresa");
        FillInput(FindLabeledInput("Dega"), "Kavajë");
        FillInput(FindLabeledInput("Adresa"), "Rruga Thabit Rexha");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase")));
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("PERIUDHA TË TJERA PUNË APO SIGURIMI SIPAS LIGJIT NR. 7703 DATË 11.05.1993"));

        Log("Assert 5 hapa, tre te paret aktiv");
        AssertSteps(3);

        Log("Assert kolonat e tabeles");
        Assert.That(driver.FindElement(By.XPath("//table//th[normalize-space()='Nr.']")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//table//th[contains(.,'Detyra që kam punuar')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//table//th[contains(.,'Data e fillimit')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//table//th[contains(.,'Data e largimit')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//table//th[normalize-space()='Vërejtje']")).Displayed, Is.True);

        Log("Assert tabela eshte bosh");
        IWebElement emptyRow = wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector("table tbody tr td[colspan='7']")));
        Assert.That(emptyRow.Text.Trim(), Is.EqualTo(string.Empty));

        Log("Kliko Vazhdo pa shtuar periudhe");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert alert Gabim");
        IWebElement alertTitle = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//h2[normalize-space()='Gabim']")));
        Assert.That(alertTitle.Displayed, Is.True);

        IWebElement alertMsg = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//*[contains(text(),'Plotësoni saktë detajet e pozicioneve dhe periudhave')]")));
        Assert.That(alertMsg.Text.Trim(), Is.EqualTo("Plotësoni saktë detajet e pozicioneve dhe periudhave"));

        Log("Kliko OK ne alert");
        SafeClick(By.XPath("//button[normalize-space()='OK']"));
        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
            By.XPath("//h2[normalize-space()='Gabim']")));

        Log("Kliko + Shto");
        SafeClick(By.XPath("//button[contains(.,'+ Shto')]"));

        Log("Assert modal title");
        IWebElement modalTitle = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector(".custom-modal-title")));
        Assert.That(modalTitle.Text.Trim(), Is.EqualTo("Shto periudhë të re"));

        Log("Kliko Anullo");
        SafeClick(By.XPath("//div[contains(@class,'custom-modal-content')]//button[contains(.,'Anullo')]"));
        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
            By.CssSelector(".custom-modal-content")));

        Log("Kliko + Shto perseri");
        SafeClick(By.XPath("//button[contains(.,'+ Shto')]"));
        wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".custom-modal-title")));

        Log("Kliko Ruaj pa plotesuar fushat e detyrueshme");
        SafeClick(By.XPath("//div[contains(@class,'custom-modal-content')]//button[contains(.,'Ruaj')]"));

        Log("Assert fushat e detyrueshme jane invalid");
        Assert.That(FindModalInput("Kompania/Institucioni").GetAttribute("class"), Does.Contain("is-invalid"));
        Assert.That(FindModalInput("Pozicioni").GetAttribute("class"), Does.Contain("is-invalid"));
        Assert.That(FindModalInput("Data e fillimit").GetAttribute("class"), Does.Contain("is-invalid"));

        Log("Ploteso fushat e modalit");
        FillInput(FindModalInput("Kompania/Institucioni"), "Ministria e Mbrojtjes");
        FillInput(FindModalInput("Pozicioni"), "Ushtar");
        FillInput(FindModalInput("Data e fillimit"), "01.01.2015");
        FillInput(FindModalInput("Data e largimit"), "31.12.2020");
        FillInput(FindModalInput("Vërejtje"), "Test");

        Log("Kliko Ruaj");
        SafeClick(By.XPath("//div[contains(@class,'custom-modal-content')]//button[contains(.,'Ruaj')]"));
        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
            By.CssSelector(".custom-modal-content")));
        Thread.Sleep(1000);

        Log("Assert rreshti i periudhes ne tabele");
        IWebElement savedRow = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("table tbody tr")));
        var cells = savedRow.FindElements(By.TagName("td"));
        Assert.That(cells[0].Text.Trim(), Is.EqualTo("1"));
        Assert.That(cells[1].Text.Trim(), Is.EqualTo("Ministria e Mbrojtjes"));
        Assert.That(cells[2].Text.Trim(), Is.EqualTo("Ushtar"));
        Assert.That(cells[3].Text.Trim(), Is.EqualTo("01.01.2015"));
        Assert.That(cells[4].Text.Trim(), Is.EqualTo("31.12.2020"));
        Assert.That(cells[5].Text.Trim(), Is.EqualTo("Test"));

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 4 Title");
        IWebElement Step4Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h4.text-uppercase")));
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("DEKLAROJ SE"));

        Log("Assert 5 hapa, kater te paret aktiv");
        AssertSteps(4);

        Log("Assert opsionet e statusit te punes");
        Assert.That(driver.FindElement(By.Id("terminatedEmployment")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id("continueEmployment")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id("amReceivingPension")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.Id("notReceivingPension")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.CssSelector("label[for='terminatedEmployment']")).Text.Trim(),
            Is.EqualTo("Kam ndërprerë marrëdhëniet e punës me subjektin"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='continueEmployment']")).Text.Trim(),
            Is.EqualTo("Vazhdoj marrëdhëniet e punës në subjektin"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='amReceivingPension']")).Text.Trim(),
            Is.EqualTo("Marr pension (lloji i pensionit)"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='notReceivingPension']")).Text.Trim(),
            Is.EqualTo("Nuk marr asnjë lloj pensioni nga sigurimet shoqërore"));

        Log("Assert seksioni i njoftimit");
        IWebElement notifyTitle = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//h6[contains(.,'Do të njoftoj zyrën më të afërt lokale të Sigurimeve Shoqërore')]")));
        Assert.That(notifyTitle.Displayed, Is.True);

        Log("Zgjidh Kam nderprere marredheniet e punes");
        SelectRadioById("terminatedEmployment");
        FillInput(FindRadioSiblingInput("terminatedEmployment", 1), "Ministria e Mbrojtjes");
        FillInput(FindRadioSiblingInput("terminatedEmployment", 2), "31.12.2020");

        Log("Zgjidh Nuk marr asnje lloj pensioni");
        SelectRadioById("notReceivingPension");
        FillInput(FindRadioSiblingInput("notReceivingPension", 1), "Ketjona Mema");

        Log("Ploteso te dhenat e perfaqesuesit");
        FillInput(FindInputAfterSpan("përfaqësues"), "Test Test");
        FillInput(FindInputAfterSpan("relacioni familjar"), "Bashkëshort");
        FillInput(FindInputAfterSpan("numër pasaporte"), "A12345678");

        Log("Kliko Vazhdo Step 4");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 5 Title");
        IWebElement Step5Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//h4[contains(.,'Deklarimet') or contains(.,'DEKLARIMET')]")));
        Assert.That(Step5Title.Text.Trim().ToUpperInvariant(),
            Is.EqualTo("DEKLARIMET E SIPËRSHËNUARA I VËRTETOJ EDHE ME KËTO DOKUMENTE"));

        Log("Assert kohezgjatja");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 5 hapa, te gjithe aktiv");
        AssertSteps(5);

        Log("Assert butoni Dergo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.Text.Trim(), Is.EqualTo("Dërgo"));

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(By.XPath("//td[contains(.,'I. Dokumente që dorëzohen nga vetë personi')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//td[contains(.,'II. Dokumente që dërgohen nga ministritë e linjës')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//td[contains(.,'III. Dokumente që përgatiten nga strukturat e sigurimeve shoqërore')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(By.XPath("//td[contains(.,'IV. Të tjera')]")).Displayed, Is.True);

        Log("Assert nuk ka dokumente shtese");
        IWebElement extraDocsEmpty = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//td[contains(.,'Nuk ka dokumente shtesë të shtuar.')]")));
        Assert.That(extraDocsEmpty.Displayed, Is.True);

        Log("Assert 18 checkbox-e");
        var documentCheckboxes = driver.FindElements(By.CssSelector("table tbody input[type='checkbox']"));
        Assert.That(documentCheckboxes.Count, Is.EqualTo(18));

        Log("Assert fushat e regjistrit jane readonly dhe bosh");
        IWebElement registrationNumber = wait.Until(ExpectedConditions.ElementExists(By.Id("registrationNumber")));
        IWebElement registrationDate = wait.Until(ExpectedConditions.ElementExists(By.Id("registrationDate")));
        Assert.That(registrationNumber.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(registrationDate.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(registrationNumber.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(registrationDate.GetAttribute("value"), Is.EqualTo(string.Empty));

        Log("Assert shenimi");
        IWebElement note = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//small[contains(.,'Zgjidh kutinë në krah të dokumentit që dorëzon')]")));
        Assert.That(note.Displayed, Is.True);

        Log("Zgjidh dokumente");
        SafeClick(By.XPath("//td[contains(.,'Fotokopje të letërnjoftimit')]/following-sibling::td//input[@type='checkbox']"));
        SafeClick(By.XPath("//td[contains(.,'Deklaratë individuale për gjendjen e punësimit')]/following-sibling::td//input[@type='checkbox']"));
        SafeClick(By.XPath("//td[contains(.,'Vërtetim i pagës referuese')]/following-sibling::td//input[@type='checkbox']"));
        SafeClick(By.XPath("//td[contains(.,'Vërtetim nëse personi trajtohet me përfitim')]/following-sibling::td//input[@type='checkbox']"));

        Assert.That(FindDocumentCheckbox("Fotokopje të letërnjoftimit").Selected, Is.True);
        Assert.That(FindDocumentCheckbox("Deklaratë individuale për gjendjen e punësimit").Selected, Is.True);
        Assert.That(FindDocumentCheckbox("Vërtetim i pagës referuese").Selected, Is.True);
        Assert.That(FindDocumentCheckbox("Vërtetim nëse personi trajtohet me përfitim").Selected, Is.True);

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
        //Assert.That(referenceNumber.Text, Does.Contain("2308-"));
        //Assert.That(driver.Url, Does.Contain("/mesazh"));

        Log("TEST PASSED");
    }

    [Test]
    public void AplikimPerPensionTeParakoheshemUshtarak_GabimMarrjaTeDhenaveGjendjaCivile()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert alert Gabim per marrjen e te dhenave nga gjendja civile");
        IWebElement alertTitle = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h2.alert-modal-title")));
        Assert.That(alertTitle.Text.Trim(), Is.EqualTo("Gabim"));

        IWebElement alertMsg = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector(".alert-modal-description")));
        Assert.That(alertMsg.Text.Trim(),
            Is.EqualTo("Ndodhi një gabim. Ju lutem provoni përsëri më vonë."));

        Log("Kliko OK ne alert");
        SafeClick(By.CssSelector("button.alert-modal-button--primary"));
        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
            By.CssSelector(".alert-modal-overlay")));

        Log("Assert titulli i hapit 1");
        IWebElement Step1Title = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("h5.ealb-header-text")));
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(), Is.EqualTo("TË DHËNA MBI GJENDJEN CIVILE"));

        Log("Assert fushat e gjendjes civile jane bosh");
        Assert.That(FindInputByLabel("NID").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindInputByLabel("Emri").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindInputByLabel("Atësia").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindInputByLabel("Mbiemri").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindInputByLabel("Datëlindja").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindSectionInput("Vendlindja", "Bashki/Komuna").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindSectionInput("Adresa", "Rruga").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

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
        Assert.That(serviceName.Text.Trim(),
            Is.EqualTo("Aplikim për pension të parakohshëm ushtarak sipas Ligjit Nr. 10142"),
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

    private void AssertSteps(int activeCount)
    {
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(5));
        for (int i = 0; i < steps.Count; i++)
        {
            if (i < activeCount)
                Assert.That(steps[i].GetAttribute("class"), Does.Contain("active"));
            else
                Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }
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

    private IWebElement FindSelectByLabel(string labelPart)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//form//label[contains(.,'{labelPart}')]/following-sibling::select")));
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

    private IWebElement FindPaymentSelect(string radioId)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//input[@id='{radioId}']/ancestor::div[contains(@class,'row')][1]//select")));
    }

    private IWebElement FindLabeledInput(string labelText)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//form//label[normalize-space()='{labelText}']/following-sibling::input")));
    }

    private IWebElement FindModalInput(string labelPart)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//div[contains(@class,'custom-modal-content')]//label[contains(.,'{labelPart}')]/following::input[not(@type='hidden')][1]")));
    }

    private IWebElement FindRadioSiblingInput(string radioId, int index)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//input[@id='{radioId}']/following-sibling::input[{index}]")));
    }

    private IWebElement FindInputAfterSpan(string spanText)
    {

        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//span[contains(.,'{spanText}')]/following-sibling::div[1]//input")));
    }

    private IWebElement FindDocumentCheckbox(string descriptionPart)
    {

        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//td[contains(.,'{descriptionPart}')]/following-sibling::td//input[@type='checkbox']")));
    }
}