using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.EF;
using AppleUsed.DAL.Identity;
using System;

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

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                _db = null;
                disposed = true;
            }
        }
    }
}
