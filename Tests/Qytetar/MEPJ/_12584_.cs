using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.MEPJ;

[Category("MEPJ")]
[Category("12584")]
public class _12584_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "12584";
    protected override string? ServiceTitle => "VerifikimILejesDrejtimit";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Aplikim për vërtetimin e verifikimit të lejes së drejtimit (patentës) për konvertim ose denoncim";
    private const string DocumentPath = @"C:\Users\Kreatx\Downloads\Test Dokument.pdf.pdf";
    private const string IdUploadId = "docIdentifikimi_vertCustUpload";

    [Test]
    public void VerifikimILejesDrejtimit()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert Title Step 1");
        IWebElement Step1Title = WaitForStepTitle("TË DHËNAT E APLIKANTIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNAT E APLIKANTIT"));

        Log("Assert kohëzgjatja");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 6 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert arsyeja e aplikimit");
        Assert.That(FindLabel("Arsyeja e aplikimit").Text, Does.Contain("*"));
        var arsyeja = new SelectElement(FindSelectByLabel("Arsyeja e aplikimit"));
        Assert.That(arsyeja.Options.Count, Is.EqualTo(2));
        Assert.That(arsyeja.Options[0].GetAttribute("value"), Is.EqualTo("1"));
        Assert.That(arsyeja.Options[0].Text.Trim(),
            Is.EqualTo("Informacion mbi Verifikimin për Leje Drejtimi"));
        Assert.That(arsyeja.Options[1].GetAttribute("value"), Is.EqualTo("2"));
        Assert.That(arsyeja.Options[1].Text.Trim(),
            Is.EqualTo("Informacion mbi Verifikimin për Leje Drejtimi për Konvertim"));
        Assert.That(arsyeja.SelectedOption.GetAttribute("value"), Is.EqualTo("1"));

        Log("Assert te dhenat e aplikantit te para-plotesuara");
        AssertDisabledByLabel("Nid", Settings.Qytetar.Username);
        AssertDisabledByLabel("Emri", "Katerina");
        AssertDisabledByLabel("Mbiemri", "Jançe");
        AssertDisabledByLabel("Datëlindja", "13.04.1993");
        AssertDisabledByLabel("Atësia", "Foti");
        AssertDisabledByLabel("Amësia", "Manushaqe");

        IWebElement gjinia = wait.Until(ExpectedConditions.ElementExists(By.Id("gjinia")));
        Assert.That(gjinia.GetAttribute("value").Trim(), Is.EqualTo("Femër"));
        Assert.That(gjinia.GetAttribute("disabled"), Is.Not.Null);

        IWebElement shtetesia = FindNamed("shtetesia");
        Assert.That(shtetesia.GetAttribute("value").Trim(), Is.EqualTo("Shqiptare"));
        Assert.That(shtetesia.GetAttribute("disabled"), Is.Null);
        Assert.That(FindLabel("Shtetësia").Text, Does.Contain("*"));

        IWebElement shtetesiaDyte = FindNamed("shtetesiaDyte");
        Assert.That(shtetesiaDyte.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(shtetesiaDyte.GetAttribute("disabled"), Is.Null);

        Log("Assert butonat e navigimit Step 1");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("ADRESA E APLIKANTIT (NE VENDIN E REZIDENCES)");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("ADRESA E APLIKANTIT (NE VENDIN E REZIDENCES)"));

        Log("Assert kohëzgjatja Step 2");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 6 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert fushat e adreses");
        Assert.That(FindLabel("Shteti").Text, Does.Contain("*"));
        Assert.That(FindLabel("Qyteti").Text, Does.Contain("*"));
        Assert.That(FindLabel("Rruga dhe numri i banesës").Text, Does.Contain("*"));
        Assert.That(FindLabel("Kodi postar").Text, Does.Contain("*"));

        IWebElement shteti = FindSelectByLabel("Shteti");
        Assert.That(new SelectElement(shteti).SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindInputByLabel("Qyteti").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindInputByLabel("Rruga dhe numri i banesës").GetAttribute("value").Trim(),
            Is.EqualTo(string.Empty));
        Assert.That(FindInputByLabel("Kodi postar").GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(FindInputByLabel("Rajoni/Ndarja administrative").GetAttribute("value").Trim(),
            Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme");
        SelectByValue(FindSelectByLabel("Shteti"), "2");
        FillInput(FindInputByLabel("Qyteti"), "test");
        FillInput(FindInputByLabel("Rruga dhe numri i banesës"), "test");
        FillInput(FindInputByLabel("Kodi postar"), "1001");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("DETAJET E LEJES SË DREJTIMIT");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("DETAJET E LEJES SË DREJTIMIT"));

        Log("Assert kohëzgjatja Step 3");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 6 hapa, tre te paret aktiv");
        AssertSteps(3);

        Log("Assert te dhenat e lejes se drejtimit");
        AssertDisabledByLabel("Numri i regjistrit", string.Empty);
        AssertDisabledByLabel("Data e lëshimit", string.Empty);
        AssertDisabledByLabel("E vlefshme deri", string.Empty);

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 4 Title");
        IWebElement Step4Title = WaitForStepTitle("KATEGORITË E LEJES SË DREJTIMIT");
        Assert.That(Step4Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("KATEGORITË E LEJES SË DREJTIMIT"));

        Log("Assert kohëzgjatja Step 4");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 6 hapa, kater te paret aktiv");
        AssertSteps(4);

        Log("Assert tabelen e kategorive");
        IWebElement table = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("table.MuiTable-root")));
        Assert.That(table.FindElement(By.XPath(".//th[normalize-space()='Nr.']")).Displayed, Is.True);
        Assert.That(table.FindElement(By.XPath(".//th[normalize-space()='Kategoria']")).Displayed, Is.True);
        Assert.That(table.FindElement(By.XPath(".//th[contains(.,'Data e lëshimit')]")).Displayed, Is.True);
        Assert.That(table.FindElement(By.XPath(".//th[contains(.,'Data e vlefshmërisë')]")).Displayed, Is.True);
        Assert.That(table.FindElement(By.XPath(".//th[contains(.,'Numër protokolli')]")).Displayed, Is.True);
        Assert.That(table.FindElement(
            By.XPath(".//td[@colspan='5' and contains(.,'Nuk u gjetën rezultate')]")).Displayed, Is.True);

        Log("Assert butonat e navigimit Step 4");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 4");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 5 Title");
        IWebElement Step5Title = WaitForStepTitle("KONTAKTI");
        Assert.That(Step5Title.Text.Trim().ToUpperInvariant(), Does.StartWith("KONTAKTI"));

        Log("Assert kohëzgjatja Step 5");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 6 hapa, pese te paret aktiv");
        AssertSteps(5);

        Log("Assert te dhenat e kontaktit");
        IWebElement email = FindInputByLabel("Adresa elektronike");
        Assert.That(email.GetAttribute("type"), Is.EqualTo("email"));
        Assert.That(email.GetAttribute("value").Trim(), Is.EqualTo("katerina.jance@kreatx.com"));
        Assert.That(email.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(FindLabel("Adresa elektronike").Text, Does.Contain("*"));

        IWebElement nrTel = FindInputByLabel("Nr. tel");
        Assert.That(nrTel.GetAttribute("type"), Is.EqualTo("tel"));
        Assert.That(nrTel.GetAttribute("value").Trim(), Is.EqualTo("+355697008820"));
        Assert.That(nrTel.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(nrTel.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(FindLabel("Nr. tel").Text, Does.Contain("*"));

        IWebElement country = FindSelectByLabel("Shteti");
        Assert.That(new SelectElement(country).SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Shteti").Text, Does.Contain("*"));

        IWebElement consularOffice = FindSelectByLabel("Zyra konsullore");
        Assert.That(new SelectElement(consularOffice).SelectedOption.GetAttribute("value"),
            Is.EqualTo(string.Empty));
        Assert.That(FindLabel("Zyra konsullore").Text, Does.Contain("*"));

        IWebElement nrCel = FindInputByLabel("Nr. cel");
        Assert.That(nrCel.GetAttribute("type"), Is.EqualTo("tel"));
        Assert.That(nrCel.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(nrCel.GetAttribute("disabled"), Is.Null);

        Log("Assert butonat e navigimit Step 5");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        AssertFieldError("Përzgjidhni një vlerë për të vazhduar");

        Log("Ploteso fushat e detyrueshme");
        SelectByValue(FindSelectByLabel("Shteti"), "Algjeria");
        SelectFirstWhenEnabled(
            By.XPath("//label[contains(.,'Zyra konsullore')]/following-sibling::select"));

        Log("Kliko Vazhdo Step 5");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 6 Title");
        IWebElement Step6Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(Step6Title.Text.Trim().ToUpperInvariant(), Does.StartWith("DOKUMENTACIONI"));

        Log("Assert kohëzgjatja Step 6");
        AssertDuration("4 minuta kohëzgjatje");

        Log("Assert 6 hapa, te gjithe aktiv");
        AssertSteps(6);

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës')]"))
            .Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(@class,'text-muted') and contains(.,'Fotokopje e Lejes së Drejtimit nga para')]"))
            .Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//p[contains(@class,'text-muted') and contains(.,'Fotokopje e Lejes së Drejtimit nga mbrapa')]"))
            .Displayed, Is.True);

        Log("Assert document-upload Fotokopje të Kartës së Identitetit ose Pasaportës");
        AssertDocumentUpload(IdUploadId, "Fotokopje të Kartës së Identitetit ose Pasaportës");

        Log("Assert checkbox dokumentacioni");
        IWebElement consentCheckbox = wait.Until(ExpectedConditions.ElementExists(By.Id("docConsentCheckbox")));
        Assert.That(consentCheckbox.GetAttribute("type"), Is.EqualTo("checkbox"));
        IWebElement consentLabel = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("label[for='docConsentCheckbox']")));
        Assert.That(consentLabel.Text.Trim(),
            Does.Contain("Me klikimin e këtij butoni, ju bini dakord"));

        Log("Assert butonat e navigimit Step 6");
        AssertNavigationButtons("Dërgo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Kliko Dergo pa ngarkuar dokumentin");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(1000);
        Assert.That(WaitForStepTitle("DOKUMENTACIONI").Text.Trim().ToUpperInvariant(),
            Does.StartWith("DOKUMENTACIONI"));

        Log("Ngarko dokumentin e detyrueshem");
        UploadDocument(IdUploadId, DocumentPath);

        Log("Kliko checkboxin e dokumentacionit");
        SafeClick(By.CssSelector("label[for='docConsentCheckbox']"));
        Thread.Sleep(500);

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
        Assert.That(perdorBtn.Text.Trim(), Is.EqualTo("Përdor"), "Butoni nuk eshte Përdor");

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
        Assert.That(steps.Count, Is.EqualTo(6));
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
            var titles = d.FindElements(By.CssSelector("h4.text-uppercase, h5.text-uppercase"));
            foreach (var title in titles)
            {
                string actual = title.Text.Trim().ToUpperInvariant();
                if (actual == expectedUpper || actual.StartsWith(expectedUpper))
                    return title;
            }
            return null;
        });
    }

    private IWebElement FindNamed(string name)
    {
        return wait.Until(ExpectedConditions.ElementExists(By.Name(name)));
    }

    private IWebElement FindLabel(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//label[contains(.,'{labelPart}')]")));
    }

    private IWebElement FindInputByLabel(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//label[contains(.,'{labelPart}')]/following-sibling::input")));
    }

    private IWebElement FindSelectByLabel(string labelPart)
    {
        return wait.Until(ExpectedConditions.ElementExists(
            By.XPath($"//label[contains(.,'{labelPart}')]/following-sibling::select")));
    }

    private void AssertDisabledByLabel(string labelPart, string expectedValue)
    {
        IWebElement field = FindInputByLabel(labelPart);
        Assert.That(field.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
        Assert.That(field.GetAttribute("disabled"), Is.Not.Null);
    }

    private void SelectByValue(IWebElement select, string value)
    {
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            select);
        Thread.Sleep(300);
        new SelectElement(select).SelectByValue(value);
        Thread.Sleep(800);
    }

    private void SelectFirstWhenEnabled(By locator)
    {
        var selectWait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        selectWait.Until(d =>
        {
            try
            {
                var els = d.FindElements(locator);
                if (els.Count == 0)
                    return false;
                var el = els[0];
                if (!el.Enabled)
                    return false;
                var se = new SelectElement(el);
                return se.Options.Any(o => !string.IsNullOrWhiteSpace(o.GetAttribute("value")));
            }
            catch
            {
                return false;
            }
        });

        IWebElement select = driver.FindElement(locator);
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            select);
        Thread.Sleep(300);

        var dropdown = new SelectElement(select);
        var options = dropdown.Options
            .Where(o => !string.IsNullOrWhiteSpace(o.GetAttribute("value")))
            .ToList();
        Assert.That(options, Is.Not.Empty, "Select nuk ka opsione te disponueshme");
        dropdown.SelectByValue(options[0].GetAttribute("value"));
        Thread.Sleep(1000);
    }

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {
        Assert.That(driver.FindElement(
            By.XPath($"//span[contains(@class,'fw-bold') and contains(normalize-space(),'{documentTitle}')]"))
            .Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-12584"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("15"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("15"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf,.jpg,.jpeg,.png"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të ngarkuar dokumentin"));

        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të ngarkuar dokumentin"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='hint']")).Text.Trim(),
            Is.EqualTo("Formatet e lejuara: PDF, JPG, JPEG, PNG. Madhësia maksimale: 15MB."));
    }

    private void UploadDocument(string uploadId, string filePath)
    {
        Assert.That(File.Exists(filePath), Is.True, "File nuk ekziston: " + filePath);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            docUpload);
        Thread.Sleep(300);

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
