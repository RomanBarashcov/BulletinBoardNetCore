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
    public class ServiceActiveTimeRepository : IServiceActiveTimeRepository
    {
        private AppDbContext _db;

        public ServiceActiveTimeRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<ServiceActiveTime> GetAllPurchase()
        {
            return _db.ServiceActiveTimes;
        }

        public async Task<ServiceActiveTime> FindServiceActiveTimeByIdAsync(int purchaseId)
        {
            return await _db.ServiceActiveTimes.FindAsync(purchaseId);
        }

        public async Task<int> AddServiceActiveTimeAsync(ServiceActiveTime serviceActiveTime)
        {
            await _db.AddAsync(serviceActiveTime);
            await _db.SaveChangesAsync();
            return serviceActiveTime.ServiceActiveTimeId;
        }

        public async Task AddServiceActiveTimeRange(List<ServiceActiveTime> serviceActiveTime)
        {
            _db.ServiceActiveTimes.AddRange(serviceActiveTime);
            await _db.SaveChangesAsync();
        }

        public async Task<int> UpdateServiceActiveTime(ServiceActiveTime serviceActiveTime)
        {
            _db.ServiceActiveTimes.Update(serviceActiveTime);
            await _db.SaveChangesAsync();
            return serviceActiveTime.ServiceActiveTimeId;
        }

        public async Task DeleteServiceActiveTime(int id)
        {
            var oldPurchase = await FindServiceActiveTimeByIdAsync(id);
            _db.ServiceActiveTimes.Remove(oldPurchase);
            await _db.SaveChangesAsync();
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
