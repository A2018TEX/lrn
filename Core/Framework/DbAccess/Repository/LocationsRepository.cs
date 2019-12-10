using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Laren.E2ETests.Core.Framework.DbAccess.Repository
{
    public class LocationsRepository
    {
        private DataBaseConnection _conn;
        public LocationsRepository(DataBaseConnection connection)
        {
            _conn = connection;
        }
        public LocationsRepository InsertLocation(int companyId, int addressId, int userId)
        {
            var query = $@" Insert Into Locations	([Name], CompanyId, AddressId, Phone, Email, CreatedBy_Id, DateCreated, DateUpdated, UpdatedBy_Id)
                          values ( 'LocationAutotest', {companyId}, {addressId}, '(941) 977-7777', 'test@test.test', {userId}, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), {userId})";
            var insert = _conn.Connection.Query<string>(query).ToList().FirstOrDefault();
            return this;
        }
        public int GetLocationId(int companyId, int addressId, int userId)
        {
            var query = $@"SELECT Id FROM Locations Where CompanyId = {companyId} AND AddressId = {addressId}";
            var id = _conn.Connection.QueryFirstOrDefault<int>(query);
            return id;
        }
    }
}
