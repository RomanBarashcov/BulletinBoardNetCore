using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class ProductTypes
    {
        public string ProductTypesId { get; set; }
        public string Name { get; set; }

        public List<ProductModels> ProductModels { get; set; }
    }
}
