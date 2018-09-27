using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.BLL.DTO
{
    [NotMapped]
    public class CityAreaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CityDTO> Cities { get; set; }
    }
}
