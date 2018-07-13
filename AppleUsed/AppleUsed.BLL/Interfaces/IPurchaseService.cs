using AppleUsed.BLL.Infrastructure;
using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IPurchaseService
    {
        Task<OperationDetails<List<Purchase>>> GetAllPurchases();
        Task<OperationDetails<Purchase>> GetPurchaseById(int id);
        Task<OperationDetails<int>> CreatePurchase(Purchase purchase);
        Task<OperationDetails<int>> UpdatePurchase(int id, Purchase purchase);
        Task<OperationDetails<int>> DeletePurchase(int id);
    }
}
