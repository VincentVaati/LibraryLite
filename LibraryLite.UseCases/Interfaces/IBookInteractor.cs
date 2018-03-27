using System.Collections.Generic;
using LibraryLite.Core.Entities;

namespace LibraryLite.UseCases.Interfaces
{
    public interface IBookInteractor
    {
        Book FindBookBy(int id);
        IList<Book> FindAllBooks();
        void Add(Book entity);
        void SaveChanges(Book entity);
        void Delete(Book entity);
    }
}
