﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITDesk.Models.Request
{
    public class ResetPasswordResponse
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
