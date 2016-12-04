using System;
using System.Collections.Generic;
using System.IO;
using NLog;
using NLog.Internal;
using Task1.BookListModel;
using Task1.BookRepository;

namespace Task1.BookUIModel
{
    public class BookUiHelpFunction
    {
        #region Field

        public static OperationWithFile WithFile { get; set; }
        public BookListService ListService { get; }
        public static Logger ServiceBookLogger { get; } = LogManager.GetCurrentClassLogger();
        private string Address { get; }
       
        #endregion

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="operationWithFile">Combine path with directory and file.</param>
        /// <param name="bookListService">List operation of books.</param>

        public BookUiHelpFunction(OperationWithFile operationWithFile, BookListService bookListService)
        {
            Address = new ConfigurationManager().AppSettings["address"];

            if (operationWithFile == null)
                operationWithFile = new OperationWithFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Address));


            if (bookListService == null)
                bookListService = new BookListService(operationWithFile);

            ListService = bookListService;
        }

        #region Main function

        /// <summary>
        /// Display in console books in file.
        /// </summary>
        /// <param name="bookList">List of books.</param>

        public static void DisplayBook(IList<Book> bookList)
        {
            ServiceBookLogger.Info("Start display the library on the console");
            if (bookList != null)
                foreach (Book book in bookList)
                    Console.WriteLine(book.ToString());

            Console.WriteLine(new string('-', 10));
            ServiceBookLogger.Info("Finish display the library on the console");
        }

        /// <summary>
        /// Add book in file.
        /// </summary>
        /// <param name="addBook">Book</param>

        public void AddBook(Book addBook)
        {
            try
            {
                ServiceBookLogger.Info("Start of block processing TYR in which adding books");
                ListService.AddBook(addBook);
                ServiceBookLogger.Info("End of block processing TYR in which adding books");
            }
            catch (AddBookException ex)
            {
                Console.WriteLine(ex.Message);
                ServiceBookLogger.Info(ex.Message);
                ServiceBookLogger.Error(ex.StackTrace);
            }
        }

        /// <summary>
        /// Remove book from file.
        /// </summary>
        /// <param name="removeBook">Book.</param>

        public void RemoveBook(Book removeBook)
        {
            try
            {
                ServiceBookLogger.Info("Start of block processing TYR in which remove book");
                ListService.RemoveBook(removeBook);
                ServiceBookLogger.Info("End of block processing TYR in which remove book");
            }
            catch (RemoveBookException ex)
            {
                Console.WriteLine(ex.Message);
                ServiceBookLogger.Info(ex.Message);
                ServiceBookLogger.Error(ex.StackTrace);
            }
        } 

        #endregion

        
    }
}
