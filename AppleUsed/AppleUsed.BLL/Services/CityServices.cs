using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Services
{
    public class CityService : ICityService, IDisposable
    {
        private readonly AppDbContext _db;

        public CityService(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<City> GetCities()
        {
            var cities = (from c in _db.Cities
                          join ca in _db.CityAreas
                          on c.CityArea.CityAreaId equals ca.CityAreaId
                          select new City
                          {
                              CityId = c.CityId,
                              Name = c.Name,
                              CityArea = ca

                          });

            return cities;
        }

        public IQueryable<City> GetCitiesByCityAreaId(int cityAreaId)
        {
            var cities = (from c in _db.Cities where c.CityArea.CityAreaId == cityAreaId
                          join ca in _db.CityAreas
                          on c.CityArea.CityAreaId equals ca.CityAreaId
                          select new City
                          {
                              CityId = c.CityId,
                              Name = c.Name,
                              CityArea = ca

                          });

            return cities;
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
            }
        }
    }
}
