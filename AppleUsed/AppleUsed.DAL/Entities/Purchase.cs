using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class Purchase
    {
        public int PurchaseId { get; set; }

        [ForeignKey("ServicesId")]
        public virtual Services Services { get; set; }

        [ForeignKey("AdId")]
        public virtual Ad Ad { get; set; }

        public decimal TotalCost { get; set; }
        public DateTime DateOfPayment { get; set; }
        public DateTime StartDateService { get; set; }
        public DateTime EndDateService { get; set; }
        public bool IsPayed { get; set; }

        
    }
}
