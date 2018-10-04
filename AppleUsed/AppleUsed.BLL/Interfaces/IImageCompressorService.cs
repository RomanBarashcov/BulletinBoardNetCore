using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.BLL.Interfaces
{
    public interface IImageCompressorService : IDisposable
    {
        string CompressingImageForSmallSize(byte[] inputImages);
        string CompressingImageForAvgSize(byte[] inputImages);
        string CompressingImageForBigSize(byte[] inputImages);
        List<string> CompressingImagesForSmallSize(List<byte[]> inputImages);
        List<string> CompressingImagesForAvgSize(List<byte[]> inputImages);
        List<string> CompressingImagesForBigSize(List<byte[]> inputImages);
    }
}
