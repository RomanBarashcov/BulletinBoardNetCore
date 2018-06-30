using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class ProductTypes
    {
        public string ProductTypesId { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<ProductModels> ProductModels { get; set; }
        public ProductTypes()
        {
            ProductModels = new List<ProductModels>();
        }
    }
}
