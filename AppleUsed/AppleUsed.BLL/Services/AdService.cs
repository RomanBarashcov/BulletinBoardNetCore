using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Enums;
using AppleUsed.BLL.Infrastructure;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using AppleUsed.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Services
{
    public class AdService : IAdService, IDisposable
    {
        private IUnityOfWork _uof;
        private IImageService _imageService;
        private IDataTransformerService _dataService;
        private IAdUpService _adUpService;

        public AdService(
            IUnityOfWork uof, 
            IImageService imageService, 
            IDataTransformerService dataService,
            IAdUpService adUpService)
        {
            _uof = uof;
            _imageService = imageService;
            _dataService = dataService;
            _adUpService = adUpService;
        }

        public async Task<OperationDetails<IQueryable<AdDTO>>> GetActiveAds()
        {
            OperationDetails<IQueryable<AdDTO>> operationDetails = 
                new OperationDetails<IQueryable<AdDTO>>(false, "", null);

            try
            {
                var ads = _uof.AdRepository.GetAdQuery(
                    x => x.AdStatusId == (int)AdStatuses.Activated && x.IsModerate,
                    ptExpression: null, 
                    auExpression: null,
                    pExpression: null);

                operationDetails = new OperationDetails<IQueryable<AdDTO>>(true, "", _dataService.TransformingAdQueryToAdDTO(ads));
            }
            catch(Exception ex)
            {
                operationDetails = new OperationDetails<IQueryable<AdDTO>>(false, ex.Message, null);
            }
            

            return operationDetails;
        }

        public async Task<OperationDetails<IQueryable<AdDTO>>> GetInProgressAds()
        {
            OperationDetails<IQueryable<AdDTO>> operationDetails =
                new OperationDetails<IQueryable<AdDTO>>(false, "", null);

            try
            {
                var ads = _uof.AdRepository.GetAdQuery(
                    x => x.AdStatusId == (int)AdStatuses.InProgress,
                    ptExpression: null, 
                    auExpression: null,
                    pExpression: null);

                operationDetails = new OperationDetails<IQueryable<AdDTO>>(true, "", _dataService.TransformingAdQueryToAdDTO(ads));
            }
            catch (Exception ex)
            {
                operationDetails = new OperationDetails<IQueryable<AdDTO>>(false, ex.Message, null);
            }


            return operationDetails;
        }

        public async Task<OperationDetails<IQueryable<AdDTO>>> GetDeactivatedAds()
        {
            OperationDetails<IQueryable<AdDTO>> operationDetails =
                new OperationDetails<IQueryable<AdDTO>>(false, "", null);

            try
            {
                var ads = _uof.AdRepository.GetAdQuery(
                        x => x.AdStatusId == (int)AdStatuses.Deactivated,
                        ptExpression: null,
                        auExpression: null,
                        pExpression: null);

                operationDetails = new OperationDetails<IQueryable<AdDTO>>(true, "", _dataService.TransformingAdQueryToAdDTO(ads));
            }
            catch (Exception ex)
            {
                operationDetails = new OperationDetails<IQueryable<AdDTO>>(false, ex.Message, null);
            }


            return operationDetails;
        }

        public async Task<OperationDetails<IQueryable<AdDTO>>> GetActiveRandomVIPAds()
        {
            OperationDetails<IQueryable<AdDTO>> operationDetails =
                new OperationDetails<IQueryable<AdDTO>>(false, "", null);

            try
            {
                var ads = _uof.AdRepository.GetAdQuery(
                       x => x.AdStatusId == (int)AdStatuses.Deactivated,
                       ptExpression: null,
                       auExpression: null,
                       p => p.ServicesId == (int)AdPurchaseTypes.VipAd && p.IsActive)
                       .OrderBy(x => Guid.NewGuid())
                       .Take(12);

                operationDetails = new OperationDetails<IQueryable<AdDTO>>(true, "", _dataService.TransformingAdQueryToAdDTO(ads));
            }
            catch (Exception ex)
            {
                operationDetails = new OperationDetails<IQueryable<AdDTO>>(false, ex.Message, null);
            }


            return operationDetails;
        }

        public async Task<OperationDetails<IQueryable<AdDTO>>> GetActiveRandomTopAds()
        {
            OperationDetails<IQueryable<AdDTO>> operationDetails =
                new OperationDetails<IQueryable<AdDTO>>(false, "", null);

            try
            {
                var ads = _uof.AdRepository.GetAdQuery(
                      x => x.AdStatusId == (int)AdStatuses.Deactivated,
                      ptExpression: null,
                      auExpression: null,
                      p => p.ServicesId == (int)AdPurchaseTypes.TopAd && p.IsActive)
                      .OrderBy(x => Guid.NewGuid())
                      .Take(5);

                operationDetails = new OperationDetails<IQueryable<AdDTO>>(true, "", _dataService.TransformingAdQueryToAdDTO(ads));
            }
            catch (Exception ex)
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
                var adById = await _uof.AdRepository.FindAdByIdAsync(id);
                operationDetails = 
                    new OperationDetails<AdDTO>(true, "", _dataService.TransformingAdToAdDTO(adById));
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
                var ads = _uof.AdRepository.FindAdsByProductTypeId(productTypeId);
                operationDetails = new OperationDetails<IQueryable<AdDTO>>(true, "", _dataService.TransformingAdQueryToAdDTO(ads));
            }
            catch (Exception ex)
            {
                operationDetails = new OperationDetails<IQueryable<AdDTO>>(false, ex.Message, null);
            }

            return operationDetails;
        }

        public async Task<OperationDetails<IQueryable<AdDTO>>> GetAdsByUserName(string userName)
        {
            OperationDetails<IQueryable<AdDTO>> operationDetails =
                new OperationDetails<IQueryable<AdDTO>>(false, "", null);

            if (string.IsNullOrEmpty(userName))
                return operationDetails;

            try
            {

                var ads = await _uof.AdRepository.GetAdsByUserName(userName);

                //foreach (var item in ads)
                //{
                //    item.NotDeliveredMessageCount = _ads.GetCountNotDeliveredMessageByAdId(item.AdId);
                //}

                operationDetails = new OperationDetails<IQueryable<AdDTO>>(true, "", _dataService.TransformingAdQueryToAdDTO(ads));
            }
            catch (Exception ex)
            {
                operationDetails = new OperationDetails<IQueryable<AdDTO>>(false, ex.Message, null);
            }

            return operationDetails;
        }

        public async Task<OperationDetails<IQueryable<AdDTO>>> GetActiveAdsByUserId(string userId)
        {
            OperationDetails<IQueryable<AdDTO>> operationDetails =
              new OperationDetails<IQueryable<AdDTO>>(false, "", null);

            if (string.IsNullOrEmpty(userId))
                return operationDetails;

            try
            {
                var activeAds = _uof.AdRepository.FindActiveAdsByUserId(userId);
                operationDetails = new OperationDetails<IQueryable<AdDTO>>(true, "", _dataService.TransformingAdQueryToAdDTO(activeAds));
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
                var ads = _uof.AdRepository.FindAdsByUserId(userId);
                operationDetails = new OperationDetails<IQueryable<AdDTO>>(true, "", _dataService.TransformingAdQueryToAdDTO(ads));
            }
            catch (Exception ex)
            {
                operationDetails = new OperationDetails<IQueryable<AdDTO>>(false, ex.Message, null);
            }

            return operationDetails;
        }

        public(
            IQueryable<CityDTO> citiesDTO,
            IQueryable<CityAreaDTO> cityAreasDTO,
            IQueryable<ProductTypeDTO> productTypesDTO,
            IQueryable<ProductModelsDTO> productModelsDTO,
            IQueryable<ProductMemorieDTO> productMemoriesDTO,
            IQueryable<ProductColorDTO> productColorsDTO,
            IQueryable<ProductStateDTO> productStateDTO) GetDataForCreatingAdOrDataForFilter()
        {
            var cities = 
                _uof.CityRepository.GetCities()
                .Select(x => new CityDTO { Id = x.CityId, Name = x.Name });

            var cityAreas =
                _uof.CityAreasRepository.GetCityAreas()
                .Select(x => new CityAreaDTO { Id = x.CityAreaId, Name = x.Name, Cities = cities.ToList() });

            var productTypes =
                _uof.ProductTypeRepository.GetProductTypes()
                .Select(x => new ProductTypeDTO { Id = x.ProductTypesId, Name = x.Name });

            var productModels = 
                _uof.ProductModelRepository.GetProductModels()
                .Select(x => new ProductModelsDTO { Id = x.ProductModelsId, Name = x.Name, ProductTypeId = x.ProductTypes.ProductTypesId });

            var productMemories =
                _uof.ProductMemoriesRepository.GetProductMemories()
                .Select(x => new ProductMemorieDTO { Id = x.ProductMemoriesId, StorageSize = x.StorageSize });

            var productColors =  
                _uof.ProductColorsRepository.GetProductColors()
                .Select(x => new ProductColorDTO { Id = x.ProductColorsId, Name = x.Name });

            var productStates =
                _uof.ProductStatesRepository.GetProductStates()
                .Select(x => new ProductStateDTO { Id = x.ProductStatesId, Name = x.Name });

            return (cities, 
                    cityAreas, 
                    productTypes, 
                    productModels, 
                    productMemories, 
                    productColors,
                    productStates);
        }

        public async Task<OperationDetails<int>> SaveAd(string userName, AdDTO ad, IFormFileCollection productPhotos)
        {
            OperationDetails<int> operationDetails = new OperationDetails<int>(false, "", 0);
            ApplicationUser user = new ApplicationUser();

            if(ad == null)
                return new OperationDetails<int>(false, "new Ad can't be null or empty", 0);

            if (String.IsNullOrEmpty(userName))
                return operationDetails;

            user = await _uof.UserRepository.FindUserByUserName(userName);
            if (user == null)
                return operationDetails;

            var newAd = _dataService.TransformingAdDTOToAdEntities(ad);
            newAd.ApplicationUser = user;

            if(newAd.AdId == 0)
            {
                newAd.AdStatusId = (int)AdStatuses.Activated;
                newAd.IsModerate = true;
                operationDetails = await CreateAd(user, newAd, productPhotos);
            }
            else
            {
                newAd.AdStatusId = (int)AdStatuses.InProgress;
                newAd.IsModerate = false;
                operationDetails = await UpdateAd(user, newAd, productPhotos);
            }

            return operationDetails;
        }   

        private async Task<OperationDetails<int>> CreateAd(
            ApplicationUser user, 
            Ad newAd,
            IFormFileCollection productPhotos)
        {
            OperationDetails<int> operationDetails = new OperationDetails<int>(false, "", 0);

            try
            {
                newAd.AdId = await _uof.AdRepository.AddAd(newAd);
                operationDetails = new OperationDetails<int>(true, "", newAd.AdId);
            }
            catch (Exception ex)
            {
                operationDetails = 
                    new OperationDetails<int>(false, ex.Message.FirstOrDefault().ToString(), 0);
            }

            newAd.AdViews = new AdViews { AdId = newAd.AdId, SumViews = 0 };

            if (productPhotos != null)
            {
                operationDetails = await AddPhotosToAd(newAd, productPhotos);
            }

            operationDetails = await _adUpService.InitUpAd(newAd.AdId);

            return operationDetails;
        }

        private async Task<OperationDetails<int>> UpdateAd(ApplicationUser user, Ad ad, IFormFileCollection productPhotos)
        {
            OperationDetails<int> operationDetails =
                new OperationDetails<int>(false, "", 0);

            if(ad.AdId > 0)
            {
                var oldAd = await _uof.AdRepository.FindAdByIdAsync(ad.AdId);

                oldAd.Title = ad.Title;
                oldAd.Description = ad.Description;
                oldAd.Price = ad.Price;
                oldAd.DateUpdated = DateTime.Now.Date;
                oldAd.Characteristics.ProductTypesId = ad.Characteristics.ProductTypesId;
                oldAd.Characteristics.ProductModelsId = ad.Characteristics.ProductModelsId;
                oldAd.Characteristics.ProductMemoriesId = ad.Characteristics.ProductMemoriesId;
                oldAd.Characteristics.ProductColorsId = ad.Characteristics.ProductColorsId;
                oldAd.Characteristics.ProductStatesId = ad.Characteristics.ProductStatesId;
                oldAd.City = ad.City;

                if (productPhotos != null)
                {
                    var oldPhotos = _uof.AdPhotoRepository.FindPhotosByAdId(ad.AdId);

                    try
                    {
                       await _uof.AdPhotoRepository.RemovePhotosRange(oldPhotos.ToList());
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
            binaryPhotoList.ForEach(x => x.AdId = ad.AdId);
            ad.Characteristics.AdId = ad.AdId;

            try
            {
                await _uof.AdPhotoRepository.AddPhotoRange(binaryPhotoList);
            }
            catch (Exception ex)
            {
                operationDetails = new OperationDetails<int>(false, ex.Message.FirstOrDefault().ToString(), 0);
            }

            return operationDetails;
        }

        public async Task<OperationDetails<int>> SetStatusAd(int id, int adStatus)
        {
            OperationDetails<int> operationDetails = new OperationDetails<int>(false, "", 0);

            if (id > 0)
            {
                var ad = await _uof.AdRepository.FindAdByIdAsync(id);
                if(ad == null)
                    return new OperationDetails<int>(false, "Невозможно найти объявление , с таким идентификатором", 0);


                ad.AdStatusId = adStatus;
                ad.IsModerate = false;

                try
                {
                    await _uof.AdRepository.UpdateAd(ad);
                }
                catch (Exception ex)
                {
                    operationDetails = 
                        new OperationDetails<int>(false, ex.Message.FirstOrDefault().ToString(), 0);
                }
            }

            return operationDetails;
        }


        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _uof.Dispose();
                    _imageService.Dispose();

                    _uof = null;
                    _imageService = null;
                    _dataService = null;
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
