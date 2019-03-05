using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Helpers;
using AppleUsed.DAL.Identity;
using AppleUsed.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Repositories
{
    public class AdRepository : IAdRepository
    {
        private AppDbContext _db;

        public AdRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<Ad> GetAdQuery(
           Expression<Func<Ad, bool>> adExpression,
           Expression<Func<ProductTypes, bool>> ptExpression,
           Expression<Func<ApplicationUser, bool>> auExpression,
           Expression<Func<Purchase, bool>> pExpression)
        {
            var ads = _db.Ads.
                Include(x => x.City).
                Include(x => x.City.CityArea).
                Include(x => x.AdViews).
                Include(x => x.Characteristics).
                Include(x => x.Characteristics.ProductType).
                Include(x => x.Characteristics.ProductModel).
                Include(x => x.Characteristics.ProductMemorie).
                Include(x => x.Characteristics.ProductColor).
                Include(x => x.Characteristics.ProductState).
                Include(x => x.Purhcases).
                Include(x => x.ApplicationUser).
                Select(ad => new Ad
                {
                    AdId = ad.AdId,
                    Title = ad.Title,
                    Description = ad.Description,
                    Price = ad.Price,
                    DateCreated = ad.DateCreated,
                    DateUpdated = ad.DateUpdated,
                    Characteristics = ad.Characteristics,
                    City = ad.City,
                    Photos = ad.Photos.ToList(),
                    AdViews = ad.AdViews,
                    AdStatusId = ad.AdStatusId,
                    IsModerate = ad.IsModerate,
                    Purhcases = ad.Purhcases,
                    ApplicationUser = ad.ApplicationUser
                });

            return ads;
        }

        public async Task<Ad> FindAdByIdAsync(int id)
        {
            var ads = await GetAdQuery(
                x => x.AdId == id, 
                ptExpression: null, 
                auExpression: null, 
                pExpression: null).FirstOrDefaultAsync();
            return ads;
        }

        public async Task<List<Ad>> FindAdsByProductTypeId(int productTypeId)
        {
            var ads = await GetAdQuery(
                adExpression: null,
                x => x.ProductTypesId == productTypeId,
                auExpression: null,
                pExpression: null).ToListAsync();

            return ads;
        }

        public async Task<List<Ad>> GetAdsByUserName(string userName)
        {
            var user = await _db.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();

            var ads = await GetAdQuery(
                adExpression: null,
                ptExpression: null,
                x=> x.Id == user.Id,
                pExpression: null).ToListAsync();

            return ads;
        }

        public async Task<List<Ad>> FindActiveAdsByUserId(string userId)
        {
                var activeAds = await GetAdQuery(
                    x => x.AdStatusId == (int)AdStatuses.Activated && x.IsModerate, 
                    ptExpression: null, 
                    x => x.Id == userId,
                    pExpression: null).ToListAsync();

            return activeAds;
        }

        public async Task<List<Ad>> FindAdsByUserId(string userId)
        {
            var ads = await GetAdQuery(
                    adExpression: null,
                    ptExpression: null,
                    x => x.Id == userId,
                    pExpression: null).ToListAsync();

            return ads;
        }

        public async Task<int> AddAd(Ad ad)
        {
            try
            {
                var addResult = await _db.Ads.AddAsync(ad);
                var saveChangesResult = await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ad.AdId;
        }

        public async Task<int> UpdateAd(Ad ad)
        {
            try
            {
                _db.Update(ad);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return ad.AdId;
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
