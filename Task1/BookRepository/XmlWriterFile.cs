using System;
using System.Collections.Generic;
using System.Xml;
using Task1.BookListModel;
using static System.String;

namespace Task1.BookRepository
{
    class XmlWriterFile : IBookListStorage
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

        public XmlWriterFile(string path)
        {
            Path = path;
        }

        #endregion

        #region Main function

        /// <summary>
        /// Write list of books in XML file.
        /// </summary>
        /// <param name="books">List of books.</param>

        public void WriteBooks(IEnumerable<Book> books)
        {
            
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings {Indent = true};
            
            using (var xmlWriterFile = XmlWriter.Create(Path, xmlWriterSettings))
            {
                xmlWriterFile.WriteStartElement("books");
                foreach (var book in books)
                {
                    xmlWriterFile.WriteStartElement("book");
                    xmlWriterFile.WriteElementString("Author", book.Author);
                    xmlWriterFile.WriteElementString("Title", book.Title);
                    xmlWriterFile.WriteElementString("Publisher", book.Publisher);
                    xmlWriterFile.WriteElementString("YearIssued", book.YearIssued.ToString());
                    xmlWriterFile.WriteElementString("NumberOfPages", book.NumberOfPages.ToString());
                    xmlWriterFile.WriteEndElement();
                }

                xmlWriterFile.WriteEndElement();
            }
        }

        /// <summary>
        /// Read from XML file to list of books. 
        /// </summary>
        /// <returns>List of books.</returns>

        public IEnumerable<Book> ReadBooks()
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

            return books;
        } 

        #endregion

        
    }
}
