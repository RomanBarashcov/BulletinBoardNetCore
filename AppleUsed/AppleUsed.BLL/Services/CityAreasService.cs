using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Identity;
using System.Linq;
using AppleUsed.DAL.Entities;

namespace AppleUsed.BLL.Services
{
    public class CityAreasService : ICityAreasService
    {
        private readonly AppDbContext _db;

        public CityAreasService(AppDbContext db)
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
    }
}
