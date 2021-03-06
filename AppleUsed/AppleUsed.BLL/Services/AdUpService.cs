﻿using AppleUsed.BLL.Infrastructure;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Services
{
    public class AdUpService : IAdUpService
    {
        private IUnityOfWork _uof;

        public AdUpService(IUnityOfWork uof)
        {
            _uof = uof;
        }

        public async Task<OperationDetails<int>> UpAd(int adId)
        {
            OperationDetails<int> operationDetails = new OperationDetails<int>(false, "Вы не можете больше поднимать объявление вверх списка, ваш лимит исчерпан. Каждый месяц предоставляется 10 поднятий, если вы исчерпали лимит, вы можете приобрести еще в разделе покупки.", 0);

            var adUp = await _uof.AdUpRepository.FindByAdIdAsync(adId);

            if(adUp.CurrentRaisedUpCount < adUp.LimitUp)
            {
                adUp.CurrentRaisedUpCount += 1;
                adUp.LastUp = DateTime.Now;

                var result = await _uof.AdUpRepository.Update(adUp);
                if (result == adUp.AdId)
                    operationDetails = new OperationDetails<int>(true, "", 0);
            }

            return operationDetails;
        }

        public async Task<OperationDetails<int>> InitUpAd(int adId)
        {
            OperationDetails<int> operationDetail = new OperationDetails<int>(false, "", 0);

            var ad = await _uof.AdRepository.FindAdByIdAsync(adId);

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

                 var result = await _uof.AdUpRepository.AddAsync(adUp);
                 if(result != 0)
                    operationDetail = new OperationDetails<int>(true, "", result);
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
                _uof.Dispose();
                _uof = null;
            }
        }
    }
}
