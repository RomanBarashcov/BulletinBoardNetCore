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

        public decimal TotalCost { get; set; }
        public DateTime DateOfPayment { get; set; }
        public DateTime StartDateActiveBLL { get; set; }
        public DateTime EndDateActiveBLL { get; set; }
        public bool IsPayed { get; set; }

        [ForeignKey("AdId")]
        public virtual Ad Ads { get; set; }
    }
}
