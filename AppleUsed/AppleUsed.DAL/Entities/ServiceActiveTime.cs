using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class ServiceActiveTime
    {
        public int ServiceActiveTimeId { get; set; }
        public decimal Cost { get; set; }
        public int DaysOfActiveService { get; set; }

        public int ServiceId { get; set; }
    }
}
