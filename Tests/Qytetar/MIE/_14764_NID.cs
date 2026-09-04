using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.MIE;

[Category("MIE")]
[Category("14764")]
public class _14764_NID : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "14764";
    protected override string? ServiceTitle => "_14764_NID_AplikimiRI";
    protected override ServiceStartMode StartMode => ServiceStartMode.NewApplication;
    protected override bool StartServiceOnSetup => false;

    [Test]
    public void _14764_NID_AplikimiRI()
    {
        OpenNewApplicationFromServicePage();

                Log("Të dhënat e preventivit");
                IWebElement DetajetePreventivit = wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/h5"))
                );
                Assert.That(DetajetePreventivit.Text.Trim(), Is.EqualTo("TË DHËNAT E PREVENTIVIT"));

                Log("Click 'Vazhdo' without required field");
                IWebElement vazhdoBtn = wait.Until(
                    ExpectedConditions.ElementToBeClickable(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div/button[2]"))
                );
                ScrollIntoView(driver, vazhdoBtn);
                Thread.Sleep(300);
                vazhdoBtn.Click();

                Log("Assert error message for required fields");
                IWebElement requiredFieldError = wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/form/div/div[1]/div"))
                );
                Assert.That(requiredFieldError.Text, Does.Contain("Plotësoni fushën për të vazhduar"));

                Thread.Sleep(500);
                Log("Fill in required fields");

                IWebElement projectorInput = driver.FindElement(By.Name("projector"));
                projectorInput.Clear();
                projectorInput.SendKeys("Test");
                projectorInput.SendKeys(Keys.Tab);

                IWebElement constructionInput = driver.FindElement(By.Name("construction"));
                constructionInput.Clear();
                constructionInput.SendKeys("Test");
                constructionInput.SendKeys(Keys.Tab);

                IWebElement dateInput = driver.FindElement(
                    By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/form/div/div[4]/div/input")
                );
                dateInput.Clear();
                dateInput.Click();
                Thread.Sleep(200);
                dateInput.SendKeys(Keys.Control + "a");
                dateInput.SendKeys(Keys.Delete);
                dateInput.SendKeys("27.03.2026");
                dateInput.SendKeys(Keys.Tab);

                ((IJavaScriptExecutor)driver).ExecuteScript(
                    "arguments[0].dispatchEvent(new Event('input', { bubbles: true }));",
                    dateInput
                );
                ((IJavaScriptExecutor)driver).ExecuteScript(
                    "arguments[0].dispatchEvent(new Event('change', { bubbles: true }));",
                    dateInput
                );

                Thread.Sleep(700);

                Log("Click Vazhdo button - Step 1");
                vazhdoBtn = wait.Until(
                    ExpectedConditions.ElementExists(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div/button[2]"))
                );

                ScrollIntoView(driver, vazhdoBtn);
                Thread.Sleep(400);
                BlurActiveElement(driver);
                Thread.Sleep(300);

                try
                {
                    wait.Until(ExpectedConditions.ElementToBeClickable(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div/button[2]"))).Click();
                }
                catch
                {
                    vazhdoBtn = driver.FindElement(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div/button[2]")
                    );
                    ScrollIntoView(driver, vazhdoBtn);
                    Thread.Sleep(300);
                    ClickJs(driver, vazhdoBtn);
                }

                Thread.Sleep(1200);

                Log("Assert Zerat e Preventivit");
                IWebElement Step2Title = wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div[1]/h4"))
                );
                Assert.That(Step2Title.Text.Trim(), Is.EqualTo("ZËRAT E PREVENTIVIT"));

                Log("Click 'Zë nga Manuali' button");
                IWebElement manualiBtn = wait.Until(
                    ExpectedConditions.ElementToBeClickable(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div[2]/div[1]/div[2]/button"))
                );
                manualiBtn.Click();

                Log("Assert Modal title 'Zgjidh Analizen'");
                IWebElement modalTitle = wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div[7]/div/div[1]/h2"))
                );
                Assert.That(modalTitle.Text.Trim(), Is.EqualTo("Zgjidh analizën"));

                Log("Fill amount");
                IWebElement amountInput = wait.Until(
                    ExpectedConditions.ElementExists(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div[7]/div/div[2]/div/div/div/div[2]/table/tbody/tr[3]/td[8]/input"))
                );

                Thread.Sleep(500);
                amountInput.SendKeys("2");

                Thread.Sleep(500);
                Log("Click 'Zgjidh' button in modal");
                IWebElement zgjedhBtn = wait.Until(
                    ExpectedConditions.ElementToBeClickable(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div[7]/div/div[2]/div/div/div/div[2]/table/tbody/tr[3]/td[9]/button"))
                );
                zgjedhBtn.Click();

                Thread.Sleep(1000);

                Log("Assert that 'analiza' is updated in the main table");
                IWebElement analizaCell = wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div[2]/div[1]/div[1]/div[1]/table/tbody/tr"))
                );
                Assert.That(analizaCell.Text, Does.Contain("Prodhim rërë lumi"));

                Log("Click 'Dergo' without required field");
                IWebElement DergoBtn = wait.Until(
                    ExpectedConditions.ElementExists(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div/button[2]"))
                );

                ((IJavaScriptExecutor)driver).ExecuteScript(
                    "arguments[0].scrollIntoView({block:'center'});",
                    DergoBtn
                );

                Thread.Sleep(500);
                wait.Until(ExpectedConditions.ElementToBeClickable(DergoBtn)).Click();

                Log("Assert error message for required fields in step 2");
                IWebElement requiredFieldErrorStep2 = wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div[4]/div[2]/div"))
                );
                Assert.That(requiredFieldErrorStep2.Text, Does.Contain("Plotësoni fushën për të vazhduar"));

                Log("Assert that previously filled data is retained in step 2");
                driver.FindElement(By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div[4]/div[2]/input")).SendKeys("Test");



                //Log("Click 'Dergo' button");
                //IWebElement dergoFinalBtn = wait.Until(
                //ExpectedConditions.ElementExists(
                //By.XPath("//button[contains(normalize-space(),'Dërgo')]"))
                // );

                //((IJavaScriptExecutor)driver).ExecuteScript(
                // "arguments[0].scrollIntoView({block:'center'});",
                // dergoFinalBtn
                //);

                //Thread.Sleep(500);

                //try
                //{
                //wait.Until(ExpectedConditions.ElementToBeClickable(
                // By.XPath("//button[contains(normalize-space(),'Dërgo')]"))).Click();
                //  }
                //catch (ElementClickInterceptedException)
                //{
                //dergoFinalBtn = driver.FindElement(By.XPath("//button[contains(normalize-space(),'Dërgo')]"));
                //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", dergoFinalBtn);
                //}

                //Thread.Sleep(1500);

                // Kontrollo nese del popup "Kujdes!"
                //var kujdesPopups = driver.FindElements(By.CssSelector(".alert-modal-container"));

                //if (kujdesPopups.Count > 0 && kujdesPopups[0].Displayed)
                //  {
                //Log("Popup 'Kujdes' u shfaq");

                //IWebElement kujdesTitle = wait.Until(
                //   ExpectedConditions.ElementIsVisible(By.CssSelector(".alert-modal-title"))
                //  );
                //  Assert.That(kujdesTitle.Text.Trim(), Is.EqualTo("Kujdes!"));

                // IWebElement kujdesDescription = wait.Until(
                //      ExpectedConditions.ElementIsVisible(By.CssSelector(".alert-modal-description"))
                // );
                // Assert.That(
                //   kujdesDescription.Text.Trim(),
                //  Is.EqualTo("Ju keni nje aplikim ne proces per kete lloj license!")
                // );

                //IWebElement okBtn = wait.Until(
                //    ExpectedConditions.ElementToBeClickable(By.CssSelector(".alert-modal-button--primary"))
                // );
                // okBtn.Click();

                //  Log("Testi perfundon me klikimin e popup 'Kujdes!'");
                // return;
                //  }

                //  Log("Assert success page");
                // IWebElement successTitle = wait.Until(
                //    ExpectedConditions.ElementIsVisible(
                //        By.XPath("//h5/b[contains(normalize-space(),'APLIKIMI JUAJ U DËRGUA ME SUKSES')]"))
                //);
                //Assert.That(successTitle.Displayed, Is.True);
                //Assert.That(successTitle.Text.Trim(), Is.EqualTo("APLIKIMI JUAJ U DËRGUA ME SUKSES."));

                //IWebElement referenceNumber = wait.Until(
                //    ExpectedConditions.ElementIsVisible(
                //        By.XPath("//h6[contains(.,'Numri referencë i aplikimit është')]//b"))
                //);
                //Assert.That(referenceNumber.Displayed, Is.True);
                //Assert.That(referenceNumber.Text.Trim(), Is.Not.Empty);

                //IWebElement gjurmoBtn = wait.Until(
                //    ExpectedConditions.ElementIsVisible(
                //        By.XPath("//button[contains(.,'Gjurmo Aplikimin')]"))
                //);
                //Assert.That(gjurmoBtn.Displayed, Is.True);

                //Log("TEST PASSED");
    }

    private void ScrollIntoView(IWebDriver driver, IWebElement element)
    {

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            element
        );
    }

    private void BlurActiveElement(IWebDriver driver)
    {

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "if (document.activeElement) document.activeElement.blur();"
        );
    }

    private void ClickJs(IWebDriver driver, IWebElement element)
    {

        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
    }
}