using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Interfaces
{
    public interface IAdPhotoRepository : IDisposable
    {
        Task<int> AddPhotoAsync(AdPhotos adPhoto);

        Task AddPhotoRange(List<AdPhotos> adPhotos);

        Task RemovePhoto(AdPhotos adPhoto);

        Task RemovePhotosRange(List<AdPhotos> adPhotos);
    }
}
