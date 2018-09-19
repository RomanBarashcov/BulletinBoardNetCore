using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Interfaces
{
    public interface IPurchaseRepository : IDisposable
    {
        IQueryable<Purchase> GetAllPurchase();

        Task<Purchase> FindPurchaseByIdAsync(int purchaseId);

        Task<int> AddPurchaseAsync(Purchase purchase);

        Task<int> UpdatePurchase(Purchase purchase);

        Task UpdatePurchaseRange(IQueryable<Purchase> purchases);

        Task DeletePurchase(int id);

        IQueryable<Purchase> GetAllUserPurchaseByAds(IQueryable<Ad> adQuery);
    }
}
