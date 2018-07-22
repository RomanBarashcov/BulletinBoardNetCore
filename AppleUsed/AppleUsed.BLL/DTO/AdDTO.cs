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

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public List<CityArea> CityAreasList { get; set; }
        public string SelectedCityArea { get; set; }

        public List<City> CityesList { get; set; }
        public string SelectedCity { get; set; }

        public List<AdPhotos> PhotosList { get; set; }

        public int AdViews { get; set; }

        public List<ProductTypes> ProductTypesList { get; set; }
        [Required]
        public string SelectedProductType { get; set; }
        public int SelectedProductTypeId { get; set; }

        public List<ProductModels> ProductModelsList { get; set; }
        [Required]
        public string SelectedProductModel { get; set; }
        public int SelectedProductModelId { get; set; }

        public List<ProductMemories> ProductMemoriesList { get; set; }
        [Required]
        public string SelectedProductMemory { get; set; }
        public int SelectedPoductMemoryId { get; set; }

        public List<ProductColors> ProductColorsList { get; set; }
        [Required]
        public string SelectedProductColor { get; set; }
        public int SelectedProductColorId { get; set; }

        public List<ProductStates> ProductStatesList { get; set; }
        [Required]
        public string SelectedProductStates { get; set; }


        public ApplicationUser User { get; set; }
    }
}
