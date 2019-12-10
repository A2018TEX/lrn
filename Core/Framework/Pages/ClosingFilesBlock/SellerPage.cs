using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock
{
    public class SellerPage
    {
        public class SellersPage : PageObject
        {
            public SellersPage(IWebDriver driver) : base(driver) { }
            public IWebElement AddSellersButton => _driver.FindElement(By.XPath("//clr-icon[@class='primary add-laren']"));
            public IWebElement SellerType => _driver.FindElement(By.Name("currentBuyerSeller.buyerTypeId"));
            public IWebElement ExemptField => _driver.FindElement(By.Name("is1099sExempt"));
            public IWebElement Gender => _driver.FindElement(By.Name("currentBuyerSeller.gender"));
            public IWebElement MaritalStatus => _driver.FindElement(By.Name("currentBuyerSeller.maritalStatus"));
            public IWebElement Email => _driver.FindElement(By.Name("emailaddress"));
            public IWebElement AddressItem => _driver.FindElement(By.XPath("//a[contains(text(),'Addresses')]"));
            public IWebElement FirstName => _driver.FindElement(By.Name("firstname"));
            public IWebElement LastName => _driver.FindElement(By.Name("lastname"));

            public SellersPage ClickAddSellersButton()
            {
                Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
                Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//clr-icon[@class='primary add-laren']")));
                AddSellersButton.Click();
                return this;
            }
            public SellersPage SelectSellerType()
            {
                SelectElement sl = new SelectElement(SellerType);
                sl.SelectByText("Individual");
                return this;
            }

            public SellersPage TypeEmail(string email)
            {
                Email.SendKeys(email);
                return this;
            }
            public SellersPage SelectGender()
            {
                SelectElement sl = new SelectElement(Gender);
                sl.SelectByIndex(1);
                return this;
            }

            public SellersPage SelectExemptField()
            {
                Thread.Sleep(100);
                SelectElement sl = new SelectElement(ExemptField);
                sl.SelectByIndex(2);
                Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='spinner spinner-sm']")));
                Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='spinner spinner-sm']")));
                return this;
            }
            public SellersPage SelectMaritalStatus()
            {
                SelectElement sl = new SelectElement(MaritalStatus);
                sl.SelectByIndex(1);
                Thread.Sleep(100);
                return this;
            }

            public SellersPage ClickAddressMenuItem()
            {
                AddressItem.Click();
                return this;
            }
            public SellersPage TypeFirstName(string FName)
            {
                FirstName.SendKeys(FName);
                return this;
            }
            public SellersPage TypeLastName(string LName)
            {
                LastName.SendKeys(LName);
                return this;
            }
        }
    }
}
