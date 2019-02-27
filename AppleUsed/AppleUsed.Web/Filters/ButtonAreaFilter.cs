using AppleUsed.BLL.DTO;
using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Filters
{
    public class ButtonAreaFilter : IDisposable
    {
        private string _filteredByState;
        private List<AdDTO> _query;

        public ButtonAreaFilter(string filteredByState, List<AdDTO> ads)
        {
            _filteredByState = filteredByState;
            _query = ads;
        }

        public List<AdDTO> GetFilteredAdsData()
        {
            switch(_filteredByState)
            {
                case "Все":
                    return _query;
                case "Б/У":
                    return GetUsedProduct();
                case "Новые":
                    return GetNewProduct();
                default:
                    return _query;
            }
        }

        private List<AdDTO> GetNewProduct()
        {
            return _query.Where(x => x.Characteristics.ProductState.ProductStatesId == 1).ToList();
        }

        private List<AdDTO> GetUsedProduct()
        {
            return _query.Where(x => x.Characteristics.ProductState.ProductStatesId == 3).ToList();
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
                if (disposing)
                {
                    _query = null;
                }
                disposed = true;
            }
        }
    }
}
