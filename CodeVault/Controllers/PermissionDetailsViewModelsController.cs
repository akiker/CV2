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
            base.Dispose(disposing);
        }
    }
}
