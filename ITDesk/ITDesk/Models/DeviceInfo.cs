using System;
using System.Collections.Generic;

namespace ITDesk.Models
{
    public partial class DeviceInfo
    {
        public int DeviceId { get; set; }
        public string UniqueCode { get; set; }
        public string DeviceName { get; set; }
        public int CategoryId { get; set; }
        public DateTime? AssignedDate { get; set; }
        public int? EmployeeId { get; set; }
        public bool? IsAssigned { get; set; }
        public string AssignedBy { get; set; }
        public string QrCode { get; set; }

        public DeviceCategory Category { get; set; }
        public EmployeeInfo Employee { get; set; }
    }
}
