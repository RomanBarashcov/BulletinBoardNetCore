using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Infrastructure;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Services
{
    public class AdService : IAdService, IDisposable
    {
        private readonly AppDbContext _db;
        private readonly IDataService _dataService;
        private readonly IImageService _imageService;
        private readonly IConversationService _conversationService;
        private readonly ICityAreasService _cityAreasService;
        private readonly ICityService _cityService;
        private readonly IProductModelsService _productModelService;
        private readonly IAdUpService _adUpService;

        public AdService(AppDbContext context,
            IDataService dataService,
            IImageService imageService,
            IConversationService conversationService,
            ICityAreasService cityAreasService,
            ICityService cityService,
            IProductModelsService productModelsService,
            IAdUpService adUpService)
        {
            _db = context;
            _dataService = dataService;
            _imageService = imageService;
            _conversationService = conversationService;
            _cityAreasService = cityAreasService;
            _cityService = cityService;
            _productModelService = productModelsService;
            _adUpService = adUpService;
        }

        public async Task<OperationDetails<IQueryable<AdDTO>>> GetAds()
        {
            OperationDetails<IQueryable<AdDTO>> operationDetails = 
                new OperationDetails<IQueryable<AdDTO>>(false, "", null);

            try
            {
                var ads = (from ad in _db.Ads
                           join au in _db.AdUps on ad.AdId equals au.AdId
                           join c in _db.Cities on ad.City.CityId equals c.CityId
                           join ca in _db.CityAreas on c.CityArea.CityAreaId equals ca.CityAreaId
                           join ap in _db.AdPhotos.ToList() on ad.AdId equals ap.Ad.AdId into aPhotos
                           join av in _db.AdViews on ad.AdViews.AdViewsId equals av.AdViewsId
                           join ch in _db.Characteristics on ad.Characteristics.CharacteristicsId equals ch.CharacteristicsId
                           join pt in _db.ProductTypes on ch.ProductTypesId equals pt.ProductTypesId
                           join pm in _db.ProductModels on ch.ProductModelsId equals pm.ProductModelsId
                           join prm in _db.ProductMemories on ch.ProductMemoriesId equals prm.ProductMemoriesId
                           join pc in _db.ProductColors on ch.ProductColorsId equals pc.ProductColorsId
                           join prs in _db.ProductStates on ch.ProductStatesId equals prs.ProductStatesId
                           join u in _db.Users on ad.ApplicationUser.Id equals u.Id
                           select new AdDTO
                           {
                               AdId = ad.AdId,
                               Title = ad.Title,
                               Description = ad.Description,
                               Price = ad.Price,
                               DateCreated = ad.DateCreated,
                               DateUpdated = ad.DateUpdated,
                               SelectedCityArea = ca.Name,
                               SelectedCity = c.Name,
                               PhotosSmallSizeList = _imageService.CreatingImageSrcForSmallSize(aPhotos.ToList()),
                               AdViews = av.SumViews,
                               SelectedProductType = pt.Name,
                               SelectedProductTypeId = pt.ProductTypesId,
                               SelectedProductModel = pm.Name,
                               SelectedProductModelId = pm.ProductModelsId,
                               SelectedProductMemory = prm.Name,
                               SelectedProductMemoryId = prm.ProductMemoriesId,
                               SelectedProductColor = pc.Name,
                               SelectedProductColorId = pc.ProductColorsId,
                               SelectedProductStates = prs.Name,
                               SelectedProductStatesId = prs.ProductStatesId,
                               LastUpAd = au.LastUp,
                               User = new ApplicationUser { Id = u.Id, Email = u.Email, UserName = u.UserName }

                           }).OrderByDescending(x => x.LastUpAd);

                operationDetails = new OperationDetails<IQueryable<AdDTO>>(true, "", ads);
            }
            catch(Exception ex)
            {
                operationDetails = new OperationDetails<IQueryable<AdDTO>>(false, ex.Message, null);
            }
            

            return operationDetails;
        }

        public async Task<OperationDetails<AdDTO>> GetAdById(int id)
        {
            
            OperationDetails<AdDTO> operationDetails = 
                new OperationDetails<AdDTO>(false, "", new AdDTO());

            if (id == 0)
                return operationDetails;

            try
            {
                var adById = await (from ad in _db.Ads
                                    where ad.AdId == id
                                    join au in _db.AdUps on ad.AdId equals au.AdId
                                    join c in _db.Cities on ad.City.CityId equals c.CityId
                                    join ca in _db.CityAreas on c.CityArea.CityAreaId equals ca.CityAreaId
                                    join av in _db.AdViews on ad.AdViews.AdViewsId equals av.AdViewsId
                                    join ap in _db.AdPhotos.ToList() on ad.AdId equals ap.Ad.AdId into aPhotos
                                    join ch in _db.Characteristics on ad.Characteristics.CharacteristicsId equals ch.CharacteristicsId
                                    join pt in _db.ProductTypes on ch.ProductTypesId equals pt.ProductTypesId
                                    join pm in _db.ProductModels on ch.ProductModelsId equals pm.ProductModelsId
                                    join prm in _db.ProductMemories on ch.ProductMemoriesId equals prm.ProductMemoriesId
                                    join pc in _db.ProductColors on ch.ProductColorsId equals pc.ProductColorsId
                                    join prs in _db.ProductStates on ch.ProductStatesId equals prs.ProductStatesId
                                    join u in _db.Users on ad.ApplicationUser.Id equals u.Id
                                    select new AdDTO
                                    {
                                        AdId = ad.AdId,
                                        Title = ad.Title,
                                        Description = ad.Description,
                                        Price = ad.Price,
                                        DateCreated = ad.DateCreated,
                                        DateUpdated = ad.DateUpdated,
                                        SelectedCityId = c.CityId,
                                        SelectedCityAreaId = ca.CityAreaId,
                                        SelectedCityArea = ca.Name,
                                        SelectedCity = c.Name,
                                        PhotosSmallSizeList = _imageService.CreatingImageSrcForSmallSize(aPhotos.ToList()),
                                        PhotosForEdit = aPhotos.ToList(),
                                        PhotosAvgSizeList = _imageService.CreatingImageSrcForAvgSize(aPhotos.ToList()),
                                        PhotosBigSizeList = _imageService.CreatingImageSrcForBigSize(aPhotos.ToList()),
                                        AdViews = av.SumViews,
                                        SelectedProductType = pt.Name,
                                        SelectedProductTypeId = pt.ProductTypesId,
                                        SelectedProductModel = pm.Name,
                                        SelectedProductModelId = pm.ProductModelsId,
                                        SelectedProductMemory = prm.Name,
                                        SelectedProductMemoryId = prm.ProductMemoriesId,
                                        SelectedProductColor = pc.Name,
                                        SelectedProductColorId = pc.ProductColorsId,
                                        SelectedProductStates = prs.Name,
                                        SelectedProductStatesId = prs.ProductStatesId,
                                        LastUpAd = au.LastUp,
                                        User = new ApplicationUser
                                        {   Id = u.Id,
                                            Email = u.Email,
                                            UserName = u.UserName
                                        }

                                    }).FirstOrDefaultAsync();

                operationDetails = new OperationDetails<AdDTO>(true, "", adById);
            }
            catch(Exception ex)
            {
                operationDetails = new OperationDetails<AdDTO>(false, ex.Message, new AdDTO());
            }

            return operationDetails;
        }

        public async Task<OperationDetails<IQueryable<AdDTO>>> GetAdsByProductTypeId(int productTypeId)
        {
            OperationDetails<IQueryable<AdDTO>> operationDetails = 
                new OperationDetails<IQueryable<AdDTO>>(false, "", null);

            if (productTypeId == 0)
                return operationDetails;

            try
            {
                var ads = (from ad in _db.Ads
                           join au in _db.AdUps on ad.AdId equals au.AdId
                           join c in _db.Cities on ad.City.CityId equals c.CityId
                           join ca in _db.CityAreas on c.CityArea.CityAreaId equals ca.CityAreaId
                           join av in _db.AdViews on ad.AdViews.AdViewsId equals av.AdViewsId
                           join ap in _db.AdPhotos.ToList() on ad.AdId equals ap.Ad.AdId into aPhotos
                           join ch in _db.Characteristics on ad.Characteristics.CharacteristicsId equals ch.CharacteristicsId
                           join pt in _db.ProductTypes on ch.ProductTypesId equals pt.ProductTypesId
                           where pt.ProductTypesId == productTypeId
                           join pm in _db.ProductModels on ch.ProductModelsId equals pm.ProductModelsId
                           join prm in _db.ProductMemories on ch.ProductMemoriesId equals prm.ProductMemoriesId
                           join pc in _db.ProductColors on ch.ProductColorsId equals pc.ProductColorsId
                           join prs in _db.ProductStates on ch.ProductStatesId equals prs.ProductStatesId
                           join u in _db.Users on ad.ApplicationUser.Id equals u.Id
                           select new AdDTO
                           {
                               AdId = ad.AdId,
                               Title = ad.Title,
                               Description = ad.Description,
                               Price = ad.Price,
                               DateCreated = ad.DateCreated,
                               DateUpdated = ad.DateUpdated,
                               SelectedCityArea = ca.Name,
                               SelectedCity = c.Name,
                               PhotosSmallSizeList = _imageService.CreatingImageSrcForSmallSize(aPhotos.ToList()),
                               AdViews = av.SumViews,
                               SelectedProductType = pt.Name,
                               SelectedProductTypeId = pt.ProductTypesId,
                               SelectedProductModel = pm.Name,
                               SelectedProductModelId = pm.ProductModelsId,
                               SelectedProductMemory = prm.Name,
                               SelectedProductMemoryId = prm.ProductMemoriesId,
                               SelectedProductColor = pc.Name,
                               SelectedProductColorId = pc.ProductColorsId,
                               SelectedProductStates = prs.Name,
                               SelectedProductStatesId = prs.ProductStatesId,
                               LastUpAd = au.LastUp,
                               User = new ApplicationUser { Id = u.Id, Email = u.Email, UserName = u.UserName }

                           }).OrderByDescending(x => x.LastUpAd);

                operationDetails = new OperationDetails<IQueryable<AdDTO>>(true, "", ads);
            }
            catch (Exception ex)
            {
                operationDetails = new OperationDetails<IQueryable<AdDTO>>(false, ex.Message, null);
            }

            return operationDetails;
        }

        public async Task<OperationDetails<IQueryable<AdDTO>>> GetAdsByUser(string userName)
        {
            OperationDetails<IQueryable<AdDTO>> operationDetails =
                new OperationDetails<IQueryable<AdDTO>>(false, "", null);

            if (string.IsNullOrEmpty(userName))
                return operationDetails;

            var user = await _db.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();

            try
            {
                var ads = (from ad in _db.Ads
                           join au in _db.AdUps on ad.AdId equals au.AdId
                           join c in _db.Cities on ad.City.CityId equals c.CityId
                           join ca in _db.CityAreas on c.CityArea.CityAreaId equals ca.CityAreaId
                           join av in _db.AdViews on ad.AdViews.AdViewsId equals av.AdViewsId
                           join ap in _db.AdPhotos.ToList() on ad.AdId equals ap.Ad.AdId into aPhotos
                           join ch in _db.Characteristics on ad.Characteristics.CharacteristicsId equals ch.CharacteristicsId
                           join pt in _db.ProductTypes on ch.ProductTypesId equals pt.ProductTypesId
                           join pm in _db.ProductModels on ch.ProductModelsId equals pm.ProductModelsId
                           join prm in _db.ProductMemories on ch.ProductMemoriesId equals prm.ProductMemoriesId
                           join pc in _db.ProductColors on ch.ProductColorsId equals pc.ProductColorsId
                           join prs in _db.ProductStates on ch.ProductStatesId equals prs.ProductStatesId
                           join u in _db.Users on ad.ApplicationUser.Id equals u.Id
                           where u.Id == user.Id
                           select new AdDTO
                           {
                               AdId = ad.AdId,
                               Title = ad.Title,
                               Description = ad.Description,
                               Price = ad.Price,
                               DateCreated = ad.DateCreated,
                               DateUpdated = ad.DateUpdated,
                               SelectedCityArea = ca.Name,
                               SelectedCity = c.Name,
                               PhotosSmallSizeList = _imageService.CreatingImageSrcForSmallSize(aPhotos.ToList()),
                               AdViews = av.SumViews,
                               SelectedProductType = pt.Name,
                               SelectedProductTypeId = pt.ProductTypesId,
                               SelectedProductModel = pm.Name,
                               SelectedProductModelId = pm.ProductModelsId,
                               SelectedProductMemory = prm.Name,
                               SelectedProductMemoryId = prm.ProductMemoriesId,
                               SelectedProductColor = pc.Name,
                               SelectedProductColorId = pc.ProductColorsId,
                               SelectedProductStates = prs.Name,
                               SelectedProductStatesId = prs.ProductStatesId,
                               LastUpAd = au.LastUp,
                               User = new ApplicationUser { Id = u.Id, Email = u.Email, UserName = u.UserName }

                           }).OrderByDescending(x => x.LastUpAd);


                var adsList = ads.ToList();

                for (int i = 0; i <= adsList.Count() - 1; i++)
                {
                    adsList[i].NotDeliveredMessageCount = _conversationService.GetCountNotDeliveredMessageByAdId(adsList[i].AdId);
                }

                operationDetails = new OperationDetails<IQueryable<AdDTO>>(true, "", adsList.AsQueryable());
            }
            catch (Exception ex)
            {
                operationDetails = new OperationDetails<IQueryable<AdDTO>>(false, ex.Message, null);
            }

            return operationDetails;
        }

        public async Task<OperationDetails<IQueryable<AdDTO>>> GetAdsByUserId(string userId)
        {
            OperationDetails<IQueryable<AdDTO>> operationDetails =
              new OperationDetails<IQueryable<AdDTO>>(false, "", null);

            if (string.IsNullOrEmpty(userId))
                return operationDetails;

            try
            {
               var ads =  (from ad in _db.Ads
                           join au in _db.AdUps on ad.AdId equals au.AdId
                           join c in _db.Cities on ad.City.CityId equals c.CityId
                           join ca in _db.CityAreas on c.CityArea.CityAreaId equals ca.CityAreaId
                           join av in _db.AdViews on ad.AdViews.AdViewsId equals av.AdViewsId
                           join ap in _db.AdPhotos.ToList() on ad.AdId equals ap.Ad.AdId into aPhotos
                           join ch in _db.Characteristics on ad.Characteristics.CharacteristicsId equals ch.CharacteristicsId
                           join pt in _db.ProductTypes on ch.ProductTypesId equals pt.ProductTypesId
                           join pm in _db.ProductModels on ch.ProductModelsId equals pm.ProductModelsId
                           join prm in _db.ProductMemories on ch.ProductMemoriesId equals prm.ProductMemoriesId
                           join pc in _db.ProductColors on ch.ProductColorsId equals pc.ProductColorsId
                           join prs in _db.ProductStates on ch.ProductStatesId equals prs.ProductStatesId
                           join u in _db.Users on ad.ApplicationUser.Id equals u.Id
                           where u.Id == userId
                           select new AdDTO
                           {
                               AdId = ad.AdId,
                               Title = ad.Title,
                               Description = ad.Description,
                               Price = ad.Price,
                               DateCreated = ad.DateCreated,
                               DateUpdated = ad.DateUpdated,
                               SelectedCityArea = ca.Name,
                               SelectedCity = c.Name,
                               PhotosSmallSizeList = _imageService.CreatingImageSrcForSmallSize(aPhotos.ToList()),
                               AdViews = av.SumViews,
                               SelectedProductType = pt.Name,
                               SelectedProductTypeId = pt.ProductTypesId,
                               SelectedProductModel = pm.Name,
                               SelectedProductModelId = pm.ProductModelsId,
                               SelectedProductMemory = prm.Name,
                               SelectedProductMemoryId = prm.ProductMemoriesId,
                               SelectedProductColor = pc.Name,
                               SelectedProductColorId = pc.ProductColorsId,
                               SelectedProductStates = prs.Name,
                               SelectedProductStatesId = prs.ProductStatesId,
                               LastUpAd = au.LastUp,
                               User = new ApplicationUser { Id = u.Id, Email = u.Email, UserName = u.UserName }

                           }).OrderByDescending(x => x.LastUpAd);

                operationDetails = new OperationDetails<IQueryable<AdDTO>>(true, "", ads);
            }
            catch (Exception ex)
            {
                operationDetails = new OperationDetails<IQueryable<AdDTO>>(false, ex.Message, null);
            }

            return operationDetails;
        }

        public async Task<AdDTO> GetDataForCreatingAdOrDataForFilter()
        {
            AdDTO adDto = new AdDTO();

            adDto.CityesList = await _cityService.GetCities().ToListAsync();
            adDto.CityAreasList = await _cityAreasService.GetCityAreas().ToListAsync();
            adDto.ProductTypesList = await _db.ProductTypes.ToListAsync();
            adDto.ProductModelsList = await _productModelService.GetProductModels().ToListAsync();
            adDto.ProductMemoriesList = await _db.ProductMemories.ToListAsync();
            adDto.ProductColorsList = await _db.ProductColors.ToListAsync();
            adDto.ProductStatesList = await _db.ProductStates.ToListAsync();

            return adDto;
        }

        public async Task<OperationDetails<int>> SaveAd(string userName, AdDTO ad, IFormFileCollection productPhotos)
        {
            OperationDetails<int> operationDetails = new OperationDetails<int>(false, "", 0);
            ApplicationUser user = new ApplicationUser();

            if(ad == null)
                return new OperationDetails<int>(false, "new Ad can't be null or empty", 0);

            if (String.IsNullOrEmpty(userName))
                return operationDetails;

            user = await _db.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();

            if (user == null)
                return operationDetails;

            var newAd = _dataService.TransformingAdDTOToAdEntities(ad);
            newAd.ApplicationUser = user;

            if(newAd.AdId == 0)
            {
                operationDetails = await CreateAd(user, newAd, productPhotos);
            }
            else
            {
                operationDetails = await UpdateAd(user, newAd, productPhotos);
            }

            return operationDetails;
        
        }   

        private async Task<OperationDetails<int>> CreateAd(ApplicationUser user, Ad newAd, IFormFileCollection productPhotos)
        {
            OperationDetails<int> operationDetails = new OperationDetails<int>(false, "", 0);

            try
            {
                var addResult = await _db.Ads.AddAsync(newAd);
                var saveChangesResult = await _db.SaveChangesAsync();
                operationDetails = new OperationDetails<int>(true, "", newAd.AdId);
            }
            catch (Exception ex)
            {
                operationDetails = new OperationDetails<int>(false, ex.Message.FirstOrDefault().ToString(), 0);
            }

            newAd.AdViews = new AdViews { AdId = newAd.AdId, SumViews = 0 };

            if (productPhotos != null)
            {
                operationDetails = await AddPhotosToAd(newAd, productPhotos);
            }

            operationDetails = await _adUpService.InitUpAd(newAd.AdId);

            return operationDetails;
        }

        private async Task<OperationDetails<int>> UpdateAd(ApplicationUser user, Ad updatedAd, IFormFileCollection productPhotos)
        {
            OperationDetails<int> operationDetails = new OperationDetails<int>(false, "", 0);

            if(updatedAd.AdId > 0)
            {
                var oldAd = await _db.Ads.Where(x => x.AdId == updatedAd.AdId).FirstOrDefaultAsync();

                oldAd.Title = updatedAd.Title;
                oldAd.Description = updatedAd.Description;
                oldAd.Price = updatedAd.Price;
                oldAd.DateUpdated = DateTime.Now.Date;
                oldAd.Characteristics.ProductTypesId = updatedAd.Characteristics.ProductTypesId;
                oldAd.Characteristics.ProductModelsId = updatedAd.Characteristics.ProductModelsId;
                oldAd.Characteristics.ProductMemoriesId = updatedAd.Characteristics.ProductMemoriesId;
                oldAd.Characteristics.ProductColorsId = updatedAd.Characteristics.ProductColorsId;
                oldAd.Characteristics.ProductStatesId = updatedAd.Characteristics.ProductStatesId;
                oldAd.City = updatedAd.City;

                if (productPhotos != null)
                {
                    var oldPhotos = await _db.AdPhotos.Where(x => x.Ad.AdId == oldAd.AdId).ToListAsync();

                    try
                    {
                        _db.RemoveRange(oldPhotos);
                    }
                    catch (Exception ex)
                    {
                        operationDetails = new OperationDetails<int>(false, ex.Message.FirstOrDefault().ToString(), 0);
                    }

                    operationDetails = await AddPhotosToAd(oldAd, productPhotos);
                }
            }

            return operationDetails;
        }

        private async Task<OperationDetails<int>> AddPhotosToAd(Ad ad, IFormFileCollection productPhotos)
        {
            OperationDetails<int> operationDetails = new OperationDetails<int>(true, "", 0);

            var binaryPhotoList = _imageService.GetPhotosHashList(productPhotos);
            binaryPhotoList.ForEach(x => x.Ad = ad);
            ad.Characteristics.Ad = ad;

            try
            {
                await _db.AdPhotos.AddRangeAsync(binaryPhotoList);
                ad.Photos = binaryPhotoList;
                _db.Update(ad);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                operationDetails = new OperationDetails<int>(false, ex.Message.FirstOrDefault().ToString(), 0);
            }

            return operationDetails;
        }

        public async Task<OperationDetails<int>> DeleteAd(int id)
        {
            OperationDetails<int> operationDetails = new OperationDetails<int>(false, "", 0);

            if (id > 0)
            {
                var oldAd = await _db.Ads.Where(x => x.AdId == id).FirstOrDefaultAsync();
                try
                {
                    _db.Remove(oldAd);
                }
                catch (Exception ex)
                {
                    operationDetails = new OperationDetails<int>(false, ex.Message.FirstOrDefault().ToString(), 0);
                }
            }

            return operationDetails;
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
