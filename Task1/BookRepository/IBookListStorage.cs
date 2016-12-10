using System.Collections.Generic;
using Task1.BookListModel;

namespace Task1.BookRepository
{
    public interface IBookListStorage
    {
        void WriteBooks(IEnumerable<Book> books);
        IEnumerable<Book> ReadBooks();
    }
}