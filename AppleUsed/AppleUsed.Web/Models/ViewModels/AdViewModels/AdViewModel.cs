using AppleUsed.BLL.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Models.ViewModels.AdViewModels
{
    public class AdViewModel
    {
        public AdDTO AdDTO { get; set; }
        public SelectList CityAreasSelectList { get; set; }
        public SelectList CityesSelectList { get; set; }
        public SelectList ProductTypesSelectList { get; set; }
        public SelectList ProductModelsSelectList { get; set; }
        public SelectList ProductMemoriesSelectList { get; set; }
        public SelectList ProductColorsSelectList { get; set; }
        public SelectList ProductStatesSelectList { get; set; }

    }
}
