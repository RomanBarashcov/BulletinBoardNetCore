using AppleUsed.BLL.DTO;
using AppleUsed.Web.Models.ViewModels.AdViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Models.ViewModels.UserViewModels
{
    public class UserIndexViewModel
    {
        public List<UserDTO> UserList { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
