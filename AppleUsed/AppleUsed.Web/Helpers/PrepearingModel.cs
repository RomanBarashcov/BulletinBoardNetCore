using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Models.ViewModels.AccountViewModels;
using AppleUsed.Web.Models.ViewModels.AdViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Helpers
{
    public class PrepearingModel 
    {
        private readonly IAdService _adService;

        public PrepearingModel(IAdService adService)
        {
            _adService = adService;
        }

        public AdViewModel PrepearingAdViewModel(AdDTO dataForSelectList, AdDTO ad)
        {
            AdViewModel prepearingModel = new AdViewModel { AdDTO = ad };

            prepearingModel.CityAreasSelectList = new SelectList(dataForSelectList.CityAreasList, "CityAreaId", "Name");
            prepearingModel.CityesSelectList = new SelectList(dataForSelectList.CityesList, "CityId", "Name");

            if(ad.SelectedProductTypeId > 0)
                prepearingModel.ProductModelsSelectList = new SelectList(dataForSelectList.ProductModelsList.Where(x => x.ProductTypes.ProductTypesId == ad.SelectedProductTypeId), "ProductModelsId", "Name");

            prepearingModel.ProductTypesSelectList = new SelectList(dataForSelectList.ProductTypesList, "ProductTypesId", "Name");
            prepearingModel.ProductMemoriesSelectList = new SelectList(dataForSelectList.ProductMemoriesList, "ProductMemoriesId", "Name");
            prepearingModel.ProductColorsSelectList = new SelectList(dataForSelectList.ProductColorsList, "ProductColorsId", "Name");
            prepearingModel.ProductStatesSelectList = new SelectList(dataForSelectList.ProductStatesList, "ProductStatesId", "Name");

            return prepearingModel;
        }

        public async Task<AdIndexViewModel> PrepearingAdIndexViewModel(IQueryable<AdDTO> ads, int selectedProductTypeId)
        {
            AdIndexViewModel adIndexViewModel = new AdIndexViewModel { AdList = ads.ToList() };
            AdDTO dataForFilter = await _adService.GetDataForCreatingAdOrDataForFilter();

            adIndexViewModel.SearchFilter = new SearchFilterViewModel();
            adIndexViewModel.SortViewModel = new SortViewModel();
            adIndexViewModel.Filter = new FilterViewModel();

            adIndexViewModel.Filter.ProductsModelFilters = new List<ProductsModelFilter>();
            adIndexViewModel.Filter.ProductMemmories = new List<ProductMemmoriesFilter>();
            adIndexViewModel.Filter.ProductsColors = new List<ProductsColorFilter>();

            adIndexViewModel.Filter.ProductsModelFilters = GetProductModelsDataForFilter(dataForFilter, selectedProductTypeId);
            adIndexViewModel.Filter.ProductMemmories = GetProductMemmoriesDataForFilter(dataForFilter);
            adIndexViewModel.Filter.ProductsColors = GetProductColorsDataFilter(dataForFilter);

            adIndexViewModel.SortViewModel.SortOptionList = GetSerachSelectionOptionsList();
            adIndexViewModel.SearchFilter.ProductTypesOptionList = GetProductTypeSelectionOptionsList();

            adIndexViewModel.Filter.SelectedProductTypeId = adIndexViewModel.SearchFilter.SelectedProductTypeId;

            return adIndexViewModel;
        }

        private List<ProductsModelFilter> GetProductModelsDataForFilter(AdDTO dataForFilter, int selectedProductTypeId)
        {
            var productModelsList = dataForFilter.ProductModelsList.Where(p => p.ProductTypes.ProductTypesId == selectedProductTypeId).OrderByDescending(x => x.ProductModelsId).ToList();

            List<ProductsModelFilter> productModelsForFilterList = new List<ProductsModelFilter>();

            for (int i = 0; i <= productModelsList.Count - 1; i++)
            {
                productModelsForFilterList.Add(
                    new ProductsModelFilter
                    {
                        Id = productModelsList[i].ProductModelsId,
                        Name = productModelsList[i].Name
                    });
            }

            return productModelsForFilterList;
        }

        private List<ProductMemmoriesFilter> GetProductMemmoriesDataForFilter(AdDTO dataForFilter)
        {
            var productMemmoriesList = dataForFilter.ProductMemoriesList.OrderBy(x => x.ProductMemoriesId).ToList();

            List<ProductMemmoriesFilter> productMemmoriesForFilterList = new List<ProductMemmoriesFilter>();

            for (int i = 0; i <= productMemmoriesList.Count - 1; i++)
            {
                productMemmoriesForFilterList.Add(
                    new ProductMemmoriesFilter
                    {
                        Id = productMemmoriesList[i].ProductMemoriesId,
                        Name = productMemmoriesList[i].Name
                    });
            }

            return productMemmoriesForFilterList;
        }

        private List<ProductsColorFilter> GetProductColorsDataFilter(AdDTO dataForFilter)
        {
            var productColorsList = dataForFilter.ProductColorsList.OrderBy(x => x.ProductColorsId).ToList();

            List<ProductsColorFilter> productsColorForFilterList = new List<ProductsColorFilter>();

            for (int i = 0; i <= productColorsList.Count - 1; i++)
            {
                productsColorForFilterList.Add(
                    new ProductsColorFilter
                    {
                        Id = productColorsList[i].ProductColorsId,
                        Name = productColorsList[i].Name
                    });
            }

            return productsColorForFilterList;
        }

        public SelectList GetSerachSelectionOptionsList()
        {
            List<SelectOption> sortOptions = new List<SelectOption> {

                new SelectOption { Name = "По дате добавления (новые - старые)", ValueOption = 1 },
                new SelectOption { Name = "По дате добавления(старые - новые)", ValueOption = 2 },
                new SelectOption { Name = "По цене (от дорогих - к дешовым)", ValueOption = 3 },
                new SelectOption { Name = "По цене (от дешовых - к дорогим)", ValueOption = 4 },
            };

           return new SelectList(sortOptions, "ValueOption", "Name");
        }

        public SelectList GetProductTypeSelectionOptionsList()
        {
            List<SelectOption> searchOptions = new List<SelectOption> {

                new SelectOption { Name = "iPhone", ValueOption = 1 },
                new SelectOption { Name = "iPad", ValueOption = 2 },
                new SelectOption { Name = "Mac & Macbook", ValueOption = 3 },
                new SelectOption { Name = "iPod", ValueOption = 4 },
                new SelectOption { Name = "Apple Watch", ValueOption = 5 },
                new SelectOption { Name = "Apple TV", ValueOption = 6 },
                new SelectOption { Name = "аксессуары", ValueOption = 7 },
            };

            return new SelectList(searchOptions, "ValueOption", "Name");
        }

    }
}
