using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class ProductColors
    {
        public string ProductColorsId { get; set; }
        public string Name { get; set; }

        public string ProductTypeId { get; set; }
        public ProductTypes ProductType { get; set; }
    }
}
