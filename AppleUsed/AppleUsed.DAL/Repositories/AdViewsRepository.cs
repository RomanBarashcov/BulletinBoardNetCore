using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using AppleUsed.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Repositories
{
    public class AdViewsRepository : IAdViewsRepository
    {
        private AppDbContext _db;

        public AdViewsRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<AdViews> FindByAdIdAsync(int adId)
        {
           return await _db.AdViews.Where(a => a.AdId == a.AdId).FirstOrDefaultAsync();
        }

        public async Task Update(AdViews adViews)
        {
            _db.AdViews.Update(adViews);
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
