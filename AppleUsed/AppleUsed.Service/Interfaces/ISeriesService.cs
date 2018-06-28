﻿using System.Collections.Generic;
using AppleUsed.Data.Entities;

namespace AppleUsed.Service.Interfaces
{
    public interface ISeriesService
    {
        IEnumerable<Series> GetSeries();
        Series GetSeries(int id);
        void InsertSeries(Series series);
        void UpdateSeries(Series series);
        void DeleteSeries(int id);
    }
}