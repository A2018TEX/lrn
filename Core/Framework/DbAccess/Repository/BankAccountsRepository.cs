using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laren.E2ETests.Core.Framework.DbAccess.Repository
{
    public class BankAccountsRepository
    {
        private DataBaseConnection _conn;
        public BankAccountsRepository(DataBaseConnection connection)
        {
            _conn = connection;
        }
        public void AddNewBankAccount(int userId)
        {
            var query = $@"Insert Into [BankAccounts]
							  ([DateCreated],[CreatedBy_Id],[DateUpdated],[UpdatedBy_Id],[Notes]
                               ,[BankId],[RoutingNumber],[AccountNumber],[Name],[Description],[VoidIssue],[VoidValue],[CsvDisplayOrder])
	                          Values
	                          (SYSDATETIMEOFFSET (),{userId},SYSDATETIMEOFFSET (),{userId},NULL
                              ,(Select MAX(id) from [Banks]),123456123
                              ,123123123,'AutoTestAccounting','Account for test only',NULL,NULL,NULL)";

            var execute = _conn.Connection.QueryFirstOrDefault<string>(query);
        }

        public void DeleteNewBankAccount(int userId)
        {
            var query = $@"Delete [BankAccounts] where  CreatedBy_Id = {userId}";
            var execute = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
        }
    }
}
