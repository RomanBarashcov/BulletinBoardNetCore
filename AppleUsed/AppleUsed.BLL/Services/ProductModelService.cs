using AppleUsed.BLL.Infrastructure;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Services
{
    public class ProductModelService : IProductModelsService
    {
        private readonly AppDbContext _db;

        public ProductModelService(AppDbContext db)
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

        public IQueryable<ProductModels> GetProductModelsByProductTypeId(int productTypeId)
        {
            var porductModels = (from m in _db.ProductModels where m.ProductModelsId == productTypeId
                                join t in _db.ProductTypes on m.ProductTypes.ProductTypesId                    equals t.ProductTypesId
                                select new ProductModels
                                {
                                    ProductModelsId = m.ProductModelsId,
                                    Name = m.Name,
                                    ProductTypes = t

                                });

            return porductModels;
        }

    }
}
