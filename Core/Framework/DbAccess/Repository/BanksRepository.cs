using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laren.E2ETests.Core.Framework.DbAccess.Repository
{
    public class BanksRepository
    {
        private DataBaseConnection _conn;
        public BanksRepository(DataBaseConnection connection)
        {
            _conn = connection;
        }

        public void AddNewBank(int userId, int companyId)
        {
            var query = $@"Insert Into [Banks]
							  ([DateCreated],[CreatedBy_Id],[DateUpdated],[UpdatedBy_Id],[Notes],[CompanyId],[LocationId],[Name]
                                 ,[Description],[AddressId],[Fax],[Mobile],[TeleNumber],[CustomerNumber])
	                          Values
	                          (GETDATE(),{userId},GETDATE(),{userId},''
                              ,{companyId},NULL,'Autotest Bank'
                              ,'Bank only for test',447,NULL,'(123) 532-1634','(123) 542-1634',Null)";
            var execute = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
        }

        public void DeleteNewBank(int userId)
        {
            var query = $@"Delete [Banks] where  CreatedBy_Id = {userId}";
            var execute = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
        }

        public int GetBankId()
        {
            var query = $@"Select (Select MAX(id) from Banks) +1";
            var id = _conn.Connection.QueryFirstOrDefault<int>(query);
            return id;
        }
    }
}
