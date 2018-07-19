using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.BLL.Interfaces
{
    public interface IImageCompressorService
    {
        byte[] CompresingImage(byte[] inputImage);
        List<byte[]> CompresingImages(List<byte[]> inputImages);
    }
}
