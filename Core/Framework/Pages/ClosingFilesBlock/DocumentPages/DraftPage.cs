using Microsoft.SqlServer.Management.XEvent;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock
{
    public class DraftPage : PageObject
    {

        public DraftPage(IWebDriver driver) : base(driver) { }
        public By DocumentInActiveSectionLocator(string fileNameWithoutFormar) => By.XPath($"//span[contains(text(), '{fileNameWithoutFormar}')]");
        public By EllipsisVerticalLocation => By.XPath("//clr-icon[@shape='ellipsis-vertical']");
        public By EditItemLocator => By.XPath("//clr-dropdown-menu[@class='pa0 dropdown-menu ng-star-inserted']/button[contains(text(), 'Edit')]");
        public By FinaliseOnDocExpanderWrapperLocator => By.XPath("//div[@class='doc-expander-wrapper']//input[@value='Finalize']");
        public By BuyerPacketCheckBoxLocator => By.XPath("//div[@class='modal-content-wrapper']//label[@for='0']//span");
        public By SellerPacketCheckBoxLocator => By.XPath("//div[@class='modal-content-wrapper']//label[@for='1']//span");
        public By LenderPacketCheckBoxLocator => By.XPath("//div[@class='modal-content-wrapper']//label[@for='2']//span");
        public By FinaliseButtonOnPopUp => By.XPath("//div[@class='modal-content-wrapper']//button[contains(text(), 'Finalize ')]");
        public By ConfirmatiomMessageLocator => By.XPath("//div[contains(text(), 'Package(s) has been added to final')]");
        private By PopUpLocator => By.XPath("//div[@class='modal-content']");
        public By TextEditorLocator => By.Id("txTemplateDesignerContainer");
        public By CloneNameFieldLocator => By.Id("cloneName");
        public By PrintButtonOnActionRowLocator => By.XPath("//input[@value='Print']");
        public By ExportButtonOnActionRowLocator => By.XPath("//input[@value='Export']");
        public By CloneButtonLocator => By.XPath("//div[@class='col-sm-6 text-right']//input[@value='Clone']");

        public IWebElement File(string fileName) => _driver.FindElement(By.XPath($"//div[@class='col-sm-12 list-box-laren ng-star-inserted']//span[contains(Text(), '{fileName}')]"));
        public By CloneButtonOnPopupLocator => By.XPath("//div[@class='modal-content-wrapper']//button[contains(text(), 'Clone')]");


        public bool FindUploadedFile(string fileName)
        {
            try
            {
                Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//div[@class='col-sm-12 list-box-laren ng-star-inserted']//span[contains(text(), '{fileName}')]")));
                return true;
            }
            catch
            {
                return false;
            }
        }
        public DraftPage ClicOnEllipsisVertical(string fileNameWithoutFormar)
        {
            Thread.Sleep(1000);
            var actions = new Actions(_driver);
            var document = Wait.Until(ExpectedConditions.ElementToBeClickable(DocumentInActiveSectionLocator(fileNameWithoutFormar)));
            //DocumentInActiveSectionLocator(fileNameWithoutFormar).WaitElementIsVisible(_driver);
            actions.MoveToElement(document).Perform();
            EllipsisVerticalLocation.WaitElementIsVisible(_driver);
            EllipsisVerticalLocation.WaitAndClick(_driver);
            return this;
        }

        public DraftPage ClickOnEditItem()
        {
            var actions = new Actions(_driver);
            EditItemLocator.WaitAndClickUsingIJavaScriptExecutor(_driver);
            actions.MoveByOffset(200,200);
            actions.Click();
            return this;
        }

        public DraftPage ClickOnFinaliseOnDocExpanderWrapper()
        {
            FinaliseOnDocExpanderWrapperLocator.WaitAndClick(_driver);
            return this;
        }
        public DraftPage ClickOnBuyerPacketCheckBox()
        {
            PopUpLocator.WaitElementIsVisible(_driver);
            BuyerPacketCheckBoxLocator.WaitAndClickUsingIJavaScriptExecutor(_driver);
            return this;
        }
        public DraftPage ClickOnSellerPacketCheckBox()
        {
            PopUpLocator.WaitElementIsVisible(_driver);

            SellerPacketCheckBoxLocator.WaitAndClickUsingIJavaScriptExecutor(_driver);
            return this;
        }

        public DraftPage ClickOnFinaliseButtonOnPopUp()
        {
            PopUpLocator.WaitElementIsVisible(_driver);
            FinaliseButtonOnPopUp.WaitAndClickUsingIJavaScriptExecutor(_driver);
            WaitConfirmatiomMessage();
            return this;
        }

        public DraftPage ClickOnLenderPacketCheckBox()
        {

            PopUpLocator.WaitElementIsVisible(_driver);
            LenderPacketCheckBoxLocator.WaitAndClickUsingIJavaScriptExecutor(_driver);
            return this;
        }
        public DraftPage WaitConfirmatiomMessage()
        {
            ConfirmatiomMessageLocator.WaitElementIsVisible(_driver);
            return this;
        }
        public DraftPage WaitUntilTextEditorIsWisible()
        {
            var actions = new Actions(_driver);
            _driver.SwitchTo().Frame("myTextControlContainer_txframe");
            var textEditor = _driver.FindElement(TextEditorLocator);
            var field = _driver.FindElement(By.Id("mainCanvas"));
            field.Click();
            var action = Keys.Control + "a";
            var act = actions.SendKeys(action).Build();
            act.Perform();
            var text = field.Text;
            IJavaScriptExecutor ex = (IJavaScriptExecutor)_driver;
            ex.ExecuteScript("arguments[0].innerHTML = 'Set text using innerHTML'", field);
            return this;
        }

        public DraftPage ClickOnCloneButton()
        {
            CloneButtonLocator.WaitAndClick(_driver);
            return this;
        }

        public DraftPage TypeClonedFileName(string fileNameForClone)
        {
            var cloneNameField = _driver.FindElement(CloneNameFieldLocator);
            cloneNameField.Clear();
            cloneNameField.SendKeys(fileNameForClone);
            return this;
        }

        public DraftPage ClickOnCloneButtonOnPopup()
        {
            Thread.Sleep(1000);
            CloneButtonOnPopupLocator.WaitAndClickUsingIJavaScriptExecutor(_driver);
            return this;
        }

        public bool FindFileInActiveSection(string fileName)
        {
            try
            {
                DocumentInActiveSectionLocator(fileName).WaitElementIsVisible(_driver);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public DraftPage ClickOnPrintButtonOnActionRow()
        {
            _driver.SwitchTo().Frame("myTextControlContainer_txframe");
            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("mainCanvas"))) ;
            
            //Thread.Sleep(1000);
            _driver.SwitchTo().DefaultContent();

            PrintButtonOnActionRowLocator.WaitAndClickUsingActions(_driver);
            return this;
        }

        public DraftPage ClickOnExportButtonButtonOnActionRow()
        {
            _driver.SwitchTo().Frame("myTextControlContainer_txframe");
            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("mainCanvas")));
            _driver.SwitchTo().DefaultContent();
            ExportButtonOnActionRowLocator.WaitAndClickUsingActions(_driver);
            Thread.Sleep(1000);
            return this;
        }
    }
}
