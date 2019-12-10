using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laren.E2ETests.Core.Framework.DbAccess.Repository
{
    public class WidgetsMembersRepository
    {
        private DataBaseConnection _conn;
        public WidgetsMembersRepository(DataBaseConnection connection)
        {
            _conn = connection;
        }

        internal void InsertWidgetsMembers(int widgetId, int companyId, int locationId, int userId)
        {
            var json = "{\"buttons\":[{\"label\":\"Edit Widget\"},{\"label\":\"Hide\"},{\"label\":\"Widgets\"}],\"mainTitle\":\"Property Maps\",\"widgetOrder\":0,\"viewAllVisible\":true,\"config\":[{\"label\":\"\",\"field\":\"\",\"showItem\":false}]}";
            var query = $@"Insert into WidgetsMembers ([DateCreated], [CreatedBy_Id], [DateUpdated], [UpdatedBy_Id], [WidgetId], [CompanyId], [LocationId], [MemberId], [WidgetOrder], [OptionsJson])
                values (SYSDATETIMEOFFSET(), {userId}, SYSDATETIMEOFFSET(), {userId}, {widgetId}, {companyId}, {locationId}, {userId}, 0, '{json}')";
            _conn.Connection.Query<string>(query);
        }

    }
}
