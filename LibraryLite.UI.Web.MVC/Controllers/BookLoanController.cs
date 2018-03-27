using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryLite.Core.Entities;
using LibraryLite.UI.Web.MVC.Gateways;
using LibraryLite.UI.Web.MVC.Presentation.Mapping;
using LibraryLite.UI.Web.MVC.Presentation.ViewModels;
using LibraryLite.UseCases.Interfaces;
using LibraryLite.UseCases.Interactors;
using LibraryLite.UseCases.Dtos;
using System.Globalization;

namespace LibraryLite.UI.Web.MVC.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class BookLoanController : Controller
    {
        private IStudentInteractor _studentInteractor;
        private IBookInteractor _bookInteractor;
        public RequestBookLoanInteractor _requestBookLoanInteractor;
        public RequestBookReturnInteractor _requestBookReturnInteractor;
             
        public BookLoanController() { }
        public BookLoanController(IStudentInteractor studentInteractor, IBookInteractor bookInteractor, RequestBookLoanInteractor requestBookLoanInteractor, RequestBookReturnInteractor requestBookReturnInteractor)
        {
            _studentInteractor = studentInteractor;
            _bookInteractor = bookInteractor;
            _requestBookLoanInteractor = requestBookLoanInteractor;
            _requestBookReturnInteractor = requestBookReturnInteractor;
        }
        public ActionResult Index()
        {
            var students = _studentInteractor.FindAllStudents();
            var books = _bookInteractor.FindAllBooks();

            ViewBag.StudentsSelectionList = new SelectList(students, "Id", "FirstName");
            ViewBag.BooksSelectionList = new SelectList(books, "Id", "BookTitle");

            return View();
        }
        public JsonResult GetBookLoans(int? id)
        {
            var students = _studentInteractor.FindAllStudents().ToList();
            var books = _bookInteractor.FindAllBooks();

            IList<StudentBookLoan> studentBookLoans  = new List<StudentBookLoan>();
            if (!id.HasValue)
                id = 0;
            if (id == 0)
            {
                foreach (var student in students)
                {
                    foreach (var loan in student.BookLoans)
                    {
                        var x = new StudentBookLoan()
                        {
                            Name = student.FirstName + " " + student.LastName,
                            BookTitle = loan.Book.BookTitle,
                            DateOfIssue = loan.DateOfIssue.ToString("dd/mm/yyyy"),
                            DueDate = loan.DueDate.ToString("dd/mm/yyyy"),
                            DateReturned = loan.DateReturned.ToString()
                        };

                        studentBookLoans.Add(x);
                    }
                }
            }
            else
            {
                var  student = students.Where(u=>u.Id==id.Value).FirstOrDefault();
                foreach (var loan in student.BookLoans)
                {
                    var x = new StudentBookLoan()
                        {
                            Name = student.FirstName + " " + student.LastName,
                            BookTitle = loan.Book.BookTitle,
                            DateOfIssue = loan.DateOfIssue.ToString("dd/mm/yyyy", CultureInfo.CurrentCulture),
                            DueDate = loan.DueDate.ToString("dd/mm/yyyy", CultureInfo.CurrentCulture),
                            DateReturned = loan.DateReturned.ToString()
                        };

                        studentBookLoans.Add(x);
                }
            }
                
            return Json(studentBookLoans, JsonRequestBehavior.AllowGet);
        }
       
        //GET: /BookLoan/Create
        public ActionResult Create()
        {
            ViewBag.BookId = new SelectList(_bookInteractor.FindAllBooks(), "Id", "BookNumber");
            ViewBag.StudentId = new SelectList(_studentInteractor.FindAllStudents(), "Id", "StudentNumber");
            return View();
        }

        // POST: /BookLoan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BookId,StudentId,BookNumber,DateOfIssue,DueDate,DateReturned")] BookLoanRequestViewModel bookLoanRequestViewModel)
        {
            if (ModelState.IsValid)
            {
                _requestBookLoanInteractor.Handle(BookLoanMapperExtensionMethods.ConvertToBookLoan(bookLoanRequestViewModel));

                return RedirectToAction("Index");
            }


            ViewBag.BookId = new SelectList(bookLoanRequestViewModel.Books, "Id", "BookNumber", bookLoanRequestViewModel.BookId);
            ViewBag.StudentId = new SelectList(bookLoanRequestViewModel.Students, "Id", "StudentNumber", bookLoanRequestViewModel.StudentId);
            return View(bookLoanRequestViewModel);
        }

        public ActionResult ReturnBook(int id) 
        {

            var bookReturnRequestMesage = new BookReturnRequestMesage(id);
            _requestBookReturnInteractor.Handle(bookReturnRequestMesage);

            return RedirectToAction("Index");
        }
    }
}
