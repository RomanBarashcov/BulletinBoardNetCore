using AppleUsed.BLL.Infrastructure;
using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IProductStatesService
    {
        Task<OperationDetails<List<ProductStates>>> GetAllProductStates();
        Task<OperationDetails<ProductStates>> GetProductStateById(int id);
        Task<OperationDetails<int>> CreateProductState(ProductStates pState);
        Task<OperationDetails<int>> UpdateProductState(int id, ProductStates pState);
        Task<OperationDetails<int>> DeleteProductState(int id);
    }
}
