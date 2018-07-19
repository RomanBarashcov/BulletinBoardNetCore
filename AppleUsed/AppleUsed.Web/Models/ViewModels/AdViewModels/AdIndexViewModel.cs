using AppleUsed.BLL.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Models.ViewModels.AdViewModels
{
    public class AdIndexViewModel
    {
        public List<AdDTO> AdDTO { get; set; } 

        public string PriceFilterFrom { get; set; }
        public string PriceFilterTo { get; set; }
        public Dictionary<string, bool> ProductModelsFilterItems { get; set; }
        public Dictionary<string, bool> ProductMemmorieFilterItems { get; set; }
        public Dictionary<string, bool> ProductColorsFilterItems { get; set; }

    }
}
