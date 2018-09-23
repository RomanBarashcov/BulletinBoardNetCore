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
    public class ProductColorsRepository : IProductColorsRepository
    {
        private AppDbContext _db;

        public ProductColorsRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<ProductColors> GetProductColors()
        {
            var productColors = _db.ProductColors;
            return productColors;
        }

        public async Task<int> AddProductColor(ProductColors productColor)
        {
            await _db.ProductColors.AddAsync(productColor);
            await _db.SaveChangesAsync();
            return productColor.ProductColorsId;
        }

        public async Task UpdateProductColor(ProductColors productColor)
        {
            _db.ProductColors.Update(productColor);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteProductColor(int id)
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
