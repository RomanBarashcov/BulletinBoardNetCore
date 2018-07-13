using AppleUsed.BLL.Infrastructure;
using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IProductMemoriesService
    {
        Task<OperationDetails<List<ProductMemories>>> GetAllProductMemories();
        Task<OperationDetails<ProductMemories>> GetProductMemoryById(int id);
        Task<OperationDetails<int>> CreateProductMemorie(ProductMemories productMemorie);
        Task<OperationDetails<int>> UpdateProductMemorie(int id, ProductMemories productMemorie);
        Task<OperationDetails<int>> DeleteProductMemories(int id);
    }
}
