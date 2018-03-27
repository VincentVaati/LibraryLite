using System.Collections.Generic;
using System.Linq;
using LibraryLite.Core.Entities;
using LibraryLite.UseCases.Interfaces;
using LibraryLite.UseCases.Dtos;

namespace LibraryLite.UseCases.Interactors
{
    public class RequestBookLoanInteractor : IRequestHandler<BookLoanRequestMessage, BookLoanResponseMessage>
    {
        private readonly IEntityRepository<Book> _bookEntityRepository;
        private readonly IEntityRepository<Student> _studentEntityRespository;
        private readonly IEntityRepository<BookInventory> _bookInventoryRepository;
        public readonly IEntityRepository<BookLoan> _bookLoanEntityRepository;

        public RequestBookLoanInteractor(IEntityRepository<Student> studentEntityRespository, IEntityRepository<Book> bookEntityRespository, IEntityRepository<BookInventory> bookInventoryRepository,IEntityRepository<BookLoan> bookLoanEntityRepository)
        {
            _bookEntityRepository = bookEntityRespository;
            _studentEntityRespository = studentEntityRespository;
            _bookInventoryRepository = bookInventoryRepository;
            _bookLoanEntityRepository=bookLoanEntityRepository;
        }

        public BookLoanResponseMessage Handle(BookLoanRequestMessage mesage)
        {
            var student = _studentEntityRespository.GetEntity(mesage.StudentId);
            var errors = new List<string>();
            var entity = new BookLoan()
            {
                StudentId=mesage.StudentId,
                BookId=mesage.BookId,
                DueDate =mesage.DueDate,
                DateOfIssue=mesage.DateOfIssue
            };
        
            _bookLoanEntityRepository.InsertOnCommit(entity);
            _bookLoanEntityRepository.CommitChangesAsync();
        
            //var bookInventoryEntity = _bookInventoryRepository.GetAll()
            //    .Where(u => u.BookId == bkId).First();
            //if (bookInventoryEntity.TotalBooks == bookInventoryEntity.NumberOfBooksOnLoan)
            //    errors.Add("Sorry! All the books are on load");

            //if (student.BookLoans.Where(u => u.BookId == bkId).Any())
            //    errors.Add("Sorry! You are already on loan for the same book ");

            //bookInventoryEntity.NumberOfBooksOnLoan += 1;

            return new BookLoanResponseMessage(!errors.Any(), errors);
        }
    }
}
