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
    public class AdDTO
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
        //public int AdId { get; set; }

        //[Required]
        //public string Title { get; set; }

        //[Required]
        //public string Description { get; set; }

        //[Required]
        //public decimal Price { get; set; }

        //public DateTime DateCreated { get; set; }
        //public DateTime DateUpdated { get; set; }

        //public List<CityArea> CityAreasList { get; set; }
        //public string SelectedCityArea { get; set; }
        //public int SelectedCityAreaId { get; set; }

        //public List<City> CityesList { get; set; }
        //public int SelectedCityId { get; set; }
        //public string SelectedCity { get; set; }

        //public List<AdPhotos> PhotosForEdit { get; set; }

        //public List<string> PhotosSmallSizeList { get; set; }
        //public List<string> PhotosAvgSizeList { get; set; }
        //public List<string> PhotosBigSizeList { get; set; }

        //public int AdViews { get; set; }

        //public int NotDeliveredMessageCount { get; set; }

        //public List<ProductTypes> ProductTypesList { get; set; }
        //[Required]
        //public string SelectedProductType { get; set; }
        //public int SelectedProductTypeId { get; set; }

        //public List<ProductModels> ProductModelsList { get; set; }
        //[Required]
        //public string SelectedProductModel { get; set; }
        //public int SelectedProductModelId { get; set; }

        //public List<ProductMemories> ProductMemoriesList { get; set; }
        //[Required]
        //public string SelectedProductMemory { get; set; }
        //public int SelectedProductMemoryId { get; set; }

        //public List<ProductColors> ProductColorsList { get; set; }
        //[Required]
        //public string SelectedProductColor { get; set; }
        //public int SelectedProductColorId { get; set; }

        //public List<ProductStates> ProductStatesList { get; set; }
        //[Required]
        //public string SelectedProductStates { get; set; }
        //public int SelectedProductStatesId { get; set; }

        //public List<PurchaseDTO> Purhcases { get; set; } 

        //public int AdStatusId { get; set; }
        //public bool IsModerate { get; set; }

        //public DateTime LastUpAd { get; set; }
        //public ApplicationUser User { get; set; }
    }
}
