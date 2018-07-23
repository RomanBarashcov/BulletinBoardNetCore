using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Infrastructure;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
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
    public class AdService : IAdService , IDisposable
    {
        private AppDbContext _db;
        private IDataService _dataService;
        private IImageCompressorService _imageCompressorService;

        public AdService(AppDbContext context, IDataService dataService, IImageCompressorService imageCompressorService)
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
                             PhotosList = aPhotos.ToList(),
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
                             User = u

                         });


            //if(!string.IsNullOrEmpty(titleFilter))
            //{
            //    ads = ads.Where(x => x.Title.ToLower().Contains(titleFilter.ToLower())).ToList();
            //}
            //List<AdDTO> ads = new List<AdDTO>();
            //List<AdPhotos> photos = new List<AdPhotos>();

            ////List<AdPhotos> photos = await (from p in _db.AdPhotos
            ////                               join ad in _db.Ads on p.Ad.AdId equals ad.AdId
            ////                               select new AdPhotos
            ////                               {
            ////                                   Ad = ad,
            ////                                   AdPhotosId = p.AdPhotosId,
            ////                                   AdPhotoName = p.AdPhotoName,
            ////                                   Photo = p.Photo

            ////                               }).ToListAsync();

            //ProcedureService procedureService = new ProcedureService();
            //SqlDataReader photosReader = procedureService.GetResultFromStoredProcedure("dbo.GetAllPhotos");

            //if (photosReader.HasRows)
            //{
            //    while (photosReader.Read())
            //    {

            //            int adPhotosId = Int32.Parse(photosReader["AdPhotosId"].ToString());
            //            string adPhotoName = photosReader["AdPhotoName"].ToString();
            //            byte[] photo = Encoding.ASCII.GetBytes(photosReader["Photo"].ToString());
            //            int adId = Int32.Parse(photosReader["AdId"].ToString());


            //            photos.Add(new AdPhotos
            //            {
            //                AdPhotosId = adPhotosId,
            //                AdPhotoName = adPhotoName,
            //                Photo = photo,
            //                Ad = _db.Ads.Where(x => x.AdId == adId).FirstOrDefault()
            //            });
            //    }
            //}

            //photosReader.Close();

            //SqlDataReader reader = procedureService.GetResultFromStoredProcedure("dbo.GetAllAds");

            //if (reader.HasRows)
            //{
            //    while (reader.Read())
            //    {
            //        ApplicationUser user = await _db.Users.Where(x => x.Id == reader["UserId"].ToString()).FirstOrDefaultAsync();

            //        ads.Add(new AdDTO
            //        {
            //            AdId = Int32.Parse(reader["AdId"].ToString()),
            //            Title = reader["Title"].ToString(),
            //            Description = reader["Description"].ToString(),
            //            Price = Decimal.Parse(reader["Price"].ToString()),
            //            DateCreated = DateTime.Parse(reader["DateCreated"].ToString()),
            //            DateUpdated = DateTime.Parse(reader["DateUpdated"].ToString()),
            //            PhotosList = photos.Where(x => x.Ad.AdId == Int32.Parse(reader["AdId"].ToString())).ToList(),
            //            SelectedProductType = reader["ProductType"].ToString(),
            //            SelectedProductModel = reader["ProductModel"].ToString(),
            //            SelectedProductMemory = reader["ProductMemory"].ToString(),
            //            SelectedProductColor = reader["ProductColor"].ToString(),
            //            SelectedProductStates = reader["ProductState"].ToString(),
            //            User = user

            //        });
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("No rows found.");
            //}



            //reader.Close();
            

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
                            PhotosList = aPhotos.ToList(),
                            //AdViews = av.SumViews,
                            SelectedProductType = pt.Name,
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
            // подавляем финализацию
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    
                }
                // освобождаем неуправляемые объекты

                _db = null;
                disposed = true;
            }
        }
    }
}
