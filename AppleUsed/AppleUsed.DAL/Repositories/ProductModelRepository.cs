using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using AppleUsed.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Repositories
{
    public class ProductModelRepository : IProductModelRepository
    {
        private AppDbContext _db;

        public ProductModelRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<ProductModels> GetProductModels()
        {
            var porductModels = (from m in _db.ProductModels
                                 join t in _db.ProductTypes on m.ProductTypes.ProductTypesId equals t.ProductTypesId
                                 select new ProductModels
                                 {
                                     ProductModelsId = m.ProductModelsId,
                                     Name = m.Name,
                                     ProductTypes = t

                                 });

            return porductModels;
        }

        public async Task<ProductModels> FindProductModelAsync(int productModelId)
        {
            var porductModel = await (from m in _db.ProductModels.Where(x => x.ProductModelsId == productModelId)
                                 join t in _db.ProductTypes on m.ProductTypes.ProductTypesId equals t.ProductTypesId
                                 select new ProductModels
                                 {
                                     ProductModelsId = m.ProductModelsId,
                                     Name = m.Name,
                                     ProductTypes = t

                                 }).FirstOrDefaultAsync();

            return porductModel;
        }

        public IQueryable<ProductModels> FindProductModelsByProductTypeId(int productTypeId)
        {
            var porductModels =  GetProductModels().Where(p => p.ProductModelsId == productTypeId);
            return porductModels;
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
