using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITDesk.Models.Response
{
    public class LoginResponse
    {
        public string TokenString { get; set; }
        public bool Role { get; set; }

        public LoginResponse(string TokenString, bool Role)
        {
            this.TokenString = TokenString;
            this.Role = Role;
        }
    }
}
