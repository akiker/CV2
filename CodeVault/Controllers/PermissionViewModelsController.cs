using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodeVault.Models;
using CodeVault.Models.ViewModels;
using CodeVault.Models.BaseTypes;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace CodeVault.Controllers
{
    public class PermissionViewModelsController : Controller
    {
        private CV2Context db = new CV2Context();
        IDALFacade facade = new DALFacade();

        // GET: PermissionViewModels
        public ActionResult Index()
        {
            IUnitOfWork unitOfWork = facade.GetUnitOfWork();
            var query = unitOfWork.ProductRepo.GetByQuery(p => p.ProductStatus != ProductStatus.Canceled, o => o.OrderBy(n => n.ProductName));
            var result = query.Select(p => new PermissionViewModel(p)).ToList();
            facade.DisposeUnitOfWork();
            return View(result);
        }

        public ActionResult PermissionViewModel_Read([DataSourceRequest]DataSourceRequest request)
        {
            IUnitOfWork unitOfWork = facade.GetUnitOfWork();
            var query = unitOfWork.ProductRepo.GetByQuery(p => p != null, o => o.OrderBy(n => n.ProductName));
            var result = query.Select(p => new PermissionViewModel(p));
            facade.DisposeUnitOfWork();

            return Json(result.ToDataSourceResult(request));
        }

        // GET: PermissionViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IUnitOfWork unitOfWork = facade.GetUnitOfWork();
            var query = unitOfWork.ProductRepo.GetByQuery(p => p.ProductId == id, o => o.OrderBy(n => n.ProductName));
            var result = query.Select(p => new PermissionViewModel(p)).FirstOrDefault();
            var detail = result.PermissionDetails.ToList();
            facade.DisposeUnitOfWork();
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        // GET: PermissionViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PermissionViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ElevatedRightsRequired,RequiresAdminRightsBasic,RequiresAdminRightsAdvanced,RequiresAdminRightsUpdate,LaVerified,WorksWithLa,DoesNotWorkWithLa")] PermissionViewModel permissionViewModel)
        {
            if (ModelState.IsValid)
            {
                db.PermissionViewModels.Add(permissionViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(permissionViewModel);
        }

        // GET: PermissionViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            IUnitOfWork unitOfWork = facade.GetUnitOfWork();
            var query = unitOfWork.ProductRepo.GetByQuery(p => p.ProductId == id, o => o.OrderBy(n => n.ProductName));
            var result = query.Select(p => new PermissionViewModel(p)).FirstOrDefault();
            facade.DisposeUnitOfWork();
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: PermissionViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ElevatedRightsRequired,RequiresAdminRightsBasic,RequiresAdminRightsAdvanced,RequiresAdminRightsUpdate,LaVerified,WorksWithLa,DoesNotWorkWithLa")] PermissionViewModel permissionViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permissionViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(permissionViewModel);
        }

        // GET: PermissionViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PermissionViewModel permissionViewModel = db.PermissionViewModels.Find(id);
            if (permissionViewModel == null)
            {
                return HttpNotFound();
            }
            return View(permissionViewModel);
        }

        // POST: PermissionViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PermissionViewModel permissionViewModel = db.PermissionViewModels.Find(id);
            db.PermissionViewModels.Remove(permissionViewModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
