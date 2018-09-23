using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using AppleUsed.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Repositories
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private AppDbContext _db;

        public ProductTypeRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<ProductTypes> GetProductTypes()
        {
            var productTypes = _db.ProductTypes;
            return productTypes;
        }

        public async Task<int> AdProductType(ProductTypes productType)
        {
            await _db.ProductTypes.AddAsync(productType);
            await _db.SaveChangesAsync();
            return productType.ProductTypesId;
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db = null;
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
