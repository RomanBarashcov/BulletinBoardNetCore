using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class ProductModels
    {
        public int ProductModelsId { get; set; }
        public string Name { get; set; }

        [ForeignKey("ProductTypesId")]
        public virtual ProductTypes ProductTypes { get; set; }
    }
}
