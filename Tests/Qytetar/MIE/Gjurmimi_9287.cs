using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar.MIE;

[Category("MIE")]
[Category("9287")]
public class Gjurmimi_9287 : QytetarNidJ557TestBase
{
    protected override string ServiceCode => "9287";
    protected override string? ServiceTitle => "GjurmoAplikim9287";
    protected override ServiceStartMode StartMode => ServiceStartMode.Track;

    [Test]
    public void GjurmoAplikim9287()
    {

Log("Click 'Gjurmo' button");
                wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("/html/body/div/main/div[3]/div/div/div/div/div/div/div[2]/div/button"))).Click();

                Thread.Sleep(1000);
                Log("Assert Aplikimet për licencë");
                IWebElement AplikimetPerLicence = wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[1]/div/h5"))
                );
                Assert.That(AplikimetPerLicence.Text.Trim(), Is.EqualTo("APLIKIMET PËR LICENCË"));



                Log("Search Aplication Number");
                driver.FindElement(By.Id(":r0:")).SendKeys("10749");

                Log("Assert search result");
                IWebElement tipiaplikimit = wait.Until(
                    ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[3]/table/tbody/tr/td[2]"))
                );
                Assert.That(tipiaplikimit.Text.Trim(), Is.EqualTo("Individ"));

                IWebElement emri = wait.Until(
                    ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[3]/table/tbody/tr/td[3]"))
                );
                Assert.That(emri.Text.Trim(), Is.EqualTo("Licence individuale e shkalles se dyte"));

                IWebElement NrAplikimit = wait.Until(
                    ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[3]/table/tbody/tr/td[5]"))
                );
                Assert.That(NrAplikimit.Text.Trim(), Is.EqualTo("10749"));

                IWebElement StatusiAplikimit = wait.Until(
                    ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[3]/table/tbody/tr/td[6]"))
                );
                Assert.That(StatusiAplikimit.Text.Trim(), Is.EqualTo("Aplikim i ri\r\nKoment: Aplikimi i ri u dergua"));

                Log("Click 'Shkarko' button");
                driver.FindElement(By.XPath("/html/body/div/main/div[3]/div/div/div/div/div[3]/table/tbody/tr/td[7]/div/button")).Click();


                Log("TEST PASSED");
    }

}