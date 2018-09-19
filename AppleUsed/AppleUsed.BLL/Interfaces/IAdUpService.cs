using AppleUsed.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IAdUpService : IDisposable
    {
        Task<OperationDetails<int>> UpdateUpAd(int adId);
        Task<OperationDetails<int>> UpAd(int adId);
        Task<OperationDetails<int>> InitUpAd(int adId);
    }
}
