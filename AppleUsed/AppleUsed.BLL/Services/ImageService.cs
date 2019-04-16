using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace AppleUsed.BLL.Services
{
    public class ImageService : IImageService
    {
        private IImageCompressorService _imageCompressor;

        public ImageService(IImageCompressorService imageCompressor)
        {
            _imageCompressor = imageCompressor;
        }

        public List<AdPhotos> GetPhotosHashList(IFormFileCollection productPhotos)
        {
            List<AdPhotos> photosList = new List<AdPhotos>();

            foreach (var uploadedFile in productPhotos)
            {
                using (var binaryReader = new BinaryReader(uploadedFile.OpenReadStream()))
                {
                    byte[] photoBytes = binaryReader.ReadBytes((int)uploadedFile.Length);

                     photosList.Add(
                        new AdPhotos
                        {
                            PhotoHashSmall = _imageCompressor.CompressingImageForSmallSize(photoBytes),
                            PhotoHashAvg = _imageCompressor.CompressingImageForAvgSize(photoBytes),
                            PhotoHashBig = _imageCompressor.CompressingImageForBigSize(photoBytes),
                            AdPhotoName = uploadedFile.FileName,
                        });
                }
            }

            return photosList;
        }


        public List<string> CreatingImageSrcForSmallSize(List<AdPhotos> photoList)
        {
            List<string> imageSrcList = new List<string>();

            for (int i = 0; i <= photoList.Count - 1; i++)
            {
                imageSrcList.Add(String.Format("data:image/jpg;base64,{0}", photoList[i].PhotoHashSmall));
            }

            return imageSrcList;
        }

        public List<string> CreatingImageSrcForAvgSize(List<AdPhotos> photoList)
        {
            List<string> imageSrcList = new List<string>();

            for(int i = 0; i <= photoList.Count - 1; i++)
            {
                imageSrcList.Add(String.Format("data:image/jpg;base64,{0}", photoList[i].PhotoHashAvg));
            }

            return imageSrcList;
        }

        public List<string> CreatingImageSrcForBigSize(List<AdPhotos> photoList)
        {
            List<string> imageSrcList = new List<string>();

            for (int i = 0; i <= photoList.Count - 1; i++)
            {
                imageSrcList.Add(String.Format("data:image/jpg;base64,{0}", photoList[i].PhotoHashBig));
            }

            return imageSrcList;
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
                disposed = true;
                _imageCompressor = null;
            }
        }
    }
}
