using AppleUsed.BLL.Infrastructure;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Services
{
    public class AdUpService : IAdUpService, IDisposable
    {
        private readonly AppDbContext _db;

        public AdUpService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<OperationDetails<int>> UpAd(int adId)
        {
            OperationDetails<int> operationDetails = new OperationDetails<int>(false, "Вы не можете больше поднимать объявление вверх списка, ваш лимит исчерпан. Каждый месяц предоставляется 10 поднятий, если вы исчерпали лимит, вы можете приобрести еще в разделе покупки.", 0);

            var adUp = await _db.AdUps.Where(x=>x.AdId == adId).FirstOrDefaultAsync();

            if(adUp.CurrentRaisedUpCount < adUp.LimitUp)
            {
                adUp.CurrentRaisedUpCount += 1;
                adUp.LastUp = DateTime.Now;

                try
                {
                    _db.AdUps.Update(adUp);
                    await _db.SaveChangesAsync();

                    operationDetails = new OperationDetails<int>(true, "", adUp.CurrentRaisedUpCount);
                }
                catch(Exception ex)
                {
                    operationDetails = new OperationDetails<int>(true, ex.Message, adUp.CurrentRaisedUpCount);
                }
            }

            return operationDetails;
        }

        public async Task<OperationDetails<int>> InitUpAd(int adId)
        {
            OperationDetails<int> operationDetail = new OperationDetails<int>(false, "", 0);

            var ad = await _db.Ads.FindAsync(adId);

            if(ad != null)
            {
                var adUp = new AdUp
                {
                    StartDateAction = DateTime.Now,
                    EndDateAction = DateTime.Now.AddDays(30.0),
                    LastUp = DateTime.Now,
                    LimitUp = 10,
                    AdId = ad.AdId
                };

                try
                {
                    await _db.AdUps.AddAsync(adUp);
                    await _db.SaveChangesAsync();

                    operationDetail = new OperationDetails<int>(true, "", adUp.AdUpId);
                }
                catch (Exception ex)
                {
                    operationDetail = new OperationDetails<int>(false, ex.Message, 0);
                }
            }

            return operationDetail;
        }

        public Task<OperationDetails<int>> UpdateUpAd(int adId)
        {
            throw new NotImplementedException();
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
            }
        }
    }
}
