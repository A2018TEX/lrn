using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.DbAccess.Repository
{
    public class ClosingFileVendorRepository
    {
        private DataBaseConnection _conn;
        public ClosingFileVendorRepository(DataBaseConnection connection)
        {
            _conn = connection;
        }
        public ClosingFileVendorRepository DeleteClosingFileVendorsByClosingFileId(string closingFileId)
        {
            var query = $@"  DELETE FROM ClosingFileVendor WHERE ClosingFileId = {closingFileId}";
            var delete = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return this;
        }
        public string GetClosingFileVendorIdByClosingFileId(int closingFileId, string lenderName)
        {
            Thread.Sleep(1000);
            var query = $@"SELECT ClosingFileVendorId FROM ClosingFileVendor WHERE ClosingFileId = {closingFileId} and VendorId in(Select Id From Vendors Where Name = '{lenderName}')";
            var closingFileVendorId = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return closingFileVendorId;
        }
    }
}
