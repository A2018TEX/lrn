//using Laren.E2ETests.Core.Framework.DbAccess.Repository;
//using Laren.E2ETests.Core.Framework.Pages;
//using Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock;
//using Laren.E2ETests.Core.Models;
//using Microsoft.Extensions.Configuration;
//using NUnit.Framework;
//using OpenQA.Selenium.Interactions;
//using OpenQA.Selenium.Remote;
//using System;
//using System.Net;
//using System.Threading;

//namespace Laren.E2ETests.Tests
//{
//    public class WorkWithDocumentsTEstsContext
//    {
//        public string ClosingFileNumber;
//        public WorkWithDocumentsTEstsContext()
//        {
//            ClosingFileNumber = $"CF{new Random().Next(1000, 5000)}";
//        }

//        public WorkWithDocumentsTEstsContext(string closingFileNumber)
//        {
//            ClosingFileNumber = closingFileNumber;
//        }
//    }

//    public class WorkWithDocumentsTEsts 
//    {
//        private readonly IConfiguration _configuration;

//        public WorkWithDocumentsTEsts()
//        {
//            _configuration = new ConfigurationFactory().CreateInstance();
//        }

//        public void BeforeEachTest(TestScope<WorkWithDocumentsTEstsContext> testScope)
//        {
//            var rand = new Random();
//            var rndValue = rand.Next(1000, 50000);
//            var tempUser = new User()
//            {
//                Email = $"email{rndValue}@gmail.com",
//                Password = "QWE123!!"
//            };

//            var id = testScope.HelperSet.CreateNewUser(tempUser.Email);
//            testScope.HelperSet.InsertClosingFileToDB(tempUser.Email, testScope.Context.ClosingFileNumber);
//            testScope.HelperSet.HelperMethodAddWorkflow();
//            testScope.HelperSet.HelperMethodAddClosingFileAddressAndLocation();
//            testScope.UserEmail = tempUser.Email;
//            testScope.UserId = id;
//        }

//        public void AfterEachTest(TestScope<WorkWithDocumentsTEstsContext> testScope)
//        {
//            var userRepository = new UserRepository(testScope.DbConnection);
//            userRepository.DeleteDataAfterEachTest(testScope.UserEmail);
//            testScope.HelperSet.DeleteAllTestData(testScope.UserEmail);
//            testScope.HelperSet.DeleteDataAfterWorkflowNotificationTest(testScope.UserEmail);
//            testScope.Driver.Close();
//            testScope.Driver.Quit();
//            testScope.DbConnection.CloseConnection();
//        }

//        [Test]
//        [Parallelizable(ParallelScope.Self)]
//        [Description(@"
//                Title: Add documents to a closing file from the Library where we choose docx file 
//                    1. Login to the website
//                    2. Go to 'Workflow'
//                    3. From drop-down list which begin with 'Property' choose Documents - Library
//                    4. Choose .docx file
//                    5. Press the button 'Preview'
//                    6. Press the button 'Use'
//                    7. Press the button 'Use'(We mightn’t rename file)
//                    8. Open the page 'Draft'
//                    ER: The document is Active on the page 'Draft'")]
//        [Obsolete]
//        public void AddDocxToClosingFile()
//        {
//            using (var scope = new TestScope<WorkWithDocumentsTEstsContext>(_configuration, BeforeEachTest, AfterEachTest))
//            {
//                scope.HelperSet.UploadFileForTest(scope.Configuration.GetSection("DocxFileName").Value);
//                var workFlownav = new WorkFlowPageNavigation(scope.Driver);
//                var library = new LibraryPage(scope.Driver);
//                var draftPage = new DraftPage(scope.Driver);
//                string fileName = scope.Configuration.GetSection("DocxFileName").Value;
//                string fileNameWithoutFormar = fileName.Replace(".docx", "");
//                string fileNameForRenaming = $"{new Random().Next(1000, 5000)}";

//                workFlownav
//                    .GoToAppropriateClosingFile()
//                    .GoToDocumentsLibraryPage();

//                library
//                    .ClickOnDocxFile(fileNameWithoutFormar)
//                    .ClickOnUseButtonOnDocumentMenu(fileNameWithoutFormar)
//                    .RenameFile(fileNameForRenaming);

//                library
//                    .ClickOnUseButtonOnPopUp();
//                    //.WaitMessage();

