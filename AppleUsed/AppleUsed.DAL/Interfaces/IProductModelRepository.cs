using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Interfaces
{
    public interface IProductModelRepository : IDisposable
    {
        IQueryable<ProductModels> GetProductModels();

        Task<ProductModels> FindProductModelAsync(int productModelId);

        IQueryable<ProductModels> FindProductModelsByProductTypeId(int productId);
    }
}
