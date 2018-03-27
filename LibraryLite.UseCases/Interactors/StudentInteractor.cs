using LibraryLite.Core.Entities;
using LibraryLite.UseCases.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryLite.UseCases.Interactors
{
    public class StudentInteractor:IStudentInteractor
    {
        private readonly IEntityRepository<Student> _studentEntityRepository;
        private readonly IEntityRepository<StudentClass> _classEntityRepository;
        private readonly IEntityRepository<BookLoan> _bookLoanEntityRepository;

        public StudentInteractor() { }
        public StudentInteractor(IEntityRepository<Student> studentEntityRepository, IEntityRepository<StudentClass> classEntityRepository,IEntityRepository<BookLoan> bookLoanEntityRepository)
        {
            _studentEntityRepository = studentEntityRepository;
            _classEntityRepository = classEntityRepository;
            _bookLoanEntityRepository = bookLoanEntityRepository;
        }
        public Student FindStudentBy(int id)
        {
            var student = _studentEntityRepository.GetEntity(id);
            var studentLoans = _bookLoanEntityRepository.GetAll().Where(s => s.StudentId == id);
            student.BookLoans = studentLoans.ToList();
            return student;
        }
        public IList<Student> FindAllStudents()
        {
            var students = _studentEntityRepository.GetAll().ToList();
            var studentLoans = _bookLoanEntityRepository.GetAll();
            foreach (var student in students)
            {
                student.BookLoans = studentLoans.Where(s => s.StudentId == student.Id).ToList();
            }
            return students ;
        }
        public void Add(Student entity)
        {
            _studentEntityRepository.InsertOnCommit(entity);
            _studentEntityRepository.CommitChangesAsync();
        }
        public void Delete(Student entity)
        {
            _studentEntityRepository.DeleteOnCommit(entity);
            _studentEntityRepository.CommitChangesAsync();
        }
        public void SaveChanges(Student student)
        {
            _studentEntityRepository.Entry(student).State = EntityState.Modified;
            _studentEntityRepository.CommitChangesAsync();
        }
        public IList<StudentClass> FindAllClasses()
        {
            return _classEntityRepository.GetAll().ToList();
        }
    }
}
