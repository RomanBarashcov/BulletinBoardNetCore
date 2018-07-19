using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Infrastructure;
using AppleUsed.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IAdService
    {
        Task<List<AdDTO>> GetAds(string titleFilter, string cityFilter);
        Task<AdDTO> GetAdById(int id);
        Task<AdDTO> GetDataForCreatingAdOrDataForFilter();
        Task<OperationDetails<int>> SaveAdAsync(string userName, AdDTO ad, IFormFileCollection productPhotos);
        Task<OperationDetails<int>> UpdateAd(int id, Ad ad);
        Task<OperationDetails<int>> DeleteAd(int id);
    }
}
