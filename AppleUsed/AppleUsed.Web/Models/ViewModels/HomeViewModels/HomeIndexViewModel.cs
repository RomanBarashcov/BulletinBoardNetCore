using AppleUsed.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Models.ViewModels.HomeViewModels
{
    public class HomeIndexViewModel
    {
        public List<AdDTO> LatestAds { get; set; }
        public List<AdDTO> VipAds { get; set; }
    }
}