//                workFlownav
//                    .GoToDocumentsDraftPage();

//                Assert.IsTrue(draftPage.FindUploadedFile(fileNameForRenaming), "Docx file is not added to drafts");
//            }
//        }
//        [Test]
//        [Parallelizable(ParallelScope.Self)]
//        [Ignore("PDF file can not be added to draft. Bug should to be fixed")]
//        [NUnit.Framework.Description(@"
//                Title: Add documents to a closing file from the Library where we choose docx file 
//                    1. Login to the website
//                    2. Go to 'Workflow'
//                    3. From drop-down list which begin with 'Property' choose Documents - Library
//                    4. Choose .pdf file
//                    5. Press the button 'Preview'
//                    6. Press the button 'Use'
//                    7. Press the button 'Use'(We mightn’t rename file)
//                    8. Open the page 'Draft'
//                    ER: The document is Active on the page 'Draft'")]
//        [Obsolete]
//        public void AddPdfToClosingFile()
//        {
//            using (var scope = new TestScope<WorkWithDocumentsTEstsContext>(_configuration, BeforeEachTest, AfterEachTest))
//            {

//                scope.HelperSet.UploadFileForTest(scope.Configuration.GetSection("PdfFileName").Value);
//                var workFlownav = new WorkFlowPageNavigation(scope.Driver);
//                var library = new LibraryPage(scope.Driver);
//                var draftPage = new DraftPage(scope.Driver);
//                string fileName = scope.Configuration.GetSection("PdfFileName").Value;
//                string fileNameWithoutFormar = fileName.Replace(".docx", "");
//                string fileNameForRenaming = $"fileNameWithoutFormar{new Random().Next(1000, 5000)}";

//                workFlownav
//                    .GoToAppropriateClosingFile()
//                    .GoToDocumentsLibraryPage();

//                library
//                    .ClickOnDocxFile(fileNameWithoutFormar)
//                    .ClickOnUseButtonOnDocumentMenu(fileNameWithoutFormar)
//                    .RenameFile(fileNameForRenaming)
//                    .ClickOnUseButtonOnPopUp()
//                    .WaitMessage();

//                workFlownav
//                    .GoToDocumentsDraftPage();

//                Assert.IsTrue(draftPage.FindUploadedFile(fileNameForRenaming), "Docx file is not added to drafts");
//            }
//        }
//        [Test]
//        [Parallelizable(ParallelScope.Self)]
//        [NUnit.Framework.Description(@"
//                Title: Edit Documents in Drafts and Finalize document which we choose all packet
//                    1. Login to the website
//                    2. Go to 'Workflow'
//                    3.From drop-down list which begin with 'Property' choose Documents - Library
//                    4. Choose .docx file
//                    5. Press the button 'Preview'
//                    6. Press the button 'Use'
//                    7. Press the button 'Use'(We mightn’t rename file)
//                    8. Open the page 'Draft'
//                    9. Press the button 'Vertical 3 points' near name file
//                    10. Choose 'Edit'
//                    11. Press the button 'Finalize'
//                    12. Choose all check-box(Buyer Packet, Seller Packe, Lender Packet)
//                    13. Go to 'Final' page
//                    ER: All packet which we choose, we can see on the 'Final' page")]
//        public void EditAndFinalizeDocumentOnDraftPage()
//        {
//            using (var scope = new TestScope<WorkWithDocumentsTEstsContext>(_configuration, BeforeEachTest, AfterEachTest))
//            {
//                scope.HelperSet.AddFileToDraft(scope.Configuration.GetSection("DocxFileName2").Value);

//                var finalPage = new FinalPage(scope.Driver);
//                var workFlownav = new WorkFlowPageNavigation(scope.Driver);
//                var draftPage = new DraftPage(scope.Driver);
//                string fileName = scope.Configuration.GetSection("DocxFileName2").Value;
//                string fileNameWithoutFormar = fileName.Replace(".docx", "");

//                workFlownav
//                    .GoToAppropriateClosingFile()
//                    .GoToDocumentsDraftPage();

//                draftPage
//                    .ClicOnEllipsisVertical(fileNameWithoutFormar)
//                    .ClickOnEditItem()
//                    .ClickOnFinaliseOnDocExpanderWrapper()
//                    .ClickOnBuyerPacketCheckBox()
//                    .ClickOnSellerPacketCheckBox()
//                    .ClickOnLenderPacketCheckBox()
//                    .ClickOnFinaliseButtonOnPopUp();

