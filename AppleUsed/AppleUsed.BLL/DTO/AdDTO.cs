using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.BLL.DTO
{
    [NotMapped]
    public class AdDTO
    {
        public int AdId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public City City { get; set; }
        public List<AdPhotos> Photos { get; set; }
        public AdViews AdViews { get; set; }
        public Characteristics Characteristics { get; set; }
        public Purchase Purchased { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
