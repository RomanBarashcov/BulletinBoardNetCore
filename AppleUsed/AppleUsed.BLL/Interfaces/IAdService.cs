using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Enums;
using AppleUsed.BLL.Infrastructure;
using AppleUsed.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IAdService : IDisposable
    {
        Task<OperationDetails<IQueryable<AdDTO>>> GetActiveAds();

        Task<OperationDetails<IQueryable<AdDTO>>> GetInProgressAds();

        Task<OperationDetails<IQueryable<AdDTO>>> GetDeactivatedAds();

        Task<OperationDetails<IQueryable<AdDTO>>> GetActiveRandomVIPAds();

        Task<OperationDetails<IQueryable<AdDTO>>> GetActiveRandomTopAds();

        Task<OperationDetails<IQueryable<AdDTO>>> GetAdsByProductTypeId(int productTypeId);

        Task<OperationDetails<IQueryable<AdDTO>>> GetActiveAdsByUserId(string userId);

        Task<OperationDetails<IQueryable<AdDTO>>> GetAdsByUserId(string userId);

        Task<OperationDetails<IQueryable<AdDTO>>> GetAdsByUserName(string userName);

        Task<OperationDetails<AdDTO>> GetAdById(int id);

        (IQueryable<CityDTO> citiesDTO,
        IQueryable<CityAreaDTO> cityAreasDTO,
        IQueryable<ProductTypeDTO> productTypesDTO,
        IQueryable<ProductModelsDTO> productModelsDTO,
        IQueryable<ProductMemorieDTO> productMemoriesDTO,
        IQueryable<ProductColorDTO> productColorsDTO,
        IQueryable<ProductStateDTO> productStateDTO) GetDataForCreatingAdOrDataForFilter();

        Task<OperationDetails<int>> SaveAd(
            string userName,
            AdDTO ad, 
            IFormFileCollection productPhotos);

        Task<OperationDetails<int>> SetStatusAd(int id, int adStatus);
    }
}
