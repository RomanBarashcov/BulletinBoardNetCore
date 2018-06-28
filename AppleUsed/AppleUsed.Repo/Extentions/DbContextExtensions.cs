﻿using System.Linq;
using AppleUsed.Repo.Data;
using AppleUsed.Repo.Helpers;
using AppleUsed.Repo.Identity;

namespace AppleUsed.Repo.Extentions
{
    public static class DbContextExtensions
    {
        public static int EnsureSeedData(this AppDbContext appContext)
        {
            var bookCount = default(int);
            var seriesCount = default(int);
            var bookSeriesCount = default(int);

            var dbSeeder = new DatabaseSeeder(appContext);

            if (!appContext.Books.Any())
            {
                bookCount = dbSeeder.SeedBookEntries().Result;
            }

            if (!appContext.Series.Any())
            {
                seriesCount = dbSeeder.SeedSeriesEntries().Result;
            }

            if (!appContext.BookSeries.Any())
            {
                bookSeriesCount = dbSeeder.SeedBookSeriesEntries().Result;
            }

            return bookCount + seriesCount + bookSeriesCount;
        }
    }
}