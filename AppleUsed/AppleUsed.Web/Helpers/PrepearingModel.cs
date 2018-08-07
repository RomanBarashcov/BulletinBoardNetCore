using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
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

            //model.CityAreasSelectList = new SelectList(model.AdDTO.CityAreasList, "CityAreaId", "Name");
            //model.CityesSelectList = new SelectList(model.AdDTO.CityesList, "CityId", "Name");

            if(ad.SelectedProductTypeId > 0)
                prepearingModel.ProductModelsSelectList = new SelectList(dataForSelectList.ProductModelsList.Where(x => x.ProductTypes.ProductTypesId == ad.SelectedProductTypeId), "ProductModelsId", "Name");

            prepearingModel.ProductTypesSelectList = new SelectList(dataForSelectList.ProductTypesList, "ProductTypesId", "Name");
            prepearingModel.ProductMemoriesSelectList = new SelectList(dataForSelectList.ProductMemoriesList, "ProductMemoriesId", "Name");
            prepearingModel.ProductColorsSelectList = new SelectList(dataForSelectList.ProductColorsList, "ProductColorsId", "Name");
            prepearingModel.ProductStatesSelectList = new SelectList(dataForSelectList.ProductStatesList, "ProductStatesId", "Name");

            return prepearingModel;
        }

        public async Task<AdIndexViewModel> PrepearingAdIndexViewModel(IQueryable<AdDTO> ads, string selectedProductType)
        {
            AdIndexViewModel adIndexViewModel = new AdIndexViewModel { AdList = ads.ToList() };
            AdDTO dataForFilter = await _adService.GetDataForCreatingAdOrDataForFilter();

            adIndexViewModel.SortViewModel = new SortViewModel();
            adIndexViewModel.Filter = new FilterViewModel();

            adIndexViewModel.Filter.ProductsModelFilters = new List<ProductsModelFilter>();
            adIndexViewModel.Filter.ProductMemmories = new List<ProductMemmoriesFilter>();
            adIndexViewModel.Filter.ProductsColors = new List<ProductsColorFilter>();

            var productModelsList = dataForFilter.ProductModelsList.Where(p => p.ProductTypes.Name == selectedProductType).OrderByDescending(x => x.Name).ToList();

            for (int i = 0; i <= productModelsList.Count - 1; i++)
            {
                adIndexViewModel.Filter.ProductsModelFilters.Add(
                    new ProductsModelFilter
                    {
                        Id = productModelsList[i].ProductModelsId,
                        Name = productModelsList[i].Name
                    });
            }

            var productMemmoriesList = dataForFilter.ProductMemoriesList.OrderBy(x => x.ProductMemoriesId).ToList();

            for (int i = 0; i <= productMemmoriesList.Count - 1; i++)
            {
                adIndexViewModel.Filter.ProductMemmories.Add(
                    new ProductMemmoriesFilter
                    {
                        Id = productMemmoriesList[i].ProductMemoriesId,
                        Name = productMemmoriesList[i].Name
                    });
            }

            var productColorsList = dataForFilter.ProductColorsList.OrderBy(x => x.ProductColorsId).ToList();

            for (int i = 0; i <= productColorsList.Count - 1; i++)
            {
                adIndexViewModel.Filter.ProductsColors.Add(
                    new ProductsColorFilter
                    {
                        Id = productColorsList[i].ProductColorsId,
                        Name = productColorsList[i].Name
                    });
            }

            adIndexViewModel.SortViewModel.SortOptionList = GetSelectionOptionsList(); ;

            return adIndexViewModel;
        }

        public SelectList GetSelectionOptionsList()
        {
            List<SortOption> sortOptions = new List<SortOption> {

                new SortOption { Name = "По дате добавления (новые - старые)", ValueOption = 1 },
                new SortOption { Name = "По дате добавления(старые - новые)", ValueOption = 2 },
                new SortOption { Name = "По цене (от дорогих - к дешовым)", ValueOption = 3 },
                new SortOption { Name = "По цене (от дешовых - к дорогим)", ValueOption = 4 },
            };

           return new SelectList(sortOptions, "ValueOption", "Name");
        }
    }
}
