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
        private List<AdDTO> _query;

        public SelectedOptionFilter(string filteredbySelectOption, List<AdDTO> ads)
        {
            _filteredbySelectOption = filteredbySelectOption;
            _query = ads;
        }

        public List<AdDTO> SelectedOptionChanged()
        {
            if(_filteredbySelectOption == null)
                return _query;

            switch (_filteredbySelectOption)
            {
                case "1":
                    return _query.OrderByDescending(x => x.DateUpdated).ToList();
                case "2":
                    return _query.OrderBy(x => x.DateUpdated).ToList();
                case "3":
                    return _query.OrderByDescending(x => x.Price).ToList();
                case "4":
                    return _query.OrderBy(x => x.Price).ToList();
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
