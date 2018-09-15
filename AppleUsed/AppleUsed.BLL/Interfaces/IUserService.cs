using AppleUsed.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppleUsed.BLL.Interfaces
{
    public interface IUserService
    {
         IQueryable<UserDTO> GetUsers();
    }
}
