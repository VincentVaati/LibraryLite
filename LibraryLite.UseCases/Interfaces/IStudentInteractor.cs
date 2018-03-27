using System.Collections.Generic;
using LibraryLite.Core.Entities;

namespace LibraryLite.UseCases.Interfaces
{
    public interface IStudentInteractor
    {
        Student FindStudentBy(int id);
        IList<Student> FindAllStudents();
        void Add(Student entity);
        void SaveChanges(Student entity);
        void Delete(Student entity);
        IList<StudentClass> FindAllClasses();
    }
}
