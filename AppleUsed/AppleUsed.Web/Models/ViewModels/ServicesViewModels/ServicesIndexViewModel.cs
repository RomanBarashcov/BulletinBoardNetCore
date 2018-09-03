using AppleUsed.BLL.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Models.ViewModels.ServicesViewModels
{
    public class ServicesIndexViewModel : BaseServicesViewModel
    {
        public AdDTO Ad { get; set; }
        public List<ServiceDTO> Services { get; set; }
        public int SelectedAdId { get; set; }
    }
}
