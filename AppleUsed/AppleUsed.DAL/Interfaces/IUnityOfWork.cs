using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Interfaces
{
    public interface IUnityOfWork : IDisposable
    {
        IAdRepository AdRepository { get; set; }

        IAdPhotoRepository AdPhotoRepository { get; set; }

        IAdUpRepository AdUpRepository { get; set; }

        IAdViewsRepository AdViewsRepository { get; set; }

        ICityAreasRepository CityAreasRepository { get; set; }

        ICityRepository CityRepository { get; set; }

        IProductTypeRepository ProductTypeRepository { get; set; }

        IProductModelRepository ProductModelRepository { get; set; }

        IProductMemoriesRepository ProductMemoriesRepository { get; set; }

        IProductColorsRepository ProductColorsRepository { get; set; }

        IProductStatesRepository ProductStatesRepository { get; set; }

        IPurchaseRepository PurchaseRepository { get; set; }

        IServiceActiveTimeRepository ServiceActiveTimeRepository { get; set; }

        IServiceRepository ServiceRepository { get; set; }

        IUserRepository UserRepository { get; set; }
    }
}
