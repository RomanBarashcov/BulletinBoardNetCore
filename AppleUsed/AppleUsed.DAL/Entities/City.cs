using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }

        [ForeignKey("CityAreaId")]
        public virtual CityArea CityArea { get; set; }
    }
}
