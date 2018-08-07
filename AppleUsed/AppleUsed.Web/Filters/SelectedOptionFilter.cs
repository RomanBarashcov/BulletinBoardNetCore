using AppleUsed.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Filters
{
    public class SelectedOptionFilter : IDisposable
    {
        private string _filteredbySelectOption;
        private IQueryable<AdDTO> _query;

        public SelectedOptionFilter(string filteredbySelectOption, IQueryable<AdDTO> ads)
        {
            _filteredbySelectOption = filteredbySelectOption;
            _query = ads;
        }

        public IQueryable<AdDTO> SelectedOptionChanged()
        {
            switch (_filteredbySelectOption)
            {
                case "1":
                    return _query.OrderByDescending(x => x.DateUpdated);
                case "2":
                    return _query.OrderBy(x => x.DateUpdated);
                case "3":
                    return _query.OrderByDescending(x => x.Price);
                case "4":
                    return _query.OrderBy(x => x.Price);
                default:
                    return _query;
            }
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

                }

                _query = null;
                disposed = true;
            }
        }
    }
}
