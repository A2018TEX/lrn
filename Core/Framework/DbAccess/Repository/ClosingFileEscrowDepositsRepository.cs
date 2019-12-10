using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laren.E2ETests.Core.Framework.DbAccess.Repository
{
    class ClosingFileEscrowDepositsRepository
    {
        private DataBaseConnection _conn;
        public ClosingFileEscrowDepositsRepository(DataBaseConnection connection)
        {
            _conn = connection;
        }

        public decimal GetEscrowDeposit(string closingNumber)
        {
            var query = $@"SELECT EscrowDeposit FROM ClosingFileEscrowDeposits Where ClosingFileId in (Select Id From ClosingFiles where ClosingNumber ='{closingNumber}')";
            decimal purchPrice = _conn.Connection.QueryFirstOrDefault<decimal>(query);
            return purchPrice;
        }
    }
}

