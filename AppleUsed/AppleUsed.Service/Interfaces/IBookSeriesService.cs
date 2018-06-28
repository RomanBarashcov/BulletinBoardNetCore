using AppleUsed.Data.Entities;

namespace AppleUsed.Service.Interfaces
{
    public interface IBookSeriesService
    {
        BookSeries GetBookSeries(int id);
    }
}