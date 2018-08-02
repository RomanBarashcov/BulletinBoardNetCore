using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppleUsed.BLL.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageCompressorService _imageCompressor;

        public ImageService(IImageCompressorService imageCompressor)
        {
            _imageCompressor = imageCompressor;
        }

        public List<AdPhotos> GetBinaryPhotoList(IFormFileCollection productPhotos)
        {
            List<AdPhotos> photosList = new List<AdPhotos>();

            foreach (var uploadedFile in productPhotos)
            {
                using (var binaryReader = new BinaryReader(uploadedFile.OpenReadStream()))
                {
                    photosList.Add(
                        new AdPhotos
                        {
                            Photo = _imageCompressor.CompresingImage(binaryReader.ReadBytes((int)uploadedFile.Length)),
                            AdPhotoName = uploadedFile.FileName,
                        });
                }
            }

            return photosList;
        }

        public List<string> CreatingImageSrc(List<AdPhotos> photoList)
        {
            List<string> imageSrcList = new List<string>();

            foreach (var item in photoList)
            {
                if (item.Photo != null)
                {
                    using (MagickImage image = new MagickImage(item.Photo))
                    {
                        var base64 = image.ToBase64();
                        imageSrcList.Add(String.Format("data:image/jpg;base64,{0}", base64));
                    }
                }
            }

            return imageSrcList;
        }
    }
}
