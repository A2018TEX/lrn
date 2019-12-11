using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.Pages
{
    public class PrintPage : PageObject
    {
        public PrintPage(IWebDriver driver) : base(driver) { }
  
        public bool PrintPageIsOpened()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            wait.Until((d) => _driver.WindowHandles.Count == 2);
            var browserTabs = _driver.WindowHandles;
            _driver.SwitchTo().Window(browserTabs[1]);
            var printPageIsOpened = _driver.Url.Contains("blob");
            _driver.Close();
            _driver.SwitchTo().Window(browserTabs[0]);
            return printPageIsOpened;
        }
        
    }
}
