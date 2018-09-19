using AppleUsed.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        IQueryable<ApplicationUser> GetUsers();
        Task<ApplicationUser> FindByIdAsync(string userId);
    }
}
