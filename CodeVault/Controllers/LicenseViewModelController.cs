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
    public class LicenseViewModelController : Controller
    {
        IDALFacade facade = new DALFacade();
        // GET: LicenseViewModel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LicenseViewModel_Read([DataSourceRequest]DataSourceRequest request, int id)
        {
            IUnitOfWork unitOfWork = facade.GetUnitOfWork();
            var query = unitOfWork.LicenseRepo.GetByQuery(p => p.ProductId == id, o => o.OrderBy(n => n.ProductId),"LicenseKeys,LicenseType");
            var result = query.Select(p => new LicenseViewModel(id));
            facade.DisposeUnitOfWork();

            return Json(result.ToDataSourceResult(request));
        }
    }
}