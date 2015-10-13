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
    public class PermissionDetailsViewModelsController : Controller
    {
        private CV2Context db = new CV2Context();
        IDALFacade facade = new DALFacade();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PermissionDetailViewModels_Read([DataSourceRequest]DataSourceRequest request, int productId)
        {
            IUnitOfWork unitOfWork = facade.GetUnitOfWork();
            var query = unitOfWork.PermissionDetailRepo.GetByQuery(p => p.ProductId == productId, o => o.OrderBy(n => n.ProductPermissionDetailId));
            var result = query.Select(p => new PermissionDetailViewModel(p));
            facade.DisposeUnitOfWork();
            
            //IQueryable<PermissionDetailViewModel> permissiondetailviewmodels = db.PermissionDetailViewModels;
            //DataSourceResult result = permissiondetailviewmodels.ToDataSourceResult(request, c => new PermissionDetailViewModel 
            //{
            //    PermissionDetailViewModel = c.PermissionDetailViewModel,
            //    Id = c.Id,
            //    Permission = c.Permission,
            //    Type = c.Type,
            //    GroupUser = c.GroupUser,
            //    Location = c.Location
            //});

            return Json(result.ToDataSourceResult(request));
        }

        public ActionResult Details([DataSourceRequest]DataSourceRequest request, int productId)
        {
            IUnitOfWork unitOfWork = facade.GetUnitOfWork();
            var query = unitOfWork.PermissionDetailRepo.GetByQuery(p => p.ProductId == productId, o => o.OrderBy(n => n.ProductPermissionDetailId));
            var result = query.Select(p => new PermissionDetailViewModel(p));
            facade.DisposeUnitOfWork();
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result.ToDataSourceResult(request));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
