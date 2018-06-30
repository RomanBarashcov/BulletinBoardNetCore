using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class City
    {
        public string CityId { get; set; }
        public string Name { get; set; }

        public virtual CityArea CityArea { get; set; }
    }
}
