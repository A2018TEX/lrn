using NUnit.Framework;
using System.Data.Common;
using System.Data.SqlClient;

namespace Laren.E2ETests.Core.Framework.DbAccess
{
    public class DataBaseConnection
    {
        public DataBaseConnection(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            Connection.Open();
        }

        public void CloseConnection()
        {
            Connection.Close();
        }
        public DbConnection Connection { get; }
    }
}
