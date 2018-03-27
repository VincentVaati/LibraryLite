using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryLite.Core.Interfaces;
using LibraryLite.UseCases.Interfaces;
using System.Data.Entity.Infrastructure;

namespace LibraryLite.UI.Web.MVC.Gateways
{
    public class EntityRepository<T> : IEntityRepository<T>
         where T : class, IEntity, new()
    {
        private readonly IEntitiesContext _entities;

        public EntityRepository() { }
        public EntityRepository(IEntitiesContext entities)
        {
            _entities = entities;
        }

        public async Task CommitChangesAsync()
        {
            await _entities.SaveChangesAsync();
        }

        public void DeleteOnCommit(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }

        public T GetEntity(int key)
        {
            return _entities.Set<T>().Find(key);
        }

        public IQueryable<T> GetAll()
        {
            return _entities.Set<T>();
        }

        public int InsertOnCommit(T entity)
        {
            _entities.Set<T>().Add(entity);
            return entity.Id;
        }
        public DbEntityEntry Entry(object entity)
        {
            return _entities.Entry(entity);
        }
    }
}