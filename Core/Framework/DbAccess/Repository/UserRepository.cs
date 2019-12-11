using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.DbAccess.Repository
{
    public class UserRepository
    {

        private DataBaseConnection _conn;

        public UserRepository(DataBaseConnection connection)
        {
            _conn = connection;
        }

        public string GetMaxIdAddOne()
        {
            var query = $@" Select (Select MAX(id) from UserRegistrations) +  1";
            var id = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return id;
        }
        public string InsertUserIntoDB(string email, Guid uniqueidentifier)
        {
            var query = $@" Insert into UserRegistrations (EmailAddress, FirstName, LastName, Phone, Company, CompanyType, HowYouHeardFrom, HowYouHeardSpecify, RegistrationToken, CreatedDate, DateCreated, HasCompletedRegistration)
                      values ('{email}','AutomateCreatedCompany', 'AutomateCreatedCompany','(321) 321-3213', 'AutomateCreatedCompany', 0 , 'A Seminar - Please Specify', 'AutomateCreatedCompany', '{uniqueidentifier}', SYSUTCDATETIME(), SYSUTCDATETIME(), 0)";
            var id = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return id;
        }
        public int GetCompanyIdFromMemberCompanies(int userId)
        {
            var query = $@"Select CompanyId From MemberCompanies Where MemberId = '{userId}'";
            var id = _conn.Connection.QueryFirstOrDefault<int>(query);
            return id;
        }
        public void DeleteUser(string email)
        {
            var query = $@" DELETE FROM  ClosingFileBuyerSellerNotaryBlocks Where ClosingFileBuyerSellerId in (Select Id FROM ClosingFileBuyerSellers WHERE CreatedBy_Id In (SELECT Id FROM Members WHERE UserName='{email}'))
                        DELETE FROM ClosingFileBuyerSellers WHERE CreatedBy_Id In (SELECT Id FROM Members WHERE UserName='{email}')
                        DELETE FROM ClosingFileVendor WHERE VendorId IN (SELECT Id FROM Vendors WHERE VendorTypeId IN (SELECT ID FROM VendorTypes WHERE UpdatedBy_Id in (SELECT Id FROM Members WHERE UserName='{email}')))
                        DELETE FROM Vendors WHERE VendorTypeId IN (SELECT ID FROM VendorTypes WHERE UpdatedBy_Id in (SELECT Id FROM Members WHERE UserName='{email}'))
                        DELETE FROM VendorTypes WHERE UpdatedBy_Id in (SELECT Id FROM Members WHERE UserName='{email}')
                        DELETE FROM CdBorrowersTransactions WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName='{email}')))
                        DELETE FROM CdAps WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName='{email}')))
                        DELETE FROM CdCalculatingCashToCloses WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName='{email}')))
                        DELETE FROM CdAirs WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName='{email}')))
                        DELETE FROM CdHeaders WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName='{email}')))
                        DELETE FROM CdLoanCalculations WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName='{email}')))
                        DELETE FROM CdLoanCosts WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName='{email}')))
                        DELETE FROM CdLoanDisclosures WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName='{email}')))
                        DELETE FROM CdLoanTerms WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName='{email}')))
                        DELETE FROM CdOtherCosts WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName='{email}')))
                        Delete FROM ClosingFileVendor WHERE VendorId IN (SELECT Id FROM Vendors WHERE CompanyId in (Select Id FROM Companies WHERE UpdatedBy_Id in (SELECT Id FROM Members WHERE UserName='{email}')))
                        DELETE FROM CdProjectedPayments WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName='{email}')))
                        DELETE FROM CdSellersTransactions WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName='{email}')))
						DELETE FROM CdPayoffAndPayment where ClosingDisclosureId in( select id from ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName='{email}')))
                        DELETE FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName='{email}'))
                        DELETE FROM MemberCompanies WHERE MemberId in (SELECT Id FROM Members WHERE UserName='{email}')
                        DELETE FROM ActivityLoggers WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName='{email}'))
                        DELETE From ClosingFileNotes where ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName='{email}'))
                   Delete FROM ClosingFileAddress where ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName='{email}'))
                   Delete FROM WidgetsMembers where CompanyId IN (SELECT Id FROM Companies WHERE UpdatedBy_Id in (SELECT id FROM Members WHERE UserName='{email}'))
                        Delete From ClosingFileBuyerSellers Where ClosingFileId in (SELECT Id From ClosingFiles Where CreatedBy_Id in (SELECT Id FROM Members WHERE UserName='{email}'))
                        Delete from ClosingFileEscrowDeposits Where CreatedBy_Id in (SELECT id FROM Members WHERE UserName='{email}')
						Delete From ClosingFiles Where CreatedBy_Id in (SELECT Id FROM Members WHERE UserName='{email}')
						
						DELETE FROM BuyerSellerContactTypes WHERE CompanyId in (Select Id FROM Companies WHERE UpdatedBy_Id in (SELECT Id FROM Members WHERE UserName='{email}'))
                        DELETE FROM ClosingFileWorkflowItemNotifications WHERE ClosingFileWorkflowItemId in (SELECT id FROM ClosingFileWorkflowItems WHERE ClosingFileWorkflowSectionId in (SELECT id FROM ClosingFileWorkflowSections WHERE CreatedBy_Id in (SELECT id FROM Members WHERE UserName='{email}')))
                        Delete From ClosingFileWorkflowItemComments where ClosingFileWorkflowItemId in (SELECT id FROM ClosingFileWorkflowItems WHERE ClosingFileWorkflowSectionId in (SELECT id FROM ClosingFileWorkflowSections WHERE CreatedBy_Id in (SELECT id FROM Members WHERE UserName='{email}')))


                        DELETE FROM ClosingFileWorkflowItemComments WHERE ClosingFileWorkflowItemId in (SELECT id FROM ClosingFileWorkflowItems WHERE ClosingFileWorkflowSectionId in (SELECT id FROM ClosingFileWorkflowSections WHERE CreatedBy_Id in (SELECT id FROM Members WHERE UserName='{email}')))
                        DELETE FROM ClosingFileWorkflowItems WHERE ClosingFileWorkflowSectionId in (SELECT id FROM ClosingFileWorkflowSections WHERE CreatedBy_Id in (SELECT id FROM Members WHERE UserName='{email}'))
                        DELETE FROM ClosingFileWorkflowSections WHERE CreatedBy_Id in (SELECT id FROM Members WHERE UserName='{email}')
                        DELETE FROM ClosingFileWorkflowInstances WHERE CreatedBy_Id in (SELECT Id FROM Members WHERE UserName='{email}')
                        Delete from WorkflowItems where CreatedBy_Id in (SELECT Id FROM Members WHERE UserName='{email}')
                        Delete from WorkflowSections where CreatedBy_Id in (SELECT Id FROM Members WHERE UserName='{email}')
                        Delete from WorkflowTemplates where CreatedBy_Id in (SELECT Id FROM Members WHERE UserName='{email}')
                        Delete from UserRegistrations where EmailAddress='{email}'
                        Delete From ActivityLoggers Where CreatedBy_Id in (SELECT Id FROM Members WHERE UserName='{email}')
                        Delete FROM ClosingFileVendor WHERE VendorId in (SELECT Id FROM Vendors WHERE CompanyId in (Select Id FROM Companies WHERE UpdatedBy_Id in (SELECT Id FROM Members WHERE UserName='{email}')))

                        DELETE FROM Vendors WHERE VendorTypeId in (Select ID FROM VendorTypes WHERE CreatedBy_Id in (SELECT Id FROM Members WHERE UserName='{email}'))
                        DELETE FROM VendorTypes WHERE CompanyId in (Select Id FROM Companies WHERE UpdatedBy_Id in (SELECT Id FROM Members WHERE UserName='{email}'))
                        
						Delete From BankAccounts Where BankId in (Select Id From Banks Where CompanyId in(Select id From Companies WHERE UpdatedBy_Id in (SELECT id FROM Members WHERE UserName='{email}')))
						    Delete From Banks Where CompanyId in(Select id From Companies WHERE UpdatedBy_Id in (SELECT id FROM Members WHERE UserName='{email}'))
							DELETE FROM CalendarWorkWeek WHERE UpdatedBy_Id in (SELECT id FROM Members WHERE UserName='{email}')
                            
                        Delete From NotaryBlockJuratTemplates where CompanyId in (Select Id FROM Companies WHERE UpdatedBy_Id in (SELECT Id FROM Members WHERE UserName='{email}'))
                        Delete From NotaryBlockAcknowledgementTemplates where CompanyId in (Select Id FROM Companies WHERE UpdatedBy_Id in (SELECT Id FROM Members WHERE UserName='{email}'))
                        Delete From Locations Where AddressId in(Select id From Addresses Where CreatedBy_Id in (SELECT id FROM Members WHERE UserName='{email}'))
                        Delete From Addresses Where CreatedBy_Id in (SELECT id FROM Members WHERE UserName='{email}')

                        Delete From DocumentTemplates Where CompanyId in(Select id From Companies WHERE UpdatedBy_Id in (SELECT id FROM Members WHERE UserName='{email}'))
                        Delete From DocumentTemplates Where LocationId in(Select id From Locations Where AddressId in(Select id From Addresses Where CreatedBy_Id in (SELECT id FROM Members WHERE UserName='{email}')))
                        DELETE FROM Companies WHERE UpdatedBy_Id in (SELECT id FROM Members WHERE UserName='{email}')
                        Delete From ClosingFileWorkflowItemOrders Where CreatedBy_Id in (SELECT id FROM Members WHERE UserName='{email}')
                        Delete from  ClosingFileDocuments Where CreatedBy_Id in (SELECT id FROM Members WHERE UserName='{email}')
                        Delete from Members where UserName='{email}'
            Delete UserRegistrations where EmailAddress='{email}'";
            string id = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
        }
       
        public void AddNewBuyerToClosingFile(string closingfileId)
        {
            var query = $@"Insert Into [ClosingFileBuyerSellers]
                        ([ClosingFileId],[BuyerOrSellerType],[MemberId],[CurrentAddressId],[ForwardingAddressId],[VestingType]
                         ,[VestingPercentage],[CreatedBy_Id],[DateCreated],[DateUpdated],[Notes],[UpdatedBy_Id],[BuyerSellerEntityType]
                         ,[MaritalStatus],[NameOnTitle],[OrganizationName],[Ssn],[AppMemberId],[OtherTitle],[Is1099sExempt],[OwnershipThroughMaritalInterest]
                         ,[VestingEscrowJournalId],[Estate],[Trust],[SsnTinToggle],[AffidavitToken],[SignatureBlockToken],[OrderNumber]
                         ,[VestingAmount],[RepTitle],[EntityStateId],[EntityType],[OtherEntityType])
	                          Values
	                     ({closingfileId}
                          ,0,Null,NULL,NULL,NULL,NULL,(Select MAX(id) from Members),SYSDATETIMEOFFSET (),SYSDATETIMEOFFSET ()
                          ,NULL,(Select MAX(id) from Members),0,0,'','','',(Select (Select MAX(id) from AppMember)),''
                          ,0,0,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,NULL)";
            var id = _conn.Connection.QueryFirstOrDefault<string>(query);
        }
        public int GetUserId(string email)
        {
            var query = $@"Select Id from Members where UserName = '{email}'";
            var id = _conn.Connection.Query<int>(query).ToList().FirstOrDefault();
            return id;
        }
        public void DeleteDataAfterEachTest(string email)
        {
            var query = $@"DELETE FROM  ClosingFileBuyerSellerNotaryBlocks Where ClosingFileBuyerSellerId in (Select Id FROM ClosingFileBuyerSellers WHERE CreatedBy_Id in (SELECT Id FROM Members WHERE UserName             = '{email}'))
                        DELETE FROM ClosingFileBuyerSellerNotaryBlocks WHERE UpdatedBy_Id in  (SELECT Id FROM Members WHERE UserName = '{email}')
                            DELETE FROM ClosingFileBuyerSellers WHERE CreatedBy_Id in (SELECT Id FROM Members WHERE UserName = '{email}')
                            DELETE FROM ClosingFileVendor WHERE VendorId IN (SELECT Id FROM Vendors WHERE VendorTypeId IN (SELECT ID FROM VendorTypes WHERE UpdatedBy_Id in (SELECT Id FROM Members WHERE UserName = '{email}')))
                            DELETE FROM Vendors WHERE VendorTypeId IN (SELECT ID FROM VendorTypes WHERE UpdatedBy_Id in (SELECT Id FROM Members WHERE UserName = '{email}'))
                            DELETE FROM CdBorrowersTransactions WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName = '{email}')))
                            DELETE FROM CdAps WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName = '{email}')))
                            DELETE FROM CdCalculatingCashToCloses WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName = '{email}')))
                            DELETE FROM CdAirs WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName = '{email}')))
                            DELETE FROM CdHeaders WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName = '{email}')))
                            DELETE FROM CdLoanCalculations WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName = '{email}')))
                            DELETE FROM CdLoanCosts WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName = '{email}')))
                            DELETE FROM CdLoanDisclosures WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName = '{email}')))
                            DELETE FROM CdLoanTerms WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName = '{email}')))
                            DELETE FROM CdOtherCosts WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName = '{email}')))
                            Delete FROM ClosingFileVendor WHERE VendorId IN (SELECT Id FROM Vendors WHERE CompanyId in (Select Id FROM Companies WHERE UpdatedBy_Id in (SELECT Id FROM Members WHERE UserName = '{email}')))
                            DELETE FROM CdProjectedPayments WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName = '{email}')))
                            DELETE FROM CdSellersTransactions WHERE ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName = '{email}')))
                            Delete From CdPayoffAndPayment Where ClosingDisclosureId in (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName = '{email}')))
                            Delete From CdPayoffAndPayment Where ClosingDisclosureId IN (Select Id FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName = '{email}')))

                            DELETE FROM ClosingDisclosures WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName = '{email}'))
                            DELETE FROM ActivityLoggers WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName = '{email}'))
                            DELETE FROM EscrowJournals WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName = '{email}'))
                            DELETE FROM ClosingFileNotes WHERE ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName = '{email}'))
                            DELETE FROM ClosingFileDocuments WHERE  ClosingFileId IN (SELECT Id FROM ClosingFiles WHERE CreatedBy_Id IN (SELECT Id FROM Members WHERE UserName = '{email}'))
                            DELETE FROM BuyerSellerContactTypes WHERE CompanyId in (Select Id FROM Companies WHERE UpdatedBy_Id in (SELECT Id FROM Members WHERE UserName = '{email}'))
                            DELETE FROM ClosingFileWorkflowItemComments WHERE ClosingFileWorkflowItemId in (SELECT id FROM ClosingFileWorkflowItems WHERE ClosingFileWorkflowSectionId in (SELECT id FROM ClosingFileWorkflowSections WHERE CreatedBy_Id in (SELECT id FROM Members WHERE UserName = '{email}')))
                            DELETE FROM ClosingFileWorkflowItemNotifications WHERE ClosingFileWorkflowItemId in (SELECT id FROM ClosingFileWorkflowItems WHERE ClosingFileWorkflowSectionId in (SELECT id FROM ClosingFileWorkflowSections WHERE CreatedBy_Id in (SELECT id FROM Members WHERE UserName = '{email}')))
                            DELETE FROM ClosingFileWorkflowItems WHERE ClosingFileWorkflowSectionId in (SELECT id FROM ClosingFileWorkflowSections WHERE CreatedBy_Id in (SELECT id FROM Members WHERE UserName = '{email}'))
                            DELETE FROM ClosingFileWorkflowSections WHERE CreatedBy_Id in(SELECT id FROM Members WHERE UserName = '{email}')
                            DELETE FROM ClosingFileWorkflowInstances WHERE CreatedBy_Id in (SELECT Id FROM Members WHERE UserName = '{email}')
                            Delete from WorkflowItems where CreatedBy_Id in (SELECT Id FROM Members WHERE UserName = '{email}')
                            Delete from WorkflowSections where CreatedBy_Id in (SELECT Id FROM Members WHERE UserName = '{email}')
                            Delete from WorkflowTemplates where CreatedBy_Id in (SELECT Id FROM Members WHERE UserName = '{email}')
                            Delete From ActivityLoggers Where CreatedBy_Id in (SELECT Id FROM Members WHERE UserName = '{email}')
                            Delete FROM ClosingFileVendor WHERE VendorId in (SELECT Id FROM Vendors WHERE CompanyId in (Select Id FROM Companies WHERE UpdatedBy_Id in (SELECT Id FROM Members WHERE UserName = '{email}')))
                            DELETE FROM Vendors WHERE VendorTypeId in (Select ID FROM VendorTypes WHERE CreatedBy_Id in (SELECT Id FROM Members WHERE UserName = '{email}'))
                            DELETE FROM CalendarWorkWeek WHERE UpdatedBy_Id in (SELECT id FROM Members WHERE UserName = '{email}')
                            
                            Delete From DocumentTemplates Where CompanyId in (Select Id FROM Companies WHERE UpdatedBy_Id in (SELECT Id FROM Members WHERE UserName = '{email}'))
                            Delete From Locations Where CompanyId in (Select Id FROM Companies WHERE UpdatedBy_Id in (SELECT Id FROM Members WHERE UserName = '{email}'))
                            Delete From Addresses Where UpdatedBy_Id in (SELECT Id FROM Members WHERE UserName = '{email}')
                            DELETE FROM NotaryBlockAcknowledgementTemplates WHERE CompanyId in (Select Id FROM Companies WHERE UpdatedBy_Id in (SELECT Id FROM Members WHERE UserName = '{email}'))
                            DELETE FROM NotaryBlockJuratTemplates  WHERE CompanyId in (Select Id FROM Companies WHERE UpdatedBy_Id in (SELECT Id FROM Members WHERE UserName = '{email}'))
                            DELETE FROM ClosingFileDocuments WHERE CompanyId in (Select Id FROM Companies WHERE UpdatedBy_Id in (SELECT Id FROM Members WHERE UserName = '{email}'))
                             Delete From ClosingFileWorkflowItemOrders Where CreatedBy_Id in (SELECT id FROM Members WHERE UserName = '{email}')";
            var delete = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
        }
    }
}
