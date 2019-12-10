using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laren.E2ETests.Core.Framework.DbAccess.Repository
{
    public class CompaniesRepository
    {

        private DataBaseConnection _conn;
        public CompaniesRepository(DataBaseConnection connection)
        {
            _conn = connection;
        }
        public CompaniesRepository AddNewCompanyToDB(string companyName, string userid)
        {
            var query = $@"Insert Into [Companies] 
                          ([Name],[CompanyTypeId],[Notes],[OwnerAccountId],[CreatedBy_Id],[DateCreated],[DateUpdated],[UpdatedBy_Id]
                          ,[TimeZone],[InvoiceDate],[CompanyStatus],[StateLicenseNumber])
	                      Values
	                      ('{companyName}',0,NULL,NULL,'{userid}',SYSDATETIMEOFFSET (),SYSDATETIMEOFFSET ()
                          ,'{userid}',0,0,0,NULL)";
            var delete = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return this;
        }

        public CompaniesRepository DeleteCompanyFromDB(string companyName)
        {
            var query = $@"   Delete MemberCompanies where CompanyId in(Select id from  [Companies] where Name = '{companyName}')
	                          Delete WorkflowItems where WorkflowSectionId in(Select id from   WorkflowSections Where WorkflowTemplateId in(Select id From WorkflowTemplates where CompanyId in(Select id from  [Companies] where Name = '{companyName}')))
	                          Delete WorkflowSections Where WorkflowTemplateId in(Select id From WorkflowTemplates where CompanyId in(Select id from  [Companies] where Name = '{companyName}') )
	                          Delete WorkflowTemplates where CompanyId in(Select id from  [Companies] where Name = '{companyName}')
                              Delete  NotaryBlockJuratTemplates where CompanyId in(Select id from [Companies] where Name = '{companyName}')
                              Delete NotaryBlockAcknowledgementTemplates where CompanyId in(Select id from [Companies] where Name = '{companyName}')
                              Delete BuyerSellerContactTypes where CompanyId in(Select id From Companies where Name = '{companyName}');
                              Delete  CalendarWorkWeek where CompanyId in(Select id From Companies where Name = '{companyName}');
	                          Delete [Companies] where Name = '{companyName}'";
            var delete = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return this;
        }

    }
}
