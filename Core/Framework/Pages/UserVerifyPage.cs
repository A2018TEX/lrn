using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.Pages
{
    public class UserVerifyPage : PageObject
    {
        public IConfiguration _configuration;
        public UserVerifyPage(IWebDriver driver, IConfiguration configuration) : base(driver) { _configuration = configuration; }

        public IWebElement PasswordField => _driver.FindElement(By.Name("password"));
        public IWebElement ConfirmPassword => _driver.FindElement(By.Name("confirmPassword"));

        public IWebElement SignInButton => _driver.FindElement(By.XPath("//*[@class='clr-col-2 text-left mt3']//*[contains(text(), 'Sign In')]"));

        public UserVerifyPage GoToUserVerifyPage(Guid uniqueidentifier)
        {
            _driver.Navigate().GoToUrl($"{_configuration.GetSection("BaseUrl").Value}/registerverify" + "/" + uniqueidentifier);
            _driver.Manage().Window.Maximize();
            return this;
        }

        public UserVerifyPage TypePassword(string password)
        {
            PasswordField.SendKeys(password);
            return this;
        }
        public UserVerifyPage EnterConfirmPassword(string password)
        {
            ConfirmPassword.SendKeys(password);
            return this;
        }

        public UserVerifyPage ClickSignInButton()
        {
            SignInButton.Click();
            Thread.Sleep(1000);
            return this;
        }

       
    }
}
