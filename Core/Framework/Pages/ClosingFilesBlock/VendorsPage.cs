using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock
{
    public class VendorsPage : PageObject
    {
        public VendorsPage(IWebDriver driver) : base(driver) { }

        public IWebElement AddVendorButton => _driver.FindElement(By.XPath("//*[@class='primary add-laren']"));
        public IWebElement SelectVendorTypeDropDown => _driver.FindElement(By.XPath("//input[@placeholder='Select Vendor Type']"));
        public IWebElement LenderType => _driver.FindElement(By.XPath("//*[contains(text(),'Lender')]"));
        public IWebElement VendorNameDropdown => _driver.FindElement(By.XPath("//*[@placeholder='Select Vendor Name']"));
        public IWebElement FirstVendorNameInList => _driver.FindElement(By.XPath("//a[@class='dropdown-item']"));
        public IWebElement UnderwriterType => _driver.FindElement(By.XPath("//*[contains(text(),'Underwriter')]"));

        public VendorsPage ClickAddVendorButton()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@class='primary add-laren']")));
            AddVendorButton.Click();
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            return this;
        }

        public VendorsPage ClickSelectVendorTypeDropDown()
        {
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            SelectVendorTypeDropDown.Click();
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            return this;
        }
        public VendorsPage ClickLenderType()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(),'Lender')]")));
            LenderType.Click();
            return this;
        }
        public VendorsPage ClickUnderwriterType()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(),'Underwriter')]")));
            UnderwriterType.Click();
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            return this;
        }
        public VendorsPage ClickVendorNameDropdown()
        {
            VendorNameDropdown.Click();
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            return this;
        }

        public VendorsPage ClickFirstVendorNameInList()
        {
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            try
            {
                Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@class='dropdown-item']")));
                FirstVendorNameInList.Click();
            }
            catch (NoSuchElementException)
            {
                Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@class='dropdown-item']")));
                FirstVendorNameInList.Click();
            }
            return this;
        }
       
    }
}
