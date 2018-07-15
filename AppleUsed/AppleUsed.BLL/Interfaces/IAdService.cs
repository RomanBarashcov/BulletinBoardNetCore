using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Infrastructure;
using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IAdService
    {
        OperationDetails<List<AdDTO>> GetAds();
        Task<OperationDetails<Ad>> GetAdById(int id);
        Task<AdDTO> GetDataForCreatingAd();
        Task<OperationDetails<int>> CreateAdAsync(string userId, AdDTO ad);
        Task<OperationDetails<int>> AddImageToAd(int id, string imageName, byte[] imageData);
        Task<OperationDetails<int>> UpdateAd(int id, Ad ad);
        Task<OperationDetails<int>> DeleteAd(int id);
    }
}
