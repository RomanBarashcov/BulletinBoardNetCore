using AppleUsed.BLL.Infrastructure;
using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IProductColorsService
    {
        Task<OperationDetails<List<ProductColors>>> GetAllProductColors();
        Task<OperationDetails<ProductColors>> GetProductColorById(int id);
        Task<OperationDetails<int>> CreateProductColor(ProductColors productColor);
        Task<OperationDetails<int>> UpdateProductColor(int id, ProductColors productColor);
        Task<OperationDetails<int>> DeleteProductColor(int id);
    }
}
