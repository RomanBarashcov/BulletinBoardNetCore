using AppleUsed.BLL.Interfaces;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.BLL.Services
{
    public class ImageCompressorService : IImageCompressorService, IDisposable
    {
        public ImageCompressorService() { }

        public string CompressingImageForSmallSize(byte[] inputImage)
        {
            string imageHash = "";
            const int size = 170;
            const int height = 130;

            imageHash = CompresingImage(inputImage, size, height);

            return imageHash;
        }

        public string CompressingImageForAvgSize(byte[] inputImage)
        {
            string imageHash = "";
            const int size = 650;
            const int height = 400;

            imageHash = CompresingImage(inputImage, size, height);

            return imageHash;
        }

        public string CompressingImageForBigSize(byte[] inputImage)
        {
            string imageHash = "";
            const int size = 1280;
            const int height = 1024;

            imageHash = CompresingImage(inputImage, size, height);

            return imageHash;
        }

        public List<string> CompressingImagesForSmallSize(List<byte[]> inputImages)
        {
            List<string> imagesHashlist = new List<string>();
            const int size = 170;
            const int height = 130;

            for (int i = 0; i <= inputImages.Count - 1; i++)
            {
                string imgHash = CompresingImage(inputImages[i], size, height);
                imagesHashlist.Add(imgHash);
            }

            return imagesHashlist;
        }

        public List<string> CompressingImagesForAvgSize(List<byte[]> inputImages)
        {
            List<string> imagesHashlist = new List<string>();
            const int size = 650;
            const int height = 400;

            for (int i = 0; i <= inputImages.Count - 1; i++)
            {
                string imgHash = CompresingImage(inputImages[i], size, height);
                imagesHashlist.Add(imgHash);
            }

            return imagesHashlist;
        }

        public List<string> CompressingImagesForBigSize(List<byte[]> inputImages)
        {
            List<string> imagesHashlist = new List<string>();
            const int size = 1280;
            const int height = 1024;

            for (int i = 0; i <= inputImages.Count - 1; i++)
            {
                string imgHash = CompresingImage(inputImages[i], size, height);
                imagesHashlist.Add(imgHash);
            }

            return imagesHashlist;
        }

        private string CompresingImage(byte[] inputImage, int size, int height)
        {
            int _size = size;
            int _height = height;
            const int quality = 40;
            string outputImage = "";

            if (inputImage == null)
                return outputImage;

            using (var image = new MagickImage(inputImage))
            {
                image.AdaptiveResize(_size, _height);
                image.Strip();
                image.Quality = quality;
                outputImage = image.ToBase64();
            }

            return outputImage;
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
            }
        }
    }
}
