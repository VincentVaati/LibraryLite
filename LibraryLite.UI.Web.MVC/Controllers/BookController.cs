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
using LibraryLite.UseCases.Interfaces;
using LibraryLite.UseCases.Dtos;
using LibraryLite.UI.Web.MVC.Presentation.ViewModels;
using LibraryLite.UI.Web.MVC.Presentation.Mapping;

namespace LibraryLite.UI.Web.MVC.Controllers
{
    public class BookController : Controller
    {
        private IBookInteractor _bookInteractor;

        public BookController()
        {

        }
        public BookController(IBookInteractor bookInteractor)
        {
            _bookInteractor = bookInteractor;
        }


        // GET: /Book/
        public ActionResult Index()
        {
            var books = _bookInteractor.FindAllBooks();
            return View(BookMapperExtensionMethods.ConvertToBookList(books));
        }
        // GET: /Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BookNumber,ISBN,BookTitle,Author")] BookViewModel bookViewModel)
        {
            if (ModelState.IsValid)
            {
                _bookInteractor.Add(BookMapperExtensionMethods.ConvertToBook(bookViewModel));
                return RedirectToAction("Index");
            }

            return View(bookViewModel);
        }

        // GET: /Book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = _bookInteractor.FindBookBy(id.Value);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(BookMapperExtensionMethods.ConvertToBookViewModel(book));
        }

        // POST: /Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BookNumber,ISBN,BookTitle,Author")] BookViewModel bookViewModel)
        {
            if (ModelState.IsValid)
            {
                _bookInteractor.SaveChanges(BookMapperExtensionMethods.ConvertToBook(bookViewModel));
                return RedirectToAction("Index");
            }
            return View(bookViewModel);
        }

        // GET: /Book/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = _bookInteractor.FindBookBy(id.Value);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(BookMapperExtensionMethods.ConvertToBookViewModel(book));
        }

        // POST: /Book/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var book = _bookInteractor.FindBookBy(id);
            _bookInteractor.Delete(book);
            return RedirectToAction("Index");
        }
    }
}
