using AppleUsed.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class Ad
    {
        public string AdId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<AdPhotos> Photos { get; set; }

        public virtual AdViews AdViews { get; set; }

        public virtual Characteristics Characteristics { get; set; }

        public virtual Purchase Purchased { get; set; }

        public virtual ApplicationUser User { get; set; }

        public Ad()
        {
            Photos = new List<AdPhotos>();
        }
    }
}
