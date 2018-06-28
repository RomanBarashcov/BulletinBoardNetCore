using System.Collections.Generic;
using AppleUsed.Data.Entities;

namespace AppleUsed.Service.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooks();
        Book GetBook(int id);
        void InsertBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int id);
    }
}