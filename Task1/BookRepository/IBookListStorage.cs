using System.Collections.Generic;

namespace Task1.BookRepository
{
    public interface IBookListStorage<T>
    {
        void WriteBooks(IEnumerable<T> books);
        IEnumerable<T> ReadBooks();
    }
}