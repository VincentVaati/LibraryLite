using System;
using LibraryLite.Core.Interfaces;

namespace LibraryLite.Core.Entities
{
    public class BookLoan : IEntity
    {

        public int Id { get; set; }
        public int BookId { get; set; }
        public int StudentId { get; set; }
        public string BookNumber { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? DateReturned { get; set; }
        public virtual Book Book { get; set; }
        public virtual Student Student { get; set; }

        public BookLoan() { }

        public bool HasBeenReturned()
        {
            return DateReturned != null;
        }
        public void MarkAsReturned()
        {
            DateReturned = DateTime.Now.Date;
        }
        //is a late return if current date is > return date
        public bool IsAlateReturn()
        {
            return DateTime.Now.Date > DueDate.Date;
        }

    }
}
