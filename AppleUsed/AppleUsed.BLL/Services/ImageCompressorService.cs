using AppleUsed.BLL.Interfaces;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.BLL.Services
{
    public class ImageCompressorService : IImageCompressorService
    {
        public ImageCompressorService() { }

        public byte[] CompresingImage(byte[] inputImage)
        {
            const int size = 640;
            const int height = 400;
            const int quality = 50;
            byte[] outputImage;

            if (inputImage == null)
                return outputImage = new byte[0];

            using (var image = new MagickImage(inputImage))
            {
                image.Resize(size, height);
                image.Strip();
                image.Quality = quality;
                outputImage = image.ToByteArray();
            }

            return outputImage;
        }

        public List<byte[]> CompresingImages(List<byte[]> inputImages)
        {
            const int size = 650;
            const int height = 400;
            const int quality = 50;
            List<byte[]> outputImages = new List<byte[]>();

            if (inputImages == null)
                return outputImages;

            for(int i = 0; i <= inputImages.Count - 1; i++)
            {
                using (var image = new MagickImage(inputImages[i]))
                {
                    image.Resize(size, height);
                    image.Strip();
                    image.Quality = quality;
                    outputImages.Add(image.ToByteArray());
                }
            }   

            return outputImages;
        }
    }
}
