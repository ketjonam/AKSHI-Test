using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Biznes.AMS;

[Category("AMS")]
[Category("15166")]
public class _15166_ : BiznesTestBase
{
    protected override string ServiceCode => "15166";
    protected override string? ServiceTitle => "FondiPyjor";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Aplikim për ndryshimin e sipërfaqeve dhe pakësimin në volum nga fondi pyjor kombëtar apo zgjerimin e tyre";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Signed_TEST_signed.pdf";
    private const int TotalSteps = 3;

    private static readonly (string Id, string Label)[] PurposeCheckboxes =
    {
        ("ruralExpansion",
            "Kthimi në truall, për zgjerimin e vijës kufizuese të ndërtimit dhe të shtrirjes territoriale të periferisë së qytetit apo të qendrave të banuara në zonat rurale"),
        ("tourismStructures",
            "Ndërtimin e strukturave akomoduese në funksion të sipërmarrjes turistike dhe të veprimtarisë së agroturizmit"),
        ("transportInfrastructure", "Ndërtimin e rrugëve automobilistike apo hekurudhore"),
        ("oilGasWells", "Shpim dhe shfrytëzim të puseve të naftës e të gazit"),
        ("miningGeological", "Veprimtari minerare e gjeologjike"),
        ("telecomAirports", "Aeroporte dhe struktura të telekomunikacionit"),
        ("militaryStructures", "Qëllime të strukturave ushtarake"),
        ("industrialCenters", "Qendra industriale dhe aktivitete me natyrë industriale dhe/ose energjitike"),
        ("otherPublicPurposes", "Për qëllime të tjera publike, për rastet e përcaktuara në nenin 25 të ligjit nr.57/2020"),
    };

