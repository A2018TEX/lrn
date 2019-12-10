using Laren.E2ETests.Core.Framework.DbAccess.Repository;
using Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock;
using Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock.Laren.E2ETests.Core.Framework.Pages.WorkFlowPages;
using Laren.E2ETests.Core.Models;
using Laren.E2ETests.Core.Pages;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using static Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock.SellerPage;

namespace Laren.E2ETests.Tests
{

    public class WorkWithClosingFileTestContext
    {
        public string ClosingFileNumber;

        public WorkWithClosingFileTestContext()
        {
            var randValue = new Random().Next(10000, 500000);
            ClosingFileNumber = $"CF{randValue}";
        }

        public WorkWithClosingFileTestContext(string closingFileNumber)
        {
            ClosingFileNumber = closingFileNumber;
        }
    }

    public class WorkWithClosingFileTest 
    {
        private readonly IConfiguration _configuration;

        public WorkWithClosingFileTest()
        {
            _configuration = new ConfigurationFactory().CreateInstance();
        }

        public void BeforeEachTest(TestScope<WorkWithClosingFileTestContext> testScope)
        {
            var rand = new Random();
            var rndValue = rand.Next(1000, 50000);
            var tempUser = new User()
            {
                Email = $"email{rndValue}@gmail.com",
                Password = "QWE123!!"
            };

            var id = testScope.HelperSet.CreateNewUser(tempUser.Email);
            testScope.HelperSet.InsertClosingFileToDB(tempUser.Email, testScope.Context.ClosingFileNumber);
            //testScope.HelperSet.HelperMethodAddWorkflow();
            testScope.UserEmail = tempUser.Email;
            testScope.UserId = id;
        }

