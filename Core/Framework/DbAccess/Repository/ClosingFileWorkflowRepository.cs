using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.DbAccess.Repository
{
    public class ClosingFileWorkflowRepository
    {
        private DataBaseConnection _conn;

        public ClosingFileWorkflowRepository(DataBaseConnection connection)
        {
            _conn = connection;
        }
        public ClosingFileWorkflowRepository DeleteClosingFileWorkflowItemNotificationsByClosingFileName(string closingNumber)
        {
            var query = $@"  DELETE FROM ClosingFileWorkflowItemNotifications 
                            WHERE ClosingFileWorkflowItemId IN 
                            (SELECT Id FROM ClosingFileWorkflowItems WHERE ClosingFileWorkflowSectionId IN 
                            (SELECT Id FROM ClosingFileWorkflowSections WHERE ClosingFileWorkflowInstanceId  IN 
                            (Select Id FROM ClosingFileWorkflowInstances WHERE ClosingFileId IN 
                            (SELECT Id FROM ClosingFiles WHERE ClosingNumber = '{closingNumber}'))))";
            var delete = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return this;
        }
        public string GetIdOfJustCreatedReminderNotification(string notificationText)
        {
            Thread.Sleep(1000);
            var query = $@"SELECT Id FROM ClosingFileWorkflowItemNotifications WHERE OptionsJson like '%{notificationText}%'";
            var id = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return id;
        }

        public int GetClosingFileWorkflowInstanceId(int companyId, int userId)
        {
            var query = $@"SELECT Id FROM ClosingFileWorkflowInstances WHERE CompanyId = {companyId} and CreatedBy_Id = {userId}";
            var id = _conn.Connection.QueryFirstOrDefault<int>(query);
            return id;
        }

        public int GetClosingFileWorkflowSectionId(int closingFileWorkflowInstanceId, int userId)
        {
            var query = $@"  Select Id From ClosingFileWorkflowSections Where ClosingFileWorkflowInstanceId = {closingFileWorkflowInstanceId} and CreatedBy_Id = {userId}";
            var id = _conn.Connection.QueryFirstOrDefault<int>(query);
            return id;
        }

        public void InsertClosingFileWorkflowItem(int closingFileWorkflowSectionId, int roleId, int userId)
        {
            var query = $@" Insert Into ClosingFileWorkflowItems (Status, Name, ClosingFileWorkflowSectionId, Priority, RoleId, [Order], PlanDate, CompletedById, ActualDate, MemberId, PlanDateModified,
                              DependentType, CreatedBy_Id, DateCreated, DateUpdated, UpdatedBy_Id, StartedById, StartedDate)
                              values (0, 'Test', {closingFileWorkflowSectionId}, 1, {roleId}, 0, SYSDATETIMEOFFSET(), 0, SYSDATETIMEOFFSET(), {userId}, 0, 
                              0, {userId},SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), {userId}, 0, SYSDATETIMEOFFSET())";
            var insert = _conn.Connection.QueryFirstOrDefault<string>(query);
        }

        public ClosingFileWorkflowRepository InsertClosingFileWorkflowSection(int closingFileWorkflowInstanceId, int userId)
        {
            var query = $@" Insert Into ClosingFileWorkflowSections (Name, ClosingFileWorkflowInstanceId, [Order], CreatedBy_Id, DateCreated, DateUpdated, UpdatedBy_Id)
                          values ('Test', {closingFileWorkflowInstanceId}, 0, {userId}, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(),  {userId})";
            var insert = _conn.Connection.QueryFirstOrDefault<int>(query);
            return this;
        }

        public void InsertClosingFileWorkflowInstance(string workflowTemplateId, int companyId, int closingFileId, int userId)
        {
            var query = $@" Insert into ClosingFileWorkflowInstances (WorkflowTemplateId, Name, CompanyId, ClosingFileId, CreatedBy_Id, DateCreated, DateUpdated, UpdatedBy_Id)
                          values ({workflowTemplateId}, 'Test', {companyId}, {closingFileId}, {userId}, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), {userId})";
            var insert = _conn.Connection.QuerySingleOrDefault<string>(query);
        }

        public ClosingFileWorkflowRepository DeleteClosingFileWorkflowItemCommentsByClosingFileName(string closingNumber)
        {
            var query = $@"  DELETE FROM ClosingFileWorkflowItemComments 
                            WHERE ClosingFileWorkflowItemId IN 
                              (SELECT Id FROM ClosingFileWorkflowItems WHERE ClosingFileWorkflowSectionId IN 
                              (SELECT Id FROM ClosingFileWorkflowSections WHERE ClosingFileWorkflowInstanceId  IN 
                              (Select Id FROM ClosingFileWorkflowInstances WHERE ClosingFileId IN 
                              (SELECT Id FROM ClosingFiles WHERE ClosingNumber = '{closingNumber}'))))";
            var delete = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return this;
        }

        public void InsertWorkWeek(int userId, int companyId)
        {
            var query = $@" Insert into CalendarWorkWeek (DateCreated, CreatedBy_Id, DateUpdated, UpdatedBy_Id, Notes, CompanyId, LocationId, WorkWeek)
                              values (SYSDATETIMEOFFSET(), {userId}, SYSDATETIMEOFFSET (), {userId}, null, {companyId}, null, 127)";
            var update = _conn.Connection.QueryFirstOrDefault<string>(query);
        }

        public ClosingFileWorkflowRepository DeleteClosingFileWorkflowItemsByClosingFileName(string closingNumber)
        {
            var query = $@"  DELETE FROM ClosingFileWorkflowItems 
                            WHERE ClosingFileWorkflowSectionId IN 
                              (SELECT Id FROM ClosingFileWorkflowSections WHERE ClosingFileWorkflowInstanceId  IN 
                              (Select Id FROM ClosingFileWorkflowInstances WHERE ClosingFileId IN 
                              (SELECT Id FROM ClosingFiles WHERE ClosingNumber = '{closingNumber}')))";
            var delete = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return this;
        }
        public ClosingFileWorkflowRepository DeleteClosingFileWorkflowSectionsByClosingFileName(string closingNumber)
        {
            var query = $@" DELETE FROM ClosingFileWorkflowSections 
                              WHERE ClosingFileWorkflowInstanceId  IN 
                              (Select Id FROM ClosingFileWorkflowInstances WHERE ClosingFileId IN 
                              (SELECT Id FROM ClosingFiles WHERE ClosingNumber = '{closingNumber}'))";
            var delete = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return this;
        }
        public ClosingFileWorkflowRepository DeleteClosingFileWorkflowInstancesByClosingFileName(string closingNumber)
        {
            var query = $@" DELETE FROM ClosingFileWorkflowInstances 
                            WHERE ClosingFileId IN 
                            (SELECT Id FROM ClosingFiles WHERE ClosingNumber = '{closingNumber}')";
            var delete = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return this;
        }
        public string GetIdOfJustCreatedWorkflow(string notificationText)
        {
            var query = $@"SELECT Id FROM ClosingFileWorkflowItemNotifications WHERE OptionsJson like '%{notificationText}%'";
            var id = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return id;
        }
        public ClosingFileWorkflowRepository SetNeededWorkWeek()
        {
            var query = $@"UPDATE CalendarWorkWeek SET WorkWeek = 127";
            var update = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return this;
        }
        public string GetIdOfJustCreatedWorkflowItems(string closinfFileName)
        {
            Thread.Sleep(100);
            var query = $@"select Id FROM ClosingFileWorkflowItems 
                            WHERE ClosingFileWorkflowSectionId IN 
                              (SELECT Id FROM ClosingFileWorkflowSections WHERE ClosingFileWorkflowInstanceId  IN 
                              (Select Id FROM ClosingFileWorkflowInstances WHERE ClosingFileId IN 
                              (SELECT Id FROM ClosingFiles WHERE ClosingNumber = '{closinfFileName}')))";
            var id = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return id;
        }
        public void InsertWorkflowTemplate(int companyId, int memberId, string workflowTemplateName)
        {
            var query = $@" Insert Into WorkflowTemplates (Name, CompanyId, CreatedBy_Id, DateCreated, DateUpdated, UpdatedBy_Id)
                            values ('{workflowTemplateName}', {companyId}, {memberId}, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(),{memberId})";

            var querySql = $@"INSERT INTO WorkflowTemplates (Name, CompanyId, CreatedBy_Id, DateCreated, DateUpdated, UpdatedBy_Id) 
                            VALUES ('{workflowTemplateName}', {companyId}, {memberId}, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), {memberId})";

            var result = _conn.Connection.QuerySingleOrDefault<int>(querySql);
            //var id = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
        }
        public string GetWorkflowTemplateId(string fileNumber)
        {
            var query = $@"Select Max(Id) From WorkflowTemplates Where Name = '{fileNumber}'";
            var id = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return id;
        }
        internal ClosingFileWorkflowRepository InsertWorkflowSection(object workflowTemplateId, int userId)
        {
            var query = $@"  Insert Into WorkflowSections (Name, WorkflowTemplateId,  CreatedBy_Id, DateCreated, DateUpdated,  UpdatedBy_Id)
                              values('Section 1', {workflowTemplateId}, {userId}, SYSDATETIMEOFFSET (), SYSDATETIMEOFFSET (), {userId})";
            var insert = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return this;
        }
        public string GetWorkflowSectionId(object workflowTemplateId, int userId)
        {
            Thread.Sleep(100);
            var query = $@"Select Id from WorkflowSections where WorkflowTemplateId = '{workflowTemplateId}' and CreatedBy_Id = '{userId}'";
            var id = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return id;
        }
        internal int GetRoleId(int companyId)
        {
            var query = $@"Select Id From Roles Where CompanyId = '{companyId}' and RoleType = 1";
            var id = _conn.Connection.QueryFirstOrDefault<int>(query);
            return id;
        }
        public void InsertWorkflowItem(string workflowItemName, string workflowSectionId, object roleId, int userId)
        {
            var query = $@"Insert Into WorkflowItems (Name, WorkflowSectionId, Priority, RoleId, CreatedBy_Id, DateCreated, DateUpdated, UpdatedBy_Id )
                            values ('{workflowItemName}', '{workflowSectionId}', 1, '{roleId}', '{userId}', SYSDATETIMEOFFSET (), SYSDATETIMEOFFSET (), '{userId}')";
            var delete = _conn.Connection.QueryFirstOrDefault<string>(query);
        }
        internal void DeleteDataAfterWorkflowNotificationTest(string email)
        {
            var query = $@" DELETE FROM ClosingFileWorkflowItemComments WHERE ClosingFileWorkflowItemId in (SELECT id FROM ClosingFileWorkflowItems WHERE ClosingFileWorkflowSectionId in (SELECT id FROM ClosingFileWorkflowSections WHERE CreatedBy_Id in (SELECT id FROM Members WHERE UserName = '{email}')))
                            DELETE FROM ClosingFileWorkflowItemNotifications WHERE ClosingFileWorkflowItemId in (SELECT id FROM ClosingFileWorkflowItems WHERE ClosingFileWorkflowSectionId in (SELECT id FROM ClosingFileWorkflowSections WHERE CreatedBy_Id in (SELECT id FROM Members WHERE UserName = '{email}')))
                            DELETE FROM ClosingFileWorkflowItems WHERE ClosingFileWorkflowSectionId in (SELECT id FROM ClosingFileWorkflowSections WHERE CreatedBy_Id in (SELECT id FROM Members WHERE UserName = '{email}'))
                            DELETE FROM ClosingFileWorkflowSections WHERE CreatedBy_Id =(SELECT id FROM Members WHERE UserName = '{email}')
                            DELETE FROM ClosingFileWorkflowInstances WHERE CreatedBy_Id = (SELECT Id FROM Members WHERE UserName = '{email}')
                            Delete from WorkflowItems where CreatedBy_Id = (SELECT Id FROM Members WHERE UserName = '{email}')
                            Delete from WorkflowSections where CreatedBy_Id = (SELECT Id FROM Members WHERE UserName = '{email}')
                            Delete from WorkflowTemplates where CreatedBy_Id = (SELECT Id FROM Members WHERE UserName = '{email}')";
            var delete = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
        }
        public void DeleteDataAfterTestAddWorkflow(string email)
        {
            var query = $@"DELETE FROM ClosingFileWorkflowItems WHERE ClosingFileWorkflowSectionId in (SELECT id FROM ClosingFileWorkflowSections WHERE CreatedBy_Id in (SELECT id FROM Members WHERE UserName = '{email}'))
                    DELETE FROM ClosingFileWorkflowSections WHERE CreatedBy_Id =(SELECT id FROM Members WHERE UserName = '{email}')
                    DELETE FROM ClosingFileWorkflowInstances WHERE CreatedBy_Id = (SELECT Id FROM Members WHERE UserName = '{email}')
                    Delete from WorkflowItems where CreatedBy_Id = (SELECT Id FROM Members WHERE UserName = '{email}')
                    Delete from WorkflowSections where CreatedBy_Id = (SELECT Id FROM Members WHERE UserName = '{email}')
                    Delete from WorkflowTemplates where CreatedBy_Id = (SELECT Id FROM Members WHERE UserName = '{email}')";
            var delete = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
        }
    }
}

