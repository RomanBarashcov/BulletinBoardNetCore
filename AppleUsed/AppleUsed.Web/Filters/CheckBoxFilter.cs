﻿using AppleUsed.BLL.DTO;
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
        private List<AdDTO> _adList;

        public CheckBoxFilter(AdIndexViewModel model, List<AdDTO> adList)
        {
            _model = model;
            _adList = adList;
        }

        public async Task<List<AdDTO>> GetFilteredAdsData()
        {
            if (_model.Filter != null)
            {
                SetDefaultValuesIfFilterByPriceEmpty();

                List<AdDTO> ads = await GetAdsWithApplyedFilterByCheckBoxes();
                List<AdDTO> filteringWithPrice = GetAdsWithApplyedFilterByPrice(ads);
                return filteringWithPrice;
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
                return _adList.Where(x => x.Characteristics.ProductType.ProductTypesId == selectedProductId).ToList();

            if (selectedByModels.Count() > 0)
            {
                var byModels = (from ad in _adList
                                join smo in selectedByModels on ad.Characteristics.ProductModel.ProductModelsId equals smo.Id
                                select ad).ToList();

                ads.AddRange(byModels);
            }

            if (selectedByMemories.Count() > 0)
            {
                List<AdDTO> byMemories;

                if (selectedByModels.Count() > 0)
                {
                    byMemories = (from ad in ads
                                      join sm in selectedByMemories on ad.Characteristics.ProductMemorie.ProductMemoriesId equals sm.Id
                                      select ad).ToList();

                    ads = byMemories;
                }
                else
                {
                    byMemories = (from ad in _adList
                                      join sm in selectedByMemories on ad.Characteristics.ProductMemorie.ProductMemoriesId equals sm.Id
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
                                    join sc in selectedByColors on ad.Characteristics.ProductColor.ProductColorsId equals sc.Id
                                    select ad).ToList();
                }
                else
                {
                    byColors = (from ad in _adList
                                join sc in selectedByColors on ad.Characteristics.ProductColor.ProductColorsId equals sc.Id
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
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _model = null;
                    _adList = null;
                }

                disposed = true;
            }
        }
    }
}
