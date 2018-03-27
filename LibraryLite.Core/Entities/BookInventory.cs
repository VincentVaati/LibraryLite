using LibraryLite.Core.Interfaces;

namespace LibraryLite.Core.Entities
{
    public class BookInventory : IEntity
    {

        public int Id { get; set; }
        public int BookId { get; set; }
        public int TotalBooks { get; set; }
        public int NumberOfBooksOnLoan { get; set; }
        public virtual Book Book { get; set; }
    }
}
