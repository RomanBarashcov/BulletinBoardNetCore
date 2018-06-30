using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.Data.Entities
{
    public class Ad
    {
        public int AdId { get; set; }
        public string Title { get; set; }
        public string MobilePhone { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdating { get; set; }
        public List<AdPhotos> Photos { get; set; }
        public AdViews AdViews { get; set; }
        public Characteristics Characteristics { get; set; }
        public Purchase Purchased { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
