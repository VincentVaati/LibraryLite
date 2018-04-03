using System.Collections.Generic;
using LibraryLite.Core.Entities;
using LibraryLite.UI.Web.MVC.Presentation.ViewModels;
using System.Globalization;
using System;

namespace LibraryLite.UI.Web.MVC.Presentation.Mapping
{
    public static  class ReportsMapperExtensionMethods
    {
        public static IList<PenaltyRevenueViewModel> ConvertToPenaltyRevenueViewModelList(IList<Student> students)
        {
            IList<PenaltyRevenueViewModel> penaltyRevenueViewModelList = new List<PenaltyRevenueViewModel>();
            
            foreach (var student in students)
            {
                if (student.BookLoans.Count >= 1)
                {
                    foreach (var loan in student.BookLoans)
                    {
                        //Get loans with penalties
                        if (loan.Penalty.HasValue)
                        {
                            var penaltyRevenueViewModel = new PenaltyRevenueViewModel
                            {
                                
                                FirstName = student.FirstName,
                                LastName = student.LastName,
                                DueDate = loan.DueDate,
                                DateReturned = loan.DateReturned.Value,
                                Penalty = loan.Penalty.Value
                            };

                            penaltyRevenueViewModelList.Add(penaltyRevenueViewModel);
                        }
                    }
                }
            }

            return penaltyRevenueViewModelList;
        }
        public static IList<BookLoansViewModel> ConvertToBookLoansViewModelList(IList<Student> students)
        {
            IList<BookLoansViewModel> bookLoansViewModelList = new List<BookLoansViewModel>();

            foreach (var student in students)
            {
                foreach (var loan in student.BookLoans)
                {
                    var bookLoansViewModel = new BookLoansViewModel
                    {
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        DateOfIssue =loan.DateOfIssue,
                        DueDate = loan.DueDate,
                    };
                    bookLoansViewModelList.Add(bookLoansViewModel);
                }
            }
            return bookLoansViewModelList;
        }
    }
}