    [Test]
    public void FondiPyjor()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert Title Step 1");
        IWebElement Step1Title = WaitForStepTitle("INFORMACION MBI SUBJEKTIN");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("INFORMACION MBI SUBJEKTIN"));

        Log("Assert kohëzgjatja");
        AssertDuration("3 minuta kohëzgjatje");

        Log("Assert 3 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert te dhenat e subjektit");
        AssertReadonlyByLabel("NIPT", "M53330201S");
        Assert.That(FindLabel("NIPT").Text, Does.Contain("NIPT"));

        AssertReadonlyByLabel("Emri i subjektit aplikues", "Migen Dërstila");
        Assert.That(FindLabel("Emri i subjektit aplikues").Text, Does.Contain("Emri i subjektit aplikues"));

        AssertReadonlyByLabel("Nid i Administratorit", "J70903019W");
        Assert.That(FindLabel("Nid i Administratorit").Text, Does.Contain("*"));

        AssertReadonlyByLabel("Emër Mbiemër i përfaqësuesit të subjektit", "Migen Dërstila");
        Assert.That(FindLabel("Emër Mbiemër i përfaqësuesit të subjektit").Text,
            Does.Contain("Emër Mbiemër i përfaqësuesit të subjektit"));

        IWebElement tel = FindInputByLabel("Nr. Cel");
        Assert.That(tel.GetAttribute("type"), Is.EqualTo("tel"));
        Assert.That(InputValue(tel), Is.EqualTo("+355684053531"));
        Assert.That(tel.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(FindLabel("Nr. Cel").Text, Does.Contain("*"));

        IWebElement email = FindInputByLabel("Email");
        Assert.That(email.GetAttribute("type"), Is.EqualTo("email"));
        Assert.That(InputValue(email), Is.EqualTo("migen.derstila@kreatx.com"));
        Assert.That(email.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(FindLabel("Email").Text, Does.Contain("*"));

        AssertReadonlyByLabel(
            "Adresa e subjektit",
            "Derstile; ; ; ; Gjinar; ; 0000; Elbasan  , , Elbasan");
        Assert.That(FindLabel("Adresa e subjektit").Text, Does.Contain("Adresa e subjektit"));

        IWebElement summary = FindTextareaByLabel("Përmbledhje ekzekutive");
        Assert.That(InputValue(summary), Is.EqualTo(string.Empty));
        Assert.That(summary.GetAttribute("readonly"), Is.Null);
        Assert.That(FindLabel("Përmbledhje ekzekutive").Text, Does.Contain("*"));

        Log("Assert butonat e navigimit Step 1");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 1");
        FillInput(summary, "test123");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("INFORMACION MBI APLIKIMIN");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("INFORMACION MBI APLIKIMIN"));

        Log("Assert kohëzgjatja Step 2");
        AssertDuration("3 minuta kohëzgjatje");

        Log("Assert 3 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert qellimet e aplikimit");
        foreach (var checkbox in PurposeCheckboxes)
        {
            IWebElement input = wait.Until(ExpectedConditions.ElementExists(By.Id(checkbox.Id)));
            Assert.That(input.GetAttribute("type"), Is.EqualTo("checkbox"));
            Assert.That(input.Selected, Is.False);
            IWebElement label = wait.Until(ExpectedConditions.ElementIsVisible(
                By.CssSelector($"label[for='{checkbox.Id}']")));
            Assert.That(label.Text.Trim(), Is.EqualTo(checkbox.Label));
        }

        Log("Assert detajet e aktivitetit");
        Assert.That(driver.FindElement(
            By.XPath("//label[contains(.,'Detajet e aktivitetit')]")).Displayed, Is.True);

        IWebElement activityName = FindInputByLabel("Emërtimi i aktivitetit");
        Assert.That(InputValue(activityName), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Emërtimi i aktivitetit").Text, Does.Contain("*"));

        Assert.That(FindLabel("Forma e pronësisë").Text, Does.Contain("*"));
        IWebElement propertyPublic = wait.Until(ExpectedConditions.ElementExists(By.Id("propertyTypePublic")));
        IWebElement propertyPrivate = wait.Until(ExpectedConditions.ElementExists(By.Id("propertyTypePrivate")));
        Assert.That(propertyPublic.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(propertyPrivate.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(propertyPublic.Selected, Is.False);
        Assert.That(propertyPrivate.Selected, Is.False);
        Assert.That(driver.FindElement(By.CssSelector("label[for='propertyTypePublic']")).Text.Trim(),
            Is.EqualTo("Publike"));
        Assert.That(driver.FindElement(By.CssSelector("label[for='propertyTypePrivate']")).Text.Trim(),
            Is.EqualTo("Private"));

        Assert.That(FindLabel("Kohëzgjatja e ushtrimit të aktivitetit").Text, Does.Contain("*"));
        IWebElement startDate = FindDateInput("Data e Fillimit");
        IWebElement endDate = FindDateInput("Data e Përfundimit");
        Assert.That(InputValue(startDate), Is.EqualTo(string.Empty));
        Assert.That(InputValue(endDate), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Data e Fillimit").Text, Does.Contain("*"));
        Assert.That(FindLabel("Data e Përfundimit").Text, Does.Contain("*"));

        IWebElement activityAddress = FindTextareaByLabel("Adresa e plotë e aktivitetit");
        Assert.That(InputValue(activityAddress), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Adresa e plotë e aktivitetit").Text, Does.Contain("*"));

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme Step 2");
        FillInput(activityName, "test");
        SafeClick(By.Id("propertyTypePrivate"));
        FillDate(startDate, "14.04.2026", 2026, 4, 14);
        FillDate(endDate, "14.05.2026", 2026, 5, 14);
        FillInput(activityAddress, "test");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(), Does.Contain("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 3");
        AssertDuration("3 minuta kohëzgjatje");

        Log("Assert 3 hapa, te gjithe aktiv");
        AssertSteps(3);

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Dërgo");

        Log("Kliko Dergo pa ngarkuar dokumentin");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        IWebElement msgError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//*[contains(normalize-space(.),'Ju lutem ngarkoni dokumentin e kërkuar.')]")));
        Assert.That(msgError.Text.Trim(), Does.Contain("Ju lutem ngarkoni dokumentin e kërkuar."));

        Log("Ngarko dok jo te sakte");
        string lejeVeprimtarise = @"C:\Users\Kreatx\Downloads\Kthim Alfis test(1).pdf";
        string planimetria = @"C:\Users\Kreatx\Downloads\E88.30_CheckPointVPN.msi";
        string vertetimiPageses = @"C:\Users\Kreatx\Downloads\TC_TestAutomation_Mobiread.docx";

        Assert.That(File.Exists(lejeVeprimtarise), Is.True, "File LejeVeprimtarise nuk ekziston.");
        Assert.That(File.Exists(planimetria), Is.True, "File Planimetria nuk ekziston.");
        Assert.That(File.Exists(vertetimiPageses), Is.True, "File VertetimiPageses nuk ekziston.");

        UploadFileByDocumentTitle("Leje e veprimtarisë të lëshuar nga institucioni që mbulon veprimtarinë", lejeVeprimtarise);
        UploadFileByDocumentTitle("Planimetrinë e sipërfaqes", planimetria);
        UploadFileByDocumentTitle("Vërtetimin për pagesën", vertetimiPageses);

        Log("Assert uncorrect doc name");
        IWebElement fileDocNameError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[contains(@class,'text-danger') and contains(text(),'Emri i dokumentit është i pavlefshëm')]")));
        Assert.That(fileDocNameError.Displayed, Is.True);
        Assert.That(fileDocNameError.Text.Trim(), Does.Contain("Emri i dokumentit është i pavlefshëm"));

        Log("Assert uncorrect doc size");
        IWebElement fileDocSizeError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[contains(@class,'text-danger') and contains(text(),'Madhësia e dokumentit nuk duhet të jetë më shumë se  20MB')]")));
        Assert.That(fileDocSizeError.Displayed, Is.True);
        Assert.That(fileDocSizeError.Text.Trim(), Does.Contain("Madhësia e dokumentit nuk duhet të jetë më shumë se 20MB"));

        Log("Assert uncorrect doc format");
        IWebElement fileDocFormatError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[contains(@class,'text-danger') and contains(text(),'Formati duhet të jetë:  PDF, JPG, JPEG, PNG')]")));
        Assert.That(fileDocFormatError.Displayed, Is.True);
        Assert.That(fileDocFormatError.Text.Trim(), Does.Contain("Formati duhet të jetë: PDF, JPG, JPEG, PNG"));

        Log("Remove uncorrect docs");
        RemoveAllUploadedDocs();
        Thread.Sleep(1500);

        Log("Prit 1 minutë para ngarkimit të dokumentit të saktë…");
        Thread.Sleep(TimeSpan.FromMinutes(1));

        Log("Ngarko dok e sakte");
        Assert.That(File.Exists(DocumentPath), Is.True, "File i sakte nuk ekziston.");
        UploadFileByDocumentTitle("Leje e veprimtarisë të lëshuar nga institucioni që mbulon veprimtarinë", DocumentPath);
        UploadFileByDocumentTitle("Planimetrinë e sipërfaqes", DocumentPath);
        UploadFileByDocumentTitle("Vërtetimin për pagesën", DocumentPath);

        Log("Kliko checkbox e autorizimit");
        ClickAllMuiCheckboxes();

        ClickDergo();
        AssertDergoOutcome();

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
        Assert.That(serviceName.Text.Replace('\u00A0', ' ').Trim(), Is.EqualTo(ExpectedServiceName),
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
        Assert.That(perdorBtn.Text.Trim(), Does.Match("Përdor|Use"), "Butoni nuk eshte Përdor");

        Log("Kliko butonin Perdor");
        SafeClick(perdorLocator);

        Log("Kliko Aplikim i ri");
        By aplikimIRiLocator = By.CssSelector("button[aria-label='Aplikim i ri']");
        IWebElement aplikimIRi = wait.Until(ExpectedConditions.ElementIsVisible(aplikimIRiLocator));
        Assert.That(aplikimIRi.Displayed, Is.True, "Karta Aplikim i ri nuk eshte visible");
        IWebElement aplikimIRiTitle = aplikimIRi.FindElement(By.CssSelector("h6.mbx-title"));
        Assert.That(aplikimIRiTitle.Text.Trim(), Is.EqualTo("Aplikim i ri"),
            "Titulli i kartes nuk eshte Aplikim i ri");
        SafeClick(aplikimIRiLocator);
        Thread.Sleep(1500);
        DismissCookieBannerIfPresent();
    }

    private void AssertDuration(string expected)
    {
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain(expected));
    }

    private void AssertSteps(int activeCount)
    {
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(TotalSteps));
        for (int i = 0; i < steps.Count; i++)
        {
            if (i < activeCount)
                Assert.That(steps[i].GetAttribute("class"), Does.Contain("active"));
            else
                Assert.That(steps[i].GetAttribute("class"), Does.Not.Contain("active"));
            Assert.That(steps[i].GetAttribute("class"), Does.Contain("no-click"));
        }
    }

    private void AssertNavigationButtons(string continueText)
    {
        IWebElement backBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-back")));
        IWebElement continueBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(backBtn.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(continueBtn.Text.Trim(), Does.Contain(continueText));
    }

    private IWebElement WaitForStepTitle(string expectedUpper)
    {
        return wait.Until(d =>
        {
            var titles = d.FindElements(By.CssSelector("h4, h5"));
            foreach (var title in titles)
            {
                try
                {
                    string actual = title.Text.Trim().ToUpperInvariant();
                    if (actual == expectedUpper || actual.StartsWith(expectedUpper) || actual.Contains(expectedUpper))
                        return title;
                }
                catch (StaleElementReferenceException)
                {
                }
            }
            return null;
        });
    }

    private IWebElement FindLabel(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//label[contains(.,'{labelPart}')]")));
    }

    private IWebElement FindInputByLabel(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//label[contains(.,'{labelPart}')]/following-sibling::input[1]")));
    }

    private IWebElement FindTextareaByLabel(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//label[contains(.,'{labelPart}')]/following-sibling::textarea[1]")));
    }

    private IWebElement FindDateInput(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//label[contains(.,'{labelPart}')]/following::input[contains(@class,'flatpickr-input')][1]")));
    }

    private void AssertReadonlyByLabel(string labelPart, string expectedValue)
    {
        IWebElement input = FindInputByLabel(labelPart);
        Assert.That(InputValue(input), Is.EqualTo(expectedValue), $"Vlera e fushes {labelPart} nuk eshte e sakte");
        Assert.That(input.GetAttribute("readonly"), Is.Not.Null, $"Fusha {labelPart} duhet te jete readonly");
    }

    private void FillDate(IWebElement input, string displayDate, int year, int month, int day)
    {
        ((IJavaScriptExecutor)driver).ExecuteScript(@"
            const el = arguments[0];
            const display = arguments[1];
            const year = Number(arguments[2]);
            const month = Number(arguments[3]);
            const day = Number(arguments[4]);
            const date = new Date(year, month - 1, day);
            el.scrollIntoView({block:'center'});
            const wrap = el.closest('.flatpickr-wrapper') || el.parentElement;
            const inputs = [el, ...wrap.querySelectorAll('input')];
            const fpInput = inputs.find(i => i._flatpickr);
            if (fpInput && fpInput._flatpickr) {
                fpInput._flatpickr.setDate(date, true);
                fpInput._flatpickr.close();
            } else if (el._flatpickr) {
                el._flatpickr.setDate(date, true);
                el._flatpickr.close();
            } else {
                const setter = Object.getOwnPropertyDescriptor(window.HTMLInputElement.prototype, 'value').set;
                setter.call(el, display);
                el.dispatchEvent(new Event('input', { bubbles: true }));
                el.dispatchEvent(new Event('change', { bubbles: true }));
            }
        ", input, displayDate, year, month, day);

        wait.Until(_ =>
        {
            try
            {
                string current = input.GetAttribute("value") ?? string.Empty;
                return current.Length > 0 &&
                       (current.Contains(displayDate) || current.Contains(year.ToString()));
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        ((IJavaScriptExecutor)driver).ExecuteScript(@"
            document.querySelectorAll('.flatpickr-calendar.open').forEach(el => {
                el.classList.remove('open');
                el.style.display = 'none';
            });
            if (document.activeElement) document.activeElement.blur();
        ");
        Thread.Sleep(300);
    }

    private void UploadFileByDocumentTitle(string titlePart, string filePath)
    {
        IWebElement input = wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//div[contains(.,'{titlePart}')]/following::input[@type='file'][1]")));
        input.SendKeys(filePath);
        Thread.Sleep(500);
    }

    private void ClickAllMuiCheckboxes()
    {
        var checkboxes = driver.FindElements(By.CssSelector(".MuiCheckbox-root input[type='checkbox']"));
        foreach (var checkbox in checkboxes)
        {
            try
            {
                if (checkbox.Selected)
                    continue;

                ((IJavaScriptExecutor)driver).ExecuteScript(
                    "arguments[0].scrollIntoView({block:'center'}); arguments[0].click();",
                    checkbox);
                Thread.Sleep(200);
            }
            catch (StaleElementReferenceException)
            {
            }
        }
    }

    private void RemoveAllUploadedDocs()
    {
        Log("Hiq dok jo te sakta");

        int safetyCounter = 0;
        while (true)
        {
            var deleteButtons = driver.FindElements(By.CssSelector("button[aria-label='Delete file']"));
            Log("Nr. i butonave Delete file: " + deleteButtons.Count);

            var deleteBtn = deleteButtons.FirstOrDefault(b =>
            {
                try { return b.Displayed && b.Enabled; }
                catch { return false; }
            });

            if (deleteBtn == null)
            {
                Log("Nuk ka me dokumente per te hequr");
                break;
            }

            try
            {
                ((IJavaScriptExecutor)driver).ExecuteScript(
                    "arguments[0].scrollIntoView({ block: 'center' });",
                    deleteBtn);
                Thread.Sleep(300);
                try
                {
                    deleteBtn.Click();
                }
                catch
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", deleteBtn);
                }

                Log("U hoq nje dokument jo i sakte");
                Thread.Sleep(1000);
            }
            catch (StaleElementReferenceException)
            {
                Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                Log("Gabim gjate heqjes se dokumentit: " + ex.Message);
                break;
            }

            safetyCounter++;
            if (safetyCounter >= 10)
            {
                Log("Ndalo heqjen e dokumenteve per shkak te safetyCounter");
                break;
            }
        }

        Log("Te gjitha dok jo te sakta u hoqen");
    }
}
