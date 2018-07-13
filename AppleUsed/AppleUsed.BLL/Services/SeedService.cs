using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.EF;
using AppleUsed.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.BLL.Services
{
    public class SeedService : ISeedService
    {
        private AppDbContext _db;

        public SeedService(AppDbContext context)
        {
            _db = context;
            SeedData();
        }

        public void SeedData()
        {
            Seed seed = new Seed(_db);
        }
    }
}
