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
        Task<OperationDetails<List<Ad>>> GetAds();
        Task<OperationDetails<Ad>> GetAdById(int id);
        //Task<AddAdQueryResult> GetDataForAddAd();
        Task<OperationDetails<int>> CreateAd(string userId, Ad ad);
        Task<OperationDetails<int>> AddImageToAd(int id, string imageName, byte[] imageData);
        Task<OperationDetails<int>> UpdateAd(int id, Ad ad);
        Task<OperationDetails<int>> DeleteAd(int id);
    }
}
