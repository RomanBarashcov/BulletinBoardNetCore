using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IAdViewsService
    {
        Task UpdateViewsAd(int adId);
        Task ResetViews(int adId);
    }
}
