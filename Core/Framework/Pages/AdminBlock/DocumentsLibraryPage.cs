using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock
{
    public class DocumentsLibraryPage : PageObject
    {
        public DocumentsLibraryPage(IWebDriver driver) : base(driver) { }
        private IWebElement ImportButton => _driver.FindElement(By.XPath("//button[@class='btn btn-laren btn-primary import pull-left']"));


        public DocumentsLibraryPage ClickonImportButton()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@class='btn btn-laren btn-primary import pull-left']")));
            ImportButton.Click();
            return this;
        }
        public DocumentsLibraryPage FileUpload(string docxFileName)
        {
            string docxFile = $@"{AppDomain.CurrentDomain.BaseDirectory}{docxFileName}";
            _driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(docxFile);
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(text(), 'Files uploaded Successfully')]")));
            //string pdfFile = AppDomain.CurrentDomain.BaseDirectory + "\\" + pdfFileName;
            //_driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(pdfFile);
            //Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(text(), 'Files uploaded Successfully')]")));
            return this;
        }
    }
}
