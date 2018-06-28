using AppleUsed.Data.Entities;
using AppleUsed.Repo.Data;
using AppleUsed.Service.Interfaces;

namespace AppleUsed.Service
{
    public class BookSeriesService : IBookSeriesService
    {
        private readonly IRepository<BookSeries> _bookSeriesRepository;

        public BookSeriesService(IRepository<BookSeries> bookSeriesRepository)
        {
            _bookSeriesRepository = bookSeriesRepository;
        }

        public BookSeries GetBookSeries(int id)
        {
            return _bookSeriesRepository.Get(id);
        }
    }
}