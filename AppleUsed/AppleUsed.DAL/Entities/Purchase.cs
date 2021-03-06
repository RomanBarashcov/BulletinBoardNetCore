﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class Purchase
    {
        public int PurchaseId { get; set; }

        public decimal TotalCost { get; set; }
        public DateTime DateOfPayment { get; set; }
        public DateTime StartDateService { get; set; }
        public DateTime EndDateService { get; set; }
        public bool IsPayed { get; set; }
        public bool IsActive { get; set; }

        public int ServicesId { get; set; }
        public int ServiceActiveTimeId { get; set; }
        public int AdId { get; set; }

    }
}
