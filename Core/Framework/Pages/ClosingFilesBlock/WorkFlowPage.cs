using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock
{
    public class WorkflowPage : PageObject
    {
        public WorkflowPage(IWebDriver driver) : base(driver) { }
        public IWebElement OpenButton => _driver.FindElement(By.XPath("//button[contains(text(), 'Open')]"));
        public IWebElement FirstTaskInList => _driver.FindElement(By.XPath("//div[@class='table-cell col-sm ng-star-inserted'][1]"));
        public IWebElement WriteACommentField => _driver.FindElement(By.Id("commentInput"));
        public IWebElement SaveCommentButton => _driver.FindElement(By.XPath("//span[@title='Save comment']"));
        public IWebElement DoneButton => _driver.FindElement(By.XPath("//div[@class='modal-footer mt3 notificationFooter']//button[contains(text(), 'Done')]"));
        public IWebElement SetTemplateButton => _driver.FindElement(By.XPath("//button[contains(text(), 'Set Template')]"));
        public IWebElement SetPlanButton => _driver.FindElement(By.XPath("//button[contains(text(), 'Set Plan')]"));
        public IWebElement AcceptWorkflowButton => _driver.FindElement(By.XPath("//button[contains(text(), 'Accept Workflow')]"));
        public IWebElement AddNotificationButton => _driver.FindElement(By.XPath("//div[@class='col-sm-2 notifCol']//a[contains(text(), 'Add')]"));
        public IWebElement RemindersRadioButton => _driver.FindElement(By.XPath("//div[@class='radio']//input[@id='styled-checkbox-2']/../label[@for='styled-checkbox-2']"));
        public IWebElement NextButonOnNotificationsPopup => _driver.FindElement(By.XPath("//div[@class='modal-body']//button[contains(text(), 'Next')]"));
        public IWebElement AddRemindersButton => _driver.FindElement(By.XPath("//div[@class='addSection']//span[@class='addNotification']"));
        public IWebElement Reminder1 => _driver.FindElement(By.XPath("//dt[@class='stack-block-label']//span[contains(text(), 'Reminder 1')]"));
        public IWebElement ReminderTextField => _driver.FindElement(By.Name("reminderText"));
        public IWebElement TimeField => _driver.FindElement(By.Name("time"));
        public IWebElement TimeValue => _driver.FindElement(By.XPath("//option[@value='12:00 AM']"));
        public IWebElement NotificationsCounter => _driver.FindElement(By.XPath("//*[@shape='bell']/../*[@class='badge badge-success totalCount']"));
        public By SubmitButtonOnMissingDatesPopUpLocator => By.XPath("//div[@class='modal-content-wrapper']//button[contains(text(), 'Submit')]");
        public WorkflowPage ClickOpenButton()
        {            
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(), 'Open')]")));
            OpenButton.Click();
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            return this;
        }
        public WorkflowPage ClickOnFirstTaskInList()
        {
            FirstTaskInList.Click();
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            return this;
        }
        public WorkflowPage ClickOnAddNotificationButton()
        {
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='col-sm-2 notifCol']//a[contains(text(), 'Add')]")));
            AddNotificationButton.Click();
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            return this;
        }
        public WorkflowPage WriteAComment(string comment)
        {
            Thread.Sleep(1000);
            WriteACommentField.SendKeys(comment);
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            return this;
        }
        public WorkflowPage ClickOnSaveCommentButton()
        {
            SaveCommentButton.Click();
            return this;
        }
        public WorkflowPage CheckThatCommentIsCreated(string comment)
        {
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"//p[contains(text(),'{comment}')]")));
            return this;
        }
        public WorkflowPage ClickOnDoneButton()
        {
            DoneButton.Click();
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            return this;
        }
        public WorkflowPage ClickOnSetTemplateButton()
        {
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Set Template')]")));
            SetTemplateButton.Click();
            return this;
        }
        public WorkflowPage ClickOnSetPlanButton()
        {
            ClickOnSubmitButtonOnMissingDatesPopUp();
            
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Set Plan')]")));
            SetPlanButton.Click();
            return this;
        }

        public WorkflowPage ClickOnSubmitButtonOnMissingDatesPopUp()
        {
            try
            {
                var submitButton = _driver.FindElement(SubmitButtonOnMissingDatesPopUpLocator);
                if (submitButton != null)
                    submitButton.Click();
                return this;
            }
            //var isDisplayed = false;
            catch
            {
                return this;
            }
        }

        public WorkflowPage ClickOnAcceptWorkflowButton()
        {
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Accept Workflow')]")));
            AcceptWorkflowButton.Click();
            return this;
        }
        public WorkflowPage ClickOnRemindersRadioButton()
        {
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[@role='dialog']")));
            var action = new Actions(_driver);
            action.MoveToElement(RemindersRadioButton);
            action.Click();
            action.Build().Perform();
            return this;
        }
        public WorkflowPage ClickOnNextButonOnNotificationsPopup()
        {
            NextButonOnNotificationsPopup.Click();
            return this;
        }
        public WorkflowPage ClickOnAddRemindersButton()
        {
            AddRemindersButton.Click();
            return this;
        }
        public WorkflowPage ClickOnReminder1()
        {
            Reminder1.Click();
            return this;
        }
        public WorkflowPage WriteReminderText(string notificationText)
        {
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("reminderText")));
            ReminderTextField.SendKeys(notificationText);
            return this;
        }
        public WorkflowPage ClickOnTimeField()
        {
            return this;
        }
        public WorkflowPage SelectTimeValue()
        {
            TimeValue.Click();
            return this;
        }
        public WorkflowPage WaitNotificationsCounter()
        {
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
            Wait.Until(ExpectedConditions.TextToBePresentInElement(NotificationsCounter, "1"));
            return this;
        }
        
    }
}
