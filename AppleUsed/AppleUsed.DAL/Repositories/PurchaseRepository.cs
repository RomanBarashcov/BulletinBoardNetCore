using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using AppleUsed.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private AppDbContext _db;

        public PurchaseRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<Purchase> GetAllPurchase()
        {
            return _db.Purchases;
        }

        public async Task<Purchase> FindPurchaseByIdAsync(int purchaseId)
        {
            return await _db.Purchases.FindAsync(purchaseId);
        }

        public async Task<int> AddPurchaseAsync(Purchase purchase)
        {
            await _db.AddAsync(purchase);
            await _db.SaveChangesAsync();
            return purchase.PurchaseId;
        }

        public async Task<int> UpdatePurchase(Purchase purchase)
        {
            _db.Purchases.Update(purchase);
            await _db.SaveChangesAsync();
            return purchase.PurchaseId;
        }

        public async Task UpdatePurchaseRange(IQueryable<Purchase> purchases)
        {
            _db.Purchases.UpdateRange(purchases);
            await _db.SaveChangesAsync();
        }

        public async Task DeletePurchase(int id)
        {
            var oldPurchase = await FindPurchaseByIdAsync(id);
            _db.Purchases.Remove(oldPurchase);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Purchase> GetAllUserPurchaseByAds(IQueryable<Ad> adQuery)
        {
            var purchases = (from ads in adQuery
                                join p in _db.Purchases on ads.AdId equals p.AdId
                                select new Purchase
                                {
                                    PurchaseId = p.PurchaseId,
                                    TotalCost = p.TotalCost,
                                    DateOfPayment = p.DateOfPayment,
                                    StartDateService = p.StartDateService,
                                    EndDateService = p.EndDateService,
                                    IsPayed = p.IsPayed,
                                    ServicesId = p.ServicesId,
                                    ServiceActiveTimeId = p.ServiceActiveTimeId,
                                    AdId = p.AdId
                                });

            return purchases;
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db = null;
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
