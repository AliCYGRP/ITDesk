using System;
using System.Collections.Generic;

namespace ITDesk.Models
{
    public partial class DeviceCategory
    {
        public DeviceCategory()
        {
            DeviceInfo = new HashSet<DeviceInfo>();
        }

        public int CategoryId { get; set; }
        public string DeviceType { get; set; }

        public ICollection<DeviceInfo> DeviceInfo { get; set; }
    }
}
