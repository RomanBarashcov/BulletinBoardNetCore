using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Interfaces
{
    public interface IProductColorsRepository : IDisposable
    {
        IQueryable<ProductColors> GetProductColors();

        Task<int> AddProductColor(ProductColors productColor);

        Task UpdateProductColor(ProductColors productColor);

        Task DeleteProductColor(int id);
    }
}
