using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Infrastructure;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Services
{
    public class GetAdsByAuthorId : IAdService , IDisposable
    {
        private AppDbContext _db;
        private IDataService _dataService;
        private readonly IImageService _imageService;
        private readonly IConversationService _conversationService;

        public GetAdsByAuthorId(AppDbContext context, IDataService dataService, IImageService imageService, IConversationService conversationService)
        {
            _db = context;
            _dataService = dataService;
            _imageService = imageService;
            _conversationService = conversationService;
        }

        public async Task<IQueryable<AdDTO>> GetAds()
        {
            var ads =  (from ad in _db.Ads
                             //join c in _db.Cities on ad.City.CityId equals c.CityId
                             //join ca in _db.CityAreas on c.CityArea.CityAreaId equals ca.CityAreaId
                             //join av in _db.AdViews on ad.AdViews.AdViewsId equals av.AdViewsId
                         join ap in _db.AdPhotos on ad.AdId equals ap.Ad.AdId into aPhotos
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
                             //SelectedCityArea 
                             //SelectedCity 
                             PhotosList = _imageService.CreatingImageSrc(aPhotos.ToList()),
                             //AdViews = av.SumViews,
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
                             User = new ApplicationUser { Id = u.Id, Email = u.Email, UserName = u.UserName }

                         }).OrderByDescending(x=>x.DateUpdated);

            return ads;
        }

        public async Task<AdDTO> GetAdById(int id)
        {
            var adById = await (from ad in _db.Ads
                                where ad.AdId == id
                                //join c in _db.Cities on ad.City.CityId equals c.CityId
                                //join ca in _db.CityAreas on c.CityArea.CityAreaId equals ca.CityAreaId
                                //join av in _db.AdViews on ad.AdViews.AdViewsId equals av.AdViewsId
                                join ap in _db.AdPhotos on ad.AdId equals ap.Ad.AdId into aPhotos
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
                                    //SelectedCityArea 
                                    //SelectedCity 
                                    PhotosForEdit = aPhotos.ToList(),
                                    PhotosList = _imageService.CreatingImageSrc(aPhotos.ToList()),
                                    //AdViews = av.SumViews,
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
                                    User = new ApplicationUser { Id = u.Id, Email = u.Email, UserName = u.UserName }

                                }).FirstOrDefaultAsync();


            return adById;
        }

        public async Task<IQueryable<AdDTO>> GetAdsByProductTypeId(int productTypeId)
        {
            var ads =  (from ad in _db.Ads
                           //join c in _db.Cities on ad.City.CityId equals c.CityId
                           //join ca in _db.CityAreas on c.CityArea.CityAreaId equals ca.CityAreaId
                           //join av in _db.AdViews on ad.AdViews.AdViewsId equals av.AdViewsId
                       join ap in _db.AdPhotos on ad.AdId equals ap.Ad.AdId into aPhotos
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
                           //SelectedCityArea 
                           //SelectedCity 
                           PhotosList = _imageService.CreatingImageSrc(aPhotos.ToList()),
                           //AdViews = av.SumViews,
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
                           User = new ApplicationUser { Id = u.Id, Email = u.Email, UserName = u.UserName }

                       }).OrderByDescending(x => x.DateUpdated);

            return ads;
        }

        public async Task<IQueryable<AdDTO>> GetAdsByUser(string userName)
        {
            var user = await _db.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();

            var ads = (from ad in _db.Ads
                                 //join c in _db.Cities on ad.City.CityId equals c.CityId
                                 //join ca in _db.CityAreas on c.CityArea.CityAreaId equals ca.CityAreaId
                                 //join av in _db.AdViews on ad.AdViews.AdViewsId equals av.AdViewsId
                             join ap in _db.AdPhotos on ad.AdId equals ap.Ad.AdId into aPhotos
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
                                 NotDeliveredMessageCount = _conversationService.GetCountNotDeliveredMessageByAdId(ad.AdId),
                                 //SelectedCityArea 
                                 //SelectedCity 
                                 PhotosList = _imageService.CreatingImageSrc(aPhotos.ToList()),
                                 //AdViews = av.SumViews,
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
                                 User = new ApplicationUser { Id = u.Id, Email = u.Email, UserName = u.UserName }

                             }).OrderByDescending(x => x.DateUpdated);

            return ads;
        }

        public async Task<IQueryable<AdDTO>> GetAdsByUserId(string userId)
        {
            var ads = (from ad in _db.Ads
                           //join c in _db.Cities on ad.City.CityId equals c.CityId
                           //join ca in _db.CityAreas on c.CityArea.CityAreaId equals ca.CityAreaId
                           //join av in _db.AdViews on ad.AdViews.AdViewsId equals av.AdViewsId
                       join ap in _db.AdPhotos on ad.AdId equals ap.Ad.AdId into aPhotos
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
                           //SelectedCityArea 
                           //SelectedCity 
                           PhotosList = _imageService.CreatingImageSrc(aPhotos.ToList()),
                           //AdViews = av.SumViews,
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
                           User = new ApplicationUser { Id = u.Id, Email = u.Email, UserName = u.UserName }

                       }).OrderByDescending(x => x.DateUpdated);

            return ads;
        }

        public async Task<AdDTO> GetDataForCreatingAdOrDataForFilter()
        {

            AdDTO adDto = new AdDTO();

            adDto.CityAreasList = await _db.CityAreas.ToListAsync();
            adDto.ProductTypesList = await (from t in _db.ProductTypes
                                            select new ProductTypes
                                            {
                                                ProductTypesId = t.ProductTypesId,
                                                Name = t.Name
                                             
                                            }).ToListAsync();

            adDto.ProductModelsList = await (from m in _db.ProductModels
                                             join t in _db.ProductTypes on m.ProductTypes.ProductTypesId equals t.ProductTypesId
                                             select new ProductModels
                                             {
                                                 ProductModelsId = m.ProductModelsId,
                                                 Name = m.Name,
                                                 ProductTypes = t

                                             }).ToListAsync();

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

        private async Task<OperationDetails<int>> CreateAd(ApplicationUser user,  Ad newAd, IFormFileCollection productPhotos)
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

            if (productPhotos != null)
            {
                var binaryPhotoList = _imageService.GetBinaryPhotoList(productPhotos);
                binaryPhotoList.ForEach(x => x.Ad = newAd);
                newAd.Characteristics.Ad = newAd;

                try
                {
                    await _db.AdPhotos.AddRangeAsync(binaryPhotoList);
                    newAd.Photos = binaryPhotoList;
                    _db.Update(newAd);
                    await _db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    operationDetails = new OperationDetails<int>(false, ex.Message.FirstOrDefault().ToString(), 0);
                }
            }

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

                    var binaryPhotoList = _imageService.GetBinaryPhotoList(productPhotos);
                    binaryPhotoList.ForEach(x => x.Ad = oldAd);
                    oldAd.Characteristics.Ad = oldAd;

                    try
                    {
                        await _db.AdPhotos.AddRangeAsync(binaryPhotoList);
                        oldAd.Photos = binaryPhotoList;
                        _db.Update(oldAd);
                        await _db.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        operationDetails = new OperationDetails<int>(false, ex.Message.FirstOrDefault().ToString(), 0);
                    }
                }
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
                _db = null;
                _dataService = null;
                disposed = true;
            }
        }
    }
}
