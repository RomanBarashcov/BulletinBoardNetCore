using AppleUsed.BLL.Infrastructure;
using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IProducTypesService
    {
        Task<OperationDetails<List<ProductTypes>>> GetAllProductTypes();
        Task<OperationDetails<ProductTypes>> GetProductTypeById(int id);
        Task<OperationDetails<int>> CreateProductType(ProductTypes pType);
        Task<OperationDetails<int>> UpdateProductType(int id, ProductTypes pType);
        Task<OperationDetails<int>> DeleteProductType(int id);
    }
}
