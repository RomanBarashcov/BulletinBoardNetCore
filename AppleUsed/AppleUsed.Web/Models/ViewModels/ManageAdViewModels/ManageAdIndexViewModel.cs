using AppleUsed.BLL.DTO;
using AppleUsed.Web.Models.ViewModels.AdViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Models.ViewModels.ManageAdViewModels
{
    public class ManageAdIndexViewModel
    {
        public List<AdDTO> AdList { get; set; }
        public int SelectedAdStatus { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
