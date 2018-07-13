using AppleUsed.BLL.Infrastructure;
using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IProductModelsService
    {
        Task<List<ProductModels>> GetAllProductModels();
        Task<ProductModels> GetProductModelById(int id);
        Task<OperationDetails<int>> CreateProductModel(ProductModels productModel);
        Task<OperationDetails<int>> UpdateProductModel(int id, ProductModels productModel);
        Task<OperationDetails<int>> DeleteProductModel(int id);
    }
}
