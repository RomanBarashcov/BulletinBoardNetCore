using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IPurchasesService
    {
        Task<OperationDetails<int>> DiactivationAllPurchaseWhereEndDateServiceIsOut();

        IQueryable<PurchaseDTO> GetPurchases();

        Task<OperationDetails<PurchaseDTO>> GetPurchaseById(int purchaseId);

        Task<OperationDetails<IQueryable<PurchaseDTO>>> GetPurchaseByUserId(string userId);

        Task<OperationDetails<int>> CreatePurchase(PurchaseDTO purchase);

        Task<OperationDetails<int>> UpdatePurchase(PurchaseDTO purchase);
    }
}
