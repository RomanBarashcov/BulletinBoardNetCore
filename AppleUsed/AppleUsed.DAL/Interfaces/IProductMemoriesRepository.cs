using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Interfaces
{
    public interface IProductMemoriesRepository : IDisposable
    {
        IQueryable<ProductMemories> GetProductMemories();

        Task<int> AddProductMemorie(ProductMemories productMemories);

        Task UpdateProductMemorie(ProductMemories productMemories);

        Task DeleteProductMemorie(int id);
    }
}
