using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock
{ 
    public class BuyerPage : PageObject
    {
        public BuyerPage(IWebDriver driver) : base(driver) { }
        public IWebElement AddBuyerButton => _driver.FindElement(By.XPath("//clr-icon[@class='primary add-laren']"));
        public IWebElement BuyerType => _driver.FindElement(By.Name("currentBuyerSeller.buyerTypeId"));
        public IWebElement FirstName => _driver.FindElement(By.Name("firstname"));
        public IWebElement Gender => _driver.FindElement(By.Name("currentBuyerSeller.gender"));
        public IWebElement MaritalStatus => _driver.FindElement(By.Name("currentBuyerSeller.maritalStatus"));
        public IWebElement Tanancy => _driver.FindElement(By.Name("currentBuyerSeller.vestingType"));
        public IWebElement LastName => _driver.FindElement(By.Name("lastname"));
        public IWebElement Address => _driver.FindElement(By.XPath("//li[@class='ng-star-inserted']/a[contains(text(),'Addresses')]"));
        public IWebElement SameAsDropDown => _driver.FindElement(By.Name("addresses"));
        public IWebElement Address1Buyer => _driver.FindElement(By.Id("addressValidation"));

        public BuyerPage ClickAddBuyerButton()
        {
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            AddBuyerButton.Click();
            return this;
        }
        public BuyerPage SelectBuyerType()
        {
            SelectElement sl = new SelectElement(BuyerType);
            sl.SelectByText("Individual");
            return this;
        }
        public BuyerPage TypeFirstName(string FName)
        {
            FirstName.SendKeys(FName);
            return this;
        }
        public BuyerPage TypeLastName(string LName)
        {
            LastName.SendKeys(LName);
            return this;
        }
        public BuyerPage SelectGender()
        {
            SelectElement sl = new SelectElement(Gender);
            sl.SelectByIndex(1);
            return this;
        }
        public BuyerPage SelectMaritalStatus()
        {
            SelectElement sl = new SelectElement(MaritalStatus);
            sl.SelectByIndex(1);
            return this;
        }
        public BuyerPage SelectTenancy()
        {
            SelectElement sl = new SelectElement(Tanancy);
            sl.SelectByIndex(1);
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            return this;
        }

        public BuyerPage ClickAddressMenuItem()
        {
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            Address.Click();
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            return this;
        }
        public BuyerPage SelectSameAsDropDown()
        {
            SelectElement sl = new SelectElement(SameAsDropDown);
            sl.SelectByIndex(1);
            return this;
        }
        public BuyerPage TypeAddress1Field(string address)
        {
            Address1Buyer.Clear();
            Address1Buyer.SendKeys(address);
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            var adress = By.XPath("//span[contains(text(),'Portland')]/ancestor::div[@class='pac-item']");
            ClickOnAddress(adress);
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            return this;
        }

        private void ClickOnAddress(By adress)
        {
            try
            {
                adress.WaitAndClickUsingActions(_driver);
            }
            catch
            {
                By.XPath("//input[@id='addressValidation']").WaitAndClickUsingActions(_driver);//
                adress.WaitAndClickUsingActions(_driver);
            }
        }
    }
}
