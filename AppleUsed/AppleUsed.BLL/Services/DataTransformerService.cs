using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using AppleUsed.DAL.Interfaces;
using System;
using System.Linq;

namespace AppleUsed.BLL.Services
{
    public class DataTransformerService : IDataTransformerService, IDisposable
    {
        private readonly IUnityOfWork _uof;

        public DataTransformerService(IUnityOfWork uof)
        {
            _uof = uof;
        }

        public Ad TransformingAdDTOToAdEntities(AdDTO ad)
        {
            int selectedProductTypeId = Convert.ToInt32(ad.SelectedProductType);
            int selectedProductModelId = Convert.ToInt32(ad.SelectedProductModel);
            int selectedProductMemorieId = Convert.ToInt32(ad.SelectedProductMemory);
            int selectedProductColorsId = Convert.ToInt32(ad.SelectedProductColor);
            int selectedProductStatesId = Convert.ToInt32(ad.SelectedProductStates);
            int selectedCityId = Convert.ToInt32(ad.SelectedCityId);

            Characteristics characteristics = new Characteristics
            {
                ProductTypesId = selectedProductTypeId,
                ProductModelsId = selectedProductModelId,
                ProductMemoriesId = selectedProductMemorieId,
                ProductColorsId = selectedProductColorsId,
                ProductStatesId = selectedProductStatesId
            };

            Ad Ad = new Ad
            {
                Title = ad.Title,
                Description = ad.Description,
                Price = ad.Price,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                City = _uof.CityRepository.FindCityAsync(selectedCityId),
                Characteristics = characteristics,
                IsModerate = ad.IsModerate,
                AdStatusId = ad.AdStatusId
            };

            return Ad;
        }

        public AdDTO TransformingAdToAdDTO(Ad ad)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AdDTO> TransformingAdQueryToAdDTO(IQueryable<Ad> adQuery)
        {
            throw new NotImplementedException();
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
