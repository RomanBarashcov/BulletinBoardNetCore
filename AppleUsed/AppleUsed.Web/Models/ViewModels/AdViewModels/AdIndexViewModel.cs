using AppleUsed.BLL.DTO;
using AppleUsed.Web.Models.ViewModels.AccountViewModels;
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
        public List<AdDTO> AdList { get; set; }

        public SearchFilterViewModel SearchFilter { get; set; }

        public SortViewModel SortViewModel { get; set; }

        public FilterViewModel Filter { get; set; }

        public PageViewModel PageViewModel { get; set; }

    }
}
