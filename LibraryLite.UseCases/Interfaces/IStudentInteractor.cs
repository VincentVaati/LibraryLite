using System.Collections.Generic;
using LibraryLite.Core.Entities;
using System.Linq;

namespace LibraryLite.UseCases.Interfaces
{
    public interface IStudentInteractor
    {
        Student FindStudentBy(int id);
        IQueryable<Student> GetStudentsLoans();
        IList<Student> FindAllStudents();
        void Add(Student entity);
        void SaveChanges(Student entity);
        void Delete(Student entity);
        IList<StudentClass> FindAllClasses();
    }
}
