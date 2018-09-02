using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.BLL.DTO
{
    [NotMapped]
    public class PurchaseDTO
    {
        public int PurchaseId { get; set; }

        [Required]
        public decimal TotalCost { get; set; }
        public DateTime DateOfPayment { get; set; }

        [Required]
        public DateTime StartDateService { get; set; }

        [Required]
        public DateTime EndDateService { get; set; }
        public bool IsPayed { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public int ServicesId { get; set; }
        public int ServiceActiveTimeId { get; set; }

        [Required]
        public int AdId { get; set; }
    }
}
