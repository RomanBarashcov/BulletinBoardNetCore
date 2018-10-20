using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.BLL.DTO
{
    [NotMapped]
    public class AdDTO : BaseAdDTO
    {
        public int AdId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public City City { get; set; }
        public ICollection<AdPhotos> Photos { get; set; }
        public AdViews AdViews { get; set; }
        public Characteristics Characteristics { get; set; }
        public ICollection<Purchase> Purhcases { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int AdStatusId { get; set; }
        public bool IsModerate { get; set; }
        public int NotDeliveredMessageCount { get; set; }

    }
}
