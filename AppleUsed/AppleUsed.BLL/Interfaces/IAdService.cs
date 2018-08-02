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
        Task<IQueryable<AdDTO>> GetAds();
        Task<IQueryable<AdDTO>> GetAdsByProductTypeId(int productTypeId);
        Task<IQueryable<AdDTO>> GetAdsByUserId(string userId);
        Task<IQueryable<AdDTO>> GetAdsByUser(string userName);
        Task<AdDTO> GetAdById(int id);
        Task<AdDTO> GetDataForCreatingAdOrDataForFilter();
        Task<OperationDetails<int>> SaveAd(string userName, AdDTO ad, IFormFileCollection productPhotos);
        Task<OperationDetails<int>> DeleteAd(int id);
    }
}
