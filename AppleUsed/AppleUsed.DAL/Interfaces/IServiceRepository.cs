using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Interfaces
{
    public interface IServiceRepository : IDisposable
    {
        IQueryable<Services> GetAllServices();

        Task<Services> FindServiceByIdAsync(int serviceId);

        Task<int> AddServiceAsync(Services services);

        Task<int> UpdateService(Services services);

        Task DeleteService(int id);

        IQueryable<Purchase> GetAllUserPurchaseByAds(IQueryable<Ad> adQuery);
    }
}
