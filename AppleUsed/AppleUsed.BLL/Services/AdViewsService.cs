using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Services
{
    public class AdViewsService : IAdViewsService, IDisposable
    {
        private readonly AppDbContext _db;

        public AdViewsService(AppDbContext db)
        {
            _db = db;
        }

        public async Task UpdateViewsAd(int adId)
        {
            var adViews = _db.AdViews.Where(x => x.AdId == adId).FirstOrDefault();

            adViews.SumViews += 1;
            try
            {
                _db.AdViews.Update(adViews);
                await _db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task ResetViews(int adId)
        {
            var adViews = _db.AdViews.Where(x => x.AdId == adId).FirstOrDefault();

            adViews.SumViews = 0;
            try
            {
                _db.AdViews.Update(adViews);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;
            }
        }
    }
}
