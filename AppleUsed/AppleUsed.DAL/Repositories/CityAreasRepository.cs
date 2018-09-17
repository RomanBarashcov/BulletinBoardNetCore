using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using AppleUsed.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppleUsed.DAL.Repositories
{
    public class CityAreasRepository : ICityAreasRepository
    {
        private AppDbContext _db;

        public CityAreasRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<CityArea> GetCityAreas()
        {
            var cityAreas = (from ca in _db.CityAreas
                             join c in _db.Cities on ca.CityAreaId equals c.CityArea.CityAreaId
                             into citiesResult
                             select new CityArea
                             {
                                 CityAreaId = ca.CityAreaId,
                                 Name = ca.Name,
                                 Cities = citiesResult.ToList()
                             });

            return cityAreas;
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
