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
using LibraryLite.UI.Web.MVC.Presentation.ViewModels;
using LibraryLite.UI.Web.MVC.Presentation.Mapping;

namespace LibraryLite.UI.Web.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SettingsController : Controller
    {
        private ISettingsInteractor _settingsInteractor;

        public SettingsController() { }
        public SettingsController(ISettingsInteractor settingsInteractor)
        {
            _settingsInteractor = settingsInteractor;
        }

        // GET: /Settings/
        public ActionResult Index()
        {
            var settings = _settingsInteractor.FindAllSettings();
           
            return View( SettingsMapperExtensionMethods.ConvertToAppSettingsList(settings));
        }

        // GET: /Settings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Settings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InstitutionName,MaxBooksLoanable,LoanDays,Penalty")] ApplicationSettingsViewModel applicationSettingsViewModel)
        {
            if (ModelState.IsValid)
            {
                _settingsInteractor.Add(SettingsMapperExtensionMethods.ConvertToApplicationSettings(applicationSettingsViewModel));
                return RedirectToAction("Index");
            }

            return View(applicationSettingsViewModel);
        }

        // GET: /Settings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var applicationsettings = _settingsInteractor.FindSettingsBy(id.Value);
            if (applicationsettings == null)
            {
                return HttpNotFound();
            }
            return View(SettingsMapperExtensionMethods.ConvertToAppSettingsViewModel(applicationsettings));
        }

        // POST: /Settings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InstitutionName,MaxBooksLoanable,LoanDays,Penalty")] ApplicationSettingsViewModel applicationSettingsViewModel)
        {
            if (ModelState.IsValid)
            {
                _settingsInteractor.SaveChanges(SettingsMapperExtensionMethods.ConvertToApplicationSettings(applicationSettingsViewModel));
                return RedirectToAction("Index");
            }
            return View(applicationSettingsViewModel);
        }

        // GET: /Settings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var applicationsettings = _settingsInteractor.FindSettingsBy(id.Value);
            if (applicationsettings == null)
            {
                return HttpNotFound();
            }
            return View(SettingsMapperExtensionMethods.ConvertToAppSettingsViewModel(applicationsettings));
        }

        // POST: /Settings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var applicationsettings = _settingsInteractor.FindSettingsBy(id);
            _settingsInteractor.Delete(applicationsettings);
            return RedirectToAction("Index");
        }
    }
}
