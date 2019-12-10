using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock
{
    public class ParticipantsPage : PageObject
    {
        public ParticipantsPage(IWebDriver driver) : base(driver) { }

        public IWebElement ParticipantsSideMenuItem => _driver.FindElement(By.XPath("//div[@class='col-sm-12']/span[contains(text(),'Participants')]"));
        public IWebElement Closinglocation => _driver.FindElement(By.Name("closinglocation"));
        public IWebElement Originator => _driver.FindElement(By.Name("originator"));
        public IWebElement ReferralSource => _driver.FindElement(By.Name("referee"));
        public IWebElement Attorney => _driver.FindElement(By.XPath("//label[contains(text(),'Attorney')]/..//select[@class='clr-select select-laren selectBox text-gray']"));
        public IWebElement SystemGuest => _driver.FindElement(By.XPath("//label[contains(text(),'SystemGuest')]/..//select[@class='clr-select select-laren selectBox text-gray']"));
        public IWebElement System => _driver.FindElement(By.XPath("//label[contains(text(),'System')]/..//select[@class='clr-select select-laren selectBox text-gray']"));
        public IWebElement Administrator => _driver.FindElement(By.XPath("//label[contains(text(),'Administrator')]/..//select[@class='clr-select select-laren selectBox text-gray']"));
        public IWebElement Integrator => _driver.FindElement(By.XPath("//label[contains(text(),'Integrator')]/..//select[@class='clr-select select-laren selectBox text-gray']"));
        public IWebElement FirstName => _driver.FindElement(By.Name("firstName"));
        public IWebElement LastName => _driver.FindElement(By.Name("lastName"));
        public IWebElement CompanyName => _driver.FindElement(By.Name("companyName"));
        public IWebElement EmailAddress => _driver.FindElement(By.Name("email"));
        public IWebElement PhoneField => _driver.FindElement(By.Name("phone"));
        public IWebElement MobileField => _driver.FindElement(By.Name("mobile"));
        public IWebElement ContactTypeDropDownMenu => _driver.FindElement(By.Name("closingFileContactType"));
        public IWebElement CancelButton => _driver.FindElement(By.XPath("//button[@class='btn btn-laren']"));
        public IWebElement SaveButton => _driver.FindElement(By.XPath("//button[contains(text(),'Save')]"));


        public ParticipantsPage ClickParticipantsSideMenuItem()
        {
            ParticipantsSideMenuItem.Click();
            return this;
        }
        public ParticipantsPage ChooseAdministrator()
        {
            SelectElement sl = new SelectElement(Administrator);
            sl.SelectByIndex(1);
            return this;
        }
        public ParticipantsPage ChooseSystem()
        {
            SelectElement sl = new SelectElement(System);
            sl.SelectByIndex(2);
            return this;
        }
        public ParticipantsPage ChooseOriginator()
        {
            SelectElement sl = new SelectElement(Originator);
            sl.SelectByIndex(1);
            return this;
        }
        public ParticipantsPage ChooseReferralSource()
        {
            SelectElement sl = new SelectElement(ReferralSource);
            sl.SelectByText("Other");
            return this;
        }
        public ParticipantsPage TypeFirstName(string FName)
        {
            FirstName.SendKeys(FName);
            return this;
        }
        public ParticipantsPage TypeLastName(string LName)
        {
            LastName.SendKeys(LName);
            return this;
        }
        public ParticipantsPage ClickSaveButton()
        {
            SaveButton.Click();
            return this;
        }
    }    
}
