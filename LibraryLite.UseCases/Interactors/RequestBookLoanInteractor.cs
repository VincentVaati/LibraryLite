using System;
using System.Collections.Generic;
using System.Linq;
using LibraryLite.Core.Entities;
using LibraryLite.UseCases.Interfaces;
using LibraryLite.UseCases.Dtos;
using LibraryLite.UseCases.Infrastructure;

namespace LibraryLite.UseCases.Interactors
{
    public class RequestBookLoanInteractor : IRequestHandler<BookLoanRequestMessage, BookLoanResponseMessage>
    {
        private readonly IEntityRepository<Book> _bookEntityRepository;
        private readonly IEntityRepository<Student> _studentEntityRespository;
        private readonly IEntityRepository<BookInventory> _bookInventoryRepository;
        public readonly IEntityRepository<BookLoan> _bookLoanEntityRepository;
        public readonly IEntityRepository<ApplicationSettings> _settings;

        public RequestBookLoanInteractor(IEntityRepository<Student> studentEntityRespository, IEntityRepository<Book> bookEntityRespository,
            IEntityRepository<BookInventory> bookInventoryRepository, IEntityRepository<BookLoan> bookLoanEntityRepository, IEntityRepository<ApplicationSettings> settings)
        {
            _bookEntityRepository = bookEntityRespository;
            _studentEntityRespository = studentEntityRespository;
            _bookInventoryRepository = bookInventoryRepository;
            _bookLoanEntityRepository=bookLoanEntityRepository;
            _settings = settings;
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

            return new BookLoanResponseMessage(!errors.Any(), errors);
        }
        public DateTime CalculateDueDate()
        {
            DateTime dueDate = DateTime.Now.Date;
            var loandays = _settings.GetAll().FirstOrDefault().LoanDays;
            dueDate = DateTime.Now.Date.AddDays(loandays);

            //due date shud not be on a sunday
            if (dueDate.DayOfWeek == DayOfWeek.Sunday)
                dueDate.AddDays(1);

            return dueDate;
        }
        public IList<BookLoan> GetBookLoanPenaltyBy(ReportFilter filter)
        {
            
            var bookLoansByMonth = _bookLoanEntityRepository.GetAll().Where(l => l.DateReturned.Value.Month == filter.MonthId && l.DateReturned.Value.Year==filter.Year).ToList();
            
            return bookLoansByMonth;
        }
    }
}