//                workFlownav
//                    .GoToDocumentsFinalPage();

//                Assert.IsTrue(finalPage.FindFileInBuyerSection(fileNameWithoutFormar), "File is not finalised to Buyer Section");
//                Assert.IsTrue(finalPage.FindFileInSelerSection(fileNameWithoutFormar), "File is not finalised to Seler Section");
//                Assert.IsTrue(finalPage.FindFileInLenderSection(fileNameWithoutFormar), "File is not finalised to Lender Section");

//            }
//        }

//        [Test]
//        [Parallelizable(ParallelScope.Self)]
//        [Ignore("not finished")]
//        [NUnit.Framework.Description(@"
//                        Title:  1. Login to the website
//                                2. Go to 'Workflow'
//                                3. From drop-down list which begin with 'Property' choose Documents - Library
//                                4. Choose .docx file
//                                5. Press the button 'Preview'
//                                6. Press the button 'Use'
//                                7. Press the button 'Use'(We mightn’t rename file)
//                                8. Open the page 'Draft'
//                                9. Press the button 'Vertical 3 points' near name file
//                                10. Choose 'Edit'
//                                11. Change text into file
//                                12. Press the button save
//                          ER: Text which we change into file is saved.")]
//        [Obsolete]
//        public void EditDocumentInDraft()
//        {
//            using (var scope = new TestScope<WorkWithDocumentsTEstsContext>(_configuration, BeforeEachTest, AfterEachTest))
//            {
//                scope.HelperSet.AddFileToDraft(scope.Configuration.GetSection("DocxFileName3").Value);
//                var actions = new Actions(scope.Driver);
//                var workFlownav = new WorkFlowPageNavigation(scope.Driver);
//                var draftPage = new DraftPage(scope.Driver);
//                string fileName = scope.Configuration.GetSection("DocxFileName3").Value;
//                string fileNameWithoutFormar = fileName.Replace(".docx", "");

//                workFlownav
//                    .GoToAppropriateClosingFile()
//                    .GoToDocumentsDraftPage();

//                draftPage
//                    .ClicOnEllipsisVertical(fileNameWithoutFormar)
//                    .ClickOnEditItem()
//                    .WaitUntilTextEditorIsWisible();


//            }
//        }

//        [Test]
//        [Parallelizable(ParallelScope.Self)]
//        [NUnit.Framework.Description(@"
//                Title: Uploading .docx file 
//                            Login to the website
//                            Go to 'Workflow'
//                            From drop-down list which begin with 'Property' choose Documents - Uploads
//                            Press the button 'Upload'
//                            From pc load .docx file
//                            Press the 'Drop items here'
//                    ER: The file should be in the Active position")]
//        [Obsolete]
//        public void UploadingDocxFile()
//        {
//            using (var scope = new TestScope<WorkWithDocumentsTEstsContext>(_configuration, BeforeEachTest, AfterEachTest))
//            {
//                var workFlownav = new WorkFlowPageNavigation(scope.Driver);
//                var uploadsPage = new UploadsPage(scope.Driver);
//                string fileName = scope.Configuration.GetSection("DocxFileName").Value;
//                string fileNameWithoutFormar = fileName.Replace(".docx", "");

//                workFlownav
//                    .GoToAppropriateClosingFile()
//                    .GoToDocumentsUploadPage();

//                uploadsPage
//                    .UploadFile(scope.Configuration.GetSection("DocxFileName").Value);

//                Assert.IsTrue(uploadsPage.FindUploadedFile(fileNameWithoutFormar), "Docx file is not uploaded");
//            }
//        }

//        [Test]
//        [Parallelizable(ParallelScope.Self)]
//        [NUnit.Framework.Description(@"
//                Title: Uploading .docx file 
//                            Login to the website
//                            Go to 'Workflow'
//                            From drop-down list which begin with 'Property' choose Documents - Uploads
//                            Press the button 'Upload'
//                            From pc load .docx file
//                            Press the 'Drop items here'
//                    ER: The file should be in the Active position")]
//        public void UploadingPdfFile()
//        {
//            using (var scope = new TestScope<WorkWithDocumentsTEstsContext>(_configuration, BeforeEachTest, AfterEachTest))
//            {
//                var workFlownav = new WorkFlowPageNavigation(scope.Driver);

