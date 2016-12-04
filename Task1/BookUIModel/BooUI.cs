using System;
using System.Collections.Generic;
using System.IO;
using NLog;
using NLog.Internal;
using Task1.BookListModel;
using Task1.BookRepository;
using System.Configuration;

namespace Task1.BookUIModel
{
    public class BooUI
    {
        static void Main(string[] args)
        {
            OperationWithFile operationWithFile = new OperationWithFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, new ConfigurationManager().AppSettings["address"]));
            BookListService bookListService = new BookListService(operationWithFile);

            Book book1 = new Book("Есенин", "Первая книга", "Москва", 1955, 150);
            Book book2 = new Book("Пушкин", "Вторая книга", "Российская Империя", 1955, 150);
            

            BookUiHelpFunction bookUiHelpFunction = new BookUiHelpFunction(operationWithFile, bookListService);

            bookUiHelpFunction.AddBook(book1);
            bookUiHelpFunction.AddBook(book1);
            bookUiHelpFunction.RemoveBook(book2);
            bookUiHelpFunction.AddBook(book2);


            List<Book> resultFind = (List<Book>) bookListService.FindByTag(book => book.NumberOfPages == 150);
            BookUiHelpFunction.ServiceBookLogger.Info("Find list book where number of page = 189");
            BookUiHelpFunction.DisplayBook(resultFind);

            Console.ReadKey();
        }
    }
}
