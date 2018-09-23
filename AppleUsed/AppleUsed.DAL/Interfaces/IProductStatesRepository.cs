using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Interfaces
{
    public interface IProductStatesRepository : IDisposable
    {
        IQueryable<ProductStates> GetProductStates();

        Task<int> AddProductState(ProductStates productState);

        Task UpdateProductState(ProductStates productState);

        Task DeleteProductState(int id);
    }
}
