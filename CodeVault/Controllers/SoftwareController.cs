using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using CodeVault.Models.ViewModels;
using CodeVault.Models;
using CodeVault.Models.BaseTypes;

namespace CodeVault.Controllers
{
    public class SoftwareController : Controller
    {
        private CV2Context db = new CV2Context();
        IDALFacade facade = new DALFacade();

        public ActionResult Index()
        {
            IUnitOfWork unitOfWork = facade.GetUnitOfWork();
            var query = unitOfWork.ProductRepo.GetByQuery(p => p.ProductStatus != ProductStatus.Canceled, o => o.OrderBy(n => n.ProductName));
            var result = query.Select(p => new ProductViewModel(p)).ToList();
            facade.DisposeUnitOfWork();
            return View(result);
        }

        public ActionResult ProductViewModels_Read([DataSourceRequest]DataSourceRequest request)
        {
            IUnitOfWork unitOfWork = facade.GetUnitOfWork();
            var query = unitOfWork.ProductRepo.GetByQuery(p => p.ProductStatus != ProductStatus.Canceled, o => o.OrderBy(n => n.ProductName));
            var result = query.Select(p => new ProductViewModel(p));
            facade.DisposeUnitOfWork();

            return Json(result.ToDataSourceResult(request));
        }

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
            //ProductViewModel productViewModel = db.ProductViewModels.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.DetailTitle = string.Format("{0} {1}", result.Name, result.Version);
            return View(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ProductViewModels_Update([DataSourceRequest]DataSourceRequest request, ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var entity = new ProductViewModel
                {
                    Id = productViewModel.Id,
                    Name = productViewModel.Name,
                    Manufacturer = productViewModel.Manufacturer,
                    Description = productViewModel.Description,
                    Version = productViewModel.Version,
                    CreatedOnDate = productViewModel.CreatedOnDate
                };

                db.ProductViewModels.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { productViewModel }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
