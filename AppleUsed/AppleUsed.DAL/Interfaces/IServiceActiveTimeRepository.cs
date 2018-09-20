using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Interfaces
{
    public interface IServiceActiveTimeRepository : IDisposable
    {
        IQueryable<ServiceActiveTime> GetAllPurchase();

        Task<ServiceActiveTime> FindServiceActiveTimeByIdAsync(int purchaseId);

        Task<int> AddServiceActiveTimeAsync(ServiceActiveTime serviceActiveTime);

        Task AddServiceActiveTimeRange(List<ServiceActiveTime> serviceActiveTime);

        Task<int> UpdateServiceActiveTime(ServiceActiveTime serviceActiveTime);

        Task UpdateServiceActiveTimeRange(List<ServiceActiveTime> serviceActiveTime);

        Task DeleteServiceActiveTime(int id);
    }
}
