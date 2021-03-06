﻿using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppleUsed.DAL.Interfaces
{
    public interface ICityRepository : IDisposable
    {
        IQueryable<City> GetCities();

        City FindCity(int cityId);

        IQueryable<City> GetCitiesByCityAreaId(int cityAreaId);
    }
}
