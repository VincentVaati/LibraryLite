using System.Linq;
using LibraryLite.Core.Entities;
using LibraryLite.UseCases.Interfaces;
using LibraryLite.UseCases.Dtos;
using System;
using System.Data.Entity;

namespace LibraryLite.UseCases.Interactors
{
    public class RequestBookReturnInteractor : IRequesthandler<BookReturnRequestMesage>
    {
        private readonly IEntityRepository<BookLoan> _bookLoanRepository;

        public RequestBookReturnInteractor(IEntityRepository<BookLoan> bookLoanRepository)
        {
            _bookLoanRepository = bookLoanRepository;
        }
        public void Handle(BookReturnRequestMesage message)
        {
            var bookLoan = _bookLoanRepository.GetAll().Where(bl => bl.Id == message.Id).First();
            
            bookLoan.DateReturned = DateTime.Now.Date;
            
            _bookLoanRepository.Entry(bookLoan).State = EntityState.Modified;
            _bookLoanRepository.CommitChangesAsync();
        }
    }
}
