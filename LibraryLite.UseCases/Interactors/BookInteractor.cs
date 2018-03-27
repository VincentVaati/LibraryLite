using System.Collections.Generic;
using System.Linq;
using LibraryLite.Core.Entities;
using LibraryLite.UseCases.Interfaces;
using System.Data.Entity;

namespace LibraryLite.UseCases.Interactors
{
    public class BookInteractor:IBookInteractor
    {
        private readonly IEntityRepository<Book> _bookEntityRepository;

        public BookInteractor() { }
        public BookInteractor(IEntityRepository<Book> bookEntityRepository)
        {
            _bookEntityRepository = bookEntityRepository;
        }
        public Book FindBookBy(int id)
        {
            return _bookEntityRepository.GetEntity(id);
        }
        public string GetBookName(int id)
        {
            var book = FindBookBy(id);
            if (book != null)
                return book.BookTitle;
            return "";
        }
        public IList<Book> FindAllBooks()
        {
            return _bookEntityRepository.GetAll().ToList();
        }
        public void Add(Book entity)
        {
            _bookEntityRepository.InsertOnCommit(entity);
            _bookEntityRepository.CommitChangesAsync();
        }
        public void Delete(Book entity)
        {
            _bookEntityRepository.DeleteOnCommit(entity);
            _bookEntityRepository.CommitChangesAsync();
        }
        public void SaveChanges(Book entity)
        {
            _bookEntityRepository.Entry(entity).State = EntityState.Modified;
            _bookEntityRepository.CommitChangesAsync();
        }
    }
}
