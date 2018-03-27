using System.Web.Mvc;
using System.Linq;
using System.Net;
using System.Data.Entity;
using LibraryLite.UseCases.Interactors;
using LibraryLite.UI.Web.MVC.Gateways;
using LibraryLite.UI.Web.MVC.Presentation.ViewModels;
using LibraryLite.Core.Entities;
using LibraryLite.UseCases.Dtos;
using System.Threading.Tasks;
using LibraryLite.UseCases.Interfaces;
using LibraryLite.UI.Web.MVC.Presentation.Mapping;

namespace LibraryLite.UI.Web.MVC.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class ClassController : Controller
    {
        private IClassInteractor _classInteractor;

        public ClassController(IClassInteractor classInteractor)
        {
            _classInteractor = classInteractor;
        }
        // GET: /Class/
        public ActionResult Index()
        {
            var classes = _classInteractor.FindAllClasses();
            return View(ClassMapperExtensionMethods.ConvertToClassList(classes));
        }

        // GET: /Class/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Class/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClassName")] ClassViewModel studentClassViewModel)
        {
            if (ModelState.IsValid)
            {
                _classInteractor.Add(ClassMapperExtensionMethods.ConvertToClass(studentClassViewModel));
                return RedirectToAction("Index");
            }

            return View(studentClassViewModel);
        }

        // GET: /Class/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var studentclass = _classInteractor.FindClassBy(id.Value);

            if (studentclass == null)
            {
                return HttpNotFound();
            }
            return View(ClassMapperExtensionMethods.ConvertToClassViewModel(studentclass));
        }

        // POST: /Class/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClassName")] ClassViewModel studentClassViewModel)
        {
            if (ModelState.IsValid)
            {
                _classInteractor.SaveChanges(ClassMapperExtensionMethods.ConvertToClass(studentClassViewModel));
                return RedirectToAction("Index");
            }
            return View(studentClassViewModel);
        }

        // GET: /Class/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var studentClass =_classInteractor.FindClassBy(id.Value);

            if (studentClass == null)
            {
                return HttpNotFound();
            }
            return View(ClassMapperExtensionMethods.ConvertToClassViewModel(studentClass));
        }

        // POST: /Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  ActionResult DeleteConfirmed(int id)
        {
            var studentClass =_classInteractor.FindClassBy(id);
            _classInteractor.Delete(studentClass);
           
            return RedirectToAction("Index");
        }
    }
}
