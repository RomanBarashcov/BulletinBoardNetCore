using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Services
{
    public class AdViewsService : IAdViewsService, IDisposable
    {
        private IAdViewsRepository _adViewsReposiotry;

        public AdViewsService(IAdViewsRepository adViewsReposiotry)
        {
            _adViewsReposiotry = adViewsReposiotry;
        }

        public async Task UpdateViewsAd(int adId)
        {
            var adViews = await _adViewsReposiotry.FindByAdIdAsync(adId);
            adViews.SumViews += 1;

            try
            {
                await _adViewsReposiotry.Update(adViews);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task ResetViews(int adId)
        {
            var adViews = await _adViewsReposiotry.FindByAdIdAsync(adId);
            adViews.SumViews = 0;
            try
            {
                await _adViewsReposiotry.Update(adViews);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;
                _adViewsReposiotry.Dispose();
                _adViewsReposiotry = null;
            }
        }
    }
}
