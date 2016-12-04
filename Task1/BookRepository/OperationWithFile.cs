using System;
using System.Collections.Generic;
using System.IO;
using Task1.BookListModel;

namespace Task1.BookRepository
{
    public class OperationWithFile : IBookListStorage<Book>
    {
        #region Field

        /// <summary>
        /// Path to file.
        /// </summary>
        public string Path { get; }

        #endregion

        #region .ctor

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="path">Path to file.</param>

        public OperationWithFile(string path)
        {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentException();

            Path = path;
        }

        #endregion

        #region Main operation

        /// <summary>
        /// Write books in file.
        /// </summary>
        /// <param name="books">List book.</param>

        public void WriteBooks(IEnumerable<Book> books)
        {
            if (books == null)
                throw new ArgumentException();

            using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                foreach (var t in books)
                {
                    writer.Write(t.Author);
                    writer.Write(t.Title);
                    writer.Write(t.NumberOfPages);
                    writer.Write(t.Publisher);
                    writer.Write(t.YearIssued);
                }
            }
        }

        /// <summary>
        /// Read books from file.
        /// </summary>
        /// <returns>Books list.</returns>

        public IEnumerable<Book> ReadBooks()
        {
            var listBookForRead = new List<Book>();

            using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                while (reader.PeekChar() > -1)
                {
                    string author = reader.ReadString();
                    string title = reader.ReadString();
                    int numberOfPages = reader.ReadInt32();
                    string publisher = reader.ReadString();
                    int yearIssued = reader.ReadInt32();

                    listBookForRead.Add(new Book(author, title, publisher, numberOfPages, yearIssued));
                }
            }
            return listBookForRead;
        } 

        #endregion
        
    }
}
