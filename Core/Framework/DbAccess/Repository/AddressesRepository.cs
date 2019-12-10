using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.DbAccess.Repository
{
    public class AddressesRepository
    {
        private DataBaseConnection _conn;
        public AddressesRepository(DataBaseConnection connection)
        {
            _conn = connection;
        }
        public void InsertAdress(int userId)
        {
            var query = $@" Insert Into Addresses (AddressLine1, City, StateId, ZipCode, CreatedBy_Id, DateCreated, DateUpdated, UpdatedBy_Id)
	                         values ('222 Berkeley St', 'Boston', 21, 02116, {userId}, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), {userId})";
            var insert = _conn.Connection.QueryFirstOrDefault<string>(query); ;
        }
        public int GetAddressId(int userId)
        {
            var query = $@" Select Id From Addresses Where CreatedBy_Id = {userId}";
            var addressId = _conn.Connection.QueryFirstOrDefault<int>(query);
            return addressId;
        }

    }
}
