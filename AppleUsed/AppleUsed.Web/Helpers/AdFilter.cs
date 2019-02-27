using AppleUsed.BLL.DTO;
using AppleUsed.Web.Filters;
using AppleUsed.Web.Models.ViewModels.AccountViewModels;
using AppleUsed.Web.Models.ViewModels.AdViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Helpers
{
    public class AdFilter
    {
        private readonly PrepearingModelHelper _prepearingModel;

        public AdFilter(PrepearingModelHelper prepearingModel)
        {
            _prepearingModel = prepearingModel;
        }

        public async Task<List<AdDTO>> FilteringData(
            string titleFilter,
            string productState, 
            List<AdDTO> adQueryResult, 
            AdIndexViewModel model)
        {
            if (!string.IsNullOrEmpty(titleFilter))
            {
                adQueryResult = adQueryResult.Where(x => x.Title.ToLower().Contains(titleFilter.ToLower())).ToList();
            }

            if(model.SearchFilter == null)
                model.SearchFilter = new SearchFilterViewModel()
                {
                    SelectedProductTypeId = adQueryResult.FirstOrDefault().Characteristics.ProductType.ProductTypesId
                };

            adQueryResult = new SerarchNavFilter(model, adQueryResult).SearchNavChanged();

            if (model.SortViewModel != null)
                adQueryResult = new SelectedOptionFilter(model.SortViewModel.SelectedOptionValue, adQueryResult).SelectedOptionChanged();

            adQueryResult = await new CheckBoxFilter(model, adQueryResult).GetFilteredAdsData();
            adQueryResult = new ButtonAreaFilter(productState, adQueryResult).GetFilteredAdsData();

            return adQueryResult;
        }

        public async Task<AdIndexViewModel> PrepearingFilter(List<AdDTO> adQueryResult, AdIndexViewModel model)
        {
            if (model.Filter == null)
            {
                model = await _prepearingModel.PrepearingAdIndexViewModel(adQueryResult, model.SearchFilter.SelectedProductTypeId);
            }
            else
            {
                model.SortViewModel.SortOptionList = 
                    _prepearingModel.GetSerachSelectionOptionsList();

                model.SearchFilter.ProductTypesOptionList =
                    _prepearingModel.GetProductTypeSelectionOptionsList();

                model.Filter.SelectedProductTypeId = 
                    model.SearchFilter.SelectedProductTypeId;

                model.SimpleAds = adQueryResult.ToList();
            }

            return model;
        }
    }
}
