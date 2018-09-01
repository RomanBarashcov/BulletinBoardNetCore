using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.BLL.DTO
{
    [NotMapped]
    public class ProductTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
