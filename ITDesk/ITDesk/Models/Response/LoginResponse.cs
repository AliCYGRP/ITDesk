using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITDesk.Models.Response
{
    public class LoginResponse
    {
        public int EmployeeId { get; set; }
        public string TokenString { get; set; }
        public bool Role { get; set; }

        public LoginResponse(int EmployeeId, string TokenString, bool Role)
        {
            this.EmployeeId = EmployeeId;
            this.TokenString = TokenString;
            this.Role = Role;
        }
    }
}
