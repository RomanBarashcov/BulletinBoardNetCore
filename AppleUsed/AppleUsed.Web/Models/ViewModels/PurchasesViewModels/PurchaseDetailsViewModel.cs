using AppleUsed.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Models.ViewModels.PurchasesViewModels
{
    public class PurchaseDetailsViewModel : BasePurchasesViewModel
    {
        public PurchaseDTO PurhcaseDetail { get; set; }
        public ServiceDTO SelectedService { get; set; }
    }
}
