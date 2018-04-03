using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryLite.UI.Web.MVC.Presentation.Mapping
{
    public class BookLoansViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BookTitle { get; set; }
        public DateTime  DateOfIssue { get; set; }
        public DateTime DueDate { get; set; }

        public BookLoansViewModel() { }
    }
}