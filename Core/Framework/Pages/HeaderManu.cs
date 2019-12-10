using Laren.E2ETests.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Pages
{
    public class HeaderManu : PageObject
    {
        public HeaderManu(IWebDriver driver) : base(driver) { }

        public IWebElement AddCircleButton => _driver.FindElement(By.XPath("//clr-icon[@shape='plus-circle']"));
        public By UserMenuButtonLocator => By.XPath("//button[@class='userMenuBtn dropdown-toggle']");
        public By SignOutButtonLocator => By.XPath("//button[@class='signoutLink dropdown-item']");
        public SelectTypeOfRecordAlert ClickAddCircleButton()
        {
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("toast-container")));
            AddCircleButton.Click();
            return new SelectTypeOfRecordAlert(_driver);
        }
        public void SignOut()
        {
            UserMenuButtonLocator.WaitAndClick(_driver);
            SignOutButtonLocator.WaitAndClickUsingActions(_driver);
        }
    }
}

