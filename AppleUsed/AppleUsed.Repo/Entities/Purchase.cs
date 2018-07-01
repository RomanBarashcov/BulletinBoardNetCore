using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class Purchase
    {
        public string PurchaseId { get; set; }

        public Services Services { get; set; }

        public decimal TotalCost { get; set; }
        public DateTime DateOfPayment { get; set; }
        public DateTime StartDateActiveBLL { get; set; }
        public DateTime EndDateActiveBLL { get; set; }
        public bool IsPayed { get; set; }

        public string AdId { get; set; }
        public Ad Ad { get; set; }
    }
}
