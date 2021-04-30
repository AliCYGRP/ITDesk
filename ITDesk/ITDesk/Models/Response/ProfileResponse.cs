using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITDesk.Models.Response
{
    public class ProfileResponse
    {
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string Designation { get; set; }

        public ProfileResponse(string EmployeeName, string EmployeeEmail, string Designation)
        {
            this.EmployeeName = EmployeeName;
            this.EmployeeEmail = EmployeeEmail;
            this.Designation = Designation;
        }
    }
}
