﻿using AppleUsed.DAL.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class Ad
    {
        public int AdId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        public virtual ICollection<AdPhotos> Photos { get; set; }

        [ForeignKey("AdViewsId")]
        public virtual AdViews AdViews { get; set; }

        [ForeignKey("CharacteristicsId")]
        public virtual Characteristics Characteristics { get; set; }

        [ForeignKey("PurchasedId")]
        public virtual Purchase Purchased { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public Ad()
        {
            Photos = new List<AdPhotos>();
        }
    }
}
