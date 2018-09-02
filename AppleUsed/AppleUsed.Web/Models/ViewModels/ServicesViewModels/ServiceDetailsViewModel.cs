using AppleUsed.BLL.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Models.ViewModels.ServicesViewModels
{
    public class ServiceDetailsViewModel : BaseServicesViewModel
    {
        public ServiceDTO ServiceDetail { get; set; }
    }
}
