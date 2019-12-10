using Laren.E2ETests.Core.Framework.DbAccess;
using Laren.E2ETests.Core.Framework.DbAccess.Repository;
using Laren.E2ETests.Core.Framework.Pages;
using Laren.E2ETests.Core.Framework.Pages.ClosingFilesBlock;
using Laren.E2ETests.Core.Pages;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.IO;
using System.Threading;

namespace Laren.E2ETests.Core.Framework
{
    public class HelperSetUp
    {
        private IConfiguration _configuration;

        public DataBaseConnection _dbConn;

        protected IWebDriver _driver;
        //TODO: Clear all properties
        private int UserId { get; set; }
        private int CompanyId { get; set; }
        public int ClosingFileId { get; set; }
        private int AddressId { get; set; }
        public int LocationId { get; set; }
        public HelperSetUp(IWebDriver driver, DataBaseConnection dbConn, IConfiguration configuration)
        {
            this._driver = driver;
            this._dbConn = dbConn;
            _configuration = configuration;
        }

        public int InsertClosingFileToDB(string userMail, string closingFile)
        {
            var userRepository = new UserRepository(_dbConn);
            var closingFileRepository = new ClosingFilesRepository(_dbConn);

            UserId = userRepository.GetUserId(userMail);
            CompanyId = userRepository.GetCompanyIdFromMemberCompanies(UserId);
            closingFileRepository.InsertClosingFile(CompanyId, UserId, closingFile);
            ClosingFileId = closingFileRepository.GetClosingFileId(closingFile);
            return ClosingFileId;
        }


        public void GetNeededData(string email, string closingFileNumber)
        {
            var userRepository = new UserRepository(_dbConn);
            var closingFileRepository = new ClosingFilesRepository(_dbConn);

            UserId = userRepository.GetUserId(email);
            CompanyId = userRepository.GetCompanyIdFromMemberCompanies(UserId);
            ClosingFileId = closingFileRepository.GetClosingFileId(closingFileNumber);
        }
        public void AddAllTestData(string userEmail)
        {
            CreateNewUser(userEmail);
        }

        public void DeleteAllTestData(string userMail)
        {
            var memberRep = new MembersRepository(_dbConn);
            DeleteNewUserFromDB(userMail);
        }

        public int CreateNewUser(string userEmail)
        {

            var userVerifyPage = new UserVerifyPage(_driver, _configuration);
            var userRepository = new UserRepository(_dbConn);
            var uniqueidentifier = Guid.NewGuid();

            userRepository
                .InsertUserIntoDB(userEmail, uniqueidentifier);
            userVerifyPage
                .GoToUserVerifyPage(uniqueidentifier)
                .TypePassword(_configuration.GetSection("NewUserPassword").Value)
                .EnterConfirmPassword(_configuration.GetSection("NewUserPassword").Value)
                .ClickSignInButton();
            return userRepository.GetUserId(userEmail);
        }

        public void DeleteDataAfterTestAddWorkflow(string userEmail)
        {
            var closingFileWorkflowRepository = new ClosingFileWorkflowRepository(_dbConn);
            closingFileWorkflowRepository.DeleteDataAfterTestAddWorkflow(userEmail);
        }

        public void DeleteNewUserFromDB(string userMail)
        {
            var userRepository = new UserRepository(_dbConn);
            userRepository.DeleteUser(userMail);
        }

        public void DeleteDataAfterWorkflowNotificationTest(string userEmail)
        {
            var closingFileWorkflowRepository = new ClosingFileWorkflowRepository(_dbConn);
            closingFileWorkflowRepository.DeleteDataAfterWorkflowNotificationTest(userEmail);
        }

        public void SignOut()
        {
            var haderManu = new HeaderManu(_driver);
            haderManu.SignOut();
        }

        public void AddHelperDataForWorkFlow()
        {
            var closingFileWorkflowRepository = new ClosingFileWorkflowRepository(_dbConn);

            closingFileWorkflowRepository.InsertWorkflowTemplate(CompanyId, UserId, _configuration.GetSection("WorkflowTemplateName").Value);
            var workflowTemplateId = closingFileWorkflowRepository.GetWorkflowTemplateId(_configuration.GetSection("WorkflowTemplateName").Value);
            closingFileWorkflowRepository.InsertWorkflowSection(workflowTemplateId, UserId);
            var workflowSectionId = closingFileWorkflowRepository.GetWorkflowSectionId(workflowTemplateId, UserId);
            var roleId = closingFileWorkflowRepository.GetRoleId(CompanyId);
            closingFileWorkflowRepository.InsertWorkflowItem(_configuration.GetSection("WorkflowItemName").Value, workflowSectionId, roleId, UserId);
            closingFileWorkflowRepository.InsertWorkWeek(UserId, CompanyId);
        }

