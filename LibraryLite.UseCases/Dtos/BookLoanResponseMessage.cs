using System.Collections.Generic;
using LibraryLite.UseCases.Interfaces;

namespace LibraryLite.UseCases.Dtos
{
    public class BookLoanResponseMessage : ResponseMessage
    {
        public IList<string> Errors { get; private set; }

        public BookLoanResponseMessage(bool success, IList<string> errors, string message = null)
            : base(success, message)
        {
            Errors = errors;
        }
    }
}
