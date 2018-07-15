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

        public List<CityArea> CityAreasList { get; set; }
        public string SelectedCityArea { get; set; }

        public List<City> CityesList { get; set; }
        public string SelectedCity { get; set; }

        public List<AdPhotos> Photos { get; set; }
        public int AdViews { get; set; }

        public List<ProductTypes> ProductTypesList { get; set; }
        public string SelectedProductType { get; set; }

        public List<ProductModels> ProductModelsList { get; set; }
        public string SelectedProductModel { get; set; }

        public List<ProductMemories> ProductMemoriesList { get; set; }
        public string SelectedProductMemory { get; set; }

        public List<ProductColors> ProductColorsList { get; set; }
        public string SelectedProductColor { get; set; }

        public List<ProductStates> ProductStatesList { get; set; }
        public string SelectedProductStates { get; set; }

        //public Purchase Purchased { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string PhoneNumber { get; set; }
    }
}
