using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Identity;
using System.Linq;
using AppleUsed.DAL.Entities;
using System;
using AppleUsed.DAL.Interfaces;

namespace AppleUsed.BLL.Services
{
    public class CityAreasService : ICityAreasService, IDisposable
    {
        private ICityAreasRepository _cityAreasRepository;

        public CityAreasService(ICityAreasRepository cityAreasRepository)
        {
            _cityAreasRepository = cityAreasRepository;
        }

        public IQueryable<CityArea> GetCityAreas()
        {
            return _cityAreasRepository.GetCityAreas();
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
                disposed = true;
                _cityAreasRepository.Dispose();
                _cityAreasRepository = null;
            }
        }

    }
}
