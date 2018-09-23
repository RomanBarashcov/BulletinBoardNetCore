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

        private IQueryable<Ad> GetAdQuery(
            Expression<Func<Ad, bool>> adExpression , 
            Expression<Func<ProductTypes, bool>> ptExpression,
            Expression<Func<ApplicationUser, bool>> auExpression)
        {
            var adQuery = adExpression == null ? _db.Ads : _db.Ads.Where(adExpression);
            var productTypeQuery = ptExpression == null ? _db.ProductTypes : _db.ProductTypes.Where(ptExpression);
            var applicationUserQuery = auExpression == null ? _db.Users : _db.Users.Where(auExpression);

            var ads = (from ad in adQuery
                       join au in _db.AdUps on ad.AdId equals au.AdId
                       join c in _db.Cities on ad.City.CityId equals c.CityId
                       join ca in _db.CityAreas on c.CityArea.CityAreaId equals ca.CityAreaId
                       join ap in _db.AdPhotos.ToList() on ad.AdId equals ap.Ad.AdId into aPhotos
                       join av in _db.AdViews on ad.AdViews.AdViewsId equals av.AdViewsId
                       join ch in _db.Characteristics on ad.Characteristics.CharacteristicsId equals ch.CharacteristicsId
                       join pt in productTypeQuery on ch.ProductTypesId equals pt.ProductTypesId
                       join pm in _db.ProductModels on ch.ProductModelsId equals pm.ProductModelsId
                       join prm in _db.ProductMemories on ch.ProductMemoriesId equals prm.ProductMemoriesId
                       join pc in _db.ProductColors on ch.ProductColorsId equals pc.ProductColorsId
                       join prs in _db.ProductStates on ch.ProductStatesId equals prs.ProductStatesId
                       join sp in _db.Purchases on ad.AdId equals sp.AdId into servicePurchses
                       join u in applicationUserQuery on ad.ApplicationUser.Id equals u.Id
                       select new Ad
                       {
                           AdId = ad.AdId,
                           Title = ad.Title,
                           Description = ad.Description,
                           Price = ad.Price,
                           DateCreated = ad.DateCreated,
                           DateUpdated = ad.DateUpdated,
                           Characteristics = new Characteristics
                           {
                               CharacteristicsId = ch.CharacteristicsId,
                               ProductTypesId = ch.ProductTypesId,
                               ProductModelsId = ch.ProductModelsId,
                               ProductMemoriesId = ch.ProductMemoriesId,
                               ProductColorsId = ch.ProductColorsId,
                               ProductStatesId = ch.ProductStatesId
                           },
                           City = new City
                           {
                               CityId = c.CityId,
                               Name = c.Name,
                               CityArea = new CityArea
                               {
                                   CityAreaId = ca.CityAreaId,
                                   Name = ca.Name
                               }
                           },

                           Photos = aPhotos.ToList(),

                           AdViews = new AdViews
                           {
                               AdId = av.AdId,
                               AdViewsId = av.AdViewsId,
                               SumViews = av.SumViews
                           },
                           AdStatusId = ad.AdStatusId,
                           IsModerate = ad.IsModerate,
                           Purhcases = servicePurchses.Select(x =>
                              new Purchase
                              {
                                  PurchaseId = x.PurchaseId,
                                  TotalCost = x.TotalCost,
                                  DateOfPayment = x.DateOfPayment,
                                  StartDateService = x.StartDateService,
                                  EndDateService = x.EndDateService,
                                  IsPayed = x.IsActive,
                                  IsActive = x.IsPayed,
                                  ServicesId = x.ServicesId,
                                  ServiceActiveTimeId = x.ServiceActiveTimeId,
                                  AdId = x.AdId
                              }).ToList(),
                           ApplicationUser = new ApplicationUser
                           {
                               Id = u.Id,
                               Email = u.Email,
                               UserName = u.UserName
                           }

                       }).OrderByDescending(x => x.DateUpdated);

            return ads;
        }

        public IQueryable<Ad> GetActiveAds()
        {
            var ads = GetAdQuery(
                x => x.AdStatusId == (int)AdStatuses.Activated && x.IsModerate,
                ptExpression: null, auExpression: null);
            return ads;
        }

        public IQueryable<Ad> GetInProgressAds()
        {
            var ads = GetAdQuery(x => x.AdStatusId == (int)AdStatuses.InProgress, 
                ptExpression: null, auExpression: null);
            return ads;
        }

        public IQueryable<Ad> GetDeactivatedAds()
        {
            var ads = GetAdQuery(x => x.AdStatusId == (int)AdStatuses.Deactivated, 
                ptExpression: null, auExpression: null);
            return ads;
        }

        //public async Task<OperationDetails<IQueryable<AdDTO>>> GetActiveRandomVIPAds()
        //{
        //    OperationDetails<IQueryable<AdDTO>> operationDetails =
        //        new OperationDetails<IQueryable<AdDTO>>(false, "", null);

        //    try
        //    {
        //        var activeAds = await GetActiveAds();
        //        var vipAds = activeAds.Property.Where(x => x.Purhcases.Where(p => p.ServicesId == (int)AdPurchaseTypes.VipAd && p.IsActive)
        //                     .Count() > 0)
        //                     .OrderBy(x => Guid.NewGuid())
        //                     .Take(12);

        //        operationDetails = new OperationDetails<IQueryable<AdDTO>>(true, "", vipAds);
        //    }
        //    catch (Exception ex)
        //    {
        //        operationDetails = new OperationDetails<IQueryable<AdDTO>>(false, ex.Message, null);
        //    }


        //    return operationDetails;
        //}

        //public async Task<OperationDetails<IQueryable<AdDTO>>> GetActiveRandomTopAds()
        //{
        //    OperationDetails<IQueryable<AdDTO>> operationDetails =
        //        new OperationDetails<IQueryable<AdDTO>>(false, "", null);

        //    try
        //    {
        //        var activeAds = await GetActiveAds();
        //        var topAds = activeAds.Property.Where(x => x.Purhcases.Where(p => p.ServicesId == (int)AdPurchaseTypes.TopAd && p.IsActive)
        //                     .Count() > 0)
        //                     .OrderBy(x => Guid.NewGuid())
        //                     .Take(5);

        //        operationDetails = new OperationDetails<IQueryable<AdDTO>>(true, "", topAds);
        //    }
        //    catch (Exception ex)
        //    {
        //        operationDetails = new OperationDetails<IQueryable<AdDTO>>(false, ex.Message, null);
        //    }


        //    return operationDetails;
        //}

        public async Task<Ad> FindAdByIdAsync(int id)
        {
            var ads = await GetAdQuery(x => x.AdId == id, 
                ptExpression: null, auExpression: null).FirstOrDefaultAsync();
            return ads;
        }

        public IQueryable<Ad> FindAdsByProductTypeId(int productTypeId)
        {
            var ads = GetAdQuery(adExpression: null, x => x.ProductTypesId == productTypeId,
                auExpression: null);
            return ads;
        }

        public async Task<IQueryable<Ad>> GetAdsByUser(string userName)
        {
            var user = await _db.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();

            var ads = GetAdQuery(
                adExpression: null,
                ptExpression: null,
                x=> x.Id == user.Id);

            //foreach (var item in ads)
            //{
            //    item.NotDeliveredMessageCount = _conversationService.GetCountNotDeliveredMessageByAdId(item.AdId);
            //}

            return ads;
        }

        public IQueryable<Ad> FindActiveAdsByUserId(string userId)
        {
                var activeAds = GetAdQuery(
                    x => x.AdStatusId == (int)AdStatuses.Activated && x.IsModerate, 
                    ptExpression: null, 
                    x => x.Id == userId);

            return activeAds;
        }

        public IQueryable<Ad> FindAdsByUserId(string userId)
        {
            var ads = GetAdQuery(
                    adExpression: null,
                    ptExpression: null,
                    x => x.Id == userId);

            return ads;
        }

        public async Task<int> AddAd(ApplicationUser user, Ad ad)
        {
            var FindedUser = await _db.Users.Where(x => x.UserName == user.UserName).FirstOrDefaultAsync();

            ad.AdStatusId = (int)AdStatuses.Activated;
            ad.IsModerate = true;

            ad.ApplicationUser = FindedUser;

            try
            {
                var addResult = await _db.Ads.AddAsync(ad);
                var saveChangesResult = await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            ad.AdViews = new AdViews { AdId = ad.AdId, SumViews = 0 };

            //if (ad.Photos != null && ad.Photos.Count > 0)
            //{
            //    await AddPhotosToAd(ad);
            //}

            return ad.AdId;
        }

        public async Task<int> UpdateAd(Ad ad)
        {
            var oldPhotos = await _db.AdPhotos.Where(x => x.Ad.AdId == ad.AdId).ToListAsync();
            ad.AdStatusId = (int)AdStatuses.InProgress;
            ad.IsModerate = false;

            try
            {
                _db.RemoveRange(oldPhotos);
                _db.Update(ad);
                //await AddPhotosToAd(ad);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return ad.AdId;
        }

        //private async Task AddPhotosToAd(Ad ad)
        //{
        //    var binaryPhotoList = ad.Photos.ToList();
        //    binaryPhotoList.ForEach(x => x.Ad = ad);
        //    ad.Characteristics.Ad = ad;

        //    try
        //    {
        //        await _db.AdPhotos.AddRangeAsync(binaryPhotoList);
        //        ad.Photos = binaryPhotoList;
        //        _db.Update(ad);
        //        await _db.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        //public async Task<OperationDetails<int>> SetStatusAd(int id, int adStatus)
        //{
        //    OperationDetails<int> operationDetails = new OperationDetails<int>(false, "", 0);

        //    if (id > 0)
        //    {
        //        var ad = await _db.Ads.FindAsync(id);
        //        if (ad == null)
        //            return new OperationDetails<int>(false, "Невозможно найти объявление , с таким идентификатором", 0);


        //        ad.AdStatusId = adStatus;
        //        ad.IsModerate = false;

        //        try
        //        {
        //            _db.Update(ad);
        //            await _db.SaveChangesAsync();
        //        }
        //        catch (Exception ex)
        //        {
        //            operationDetails = new OperationDetails<int>(false, ex.Message.FirstOrDefault().ToString(), 0);
        //        }
        //    }

        //    return operationDetails;
        //}

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
