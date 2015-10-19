using CodeVault.Models;
using CodeVault.Models.BaseTypes;
using CodeVault.Models.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeVault.Controllers
{
    public class DependenciesController : Controller
    {
        private CV2Context db = new CV2Context();
        IDALFacade facade = new DALFacade();

        // GET: Dependencies
        public ActionResult Index()
        {
            return View();
        }

        // GET: Dependencies/Details/5
        public ActionResult Details(int id)
        {
            IUnitOfWork unitOfWork = facade.GetUnitOfWork();
            var query = unitOfWork.ProductRepo.GetByQuery(p => p.ProductId == id, o => o.OrderBy(n => n.ProductName));
            var result = query.Select(p => p.Dependencies);
            facade.DisposeUnitOfWork();

            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        public ActionResult Dependencies_Read([DataSourceRequest]DataSourceRequest request)
        {
            IUnitOfWork unitOfWork = facade.GetUnitOfWork();
            var query = unitOfWork.ProductRepo.GetByQuery(p => p != null, o => o.OrderBy(n => n.ProductName));
            var result = query.Select(p => p.Dependencies);
            facade.DisposeUnitOfWork();

            return Json(result.ToDataSourceResult(request));
        }
    }
}
