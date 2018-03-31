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
        private readonly IEntityRepository<ApplicationSettings> _settings;

        public RequestBookReturnInteractor(IEntityRepository<BookLoan> bookLoanRepository,IEntityRepository<ApplicationSettings> settings)
        {
            _bookLoanRepository = bookLoanRepository;
            _settings = settings;
        }
        public void Handle(BookReturnRequestMesage message)
        {
            var responseMessage = new BookReturnResponseMessage();
            var bookLoan = _bookLoanRepository.GetAll().Where(bl => bl.Id == message.Id).First();
            
            if (IsLateLoan(message.Id))
            {
                responseMessage.IsLateReturn = true;
                responseMessage.Penalty = CalculatePenalty(message.Id);
                bookLoan.Penalty = responseMessage.Penalty;
            }
            else
            {
                responseMessage.Penalty = 0;
                responseMessage.IsLateReturn = false;
            }
            bookLoan.DateReturned = DateTime.Now.Date;

            _bookLoanRepository.Entry(bookLoan).State = EntityState.Modified;
            _bookLoanRepository.CommitChangesAsync();
        }
        public bool IsLateLoan(int id)
        {
            var bookLoan = _bookLoanRepository.GetAll().Where(bl => bl.Id == id).First();
            return bookLoan.IsAlateReturn();
        }
        public DateTime GetDueDate(int id)
        {
            var dueDate = _bookLoanRepository.GetAll().Where(bl => bl.Id == id).First().DueDate;
            return dueDate;
        }
        public decimal CalculatePenalty(int id)
        {
            var bookLoan = _bookLoanRepository.GetAll().Where(bl => bl.Id == id).First();
            decimal amountPayable = 0;

            var penalty = _settings.GetAll().FirstOrDefault().Penalty;
            var extraDays = DateTime.Now.Date.Subtract(bookLoan.DueDate).Days;

            amountPayable = penalty * extraDays;

            return amountPayable;
        }
    }
}