        public void AfterEachTest(TestScope<WorkWithClosingFileTestContext> testScope)
        {
            var userRepository = new UserRepository(testScope.DbConnection);
            userRepository.DeleteDataAfterEachTest(testScope.UserEmail);
            testScope.HelperSet.DeleteAllTestData(testScope.UserEmail);
            testScope.HelperSet.DeleteDataAfterWorkflowNotificationTest(testScope.UserEmail);
            testScope.Driver.Close();
            testScope.Driver.Quit();
            testScope.DbConnection.CloseConnection();
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        //[Ignore("Need to change login. remove WorkFlow Data")]
        [NUnit.Framework.Description("")]
        public void AddWorkflow()
        {
            using (var scope = new TestScope<WorkWithClosingFileTestContext>(_configuration, BeforeEachTest, AfterEachTest))
            {
                scope.HelperSet.AddHelperDataForWorkFlow();
                var workFlownav = new WorkFlowPageNavigation(scope.Driver);
                var workflow = new WorkflowPage(scope.Driver);
                var closingFileWorkflowRep = new ClosingFileWorkflowRepository(scope.DbConnection);

                workFlownav
                    .GoToAppropriateClosingFile()
                   .ClicWorkflowMenuItem();
                workflow
                    .ClickOnSetTemplateButton()
                    .ClickOnSetPlanButton()
                    .ClickOnAcceptWorkflowButton();

                Assert.IsNotNull(closingFileWorkflowRep.GetIdOfJustCreatedWorkflowItems(scope.Context.ClosingFileNumber), "Workflow is sucsessfully added");
            }
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
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
            using (var scope = new TestScope<WorkWithClosingFileTestContext>(_configuration, BeforeEachTest, AfterEachTest))
            {
                var lenderVendorName = $"Lender{new Random().Next(1000, 50000)}";
                var underwrittenVendorName = $"Lender{new Random().Next(1000, 50000)}";
                scope.HelperSet.HelperMethodAddVendors(lenderVendorName, underwrittenVendorName);
                var workFlownav = new WorkFlowPageNavigation(scope.Driver);
                var vendors = new VendorsPage(scope.Driver);
                var closingRep = new ClosingFilesRepository(scope.DbConnection);
                var closingFileVendorRepository = new ClosingFileVendorRepository(scope.DbConnection);
                var closinfFileId = closingRep.GetClosingFileIdByClosingNumber(scope.Context.ClosingFileNumber);

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
        [Parallelizable(ParallelScope.Self)]
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
            using (var scope = new TestScope<WorkWithClosingFileTestContext>(_configuration, BeforeEachTest, AfterEachTest))
            {
                var lenderVendorName = $"Lender{new Random().Next(1000, 50000)}";
                var underwrittenVendorName = $"Lender{new Random().Next(1000, 50000)}";
                scope.HelperSet.HelperMethodAddVendors(lenderVendorName, underwrittenVendorName);
                var workFlownav = new WorkFlowPageNavigation(scope.Driver);
                var vendors = new VendorsPage(scope.Driver);
                var closingRep = new ClosingFilesRepository(scope.DbConnection);
                var closingFileVendorRepository = new ClosingFileVendorRepository(scope.DbConnection);
                var closinfFileId = closingRep.GetClosingFileIdByClosingNumber(scope.Context.ClosingFileNumber);

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
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
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
            using (var scope = new TestScope<WorkWithClosingFileTestContext>(_configuration, BeforeEachTest, AfterEachTest))
            {
                scope.HelperSet.HelperMethodAddWorkflow();

                var workFlownav = new WorkFlowPageNavigation(scope.Driver);
                var workflow = new WorkflowPage(scope.Driver);
                var notificationText = DateTime.Now.ToString();
                var closingFileWorkflowItemNotificationsRepository = new ClosingFileWorkflowRepository(scope.DbConnection);

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
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        [Obsolete]
        public void AddBuyerAddressTest()
        {
            Console.WriteLine("AddBuyerAddressTest");
            using (var scope = new TestScope<WorkWithClosingFileTestContext>(_configuration, BeforeEachTest, AfterEachTest))
            {
                var userRep = new UserRepository(scope.DbConnection);
                var closingFileRep = new ClosingFilesRepository(scope.DbConnection);
                var workFlowPage = new WorkFlowPageNavigation(scope.Driver);
                var buyerPage = new BuyerPage(scope.Driver);
                var memberRep = new UserRepository(scope.DbConnection);
                var appMemberRep = new AppMemberRepository(scope.DbConnection);

                var fName = $"fname{new Random().Next(1000, 50000)}";
                var lName = $"lname{new Random().Next(1000, 50000)}";

                appMemberRep.AddNewAppMember(fName, lName);

                var closingFileId = closingFileRep.GetClosingFileId(scope.Context.ClosingFileNumber);

                scope.HelperSet.HelperMethodAddWorkflow();

                closingFileRep.AddSellerBuyerToClosingFile(closingFileId, scope.UserId);
                var closingFile = closingFileRep.SelectClosingFileWithBuyer(scope.Context.ClosingFileNumber, scope.UserId);
                var closingFileNumber = closingFile.ClosingNumber;
                var buyerFName = closingFile.FirstName;
                var buyerLName = closingFile.LastName;
                scope.Driver.Manage().Window.Maximize();
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
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        public void AddBuyerToClosingFile()
        {
            using (var scope = new TestScope<WorkWithClosingFileTestContext>(_configuration, BeforeEachTest, AfterEachTest))
            {
                var workFlowPage = new WorkFlowPageNavigation(scope.Driver);
                var buyerPage = new BuyerPage(scope.Driver);
                var appMembersRep = new AppMemberRepository(scope.DbConnection);
                
                scope.HelperSet.HelperMethodAddWorkflow();

                var firstName = scope.Configuration.GetSection("BuyerFirstName").Value;
                var lastName = scope.Configuration.GetSection("BuyerLastName").Value;

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
                    .SelectTenancy();

                Assert.AreEqual(lastName, appMembersRep.GetSellerLastName(firstName), "Buyer Was created");
            }
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
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
            using (var scope = new TestScope<WorkWithClosingFileTestContext>(_configuration, BeforeEachTest, AfterEachTest))
            {
                scope.HelperSet.HelperMethodAddWorkflow();

                var workFlownav = new WorkFlowPageNavigation(scope.Driver);
                var workflow = new WorkflowPage(scope.Driver);
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
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
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
            using (var scope = new TestScope<WorkWithClosingFileTestContext>(_configuration, BeforeEachTest, AfterEachTest))
            {
                scope.HelperSet.HelperMethodAddWorkflow();

                var headerMenu = new HeaderManu(scope.Driver);
                var login = new LoginPage(scope.Driver, scope.Configuration);
                var closingFileRep = new ClosingFilesRepository(scope.DbConnection);
                headerMenu
                    .ClickAddCircleButton()
                    .ClickClosingButton()
                    .TypeFilenumberField(scope.Context.ClosingFileNumber)
                    .ChooseTransactionTypeValue()
                    .ChooseFinanceTypeValue()
                    .ChooseUsageTypeValue()
                    .ChoosePropertyTypeValue()
                    .ClickNextButton();
                var dbFileNumber = closingFileRep.GetClosingNumberList(scope.Context.ClosingFileNumber);

                Assert.IsTrue(dbFileNumber.Contains(scope.Context.ClosingFileNumber), "New ClosingFile is created");
            }
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        [Ignore("Need to fix Assert")]
        [NUnit.Framework.Description(@"

                ")]
        public void AddImportantDatesTest()
        {
            using (var scope = new TestScope<WorkWithClosingFileTestContext>(_configuration, BeforeEachTest, AfterEachTest))
            {
                var workFlowPage = new WorkFlowPageNavigation(scope.Driver);
                var importantDatesPage = new ImportantDatesPage(scope.Driver);
                var closingRep = new ClosingFilesRepository(scope.DbConnection);

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

                var closingDate = closingRep.GetClosingFileDate(scope.Context.ClosingFileNumber, scope.UserEmail);

                Assert.AreEqual(dtClosing, closingDate, "Date is edited");

            }
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        public void AddMoneyTest()
        {
            using (var scope = new TestScope<WorkWithClosingFileTestContext>(_configuration, BeforeEachTest, AfterEachTest))
            {
                scope.HelperSet.HelperMethodAddWorkflow();

                var workFlowPage = new WorkFlowPageNavigation(scope.Driver);
                var moneyPage = new MoneyPage(scope.Driver);
                var summarypage = new SummaryPage(scope.Driver);
                var closingRep = new ClosingFilesRepository(scope.DbConnection);
                var closingFileEscrowDepRep = new ClosingFileEscrowDepositsRepository(scope.DbConnection);

                scope.HelperSet.AddBankAndBankAccounts();

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
                var wait = new WebDriverWait(scope.Driver, TimeSpan.FromSeconds(10));

                wait.Until((Driver) => closingRep.GetPurchasePrice(scope.Context.ClosingFileNumber) == eRexpPrice);

                decimal expPurchPrice = closingRep.GetPurchasePrice(scope.Context.ClosingFileNumber);
                decimal expEscrDeposit = closingFileEscrowDepRep.GetEscrowDeposit(scope.Context.ClosingFileNumber);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(eRexpPrice, expPurchPrice, "Purchase Price is changed");
                    Assert.AreEqual(erEscrowDeposit, expEscrDeposit, "Escrow Price is changed");
                });
            }
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
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
            using (var scope = new TestScope<WorkWithClosingFileTestContext>(_configuration, BeforeEachTest, AfterEachTest))
            {
                scope.HelperSet.HelperMethodAddWorkflow();

                var workFlownav = new WorkFlowPageNavigation(scope.Driver);
                var notes = new NotesPage(scope.Driver);
                var closingRep = new ClosingFilesRepository(scope.DbConnection);
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
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        [Ignore("NEED TO ADD ASSERTS")]
        public void AddParticipants()
        {
            using (var scope = new TestScope<WorkWithClosingFileTestContext>(_configuration, BeforeEachTest, AfterEachTest))
            {
                var workFlowPage = new WorkFlowPageNavigation(scope.Driver);
                var participantPage = new ParticipantsPage(scope.Driver);

                var firstName = $"AutoTest{DateTime.Now.ToString("ddmmyy")}";
                var lastName = $"AutoTest{DateTime.Now.ToString("ddmmyy")}";

                workFlowPage
                    .GoToWorkFlowPage(/*scope.Configuration.GetSection("NewUserEmail").Value, scope.Configuration.GetSection("NewUserPassword").Value*/)
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
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        //[Ignore("Need to fix Assert")]
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
            using (var scope = new TestScope<WorkWithClosingFileTestContext>(_configuration, BeforeEachTest, AfterEachTest))
            {
                scope.HelperSet.DisplayWidget("Property Maps");

                var workFlownav = new WorkFlowPageNavigation(scope.Driver);
                var property = new PropertyPage(scope.Driver);
                var summaryPage = new SummaryPage(scope.Driver);

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
                scope.Driver.Navigate().Refresh();

                var addresslocation = summaryPage.GetLocation();
                Assert.IsTrue(addresslocation.Contains("Hillsboro"));
            }
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        public void AddSellerToClosingFile()
        {
            using (var scope = new TestScope<WorkWithClosingFileTestContext>(_configuration, BeforeEachTest, AfterEachTest))
            {
                scope.HelperSet.HelperMethodAddWorkflow();

                var workFlowPage = new WorkFlowPageNavigation(scope.Driver);
                var sellerPage = new SellersPage(scope.Driver);
                var appMembersRep = new AppMemberRepository(scope.DbConnection);
                var closingRep = new ClosingFilesRepository(scope.DbConnection);

                var closinfFileName = closingRep.GetClosingNumberListWithoutBuyerSeller();

                workFlowPage
                    .GoToWorkFlowPage()
                    .GoToAppropriateClosingFile()
                    .ClickSellerMenuItem();

                sellerPage
                    .ClickAddSellersButton()
                    .SelectSellerType()
                    .TypeFirstName(scope.Configuration.GetSection("SallerFirstName").Value)
                    .TypeLastName(scope.Configuration.GetSection("SellerLastName").Value)
                    .TypeEmail(scope.Configuration.GetSection("SellerEmail").Value)
                    .SelectGender()
                    .SelectMaritalStatus()
                    .SelectExemptField();

                Assert.AreEqual(scope.Configuration.GetSection("SellerLastName").Value, appMembersRep.GetSellerLastName(scope.Configuration.GetSection("SallerFirstName").Value));

                var appMemberRep = new AppMemberRepository(scope.DbConnection);
                appMemberRep.DeleteSellerBuyerByFirstName(scope.Configuration.GetSection("SallerFirstName").Value);
            }
        }
    }
}