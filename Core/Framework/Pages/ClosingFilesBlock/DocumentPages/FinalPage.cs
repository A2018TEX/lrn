using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock
{
    public class FinalPage : PageObject
    {
        public FinalPage(IWebDriver driver) : base(driver) { }

        public By FileInBuyerSectionLocator(string fileName) => By.XPath($"//div[@class='list-wrapper-laren text-left ng-star-inserted'][1]//span[contains(text(), '{fileName}')]");
        public By FileInSelerSectionLocator(string fileName) => By.XPath($"//div[@class='list-wrapper-laren text-left ng-star-inserted'][2]//span[contains(text(), '{fileName}')]");
        public By FileInLenderSectionLocator(string fileName) => By.XPath($"//div[@class='list-wrapper-laren text-left ng-star-inserted'][3]//span[contains(text(), '{fileName}')]");
        public bool FindFileInBuyerSection(string fileName)
        {
            try
            {
                FileInBuyerSectionLocator(fileName).WaitElementIsVisible(_driver);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool FindFileInSelerSection(string fileName)
        {
            try
            {
                FileInSelerSectionLocator(fileName).WaitElementIsVisible(_driver);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool FindFileInLenderSection(string fileName)
        {
            try
            {
                FileInLenderSectionLocator(fileName).WaitElementIsVisible(_driver);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
