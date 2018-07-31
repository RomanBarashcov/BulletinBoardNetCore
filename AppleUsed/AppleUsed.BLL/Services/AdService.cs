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
        private IImageCompressorService _imageCompressorService;

        public GetAdsByAuthorId(AppDbContext context, IDataService dataService, IImageCompressorService imageCompressorService)
        {
            _db = context;
            _dataService = dataService;
            _imageCompressorService = imageCompressorService;
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
                             PhotosList = CreatingImageSrc(aPhotos.ToList()),
                             //AdViews = av.SumViews,
                             SelectedProductType = pt.Name,
                             SelectedProductTypeId = pt.ProductTypesId,
                             SelectedProductModel = pm.Name,
                             SelectedProductModelId = pm.ProductModelsId,
                             SelectedProductMemory = prm.Name,
                             SelectedPoductMemoryId = prm.ProductMemoriesId,
                             SelectedProductColor = pc.Name,
                             SelectedProductColorId = pc.ProductColorsId,
                             SelectedProductStates = prs.Name,
                             User = new ApplicationUser { Id = u.Id, Email = u.Email, UserName = u.UserName }

                         }).OrderByDescending(x=>x.DateUpdated);

            return ads;
        }

        private List<string> CreatingImageSrc(List<AdPhotos> photoList)
        {
            List<string> imageSrcList = new List<string>();

            foreach(var item in photoList)
            {
                if (item.Photo != null)
                {
                    using(MagickImage image = new MagickImage(item.Photo))
                    {
                        var base64 = image.ToBase64();
                        imageSrcList.Add(String.Format("data:image/jpg;base64,{0}", base64));
                    }
                }
            }
           
            return imageSrcList;
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
                           PhotosList = CreatingImageSrc(aPhotos.ToList()),
                           //AdViews = av.SumViews,
                           SelectedProductType = pt.Name,
                           SelectedProductTypeId = pt.ProductTypesId,
                           SelectedProductModel = pm.Name,
                           SelectedProductModelId = pm.ProductModelsId,
                           SelectedProductMemory = prm.Name,
                           SelectedPoductMemoryId = prm.ProductMemoriesId,
                           SelectedProductColor = pc.Name,
                           SelectedProductColorId = pc.ProductColorsId,
                           SelectedProductStates = prs.Name,
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
                                 //SelectedCityArea 
                                 //SelectedCity 
                                 PhotosList = CreatingImageSrc(aPhotos.ToList()),
                                 //AdViews = av.SumViews,
                                 SelectedProductType = pt.Name,
                                 SelectedProductTypeId = pt.ProductTypesId,
                                 SelectedProductModel = pm.Name,
                                 SelectedProductModelId = pm.ProductModelsId,
                                 SelectedProductMemory = prm.Name,
                                 SelectedPoductMemoryId = prm.ProductMemoriesId,
                                 SelectedProductColor = pc.Name,
                                 SelectedProductColorId = pc.ProductColorsId,
                                 SelectedProductStates = prs.Name,
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
                           PhotosList = CreatingImageSrc(aPhotos.ToList()),
                           //AdViews = av.SumViews,
                           SelectedProductType = pt.Name,
                           SelectedProductTypeId = pt.ProductTypesId,
                           SelectedProductModel = pm.Name,
                           SelectedProductModelId = pm.ProductModelsId,
                           SelectedProductMemory = prm.Name,
                           SelectedPoductMemoryId = prm.ProductMemoriesId,
                           SelectedProductColor = pc.Name,
                           SelectedProductColorId = pc.ProductColorsId,
                           SelectedProductStates = prs.Name,
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

        public async Task<OperationDetails<int>> SaveAdAsync(string userName, AdDTO ad, IFormFileCollection productPhotos)
        {
            OperationDetails<int> operationDetails = new OperationDetails<int>(false, "", 0);
            ApplicationUser user = new ApplicationUser();

            if(ad == null)
                return new OperationDetails<int>(false, "new Ad can't be null or empty", 0);

            if (!String.IsNullOrEmpty(userName))
            {
                user = await _db.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();

                if (user != null)
                {
                    var newAd = _dataService.TransformingAdDTOToAdEntities(ad);
                    newAd.ApplicationUser = user;

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
                        var binaryPhotoList = GetBinaryPhotoList(productPhotos);
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
                }
            }

            return operationDetails;
        }

        public Task<OperationDetails<int>> DeleteAd(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AdDTO> GetAdById(int id)
        {
            var adById = await(from ad in _db.Ads
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
                            PhotosList = CreatingImageSrc(aPhotos.ToList()),
                            //AdViews = av.SumViews,
                            SelectedProductType = pt.Name,
                            SelectedProductTypeId = pt.ProductTypesId,
                            SelectedProductModel = pm.Name,
                            SelectedProductMemory = prm.Name,
                            SelectedProductColor = pc.Name,
                            SelectedProductStates = prs.Name,
                            User = u

                        }).Where(x=>x.AdId == id).FirstOrDefaultAsync();


            return adById;
        }

        public Task<OperationDetails<int>> UpdateAd(int id, Ad ad)
        {
            throw new NotImplementedException();
        }

        private List<AdPhotos> GetBinaryPhotoList(IFormFileCollection productPhotos)
        {
            List<AdPhotos> photosList = new List<AdPhotos>();

            foreach (var uploadedFile in productPhotos)
            {
                using (var binaryReader = new BinaryReader(uploadedFile.OpenReadStream()))
                {
                    photosList.Add(
                        new AdPhotos
                        {
                            Photo = _imageCompressorService.CompresingImage(binaryReader.ReadBytes((int)uploadedFile.Length)),
                            AdPhotoName = uploadedFile.FileName,
                        });
                }
            }

            return photosList;
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
                _imageCompressorService = null;
                disposed = true;
            }
        }
    }
}
