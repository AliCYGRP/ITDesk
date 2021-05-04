using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITDesk.Models
{
    public class CategoryList
    {
        public int CategoryId { get; set; }
        public string DeviceType { get; set; }
        public int TotalCount { get; set; }
        public int FreeCount { get; set; }
        public int AllocatedCount { get; set; }
    }
}
