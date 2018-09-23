using AppleUsed.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Repositories
{
    public class UnityOfWork : IUnityOfWork
    {
        private IAdRepository _adRepository { get; set; }
        private IAdPhotoRepository _adPhotoRepository { get; set; }
        private IAdUpRepository _adUpRepository { get; set; }
        private IAdViewsRepository _adViewsRepository { get; set; }
        private ICityAreasRepository _cityAreasRepository { get; set; }
        private ICityRepository _cityRepository { get; set; }
        private IProductTypeRepository _productTypeRepository { get; set; }
        private IProductModelRepository _productModelRepository { get; set; }
        private IProductMemoriesRepository _productMemoriesRepository { get; set; }
        private IProductColorsRepository _productColorsRepository { get; set; }
        private IProductStatesRepository _productStatesRepository { get; set; }
        private IPurchaseRepository _purchaseRepository { get; set; }
        private IServiceActiveTimeRepository _serviceActiveTimeRepository { get; set; }
        private IServiceRepository _serviceRepository { get; set; }
        private IUserRepository _userRepository { get; set; }
        

        public UnityOfWork(
            IAdRepository adRepository,
            IAdPhotoRepository adPhotoRepository,
            IAdUpRepository adUpRepository,
            IAdViewsRepository adViewsRepository,
            ICityAreasRepository cityAreasRepository,
            ICityRepository cityRepository,
            IProductTypeRepository productTypeRepository,
            IProductModelRepository productModelRepository,
            IProductMemoriesRepository productMemoriesRepository,
            IProductColorsRepository productColorsRepository,
            IProductStatesRepository productStatesRepository,
            IPurchaseRepository purchaseRepository,
            IServiceActiveTimeRepository serviceActiveTimeRepository,
            IServiceRepository serviceRepository,
            IUserRepository userRepository)

        {
            _adRepository = adRepository;
            _adPhotoRepository = adPhotoRepository;
            _adUpRepository = adUpRepository;
            _adViewsRepository = adViewsRepository;
            _cityAreasRepository = cityAreasRepository;
            _cityRepository = cityRepository;
            _productTypeRepository = productTypeRepository;
            _productModelRepository = productModelRepository;
            _productMemoriesRepository = productMemoriesRepository;
            _productColorsRepository = productColorsRepository;
            _productStatesRepository = productStatesRepository;
            _purchaseRepository = purchaseRepository;
            _serviceActiveTimeRepository = serviceActiveTimeRepository;
            _serviceRepository = serviceRepository;
            _userRepository = userRepository;
            
        }

        public IAdRepository AdRepository
        {
            get { return _adRepository; }
            set { _adRepository = value; }
        }

        public IAdPhotoRepository AdPhotoRepository
        {
            get { return _adPhotoRepository; }
            set { _adPhotoRepository = value; }
        }

        public IAdUpRepository AdUpRepository
        {
            get { return _adUpRepository; }
            set { _adUpRepository = value; }
        }

        public IAdViewsRepository AdViewsRepository
        {
            get { return _adViewsRepository; }
            set { _adViewsRepository = value; }
        }

        public ICityAreasRepository CityAreasRepository
        {
            get { return _cityAreasRepository; }
            set { _cityAreasRepository = value; }
        }

        public ICityRepository CityRepository
        {
            get { return _cityRepository; }
            set { _cityRepository = value; }
        }

        public IProductTypeRepository ProductTypeRepository
        {
            get { return _productTypeRepository; }
            set { _productTypeRepository = value; }
        }

        public IProductModelRepository ProductModelRepository
        {
            get { return _productModelRepository; }
            set { _productModelRepository = value; }
        }

        public IProductMemoriesRepository ProductMemoriesRepository
        {
            get { return _productMemoriesRepository; }
            set { _productMemoriesRepository = value; }
        }

        public IProductColorsRepository ProductColorsRepository
        {
            get { return _productColorsRepository; }
            set { _productColorsRepository = value; }
        }

        public IProductStatesRepository ProductStatesRepository
        {
            get { return _productStatesRepository; }
            set { _productStatesRepository = value; }
        }

        public IPurchaseRepository PurchaseRepository
        {
            get { return _purchaseRepository; }
            set { _purchaseRepository = value; }
        }

        public IServiceActiveTimeRepository ServiceActiveTimeRepository
        {
            get { return _serviceActiveTimeRepository; }
            set { _serviceActiveTimeRepository = value; }
        }

        public IServiceRepository ServiceRepository
        {
            get { return _serviceRepository; }
            set { _serviceRepository = value; }
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository; }
            set { _userRepository = value; }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _adRepository.Dispose();
                    _adPhotoRepository.Dispose();
                    _adUpRepository.Dispose();
                    _adViewsRepository.Dispose();
                    _cityAreasRepository.Dispose();
                    _cityRepository.Dispose();
                    _productTypeRepository.Dispose();
                    _productModelRepository.Dispose();
                    _productMemoriesRepository.Dispose();
                    _productColorsRepository.Dispose();
                    _productStatesRepository.Dispose();
                    _purchaseRepository.Dispose();
                    _serviceActiveTimeRepository.Dispose();
                    _serviceRepository.Dispose();
                    _userRepository.Dispose();

                    _adRepository = null;
                    _adPhotoRepository = null;
                    _adUpRepository = null;
                    _adViewsRepository = null;
                    _cityAreasRepository = null;
                    _cityRepository = null;
                    _productTypeRepository = null;
                    _productModelRepository = null;
                    _productMemoriesRepository = null;
                    _productColorsRepository = null;
                    _productStatesRepository = null;
                    _purchaseRepository = null;
                    _serviceActiveTimeRepository = null;
                    _serviceRepository = null;
                    _userRepository = null;
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
