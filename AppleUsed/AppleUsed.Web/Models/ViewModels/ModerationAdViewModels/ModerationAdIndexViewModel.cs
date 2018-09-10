using AppleUsed.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Models.ViewModels.ModerationAdViewModels
{
    public class ModerationAdIndexViewModel
    {
        public List<AdDTO> AdList { get; set; }
        public string StatusMessage { get; set; }
    }
}
