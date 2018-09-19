using AppleUsed.DAL.Entities;
using System;
using System.Linq;

namespace AppleUsed.BLL.Interfaces
{
    public interface ICityService : IDisposable
    {
        IQueryable<City> GetCities();
        IQueryable<City> GetCitiesByCityAreaId(int cityAreaId);
    }
}
