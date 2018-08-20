using AppleUsed.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Models.ViewModels.ServicesViewModels
{
    public class ServicesIndexViewModel : BaseServicesViewModel
    {
        public IEnumerable<ServiceDTO> Services { get; set; }
        public int SelectedAdId { get; set; }
    }
}
