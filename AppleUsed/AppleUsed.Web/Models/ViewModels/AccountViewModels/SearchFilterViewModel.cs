using AppleUsed.BLL.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Models.ViewModels.AccountViewModels
{
    public class SearchFilterViewModel
    {
        public SelectList ProductTypesOptionList { get; set; }
        public int SelectedProductTypeId { get; set; }
        public string SearchByCity { get; set; }
    }
}
