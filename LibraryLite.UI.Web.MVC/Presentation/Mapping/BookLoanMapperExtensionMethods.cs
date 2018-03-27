using LibraryLite.Core.Entities;
using LibraryLite.UI.Web.MVC.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using LibraryLite.UseCases.Interfaces;
using LibraryLite.UseCases.Dtos;

namespace LibraryLite.UI.Web.MVC.Presentation.Mapping
{
    public static class BookLoanMapperExtensionMethods
    {
        public static BookLoanRequestMessage ConvertToBookLoan(BookLoanRequestViewModel bookLoanViewModel)
        {
            var bookLoanRequestMessage = new BookLoanRequestMessage
             {
                 StudentId = bookLoanViewModel.StudentId,
                 BookId =bookLoanViewModel.BookId,
                 DateOfIssue = bookLoanViewModel.DateOfIssue,
                 DueDate = bookLoanViewModel.DueDate,
             };
            return bookLoanRequestMessage;
        }
        public static BookLoanRequestViewModel ConvertToBookLoanRequestViewModel(IList<Student> students, IList<Book> books, int studentIdFilter)
        {
            var bookLoanRequestViewModel = new BookLoanRequestViewModel
            {
                Students = students,
                Books = books,
                DateOfIssue = DateTime.Now.Date,
                Id = 0,
                StudentIdFilter =studentIdFilter
            };
            return bookLoanRequestViewModel;
        }
        public static BookLoanRequestViewModel ConvertToBookLoanRequestViewModel(Student student, IList<Book> books, int studentIdFilter)
        {
            var bookLoanRequestViewModel = new BookLoanRequestViewModel
            {
                Student = student,
                Books = books,
                DateOfIssue = DateTime.Now.Date,
                Id = 0,
                StudentIdFilter = studentIdFilter
            };
            return bookLoanRequestViewModel;
        }
    }
}