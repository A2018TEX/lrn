using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laren.E2ETests.Core.Utils
{
    public class LocalStorageJS : PageObject
    {
        public LocalStorageJS(IWebDriver driver) : base(driver) { }

        public void ClearLocalStorage()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript(String.Format("window.localStorage.clear();"));
        }

        public void CleanSessionStorage()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript(String.Format("window.sessionStorage.clear();"));
        }
    }
}
