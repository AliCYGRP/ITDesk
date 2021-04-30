using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITDesk.Models.Response
{
    public class DeviceResponse
    {
        public string UniqueCode { get; set; }
        public string DeviceName { get; set; }
        public string AssignedDate { get; set; }
        public string EmployeeEmail { get; set; }
        public string AssignedBy { get; set; }
    }
}
