using System.Collections.Generic;
using LibraryLite.Core.Entities;

namespace LibraryLite.UseCases.Interfaces
{
    public interface IClassInteractor
    {
        StudentClass FindClassBy(int id);
        IList<StudentClass> FindAllClasses();
        void Add(StudentClass entity);
        void SaveChanges(StudentClass entity);
        void Delete(StudentClass entity);
    }
}
