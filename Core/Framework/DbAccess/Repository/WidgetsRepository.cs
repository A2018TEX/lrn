using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laren.E2ETests.Core.Framework.DbAccess.Repository
{
    public class WidgetsRepository
    {
        private DataBaseConnection _conn;

        public WidgetsRepository(DataBaseConnection connection)
        {
            _conn = connection;
        }
        public int GetWidggetId(string WidgetName)
        {
            var query = $@"Select Id From Widgets Where Name = '{WidgetName}'";
            var id = Convert.ToInt32(_conn.Connection.Query<string>(query).FirstOrDefault());
            return id;
        }
        public void DeleteWidgetsData(string email)
        {
            var query = $@"delete [WidgetsMembers] where WidgetId in(Select id from [Widgets] where [UpdatedBy_Id] in(SELECT Id FROM Members WHERE UserName ='{email}'))
                           delete [Widgets] where [UpdatedBy_Id] in(SELECT Id FROM Members WHERE UserName ='{email}')";
            var id = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
        }
    }
}
