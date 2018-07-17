using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppleUsed.BLL.Services
{
    public class DataService : IDataService
    {
        private AppDbContext _db;

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
                //City = ad.CityesList.Where(x => x.Name == ad.SelectedCity).FirstOrDefault(),
                //Photos = ad.PhotosList,
                Characteristics = characteristics
            };

            return Ad;
        }
    }
}
