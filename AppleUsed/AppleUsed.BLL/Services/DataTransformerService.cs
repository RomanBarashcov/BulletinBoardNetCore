using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Enums;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppleUsed.BLL.Services
{
    public class DataTransformerService : IDataTransformerService
    {
        private IUnityOfWork _uof;

        public DataTransformerService(IUnityOfWork uof)
        {
            _uof = uof;
        }

        public Ad TransformingAdDTOToAdEntities(AdDTO ad)
        {
            Characteristics characteristics = new Characteristics
            {
                ProductTypesId = ad.SelectedProductTypeId,
                ProductModelsId = ad.SelectedProductModelId,
                ProductMemoriesId = ad.SelectedProductMemoryId,
                ProductColorsId = ad.SelectedProductColorId,
                ProductStatesId = ad.SelectedProductStateId
            };

            Ad Ad = new Ad
            {
                Title = ad.Title,
                Description = ad.Description,
                Price = ad.Price,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                CityId = ad.SelectedCityId,
                Characteristics = characteristics,
                IsModerate = ad.IsModerate,
                AdStatusId = ad.AdStatusId
            };

            return Ad;
        }

        public AdDTO TransformingAdToAdDTO(Ad ad)
        {
            var adDTO = new AdDTO
            {
                AdId = ad.AdId,
                Title = ad.Title,
                Description = ad.Description,
                Price = ad.Price,
                DateCreated = ad.DateCreated,
                DateUpdated = ad.DateUpdated,
                Characteristics = ad.Characteristics,
                City = ad.City,
                Photos = ad.Photos,
                AdViews = ad.AdViews,
                AdStatusId = ad.AdStatusId,
                IsModerate = ad.IsModerate,
                Purhcases = ad.Purhcases,
                ApplicationUser = ad.ApplicationUser
            };

            return adDTO;
        }

        public IQueryable<AdDTO> TransformingAdQueryToAdDTO(IQueryable<Ad> adQuery)
        {
            var adDTOQuery = adQuery.Select(ad => new AdDTO
            {
                AdId = ad.AdId,
                Title = ad.Title,
                Description = ad.Description,
                Price = ad.Price,
                DateCreated = ad.DateCreated,
                DateUpdated = ad.DateUpdated,
                Characteristics = ad.Characteristics,
                City = ad.City,
                Photos = ad.Photos,
                AdViews = ad.AdViews,
                AdStatusId = ad.AdStatusId,
                IsModerate = ad.IsModerate,
                Purhcases = ad.Purhcases,
                ApplicationUser = ad.ApplicationUser
            });

            return adDTOQuery;
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
                _uof.Dispose();
                _uof = null;
            }
        }

    }
}