//                var uploadsPage = new UploadsPage(scope.Driver);
//                string fileName = scope.Configuration.GetSection("PdfFileName").Value;
//                string fileNameWithoutFormar = fileName.Replace(".pdf", "");

//                workFlownav
//                    .GoToAppropriateClosingFile()
//                    .GoToDocumentsUploadPage();

//                uploadsPage
//                    .UploadFile(fileName);

//                Assert.IsTrue(uploadsPage.FindUploadedFile(fileNameWithoutFormar), "Docx file is not uploaded");
//            }
//        }
//        [Test]
//        [Parallelizable(ParallelScope.Self)]
//        [NUnit.Framework.Description(@"
//                Title: 1. Login to the website
//                        2. Go to 'Workflow'
//                        3. From drop-down list which begin with 'Property' choose Documents - Uploads
//                        4. Press the icon 'Drop items here'
//                        5. From pc load .docx or pdf files
//                        6. Press the 'Drop items here'
//                        7. Press the button 'Vertical 3 points' near name file
//                        8. Choose the button 'Rename'
//                        9. Write another name
//                    ER: Name should be changed")]
//        public void RenameOfUploadedFile()
//        {
//            using (var scope = new TestScope<WorkWithDocumentsTEstsContext>(_configuration, BeforeEachTest, AfterEachTest))
//            {
//                var workFlownav = new WorkFlowPageNavigation(scope.Driver);
//                var closingFileDocumentsRepository = new ClosingFileDocumentsRepository(scope.DbConnection);
//                var uploadsPage = new UploadsPage(scope.Driver);
//                string fileName = scope.Configuration.GetSection("PdfFileName").Value;
//                string fileNameWithoutFormar = fileName.Replace(".pdf", "");
//                string fileNameAfterRemaming = $"{fileNameWithoutFormar}{DateTime.Now.ToString("hhmmss")}";

//                workFlownav
//                    .GoToAppropriateClosingFile()
//                    .GoToDocumentsUploadPage();

//                uploadsPage
//                    .UploadFile(fileName)
//                    .ClickOnEllipsisVertical(fileNameWithoutFormar)
//                    .ClickOnRenameButton()
//                    .TypeNewFileName(fileNameAfterRemaming)
//                    .ClickOnRenameButtonOnPopup();

//                Assert.IsNotNull(closingFileDocumentsRepository.CheckThatDocumentIsPresent(fileNameAfterRemaming), "Document is not renamed");
//            }
//        }


//        [Test]
//        [Parallelizable(ParallelScope.Self)]
//        [NUnit.Framework.Description(@"
//                Title: 1. Press the button 'Rename'
//                        2. Go to 'Workflow'
//                        3. From drop-down list which begin with 'Property' choose Documents - Uploads
//                        4. Press the icon 'Drop items here'
//                        5. From pc load .docx or pdf files
//                        6. Press the 'Drop items here'
//                        7. Press the button 'Vertical 3 points' near name file
//                        8. Choose the button 'Finalize'
//                        9. Choos all check-box(Buyer Packet, Seller Packe, Lender Packet)
//                        10. Go to 'Final' page
//                    ER: This file which we choose, we can see on the 'Final' page")]
//        [Obsolete]
//        public void FinliseOfUploadedFile()
//        {
//            using (var scope = new TestScope<WorkWithDocumentsTEstsContext>(_configuration, BeforeEachTest, AfterEachTest))
//            {
//                var workFlownav = new WorkFlowPageNavigation(scope.Driver);
//                var finalPage = new FinalPage(scope.Driver);
//                var closingFileDocumentsRepository = new ClosingFileDocumentsRepository(scope.DbConnection);
//                var uploadsPage = new UploadsPage(scope.Driver);
//                string fileName = scope.Configuration.GetSection("PdfFileName").Value;
//                string fileNameWithoutFormar = fileName.Replace(".pdf", "");
//                string fileNameAfterRemaming = fileNameWithoutFormar + DateTime.Now.ToString();

//                workFlownav
//                    .GoToAppropriateClosingFile()
//                    .GoToDocumentsUploadPage();

