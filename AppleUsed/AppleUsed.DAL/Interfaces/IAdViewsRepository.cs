using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Interfaces
{
    public interface IAdViewsRepository : IDisposable
    {
        Task<AdViews> FindByAdIdAsync(int adId);

        Task Update(AdViews adViews);
    }
}
