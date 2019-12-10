using Dapper;
using Laren.E2ETests.Core.Framework.DbAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.DbAccess.Repository
{
    public class ClosingFilesRepository
    {

        private DataBaseConnection _conn;

        public ClosingFilesRepository(DataBaseConnection connection)
        {
            _conn = connection;
        }
        public List<string> GetClosingNumberList(string closingFileName)
        {
            Thread.Sleep(100);
            var query = $@"SELECT ClosingNumber
                           FROM ClosingFiles Where ClosingNumber Like '{closingFileName}%'order by id desc";
            var closingNumber = _conn.Connection.Query<string>(query).ToList();
            return closingNumber;
        }

        public string GetClosingNumberListWithoutBuyerSeller(/*string createdBy*/)
        {
            Thread.Sleep(100);
            var query = $@" Select cf.[ClosingNumber], cfb.[Id] 
                            from [ClosingFileBuyerSellers] cfb 
                            Right Join ClosingFiles cf ON cf.[Id] = cfb.[ClosingFileId] 
                            Where cf.[CreatedBy_Id]=72 And [FileStatus] = 0  order by cf.[Id] desc";
            var closingNumber = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return closingNumber;
        }
        public string GetClosingNumber(/*string createdBy*/)
        {
            var query = $@"SELECT ClosingNumber
                           FROM [ClosingFiles] Where [CreatedBy_Id]=72 order by id desc";
            var closingNumber = _conn.Connection.QueryFirstOrDefault<string>(query);
            return closingNumber;
        }
        public void UpdateMoneyToNull(string closingNumber)
        {
            var query = $@" Update [ClosingFiles] Set [AdditionalDeposit] = NULL, [EscrowAccount] = NULL, [EscrowDeposit]= 0, [PurchasePrice]= 0, [LoanAmount]= 0
                            Where ClosingNumber = '{closingNumber}'";
            var update = _conn.Connection.QueryFirstOrDefault<string>(query);
        }

        public decimal GetPurchasePrice(string closingNumber)
        {
            Thread.Sleep(100);
            var query = $@"SELECT PurchasePrice FROM [ClosingFiles] Where ClosingNumber ='{closingNumber}' order by id desc";
            decimal purchPrice = _conn.Connection.QueryFirstOrDefault<decimal>(query);
            return purchPrice;
        }

        public ClosingFilesModel SelectClosingFile(string createdBy)
        {
            var query = $@" Select [ClosingNumber], [Id],   
                            from  ClosingFiles                           
                            Where [CreatedBy_Id]='{createdBy}' And [FileStatus] = 0";
            ClosingFilesModel closingFileName = _conn.Connection.QueryFirstOrDefault<ClosingFilesModel>(query);
            return closingFileName;

        }
        public ClosingFilesModel SelectClosingFileWithBuyer(string closingNumber, int createdBy)
        {
            var query = $@" Select cf.ClosingNumber, cfb.Id ,am.FirstName, am.LastName 
                            from ClosingFileBuyerSellers cfb 
                            Join ClosingFiles cf ON cf.[Id] = cfb.[ClosingFileId] 
                            Join AppMember am ON am.id = cfb.[AppMemberId]
                            Where cf.ClosingNumber = '{closingNumber}' And cf.CreatedBy_Id={createdBy} And cf.FileStatus = 0 And cfb.BuyerSellerEntityType = 0 And cfb.BuyerOrSellerType = 0 order by cf.Id desc";

            ClosingFilesModel closingFileName = _conn.Connection.QueryFirstOrDefault<ClosingFilesModel>(query);
            return closingFileName;

        }
        public List<string> CheckNewBuyerAddress()
        {
            var query = $@"Select cfbs.Id From [ClosingFileBuyerSellers] cfbs Join [Addresses] a ON a.Id = cfbs.[CurrentAddressId] Join ClosingFiles cf ON cf.[Id] = cfbs.[ClosingFileId] Where a.[AddressLine1] = '500 SE 8th Ave' And a.StateId = 1";
            List<string> closingFileName = _conn.Connection.Query<string>(query).ToList();
            return closingFileName;
        }

        public List<String> GetClosingFileBuyerList(string closingFileId)
        {
            Thread.Sleep(1000);
            var query = $@"Select am.id From [ClosingFileBuyerSellers] cf join AppMember am on cf.[AppMemberId]=am.Id
                            Where cf.[ClosingFileId] = {closingFileId}";
            var closingNumber = _conn.Connection.Query<string>(query).ToList();
            return closingNumber;
        }

        public int GetClosingFileId(string closingNumber)
        {
            Thread.Sleep(100);
            var query = $@"Select id From ClosingFiles 
                            Where ClosingNumber = '{closingNumber}'";
            var closing = _conn.Connection.QueryFirstOrDefault<int>(query);
            return closing;
        }

        public string GetAppMemberId(string firstName)
        {
            Thread.Sleep(100);
            var query = $@"select id from AppMember where FirstName='{firstName}'";
            var closing = _conn.Connection.QueryFirstOrDefault<string>(query);
            return closing;

        }
        public string GetAddressIdForBuyer(string firstName, string address = "SE 8th Ave")
        {
            Thread.Sleep(100);
            var query = $@"  Select id From [Addresses] where Id in(Select[CurrentAddressId] from  [ClosingFileBuyerSellers] where [AppMemberId] in(Select id from AppMember where FirstName = '{firstName}'))And AddressLine1 like '%{address}%' order by DateCreated";
            var closing = _conn.Connection.QueryFirstOrDefault<string>(query);
            return closing;
        }

        public List<string> GetAddressId(string address = "SE 8th Ave")
        {
            Thread.Sleep(100);
            var query = $@"Select [CurrentAddressId] From [ClosingFileBuyerSellers] cf Join AppMember am ON cf.AppMemberId = am.Id Join Addresses a On a.Id = cf.CurrentAddressId Where a.AddressLine1 like '%{address}' order by a.DateCreated desc";
            var closing = _conn.Connection.Query<string>(query).ToList();
            return closing;
        }
        public int GetClosingFileIdByClosingNumber(string closingNumber)
        {
            var query = $@"SELECT Id FROM ClosingFiles Where ClosingNumber = '{closingNumber}'";
            var closingFileId = _conn.Connection.QueryFirstOrDefault<int>(query);
            return closingFileId;
        }

        public async void DeleteClosingFiles(string email, string closingNumber)
        {
            var query = $@" DELETE FROM ClosingFileWorkflowItemNotifications 
                                WHERE ClosingFileWorkflowItemId IN 
                                (SELECT Id FROM ClosingFileWorkflowItems WHERE ClosingFileWorkflowSectionId IN 
                                (SELECT Id FROM ClosingFileWorkflowSections WHERE ClosingFileWorkflowInstanceId  IN 
                                (Select Id FROM ClosingFileWorkflowInstances WHERE ClosingFileId IN 
                                (SELECT Id FROM ClosingFiles WHERE ClosingNumber = '{closingNumber}'))));
                            DELETE FROM ClosingFileWorkflowItemComments 
                                 WHERE ClosingFileWorkflowItemId IN 
                                 (SELECT Id FROM ClosingFileWorkflowItems WHERE ClosingFileWorkflowSectionId IN 
                                 (SELECT Id FROM ClosingFileWorkflowSections WHERE ClosingFileWorkflowInstanceId  IN 
                                 (Select Id FROM ClosingFileWorkflowInstances WHERE ClosingFileId IN 
                                 (SELECT Id FROM ClosingFiles WHERE ClosingNumber = '{closingNumber}'))));
                            DELETE FROM ClosingFileWorkflowItems 
                                  WHERE ClosingFileWorkflowSectionId IN 
                                  (SELECT Id FROM ClosingFileWorkflowSections WHERE ClosingFileWorkflowInstanceId  IN 
                                  (Select Id FROM ClosingFileWorkflowInstances WHERE ClosingFileId IN 
                                  (SELECT Id FROM ClosingFiles WHERE ClosingNumber = '{closingNumber}')));
                            DELETE FROM ClosingFileWorkflowSections 
                                WHERE ClosingFileWorkflowInstanceId  IN 
                                (Select Id FROM ClosingFileWorkflowInstances WHERE ClosingFileId IN 
                                (SELECT Id FROM ClosingFiles WHERE ClosingNumber = '{closingNumber}'));
                            DELETE FROM ClosingFileWorkflowInstances 
                                WHERE ClosingFileId IN 
                                (SELECT Id FROM ClosingFiles WHERE ClosingNumber = '{closingNumber}');
                            
                            Delete From [CdAirs] Where ClosingDisclosureId In(Select Id From ClosingDisclosures where [ClosingFileId] In (SELECT id FROM [ClosingFiles] where [ClosingNumber] = '{closingNumber}'));
                            Delete From [CdAps] Where ClosingDisclosureId In(Select Id From [ClosingDisclosures] where [ClosingFileId] In (SELECT id FROM [ClosingFiles] where [ClosingNumber] = '{closingNumber}'));
                            Delete From [CdBorrowersTransactions] Where [ClosingDisclosureId] In(Select Id From [ClosingDisclosures] where [ClosingFileId] In (SELECT id FROM [ClosingFiles] where [ClosingNumber] = '{closingNumber}'));
                            Delete From [CdCalculatingCashToCloses] Where [ClosingDisclosureId] In(Select Id From [ClosingDisclosures] where [ClosingFileId] In (SELECT id FROM [ClosingFiles] where [ClosingNumber] = '{closingNumber}'));
                            Delete From [CdHeaders] Where [ClosingDisclosureId] In(Select Id From [ClosingDisclosures] where [ClosingFileId] In (SELECT id FROM [ClosingFiles] where [ClosingNumber] = '{closingNumber}'));
                            Delete From [CdLoanCalculations]Where [ClosingDisclosureId] In(Select Id From [ClosingDisclosures] where [ClosingFileId] In (SELECT id FROM [ClosingFiles] where [ClosingNumber] = '{closingNumber}'));
                            Delete From [CdLoanCosts]Where [ClosingDisclosureId] In(Select Id From [ClosingDisclosures] where [ClosingFileId] In (SELECT id FROM [ClosingFiles] where [ClosingNumber] = '{closingNumber}'));
                            Delete From [CdLoanDisclosures] Where [ClosingDisclosureId] In(Select Id From [ClosingDisclosures] where [ClosingFileId] In (SELECT id FROM [ClosingFiles] where [ClosingNumber] = '{closingNumber}'));
                            Delete From [CdLoanTerms] Where [ClosingDisclosureId] In(Select Id From [ClosingDisclosures] where [ClosingFileId] In (SELECT id FROM [ClosingFiles] where [ClosingNumber] = '{closingNumber}'));
                            Delete From [CdOtherCosts] Where [ClosingDisclosureId] In(Select Id From [ClosingDisclosures] where [ClosingFileId] In (SELECT id FROM [ClosingFiles] where [ClosingNumber] = '{closingNumber}'));
                            Delete From [CdProjectedPayments] Where [ClosingDisclosureId] In(Select Id From [ClosingDisclosures] where [ClosingFileId] In (SELECT id FROM [ClosingFiles] where [ClosingNumber] = '{closingNumber}'));
                            Delete From [CdSellersTransactions] Where [ClosingDisclosureId] In(Select Id From [ClosingDisclosures] where [ClosingFileId] In (SELECT id FROM [ClosingFiles] where [ClosingNumber] = '{closingNumber}'));
                            Delete From [ClosingDisclosures] where [ClosingFileId] In (SELECT id FROM [ClosingFiles] where [ClosingNumber] = '{closingNumber}');
                            Delete[ClosingFileBuyerSellerNotaryBlocks] where[ClosingFileBuyerSellerId] In(Select id From[ClosingFileBuyerSellers] where[ClosingFileId] In(SELECT id FROM [ClosingFiles] where[ClosingNumber] = '{closingNumber}'));
                            Delete from [BuyerSellerContactTypes] where CreatedBy_Id In(SELECT id FROM Members where email = 'autotestuser@gmail.com');
                            Delete[ClosingFileBuyerSellers] where[ClosingFileId] In(SELECT id FROM [ClosingFiles] where[ClosingNumber] = '{closingNumber}');
                            Delete[ActivityLoggers] where[ClosingFileId] In(SELECT id FROM [ClosingFiles] where[ClosingNumber] = '{closingNumber}');
                            Delete[ClosingFileNotes] where[ClosingFileId] In(SELECT id FROM [ClosingFiles] where[ClosingNumber] = '{closingNumber}');

                            Delete[ClosingFiles] where[ClosingNumber] = '{closingNumber}' And CreatedBy_Id in(Select id From Members where Email = '{email}')";
            var delete = await _conn.Connection.QueryFirstOrDefaultAsync<string>(query);

        }
        //public async void InsertVendor(string vendorLenderName, string vendorUnderwriterName, string userId, string companyId)
        //{
        //    var query = $@" Insert Into Vendors	(Name, VendorTypeId, CompanyId, LocationId, AddressId, CreatedBy_Id, Status, DateCreated, DateUpdated, UpdatedBy_Id, PaymentMethod)
        //                      values ('{vendorLenderName}', (Select Id From VendorTypes where CreatedBy_Id = {userId} and CompanyId = {companyId} and Name = 'Lender'), {companyId}, 1, (Select TOP(1) Id From Addresses), {userId}, 0, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), {userId}, 0)
        //                    Insert Into Vendors	(Name, VendorTypeId, CompanyId, LocationId, AddressId, CreatedBy_Id, Status, DateCreated, DateUpdated, UpdatedBy_Id, PaymentMethod)
        //                      values ('{vendorUnderwriterName}', (Select Id From VendorTypes where CreatedBy_Id = {userId} and CompanyId = {companyId} and Name = 'Underwriter'), {companyId}, 1, (Select TOP(1) Id From Addresses), {userId}, 0, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), {userId}, 0)";
        //    var closingNumber = await _conn.Connection.QueryFirstOrDefaultAsync<string>(query);
        //}
        public void InsertClosingFile(int companyId, int memberId, string fileNumber)
        {
            var query = $@"Insert into ClosingFiles (ClosingNumber, FinanceTypeId, UsageTypeId, propertyTypeId, ClosingDate, EffectiveDate, FundingDate, RefereeType, CompanyId, CreatedBy_Id, DateCreated, DateUpdated, UpdatedBy_Id, SellerId, FileStatus, IsUnknownOriginatorSelected, BuyerSigningDate, SellerSigningDate, IsCustomDistributionOfProceeds)
                               values ('{fileNumber}', 0, 0, 0, SYSDATETIMEOFFSET (), SYSDATETIMEOFFSET (), SYSDATETIMEOFFSET (), 0, {companyId}, {memberId}, SYSDATETIMEOFFSET (), SYSDATETIMEOFFSET (), {memberId}, 1, 0, 0, SYSDATETIMEOFFSET (), SYSDATETIMEOFFSET (), 0 )";
            var id = _conn.Connection.QueryFirstOrDefault<string>(query);
        }

        public string GetClosingFileDate(string closingNumber, string email)
        {
            var query = $@"Select ClosingDate From [ClosingFiles] where[ClosingNumber] = '{closingNumber}' And CreatedBy_Id in(Select id From Members where Email = '{email}')";
            var date = _conn.Connection.QueryFirstOrDefault<string>(query);
            return date;
        }

        public void InsertVendor(string vendorLenderName, string vendorUnderwriterName, int userId, int companyId, int addressId, int locationId)
        {
            var query = $@"Insert Into Vendors	(Name, VendorTypeId, CompanyId, LocationId, AddressId, CreatedBy_Id, Status, DateCreated, DateUpdated, UpdatedBy_Id, PaymentMethod)
                              values ('{vendorLenderName}', (Select Id From VendorTypes where CreatedBy_Id = {userId} and CompanyId = {companyId} and Name = 'Lender'), {companyId}, {locationId}, {addressId}, {userId}, 0, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), {userId}, 0)
                            Insert Into Vendors	(Name, VendorTypeId, CompanyId, LocationId, AddressId, CreatedBy_Id, Status, DateCreated, DateUpdated, UpdatedBy_Id, PaymentMethod)
                              values ('{vendorUnderwriterName}', (Select Id From VendorTypes where CreatedBy_Id = {userId} and CompanyId = {companyId} and Name = 'Underwriter'), {companyId}, {locationId}, {addressId}, {userId}, 0, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), {userId}, 0)";
            var closingNumber = _conn.Connection.QueryFirstOrDefault<string>(query);
        } 
        public void AddSellerBuyerToClosingFile(int closingFileId, int memberid)
        {
            var query = $@"Insert Into ClosingFileBuyerSellers([ClosingFileId],[BuyerOrSellerType],[MemberId],[CurrentAddressId],[ForwardingAddressId],[VestingType],[VestingPercentage],[CreatedBy_Id] 
                            ,[DateCreated],[DateUpdated],[Notes],[UpdatedBy_Id],[BuyerSellerEntityType],[MaritalStatus],[NameOnTitle],[OrganizationName],[Ssn],[AppMemberId],[OtherTitle],[Is1099sExempt]
                            ,[OwnershipThroughMaritalInterest],[VestingEscrowJournalId],[Estate],[Trust],[SsnTinToggle],[AffidavitToken],[SignatureBlockToken]
                            ,[OrderNumber],[VestingAmount],[RepTitle],[EntityStateId],[EntityType],[OtherEntityType],[GroupSigningIncluded],[IsRepresentative],[OrganizationId])
	                          Values
                              ({closingFileId},0,Null,Null,Null,Null,Null
                              ,{memberid},GETDATE(),GETDATE(),Null,{memberid},0,0,0
                              ,null,null,(Select Max(id) From AppMember),'',0,0,Null,Null,Null,Null,Null,Null,0,Null,Null,Null ,Null,Null,1,0,Null)";
            var closingNumber = _conn.Connection.QueryFirstOrDefault<string>(query);
        }
    }
}
