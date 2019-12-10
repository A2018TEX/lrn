using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock
{
    public class LibraryPage : PageObject
    {
        public LibraryPage(IWebDriver driver) : base(driver) { }
        public IWebElement DocxFile => _driver.FindElement(By.XPath("//div[contains(text(), 'DocxFileForAutoTest.docx')]"));
        public By DocumentMenuButtonLocator(string documentName) => By.XPath($"//div[contains(text(), '{documentName}')]/../div[@class='line']");
        public IWebElement UseButtonOnPopUp => _driver.FindElement(By.XPath("//button[@class='btn btn-laren']/../button[contains(text(), 'Use')]"));
        public By UseButtoInDocumentMenunLocator => By.XPath("//button[@class='mat-menu-item'and contains(text(), 'Use')]");
        public By DocxFileLocator(string fileName) => By.XPath($"//div[contains(text(), '{fileName}')]");
        public By UseButtonLocator => By.XPath("//div[@class='doc-expander-wrapper']//input[@value='Use']");
        public By RenameFieldOnPopUpLocator => By.XPath("//input[@id='renameFile']");

        //public By UseButtonOnPopUpLocator => By.XPath("//button[@class='btn btn-laren']/../button[contains(text(), 'Use')]");
        public By MessageLocator => By.XPath("//div[contains(text(), 'Closing file document has been saved to Drafts')]");

        public LibraryPage ClickOnDocxFile(string fileName)
        {
            //Thread.Sleep(10000);
            DocxFileLocator(fileName).WaitAndClick(_driver);
            return this;
        }

        public LibraryPage ClickOnUseButtonOnDocumentMenu(string documentName)
        {
            Thread.Sleep(10000);
            DocumentMenuButtonLocator(documentName).WaitAndClickUsingActions(_driver);
            UseButtoInDocumentMenunLocator.WaitAndClickUsingActions(_driver);
            return this;
        }
        public LibraryPage ClickOnUseButtonOnPopUp()
        {
            Thread.Sleep(2000);
            var actions = new Actions(_driver);
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@class='btn btn-laren']/../button[contains(text(), 'Use')]")));
            IJavaScriptExecutor ex = (IJavaScriptExecutor)_driver;
            ex.ExecuteScript("arguments[0].click();", UseButtonOnPopUp);
            //WaitMessage();
            Thread.Sleep(3000);
            return this;
        }
        public LibraryPage WaitMessage()
        {
            MessageLocator.WaitElementIsVisible(_driver);
            return this;
        }
        public LibraryPage WaitMessage2()
        {
            MessageLocator.WaitElementIsVisible(_driver);
            return this;
        }

        public LibraryPage RenameFile(string fileNameForRenaming)
        {
            var RenameField = _driver.FindElement(RenameFieldOnPopUpLocator);
            RenameField.Clear();
            RenameField.SendKeys(fileNameForRenaming);
            Thread.Sleep(2000);
            return this;
        }
    }
}
