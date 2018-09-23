using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using AppleUsed.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Repositories
{
    public class AdPhotoRepository : IAdPhotoRepository
    {
        private AppDbContext _db;

        public AdPhotoRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddPhotoAsync(AdPhotos adPhoto)
        {
            await _db.AddAsync(adPhoto);
            await _db.SaveChangesAsync();
            return adPhoto.AdPhotosId;
        }

        public async Task AddPhotoRange(List<AdPhotos> adPhotos)
        {
            _db.AddRange(adPhotos);
            await _db.SaveChangesAsync();
        }

        public async Task RemovePhoto(AdPhotos adPhoto)
        {
            _db.Remove(adPhoto);
            await _db.SaveChangesAsync();
        }

        public async Task RemovePhotosRange(List<AdPhotos> adPhotos)
        {
            _db.RemoveRange(adPhotos);
            await _db.SaveChangesAsync();
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
