using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using AppleUsed.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppleUsed.DAL.Repositories
{
    public class CityRepository : ICityRepository
    {
        private AppDbContext _db;

        public CityRepository(AppDbContext db)
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

        public City FindCity(int cityId)
        {
            var city = (from c in _db.Cities.Where(c => c.CityId == cityId)
                          join ca in _db.CityAreas
                          on c.CityArea.CityAreaId equals ca.CityAreaId
                          select new City
                          {
                              CityId = c.CityId,
                              Name = c.Name,
                              CityArea = ca

                          }).FirstOrDefault();

            return city;
        }

        public IQueryable<City> GetCitiesByCityAreaId(int cityAreaId)
        {
            var cities = (from c in _db.Cities
                          where c.CityArea.CityAreaId == cityAreaId
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

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db = null;
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
