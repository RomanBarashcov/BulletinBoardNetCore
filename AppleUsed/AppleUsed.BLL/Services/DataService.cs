using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppleUsed.BLL.Services
{
    public class DataService : IDataService
    {
        public DataService() { }

        public Ad TransformingAdDTOToAdEntities(AdDTO ad)
        {
            Characteristics characteristics = new Characteristics
            {
                ProductModels = ad.ProductModelsList
                       .Where(
                                x => x.Name == ad.SelectedProductType
                             ).FirstOrDefault(),

                ProductMemories = ad.ProductMemoriesList
                       .Where(
                                x => x.Name == ad.SelectedProductMemory
                             ).FirstOrDefault(),

                ProductColors = ad.ProductColorsList
                       .Where(
                                x => x.Name == ad.SelectedProductColor
                             ).FirstOrDefault(),

                ProductStates = ad.ProductStatesList
                       .Where(
                                x => x.Name == ad.SelectedProductStates
                           ).FirstOrDefault()
            };

            Ad Ad = new Ad
            {
                Title = ad.Title,
                Description = ad.Description,
                Price = ad.Price,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                City = ad.CityesList.Where(x => x.Name == ad.SelectedCity).FirstOrDefault(),
                Photos = ad.Photos,
                Characteristics = characteristics,
            };

            return Ad;
        }
    }
}
