using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Task1.BookListModel;
using static System.String;

namespace Task1.BookRepository
{
    class XmlWriter<T> : IBookListStorage<T>
    {
        #region Fields

        private string Path
        {
            get { return Path; }
            set
            {
                if (IsNullOrWhiteSpace(value))
                    throw new ArgumentException();
            }
        }

        #endregion

        #region .ctor

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="path">Path to XML file.</param>

        public XmlWriter(string path)
        {
            Path = path;
        }

        #endregion

        #region Main function

        /// <summary>
        /// Write list of books in XML file.
        /// </summary>
        /// <param name="books">List of books.</param>

        public void WriteBooks(IEnumerable<T> books)
        {
            BookListService bookListService = (BookListService)books;
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings {Indent = true};
            using (XmlWriter xmlWriter = XmlWriter.Create(Path, xmlWriterSettings))
            {
                xmlWriter.WriteStartElement("books");
                foreach (var book in bookListService.ListBooks)
                {
                    xmlWriter.WriteStartElement("book");
                    xmlWriter.WriteElementString("Author", book.Author);
                    xmlWriter.WriteElementString("Title", book.Title);
                    xmlWriter.WriteElementString("Publisher", book.Publisher);
                    xmlWriter.WriteElementString("YearIssued", book.YearIssued.ToString());
                    xmlWriter.WriteElementString("NumberOfPages", book.NumberOfPages.ToString());
                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
            }
        }

        /// <summary>
        /// Read from XML file to list of books. 
        /// </summary>
        /// <returns>List of books.</returns>

        public IEnumerable<T> ReadBooks()
        {
            var settings = new XmlReaderSettings
            {
                IgnoreComments = true,
                IgnoreProcessingInstructions = true,
                IgnoreWhitespace = true,
            };

            var books = new List<Book>();

            using (var reader = XmlReader.Create(Path, settings))
            {
                while (reader.Read())
                {
                    if (reader.Name == "Author")
                    {
                        var author = reader.ReadElementContentAsString("Author", "");
                        var title = reader.ReadElementContentAsString("Title", "");
                        var publisher = reader.ReadElementContentAsString("Publisher", "");
                        var yearIssued = reader.ReadElementContentAsInt("YearIssued", "");
                        var numberOfPages = reader.ReadElementContentAsInt("NumberOfPages", "");
                        
                        books.Add(new Book(author, title, publisher, yearIssued, numberOfPages));
                    }
                }
            }

            return (IEnumerable<T>) books;
        } 

        #endregion

        
    }
}
