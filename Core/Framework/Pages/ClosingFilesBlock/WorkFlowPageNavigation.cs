using Laren.E2ETests.Core.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock
{
    public class WorkFlowPageNavigation : PageObject
    {
        public WorkFlowPageNavigation(IWebDriver driver) : base(driver) { }

        public IWebElement DetailsMenuItem => _driver.FindElement(By.XPath("//a[contains(text(),' Details')]"));
        public IWebElement BuyerMenuItem => _driver.FindElement(By.XPath("//li[@class='ng-star-inserted']/a[contains(text(),' Buyer')]"));
        public IWebElement NextButton => _driver.FindElement(By.XPath("//button[@class='next-btn']"));        
        public IWebElement SummaryMenuItem => _driver.FindElement(By.XPath("//li[@class='ng-star-inserted']/a[contains(text(),' Summary')]"));
        public IWebElement SignInPage => _driver.FindElement(By.XPath("//a[@routerlink='login']"));
        public IWebElement WorkFlowMenuItem => _driver.FindElement(By.XPath("//a[@href='/workflow']/span"));
        public IWebElement BuyesrName => _driver.FindElement(By.XPath("//span[@class='action ng-star-inserted'][1]"));
        public IWebElement SellerMenuItem => _driver.FindElement(By.XPath("//li[@class='ng-star-inserted']/a[contains(text(),' Seller')]"));
        public IWebElement NotesMenuItem => _driver.FindElement(By.XPath("//li[@class='ng-star-inserted']/a[contains(text(),' Notes')]"));
        //public IWebElement WorkflowMenuItem => _driver.FindElement(By.XPath("//li[@class='ng-star-inserted']/a[contains(text(),' Workflow')]"));
        public By WorkflowMenuItem => By.XPath("//li[@class='ng-star-inserted']/a[contains(text(),' Workflow')]");
        public By VendorsMenuItem => By.XPath("//li[@class='ng-star-inserted']/a[contains(text(),'Vendors')]");
        public IWebElement ViewWidgets => _driver.FindElement(By.XPath("//button[contains(text(),' View Widgets ')]/span"));
        public IWebElement PropertyMaps => _driver.FindElement(By.XPath("//label[contains(text(),'Property maps')]"));
        public IWebElement SaveButton =>_driver.FindElement(By.XPath("//button[@type='submit']"));
        public IWebElement DetailsCheckBox => _driver.FindElement(By.XPath("//label[contains(text(),'Details')]"));
        public By DocumentsItemOnDropDown => By.Id("but-1");
        public IWebElement MenuDropDown => _driver.FindElement(By.XPath("//clr-dropdown[@class='dropdown open ng-star-inserted']//clr-icon[@shape='bars']"));
        public IWebElement LibrarySubItem => _driver.FindElement(By.XPath("//button[contains(text(), 'Library')]"));
        public IWebElement DraftsSubItem => _driver.FindElement(By.XPath("//button[contains(text(), 'Drafts')]"));
        public IWebElement FinalSubItem => _driver.FindElement(By.XPath("//button[contains(text(), 'Final')]"));
        public By FinalSubItemLocator => By.XPath("//button[contains(text(), 'Final')]");
        public IWebElement UploadSubItem => _driver.FindElement(By.XPath("//button[contains(text(), 'Uploads')]"));
        public IWebElement WorkflowCheckBox => _driver.FindElement(By.XPath("//label[contains(text(),'Workflow')]"));

        public WorkFlowPageNavigation GoToAppropriateClosingFile(/*string closingFileName*/)
        {
            GoToWorkFlowPage();
            //ClickAppropriateClosingField(closingFileName);
            return this;
        }
        public WorkFlowPageNavigation ClickSummaryMenuItem()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@class='ng-star-inserted']/a[contains(text(),' Summary')]")));
            SummaryMenuItem.Click();
            return new WorkFlowPageNavigation(_driver);
        }
        public WorkFlowPageNavigation ClickSellerMenuItem()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@class='ng-star-inserted']/a[contains(text(),' Seller')]")));
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            SellerMenuItem.Click();
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            return new WorkFlowPageNavigation(_driver);
        }
        public WorkFlowPageNavigation ClickVendorsMenuItem()
        {
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            VendorsMenuItem.WaitAndClick(_driver);
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            return this;
        }
        public bool IsElementDisplayed()
        {           
            bool isDisplayed = false;
            try
            {
                
                IWebElement el = _driver.FindElement(By.XPath("//a[@routerlink='workflow']"));
                isDisplayed = el.Displayed;
                return isDisplayed;
            }
            catch
            {
                return isDisplayed;
            }
        }
        public WorkFlowPageNavigation GoToWorkFlowPage(/*string login, string password*/)
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@routerlink='workflow']")));
            WorkFlowMenuItem.Click();
            //if (!IsElementDisplayed())
            //{

            //    SignInPage.Click();
            //    var loginPage = new LoginPage(_driver);
            //    loginPage.SuccessLogin(login, password);
            //    Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@routerlink='workflow']")));
            //    WorkFlowMenuItem.Click();
            //}
            //if (IsElementDisplayed())
            //{
            //    WorkFlowMenuItem.Click();
            //}
            return this;
        }
        public WorkFlowPageNavigation ClickDetailsMenuItem()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(),' Details')]")));
            DetailsMenuItem.Click();
            Thread.Sleep(1000);
            return this;
        }                          
        public bool IsElementDisplayed(string closingName)
        {
            bool isDisplayed = false;
            try
            {
                IWebElement el = _driver.FindElement(By.XPath($"//div[contains(text(),' {closingName}')]"));
                isDisplayed = el.Displayed;
                return isDisplayed;
            }
            catch
            {
                return isDisplayed;
            }
        }
 
        public WorkFlowPageNavigation ClickBuyerMenuItem()
        {
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//li[@class='ng-star-inserted']/a[contains(text(),' Buyer')]")));            
            BuyerMenuItem.Click();
            Thread.Sleep(1000);
            return this;
        }
        public WorkFlowPageNavigation ClickOnBuyer(string name)
        {
           Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//span[contains(text(),'{name}')]")));
            BuyesrName.Click();
            
            return this;
        }
        public WorkFlowPageNavigation ClickNotesMenuItem()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//li[@class='ng-star-inserted']/a[contains(text(),' Notes')]")));
            NotesMenuItem.Click();
            return this;
        }
        public WorkFlowPageNavigation ClicWorkflowMenuItem()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//li[@class='ng-star-inserted']/a[contains(text(),' Notes')]")));
            try
            {
                WorkflowMenuItem.WaitAndClickUsingIJavaScriptExecutor(_driver);
            }
            catch
            {
                WorkflowMenuItem.WaitAndClickUsingIJavaScriptExecutor(_driver);
            }
            return this;
        }

        public bool Address1IsElementDisplayed()
        {
            bool isDisplayed = false;
            try
            {
                IWebElement el = _driver.FindElement(By.XPath("//input[@ng-reflect-model='SE 8th Ave']"));
                isDisplayed = el.Displayed;
                return isDisplayed;
            }
            catch
            {
                return isDisplayed;
            }
        }
        
       public WorkFlowPageNavigation WaitBuyerName(string buyerName)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//div[@class='row ng-star-inserted']//span[contains(text(),'{buyerName}')]")));
            return this;
        }
        public WorkFlowPageNavigation ClickViewWidgetsButton()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//clr-icon")));
            ViewWidgets.Click();
            return this;
        }
        public WorkFlowPageNavigation ClickPropertyMapsCheckBox()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//label[contains(text(),'Property maps')]")));
            PropertyMaps.Click();
            return this;
        }

        public WorkFlowPageNavigation ClickDetailsCheckBox()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//label[contains(text(),'Details')]")));
            DetailsCheckBox.Click();
            return this;
        }
        public WorkFlowPageNavigation ClickWorkFlowCheckBox()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//label[contains(text(),'Workflow')]")));
            WorkflowCheckBox.Click();
            return this;
        }
        public WorkFlowPageNavigation ClickSaveButton()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@type='submit']")));
            SaveButton.Click();
            return this;
        }
       
        public WorkFlowPageNavigation ClickOnMenuDropDown()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//clr-dropdown[@class='dropdown open ng-star-inserted']//clr-icon[@shape='bars']")));
            MenuDropDown.Click();
            return this;
        }
        public WorkFlowPageNavigation GoToDocumentsDraftPage()
        {
            var actions = new Actions(_driver);
            ClickOnDocumentsItemOnDropDown();
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Drafts')]")));
            actions.MoveToElement(DraftsSubItem).Click().Perform();
            //DraftsSubItem.Click();
            return this;
        }
        public FinalPage GoToDocumentsFinalPage()
        {
            ClickOnDocumentsItemOnDropDown();
            FinalSubItemLocator.WaitAndClickUsingActions(_driver);
            return new FinalPage(_driver);
        }        
        public WorkFlowPageNavigation ClickOnDocumentsItemOnDropDown()
        {
            ClickOnMenuDropDown();
            //Wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("but-1")));
            //DocumentsItemOnDropDown.Click();
            try
            {
                DocumentsItemOnDropDown.WaitAndClick(_driver);
            }
            catch
            {
                DocumentsItemOnDropDown.WaitAndClick(_driver);

            }
            return this;
        }        
        public WorkFlowPageNavigation GoToDocumentsLibraryPage()
        {
            var actions = new Actions(_driver);
            ClickOnDocumentsItemOnDropDown();
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Library')]")));
            actions.MoveToElement(LibrarySubItem).Click().Perform();
            //LibrarySubItem.Click();
            return this;
        }
        public WorkFlowPageNavigation GoToDocumentsUploadPage()
        {
            var actions = new Actions(_driver);
            ClickOnDocumentsItemOnDropDown();
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Drafts')]")));
            actions.MoveToElement(UploadSubItem).Click().Perform();
            //DraftsSubItem.Click();
            return this;
        }
    }
}
