using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Task1.BookListModel;
using static System.String;

namespace Task1.BookRepository
{
    class LINQToXML : IBookListStorage
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

        public LINQToXML(string path)
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
            XElement xElement = new XElement("Books");
            foreach (var writeBook in books)
            {
                xElement.Add(new XElement("book",
                    new XElement("Author", writeBook?.Author),
                    new XElement("Title", writeBook?.Title),
                    new XElement("Publisher", writeBook?.Publisher),
                    new XElement("YearIssued", writeBook?.YearIssued),
                    new XElement("NumberOfPages", writeBook?.NumberOfPages)
                    ));
            }

            xElement.Save(Path);
        }

        /// <summary>
        /// Read from XML file to list of books. 
        /// </summary>
        /// <returns>List of books.</returns>

        public IEnumerable<Book> ReadBooks()
        {
            string author = "", title = "", publisher = "";
            int yearIssued = 0;
            List<Book> books = new List<Book>();

            XElement xmlBooks = XElement.Load(Path);
            var book = from b in xmlBooks.Elements()
                       where b.Name == "book"
                       select b;

            foreach (var item in book.Elements())
            {
                switch (item.Name.ToString())
                {
                    case "Author":
                        author = item.Value;
                        break;
                    case "Title":
                        title = item.Value;
                        break;
                    case "Publisher":
                        publisher = item.Value;
                        break;
                    case "YearIssued":
                        yearIssued = int.Parse(item.Value);
                        break;
                    case "NumberOfPages":
                        var numberOfPages = int.Parse(item.Value);
                        books.Add(new Book(author, title, publisher, yearIssued, numberOfPages));
                        break;
                }
            }

            return books;
        }

        #endregion


    }
}
