using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using LibraryLite.Core.Interfaces;

namespace LibraryLite.Core.Entities
{
    public class Book: IEntity
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string BookNumber { get; set; }
        [StringLength(30)]
        public string ISBN { get; set; }
        [StringLength(150)]
        public string BookTitle { get; set; }

        public virtual ICollection<BookLoan> Loans { get; set; }

        public Book()
        {
            Loans = new HashSet<BookLoan>();
        }
    }
}
