using AppleUsed.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.BLL.Interfaces
{
    public interface IImageService
    {
        List<AdPhotos> GetBinaryPhotoList(IFormFileCollection productPhotos);
        List<string> CreatingImageSrc(List<AdPhotos> photoList);
    }
}
