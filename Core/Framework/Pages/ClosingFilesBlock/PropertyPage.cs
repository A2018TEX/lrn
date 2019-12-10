using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock
{
    public class PropertyPage : PageObject
    {
        public PropertyPage(IWebDriver driver) : base(driver) { }
        public IWebElement PropertySideMenuItem => _driver.FindElement(By.XPath("//div[@class='col-sm-12']/span[contains(text(),'Property')]"));
        public IWebElement Address1Field => _driver.FindElement(By.Id("addressValidation"));
        public IWebElement LookupButton => _driver.FindElement(By.XPath("//a[contains(text(),'Lookup')]"));
        public IWebElement StateDropDown => _driver.FindElement(By.XPath("//div[@class='modal-body col-sm-12 pt0 pb0 text-center choose_listing pt3']//select[@name='state']"));
        public IWebElement ConfirmButton => _driver.FindElement(By.XPath("//button[contains(text(),'Confirm')]"));
        public PropertyPage ClickPropertySideMenuItem()
        {
            PropertySideMenuItem.Click();
            return this;
        }

        public PropertyPage TypeAddress1Field(string address)
        {
            Address1Field.Clear();
            Address1Field.SendKeys(address);
            var adress = By.XPath("//span[contains(text(),'Hillsboro')]/ancestor::div[@class='pac-item']");
            ClickOnAddress(adress);
            Thread.Sleep(1000);
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
        public PropertyPage ClickLookupButton()
        {
            LookupButton.Click();
            //Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='toast-bottom-right toast-container']")));
            Thread.Sleep(2000);
            return this;
        }

        public PropertyPage ChooseState(string address)
        {
            Thread.Sleep(1500);
            if (!IsElementDisplayed())
            {
                TypeAddress1Field(address);
            }
            if (IsElementDisplayed()) 
            {
                SelectElement sl = new SelectElement(StateDropDown);
                sl.SelectByIndex(1);
                Actions act = new Actions(_driver);
                act.SendKeys(Keys.Down + Keys.Enter);
                act.Perform();
            }
            return this;
        }

        public bool IsElementDisplayed()
        {
            bool isDisplayed = false;
            try
            {
                IWebElement el = _driver.FindElement(By.XPath("//div[@class='modal-body col-sm-12 pt0 pb0 text-center choose_listing pt3']//select[@name='state']"));
                isDisplayed = el.Displayed;
                return isDisplayed;
            }
            catch
            {
                return isDisplayed;
            }
        }
        public PropertyPage ClickConfirmButton()
        {
            ConfirmButton.Click();
            return this;
        }
    }
}