//                uploadsPage
//                    .UploadFile(fileName)
//                    .ClickOnEllipsisVertical(fileNameWithoutFormar)
//                    .ClickOnFinaliseButtonOnDropdown()
//                    .ClickOnBuyerPacketCheckBox()
//                    .ClickOnSellerPacketCheckBox()
//                    .ClickOnLenderPacketCheckBox()
//                    .ClickOnFinaliseButtonOnPopUp()
//                    .WaitConfirmatiomMessage();

//                workFlownav
//                    .GoToDocumentsFinalPage();

//                Assert.IsTrue(finalPage.FindFileInBuyerSection(fileNameWithoutFormar), "File is not finalised to Buyer Section");
//                Assert.IsTrue(finalPage.FindFileInSelerSection(fileNameWithoutFormar), "File is not finalised to Seler Section");
//                Assert.IsTrue(finalPage.FindFileInLenderSection(fileNameWithoutFormar), "File is not finalised to Lender Section");
//            }
//        }

//        [Test]
//        [Parallelizable(ParallelScope.Self)]
//        [NUnit.Framework.Description(@"
//                Title:  1. Login to the website
//                        2. Go to 'Workflow'
//                        3. From drop-down list which begin with 'Property' choose Documents - Uploads
//                        4. Press the icon 'Drop items here'
//                        5. From pc load .docx or pdf files
//                        6. Press the 'Drop items here'
//                        7. Press the button 'Vertical 3 points' near name file
//                        8. Choose the button 'Archive'
//                    ER: The file is transferred to the Archive section")]
//        [Obsolete]
//        public void ArchiveOfUploadedFile()
//        {
//            using (var scope = new TestScope<WorkWithDocumentsTEstsContext>(_configuration, BeforeEachTest, AfterEachTest))
//            {
//                var workFlownav = new WorkFlowPageNavigation(scope.Driver);
//                var uploadsPage = new UploadsPage(scope.Driver);
//                var closingFileDocumentsRepository = new ClosingFileDocumentsRepository(scope.DbConnection);
//                string fileName = scope.Configuration.GetSection("PdfFileName").Value;
//                string fileNameWithoutFormar = fileName.Replace(".pdf", "");

//                workFlownav
//                    .GoToAppropriateClosingFile()
//                    .GoToDocumentsUploadPage();

//                uploadsPage
//                    .UploadFile(fileName)
//                    .ClickOnEllipsisVertical(fileNameWithoutFormar)
//                    .ClickOnArchiveButton();

//                Assert.AreEqual("1", closingFileDocumentsRepository.GetClosingFileDocumentStatus(fileNameWithoutFormar, scope.HelperSet.ClosingFileId), "Document is not archived");
//            }
//        }

//        [Test]
//        [Parallelizable(ParallelScope.Self)]
//        [NUnit.Framework.Description(@"
//                Title:  1. Login to the website
//                        2. Go to 'Workflow'
//                        3. From drop-down list which begin with 'Property' choose Documents - Uploads
//                        4. Press the icon 'Drop items here'
//                        5. From pc load .docx or pdf files
//                        6. Press the 'Drop items here'
//                        7. Press the button 'Vertical 3 points' near name file
//                        8. Choose the button 'Archive'
//                    ER: The file is transferred to the Archive section")]
//        public void UnArchiveOfUploadedAtchivedFile()
//        {
//            using (var scope = new TestScope<WorkWithDocumentsTEstsContext>(_configuration, BeforeEachTest, AfterEachTest))
//            {
//                var workFlownav = new WorkFlowPageNavigation(scope.Driver);
//                var uploadsPage = new UploadsPage(scope.Driver);
//                var closingFileDocumentsRepository = new ClosingFileDocumentsRepository(scope.DbConnection);
//                string fileName = scope.Configuration.GetSection("PdfFileName").Value;
//                string fileNameWithoutFormar = fileName.Replace(".pdf", "");

//                workFlownav
//                    .GoToAppropriateClosingFile()
//                    .GoToDocumentsUploadPage();

//                uploadsPage
//                    .UploadFile(fileName);

//                closingFileDocumentsRepository
//                    .SetArchivedClosingFileDocumentStatus(fileName, scope.HelperSet.ClosingFileId);

//                scope.Driver.Navigate().Refresh();

//                workFlownav
//                    .GoToDocumentsUploadPage();

//                uploadsPage
//                    .ClickOnEllipsisVertical(fileNameWithoutFormar)
//                    .ClickOnUnArchiveButton();

