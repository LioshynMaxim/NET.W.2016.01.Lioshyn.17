using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Task1.BookListModel;
using static System.String;

namespace Task1.BookRepository
{
    class BinarySerializator : IBookListStorage
    {
        #region Fields

        private string Path
        {
            get { return Path; }
            set
            {
                if (IsNullOrEmpty(value))
                {
                    throw new ArgumentException();
                }
            }
        }

        private BookListService bookListService;
        
        #endregion

        #region .ctor

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="path">Path to serializator file.</param>

        public BinarySerializator(string path)
        {
            Path = path;
        }

        #endregion

        #region Main function

        /// <summary>
        /// Write list of books in file.
        /// </summary>
        /// <param name="books">List of books.</param>

        public void WriteBooks(IEnumerable<Book> books)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Create(Path))
            {
                binaryFormatter.Serialize(fileStream,bookListService);
            }
        }

        /// <summary>
        /// Read from serialization file list of books.
        /// </summary>
        /// <returns>List of books.</returns>

        public IEnumerable<Book> ReadBooks()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.OpenRead(Path))
            {
                bookListService = (BookListService) binaryFormatter.Deserialize(fileStream);
            }

            return bookListService.ListBooks;
        }

        #endregion
    }
}
