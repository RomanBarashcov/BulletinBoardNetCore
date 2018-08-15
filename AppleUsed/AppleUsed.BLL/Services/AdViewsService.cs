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
    public class AdViewsService : IAdViewsService
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
    }
}
