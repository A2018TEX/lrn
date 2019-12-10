using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.DbAccess.Repository
{
    public class MembersRepository
    {
        private DataBaseConnection _conn;
        public MembersRepository(DataBaseConnection connection)
        {
            _conn = connection;
        }

        public void DeleteMember(string email)
        {
            var query = $@"Delete from Members where UserName = '{email}'";
            var id = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
        }

        public string GetUserId(string email)
        {
            Thread.Sleep(100);
            var query = $@"Select Id from Members where UserName = '{email}'";
            var id = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return id;
        }

        public string GetCompanyIdFromMemberCompanies(string userId)
        {
            var query = $@"Select CompanyId From MemberCompanies Where MemberId = '{userId}'";
            var id = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return id;
        }
    }
}
