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
            return View();
        }

        public ActionResult ProductViewModels_Read([DataSourceRequest]DataSourceRequest request)
        {
            IUnitOfWork unitOfWork = facade.GetUnitOfWork();
            var query = unitOfWork.ProductRepo.GetByQuery(p => p.ProductStatus != ProductStatus.Canceled, o => o.OrderBy(n => n.ProductName));
            var result = query.Select(p => new ProductViewModel(p));
            
            facade.DisposeUnitOfWork();
            return Json(result.ToDataSourceResult(request));
        }

        public ActionResult SoftwarePolicyGroupAssociationViewModel_Read([DataSourceRequest]DataSourceRequest request, int id)
        {
            IUnitOfWork unitOfWork = facade.GetUnitOfWork();
            var query = unitOfWork.ProductRepo.GetByQuery(p => p.ProductId == id, o => o.OrderBy(n => n.ProductName), "SoftwarePolicy");
            var productViewModel = query.Select(p => new ProductViewModel(p)).FirstOrDefault();
            facade.DisposeUnitOfWork();
            var result = productViewModel.SoftwarePolicyGroupAssociations;
            return Json(result.ToDataSourceResult(request));
        }

        public ActionResult PreInstallDependencyViewModel_Read([ DataSourceRequest]DataSourceRequest request, int id)
        {
            IUnitOfWork unitOfWork = facade.GetUnitOfWork();
            var query = unitOfWork.ProductRepo.GetByQuery(p => p.ProductId == id, o => o.OrderBy(n => n.ProductName),"Dependencies");
            var productViewModel = query.Select(p => new ProductViewModel(p)).FirstOrDefault();
            facade.DisposeUnitOfWork();
            var result = productViewModel.PreInstallDependencies;
            return Json(result.ToDataSourceResult(request));
        }

        public ActionResult PostInstallDependencyViewModel_Read([DataSourceRequest]DataSourceRequest request, int id)
        {
            IUnitOfWork unitOfWork = facade.GetUnitOfWork();
            var query = unitOfWork.ProductRepo.GetByQuery(p => p.ProductId == id, o => o.OrderBy(n => n.ProductName),"Dependencies");
            var productViewModel = query.Select(p => new ProductViewModel(p)).FirstOrDefault();
            facade.DisposeUnitOfWork();
            var result = productViewModel.PostInstallDependencies;
            return Json(result.ToDataSourceResult(request));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IUnitOfWork unitOfWork = facade.GetUnitOfWork();
            var query = unitOfWork.ProductRepo.GetByQuery(p => p.ProductId == id, o => o.OrderBy(n => n.ProductName), "ProductCategory,ProductType");
            var result = query.Select(p => new ProductViewModel(p)).FirstOrDefault();
            facade.DisposeUnitOfWork();
            
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.DetailTitle = string.Format("{0} {1}", result.Name, result.Version);
            return View(result);
        }

        //private IEnumerable<DependencyViewModel> GetPreInstallDependencies(int productId)
        //{
        //    //IUnitOfWork unitOfWork = facade.GetUnitOfWork();
        //    //var query = unitOfWork.ProductRepo.GetByQuery(p => p.ProductId == productId, o => o.OrderBy(n => n.ProductName));
        //    //var result = query.Select(p => new ProductViewModel(p));
        //    //facade.DisposeUnitOfWork();
        //    //var temp = result.Select(d => d.Dependencies).Where(d => d.depen)
            
        //    //List<DependencyViewModel> depViewModels = new List<DependencyViewModel>(); 

        //    //var db = new CV2Context(); 
        //    //var query = db.Products.Where(d => d.ProductId == productId).FirstOrDefault();
        //    //List<Dependencies> dependencies = query.Dependencies.Where(d => d.DependencyType == Models.DependencyType.PreInstall).ToList();
        //    //foreach (var dep in dependencies)
        //    //{
        //    //    DependencyViewModel depViewModel = new DependencyViewModel(dep);
        //    //    depViewModels.Add(depViewModel);
        //    //}
        //    //return depViewModels;
        //}

        //private IEnumerable<DependencyViewModel> GetPostInstallDependencies(int productId)
        //{
        //    var db = new CV2Context();
        //    var query = db.Dependencies.Where(d => d.BaseProductId == productId && d.DependencyType == Models.DependencyType.PostInstall).AsEnumerable();
        //    var result = query.Select(p => new DependencyViewModel(p));
            
        //    return result;
        //}


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
