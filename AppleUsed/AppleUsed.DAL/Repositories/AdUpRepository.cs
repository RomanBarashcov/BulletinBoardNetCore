using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using AppleUsed.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Repositories
{
    public class AdUpRepository : IAdUpRepository
    {
        private AppDbContext _db;

        public AdUpRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<AdUp> FindByAdIdAsync(int adId)
        {
            return await _db.AdUps.Where(a => a.AdId == adId).FirstOrDefaultAsync();
        }

        public async Task<int> AddAsync(AdUp adUp)
        {
            try
            {
                await _db.AdUps.AddAsync(adUp);
                await _db.SaveChangesAsync();
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return adUp.AdUpId;
        }

        public async Task<int> Update(AdUp adUp)
        {
            try
            {
                _db.AdUps.Update(adUp);
                await _db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return adUp.AdUpId;
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
