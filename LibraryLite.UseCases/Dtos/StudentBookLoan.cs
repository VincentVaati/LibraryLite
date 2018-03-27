using System;

namespace LibraryLite.UseCases.Dtos
{
    public class StudentBookLoan
    {
        public string Name { get; set; }
        public string BookTitle { get; set; }
        public string DateOfIssue { get; set; }
        public string DueDate { get; set; }
        public string DateReturned { get; set; }
    }
}
