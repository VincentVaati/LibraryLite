using System.Collections.Generic;
using System.Linq;
using LibraryLite.Core.Entities;
using LibraryLite.UseCases.Interfaces;
using System.Data.Entity;

namespace LibraryLite.UseCases.Interactors
{
    public class ClassInteractor:IClassInteractor
    {
        private readonly IEntityRepository<StudentClass> _classEntityRepository;

        public ClassInteractor() { }
        public ClassInteractor(IEntityRepository<StudentClass> classEntityRepository)
        {
            _classEntityRepository = classEntityRepository;
        }
        public StudentClass FindClassBy(int id)
        {
            return _classEntityRepository.GetEntity(id);
        }
        public IList<StudentClass> FindAllClasses()
        {
            return _classEntityRepository.GetAll().ToList();
        }
        public void Add(StudentClass entity)
        {
            _classEntityRepository.InsertOnCommit(entity);
            _classEntityRepository.CommitChangesAsync();
        }
        public void Delete(StudentClass entity)
        {
            _classEntityRepository.DeleteOnCommit(entity);
            _classEntityRepository.CommitChangesAsync();
        }
        public void SaveChanges(StudentClass studentClass)
        {
            _classEntityRepository.Entry(studentClass).State = EntityState.Modified;
            _classEntityRepository.CommitChangesAsync();
        }
    }
}
