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
    public class ConfigRecordController : Controller
    {
        IDALFacade facade = new DALFacade();

        // GET: ConfigRecord
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConfigRecords_Read([DataSourceRequest]DataSourceRequest request)
        {
            IUnitOfWork unitOfWork = facade.GetUnitOfWork();
            var query = unitOfWork.ConfigRecordRepo.GetByQuery(p => p.CosmicConfigRecordId != 0, o => o.OrderBy(n => n.CosmicConfigRecordName));
            var result = query.Select(p => p);

            facade.DisposeUnitOfWork();
            return Json(result.ToDataSourceResult(request),JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfigRecord_Optional_Products_Read([DataSourceRequest]DataSourceRequest request, int id)
        {
            IUnitOfWork unitOfWork = facade.GetUnitOfWork();
            var configRecord = unitOfWork.ConfigRecordRepo.GetById(id);
            var result = configRecord.Products.Select(p => new ProductViewModel(p));
            //var result = query.Select(p => p.Products).ToList();
            //var configRecord = dataContext.CosmicConfigRecords.Find(this.GenericConfig.ConfigRecordID);
            //var products = configRecord.Products.Select(p => new ProductSummaryBdo(p));

            foreach (var product in result)
            {
                Console.WriteLine(product.Name);
            }

            facade.DisposeUnitOfWork();
            return Json(result.ToDataSourceResult(request));
        }
    }
}