using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.BLL.DTO
{
    [NotMapped]
    public class BaseAdDTO
    {
        public List<string> PhotosSmallSizeList { get; set; }
        public List<string> PhotosAvgSizeList { get; set; }
        public List<string> PhotosBigSizeList { get; set; }
        public List<AdPhotos> PhotosForEdit { get; set; }

        public string SelectedProductType { get; set; }
        public int SelectedProductTypeId { get; set; }

        public string SelectedProductModel { get; set; }
        public int SelectedProductModelId { get; set; }

        public string SelectedProductMemory { get; set; }
        public int SelectedProductMemoryId { get; set; }

        public string SelectedProductColor { get; set; }
        public int SelectedProductColorId { get; set; }

        public string SelectedProductStates { get; set; }
        public int SelectedProductStateId { get; set; }

        public string SelectedCity { get; set; }
        public int SelectedCityId { get; set; }

        public string SelectedCityArea { get; set; }
        public int SelectedCityAreaId { get; set; }

    }
}
