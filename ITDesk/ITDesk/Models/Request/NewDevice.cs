using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITDesk.Models.Request
{
    public class NewDevice
    {
        public string UniqueCode { get; set; }
        public string DeviceName { get; set; }
        public int CategoryId { get; set; }
    }
}
