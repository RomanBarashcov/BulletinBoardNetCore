using AppleUsed.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.BLL.Interfaces
{
    public interface IImageService
    {
        List<AdPhotos> GetPhotosHashList(IFormFileCollection productPhotos);
        List<string> CreatingImageSrcForSmallSize(List<AdPhotos> photoList);
        List<string> CreatingImageSrcForAvgSize(List<AdPhotos> photoList);
        List<string> CreatingImageSrcForBigSize(List<AdPhotos> photoList);
    }
}