//                Assert.AreEqual("0", closingFileDocumentsRepository.GetClosingFileDocumentStatus(fileNameWithoutFormar, scope.HelperSet.ClosingFileId), "Document is not un-archived");
//            }
//        }

//        [Test]
//        [Parallelizable(ParallelScope.Self)]
//        [NUnit.Framework.Description(@"
//                Title: Edit and Clone Documents in Drafts
//                        1. Login to the website
//                        2. Go to 'Workflow'
//                        3. From drop-down list which begin with 'Property' choose Documents - Library
//                        4. Choose .docx file
//                        5. Press the button 'Preview'
//                        6. Press the button 'Use'
//                        7. Press the button 'Use'(We mightn’t rename file)
//                        8. Open the page 'Draft'
//                        9. Press the button 'Vertical 3 points' near name file
//                        10. Choose 'Edit'
//                        11. Press the button 'Clone'
//                        12. Write name for the clone file
//                        13. Click on clone button
//                    ER: The file should be add to the column that is on the left")]
//        public void EditAndCloneDocumentOnDraftPage()
//        {
//            using (var scope = new TestScope<WorkWithDocumentsTEstsContext>(_configuration, BeforeEachTest, AfterEachTest))
//            {
//                scope.HelperSet.AddFileToDraft(scope.Configuration.GetSection("DocxFileName3").Value);

//                var workFlownav = new WorkFlowPageNavigation(scope.Driver);
//                var draftPage = new DraftPage(scope.Driver);
//                string fileName = scope.Configuration.GetSection("DocxFileName3").Value;
//                string fileNameWithoutFormar = fileName.Replace(".docx", "");
//                string fileNameForClone = DateTime.Now.ToString("ss");

//                workFlownav
//                    .GoToAppropriateClosingFile()
//                    .GoToDocumentsDraftPage();

//                draftPage
//                    .ClicOnEllipsisVertical(fileNameWithoutFormar)
//                    .ClickOnEditItem()
//                    .ClickOnCloneButton()
//                    .TypeClonedFileName(fileNameForClone)
//                    .ClickOnCloneButtonOnPopup();

//                Assert.IsTrue(draftPage.FindFileInActiveSection(fileNameForClone), "File is not Cloned");
//                Assert.IsTrue(draftPage.FindFileInActiveSection(fileNameWithoutFormar), "Previous file disappeared");
//            }
//        }
//        [Test]
//        [Parallelizable(ParallelScope.Self)]
//        [NUnit.Framework.Description(@"
//                Title:  1. Login to the website
//                        2. Go to 'Workflow'
//                        3. From drop-down list which begin with 'Property' choose Documents - Uploads
//                        4. Press the icon 'Drop items here'
//                        5. From pc load .docx or pdf files
//                        6. Press the 'Drop items here'
//                        7. Press the button 'Vertical 3 points' near name file
//                        8. Choose the button 'Archive'
//                        9. Press the button 'Vertical 3 points' near name file
//                        10. Choose the button 'Delete
//                    ER: The file shoul be deleted")]
//        [Obsolete]
//        public void DeleteOfUploadedAtchivedFile()
//        {
//            using (var scope = new TestScope<WorkWithDocumentsTEstsContext>(_configuration, BeforeEachTest, AfterEachTest))
//            {
//                var workFlownav = new WorkFlowPageNavigation(scope.Driver);
//                var uploadsPage = new UploadsPage(scope.Driver);
//                var closingFileDocumentsRepository = new ClosingFileDocumentsRepository(scope.DbConnection);
//                string fileName = scope.Configuration.GetSection("PdfFileName").Value;
//                string fileNameWithoutFormar = fileName.Replace(".pdf", "");

//                workFlownav
//                    .GoToAppropriateClosingFile()
//                    .GoToDocumentsUploadPage();

//                uploadsPage
//                    .UploadFile(fileName);

//                closingFileDocumentsRepository
//                    .SetArchivedClosingFileDocumentStatus(fileName, scope.HelperSet.ClosingFileId);

//                scope.Driver.Navigate().Refresh();

//                workFlownav
//                    .GoToDocumentsUploadPage();

