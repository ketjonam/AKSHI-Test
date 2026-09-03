using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.AMS;

[Category("AMS")]
[Category("11145")]
public class _11145_ : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "11145";
    protected override string? ServiceTitle => "VertetimPerGradatShkencore";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    private const string ExpectedServiceName =
        "Aplikim për vërtetim për gradat shkencore dhe titujt akademikë";
    private const string ExpectedAddress =
        "FROSINA PLAKU; Nd. 88; H. 2; Ap. 9; NJËSIA ADMINISTRATIVE NR. 7; NJËSIA BASHKIAKE NR. 7; 1023; TIRANË";

    [Test]
    public void VertetimPerGradatShkencore()
    {
        OpenNewApplicationFromServicePage();

        Log("Assert kohëzgjatja");
        IWebElement durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 3 hapa, hapi i pare aktiv");
        AssertSteps(1);

        Log("Assert Step 1 Title");
        IWebElement Step1Title = WaitForStepTitle("TË DHËNAT E APLIKANTIT");
        Assert.That(Step1Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("TË DHËNAT E APLIKANTIT"));

        Log("Assert te dhenat e aplikantit te para-plotesuara");
        AssertReadonlyFieldByLabel("NID", Settings.Qytetar.Username);
        AssertReadonlyFieldByLabel("Emri", "Katerina");
        AssertReadonlyFieldByLabel("Mbiemri", "Jançe");
        AssertReadonlyFieldByLabel("Atësia", "Foti");
        AssertReadonlyFieldByLabel("Datëlindja", "13/04/1993");

        Log("Assert gjinia");
        IWebElement genderMale = wait.Until(ExpectedConditions.ElementExists(By.Id("genderMale")));
        IWebElement genderFemale = wait.Until(ExpectedConditions.ElementExists(By.Id("genderFemale")));
        Assert.That(genderMale.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(genderMale.GetAttribute("value"), Is.EqualTo("Male"));
        Assert.That(genderMale.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(driver.FindElement(By.CssSelector("label[for='genderMale']")).Text.Trim(),
            Is.EqualTo("Mashkull"));
        Assert.That(genderFemale.GetAttribute("type"), Is.EqualTo("radio"));
        Assert.That(genderFemale.GetAttribute("value"), Is.EqualTo("Female"));
        Assert.That(genderFemale.GetAttribute("disabled"), Is.Not.Null);
        Assert.That(driver.FindElement(By.CssSelector("label[for='genderFemale']")).Text.Trim(),
            Is.EqualTo("Femër"));
        Assert.That(genderFemale.Selected, Is.True);

        Log("Assert butonat e navigimit Step 1");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 1");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 2 Title");
        IWebElement Step2Title = WaitForStepTitle("INFORMACION I KONTAKTIT TË APLIKANTIT");
        Assert.That(Step2Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("INFORMACION I KONTAKTIT TË APLIKANTIT"));

        Log("Assert kohëzgjatja Step 2");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 3 hapa, dy te paret aktiv");
        AssertSteps(2);

        Log("Assert te dhenat e kontaktit");
        IWebElement cityVillage = FindNamed("cityVillage");
        Assert.That(cityVillage.GetAttribute("value").Trim(), Is.EqualTo("TIRANË"));
        Assert.That(cityVillage.GetAttribute("readonly"), Is.Not.Null);

        IWebElement email = FindNamed("email");
        Assert.That(email.GetAttribute("type"), Is.EqualTo("email"));
        Assert.That(email.GetAttribute("value").Trim(), Is.EqualTo("katerina.jance@kreatx.com"));
        Assert.That(email.GetAttribute("readonly"), Is.Not.Null);

        IWebElement mobilePhone = FindNamed("mobilePhone");
        Assert.That(mobilePhone.GetAttribute("value").Trim(), Is.EqualTo("+355697008820"));
        Assert.That(mobilePhone.GetAttribute("readonly"), Is.Not.Null);

        IWebElement landlinePhone = FindNamed("landlinePhone");
        Assert.That(landlinePhone.GetAttribute("type"), Is.EqualTo("number"));
        Assert.That(landlinePhone.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));
        Assert.That(landlinePhone.GetAttribute("readonly"), Is.Null);

        IWebElement address = FindNamed("address");
        Assert.That(address.GetAttribute("value").Trim(), Is.EqualTo(ExpectedAddress));
        Assert.That(address.GetAttribute("readonly"), Is.Not.Null);

        Log("Assert butonat e navigimit Step 2");
        AssertNavigationButtons("Vazhdo");

        Log("Kliko Vazhdo Step 2");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Thread.Sleep(3000);

        Log("Assert Step 3 Title");
        IWebElement Step3Title = WaitForStepTitle("INFORMACION MBI TITULLIN SHKENCOR");
        Assert.That(Step3Title.Text.Trim().ToUpperInvariant(),
            Does.StartWith("INFORMACION MBI TITULLIN SHKENCOR"));

        Log("Assert kohëzgjatja Step 3");
        durationBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-5minutes")));
        Assert.That(durationBtn.Text.Trim(), Does.Contain("4 minuta kohëzgjatje"));

        Log("Assert 3 hapa, te gjithe aktiv");
        AssertSteps(3);

        Log("Assert fushat e titullit shkencor");
        IWebElement academicTitle = FindNamed("academicTitle");
        var academicTitleSelect = new SelectElement(academicTitle);
        Assert.That(academicTitle.GetAttribute("required"), Is.Not.Null);
        Assert.That(academicTitleSelect.SelectedOption.GetAttribute("value"), Is.EqualTo(string.Empty));
        Assert.That(academicTitleSelect.Options.Count, Is.EqualTo(8));
        Assert.That(academicTitleSelect.Options[1].GetAttribute("value"), Is.EqualTo("Doktor"));
        Assert.That(academicTitleSelect.Options[2].GetAttribute("value"), Is.EqualTo("Drejtues Kërkimesh"));
        Assert.That(academicTitleSelect.Options[3].GetAttribute("value"), Is.EqualTo("Kandidat i Shkencave"));
        Assert.That(academicTitleSelect.Options[4].GetAttribute("value"), Is.EqualTo("Mjeshtër i Kerkimeve"));
        Assert.That(academicTitleSelect.Options[5].GetAttribute("value"), Is.EqualTo("Profesor"));
        Assert.That(academicTitleSelect.Options[6].GetAttribute("value"), Is.EqualTo("Profesor i Asociuar"));
        Assert.That(academicTitleSelect.Options[7].GetAttribute("value"), Is.EqualTo("other"));
        Assert.That(academicTitleSelect.Options[7].Text.Trim(), Is.EqualTo("Tjetër"));

        IWebElement institution = FindNamed("institution");
        Assert.That(institution.GetAttribute("required"), Is.Not.Null);
        Assert.That(institution.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement decisionNumber = FindNamed("decisionNumber");
        Assert.That(decisionNumber.GetAttribute("required"), Is.Not.Null);
        Assert.That(decisionNumber.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        IWebElement decisionDate = FindNamed("decisionDate");
        Assert.That(decisionDate.GetAttribute("value").Trim(), Is.EqualTo(string.Empty));

        Log("Assert butonat e navigimit Step 3");
        AssertNavigationButtons("Dërgo");
        IWebElement dergoBtn = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("button.ealb-btn-continue")));
        Assert.That(dergoBtn.GetAttribute("class"), Does.Contain("with-arrow"));

        Log("Kliko Dergo pa plotesuar fushat e detyrueshme");
        SafeClick(By.CssSelector("button.ealb-btn-continue"));
        Assert.That(WaitForStepTitle("INFORMACION MBI TITULLIN SHKENCOR")
            .Text.Trim().ToUpperInvariant(),
            Does.StartWith("INFORMACION MBI TITULLIN SHKENCOR"));
        AssertFieldError("Plotësoni fushën për të vazhduar");

        Log("Ploteso fushat e detyrueshme");
        academicTitle = FindNamed("academicTitle");
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            academicTitle);
        Thread.Sleep(300);
        new SelectElement(academicTitle).SelectByValue("Drejtues Kërkimesh");
        FillInput(FindNamed("institution"), "Universiteti i Tiranës");
        FillInput(FindNamed("decisionNumber"), "3434");

        ClickDergo();

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
        Assert.That(perdorBtn.Text.Trim(), Is.EqualTo("Përdor"), "Butoni nuk eshte Përdor");

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

    private void AssertSteps(int activeCount)
    {
        var steps = driver.FindElements(By.CssSelector(".ealb-step"));
        Assert.That(steps.Count, Is.EqualTo(3));
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
            var titles = d.FindElements(By.CssSelector(
                "h5.px-4.my-2.text-uppercase, h4.px-4.pb-4, h4.text-uppercase"));
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
        return wait.Until(ExpectedConditions.ElementExists(
            By.CssSelector($"[name='{name}']")));
    }

    private IWebElement FindFieldByLabel(string labelText)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//label[contains(normalize-space(),'{labelText}')]/following-sibling::*[self::input or self::select][1]")));
    }

    private void AssertReadonlyFieldByLabel(string labelText, string expectedValue)
    {
        IWebElement field = FindFieldByLabel(labelText);
        Assert.That(field.GetAttribute("readonly"), Is.Not.Null);
        Assert.That(field.GetAttribute("value").Trim(), Is.EqualTo(expectedValue));
    }
}
