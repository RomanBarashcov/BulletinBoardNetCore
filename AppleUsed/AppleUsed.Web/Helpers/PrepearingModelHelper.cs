using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Models.ViewModels.AccountViewModels;
using AppleUsed.Web.Models.ViewModels.AdViewModels;
using AppleUsed.Web.Models.ViewModels.ServicesViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Helpers
{
    public class PrepearingModelHelper : IDisposable
    {
        private IAdService _adService;

        public PrepearingModelHelper(IAdService adService)
        {
            _adService = adService;
        }

        public PrepearingModelHelper() { }

        public AdViewModel PrepearingAdViewModel(IQueryable<CityDTO> citiesDTO,
                                                IQueryable<CityAreaDTO> cityAreasDTO,
                                                IQueryable<ProductTypeDTO> productTypesDTO,
                                                IQueryable<ProductModelsDTO> productModelsDTO,
                                                IQueryable<ProductMemorieDTO> productMemoriesDTO,
                                                IQueryable<ProductColorDTO> productColorsDTO,
                                                IQueryable<ProductStateDTO> productStateDTO, 
                                                AdDTO ad)
        {
            AdViewModel prepearingModel = new AdViewModel { AdDTO = ad };

            prepearingModel.CityesSelectList = new SelectList(citiesDTO, "Id", "Name");
            prepearingModel.CityAreasSelectList = new SelectList(cityAreasDTO, "Id", "Name");

            if (ad.Characteristics != null)
            {
                prepearingModel.ProductModelsSelectList = new SelectList(productModelsDTO.Where(x => x.ProductTypeId == ad.Characteristics.ProductTypesId), "Id", "Name");
            }
            else
            {
                prepearingModel.ProductModelsSelectList = new SelectList(productModelsDTO, "Id", "Name");
            }
                

            prepearingModel.ProductTypesSelectList =
                new SelectList(productTypesDTO, "Id", "Name");

            prepearingModel.ProductMemoriesSelectList =
                new SelectList(productMemoriesDTO, "Id", "StorageSize");

            prepearingModel.ProductColorsSelectList = 
                new SelectList(productColorsDTO, "Id", "Name");

            prepearingModel.ProductStatesSelectList = 
                new SelectList(productStateDTO, "Id", "Name");

            return prepearingModel;
        }

        public async Task<AdIndexViewModel> PrepearingAdIndexViewModel(IQueryable<AdDTO> ads, int selectedProductTypeId)
        {
            AdIndexViewModel adIndexViewModel = new AdIndexViewModel { SimpleAds = ads.ToList() };
            var dataForFilter = _adService.GetDataForCreatingAdOrDataForFilter();

            adIndexViewModel.SearchFilter = new SearchFilterViewModel();
            adIndexViewModel.SortViewModel = new SortViewModel();
            adIndexViewModel.Filter = new FilterViewModel();

            adIndexViewModel.Filter.ProductsModelFilters = new List<ProductsModelFilter>();
            adIndexViewModel.Filter.ProductMemmories = new List<ProductMemmoriesFilter>();
            adIndexViewModel.Filter.ProductsColors = new List<ProductsColorFilter>();

            adIndexViewModel.Filter.ProductsModelFilters =
                GetProductModelsDataForFilter(dataForFilter.productModelsDTO, selectedProductTypeId);

            adIndexViewModel.Filter.ProductMemmories = 
                GetProductMemmoriesDataForFilter(dataForFilter.productMemoriesDTO);

            adIndexViewModel.Filter.ProductsColors = 
                GetProductColorsDataFilter(dataForFilter.productColorsDTO);

            adIndexViewModel.SortViewModel.SortOptionList = GetSerachSelectionOptionsList();
            adIndexViewModel.SearchFilter.ProductTypesOptionList = GetProductTypeSelectionOptionsList();
            adIndexViewModel.SearchFilter.SelectedProductTypeId = selectedProductTypeId;

            adIndexViewModel.Filter.SelectedProductTypeId = adIndexViewModel.SearchFilter.SelectedProductTypeId;

            return adIndexViewModel;
        }

        private List<ProductsModelFilter>
            GetProductModelsDataForFilter(IQueryable<ProductModelsDTO> productModelsDTO, 
            int selectedProductTypeId)
        {
            var productModelsList = productModelsDTO.Where(p => p.ProductTypeId == selectedProductTypeId).OrderByDescending(x => x.Id).ToList();

            List<ProductsModelFilter> productModelsForFilterList = new List<ProductsModelFilter>();

            for (int i = 0; i <= productModelsList.Count - 1; i++)
            {
                productModelsForFilterList.Add(
                new ProductsModelFilter
                {
                    Id = productModelsList[i].Id,
                    Name = productModelsList[i].Name
                });
            }

            return productModelsForFilterList;
        }

        private List<ProductMemmoriesFilter> 
            GetProductMemmoriesDataForFilter(IQueryable<ProductMemorieDTO> ProductMemoriesDTO)
        {
            var productMemmoriesList = ProductMemoriesDTO.OrderBy(x => x.Id).ToList();

            List<ProductMemmoriesFilter> productMemmoriesForFilterList = new List<ProductMemmoriesFilter>();

            for (int i = 0; i <= productMemmoriesList.Count - 1; i++)
            {
                productMemmoriesForFilterList.Add(
                new ProductMemmoriesFilter
                {
                    Id = productMemmoriesList[i].Id,
                    StorageSize = productMemmoriesList[i].StorageSize
                });
            }

            return productMemmoriesForFilterList;
        }

        private List<ProductsColorFilter> 
            GetProductColorsDataFilter(IQueryable<ProductColorDTO> productColorDTO)
        {
            var productColorsList = productColorDTO.OrderBy(x => x.Id).ToList();

            List<ProductsColorFilter> productsColorForFilterList = new List<ProductsColorFilter>();

            for (int i = 0; i <= productColorsList.Count - 1; i++)
            {
                productsColorForFilterList.Add(
                new ProductsColorFilter
                {
                    Id = productColorsList[i].Id,
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
            List<SelectOption> searchOptions = new List<SelectOption>
            {
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


        public ServiceDetailsViewModel ConfigServiceDetailsViewModel(ServiceDetailsViewModel model)
        {
            model.ServiceActiveSevenDays = new ServiceActiveTimesDTO();
            model.ServiceActiveTwoWeeks = new ServiceActiveTimesDTO();
            model.ServiceActiveMonth = new ServiceActiveTimesDTO();

            model.ServiceActiveSevenDays = GetServiceActiveTimeDTOByDayActiveFromList(7, model);
            model.ServiceActiveTwoWeeks = GetServiceActiveTimeDTOByDayActiveFromList(14, model);
            model.ServiceActiveMonth = GetServiceActiveTimeDTOByDayActiveFromList(30, model);

            return model;
        }

        private ServiceActiveTimesDTO GetServiceActiveTimeDTOByDayActiveFromList(int serviceActiveDay, ServiceDetailsViewModel model)
        {
            var serviceActiveTimeList = model.ServiceDetail.ServiceActiveTimes;
            var serviceActiveTimeDto = serviceActiveTimeList.Where(x => x.DaysOfActiveService == serviceActiveDay)
                    .Select(x => new ServiceActiveTimesDTO
                    {

                        ServiceActiveTimeId = x.ServiceActiveTimeId,
                        Cost = x.Cost,
                        DaysOfActiveService = x.DaysOfActiveService,
                        ServiceId = x.ServiceId

                    }).FirstOrDefault();

            return serviceActiveTimeDto;
        }

        public ServicesIndexViewModel ConfigServicesIndexViewModel(ServicesIndexViewModel model)
        {

            for(int s = 0; s <= model.Services.Count() - 1; s++)
            {
                List<SelectOption> serviceActiveDaysOptions = new List<SelectOption>();

                for (int sa = 0; sa <= model.Services[s].ServiceActiveTimes.Count() - 1; sa++)
                {
                    serviceActiveDaysOptions.Add(
                    new SelectOption
                    {
                        Name = model.Services[s].ServiceActiveTimes[sa].DaysOfActiveService.ToString(),
                        ValueOption = model.Services[s].ServiceActiveTimes[sa].ServiceActiveTimeId
                    });
                }

                model.Services[s].SelectListServiceActiveDays = new SelectList(serviceActiveDaysOptions, "ValueOption", "Name");
            }

            return model;
            
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _adService.Dispose();
                    _adService = null;
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
