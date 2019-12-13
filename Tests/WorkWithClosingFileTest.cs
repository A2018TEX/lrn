using Laren.E2ETests.Core.Framework;
using Laren.E2ETests.Core.Framework.DbAccess.Repository;
using Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock;
using Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock.Laren.E2ETests.Core.Framework.Pages.WorkFlowPages;
using Laren.E2ETests.Core.Models;
using Laren.E2ETests.Core.Pages;
using Laren.E2ETests.Core.Utils;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using static Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock.SellerPage;

namespace Laren.E2ETests.Tests
{
    [Parallelizable(ParallelScope.Fixtures)]
    public class WorkWithClosingFileTest : BaseTest
    {
        
        [SetUp]
        public void BeforeEachTests()
        {
            HelperSet.GetNeededData(UserEmail, ClosingFileNumber);
            var login = new LoginPage(Driver, Configuration);

            login
                .SuccessLogin(UserEmail, UserPassword);
        }


        [Test]
        [Ignore("Need to change logic. remove WorkFlow Data")]
        [NUnit.Framework.Description("")]
        [Obsolete]
        public void AddWorkflow()
        {
                HelperSet.AddHelperDataForWorkFlow();
                var workFlownav = new WorkFlowPageNavigation(Driver);
                var workflow = new WorkflowPage(Driver);
                var closingFileWorkflowRep = new ClosingFileWorkflowRepository(DbConn);

                workFlownav
                    .GoToAppropriateClosingFile()
                   .ClicWorkflowMenuItem();
                workflow
                    .ClickOnSetTemplateButton()
                    .ClickOnSetPlanButton()
                    .ClickOnAcceptWorkflowButton();

                Assert.IsNotNull(closingFileWorkflowRep.GetIdOfJustCreatedWorkflowItems(ClosingFileNumber), "Workflow is sucsessfully added");
            
        }

        [Test]
        
