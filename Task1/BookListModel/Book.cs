using System;

namespace Task1.BookListModel
{
    public class Book : IEquatable<Book>, IComparable<Book>, IComparable
    {
        #region Fields

        public string Author { get; }
        public string Title { get; }
        public string Publisher { get; }
        public int YearIssued { get; }
        public int NumberOfPages { get; }

        #endregion

        #region .ctor

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="author">Author name</param>
        /// <param name="title">Book title</param>
        /// <param name="publisher">Publisher of book</param>
        /// <param name="yearIssued">Year issued book</param>
        /// <param name="numberOfPages">Number of pages in book</param>

        public Book(string author, string title, string publisher, int yearIssued, int numberOfPages)
        {
            if (String.IsNullOrEmpty(author) || String.IsNullOrEmpty(title) || String.IsNullOrEmpty(publisher) || yearIssued <= 0 || numberOfPages <= 0)
                throw new ArgumentException("Null or empty or illegal int");

            Author = author;
            Title = title;
            Publisher = publisher;
            YearIssued = yearIssued;
            NumberOfPages = numberOfPages;
        }

        #endregion

        #region Oberride function

        /// <summary>
        /// Equals two books
        /// </summary>
        /// <param name="other">Object of Book class</param>
        /// <returns>Equals or NotEquals</returns>

        public bool Equals(Book other) => Title.Equals(other?.Title) && Author.Equals(other?.Author) && Publisher.Equals(other?.Publisher) &&
                                          YearIssued == other?.YearIssued && NumberOfPages == other.NumberOfPages;

        /// <summary>
        /// Compare two books
        /// </summary>
        /// <param name="other">Object of Book class</param>
        /// <returns>Sequence</returns>

        public int CompareTo(Book other) => String.Compare(Author, other.Author, StringComparison.Ordinal);

        /// <summary>
        /// Compare two books
        /// </summary>
        /// <param name="obj">Object of Book class</param>
        /// <returns>Sequence</returns>

        public int CompareTo(object obj) => CompareTo((Book) obj);

        /// <summary>
        /// Override ToString
        /// </summary>
        /// <returns>String of book information</returns>

        public override string ToString() => $"Author: {Author}, Title: \"{Title}\", Publisher: {Publisher}, {NumberOfPages}, {YearIssued}";

        #endregion


    }
}