        public void HelperMethodAddWorkflow()
        {
            var closingFileWorkflowRepository = new ClosingFileWorkflowRepository(_dbConn);
            var roleId = closingFileWorkflowRepository.GetRoleId(CompanyId);
            closingFileWorkflowRepository.InsertWorkflowTemplate(CompanyId, UserId, _configuration.GetSection("WorkflowTemplateName").Value);
            var workflowTemplateId = closingFileWorkflowRepository.GetWorkflowTemplateId(_configuration.GetSection("WorkflowTemplateName").Value);
            closingFileWorkflowRepository.InsertClosingFileWorkflowInstance(workflowTemplateId, CompanyId, ClosingFileId, UserId);
            var closingFileWorkflowInstanceId = closingFileWorkflowRepository.GetClosingFileWorkflowInstanceId(CompanyId, UserId);
            closingFileWorkflowRepository.InsertClosingFileWorkflowSection(closingFileWorkflowInstanceId, UserId);
            var closingFileWorkflowSectionId = closingFileWorkflowRepository.GetClosingFileWorkflowSectionId(closingFileWorkflowInstanceId, UserId);
            closingFileWorkflowRepository.InsertClosingFileWorkflowItem(closingFileWorkflowSectionId, roleId, UserId);
        }

        public void HelperMethodAddVendors(string lenderName, string underwriterVendorName)
        {
            HelperMethodAddClosingFileAddressAndLocation();
            var closingFileRepository = new ClosingFilesRepository(_dbConn);
            closingFileRepository.InsertVendor(lenderName, underwriterVendorName, UserId, CompanyId, AddressId, LocationId);

        }

        public void AddBankAndBankAccounts()
        {
            var bankRep = new BanksRepository(_dbConn);
            var bankAccRep = new BankAccountsRepository(_dbConn);
            bankRep.AddNewBank(UserId, CompanyId);
            bankAccRep.AddNewBankAccount(UserId);
        }

        public void DeleteBankAndBankAccounts(string email)
        {
            var bankRep = new BanksRepository(_dbConn);
            var closingFile = new ClosingFilesRepository(_dbConn);
            var bankAccRep = new BankAccountsRepository(_dbConn);
            closingFile.DeleteClosingFiles(email, _configuration.GetSection("ClosingFileNumber").Value);
            bankAccRep.DeleteNewBankAccount(UserId);
            bankRep.DeleteNewBank(UserId);
        }
        public void HelperMethodAddClosingFileAddressAndLocation()
        {
            var addressesRepository = new AddressesRepository(_dbConn);
            addressesRepository.InsertAdress(UserId);
            AddressId = addressesRepository.GetAddressId(UserId);
            var locationsRepository = new LocationsRepository(_dbConn);
            locationsRepository.InsertLocation(CompanyId, AddressId, UserId);
            LocationId = locationsRepository.GetLocationId(CompanyId, AddressId, UserId);
        }
        public void UploadFileForTest(string docxFileName/*, string pdfFileName*/)
        {
            var adminPage = new AdminPage(_driver, _configuration);
            adminPage
                .GoToDocumentsLibraryPage()
                .FileUpload(docxFileName/*, pdfFileName*/);
        }
        public string AddFileToDraft(string fileName/*, string pdfFileName*/)
        {
            UploadFileForTest(fileName);
            var locationsRepository = new LocationsRepository(_dbConn);
            var closingFileDocumentsRepository = new ClosingFileDocumentsRepository(_dbConn);
            //var locationId = locationsRepository.GetLocationId(CompanyId, AddressId, UserId);
            var documentTemplatesRepository = new DocumentTemplatesRepository(_dbConn);
            var documentTemplateId = documentTemplatesRepository.GetDocumentTemplateId(CompanyId, LocationId);
            var documentTemplateFileGuid = documentTemplatesRepository.GetDocumentTemplateFileGuid(CompanyId, LocationId);
            var documentTemplateFileName = documentTemplatesRepository.GetDocumentTemplateFileName(CompanyId, LocationId) + DateTime.Now.ToString();
            closingFileDocumentsRepository.InsertFileToDraft(ClosingFileId, documentTemplateId, CompanyId, LocationId, documentTemplateFileGuid, documentTemplateFileName);
            return documentTemplateFileName;
        }

        public bool FindExportedFile(string fileName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\";
            if (Directory.Exists(path)) //we check if the directory or folder exists
            {
                bool result = CheckFile(path, fileName); // boolean result true or false is stored after checking the zip file name
                if (result == true)
                {
                    return true;
                }
                else
                {
                    Assert.Fail("The file does not exist.");// if the zip file is not present, then the  test fails
                }
            }
            return false;
        }

        public bool CheckFile(string path, string name) // the name of the zip file which is obtained, is passed in this method
        {
            var currentFile = $@"{path}" + name; // the zip filename is stored in a variable

            bool fileExist = false;
            for (int i = 0; i < 100; i++)
            {
                if (File.Exists(currentFile)) //helps to check if the zip file is present
                {
                    fileExist = true; //if the zip file exists return boolean true
                    File.Delete(currentFile);
                    break;
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
            return fileExist;
        }

        public void DisplayWidget(string widgetName)
        {
            HelperMethodAddClosingFileAddressAndLocation();
            var widgetsRepository = new WidgetsRepository(_dbConn);
            var widgetsMembersRepository = new WidgetsMembersRepository(_dbConn);
            int widgetId = widgetsRepository.GetWidggetId(widgetName);
            widgetsMembersRepository.InsertWidgetsMembers(widgetId, CompanyId, LocationId, UserId);

        }
    }
}
