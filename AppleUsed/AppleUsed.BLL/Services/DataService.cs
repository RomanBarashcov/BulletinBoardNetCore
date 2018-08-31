using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using System;
using System.Linq;

namespace AppleUsed.BLL.Services
{
    public class DataService : IDataService, IDisposable
    {
        private readonly AppDbContext _db;

        public DataService(AppDbContext db)
        {
            _db = db;
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
                City = _db.Cities.Where(x => x.CityId == selectedCityId).FirstOrDefault(),
                Characteristics = characteristics,
                IsModerate = ad.IsModerate,
                AdStatusId = ad.AdStatusId
                
            };

            return Ad;
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
