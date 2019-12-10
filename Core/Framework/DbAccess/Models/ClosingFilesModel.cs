using System;
using System.Collections.Generic;
using System.Text;

namespace Laren.E2ETests.Core.Framework.DbAccess.Models
{
    public class ClosingFilesModel
    {
        public string EscrowAccount { get; set; }
        public decimal PurchasePrice { get; set; }
        public string ClosingNumber { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TrustName { get; set; }
    }
}
