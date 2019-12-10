using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Laren.E2ETests.Core.Pages
{
    public class LoginPage : PageObject
    {
        private IConfiguration _configuration;
        public LoginPage(IWebDriver driver, IConfiguration configuration) : base(driver) { _configuration = configuration; }
        public IWebElement LoginField => _driver.FindElement(By.Id("login_email"));
        public IWebElement PasswordField => _driver.FindElement(By.Id("login_password"));
        public IWebElement SignInButton => _driver.FindElement(By.XPath("//button[@type='submit']"));
        public IWebElement WelcomeToastAlert => _driver.FindElement(By.Id("toast-container"));
        

        public LoginPage GoToLoginPage()
        {
            _driver.Navigate().GoToUrl($"{_configuration.GetSection("BaseUrl").Value}/login");
            return this;
        }
        public LoginPage TypeLogin(string login)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("login_email")));
            LoginField.SendKeys(login);
            return this;
        }
        public LoginPage TypePassword(string password)
        {
            PasswordField.SendKeys(password);
            return this;
        }

        public LoginPage ClickSignIn()
        {
            SignInButton.Click();
            return this;
        }

        public string GetWelcomeText()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("toast-container")));
            string welcometext = WelcomeToastAlert.Text;
            return welcometext;
        }
        public LoginPage SuccessLogin(string login, string password)
        {
            GoToLoginPage();
            TypeLogin(login);
            TypePassword(password);
            ClickSignIn();
            //Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@routerlink='workflow']")));
            return this;
        }
    }
}
