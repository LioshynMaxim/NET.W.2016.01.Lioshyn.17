using System;

namespace Task1.BookListModel
{
    public class BookListServiceException : Exception
    {
        public BookListServiceException(string messae) : base(messae)
        {
        }

        public BookListServiceException(string message, Exception ex)
        {
        }
    }

    public class AddBookException : BookListServiceException
    {
        public AddBookException(string messae) : base(messae)
        {
        }

        public AddBookException(string message, Exception ex) : base(message, ex)
        {
        }
    }

    public class RemoveBookException : BookListServiceException
    {
        public RemoveBookException(string messae) : base(messae)
        {
        }

        public RemoveBookException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}
