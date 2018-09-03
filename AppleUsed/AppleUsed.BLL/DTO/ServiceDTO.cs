using AppleUsed.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.BLL.DTO
{
    [NotMapped]
    public class ServiceDTO
    {
        public int ServicesId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public List<ServiceActiveTime> ServiceActiveTimes { get; set; }

        public int SelectedServiceActiveDaysId { get; set; }
        public SelectList SelectListServiceActiveDays { get; set; }
    }
}
