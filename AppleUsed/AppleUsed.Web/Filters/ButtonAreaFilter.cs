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
        private IQueryable<AdDTO> _query;

        public ButtonAreaFilter(string filteredByState, IQueryable<AdDTO> ads)
        {
            _filteredByState = filteredByState;
            _query = ads;
        }

        public IQueryable<AdDTO> GetFilteredAdsData()
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

        private IQueryable<AdDTO> GetNewProduct()
        {
            return _query.Where(x => x.SelectedProductStatesId == 1);
        }

        private IQueryable<AdDTO> GetUsedProduct()
        {
            return _query.Where(x => x.SelectedProductStatesId == 3);
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            // подавляем финализацию
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {

                }

                _query = null;
                disposed = true;
            }
        }
    }
}
