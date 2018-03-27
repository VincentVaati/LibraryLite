using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using LibraryLite.Core.Interfaces;

namespace LibraryLite.Core.Entities
{
    public class Student : IEntity
    {

        public int Id { get; set; }
        [StringLength(15)]
        public string StudentNumber { get; set; }
        [StringLength(20)]
        public string FirstName { get; set; }
        [StringLength(20)]
        public string LastName { get; set; }
        public virtual int ClassId { get; set; }
        public virtual StudentClass Class { get; set; }
        public ICollection<BookLoan> BookLoans { get; set; }

        public Student()
        {
            this.BookLoans = new HashSet<BookLoan>();
        }

        public bool CanBorrowABook(Book book)
        {
            return BookLoans.Count < 2 ? true : false;
        }
    }
}
