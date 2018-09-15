using AppleUsed.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppleUsed.DAL.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<ApplicationUser> GetUsers();
    }
}
