using LibraryLite.Core.Entities;
using LibraryLite.UI.Web.MVC.Presentation.ViewModels;
using System.Collections.Generic;

namespace LibraryLite.UI.Web.MVC.Presentation.Mapping
{
    public static class ClassMapperExtensionMethods
    {
        public static StudentClass ConvertToClass(ClassViewModel classViewModel)
        {
            var studentClass = new StudentClass
            {
                Id=classViewModel.Id,
                ClassName=classViewModel.ClassName
            };
            return studentClass;
        }
        public static ClassViewModel ConvertToClassViewModel(StudentClass cls)
        {
            var classViewModel = new ClassViewModel
            {
                Id = cls.Id,
                ClassName = cls.ClassName
            };
            return classViewModel;
        }
        public static IList<ClassViewModel> ConvertToClassList(IList<StudentClass> classes)
        {
            IList<ClassViewModel> classesViewModelList = new List<ClassViewModel>();
            foreach (var cls in classes)
            {
                classesViewModelList.Add(ConvertToClassViewModel(cls));
            }
            return classesViewModelList;
        }
    }
}