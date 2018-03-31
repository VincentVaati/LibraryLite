using System;

namespace LibraryLite.UseCases.Dtos
{
    public class StudentBookLoan
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BookTitle { get; set; }
        public string DateOfIssue { get; set; }
        public string DueDate { get; set; }
        public string DateReturned { get; set; }
        public decimal? Penalty { get; set; }
    }
}
