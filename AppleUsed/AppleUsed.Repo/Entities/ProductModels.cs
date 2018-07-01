using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class ProductModels
    {
        public string ProductModelsId { get; set; }
        public string Name { get; set; }

        public string ProdyctTypesId { get; set; }
        public ProductTypes ProductTypes { get; set; }
    }
}
