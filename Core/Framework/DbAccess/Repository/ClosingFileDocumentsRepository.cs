using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.DbAccess.Repository
{
    public class ClosingFileDocumentsRepository
    {
        private DataBaseConnection _conn;
        public ClosingFileDocumentsRepository(DataBaseConnection connection)
        {
            _conn = connection;
        }
        public ClosingFileDocumentsRepository InsertAdress(string userId)
        {
            var query = $@"Insert Into Addresses (Id, AddressLine1, City, StateId, ZipCode, CreatedBy_Id, DateCreated, DateUpdated, UpdatedBy_Id)
	                         values ((	 Select (Select MAX(Id) From Addresses) + 1), '222 Berkeley St', 'Boston', 21, 02116, {userId}, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), {userId})";
            var insert = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return this;
        }

        public ClosingFileDocumentsRepository InsertFileToDraft(int closingFileId, int documentTemplateId, int companyId, int locationId, string documentTemplateFileGuid, string documentTemplateFileName)
        {
            var query = $@"Insert Into ClosingFileDocuments ([DateCreated], [ClosingFileId], [DocumentTemplateId], [CompanyId], [LocationId], [FileGuid], [FileName], [FileType], [FileSize], [Tokens], [LastModified], [ClosingfileDocumentType], [ClosingFileDocumentState], [ClosingFileDocumentStatus], [ClosingPacketOrder], [AreTokensPopulated])
                            values (SYSUTCDATETIME(), {closingFileId}, {documentTemplateId}, {companyId}, {locationId}, '{documentTemplateFileGuid}', '{documentTemplateFileName}',  0, 9, 1, SYSUTCDATETIME(), 3, 0, 0, 0, 1)";
            var insert = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return this;
        }
        public void SetArchivedClosingFileDocumentStatus(string fileName, int closingFileId)
        {
            Thread.Sleep(1000);
            var query = $@"update ClosingFileDocuments set ClosingFileDocumentStatus = 1 where ClosingFileId = {closingFileId} and FileName LIKE '{fileName}%'";
            var update = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
        }
        public string GetClosingFileDocumentStatus(string fileNameWithoutFormar, int closingFileId)
        {
            Thread.Sleep(1000);
            var query = $@"Select ClosingFileDocumentStatus From ClosingFileDocuments Where ClosingFileId = {closingFileId} and FileName LIKE '{fileNameWithoutFormar}%' ";
            var id = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return id;
        }
        public string CheckThatDocumentIsPresent(string fileNameAfterRemaming)
        {
            Thread.Sleep(1000);
            var query = $@"Select Id FROM [ClosingFileDocuments] Where FileName like '{fileNameAfterRemaming}%' ";
            var id = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return id;
        }
        public string GetClosingFileDocumentId(string fileNameWithoutFormar, int closingFileId)
        {
            Thread.Sleep(1000);
            var query = $@"Select Id From ClosingFileDocuments Where ClosingFileId = {closingFileId} and FileName LIKE '{fileNameWithoutFormar}%' ";
            var id = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return id;
        }
    }
}
