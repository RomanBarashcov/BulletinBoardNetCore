using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using AppleUsed.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Services
{
    public class CityService : ICityService
    {
        private IUnityOfWork _uof;

        public CityService(IUnityOfWork uof)
        {
            _uof = uof;
        }

        public IQueryable<City> GetCities()
        {
            return _uof.CityRepository.GetCities();
        }

        public IQueryable<City> GetCitiesByCityAreaId(int cityAreaId)
        {
            return _uof.CityRepository.GetCitiesByCityAreaId(cityAreaId);
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
