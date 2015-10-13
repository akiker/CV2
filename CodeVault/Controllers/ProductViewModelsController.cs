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

namespace CodeVault.Controllers
{
    public class ProductViewModelsController : Controller
    {
        private CV2Context db = new CV2Context();
        IDALFacade facade = new DALFacade();

        // GET: ProductViewModels
        public ActionResult Index()
        {
            IUnitOfWork unitOfWork = facade.GetUnitOfWork();
            var query = unitOfWork.ProductRepo.GetByQuery(p => p.ProductStatus != ProductStatus.Canceled, o => o.OrderBy(n => n.ProductName));
            var result = query.Select(p => new ProductViewModel(p)).ToList();
            facade.DisposeUnitOfWork();
            return View(result);
        }

        // GET: ProductViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IUnitOfWork unitOfWork = facade.GetUnitOfWork();
            var query = unitOfWork.ProductRepo.GetByQuery(p => p.ProductId == id, o => o.OrderBy(n => n.ProductName));
            var result = query.Select(p => new ProductViewModel(p)).FirstOrDefault();
            facade.DisposeUnitOfWork();
            if (result == null)
            {
                return HttpNotFound();
            }
            
            return View(result);
        }

        // GET: ProductViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Manufacturer,Description,Version,CreatedOnDate")] ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                db.ProductViewModels.Add(productViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productViewModel);
        }

        // GET: ProductViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductViewModel productViewModel = db.ProductViewModels.Find(id);
            if (productViewModel == null)
            {
                return HttpNotFound();
            }
            return View(productViewModel);
        }

        // POST: ProductViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Manufacturer,Description,Version,CreatedOnDate")] ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productViewModel);
        }

        // GET: ProductViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductViewModel productViewModel = db.ProductViewModels.Find(id);
            if (productViewModel == null)
            {
                return HttpNotFound();
            }
            return View(productViewModel);
        }

        // POST: ProductViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductViewModel productViewModel = db.ProductViewModels.Find(id);
            db.ProductViewModels.Remove(productViewModel);
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
