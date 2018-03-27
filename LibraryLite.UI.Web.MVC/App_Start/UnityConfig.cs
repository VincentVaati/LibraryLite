using Unity;
using Unity.Injection;
using Unity.Mvc5;
using LibraryLite.Core.Entities;
using LibraryLite.UseCases.Interactors;
using LibraryLite.UseCases.Interfaces;
using LibraryLite.UI.Web.MVC.Controllers;
using LibraryLite.UI.Web.MVC.Gateways;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using LibraryLite.UI.Web.MVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LibraryLite.UI.Web.MVC
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IEntitiesContext, EntitiesContext>();
            container.RegisterType(typeof(IEntityRepository<Book>), typeof(EntityRepository<Book>));
            container.RegisterType(typeof(IEntityRepository<BookInventory>), typeof(EntityRepository<BookInventory>));
            container.RegisterType(typeof(IEntityRepository<Student>), typeof(EntityRepository<Student>));
            container.RegisterType(typeof(IEntityRepository<StudentClass>), typeof(EntityRepository<StudentClass>));
            container.RegisterType(typeof(IEntityRepository<ApplicationSettings>), typeof(EntityRepository<ApplicationSettings>));
            container.RegisterType(typeof(IEntityRepository<BookLoan>), typeof(EntityRepository<BookLoan>));

            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<AdminController>(new InjectionConstructor());

            container.RegisterType<IStudentInteractor, StudentInteractor>();
            container.RegisterType<IBookInteractor, BookInteractor>();
            container.RegisterType<IClassInteractor, ClassInteractor>();
            container.RegisterType<ISettingsInteractor, SettingsInteractor>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}