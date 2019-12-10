using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laren.E2ETests.Core
{
    public class PageObject
    {
        protected readonly IWebDriver _driver;

        private WebDriverWait _waiter;

        public PageObject(IWebDriver driver)
        {
            this._driver = driver;
        }
        protected WebDriverWait Wait => _waiter ?? (_waiter = new WebDriverWait(_driver, TimeSpan.FromSeconds(40)));
    }
}
