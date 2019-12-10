using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock
{
    public class SummaryPage : PageObject
    {
        public SummaryPage(IWebDriver driver) : base(driver) { }

        public IWebElement SummaryClosingDate => _driver.FindElement(By.XPath("//span[contains(text(),'Closing Date:')]"));
        public IWebElement GoogleLocation => _driver.FindElement(By.XPath("//div[@class='gm-iv-address-description']"));

        public string GetClosingDate()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(),'Closing Date:')]")));
            var date = SummaryClosingDate.Text;
            return date;
        }
        public string GetLocation()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='gm-iv-address-description']")));
            var location = GoogleLocation.Text;
            return location;
        }
    }
}
