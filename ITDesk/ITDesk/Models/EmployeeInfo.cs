using System;
using System.Collections.Generic;

namespace ITDesk.Models
{
    public partial class EmployeeInfo
    {
        public EmployeeInfo()
        {
            DeviceInfo = new HashSet<DeviceInfo>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string Designation { get; set; }
        public string Password { get; set; }
        public bool Role { get; set; }

        public ICollection<DeviceInfo> DeviceInfo { get; set; }
    }
}
