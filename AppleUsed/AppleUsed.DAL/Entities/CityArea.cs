using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class CityArea
    {
        public int CityAreaId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }
        public CityArea()
        {
            Cities = new List<City>();
        }
    }
}
