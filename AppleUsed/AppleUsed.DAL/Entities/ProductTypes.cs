using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class ProductTypes
    {
        public int ProductTypesId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductModels> ProductModels { get; set; }

        public ProductTypes()
        {
            ProductModels = new List<ProductModels>();
        }
    }
}
