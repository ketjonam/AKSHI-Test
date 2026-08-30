using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Biznes.MIE;

[Category("MIE")]
[Category("11143")]
public class _11143_BiznesWEB : BiznesTestBase
{
    protected override string ServiceCode => "11143";
    protected override string? ServiceTitle => "Mbyllje_Aktiviteti_11143";
    protected override ServiceStartMode StartMode => ServiceStartMode.Track;

    [Test]
    public void Mbyllje_Aktiviteti_11143()
    {


                Log("Assert detajet e subjektit");
                IWebElement DetajeteSubjektit = wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/h5"))
                );
                Assert.That(DetajeteSubjektit.Text.Trim(), Is.EqualTo("DETAJET E SUBJEKTIT"));

                IWebElement nipt = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("nipt")));
                Assert.That(InputValue(nipt), Is.EqualTo("L12121023B"));

                IWebElement EmriSubjektit = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("emriSubjektit")));
                Assert.That(InputValue(EmriSubjektit), Is.EqualTo("KREATX"));

                IWebElement DtRegjistrimit = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("dataRregjistrimit")));
                Assert.That(InputValue(DtRegjistrimit), Is.EqualTo("21.09.2011"));

                IWebElement StatusiSubjektit = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("statusi")));
                Assert.That(InputValue(StatusiSubjektit), Is.EqualTo("Aktiv"));

                IWebElement Administratori = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("perfaqesuesi")));
                Assert.That(InputValue(Administratori), Is.EqualTo("Enor  Vlash  Nakuçi"));

                Log("Click Vazhdo button - Step 1");
                IWebElement vazhdoBtn1 = wait.Until(
                    ExpectedConditions.ElementExists(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/button[2]"))
                );

                ((IJavaScriptExecutor)driver).ExecuteScript(
                    "arguments[0].scrollIntoView({ block: 'center' });",
                    vazhdoBtn1
                );

                Thread.Sleep(500);
                wait.Until(ExpectedConditions.ElementToBeClickable(vazhdoBtn1)).Click();

                Log("Assert Kontakti");
                IWebElement kontaktiTitle = wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/h4"))
                );
                Assert.That(kontaktiTitle.Text.Trim(), Is.EqualTo("KONTAKTI"));

                IWebElement email = wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.Name("email"))
                );
                Assert.That(InputValue(email), Is.EqualTo("ketjona.mema@kreatx.com"));

                IWebElement phoneNumber = wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.Name("mobile"))
                );
                Assert.That(InputValue(phoneNumber), Is.EqualTo("0676041404"));

                Thread.Sleep(500);
                Log("Click checkbox per mbylljen e aktivitetit - Step 2");

                IWebElement checkbox = wait.Until(
                    ExpectedConditions.ElementExists(By.Id("confirmClosure"))
                );
                ((IJavaScriptExecutor)driver).ExecuteScript(
    "arguments[0].scrollIntoView({ block: 'center' });",
    checkbox
);

                Thread.Sleep(500);

                if (!checkbox.Selected)
                {
                    try
                    {
                        wait.Until(ExpectedConditions.ElementToBeClickable(checkbox)).Click();
                    }
                    catch
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", checkbox);
                    }
                }




                //Log("Click 'Dergo' button");
                //IWebElement dergoFinalBtn = wait.Until(
                //    ExpectedConditions.ElementExists(
                //        By.XPath("//button[contains(normalize-space(),'Dërgo')]"))
                //);

                //((IJavaScriptExecutor)driver).ExecuteScript(
                //    "arguments[0].scrollIntoView({ block: 'center' });",
                //    dergoFinalBtn
                //);

                //Thread.Sleep(500);

                //try
                //{
                //    wait.Until(ExpectedConditions.ElementToBeClickable(
                //        By.XPath("//button[contains(normalize-space(),'Dërgo')]"))).Click();
                //}
                //catch (ElementClickInterceptedException)
                //{
                //    dergoFinalBtn = driver.FindElement(By.XPath("//button[contains(normalize-space(),'Dërgo')]"));
                //    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", dergoFinalBtn);
                //}

                //Thread.Sleep(1500);

                //var kujdesPopups = driver.FindElements(By.CssSelector(".alert-modal-container"));

                //if (kujdesPopups.Count > 0 && kujdesPopups[0].Displayed)
                //{
                //    Log("Popup 'Kujdes' u shfaq");

                //    IWebElement kujdesTitle = wait.Until(
                //        ExpectedConditions.ElementIsVisible(By.CssSelector(".alert-modal-title"))
                //    );
                //    Assert.That(kujdesTitle.Text.Trim(), Is.EqualTo("Kujdes!"));

                //    IWebElement kujdesDescription = wait.Until(
                //        ExpectedConditions.ElementIsVisible(By.CssSelector(".alert-modal-description"))
                //    );
                //    Assert.That(
                //        kujdesDescription.Text.Trim(),
                //        Is.EqualTo("Ju keni nje aplikim ne proces per kete lloj license!")
                //    );

                //    IWebElement okBtn = wait.Until(
                //        ExpectedConditions.ElementToBeClickable(By.CssSelector(".alert-modal-button--primary"))
                //    );
                //    okBtn.Click();

                //    Log("Testi perfundon me klikimin e popup 'Kujdes!'");
                //    return;
                //}

                //Log("Assert success page");
                //IWebElement successTitle = wait.Until(
                //    ExpectedConditions.ElementIsVisible(
                //        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[1]/h5"))
                //);
                //Assert.That(successTitle.Displayed, Is.True);
                //Assert.That(successTitle.Text.Trim(), Is.EqualTo("APLIKIMI JUAJ U KRYE ME SUKSES."));

                //IWebElement referenceNumber = wait.Until(
                //    ExpectedConditions.ElementIsVisible(
                //        By.XPath("//h6[contains(.,'Numri referencë i aplikimit është')]//b"))
                //);
                //Assert.That(referenceNumber.Displayed, Is.True);
                //Assert.That(referenceNumber.Text.Trim(), Is.Not.Empty);

                //Log("Assert that in success page is GjurmoBtn, PaguajOnlline, ShkarkoMandatin");
                //IWebElement gjurmoBtn = wait.Until(
                //    ExpectedConditions.ElementIsVisible(
                //        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[1]/div/button"))
                //);
                //Assert.That(gjurmoBtn.Displayed, Is.True);

                //IWebElement PayOnline = wait.Until(
                //    ExpectedConditions.ElementIsVisible(
                //        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div/div/div/button[1]"))
                //);
                //Assert.That(PayOnline.Displayed, Is.True);

                //IWebElement ShkarkoMandatin = wait.Until(
                //    ExpectedConditions.ElementIsVisible(
                //        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[2]/div/div/div/button[2]"))
                //);
                //Assert.That(ShkarkoMandatin.Displayed, Is.True);

                //Log("Click 'gjurmoBtn' button");
                //((IJavaScriptExecutor)driver).ExecuteScript(
                //    "arguments[0].scrollIntoView({block:'center'});",
                //    gjurmoBtn
                //);

                //Thread.Sleep(500);

                //try
                //{
                //    wait.Until(ExpectedConditions.ElementToBeClickable(
                //        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[1]/div/button"))).Click();
                //}
                //catch (ElementClickInterceptedException)
                //{
                //    gjurmoBtn = driver.FindElement(
                //        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[1]/div/button"));
                //    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", gjurmoBtn);
                //}

                Log("TEST PASSED");
    }

}