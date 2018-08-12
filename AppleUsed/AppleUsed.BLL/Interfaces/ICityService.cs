using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface ICityService
    {
        IQueryable<City> GetCities();
        IQueryable<City> GetCitiesByCityAreaId(int cityAreaId);
    }
}
