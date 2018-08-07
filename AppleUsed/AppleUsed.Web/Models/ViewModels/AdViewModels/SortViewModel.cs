using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Models.ViewModels.AdViewModels
{
    public class SortViewModel
    {
        public string SelectedOptionValue { get; set; }
        public SelectList SortOptionList { get; set; }
    }
}
