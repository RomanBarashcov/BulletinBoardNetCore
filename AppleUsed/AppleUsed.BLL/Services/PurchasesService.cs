using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Infrastructure;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Services
{
    public class PurchasesService : IPurchasesService
    {
        private readonly AppDbContext _db;
        private readonly IAdService _adService;

        public PurchasesService(AppDbContext db, IAdService adService)
        {
            _db = db;
            _adService = adService;
        }

        public async Task<OperationDetails<int>> DiactivationAllPurchaseWhereEndDateServiceIsOut()
        {
            OperationDetails<int> operationDetails =
              new OperationDetails<int>(false, "", 0);

            var purchases = GetPurchases();
            if (purchases == null) return operationDetails;

            var deactivationPurchases = purchases.Where(x => x.IsActive == true && x.EndDateService >= DateTime.Now.Date);
            
            foreach(var item in deactivationPurchases)
            {
                item.IsActive = false;
            }

            try
            {
                _db.UpdateRange(deactivationPurchases);
                await _db.SaveChangesAsync();
                operationDetails = new OperationDetails<int>(true, "", 0);
            }
            catch(Exception ex)
            {
                operationDetails = new OperationDetails<int>(false, ex.Message, 0);
            }

            return operationDetails;
        }

        public IQueryable<PurchaseDTO> GetPurchases()
        {
            var purchases = _db.Purchases.Select(x => new PurchaseDTO
            { 
                PurchaseId = x.PurchaseId,
                TotalCost = x.TotalCost,
                DateOfPayment = x.DateOfPayment,
                StartDateService = x.StartDateService,
                EndDateService = x.EndDateService,
                IsPayed = x.IsPayed,
                ServicesId = x.ServicesId,
                AdId = x.AdId
                
            });

            return purchases;
        }

        public async Task<OperationDetails<PurchaseDTO>> GetPurchaseById(int purchaseId)
        {
            OperationDetails<PurchaseDTO> operationDetails = 
                new OperationDetails<PurchaseDTO>(false, "Что-то пошло не так, проверте данные и повториет позже", new PurchaseDTO());

            if (purchaseId <= 0)
                return operationDetails;

            var purchase = await _db.Purchases.FindAsync(purchaseId);

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

            var user = await _db.Users.FindAsync(userId);
            if (user == null)
                return operationDetails;

            var ads = await _adService.GetAdsByUser(user.UserName);
            if(ads.Property == null)
                return operationDetails;

            var purchasesDTO = GetPurchaseDTOQueryJoinWithUserAds(ads.Property);


            operationDetails = new OperationDetails<IQueryable<PurchaseDTO>>(true, "", purchasesDTO);

            return operationDetails;
        }

        public async Task<OperationDetails<int>> CreatePurchase(PurchaseDTO purchase)
        {
            OperationDetails<int> operationDetails =
               new OperationDetails<int>(false, "Что-то пошло не так, проверте данные и повториет позже", 0);

            if (purchase.AdId <= 0)
                return operationDetails;

            var ad = await _db.Ads.FindAsync(purchase.AdId);
            if (ad == null)
                return operationDetails;

            var service = await _db.Services.FindAsync(purchase.ServicesId);
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
                await _db.Purchases.AddAsync(newPurchase);
                await _db.SaveChangesAsync();

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

            var ad = await _db.Ads.FindAsync(purchase.AdId);
            if (ad == null)
                return operationDetails;

            var oldService = await _db.Services.FindAsync(purchase.ServicesId);
            if (oldService == null)
                return operationDetails;

            var oldPurchase = await _db.Purchases.FindAsync(purchase.PurchaseId);
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
                _db.Purchases.Update(oldPurchase);
                await _db.SaveChangesAsync();

                operationDetails = new OperationDetails<int>(true, "", oldPurchase.PurchaseId);
            }
            catch (Exception ex)
            {
                operationDetails = new OperationDetails<int>(false, ex.Message, 0);
            }

            return operationDetails;
        }

        private IQueryable<PurchaseDTO> GetPurchaseDTOQueryJoinWithUserAds(IQueryable<AdDTO> adsQuery)
        {

                var purchasesDTO = (from ads in adsQuery
                                    join p in _db.Purchases on ads.AdId equals p.AdId
                                    select new PurchaseDTO
                                    {
                                        PurchaseId = p.PurchaseId,
                                        TotalCost = p.TotalCost,
                                        DateOfPayment = p.DateOfPayment,
                                        StartDateService = p.StartDateService,
                                        EndDateService = p.EndDateService,
                                        IsPayed = p.IsPayed,
                                        ServicesId = p.ServicesId,
                                        ServiceActiveTimeId = p.ServiceActiveTimeId,
                                        AdId = p.AdId
                                    });
            
            return purchasesDTO;
        }
    }
}
