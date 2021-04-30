using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITDesk.Models.Request
{
    public class LoginRequest
    {
        public string EmployeeEmail { get; set; }
        public string Password { get; set; }
    }
}
