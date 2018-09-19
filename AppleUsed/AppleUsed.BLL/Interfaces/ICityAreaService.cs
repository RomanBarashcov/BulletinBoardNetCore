using AppleUsed.BLL.Services;
using AppleUsed.DAL.Entities;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.BLL.Interfaces
{
    public interface ICityAreasService : IDisposable
    {
        IQueryable<CityArea> GetCityAreas();
    }
}
