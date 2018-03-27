using System.Collections.Generic;
using LibraryLite.UseCases.Interfaces;
using System;

namespace LibraryLite.UseCases.Dtos
{
    public class BookLoanRequestMessage : IRequest<BookLoanResponseMessage>
    {
        public int StudentId { get;  set; }
        public int BookId { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime DueDate { get; set; }

        public BookLoanRequestMessage() { }
        public BookLoanRequestMessage(int studentId, int bookId, DateTime dateOfIssue, DateTime dueDate)
        {
            StudentId = studentId;
            BookId = bookId;
            DateOfIssue = dateOfIssue;
            DueDate = dueDate;
        }
    }
}
