using AppleUsed.BLL.DTO;
using AppleUsed.Web.Models.ViewModels.AdViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Filters
{
    public class CheckBoxFilter : IDisposable
    { 
        private AdIndexViewModel _model;
        private IQueryable<AdDTO> _adList;

        public CheckBoxFilter(AdIndexViewModel model, IQueryable<AdDTO> adList)
        {
            _model = model;
            _adList = adList;
        }

        public async Task<IQueryable<AdDTO>> GetFilteredAdsData()
        {
            if (_model.Filter != null)
            {
                SetDefaultValuesIfFilterByPriceEmpty();

                List<AdDTO> ads = await GetAdsWithApplyedFilterByCheckBoxes();
                List<AdDTO> filteringWithPrice = GetAdsWithApplyedFilterByPrice(ads);
                return filteringWithPrice.AsQueryable();
            }

            return _adList;
        }

        private void SetDefaultValuesIfFilterByPriceEmpty()
        {
            if (String.IsNullOrEmpty(_model.Filter.PriceFilterFrom))
                _model.Filter.PriceFilterFrom = "0";

            if (String.IsNullOrEmpty(_model.Filter.PriceFilterTo))
                _model.Filter.PriceFilterTo = _adList.Max(x => x.Price).ToString();
        }

        private async Task<List<AdDTO>> GetAdsWithApplyedFilterByCheckBoxes()
        {
            var ads = new List<AdDTO>();

            int selectedProductId = _model.SearchFilter.SelectedProductTypeId != 0 ? _model.SearchFilter.SelectedProductTypeId : _model.Filter.SelectedProductTypeId;
            var selectedByModels = _model.Filter.ProductsModelFilters.Where(x => x.Selected).AsQueryable();
            var selectedByMemories = _model.Filter.ProductMemmories.Where(x => x.Selected).AsQueryable();
            var selectedByColors = _model.Filter.ProductsColors.Where(x => x.Selected).AsQueryable();

            if (selectedByModels.Count() == 0 && selectedByColors.Count() == 0 && selectedByMemories.Count() == 0)
                return await _adList.Where(x => x.SelectedProductTypeId == selectedProductId).ToListAsync();

            if (selectedByModels.Count() > 0)
            {
                var byModels = (from ad in _adList
                                join smo in selectedByModels on ad.SelectedProductModelId equals smo.Id
                                select ad).ToList();

                ads.AddRange(byModels);
            }

            if (selectedByMemories.Count() > 0)
            {
                List<AdDTO> byMemories;

                if (selectedByModels.Count() > 0)
                {
                    byMemories = (from ad in ads
                                      join sm in selectedByMemories on ad.SelectedProductMemoryId equals sm.Id
                                      select ad).ToList();

                    ads = byMemories;
                }
                else
                {
                    byMemories = (from ad in _adList
                                      join sm in selectedByMemories on ad.SelectedProductMemoryId equals sm.Id
                                      select ad).ToList();

                    ads.AddRange(byMemories);
                }
            }


            if (selectedByColors.Count() > 0)
            {
                List<AdDTO> byColors;

                if (selectedByModels.Count() > 0 || selectedByMemories.Count() > 0)
                {
                    byColors = (from ad in ads
                                    join sc in selectedByColors on ad.SelectedProductColorId equals sc.Id
                                    select ad).ToList();

                    ads = byColors;
                }
                else
                {
                    byColors = (from ad in _adList
                                join sc in selectedByColors on ad.SelectedProductColorId equals sc.Id
                                select ad).ToList();

                    ads.AddRange(byColors);
                }   
            }

            return ads;
        }

        private List<AdDTO> GetAdsWithApplyedFilterByPrice(List<AdDTO> ads)
        {
            var filteringWithPrice = ads.Where(x => x.Price >= decimal.Parse(_model.Filter.PriceFilterFrom)
                   && x.Price <= decimal.Parse(_model.Filter.PriceFilterTo)).ToList();

            return filteringWithPrice;
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


                _model = null;
                _adList = null;
                disposed = true;
            }
        }
    }
}
