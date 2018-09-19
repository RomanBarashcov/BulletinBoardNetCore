using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Identity;
using System.Linq;
using AppleUsed.DAL.Entities;
using System;
using AppleUsed.DAL.Interfaces;

namespace AppleUsed.BLL.Services
{
    public class CityAreasService : ICityAreasService
    {
        private IUnityOfWork _uof;

        public CityAreasService(IUnityOfWork uof)
        {
            _uof = uof;
        }

        public IQueryable<CityArea> GetCityAreas()
        {
            return _uof.CityAreasRepository.GetCityAreas();
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
                _uof.Dispose();
                _uof = null;
            }
        }

    }
}
