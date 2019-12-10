using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock
{
    public class MoneyPage : PageObject
    {
        public MoneyPage(IWebDriver driver) : base(driver) { }

        public IWebElement MoneySideMenuItem => _driver.FindElement(By.XPath("//div[@class='col-sm-12']/span[contains(text(),'Money')]"));
        public IWebElement PurchasePrice =>_driver.FindElement(By.Id("Purchase Price"));
        public IWebElement RawInput => _driver.FindElement(By.Id("rawInput"));
        public IWebElement EscrowDeposit => _driver.FindElement(By.Id("Escrow Deposit"));
        public IWebElement LoanAmount => _driver.FindElement(By.Id("Loan Amount"));
        public IWebElement EscrowAccount => _driver.FindElement(By.XPath("//select[@name='parent']"));
        public MoneyPage ClickMoneySideMenuItem()
        {
            MoneySideMenuItem.Click();
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            return this;
        }
        public MoneyPage CleanAndEnterPurchasePrice(decimal price)        
        {
            PurchasePrice.Click();
            Actions act = new Actions(_driver);
            act.MoveToElement(PurchasePrice);
            PurchasePrice.SendKeys(Keys.Backspace);
            PurchasePrice.SendKeys(Keys.Backspace);
            PurchasePrice.SendKeys(Keys.Backspace);
            PurchasePrice.SendKeys(Keys.Backspace);
            PurchasePrice.SendKeys(Keys.Backspace);
            PurchasePrice.SendKeys(Keys.Backspace);
            act.SendKeys(price.ToString());
            act.Perform();
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            return this;
        }
        public MoneyPage ClearAndEnterEscrowDeposit(decimal deposit)
        {
            EscrowDeposit.Click();
            Actions act = new Actions(_driver);
            act.MoveToElement(EscrowDeposit);
            EscrowDeposit.SendKeys(Keys.Backspace);
            EscrowDeposit.SendKeys(Keys.Backspace);
            EscrowDeposit.SendKeys(Keys.Backspace);
            EscrowDeposit.SendKeys(Keys.Backspace);
            EscrowDeposit.SendKeys(Keys.Backspace);
            EscrowDeposit.SendKeys(Keys.Backspace);
            act.SendKeys(deposit.ToString());
            act.Perform();
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            return this;
        }

        public MoneyPage ClearAndEnterLoanAmount(string amount)
        {
            LoanAmount.Click();
            Actions act = new Actions(_driver);
            act.MoveToElement(LoanAmount);
            LoanAmount.SendKeys(Keys.Backspace);
            LoanAmount.SendKeys(Keys.Backspace);
            LoanAmount.SendKeys(Keys.Backspace);
            LoanAmount.SendKeys(Keys.Backspace);
            LoanAmount.SendKeys(Keys.Backspace);
            LoanAmount.SendKeys(Keys.Backspace);
            act.SendKeys(amount.ToString());
            act.Perform();
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            return this;
        }

        public MoneyPage ChooseEscrowAccount()
        {
            SelectElement sl = new SelectElement(EscrowAccount);
            sl.SelectByIndex(1);
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[contains(text(),'Saved')]")));
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            return this;
        }


    }
}
