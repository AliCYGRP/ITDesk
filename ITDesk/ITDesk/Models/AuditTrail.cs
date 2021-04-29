using System;
using System.Collections.Generic;

namespace ITDesk.Models
{
    public partial class AuditTrail
    {
        public int AuditId { get; set; }
        public string UniqueCode { get; set; }
        public string EmployeeEmail { get; set; }
        public DateTime Date { get; set; }
    }
}
