using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock
{
    public class ImportantDatesPage : PageObject
    {
        public ImportantDatesPage(IWebDriver driver) : base(driver) { }
        public IWebElement ImportantDatesSideMenuItem => _driver.FindElement(By.XPath("//div[@class='col-sm-12']/span[contains(text(),'Important Dates')]"));
        public IWebElement EffectiveDate => _driver.FindElement(By.XPath("//label[@for='effectiveDate']/../div[@class='position-relative']"));
        public IWebElement CllosingDate => _driver.FindElement(By.XPath("//label[@for='closingDate']/../div[@class='position-relative']"));
        public IWebElement FindingDate => _driver.FindElement(By.XPath("//label[@for='fundingDate']/../div[@class='position-relative']"));
        public IWebElement BuyerSigningDate => _driver.FindElement(By.XPath("//label[@for='buyerSigningDate']/../div[@class='position-relative']"));
        public IWebElement SellerSigningDate => _driver.FindElement(By.XPath("//label[@for='sellerSigningDate']/../div[@class='position-relative']"));
        public IWebElement ClosingDateModalWindow => _driver.FindElement(By.XPath("//h3[@class='modal-title']"));
        public IWebElement OKButtoneModalWindow => _driver.FindElement(By.XPath("//button[contains(text(),'Ok')]"));
        public IWebElement PreviousMonthButton => _driver.FindElement(By.XPath("//button[@aria-label='Previous month']"));
        public IWebElement NextMonthButton => _driver.FindElement(By.XPath("//button[@aria-label='Next month']"));

        public ImportantDatesPage ClickImportantDatesSideMenuItem()
        {
            ImportantDatesSideMenuItem.Click();
            return this;
        }

        public ImportantDatesPage SetEffectiveDate(string date)
        {
            EffectiveDate.Click();
            if (IsDateElementDisplayed(date))
            {
                IWebElement el = _driver.FindElement(By.XPath($"//div[@aria-label='{date}']"));
                el.Click();
            }
            return this;
        }
        private bool PopUpDisplayed()
        {
            bool isDisplayed = false;
            try
            {
                IWebElement el = _driver.FindElement(By.XPath("//div[@class='modal-content']"));
                isDisplayed = el.Displayed;
                return isDisplayed;
            }
            catch
            {
                return isDisplayed;
            }
        }
        public ImportantDatesPage ClickOkOnPopupIfDisplayed()
        {
            if (PopUpDisplayed())
            {
                var ok = _driver.FindElement(By.XPath("//div[@class='modal-content']//button[@type='button']/../*[contains(text(), 'Ok')]"));
                ok.Click();
            }
            return this;
        }

        public ImportantDatesPage SetFundingDate(string date)
        {
            FindingDate.Click(); if (IsDateElementDisplayed(date))
            {
                IWebElement el = _driver.FindElement(By.XPath($"//div[@aria-label='{date}']"));
                el.Click();
            }

            return this;
        }

        public ImportantDatesPage SetBuyerSigningDate(string date)
        {
        //    ClickOkButtonClosingDateModalWindow();
            BuyerSigningDate.Click();
            if (IsDateElementDisplayed(date))
            {
                IWebElement el = _driver.FindElement(By.XPath($"//div[@aria-label='{date}']"));
                el.Click();
            }
            if (!IsDateElementDisplayed(date))
            {
                PreviousMonthButton.Click();
                if (IsDateElementDisplayed(date))
                {
                    IWebElement el = _driver.FindElement(By.XPath($"//div[@aria-label='{date}']"));
                    el.Click();
                }

            }
            else
            {
                if (!IsDateElementDisplayed(date))
                {
                    NextMonthButton.Click();
                    NextMonthButton.Click();
                    if (IsDateElementDisplayed(date))
                    {
                        IWebElement el = _driver.FindElement(By.XPath($"//div[@aria-label='{date}']"));
                        el.Click();
                    }

                }
            }

                return this;           
        }

        public bool IsDateElementDisplayed(string date)
        {
            bool isDisplayed = false;
            try
            {
                IWebElement el = _driver.FindElement(By.XPath($"//div[@aria-label='{date}']"));
                isDisplayed = el.Displayed;
                return isDisplayed;
            }
            catch
            {
                return isDisplayed;
            }
        }


        public ImportantDatesPage SetCllosingDate(string date)
        {
            CllosingDate.Click(); 
            if (IsDateElementDisplayed(date))
            {
                IWebElement el = _driver.FindElement(By.XPath($"//div[@aria-label='{date}']"));
                el.Click();
            }
            return this;
        }
        public ImportantDatesPage SetSellerSignInDate(string date)
        {
            SellerSigningDate.Click(); if (IsDateElementDisplayed(date))
            {
                IWebElement el = _driver.FindElement(By.XPath($"//div[@aria-label='{date}']"));
                el.Click();
            }

            return this;
        }
        
        public ImportantDatesPage ClickOkButtonClosingDateModalWindow()
        {
            if (IsElementDisplayed())
            {
                OKButtoneModalWindow.Click();
            }
            
                return this;
            
        }
        public bool IsElementDisplayed()
        {
            bool isDisplayed = false;
            try
            {
                IWebElement el = _driver.FindElement(By.XPath("//h3[@class='modal-title']")); 
                isDisplayed = el.Displayed;
                return isDisplayed;
            }
            catch
            {
                return isDisplayed;
            }
        }
    }
}
