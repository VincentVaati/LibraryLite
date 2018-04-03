using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryLite.UseCases.Interactors;
using LibraryLite.UseCases.Infrastructure;
using LibraryLite.UI.Web.MVC.Presentation.Mapping;
using LibraryLite.UI.Web.MVC.Helpers;

namespace LibraryLite.UI.Web.MVC.Controllers
{
    public class ReportsController : Controller
    {
        private readonly StudentInteractor _studentInteractor;

        public ReportsController() { }
        public ReportsController(StudentInteractor studentInteractor)
        {
            _studentInteractor = studentInteractor;
        }
        public ActionResult RevenueReport()
        {
            ViewBag.Reports = new SelectList(EnumHelper.ToSelectList<Report>(new Report()), "Value", "Text");
            ViewBag.Months = new SelectList(EnumHelper.ToSelectList<Months>(new Months()), "Value", "Text");

            return View();
        }
        public JsonResult GetRevenueReport(ApplicationFilter filter)
        {
            var students = _studentInteractor.GetStudentsLoans().ToList();
            var penaltyRevenueViewModelList = ReportsMapperExtensionMethods.ConvertToPenaltyRevenueViewModelList(students);

            return Json(penaltyRevenueViewModelList,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPenaltyRevenueReport(ApplicationFilter filter)
        {
            var students = _studentInteractor.FindAllStudents();
            var penaltyRevenueViewModelList = ReportsMapperExtensionMethods.ConvertToPenaltyRevenueViewModelList(students);

            return Json(penaltyRevenueViewModelList,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCurrentStudentsLoans(ApplicationFilter filter)
        {
            var bookLoansViewModelList  = ReportsMapperExtensionMethods.ConvertToBookLoansViewModelList(_studentInteractor.GetCurrentStudentsLoans());

            return Json(bookLoansViewModelList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPendingLoans(ApplicationFilter filter)
        {
            var bookLoansViewModelList = ReportsMapperExtensionMethods.ConvertToBookLoansViewModelList(_studentInteractor.GetPendingStudentsLoans());

            return Json(bookLoansViewModelList, JsonRequestBehavior.AllowGet);
        }
	}
}