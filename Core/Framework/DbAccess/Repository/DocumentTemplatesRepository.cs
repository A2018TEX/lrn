using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.DbAccess.Repository
{
    public class DocumentTemplatesRepository
    {
        private DataBaseConnection _conn;
        public DocumentTemplatesRepository(DataBaseConnection connection)
        {
            _conn = connection;
        }

        public int GetDocumentTemplateId(int companyId, int locationId)
        {
            var query = $@"SELECT Id FROM DocumentTemplates WHERE CompanyId = {companyId} AND LocationId = {locationId}";
            var id = _conn.Connection.QueryFirstOrDefault<int>(query);
            return id;
        }

        public string GetDocumentTemplateFileGuid(int companyId, int locationId)
        {
            var query = $@"SELECT FileGuid FROM DocumentTemplates WHERE CompanyId = {companyId} AND LocationId = {locationId}";
            var id = _conn.Connection.QueryFirstOrDefault<string>(query);
            return id;
        }

        public string GetDocumentTemplateFileName(int companyId, int locationId)
        {
            var query = $@"SELECT FileName FROM DocumentTemplates WHERE CompanyId = {companyId} AND LocationId = {locationId}";
            var id = _conn.Connection.QueryFirstOrDefault<string>(query);
            return id;
        }
    }
}
