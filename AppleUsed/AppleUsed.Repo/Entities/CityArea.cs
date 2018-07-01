using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class CityArea
    {
        public string CityAreaId { get; set; }
        public string Name { get; set; }

        public List<City> Cities { get; set; }
    }
}
