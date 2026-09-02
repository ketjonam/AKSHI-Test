using AKSHI.Test.Core;
using System.Text.RegularExpressions;

namespace AKSHI.Test.Tests.Biznes.MSHMS;

[Category("MSHMS")]
[Category("15129")]
public class _15129_ : BiznesTestBase
{
    protected override string ServiceCode => "15129";
    protected override string? ServiceTitle => "NdryshimRegjistrimiPajisjeMjekesore";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName = "Aplikim për ndryshim regjistrimi pajisje mjekësore";
    private const string SignedPdf = @"C:\Users\Kreatx\Downloads\Signed_TEST_signed.pdf";

    [Test]
    public void NdryshimRegjistrimiPajisjeMjekesore()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("6 minuta kohëzgjatje"));

        Log("Assert 6 hapa, hapi i pare aktiv");
        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("section-a-form")));
        AssertActiveSteps(activeCount: 1, totalCount: 6);

        Log("Assert kolonat e tabeles se regjistrit");
        AssertTableHeader("Nr. Regjistrit");
        AssertTableHeader("Dt. Regjistrit");
        AssertTableHeader("Numri autorizimit");
        AssertTableHeader("Data autorizimit");
        AssertTableHeader("Nr.Certifikatës CE");
        AssertTableHeader("Dt. lëshimit të certifikatës");
        AssertTableHeader("Dt. skadencës së certifikatës");

        Log("Assert rreshtin e regjistrit dhe butonin Zgjidh");
        IWebElement zgjidhBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//table//button[normalize-space()='Zgjidh']")));
        Assert.That(zgjidhBtn.Displayed, Is.True);
        Assert.That(zgjidhBtn.Text.Trim(), Is.EqualTo("Zgjidh"));

        IWebElement registerRow = zgjidhBtn.FindElement(By.XPath("./ancestor::tr[1]"));
        var registerCells = registerRow.FindElements(By.CssSelector("td"));
        Assert.That(registerCells.Count, Is.GreaterThanOrEqualTo(8));
        Assert.That(registerCells[0].Text.Trim(), Is.EqualTo("22748"));
        Assert.That(registerCells[1].Text.Trim(), Is.EqualTo("17.04.2026"));

        Log("Assert butoni Kthehu ne Step 1 (pa Vazhdo)");
        IWebElement step1Back = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("#section-a-form button.ealb-btn-back")));
        Assert.That(step1Back.Text.Trim(), Is.EqualTo("Kthehu"));
        Assert.That(
            driver.FindElements(By.CssSelector("#section-a-form button.ealb-btn-continue")).Count,
            Is.EqualTo(0));

        Log("Kliko Zgjidh");
        SafeClick(By.XPath("//table//button[normalize-space()='Zgjidh']"));
        Thread.Sleep(2500);

        Log("Assert Step 2 title");
        IWebElement step2Title = WaitForStepTitle("INFORMACION MBI PËRFAQËSUESIN");
        Assert.That(NormalizeText(step2Title.Text),
            Does.Contain("INFORMACION MBI PËRFAQËSUESIN E PRODHUESIT TË HUAJ"));

        Log("Assert 7 hapa, dy te paret aktiv");
        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("section-b-form")));
        AssertActiveSteps(activeCount: 2, totalCount: 7);

        Log("Assert te dhenat e perfaqesuesit");
        AssertReadonlyByExactLabel("Emri i subjektit", "Migen Dërstila");
        AssertReadonlyByExactLabel("Emri i personit të kontaktit", "Migen Dërstila");
        AssertReadonlyByExactLabel("Telefoni/Fax", "0696674989", index: 0);
        AssertReadonlyByExactLabel("Emri i prodhuesit që përfaqëson", "test");
        AssertReadonlyByExactLabel("Qyteti/Shteti", "test", index: 0);
        AssertReadonlyByExactLabel("Qyteti/Shteti", "test", index: 1);
        AssertReadonlyByExactLabel("Emaili", "mario.sinanaj@kreatx.com");
        AssertReadonlyByExactLabel("Telefoni/Fax", string.Empty, index: 1);
        AssertReadonlyByExactLabel(
            "Adresa kryesore e prodhuesit në vendin e regjistrimit",
            "Derstile; ; ; ; Gjinar; ; 0000; Elbasan");
        AssertReadonlyByExactLabel("Adresa", string.Empty);

        Log("Assert Kategoria");
        IWebElement categorySelectEl = FindControlByLabelContains("Kategoria");
        var category = new SelectElement(categorySelectEl);
        Assert.That(category.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(category.Options.Count, Is.EqualTo(18));
        Assert.That(category.Options[1].GetAttribute("value"), Is.EqualTo("100"));
        Assert.That(category.Options[1].Text.Trim(), Is.EqualTo("Pajisje te implantueshme aktive"));
        Assert.That(category.Options[2].GetAttribute("value"), Is.EqualTo("110"));
        Assert.That(category.Options[2].Text.Trim(), Is.EqualTo("Pajisje anestezie dhe respiratore"));
        Assert.That(category.Options[3].GetAttribute("value"), Is.EqualTo("120"));
        Assert.That(category.Options[3].Text.Trim(), Is.EqualTo("Pajisje dentare"));
        Assert.That(category.Options[4].GetAttribute("value"), Is.EqualTo("130"));
        Assert.That(category.Options[4].Text.Trim(), Is.EqualTo("Pajisje elektromekanike"));
        Assert.That(category.Options[5].GetAttribute("value"), Is.EqualTo("140"));
        Assert.That(category.Options[5].Text.Trim(), Is.EqualTo("Pajisje mobilim spitalor"));
        Assert.That(category.Options[6].GetAttribute("value"), Is.EqualTo("150"));
        Assert.That(category.Options[6].Text.Trim(), Is.EqualTo("Pajisje te diagnostifikimit in vitro"));
        Assert.That(category.Options[7].GetAttribute("value"), Is.EqualTo("160"));
        Assert.That(category.Options[7].Text.Trim(), Is.EqualTo("Pajisje te implantueshme jo aktive"));
        Assert.That(category.Options[8].GetAttribute("value"), Is.EqualTo("170"));
        Assert.That(category.Options[8].Text.Trim(), Is.EqualTo("Pajisje oftalmike dhe optike"));
        Assert.That(category.Options[9].GetAttribute("value"), Is.EqualTo("180"));
        Assert.That(category.Options[9].Text.Trim(), Is.EqualTo("Instrumenta shumeperdorimeshe"));
        Assert.That(category.Options[10].GetAttribute("value"), Is.EqualTo("190"));
        Assert.That(category.Options[10].Text.Trim(), Is.EqualTo("Pajisje nje perdorimeshe"));
        Assert.That(category.Options[11].GetAttribute("value"), Is.EqualTo("200"));
        Assert.That(category.Options[11].Text.Trim(), Is.EqualTo("Pajisje asistive per persona me aftesi te kufizuara"));
        Assert.That(category.Options[12].GetAttribute("value"), Is.EqualTo("210"));
        Assert.That(category.Options[12].Text.Trim(), Is.EqualTo("Pajisje diagnostikuese dhe terapeutike me rezatim"));
        Assert.That(category.Options[13].GetAttribute("value"), Is.EqualTo("220"));
        Assert.That(category.Options[13].Text.Trim(), Is.EqualTo("Pajisje terapie komplementare"));
        Assert.That(category.Options[14].GetAttribute("value"), Is.EqualTo("230"));
        Assert.That(category.Options[14].Text.Trim(), Is.EqualTo("Pajisje me prejardhje biologjike"));
        Assert.That(category.Options[15].GetAttribute("value"), Is.EqualTo("240"));
        Assert.That(category.Options[15].Text.Trim(), Is.EqualTo("Produkte per institucione te kujdesit shendetesor"));
        Assert.That(category.Options[16].GetAttribute("value"), Is.EqualTo("250"));
        Assert.That(category.Options[16].Text.Trim(), Is.EqualTo("Pajisje laboratori"));
        Assert.That(category.Options[17].GetAttribute("value"), Is.EqualTo("260"));
        Assert.That(category.Options[17].Text.Trim(), Is.EqualTo("Tjeter"));

        Log("Assert butonat e navigimit Step 2");
        AssertBackAndContinue("Vazhdo");

        Log("Kliko Vazhdo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        Log("Assert error message per fushat e detyrueshme");
        IWebElement requiredError = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//label[contains(.,'Prodhuesi dhe adresa')]/following::*[contains(@class,'invalid-feedback') or contains(@class,'text-danger')][1]")));
        Assert.That(requiredError.Text.Trim(), Is.EqualTo("Plotësoni fushën për të vazhduar"));

        Log("Ploteso fushat e detyrueshme");
        FillControl(FindControlByLabelContains("Prodhuesi dhe adresa"), "test");
        new SelectElement(FindControlByLabelContains("Kategoria")).SelectByValue("100");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 title");
        IWebElement step3Title = WaitForStepTitle("INFORMACION MBI PAJISJEN MJEKËSORE");
        Assert.That(NormalizeText(step3Title.Text), Is.EqualTo("INFORMACION MBI PAJISJEN MJEKËSORE"));

        Log("Assert 7 hapa, tre te paret aktiv");
        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("section-c-form")));
        AssertActiveSteps(activeCount: 3, totalCount: 7);

        Log("Assert kolonat e tabeles se pajisjeve");
        AssertTableHeader("Emri i Pajisjes");
        AssertTableHeader("Klasa");
        AssertTableHeader("Kategoria");
        AssertTableHeader("Prodhuesi dhe Adresa");
        AssertTableHeader("Modeli(Emri dhe kodi)");
        AssertTableHeader("Përshkrimi");

        Log("Assert rreshtin e pajisjes");
        IWebElement deviceRow = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//div[@id='section-c-form']//tbody/tr")));
        var deviceCells = deviceRow.FindElements(By.CssSelector("td"));
        Assert.That(deviceCells.Count, Is.GreaterThanOrEqualTo(7));
        Assert.That(deviceCells[0].Text.Trim(), Is.EqualTo("test"));
        Assert.That(deviceCells[1].Text.Trim(), Is.EqualTo("I"));
        Assert.That(deviceCells[2].Text.Trim(), Is.EqualTo("260"));
        Assert.That(deviceCells[3].Text.Trim(), Is.EqualTo("test"));
        Assert.That(deviceCells[4].Text.Trim(), Is.EqualTo("test"));
        Assert.That(deviceCells[5].Text.Trim(), Is.EqualTo("test"));
        Assert.That(deviceRow.FindElement(By.CssSelector("span.delete-icon[title='Fshi']")).Displayed, Is.True);

        Log("Assert butoni + Shto Pajisje");
        IWebElement shtoPajisje = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//button[.//b[contains(.,'Shto Pajisje')]]")));
        Assert.That(shtoPajisje.Text.Trim(), Does.Contain("+ Shto Pajisje"));

        Log("Assert butonat e navigimit Step 3");
        AssertBackAndContinue("Vazhdo");

        Log("Kliko Vazhdo Step 3");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 4 title");
        IWebElement step4Title = WaitForStepTitle("INFORMACION MBI ORGANIN E MIRATUAR");
        Assert.That(NormalizeText(step4Title.Text), Is.EqualTo("INFORMACION MBI ORGANIN E MIRATUAR"));

        Log("Assert 7 hapa, kater te paret aktiv");
        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("section-d-form")));
        AssertActiveSteps(activeCount: 4, totalCount: 7);

        Log("Assert fushat e organit te miratuar");
        AssertReadonlyByExactLabel("Emri", string.Empty);
        AssertReadonlyByExactLabel("Numri i certifikatës CE", string.Empty);
        AssertReadonlyByExactLabel("Data e lëshimit", string.Empty);
        AssertReadonlyByExactLabel("Numri i trupit të miratuar", string.Empty);
        AssertReadonlyByExactLabel("Data e skadencës", string.Empty);
        AssertReadonlyByExactLabel(
            "Linku ku gjendet kjo certifikatë në websitin zyrtar të kompanisë apo të trupit të notifikuar",
            string.Empty);

        Log("Assert butonat e navigimit Step 4");
        AssertBackAndContinue("Vazhdo");

        Log("Kliko Vazhdo Step 4");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 5 Title");
        IWebElement step5Title = WaitForStepTitle("DOKUMENTACIONI");
        Assert.That(NormalizeText(step5Title.Text), Does.Contain("DOKUMENTACIONI"));

        Log("Assert 7 hapa, pese te paret aktiv");
        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("section-e-form")));
        AssertActiveSteps(activeCount: 5, totalCount: 7);

        Log("Assert seksionet e dokumenteve");
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që ngarkohen nga aplikanti')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//h6[contains(.,'Dokumente që sigurohen nga nëpunësit e administratës')]")).Displayed, Is.True);
        Assert.That(driver.FindElement(
            By.XPath("//*[contains(.,'Për këtë shërbim nuk nevojitet të sigurohet dokumentacion nga nëpunësi i administratës')]"))
            .Text.Trim(),
            Does.Contain("Për këtë shërbim nuk nevojitet të sigurohet dokumentacion nga nëpunësi i administratës"));

        Log("Assert document-upload");
        AssertDocumentUpload("doc_20Upload", "Certifikata CE/FDA");
        AssertDocumentUpload("doc_30Upload", "Autorizimi për tregtim");
        AssertDocumentUpload("doc_300Upload", "Lista e pajisjeve mjekesore");
        AssertDocumentUpload("doc_40Upload", "Deklarata e konformitetit");
        AssertDocumentUpload("doc_50Upload", "Autorizimi i aplikuesit");
        AssertDocumentUpload("doc_60Upload", "Lista e standardeve");
        AssertDocumentUpload("doc_70Upload", "Autorizimi nga prodhuesi");
        AssertDocumentUpload("doc_90Upload", "Dokumente të tjera");

        Log("Assert butoni Dergo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.Text.Trim(), Does.Contain("Dërgo"));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Kliko Dergo pa ngarkuar dokumentin");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));

        const string requiredDocError = "Ju lutem ngarkoni dokumentin e kërkuar.";
        var errorLoc = Page.GetByText(requiredDocError);
        wait.Until(_ => errorLoc.CountAsync().GetAwaiter().GetResult() > 0);
        Assert.That(
            errorLoc.First.InnerTextAsync().GetAwaiter().GetResult().Trim(),
            Is.EqualTo(requiredDocError));

        Log("Ngarko dokumentet e detyrueshme");
        UploadDocument("doc_20Upload", SignedPdf);
        UploadDocument("doc_30Upload", SignedPdf);
        UploadDocument("doc_300Upload", SignedPdf);
        UploadDocument("doc_40Upload", SignedPdf);
        UploadDocument("doc_50Upload", SignedPdf);
        UploadDocument("doc_60Upload", SignedPdf);
        UploadDocument("doc_70Upload", SignedPdf);

        // Log("Kliko Dergo");
        // ClickDerghoAfterDocumentationReady();
        // AssertSuccessOrKujdesAfterDergo();

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
        DismissCookieBannerIfPresent();
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

    private IWebElement FindControlByExactLabel(string label, int index = 0)
    {
        By labelBy = By.XPath(
            $"//label[contains(@class,'form-label') and normalize-space()='{label}']");
        wait.Until(d => d.FindElements(labelBy).Count > index);
        IWebElement labelEl = driver.FindElements(labelBy)[index];
        return labelEl.FindElement(By.XPath(
            "./following-sibling::*[self::input or self::textarea or self::select] | ./following-sibling::div//*[self::input or self::textarea or self::select][1]"));
    }

    private IWebElement FindControlByLabelContains(string labelPart)
    {
        IWebElement labelEl = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//label[contains(@class,'form-label') and contains(normalize-space(),'{labelPart}')]")));
        return labelEl.FindElement(By.XPath(
            "./following-sibling::*[self::input or self::textarea or self::select] | ./following-sibling::div//*[self::input or self::textarea or self::select][1]"));
    }

    private void AssertReadonlyByExactLabel(string label, string expectedValue, int index = 0)
    {
        IWebElement input = FindControlByExactLabel(label, index);
        Assert.That(input.GetAttribute("readonly") ?? input.GetAttribute("disabled"), Is.Not.Null,
            $"Fusha '{label}' duhet te jete readonly");
        string actual = input.TagName.Equals("textarea", StringComparison.OrdinalIgnoreCase)
            ? (input.GetAttribute("value") ?? input.Text)
            : (input.GetAttribute("value") ?? string.Empty);
        Assert.That(actual.Trim(), Is.EqualTo(expectedValue),
            $"Vlera e fushes '{label}' nuk eshte e sakte");
    }

    private void FillControl(IWebElement input, string value)
    {
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

    private void AssertActiveSteps(int activeCount, int totalCount)
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

    private void AssertDocumentUpload(string uploadId, string documentTitle)
    {
        Assert.That(driver.FindElement(
            By.XPath($"//span[contains(normalize-space(),'{documentTitle}')]")).Displayed, Is.True);

        IWebElement docUpload = wait.Until(ExpectedConditions.ElementExists(By.Id(uploadId)));
        Assert.That(docUpload.GetAttribute("application-reference"), Is.EqualTo("docstreamv2-15129"));
        Assert.That(docUpload.GetAttribute("selection-mode"), Is.EqualTo("single"));
        Assert.That(docUpload.GetAttribute("max-single-file-mb"), Is.EqualTo("50"));
        Assert.That(docUpload.GetAttribute("max-total-files-mb"), Is.EqualTo("50"));
        Assert.That(docUpload.GetAttribute("file-types"), Is.EqualTo(".pdf,.jpg,.jpeg,.png"));
        Assert.That(docUpload.GetAttribute("button-label"), Is.EqualTo("Kliko për të ngarkuar dokumentin"));

        ISearchContext shadow = docUpload.GetShadowRoot();
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='label']")).Text.Trim(),
            Is.EqualTo("Ju lutemi ngarkoni dokumentin!"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='dropzone-text']")).Text.Trim(),
            Is.EqualTo("Kliko për të ngarkuar dokumentin"));
        Assert.That(shadow.FindElement(By.CssSelector("[data-role='hint']")).Text.Trim(),
            Is.EqualTo("Formatet e lejuara: PDF, JPG, JPEG, PNG. Madhësia maksimale: 50MB."));
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

    private IWebElement FindDerghoButtonInMain()
    {
        var candidates = driver.FindElements(
            By.XPath("//main//button[contains(normalize-space(.), 'Dërgo') or contains(normalize-space(.), 'Dergo')]"));
        IWebElement? pick = candidates.LastOrDefault(e =>
        {
            try
            {
                return e.Displayed;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });
        if (pick is null && candidates.Count > 0)
            pick = candidates[^1];
        if (pick is null)
            throw new NoSuchElementException("Nuk u gjet butoni 'Dërgo' brenda main.");
        return pick;
    }

    private void ClickDerghoAfterDocumentationReady()
    {
        var sendWait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
        sendWait.Until(drv =>
        {
            try
            {
                var b = FindDerghoButtonInMain();
                return b.Displayed && b.Enabled;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        });

        IWebElement dergo = FindDerghoButtonInMain();
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center', inline:'nearest'});",
            dergo);
        Thread.Sleep(400);
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", dergo);
        Log("Klikuar butoni 'Dërgo' (JavaScript click pasi u aktivizua).");
    }

    private void AssertSuccessOrKujdesAfterDergo()
    {
        const string successHeadline = "APLIKIMI JUAJ U DËRGUA ME SUKSES";
        const string alertExpectedTitle = "Kujdes";
        const string alertExpectedDescription =
            "Ekzistojne aplikime te pa perfunduara per kete mjet.";

        By successHeadlineBy = By.XPath(
            "//h5[contains(normalize-space(.),'APLIKIMI JUAJ U DËRGUA ME SUKSES')] | //h5/b[contains(normalize-space(.),'APLIKIMI JUAJ U DËRGUA ME SUKSES')]");
        By alertModalBy = By.CssSelector(".alert-modal-container");

        string? outcome = null;
        try
        {
            outcome = new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(drv =>
            {
                try
                {
                    var successEls = drv.FindElements(successHeadlineBy);
                    if (successEls.Any(e =>
                    {
                        try { return e.Displayed; }
                        catch (StaleElementReferenceException) { return false; }
                    }))
                        return "success";
                }
                catch (StaleElementReferenceException)
                {
                }

                try
                {
                    var alertEls = drv.FindElements(alertModalBy);
                    if (alertEls.Any(e =>
                    {
                        try { return e.Displayed; }
                        catch (StaleElementReferenceException) { return false; }
                    }))
                        return "alert";
                }
                catch (StaleElementReferenceException)
                {
                }

                return null;
            });
        }
        catch (WebDriverTimeoutException)
        {
        }

        if (outcome == "success")
        {
            Log("Pas 'Dërgo' u shfaq ekrani i suksesit.");
            IWebElement headline = wait.Until(ExpectedConditions.ElementIsVisible(successHeadlineBy));
            Assert.That(headline.Text.Trim(), Does.Contain(successHeadline).IgnoreCase);

            var refEls = driver.FindElements(
                By.XPath("//h6[contains(normalize-space(.),'Numri referencë i aplikimit')]"));
            var trackEls = driver.FindElements(
                By.XPath("//button[contains(normalize-space(.),'GJURMO APLIKIMIN')]"));
            bool hasRef = refEls.Any(e =>
            {
                try { return e.Displayed; }
                catch (StaleElementReferenceException) { return false; }
            });
            bool hasTrack = trackEls.Any(e =>
            {
                try { return e.Displayed; }
                catch (StaleElementReferenceException) { return false; }
            });

            if (hasRef && hasTrack)
            {
                IWebElement referenceLine = refEls.First(e =>
                {
                    try { return e.Displayed; }
                    catch (StaleElementReferenceException) { return false; }
                });
                Assert.That(
                    referenceLine.Text.Trim(),
                    Does.Contain("Numri referencë i aplikimit është:").IgnoreCase);
                Assert.That(
                    referenceLine.Text.Trim(),
                    Does.Match("(?i)eALB-\\d+"));

                IWebElement trackBtn = trackEls.First(e =>
                {
                    try { return e.Displayed; }
                    catch (StaleElementReferenceException) { return false; }
                });
                Assert.That(trackBtn.Displayed, Is.True);
                Log("Sukses i verifikuar: headline, referenca eALB dhe butoni GJURMO APLIKIMIN.");
            }
            else
            {
                Log("Sukses i verifikuar: headline (eALB/GJURMO nuk u gjetën).");
            }
        }
        else if (outcome == "alert")
        {
            Log("Aplikimi u dërgua: sistemi u përgjigj dhe u shfaq modal paralajmërimi 'Kujdes'.");
            IWebElement alertModal = driver.FindElement(alertModalBy);
            IWebElement modalTitle = alertModal.FindElement(By.CssSelector("h2.alert-modal-title"));
            Assert.That(modalTitle.Text.Trim(), Is.EqualTo(alertExpectedTitle));

            var descEls = alertModal.FindElements(By.CssSelector(".alert-modal-description"));
            if (descEls.Count > 0)
            {
                Assert.That(descEls[0].Text.Trim(), Is.EqualTo(alertExpectedDescription));
            }

            IWebElement mbyllBtn = alertModal.FindElement(
                By.CssSelector("button.alert-modal-button--primary"));
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].scrollIntoView({block:'center'});",
                mbyllBtn);
            Thread.Sleep(300);
            try
            {
                mbyllBtn.Click();
            }
            catch (ElementClickInterceptedException)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", mbyllBtn);
            }
        }
        else
        {
            Assert.Fail(
                "Pas 'Dërgo' nuk u shfaq as ekrani i suksesit ('APLIKIMI JUAJ U DËRGUA ME SUKSES') " +
                "as modal paralajmërimi 'Kujdes' (.alert-modal-container).");
        }
    }

    private static string NormalizeText(string? value)
    {
        return Regex.Replace(value ?? string.Empty, @"\s+", " ").Trim().ToUpperInvariant();
    }
}
