using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog.Internal;
using Task1.BookRepository;

namespace Task1.BookListModel
{
    public class BookListService
    {
        #region Field

        private List<Book> BookList { get; }
        public List<Book> ListBooks => BookList;

        #endregion

        #region .ctor

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="bookListStorage">List of books.</param>

        public BookListService(IBookListStorage<Book> bookListStorage)
        {
            if (bookListStorage == null)
                throw new ArgumentNullException();

            try
            {
                BookList = (List<Book>)bookListStorage.ReadBooks();
            }
            catch (Exception ex)
            {
                throw new BookListServiceException("Unable to download from file: ", ex);
            }
        }
        #endregion

        #region Main function

        /// <summary>
        /// Add one book in library.
        /// </summary>
        /// <param name="book">Book information.</param>

        public void AddBook(Book book)
        {
            if (book == null)
                return;
            if (BookList.Contains(book))
                throw new AddBookException("Alarm!!! This book exists in the list!");

            BookList.Add(book);
        }

        /// <summary>
        /// Add some book in library.
        /// </summary>
        /// <param name="books">List of book information.</param>

        public void AddBook(IEnumerable<Book> books)
        {
            if (books == null)
                throw new ArgumentException("Books collection is null");

            foreach (Book book in books)
            {
                AddBook(book);
            }
        }

        /// <summary>
        /// Remove one book in library.
        /// </summary>
        /// <param name="book">Book.</param>

        public void RemoveBook(Book book)
        {
            if (book == null)
                return;
            if (!BookList.Contains(book))
                throw new RemoveBookException("Book to be deleted was not found");

            BookList.Remove(book);
        }

        /// <summary>
        /// Remove some book in library.
        /// </summary>
        /// <param name="books">List of book.</param>

        public void RemoveBook(IEnumerable<Book> books)
        {
            if (books == null)
                return;

            foreach (Book book in books)
            {
                BookList.Remove(book);
            }
        }

        /// <summary>
        /// Find book by tag name.
        /// </summary>
        /// <param name="tag">Key word.</param>
        /// <returns>List of book.</returns>

        public IEnumerable<Book> FindByTag(Predicate<Book> tag) => BookList.FindAll(tag).ToList();

        /// <summary>
        /// Sort book library by tag name.
        /// </summary>

        public void SortsBooks() => BookList.Sort();

        /// <summary>
        /// Sort book library by tag name.
        /// </summary>
        /// <param name="keySelector">Key selector.</param>

        public void SortsBooksByTag(Comparison<Book> keySelector) => BookList.Sort(keySelector);

        /// <summary>
        /// Save from library to file.
        /// </summary>
        /// <param name="address">String address file.</param>

        public void SaveFromBookListToFile(string address)
        {
            string addr = address;
            if (String.IsNullOrEmpty(address))
            {
                addr = new ConfigurationManager().AppSettings["address"];
            }

            OperationWithFile operationWithFile =
                        new OperationWithFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, addr));

            operationWithFile.WriteBooks(BookList);
        }

        /// <summary>
        /// Load from library.
        /// </summary>
        /// <param name="address">String address file.</param>
        /// <returns>Book list.</returns>
        public IEnumerable<Book> LoadFromFileToBookList(string address)
        {
            string addr = address;
            if (String.IsNullOrEmpty(address))
            {
                addr = new ConfigurationManager().AppSettings["address"];
            }

            var operationWithFile = new OperationWithFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, addr));
            return operationWithFile.ReadBooks();

        }

        #endregion

    }
}
