using LibraryLite.Core.Entities;
using LibraryLite.UI.Web.MVC.Presentation.ViewModels;
using System.Collections.Generic;

namespace LibraryLite.UI.Web.MVC.Presentation.Mapping
{
    public static class BookMapperExtensionMethods
    {
        public static Book ConvertToBook(BookViewModel bookViewModel)
        {
            var book = new Book
            {
                Id = bookViewModel.Id,
                BookNumber = bookViewModel.BookNumber,
                BookTitle = bookViewModel.BookTitle,
                ISBN = bookViewModel.ISBN
            };
            return book;
        }
        public static BookViewModel ConvertToBookViewModel(Book book)
        {
            var bookViewModel = new BookViewModel
            {
                Id = book.Id,
                BookNumber = book.BookNumber,
                BookTitle = book.BookTitle,
                ISBN = book.ISBN
            };
            return bookViewModel;
        }
        public static IList<BookViewModel> ConvertToBookList(IList<Book> books)
        {
            IList<BookViewModel> bookViewModelList = new List<BookViewModel>();
            foreach (var book in books)
            {
                bookViewModelList.Add(ConvertToBookViewModel(book));
            }
            return bookViewModelList;
        }
    }
}