using AppleUsed.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Models.ViewModels.AdViewModels
{
    public class AdDetailsViewModel
    {
        public AdDTO AddDetails { get; set; }
        public List<AdDTO> SimilarAds { get; set; }
        public List<AdDTO> OtherAdsByAuthor { get; set; }
    }
}
