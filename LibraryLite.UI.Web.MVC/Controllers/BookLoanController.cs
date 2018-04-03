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
using LibraryLite.UI.Web.MVC.Helpers;
using LibraryLite.UseCases.Infrastructure;

namespace LibraryLite.UI.Web.MVC.Controllers
{
    
    [Authorize(Roles = "Admin,Staff")]
    public class BookLoanController : Controller
    {
        private IStudentInteractor _studentInteractor;
        private IBookInteractor _bookInteractor;
        public RequestBookLoanInteractor _requestBookLoanInteractor;
        public RequestBookReturnInteractor _requestBookReturnInteractor;
        private IList<Student> Students
        {
            get
            {
                return _studentInteractor.FindAllStudents();
            }
        }
        private IList<Book> Books
        {
            get
            {
                return _bookInteractor.FindAllBooks();
            }
        }
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
            ViewBag.StudentSelectionList = new SelectList(Students.Select(s => new { Id = s.Id, Name = s.FirstName + " " + s.LastName }), "Id", "Name");
            ViewBag.BookSelectionList = new SelectList(Books.Select(b => new { Id = b.Id, BookTitle = b.BookTitle }), "Id", "BookTitle");
            ViewBag.Reports = new SelectList(EnumHelper.ToSelectList<Report>(new Report()), "Value", "Text");
            ViewBag.Months = new SelectList(EnumHelper.ToSelectList<Months>(new Months()), "Value", "Text");

            return View();
        }
        public JsonResult GetLoans(int? id)
        {
            IList<StudentBookLoan> studentBookLoans = new List<StudentBookLoan>();
            if (!id.HasValue)
                id = 0;
            if (id == 1)
            {
                foreach (var student in Students)
                {
                    foreach (var loan in student.BookLoans)
                    {
                        //Get late loans only
                        if(DateTime.Now.Date > loan.DueDate && loan.DateReturned ==null)
                        {
                            var x = new StudentBookLoan()
                            {
                                FirstName = student.FirstName ,
                                LastName = student.LastName,
                                BookTitle = loan.Book.BookTitle,
                                DateOfIssue = loan.DateOfIssue.ToString("dd/mm/yyyy"),
                                DueDate = loan.DueDate.ToString("dd/mm/yyyy"),
                                DateReturned = loan.DateReturned.ToString()
                            };

                            studentBookLoans.Add(x);
                        }
                    }
                }
            }
            else if (id == 2)
            {
                foreach (var student in Students)
                {
                    foreach (var loan in student.BookLoans)
                    {
                        if(loan.DateReturned!= null && loan.DateReturned > loan.DueDate )
                        {
                            var x = new StudentBookLoan()
                            {
                                FirstName = student.FirstName ,
                                LastName = student.LastName,
                                BookTitle = loan.Book.BookTitle,
                                Penalty =loan.Penalty
                            };
                        }
                    }
                }
            }
            return Json(studentBookLoans, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBookLoansbyMonth(ApplicationFilter filter)
        {
            var loans = _requestBookLoanInteractor.GetBookLoanPenaltyBy(filter);
            IList<StudentBookLoan> loansList = new List<StudentBookLoan>();
            foreach (var loan in loans)
            {
                var x = new StudentBookLoan()
                {
                    FirstName = loan.Student.FirstName,
                    LastName = loan.Student.LastName,
                    BookTitle = loan.Book.BookNumber,
                    Penalty = loan.Penalty,
                };
                loansList.Add(x);
            }
            return Json(loansList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBookLoans(int? id)
        {
            var students = _studentInteractor.GetStudentsLoans().ToList();
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
                        if (loan.DateReturned == null)
                        {
                            var x = new StudentBookLoan()
                            {
                                Id = loan.Id,
                                FirstName = student.FirstName,
                                LastName = student.LastName,
                                BookTitle = loan.Book.BookTitle,
                                DateOfIssue = loan.DateOfIssue.ToString("dd/mm/yyyy"),
                                DueDate = loan.DueDate.ToString("dd/mm/yyyy"),
                                DateReturned = loan.DateReturned.ToString()
                            };

                            studentBookLoans.Add(x);
                        }
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
                            Id = loan.Id,
                            FirstName = student.FirstName,
                            LastName = student.LastName,
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

            ViewBag.StudentSelectionList = new SelectList(Students.Select(s => new { Id = s.Id, Name = s.FirstName + " " + s.LastName }), "Id", "Name");
            ViewBag.BookSelectionList = new SelectList(Books.Select(b => new { Id = b.Id, BookTitle = b.BookTitle }), "Id", "BookTitle");

            var view = new BookLoanRequestViewModel();
            var dueDate = DateTime.Now.Date.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture);
            view.DateOfIssue = Convert.ToDateTime(dueDate);
            view.DueDate = _requestBookLoanInteractor.CalculateDueDate();

            return View(view);
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
                if (bookLoanRequestViewModel.DueDate < DateTime.Now.Date)
                    ModelState.AddModelError("PreviousDate", "Due date cannot be in the past");

                if (bookLoanRequestViewModel.DueDate != _requestBookLoanInteractor.CalculateDueDate())
                    ModelState.AddModelError("Duedate", "Wrong due date");

                _requestBookLoanInteractor.Handle(BookLoanMapperExtensionMethods.ConvertToBookLoan(bookLoanRequestViewModel));

                return RedirectToAction("Index");
            }

            ViewBag.BookSelectList = new SelectList(bookLoanRequestViewModel.Books, "Id", "BookTitle");
            ViewBag.StudentSelectList = new SelectList(bookLoanRequestViewModel.Students, "Id", "FirstName");
            return View(bookLoanRequestViewModel);
        }

        public ActionResult Returnbook(int? id)
        {
            var vm = new ReturnBookResponseViewModel();
            vm.ReturnDate = DateTime.Now.Date;
            vm.DueDate = _requestBookReturnInteractor.GetDueDate(id.Value);

            var bookReturnRequestMesage = new BookReturnRequestMesage(id.Value);
            if (_requestBookReturnInteractor.IsLateLoan(id.Value))
            {
                vm.Penalty = _requestBookReturnInteractor.CalculatePenalty(id.Value);
            }
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReturnBook(ReturnBookResponseViewModel vm) 
        {
            if (ModelState.IsValid)
            {
                var bookReturnRequestMesage = new BookReturnRequestMesage(vm.Id);
                _requestBookReturnInteractor.Handle(bookReturnRequestMesage);
                return RedirectToAction("Index");
            }

            return View(vm);
        }
    }
}
