using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Services
{
    public class AdViewsService : IAdViewsService
    {
        private IUnityOfWork _uof;

        public AdViewsService(IUnityOfWork uof)
        {
            _uof = uof;
        }

        public async Task UpdateViewsAd(int adId)
        {
            var adViews = await _uof.AdViewsRepository.FindByAdIdAsync(adId);
            adViews.SumViews += 1;

            try
            {
                await _uof.AdViewsRepository.Update(adViews);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task ResetViews(int adId)
        {
            var adViews = await _uof.AdViewsRepository.FindByAdIdAsync(adId);
            adViews.SumViews = 0;
            try
            {
                await _uof.AdViewsRepository.Update(adViews);
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
                _uof.Dispose();
                _uof = null;
            }
        }
    }
}
