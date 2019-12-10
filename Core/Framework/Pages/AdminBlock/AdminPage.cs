using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock
{
    public class AdminPage : PageObject
    {
        private IConfiguration _configuration;
        public AdminPage(IWebDriver driver, IConfiguration configuration) : base(driver)
        {
            _configuration = configuration;
        }
        public IWebElement MenuDropDown => _driver.FindElement(By.XPath("//button[@class='userMenuBtn dropdown-toggle']//clr-icon[@shape='bars']"));
        public IWebElement DocumentsItemInDropDown => _driver.FindElement(By.Id("but-1"));

        public AdminPage ClickMenuDropDown()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='menuHost useDarkTheme ng-star-inserted']//button[@class='userMenuBtn dropdown-toggle']")));
            MenuDropDown.Click();
            return this;
        }
        public DocumentsLibraryPage GoToDocumentsLibraryPage()
        {
            GoToAdminPage();
            ClickMenuDropDown();
            DocumentsItemInDropDown.Click();
            return new DocumentsLibraryPage(_driver);
        }
        public AdminPage GoToAdminPage()
        {
            _driver.Navigate().GoToUrl($"{_configuration.GetSection("BaseUrl").Value}/admin/areas/people/quicklinks");
            return this;
        }

    }
}
