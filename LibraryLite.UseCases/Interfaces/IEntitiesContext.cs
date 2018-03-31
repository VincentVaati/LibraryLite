using System.Data.Entity;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using LibraryLite.Core.Entities;

namespace LibraryLite.UseCases.Interfaces
{
    public interface IEntitiesContext
    {
        IDbSet<ApplicationSettings> ApplicationSettings { get; set; }
        IDbSet<Book> Books { get; set; }
        IDbSet<BookInventory> BookInventories { get; set; }
        IDbSet<Student> Students { get; set; }
        IDbSet<StudentClass> StudentClasses { get; set; }
        IDbSet<BookLoan> BookLoan { get; set; }

        DbEntityEntry Entry(object entity);
        Task<int> SaveChangesAsync();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Set", Justification = "This is to match the EF terminology.")]
        IDbSet<T> Set<T>() where T : class;
        void DeleteOnCommit<T>(T entity) where T : class;
        Database GetDatabase();
    }
}
