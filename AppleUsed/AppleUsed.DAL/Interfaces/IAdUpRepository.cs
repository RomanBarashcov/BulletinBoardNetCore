using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Interfaces
{
    public interface IAdUpRepository : IDisposable
    {
        Task<AdUp> FindByAdIdAsync(int adId);

        Task<int> AddAsync(AdUp adUp);

        Task<int> Update(AdUp adUp);
    }
}
