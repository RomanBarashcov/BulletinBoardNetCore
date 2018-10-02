using AppleUsed.BLL.Infrastructure;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using AppleUsed.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Services
{
    public class ProductModelService : IProductModelsService
    {
        private IUnityOfWork _uof;

        public ProductModelService(IUnityOfWork uof)
        {
            _uof = uof;
        }

        public IQueryable<ProductModels> GetProductModels()
        {
            var porductModels = _uof.ProductModelRepository.GetProductModels();
            return porductModels;
        }

        public async Task<ProductModels> GetProductModelById(int productModelId)
        {
            var productModel = await _uof.ProductModelRepository.FindProductModelAsync(productModelId);
            return productModel;
        }

        public IQueryable<ProductModels> GetProductModelsByProductTypeId(int productTypeId)
        {
            var porductModels = _uof.ProductModelRepository
                .FindProductModelsByProductTypeId(productTypeId);

            return porductModels;
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;
                _uof.Dispose();
                _uof = null;
            }
        }
    }
}
