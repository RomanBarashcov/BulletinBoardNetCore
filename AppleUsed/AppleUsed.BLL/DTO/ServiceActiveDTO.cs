using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.BLL.DTO
{
    [NotMapped]
    public class ServiceActiveTimesDTO
    {
        public int ServiceActiveTimeId { get; set; }
        public decimal Cost { get; set; }
        public int DaysOfActiveService { get; set; }

        public int ServiceId { get; set; }
    }
}
