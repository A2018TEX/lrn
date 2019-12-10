using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock
{
    namespace Laren.E2ETests.Core.Framework.Pages.WorkFlowPages
    {
        public class NotesPage : PageObject
        {
            public NotesPage(IWebDriver driver) : base(driver) { }
            public IWebElement WriteACommentField => _driver.FindElement(By.Name("commentText"));
            public IList<IWebElement> CommentList => _driver.FindElements(By.XPath("//div[@class='comment-text']/p"));
            public IWebElement SaveCommentButton => _driver.FindElement(By.XPath("//span[@title='Save comment']"));

            public NotesPage WriteAComment(string comment)
            {
                WriteACommentField.SendKeys(comment);
                return this;
            }
            public NotesPage ClickOnSaveCommentButton()
            {
                SaveCommentButton.Click();
                Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@class='spinner']")));
                return this;
            }
            public bool CheckThatCommentIsCreated(string comment)
            {
                var commentIsDisplayed = false;
                try
                {
                    Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"//p[contains(text(),'{comment}')]")));
                    commentIsDisplayed = true;
                }
                catch (NoSuchElementException) 
                {
                    commentIsDisplayed = true;
                }

                return commentIsDisplayed;
            }
        }
    }
}
