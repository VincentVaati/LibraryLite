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
using LibraryLite.UseCases.Interactors;
using LibraryLite.UseCases.Interfaces;
using LibraryLite.UI.Web.MVC.Presentation.ViewModels;
using LibraryLite.UI.Web.MVC.Presentation.Mapping;
using System.Threading.Tasks;

namespace LibraryLite.UI.Web.MVC.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class StudentController : Controller
    {
        private IStudentInteractor _studentInteractor;

        public StudentController() { }
        public StudentController(IStudentInteractor studentInteractor)
        {
            _studentInteractor = studentInteractor;
        }
        // GET: /Student/
        public ActionResult Index()
        {
            var students = _studentInteractor.FindAllStudents();
            return View(StudentMapperExtensionMethods.ConvertToStudentList(students));
        }

        // GET: /Student/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(_studentInteractor.FindAllClasses(), "Id", "ClassName");
            return View();
        }

        // POST: /Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StudentNumber,FirstName,LastName,ClassId")] StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                _studentInteractor.Add(StudentMapperExtensionMethods.ConvertToStudent(studentViewModel));
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(_studentInteractor.FindAllClasses(), "Id", "ClassName", studentViewModel.ClassId);
            return View(studentViewModel);
        }

        // GET: /Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var student = _studentInteractor.FindStudentBy(id.Value);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(_studentInteractor.FindAllClasses(), "Id", "ClassName", student.ClassId);
            return View(StudentMapperExtensionMethods.ConvertToStudentViewModel(student));
        }

        // POST: /Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StudentNumber,FirstName,LastName,ClassId")] StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                //_studentInteractor.Entry(student).State = EntityState.Modified;
                _studentInteractor.SaveChanges(StudentMapperExtensionMethods.ConvertToStudent(studentViewModel));
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(_studentInteractor.FindAllClasses(), "Id", "ClassName", studentViewModel.ClassId);
            return View(studentViewModel);
        }

        // GET: /Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var student = _studentInteractor.FindStudentBy(id.Value);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(StudentMapperExtensionMethods.ConvertToStudentViewModel(student));
        }

        // POST: /Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var student = _studentInteractor.FindStudentBy(id);
            _studentInteractor.Delete(student);
            return RedirectToAction("Index");
        }
    }
}
