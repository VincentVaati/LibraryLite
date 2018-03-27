using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using LibraryLite.UseCases.Interfaces;
using LibraryLite.Core.Entities;
using System.Data.Entity.Infrastructure;

namespace LibraryLite.UI.Web.MVC.Gateways
{
    public class EntitiesContext
        : DbContext, IEntitiesContext
    {
        public EntitiesContext()
            : base("LibraryLiteDb.SqlServer") // Use the connection string in a web.config (if one is found)
        {
        }

        public IDbSet<ApplicationSettings> ApplicationSettings { get; set; }
        public IDbSet<Book> Books { get; set; }
        public IDbSet<BookInventory> BookInventories { get; set; }
        public IDbSet<Student> Students { get; set; }
        public IDbSet<StudentClass> StudentClasses { get; set; }

        
        IDbSet<T> IEntitiesContext.Set<T>()
        {
            return base.Set<T>();
        }
        public new  DbEntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }
        public override async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public void DeleteOnCommit<T>(T entity) where T : class
        {
            Set<T>().Remove(entity);
        }

        public Database GetDatabase()
        {
            return Database;
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Book>()
                .HasKey(b => b.Id);
                
            modelBuilder.Entity<Student>()
                .HasKey(s => s.Id)
                .HasRequired<StudentClass>(cls => cls.Class)
                .WithMany(s => s.Students)
                .HasForeignKey(s => s.ClassId);

            modelBuilder.Entity<StudentClass>()
                .HasKey(cls => cls.Id);

            modelBuilder.Entity<BookLoan>()
                .HasKey(bl => bl.Id)
                .HasRequired<Student>(s => s.Student);

            modelBuilder.Entity<BookLoan>()
                .HasRequired<Book>(b => b.Book);

            modelBuilder.Entity<BookInventory>()
                .HasRequired(b => b.Book);

        }

        public System.Data.Entity.DbSet<LibraryLite.Core.Entities.BookLoan> BookLoans { get; set; }
    }
}