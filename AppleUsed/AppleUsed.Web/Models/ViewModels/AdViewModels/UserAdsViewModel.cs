using AppleUsed.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Models.ViewModels.AdViewModels
{
    public class UserAdsViewModel
    {
        public UserDTO User { get; set; }
        public List<AdDTO> Ads { get; set; }
    }
}
