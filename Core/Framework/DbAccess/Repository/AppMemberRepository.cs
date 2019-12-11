using Dapper;
using Laren.E2ETests.Core.Framework.DbAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.DbAccess.Repository
{
    public class AppMemberRepository
    {
        private DataBaseConnection _conn;
        public AppMemberRepository(DataBaseConnection connection)
        {
            _conn = connection;
        }

        public string GetSellerLastName(string name)
        {
            Thread.Sleep(100);
            var query = $@"SELECT [LastName]
                           FROM [AppMember] where FirstName = '{name}' order by id desc";
            AppMembersModel closingNumber = _conn.Connection.QueryFirstOrDefault<AppMembersModel>(query);
            return closingNumber.LastName;
        }

        public void DeleteBuyerSeller(string companyId, string firstName, int buyerOrSeller)
        {
            var query = $@"   Delete ClosingFileRelationshipBuyerSellers Where ClosingFileBuyerSellerId in(Select id From ClosingFileBuyerSellers Where AppMemberId in(Select id From [AppMember] Where FirstName ='{firstName}' And id in(Select [AppMemberId] From [ClosingFileBuyerSellers] where [BuyerOrSellerType] = {buyerOrSeller})));
                              Delete ClosingFileBuyerSellerNotaryBlocks Where ClosingFileBuyerSellerId in(Select id From ClosingFileBuyerSellers Where AppMemberId in(Select id From [AppMember] Where FirstName ='{firstName}'And id in(Select [AppMemberId] From [ClosingFileBuyerSellers] where [BuyerOrSellerType] = {buyerOrSeller} )));
                              Delete EscrowJournals Where AppMemberId in(Select id From [AppMember] Where FirstName ='{firstName}' And id in(Select [AppMemberId] From [ClosingFileBuyerSellers] where [BuyerOrSellerType] = {buyerOrSeller}));
                              Delete ClosingFileBuyerSellers Where AppMemberId in(Select id From [AppMember] Where FirstName ='{firstName}' And id in(Select [AppMemberId] From [ClosingFileBuyerSellers] where [BuyerOrSellerType] = {buyerOrSeller}));
                           
                              Delete [AppMember] Where FirstName ='{firstName}' And id in(Select [AppMemberId] From [ClosingFileBuyerSellers] where [BuyerOrSellerType] = {buyerOrSeller})";
            AppMembersModel closingNumber = _conn.Connection.Query<AppMembersModel>(query).ToList().FirstOrDefault();
            
        }

        public void AddNewAppMember(string fName, string lName)
        {
            var query = $@"Insert Into [AppMember]
                              ([FirstName],[LastName],[Suffix],[MiddleName],[JobTitle],[PhoneMobile],[PhoneFax]
                              ,[StateLicNumber],[ProfileImage],[SignatureImage],[RoleId],[Status],[ApplicationViewId],[Gender]
                              ,[BuyerType],[InternalMember],[LockoutEnd],[TwoFactorEnabled],[PhoneNumberConfirmed],[PhoneNumber],[ConcurrencyStamp],[SecurityStamp]
                              ,[PasswordHash],[EmailConfirmed],[NormalizedEmail],[Email],[NormalizedUserName],[UserName],[LockoutEnabled],[AccessFailedCount])
	                          Values
	                          ('{fName}','{lName}',NULL,NULL,NULL,''
                                      ,'' ,'' ,'','',NULL,NULL,NULL,NULL,0,0,NULL,NULL,NULL,'',NULL,NULL,NULL,NULL,NULL,'',NULL,'',NULL,NULL)";
            AppMembersModel closingNumber = _conn.Connection.Query<AppMembersModel>(query).ToList().FirstOrDefault();
        }
        public void DeleteSellerBuyerByFirstName(string firstName)
        {
            var query = $@"   SELECT* FROM [AppMember] where FirstName ='{firstName}'
                              Delete ClosingFileBuyerSellerNotaryBlocks where ClosingFileBuyerSellerId in (Select id from ClosingFileBuyerSellers where AppMemberId in (Select id from [AppMember] where FirstName ='{firstName}'))
                              Delete ClosingFileBuyerSellers where  AppMemberId in (Select id from [AppMember] where FirstName ='{firstName}')
                              delete EscrowJournals where  AppMemberId in (Select id from [AppMember] where FirstName ='{firstName}')
                              Delete from [AppMember] where FirstName ='{firstName}' ";
            AppMembersModel closingNumber = _conn.Connection.Query<AppMembersModel>(query).ToList().FirstOrDefault();

        }
    }
}
