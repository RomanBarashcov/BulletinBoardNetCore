using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class ProductColors
    {
        public int ProductColorsId { get; set; }
        public string Name { get; set; }

        [ForeignKey("ProductTypeId")]
        public virtual ProductTypes ProductType { get; set; }
    }
}
