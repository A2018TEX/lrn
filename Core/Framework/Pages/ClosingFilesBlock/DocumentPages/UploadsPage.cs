using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock
{
    public class UploadsPage : PageObject
    {

        public UploadsPage(IWebDriver driver) : base(driver) { }
        public By DocumentLocator(string fileNameWithoutFormar) => By.XPath($"//span[contains(text(), '{fileNameWithoutFormar}')]");
        public By EllipsisVerticalLocation => By.XPath("//clr-icon[@shape='ellipsis-vertical']");
        public By RenameButtonLocator => By.XPath("//clr-dropdown-menu[@class='pa0 dropdown-menu ng-star-inserted']//button[contains(text(),'Rename')]");
        public By RenameFieldLocator => By.XPath("//div[@class='modal-content-wrapper']//input[@id='rename']");
        public By RenameButtonOnPopUpLocator => By.XPath("//div[@class='modal-content-wrapper']//button[contains(text(), 'Rename')]");
        public By FinaliseButtonLocator => By.XPath("//clr-dropdown-menu/button[contains(text(), 'Finalize')]");
        public By BuyerPacketCheckBoxLocator => By.XPath("//div[@class='modal-content-wrapper']//label[@for='0']//span");
        public By SellerPacketCheckBoxLocator => By.XPath("//div[@class='modal-content-wrapper']//label[@for='1']//span");
        public By LenderPacketCheckBoxLocator => By.XPath("//div[@class='modal-content-wrapper']//label[@for='2']//span");
        private By PopUpLocator => By.XPath("//div[@class='modal-content']");
        public By FinaliseButtonOnPopUp => By.XPath("//div[@class='modal-content-wrapper']//button[contains(text(), 'Finalize ')]");
        public By ConfirmatiomMessageLocator => By.XPath("//div[contains(text(), 'Package(s) has been added to final')]");
        public By ArchiveButtonLocator => By.XPath("//clr-dropdown-menu/button[contains(text(), 'Archive')]");
        public By UnArchiveButtonLocator => By.XPath("//clr-dropdown-menu/button[contains(text(), 'Un-Archive')]");

        private By documentInArchiveSectionLocator(string fileName) => By.XPath($"//label[contains(text(), 'Archive')]/ancestor::div[@class='list-wrapper-laren text-left ng-star-inserted']//span[contains(text(), '{fileName}')]");
        public By DeleteButtonLocator => By.XPath("//clr-dropdown-menu/button[contains(text(), 'Delete')]");
        public By OkButtonOnDeleteItemPopupLocator => By.XPath("//div[@class='modal-dialog ng-trigger ng-trigger-fadeDown modal-lg']//button[contains(text(), 'Ok')]");


        public UploadsPage UploadFile(string fileName)
        {
            string docxFile = $@"{AppDomain.CurrentDomain.BaseDirectory}{fileName}";
            _driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(docxFile);
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(text(), 'Files uploaded Successfully')]")));
            return this;
        }

        public bool FindUploadedFile(string fileNameWithoutFormar)
        {
            try
            {
                Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//div[@class='col-sm-12 list-box-laren ng-star-inserted']//span[contains(text(), '{fileNameWithoutFormar}')]")));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public UploadsPage ClickOnEllipsisVertical(string fileNameWithoutFormar)
        {

            Thread.Sleep(2000);
            var actions = new Actions(_driver);
            var document = _driver.FindElement(DocumentLocator(fileNameWithoutFormar));
            DocumentLocator(fileNameWithoutFormar).WaitElementIsVisible(_driver);
            actions.MoveToElement(document).Perform();
            EllipsisVerticalLocation.WaitElementIsVisible(_driver);
            EllipsisVerticalLocation.WaitAndClick(_driver);
            return this;
        }

        public UploadsPage ClickOnRenameButton()
        {
            RenameButtonLocator.WaitAndClickUsingActions(_driver);
            return this;
        }

        public UploadsPage TypeNewFileName(string fileNameAfterRemaming)
        {
            var renameField = _driver.FindElement(RenameFieldLocator);
            renameField.Clear();
            renameField.SendKeys(fileNameAfterRemaming);
            return this;
        }

        public UploadsPage ClickOnRenameButtonOnPopup()
        {
            RenameButtonOnPopUpLocator.WaitAndClickUsingIJavaScriptExecutor(_driver);
            return this;
        }

        public UploadsPage ClickOnFinaliseButtonOnDropdown()
        {
            FinaliseButtonLocator.WaitAndClickUsingActions(_driver);
            return this;
        }
        public UploadsPage ClickOnBuyerPacketCheckBox()
        {
            PopUpLocator.WaitElementIsVisible(_driver);
            BuyerPacketCheckBoxLocator.WaitAndClickUsingIJavaScriptExecutor(_driver);
            return this;
        }
        public UploadsPage ClickOnSellerPacketCheckBox()
        {
            PopUpLocator.WaitElementIsVisible(_driver);

            SellerPacketCheckBoxLocator.WaitAndClickUsingIJavaScriptExecutor(_driver);
            return this;
        }
        public UploadsPage ClickOnLenderPacketCheckBox()
        {

            PopUpLocator.WaitElementIsVisible(_driver);
            LenderPacketCheckBoxLocator.WaitAndClickUsingIJavaScriptExecutor(_driver);
            return this;
        }
        public UploadsPage ClickOnFinaliseButtonOnPopUp()
        {
            PopUpLocator.WaitElementIsVisible(_driver);
            FinaliseButtonOnPopUp.WaitAndClickUsingIJavaScriptExecutor(_driver);
            WaitConfirmatiomMessage();
            return this;
        }
        public UploadsPage WaitConfirmatiomMessage()
        {
            ConfirmatiomMessageLocator.WaitElementIsVisible(_driver);
            return this;
        }

        public UploadsPage ClickOnArchiveButton()
        {
            ArchiveButtonLocator.WaitAndClickUsingActions(_driver);
            return this;
        }

        public bool CheckThatDocumentIsPresentOnArchiveSection(string fileName)
        {
            try
            {
                Wait.Until(ExpectedConditions.ElementIsVisible(documentInArchiveSectionLocator(fileName)));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public UploadsPage ClickOnUnArchiveButton()
        {
            UnArchiveButtonLocator.WaitAndClick(_driver);
            return this;
        }
        public UploadsPage ClickOnDeleteButton()
        {
            DeleteButtonLocator.WaitAndClickUsingActions(_driver);
            return this;
        }
        public UploadsPage ClickOnOkButtonOnDeleteItemPopup()
        {
            OkButtonOnDeleteItemPopupLocator.WaitAndClickUsingActions(_driver);
            return this;
        }
        public UploadsPage WainUntilDocumentIsDisappearedFromArchiveSection(string fileName)
        {
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(documentInArchiveSectionLocator(fileName)));
            return this;
        }
    }
}
