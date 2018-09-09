using AppleUsed.BLL.DTO;
using AppleUsed.Web.Models.ViewModels.AdViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Filters
{
    public class SerarchNavFilter
    {
        private AdIndexViewModel _model;
        private IQueryable<AdDTO> _query;

        public SerarchNavFilter(AdIndexViewModel model, IQueryable<AdDTO> ads)
        {
            _model = model;
            _query = ads;
        }

        public IQueryable<AdDTO> SearchNavChanged()
        {
            IQueryable<AdDTO> queryResult = _query;

            if (_model.SearchFilter != null)
            {
                if(_model.SearchFilter.SelectedProductTypeId > 0)
                    queryResult = _query.Where(x => x.SelectedProductTypeId == _model.SearchFilter.SelectedProductTypeId);

                if (!String.IsNullOrEmpty(_model.SearchFilter.SearchByCity))
                    queryResult = queryResult.Where(x => x.SelectedCity.ToLower().Contains(_model.SearchFilter.SearchByCity.ToLower()));  
            }
           
            return queryResult;
        }
    }
}