        [NUnit.Framework.Description(@"
                Title: Add Vendor With Lender Type
                Pre -Condition:  1. User sign in with credentials: 
                                Login:  bodnarmihail17@gmail.com
                                Password: Bodnar17-mihail
                                2. Closing File is created with 'File Number' -  RT 2019.09.11
                                3.Link: http://195.26.92.83:3475 
                1. Open main page.
                3. Click 'Vendors' at the internal menu.
                4. Click field 'Vendor Type' with drop-down list and select 'Lender' type.
                5. Click field 'Vendor Name' with drop-down list and select any proposed record.")]
        [Obsolete]
        public void AddVendorWithLenderType()
        {
           
            {
                var lenderVendorName = $"Lender{new Random().Next(1000, 50000)}";
                var underwrittenVendorName = $"Lender{new Random().Next(1000, 50000)}";
                HelperSet.HelperMethodAddVendors(lenderVendorName, underwrittenVendorName);
                var workFlownav = new WorkFlowPageNavigation(Driver);
                var vendors = new VendorsPage(Driver);
                var closingRep = new ClosingFilesRepository(DbConn);
                var closingFileVendorRepository = new ClosingFileVendorRepository(DbConn);
                var closinfFileId = closingRep.GetClosingFileIdByClosingNumber(ClosingFileNumber);

                workFlownav
                   .GoToAppropriateClosingFile()
                   .ClickVendorsMenuItem();

                vendors
                    .ClickAddVendorButton()
                    .ClickSelectVendorTypeDropDown()
                    .ClickLenderType()
                    .ClickVendorNameDropdown()
                    .ClickFirstVendorNameInList();

                Assert.IsNotNull(closingFileVendorRepository.GetClosingFileVendorIdByClosingFileId(closinfFileId, lenderVendorName), "Vendor With Lender Type Is Added");
            }
        }

        [Test]        
        [NUnit.Framework.Description(@"
                Title: Add Vendor With Lender Type
                Pre -Condition:  1. User sign in with credentials: 
                                Login:  bodnarmihail17@gmail.com
                                Password: Bodnar17-mihail
                                2. Closing File is created with 'File Number' -  RT 2019.09.11
                                3.Link: http://195.26.92.83:3475 
                1. Open main page.
                3.Click 'Vendors' at the internal menu.
                4. Click field 'Vendor Type' with drop-down list and select 'Underwriter' type.
                5. Click field 'Vendor Name' with drop-down list and select any proposed record.")]
        [Obsolete]
        public void AddVendorWithUnderwriterType()
        {
           
                var lenderVendorName = $"Lender{new Random().Next(1000, 50000)}";
                var underwrittenVendorName = $"Lender{new Random().Next(1000, 50000)}";
                HelperSet.HelperMethodAddVendors(lenderVendorName, underwrittenVendorName);
                var workFlownav = new WorkFlowPageNavigation(Driver);
                var vendors = new VendorsPage(Driver);
                var closingRep = new ClosingFilesRepository(DbConn);
                var closingFileVendorRepository = new ClosingFileVendorRepository(DbConn);
                var closinfFileId = closingRep.GetClosingFileIdByClosingNumber(ClosingFileNumber);

                workFlownav
                  .GoToAppropriateClosingFile()
                  .ClickVendorsMenuItem();

                vendors
                    .ClickAddVendorButton()
                    .ClickSelectVendorTypeDropDown()
                    .ClickUnderwriterType()
                    .ClickVendorNameDropdown()
                    .ClickFirstVendorNameInList();

                Assert.IsNotNull(closingFileVendorRepository.GetClosingFileVendorIdByClosingFileId(closinfFileId, underwrittenVendorName), "Vendor With Underwriter Type Is Added");
        }

        [Test]        
        [Description(@"
                Title: Add Workflow Reminder Notification
                Pre -Condition:  1. User sign in with credentials: 
                                Login:  bodnarmihail17@gmail.com
                                Password: Bodnar17-mihail
                                2. Closing File is created with 'File Number' -  RT 2019.09.11
                                3.Workflow is already created.
                                4.Link: http://195.26.92.83:3475 
                1. Open main page.
                2. Click Closing File with 'File Number' -  RT 2019.09.11.
                3.Click 'Workflow' at the internal menu.
                4. Select any record from 'Open' list.
                5. Click 'Add' button below title 'Notifocations'.
                6. Select checkbox 'Reminders'.
                7. Click button 'Next'.
                8. Click button '+Add' at top of the modal window.
                9. Select 'Reminder 1'  record and click it.
                10. Click field 'Post This Text' and type  any text (as example 'Lorem Ipsum').
                11. Click field 'Days' and type any number.
                12. Click field 'Time' and select from drop-down list proposed time.
                ER: 'Notification' is successfully created.'")]
        [Obsolete]       
        public void AddWorkflowNotification()
        {
                HelperSet.HelperMethodAddWorkflow();

                var workFlownav = new WorkFlowPageNavigation(Driver);
                var workflow = new WorkflowPage(Driver);
                var notificationText = DateTime.Now.ToString();
                var closingFileWorkflowItemNotificationsRepository = new ClosingFileWorkflowRepository(DbConn);

                workFlownav
                    .GoToAppropriateClosingFile()
                    .ClicWorkflowMenuItem();

                workflow
                    .ClickOpenButton()
                    .ClickOnFirstTaskInList()
                    .ClickOnAddNotificationButton()
                    .ClickOnRemindersRadioButton()
                    .ClickOnNextButonOnNotificationsPopup()
                    .ClickOnAddRemindersButton()
                    .ClickOnReminder1()
                    .WriteReminderText(notificationText)
                    .ClickOnTimeField()
                    .SelectTimeValue()
                    .ClickOnDoneButton()
                    .WaitNotificationsCounter();

                Assert.IsNotNull(closingFileWorkflowItemNotificationsRepository.GetIdOfJustCreatedReminderNotification(notificationText), "Reminder Notification is sucsessfully added");
        }

        [Test]        
        [Obsolete]
        public void AddBuyerAddressTest()
        {
            Console.WriteLine("AddBuyerAddressTest");
           
                var userRep = new UserRepository(DbConn);
                var closingFileRep = new ClosingFilesRepository(DbConn);
                var workFlowPage = new WorkFlowPageNavigation(Driver);
                var buyerPage = new BuyerPage(Driver);
                var memberRep = new UserRepository(DbConn);
                var appMemberRep = new AppMemberRepository(DbConn);

                var fName = $"fname{new Random().Next(1000, 50000)}";
                var lName = $"lname{new Random().Next(1000, 50000)}";

                appMemberRep.AddNewAppMember(fName, lName);

                var closingFileId = closingFileRep.GetClosingFileId(ClosingFileNumber);

                HelperSet.HelperMethodAddWorkflow();

                closingFileRep.AddSellerBuyerToClosingFile(closingFileId, UserId);
                var closingFile = closingFileRep.SelectClosingFileWithBuyer(ClosingFileNumber, UserId);
                var closingFileNumber = closingFile.ClosingNumber;
                var buyerFName = closingFile.FirstName;
                var buyerLName = closingFile.LastName;
                Driver.Manage().Window.Maximize();
                workFlowPage
                    .GoToAppropriateClosingFile()
                    .ClickBuyerMenuItem()
                    .ClickOnBuyer($"{buyerFName}  {buyerLName}");
                buyerPage
                    .ClickAddressMenuItem()
                    .TypeAddress1Field("500 Southeast 8th Avenue, Portland, OR, US");
                Thread.Sleep(1000);

                Assert.IsTrue(closingFileRep.GetAddressId().Contains(closingFileRep.GetAddressIdForBuyer(buyerFName)));
        }

        [Test]  
        public void AddBuyerToClosingFile()
        {
           
            {
                var workFlowPage = new WorkFlowPageNavigation(Driver);
                var buyerPage = new BuyerPage(Driver);
                var appMembersRep = new AppMemberRepository(DbConn);
                
                HelperSet.HelperMethodAddWorkflow();

                var firstName = Configuration.GetSection("BuyerFirstName").Value;
                var lastName = Configuration.GetSection("BuyerLastName").Value;

                workFlowPage
                    .GoToAppropriateClosingFile()
                    .ClickBuyerMenuItem();

                buyerPage
                    .ClickAddBuyerButton()
                    .SelectBuyerType()
                    .TypeFirstName(firstName)
                    .TypeLastName(lastName)
                    .SelectGender()
                    .SelectMaritalStatus()
                    .SelectTenancy(Configuration.GetSection("BuyerFirstName").Value);

                Assert.AreEqual(lastName, appMembersRep.GetSellerLastName(firstName), "Buyer Was created");
            }
        }

        [Test]        
        [NUnit.Framework.Description(@"
                Title: Add Comment to Workflow Task
                Pre -Condition:  1. User sign in with credentials: 
                                Login:  bodnarmihail17@gmail.com
                                Password: Bodnar17-mihail
                                2. Closing File is created with 'File Number' -  RT 2019.09.11
                                3.Workflow is already created.
                                4.Link: http://195.26.92.83:3475 
                1. Open main page.
                2. Click Closing File with 'File Number' -  RT 2019.09.11.
                3.Click 'Workflow' at the internal menu.
                4. Click on any record from 'Open' list.
                5. Click field 'Write a comment...' and type comment(any text)
                6. Click on 'Save comment' button
                ER: Comment at Workflow Task is successfully created.")]
        public void AddCommentToWorkflow()
        {
                HelperSet.HelperMethodAddWorkflow();

                var workFlownav = new WorkFlowPageNavigation(Driver);
                var workflow = new WorkflowPage(Driver);
                var comment = DateTime.Now.ToString();

                workFlownav
                    .GoToAppropriateClosingFile()
                    .ClicWorkflowMenuItem();

                workflow
                    .ClickOpenButton()
                    .ClickOnFirstTaskInList()
                    .WriteAComment(comment)
                    .ClickOnSaveCommentButton();

                Assert.IsNotNull(workflow.CheckThatCommentIsCreated(comment));
        }

        [Test]               
        [Description(@"
        DataRow# 1
                Title: Add Closing File
                Pre-Condition:  1. User sign in with credentials: 
                                Login:  bodnarmihail17 @gmail.com
                                Password: Bodnar17-mihail
                                2. Link: http://195.26.92.83:3475    
                1. Open main page.
                2. Click button + at the right top corner side.
                3. Click button Closing File at the pop-up modal window Select the type of record you would like to create.
                4. At opened modal window Add File click required field File Number and type as example RT 2019.09.11
                5. Click required field Transaction Type and choose one of three records: Cash, Finance, Refinance.
                6. Click required field Usage Type and  choose one of two records: Residential or Commercial.
                7. Click optional field Property Type and choose one of four records: Lot, House, Condo, Building.
                8. Click button Next at the right bottom corner side modal window.
                ")]
        [Obsolete]
        public void AddClosingFile()
        {
            HelperSet.HelperMethodAddWorkflow();

            string closingFileNumber = $"CF{new Random().Next(200, 500000)}";
            var headerMenu = new HeaderManu(Driver);
            var login = new LoginPage(Driver, Configuration);
            var closingFileRep = new ClosingFilesRepository(DbConn);
            headerMenu
                .ClickAddCircleButton()
                .ClickClosingButton()
                .TypeFilenumberField(closingFileNumber)
                .ChooseTransactionTypeValue()
                .ChooseFinanceTypeValue()
                .ChooseUsageTypeValue()
                .ChoosePropertyTypeValue()
                .ClickNextButton();
            var dbFileNumber = closingFileRep.GetClosingNumberList(closingFileNumber);

            Assert.IsTrue(dbFileNumber.Contains(closingFileNumber), "New ClosingFile is created");

            closingFileRep.DeleteClosingFiles(UserEmail, closingFileNumber);
        }

        [Test]        
        [Ignore("Need to fix Assert")]
        [NUnit.Framework.Description(@"

                ")]
        public void AddImportantDatesTest()
        {
                var workFlowPage = new WorkFlowPageNavigation(Driver);
                var importantDatesPage = new ImportantDatesPage(Driver);
                var closingRep = new ClosingFilesRepository(DbConn);

                var dtFunding = $"{DateTime.Now.Date.AddDays(6).ToString($"{ 0:dddd',' MMMM d',' yyyy}")}";
                var dtClosing = $"{DateTime.Now.Date.AddDays(5).ToString($"{ 0:dddd',' MMMM d',' yyyy}")}";
                var dtEffective = $"{DateTime.Now.Date.AddDays(2).ToString($"{ 0:dddd',' MMMM d',' yyyy}")}";
                var dtBuyerSigning = $"{DateTime.Now.Date.AddDays(3).ToString($"{ 0:dddd',' MMMM d',' yyyy}")}";
                var dtSellerSigning = $"{DateTime.Now.Date.AddDays(3).ToString($"{ 0:dddd',' MMMM d',' yyyy}")}";

                workFlowPage
                   .GoToWorkFlowPage()
                    .GoToAppropriateClosingFile()
                    .ClickDetailsMenuItem();
                importantDatesPage
                    .ClickImportantDatesSideMenuItem()
                    .SetEffectiveDate(dtEffective)
                    .ClickOkOnPopupIfDisplayed()
                    .SetCllosingDate(dtClosing)
                    .ClickOkOnPopupIfDisplayed()
                    .SetBuyerSigningDate(dtBuyerSigning)
                    .SetFundingDate(dtFunding)
                    .SetSellerSignInDate(dtSellerSigning)
                    .ClickOkButtonClosingDateModalWindow()
                    .SetSellerSignInDate(dtSellerSigning)
                    .ClickOkButtonClosingDateModalWindow();

                var closingDate = closingRep.GetClosingFileDate(ClosingFileNumber, UserEmail);

                Assert.AreEqual(dtClosing, closingDate, "Date is edited");
        }

        [Test]        
        public void AddMoneyTest()
        {
                HelperSet.HelperMethodAddWorkflow();

                var workFlowPage = new WorkFlowPageNavigation(Driver);
                var moneyPage = new MoneyPage(Driver);
                var summarypage = new SummaryPage(Driver);
                var closingRep = new ClosingFilesRepository(DbConn);
                var closingFileEscrowDepRep = new ClosingFileEscrowDepositsRepository(DbConn);

                HelperSet.AddBankAndBankAccounts();

                Random rand = new Random();
                decimal purchasePrice = rand.Next(10000, 50000);
                decimal escrowDep = rand.Next(1000, 5000);

                workFlowPage
                    .GoToWorkFlowPage()
                    .GoToAppropriateClosingFile()
                    .ClickDetailsMenuItem();
                moneyPage
                     .ClickMoneySideMenuItem()
                     .CleanAndEnterPurchasePrice(purchasePrice)
                     .ClearAndEnterEscrowDeposit(escrowDep)
                     .ClearAndEnterLoanAmount("10")
                     .ChooseEscrowAccount();

                decimal eRexpPrice = purchasePrice;
                decimal erEscrowDeposit = escrowDep;
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

                //wait.Until((Driver) => closingRep.GetPurchasePrice(ClosingFileNumber) == eRexpPrice);

                decimal expPurchPrice = closingRep.GetPurchasePrice(ClosingFileNumber);
                decimal expEscrDeposit = closingFileEscrowDepRep.GetEscrowDeposit(ClosingFileNumber);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(eRexpPrice, expPurchPrice, "Purchase Price is changed");
                    Assert.AreEqual(erEscrowDeposit, expEscrDeposit, "Escrow Price is changed");
                });
        }

        [Test]
        [NUnit.Framework.Description(@"
                Title: Add Note to 'Notes'
                Pre -Condition:  1. User sign in with credentials: 
                                Login:  bodnarmihail17@gmail.com
                                Password: Bodnar17-mihail
                                2. Closing File is created with 'File Number' -  RT 2019.09.11
                                3.Link: http://195.26.92.83:3475 
                1. Open main page.
                2. Click Closing File with 'File Number' -  RT 2019.09.11.
                3. Click 'Notes' at the internal menu.
                4. Click field 'Write a comment...' and type Note (any text)
                5.Click on 'Save comment' button
                ER: Note is successfully created.")]
        public void AddNoteToNotes()
        {
                HelperSet.HelperMethodAddWorkflow();

                var workFlownav = new WorkFlowPageNavigation(Driver);
                var notes = new NotesPage(Driver);
                var closingRep = new ClosingFilesRepository(DbConn);
                var comment = DateTime.Now.ToString();
                workFlownav
                    .GoToWorkFlowPage()
                    .GoToAppropriateClosingFile()
                    .ClickNotesMenuItem();

                notes
                    .WriteAComment(comment)
                    .ClickOnSaveCommentButton();

                Assert.IsTrue(notes.CheckThatCommentIsCreated(comment));
        }

        [Test]        
        [Ignore("NEED TO ADD ASSERTS")]
        public void AddParticipants()
        {
                var workFlowPage = new WorkFlowPageNavigation(Driver);
                var participantPage = new ParticipantsPage(Driver);

                var firstName = $"AutoTest{DateTime.Now.ToString("ddmmyy")}";
                var lastName = $"AutoTest{DateTime.Now.ToString("ddmmyy")}";

                workFlowPage
                    .GoToWorkFlowPage(/*Configuration.GetSection("NewUserEmail").Value, Configuration.GetSection("NewUserPassword").Value*/)
                        .GoToAppropriateClosingFile()
                        .ClickDetailsMenuItem();
                participantPage
                         .ClickParticipantsSideMenuItem()
                         .ChooseOriginator()
                         .ChooseReferralSource()
                         .ChooseReferralSource()
                         .TypeFirstName(firstName)
                         .TypeLastName(lastName)
                         .ClickSaveButton();

                //Assert.Multiple(() =>
                //{
                //    Assert.AreEqual();
                //    Assert.AreEqual();
                //});
        }

        [Test]        
        [Ignore("Need to fix Assert")]
        [NUnit.Framework.Description(@"
                DataRow# 2
                Title: Add Details Closing File. Add Property from Sections menu.
                Pre-Condition:  1. User sign in with credentials: 
                                Login:  bodnarmihail17@gmail.com
                                Password: Bodnar17-mihail
                                2. Closing File is created with File Number -  RT 2019.09.11
                                3.Link: http://195.26.92.83:3475 
                1. Open main page.
                2. Click Closing File with File Number -  RT 2019.09.11.
                3.Click Details at the internal menu.
                4. Click Property from Sections menu.
                5. Click Address 1 and type as example 16218
                6. Select the address from the drop-down list.
                7. Click button Lookup.
                8. Click on the field with a drop-down list and select any of proposed Parcel ID at the pop-up modal window Multiple Parcel Results.
                9. Click button Confirm at the pop-up modal window Multiple Parcel Results.
                ")]
        [Obsolete]
        public void AddDetailPropertyToClosingFile()
        {
                //HelperSet.DisplayWidget("Property Maps");

                var workFlownav = new WorkFlowPageNavigation(Driver);
                var property = new PropertyPage(Driver);
                var summaryPage = new SummaryPage(Driver);

                workFlownav
                   .GoToWorkFlowPage()
                   .GoToAppropriateClosingFile()
                    .ClickDetailsMenuItem();
                property
                    .ClickPropertySideMenuItem()
                    .TypeAddress1Field("500 Southeast 8th Avenue, Hillsboro")
                    .ClickLookupButton();
                //    .ChooseState("500 Southeast 8th Avenue")
                //    .ClickConfirmButton();
                //workFlownav
                //    .ClickSummaryMenuItem();
                Driver.Navigate().Refresh();

                var addresslocation = summaryPage.GetLocation();
                Assert.IsTrue(addresslocation.Contains("Hillsboro"));
        }

        [Test]        
        public void AddSellerToClosingFile()
        {
                HelperSet.HelperMethodAddWorkflow();

                var workFlowPage = new WorkFlowPageNavigation(Driver);
                var sellerPage = new SellersPage(Driver);
                var appMembersRep = new AppMemberRepository(DbConn);
                var closingRep = new ClosingFilesRepository(DbConn);

                var closinfFileName = closingRep.GetClosingNumberListWithoutBuyerSeller();

                workFlowPage
                    .GoToWorkFlowPage()
                    .GoToAppropriateClosingFile()
                    .ClickSellerMenuItem();

            sellerPage
                .ClickAddSellersButton()
                .SelectSellerType()
                .TypeFirstName(Configuration.GetSection("SallerFirstName").Value)
                .TypeLastName(Configuration.GetSection("SellerLastName").Value)
                .TypeEmail(Configuration.GetSection("SellerEmail").Value)
                .SelectGender()
                .SelectMaritalStatus()
                .SelectExemptField(Configuration.GetSection("SallerFirstName").Value);

                Assert.AreEqual(Configuration.GetSection("SellerLastName").Value, appMembersRep.GetSellerLastName(Configuration.GetSection("SallerFirstName").Value));

                var appMemberRep = new AppMemberRepository(DbConn);
                appMemberRep.DeleteSellerBuyerByFirstName(Configuration.GetSection("SallerFirstName").Value);
        }

        [TearDown]       
        public void AfterEachTests()
        {
            var userRepository = new UserRepository(DbConn);
            LocalStorageJS localstorage = new LocalStorageJS(Driver);
            localstorage.CleanSessionStorage();
            localstorage.ClearLocalStorage();
            userRepository.DeleteDataAfterEachTest(UserEmail);
            HelperSet.SignOut();
        }
    }
}