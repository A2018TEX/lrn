using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laren.E2ETests.Core.Pages
{
    public class SelectTypeOfRecordAlert : PageObject
    {
        public SelectTypeOfRecordAlert(IWebDriver driver) : base(driver) { }

        public IWebElement ClosingButton => _driver.FindElement(By.XPath("//button[contains(text(),'Closing File')]"));
        public IWebElement FilenumberField => _driver.FindElement(By.Name("filenumber"));
        public IWebElement TransactionTypeDropDown => _driver.FindElement(By.Name("transactionTypeId"));
        public IWebElement UsageTypeIdDropDown => _driver.FindElement(By.Name("usageTypeId"));
        public IWebElement FinanceTypeIddDropDown => _driver.FindElement(By.Name("financeTypeId"));

        public IWebElement PropertyTypeIdDropDown => _driver.FindElement(By.Name("propertyTypeId"));
        public IWebElement NextButton => _driver.FindElement(By.XPath("//button[contains(text(),'Next')]"));
        public IWebElement FileCreatedAlert => _driver.FindElement(By.XPath("//div[@role='alertdialog']"));

        public SelectTypeOfRecordAlert ClickClosingButton()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(),'Closing File')]")));
            ClosingButton.Click();
            return this;
        }
        public SelectTypeOfRecordAlert TypeFilenumberField(string fileName)
        {
            FilenumberField.SendKeys(fileName);
            return this;
        }
        public SelectTypeOfRecordAlert ChooseTransactionTypeValue()
        {

            int i = 2;
            var transactionType = new SelectElement(TransactionTypeDropDown);
            transactionType.SelectByIndex(i);
            return this;
        }
        public SelectTypeOfRecordAlert ChooseUsageTypeValue()
        {
            int i = 2;
            var usageType = new SelectElement(UsageTypeIdDropDown);
            usageType.SelectByIndex(i);
            return this;
        }
        public SelectTypeOfRecordAlert ChoosePropertyTypeValue()
        {
            int i = 2;
            var propertyType = new SelectElement(PropertyTypeIdDropDown);
            propertyType.SelectByIndex(i);
            return this;
        }
        public SelectTypeOfRecordAlert ClickNextButton()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(),'Next')]")));
            NextButton.Click();
            return this;
        }
        public string CreatedFileNumber()
        {            
             Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@role='alertdialog']")));
             string fileCreate = FileCreatedAlert.Text;
             return fileCreate;
        }

        public SelectTypeOfRecordAlert ChooseFinanceTypeValue()
        {
            int i = 2;
            var propertyType = new SelectElement(FinanceTypeIddDropDown);
            propertyType.SelectByIndex(i);
            return this;
        }
    }
}