//                uploadsPage
//                    .ClickOnEllipsisVertical(fileNameWithoutFormar)
//                    .ClickOnDeleteButton()
//                    .ClickOnOkButtonOnDeleteItemPopup()
//                    .WainUntilDocumentIsDisappearedFromArchiveSection(fileNameWithoutFormar);

//                Assert.IsNull(closingFileDocumentsRepository.GetClosingFileDocumentId(fileNameWithoutFormar, scope.HelperSet.ClosingFileId), "Document is not deleted");
//            }
//        }

//        [Test]
//        [Parallelizable(ParallelScope.Self)]
//        [NUnit.Framework.Description(@"
//                Title: Edit and Clone Documents in Drafts
//                        1. Login to the website
//                        2. Go to 'Workflow'
//                        3. From drop-down list which begin with 'Property' choose Documents - Library
//                        4. Choose .docx file
//                        5. Press the button 'Preview'
//                        6. Press the button 'Use'
//                        7. Press the button 'Use'(We mightn’t rename file)
//                        8. Open the page 'Draft'
//                        9. Press the button 'Vertical 3 points' near name file
//                        10. Choose 'Edit'
//                        11. Press the button 'Clone'
//                        12. Write name for the clone file
//                        13. Click on clone button
//                    ER: The file should be add to the column that is on the left")]
//        public void EditAndPrintDocumentOnDraftPage()
//        {
//            using (var scope = new TestScope<WorkWithDocumentsTEstsContext>(_configuration, BeforeEachTest, AfterEachTest))
//            {
//                scope.HelperSet.AddFileToDraft(scope.Configuration.GetSection("DocxFileName3").Value);

//                var workFlownav = new WorkFlowPageNavigation(scope.Driver);
//                var draftPage = new DraftPage(scope.Driver);
//                var printPage = new PrintPage(scope.Driver);
//                string fileName = scope.Configuration.GetSection("DocxFileName3").Value;
//                string fileNameWithoutFormar = fileName.Replace(".docx", "");

//                workFlownav
//                    .GoToAppropriateClosingFile()
//                    .GoToDocumentsDraftPage();

//                draftPage
//                    .ClicOnEllipsisVertical(fileNameWithoutFormar)
//                    .ClickOnEditItem()
//                    .ClickOnPrintButtonOnActionRow();

//                Assert.IsTrue(printPage.PrintPageIsOpened(), "Print Page is not opened");

//            }
//        }

//        [Test]
//        [Parallelizable(ParallelScope.Self)]
//        [NUnit.Framework.Description(@"
//                Title: Edit and Export Documents in Drafts
//                        1. Login to the website
//                        2. Go to 'Workflow'
//                        3. From drop-down list which begin with 'Property' choose Documents - Library
//                        4. Choose .docx file
//                        5. Press the button 'Preview'
//                        6. Press the button 'Use'
//                        7. Press the button 'Use'(We mightn’t rename file)
//                        8. Open the page 'Draft'
//                        9. Press the button 'Vertical 3 points' near name file
//                        10. Choose 'Edit'
//                        11. Press the button 'Export'
//                    ER: The file must be load on the pc")]
//        public void EditAndExportDocumentOnDraftPage()
//        {
//            using (var scope = new TestScope<WorkWithDocumentsTEstsContext>(_configuration, BeforeEachTest, AfterEachTest))
//            {
//                var fileName1 = scope.HelperSet.AddFileToDraft(scope.Configuration.GetSection("DocxFileName2").Value);

//                var sessionIdProperty = typeof(RemoteWebDriver).GetProperty("SessionId");
//                var sessionId = sessionIdProperty.GetValue(scope.Driver);

//                var workFlownav = new WorkFlowPageNavigation(scope.Driver);
//                var draftPage = new DraftPage(scope.Driver);
//                var printPage = new PrintPage(scope.Driver);

//                workFlownav
//                    .GoToAppropriateClosingFile()
//                    .GoToDocumentsDraftPage();

//                draftPage
//                    .ClicOnEllipsisVertical(fileName1.Replace(".docx", ""))
//                    .ClickOnEditItem()
//                    .ClickOnExportButtonButtonOnActionRow();

//                Thread.Sleep(1000);
//                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://195.26.92.83:4444/download/{sessionId}/{fileName1}"); /*/{fileName1}*/
//                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
//                var status = response.StatusCode;

//                Assert.AreEqual("OK", $"{status}", "File is downloaded");

//            }
//        }
//    }
//}
