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
    public class ProductStatesRepository : IProductStatesRepository
    {
        private AppDbContext _db;

        public ProductStatesRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<ProductStates> GetProductStates()
        {
            var productStates = _db.ProductStates;
            return productStates;
        }

        public async Task<int> AddProductState(ProductStates productState)
        {
            await _db.ProductStates.AddAsync(productState);
            await _db.SaveChangesAsync();
            return productState.ProductStatesId;
        }

        public async Task UpdateProductState(ProductStates productState)
        {
            _db.ProductStates.Update(productState);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteProductState(int id)
        {
            var oldItem = await _db.ProductColors.FindAsync(id);
            _db.ProductColors.Remove(oldItem);
            await _db.SaveChangesAsync();
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
