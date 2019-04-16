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
        Task<OperationDetails<List<AdDTO>>> GetActiveAds();

        Task<OperationDetails<List<AdDTO>>> GetInProgressAds();

        Task<OperationDetails<List<AdDTO>>> GetDeactivatedAds();

        Task<OperationDetails<List<AdDTO>>> GetActiveRandomVIPAds();

        Task<OperationDetails<List<AdDTO>>> GetActiveRandomTopAds();

        Task<OperationDetails<List<AdDTO>>> GetAdsByProductTypeId(int productTypeId);

        Task<OperationDetails<List<AdDTO>>> GetActiveAdsByUserId(string userId);

        Task<OperationDetails<List<AdDTO>>> GetAdsByUserId(string userId);

        Task<OperationDetails<List<AdDTO>>> GetAdsByUserName(string userName);

        Task<OperationDetails<AdDTO>> GetAdById(int id, bool dataForEdit);

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
