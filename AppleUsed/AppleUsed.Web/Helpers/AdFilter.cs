using AppleUsed.BLL.DTO;
using AppleUsed.Web.Filters;
using AppleUsed.Web.Models.ViewModels.AccountViewModels;
using AppleUsed.Web.Models.ViewModels.AdViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Helpers
{
    public class AdFilter
    {
        private readonly PrepearingModel _prepearingModel;

        public AdFilter(PrepearingModel prepearingModel)
        {
            _prepearingModel = prepearingModel;
        }

        public async Task<IQueryable<AdDTO>> FilteringData(string titleFilter, string adType, IQueryable<AdDTO> adQueryResult, AdIndexViewModel model)
        {
            if (!string.IsNullOrEmpty(titleFilter))
            {
                adQueryResult = adQueryResult.Where(x => x.Title.ToLower().Contains(titleFilter.ToLower()));
            }

            if(model.SearchFilter == null)
                model.SearchFilter = new SearchFilterViewModel() { SelectedProductTypeId = adQueryResult.FirstOrDefault().SelectedProductTypeId };

            adQueryResult = new SerarchNavFilter(model, adQueryResult).SearchNavChanged();

            if (model.SortViewModel != null)
                adQueryResult = new SelectedOptionFilter(model.SortViewModel.SelectedOptionValue, adQueryResult).SelectedOptionChanged();

            adQueryResult = await new CheckBoxFilter(model, adQueryResult).GetFilteredAdsData();
            adQueryResult = new ButtonAreaFilter(adType, adQueryResult).GetFilteredAdsData();

            return adQueryResult;
        }
    }
}
