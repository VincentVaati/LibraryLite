using LibraryLite.Core.Entities;
using LibraryLite.UI.Web.MVC.Presentation.ViewModels;
using System.Collections.Generic;

namespace LibraryLite.UI.Web.MVC.Presentation.Mapping
{
    public static class StudentMapperExtensionMethods
    {
        public static Student ConvertToStudent(StudentViewModel studentViewModel)
        {
            var student = new Student
            {
                Id = studentViewModel.Id,
                StudentNumber = studentViewModel.StudentNumber,
                FirstName = studentViewModel.FirstName,
                LastName = studentViewModel.LastName,
                ClassId = studentViewModel.ClassId,
                Class= studentViewModel.Class
            };
            return student;
        }
        public static StudentViewModel ConvertToStudentViewModel(Student student)
        {
            var studentViewModel = new StudentViewModel
            {
                Id = student.Id,
                StudentNumber = student.StudentNumber,
                FirstName = student.FirstName,
                LastName = student.LastName,
                ClassId = student.ClassId,
                Class = student.Class
            };
            return studentViewModel;
        }
        public static IList<StudentViewModel> ConvertToStudentList(IList<Student> students)
        {
            IList<StudentViewModel> studentViewModelList = new List<StudentViewModel>();
            foreach (var student in students)
            {
                studentViewModelList.Add(ConvertToStudentViewModel(student));
            }
            return studentViewModelList;
        }
    }
}