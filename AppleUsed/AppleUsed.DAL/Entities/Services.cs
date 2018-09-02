using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class Services
    {
        public int ServicesId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<ServiceActiveTime> ServiceActiveTimes { get; set; }
        public Services()
        {
            ServiceActiveTimes = new List<ServiceActiveTime>();
        }
    }
}
