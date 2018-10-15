using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using AppleUsed.DAL.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Repositories
{
    public class ProductMemoriesRepository : IProductMemoriesRepository
    {
        private AppDbContext _db;

        public ProductMemoriesRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<ProductMemories> GetProductMemories()
        {
            var productMeemmories = _db.ProductMemories;
            return productMeemmories;
        }

        public async Task<int> AddProductMemorie(ProductMemories productMemories)
        {
            await _db.AddAsync(productMemories);
            await _db.SaveChangesAsync();
            return productMemories.ProductMemoriesId;
        }

        public async Task UpdateProductMemorie(ProductMemories productMemories)
        {
            _db.Update(productMemories);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteProductMemorie(int id)
        {
            var oldItem = _db.ProductMemories.Where(x => x.ProductMemoriesId == id).FirstOrDefault();
            _db.Remove(oldItem);
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
