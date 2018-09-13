using AppleUsed.BLL.DTO;
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
    public interface IAdService
    {
        Task<OperationDetails<IQueryable<AdDTO>>> GetActiveAds();
        Task<OperationDetails<IQueryable<AdDTO>>> GetInProgressAds();
        Task<OperationDetails<IQueryable<AdDTO>>> GetDeactivatedAds();
        Task<OperationDetails<IQueryable<AdDTO>>> GetActiveRandomVIPAds();
        Task<OperationDetails<IQueryable<AdDTO>>> GetActiveRandomTopAds();
        Task<OperationDetails<IQueryable<AdDTO>>> GetAdsByProductTypeId(int productTypeId);
        Task<OperationDetails<IQueryable<AdDTO>>> GetActiveAdsByUserId(string userId);
        Task<OperationDetails<IQueryable<AdDTO>>> GetAdsByUserId(string userId);
        Task<OperationDetails<IQueryable<AdDTO>>> GetAdsByUser(string userName);
        Task<OperationDetails<AdDTO>> GetAdById(int id);
        Task<AdDTO> GetDataForCreatingAdOrDataForFilter();
        Task<OperationDetails<int>> SaveAd(string userName, AdDTO ad, IFormFileCollection productPhotos);
        Task<OperationDetails<int>> SetStatusAd(int id, int adStatus);
    }
}
