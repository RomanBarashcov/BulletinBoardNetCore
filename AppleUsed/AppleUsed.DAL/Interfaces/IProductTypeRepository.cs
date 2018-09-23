using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Interfaces
{
    public interface IProductTypeRepository : IDisposable
    {
        IQueryable<ProductTypes> GetProductTypes();

        Task<int> AdProductType(ProductTypes productType);
    }
}
