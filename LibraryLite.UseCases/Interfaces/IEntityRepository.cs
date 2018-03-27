using System.Linq;
using System.Threading.Tasks;
using LibraryLite.Core.Interfaces;
using System.Data.Entity.Infrastructure;

namespace LibraryLite.UseCases.Interfaces
{
    public interface IEntityRepository<T>
        where T : class,IEntity, new()
    {
        Task CommitChangesAsync();
        void DeleteOnCommit(T entity);
        T GetEntity(int key);
        IQueryable<T> GetAll();
        int InsertOnCommit(T entity);
        DbEntityEntry Entry(object entity);
    }
}
