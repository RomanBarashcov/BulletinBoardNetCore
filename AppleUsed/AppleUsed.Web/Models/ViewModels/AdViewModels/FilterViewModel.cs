using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Models.ViewModels.AdViewModels
{
    public class FilterViewModel
    {
        public string PriceFilterFrom { get; set; }
        public string PriceFilterTo { get; set; }

        public string SelectedProductType { get; set; }

        public List<ProductsModelFilter> ProductsModelFilters { get; set; }
        public List<ProductMemmoriesFilter> ProductMemmories { get; set; }
        public List<ProductsColorFilter> ProductsColors { get; set; }
    }
}
