using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Infrastructure;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using AppleUsed.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Services
{
    public class PurchasesService : IPurchasesService
    {
        private IUnityOfWork _uof;

        public PurchasesService(IUnityOfWork uof)
        {
            _uof = uof;
        }

        public IQueryable<PurchaseDTO> GetPurchases()
        {
            var purchases = _uof.PurchaseRepository.GetAllPurchase().Select(p => new PurchaseDTO
            {
                 PurchaseId = p.PurchaseId,
                 TotalCost = p.TotalCost,
                 DateOfPayment = p.DateOfPayment,
                 StartDateService = p.StartDateService,
                 EndDateService = p.EndDateService,
                 IsPayed = p.IsPayed,
                 IsActive = p. IsActive,
                 ServicesId = p.ServicesId,
                 ServiceActiveTimeId = p.ServiceActiveTimeId,
                 AdId = p.AdId
             });

            return purchases;
        }

        private IQueryable<Purchase> GetPurchaseQuery()
        {
            var purchases = _uof.PurchaseRepository.GetAllPurchase();
            return purchases;
        }

        public async Task<OperationDetails<int>> DiactivationAllPurchaseWhereEndDateServiceIsOut()
        {
            OperationDetails<int> operationDetails =
              new OperationDetails<int>(false, "", 0);

            var purchases = GetPurchaseQuery();
            if (purchases == null) return operationDetails;

            var deactivationPurchases = purchases.Where(x => x.IsActive == true && x.EndDateService >= DateTime.Now.Date);
            
            foreach(var item in deactivationPurchases)
            {
                item.IsActive = false;
            }

            try
            {
                await _uof.PurchaseRepository.UpdatePurchaseRange(deactivationPurchases);
                operationDetails = new OperationDetails<int>(true, "", 0);
            }
            catch(Exception ex)
            {
                operationDetails = new OperationDetails<int>(false, ex.Message, 0);
            }

            return operationDetails;
        }

        public async Task<OperationDetails<PurchaseDTO>> GetPurchaseById(int purchaseId)
        {
            OperationDetails<PurchaseDTO> operationDetails = 
                new OperationDetails<PurchaseDTO>(false, "Что-то пошло не так, проверте данные и повториет позже", new PurchaseDTO());

            if (purchaseId <= 0)
                return operationDetails;

            var purchase = await _uof.PurchaseRepository.FindPurchaseByIdAsync(purchaseId);

            if (purchase == null)
                return operationDetails;

            var pruchaseDTO = new PurchaseDTO
            {
                PurchaseId = purchase.PurchaseId,
                TotalCost = purchase.TotalCost,
                DateOfPayment = purchase.DateOfPayment,
                StartDateService = purchase.StartDateService,
                EndDateService = purchase.EndDateService,
                IsPayed = purchase.IsPayed,
                ServicesId = purchase.ServicesId,
                AdId = purchase.AdId
            };

            operationDetails = new OperationDetails<PurchaseDTO>(true, "", pruchaseDTO);

            return operationDetails;
        }

        public async Task<OperationDetails<IQueryable<PurchaseDTO>>> GetPurchaseByUserId(string userId)
        {
            OperationDetails <IQueryable < PurchaseDTO >> operationDetails =
                new OperationDetails<IQueryable<PurchaseDTO>> (false, "Что-то пошло не так, проверте данные и повториет позже", null);

            if (string.IsNullOrEmpty(userId))
                return operationDetails;

            var user = await _uof.UserRepository.FindByIdAsync(userId);
            if (user == null)
                return operationDetails;

            var ads = await _uof.AdRepository.FindAdsByUserId(userId);
            if(ads == null)
                return operationDetails;

            var purchasesDTO = ads.SelectMany(x => x.Purhcases.Select(
            p => new PurchaseDTO
            { 
                PurchaseId = p.PurchaseId,
                TotalCost = p.TotalCost,
                DateOfPayment = p.DateOfPayment,
                StartDateService = p.StartDateService,
                EndDateService = p.EndDateService,
                IsPayed = p.IsPayed,
                IsActive = p.IsActive,
                ServicesId = p.ServicesId,
                ServiceActiveTimeId = p.ServiceActiveTimeId,
                AdId = p.AdId
            })).AsQueryable();

            operationDetails = new OperationDetails<IQueryable<PurchaseDTO>>(true, "", purchasesDTO);

            return operationDetails;
        }

        public async Task<OperationDetails<int>> CreatePurchase(PurchaseDTO purchase)
        {
            OperationDetails<int> operationDetails =
               new OperationDetails<int>(false, "Что-то пошло не так, проверте данные и повториет позже", 0);

            if (purchase.AdId <= 0)
                return operationDetails;

            var ad = await _uof.AdRepository.FindAdByIdAsync(purchase.AdId);
            if (ad == null)
                return operationDetails;

            var service = await _uof.ServiceRepository.FindServiceByIdAsync(purchase.ServicesId);
            if (service == null)
                return operationDetails;

            Purchase newPurchase = new Purchase
            {
                TotalCost = purchase.TotalCost,
                DateOfPayment = purchase.DateOfPayment,
                StartDateService = purchase.StartDateService,
                EndDateService = purchase.EndDateService,
                IsPayed = purchase.IsPayed,
                ServicesId = purchase.ServicesId,
                AdId = purchase.AdId
            };

            try
            {
                newPurchase.PurchaseId = await _uof.PurchaseRepository.AddPurchaseAsync(newPurchase);
                operationDetails = new OperationDetails<int>(true, "", newPurchase.PurchaseId);
            }
            catch (Exception ex)
            {
                operationDetails = new OperationDetails<int>(false, ex.Message, 0);
            }

            return operationDetails;
        }

        public async Task<OperationDetails<int>> UpdatePurchase(PurchaseDTO purchase)
        {
            OperationDetails<int> operationDetails = 
                new OperationDetails<int>(false, "Что-то пошло не так, проверте данные и повториет позже", 0);

            if (purchase.AdId <= 0)
                return operationDetails;

            var ad = await _uof.AdRepository.FindAdByIdAsync(purchase.AdId);
            if (ad == null)
                return operationDetails;

            var oldService = await _uof.ServiceRepository.FindServiceByIdAsync(purchase.ServicesId);
            if (oldService == null)
                return operationDetails;

            var oldPurchase = await _uof.PurchaseRepository.FindPurchaseByIdAsync(purchase.PurchaseId);
            if (oldPurchase == null)
                return operationDetails;

            oldPurchase.TotalCost = purchase.TotalCost;
            oldPurchase.DateOfPayment = purchase.DateOfPayment;
            oldPurchase.StartDateService = purchase.StartDateService;
            oldPurchase.EndDateService = purchase.EndDateService;
            oldPurchase.IsPayed = purchase.IsPayed;
            oldPurchase.ServicesId = purchase.ServicesId;
            oldPurchase.IsActive = purchase.IsActive;
            oldPurchase.AdId = purchase.AdId;

            try
            {
                await _uof.PurchaseRepository.UpdatePurchase(oldPurchase);
                operationDetails = new OperationDetails<int>(true, "", oldPurchase.PurchaseId);
            }
            catch (Exception ex)
            {
                operationDetails = new OperationDetails<int>(false, ex.Message, 0);
            }

            return operationDetails;
        }

        private IQueryable<Purchase> GetAllUserPurchaseByAds(IQueryable<Ad> adsQuery)
        {
            var purchases = _uof.PurchaseRepository.GetAllUserPurchaseByAds(adsQuery);
            return purchases;
        }
    }
}
