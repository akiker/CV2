using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeVault.Models;
using System.Threading.Tasks;
using CodeVault.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace CodeVault.Controllers
{
    public class SoftwarePolicyGroupAssociationViewModelController : Controller
    {
        private readonly Cv2Context _db;

        public SoftwarePolicyGroupAssociationViewModelController()
        {
                _db = new Cv2Context();
        }
        // GET: SoftwarePolicyGroupAssociationViewModel
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> SoftwarePolicyGroupAssociationViewModel_Read([DataSourceRequest] DataSourceRequest request, int id)
        {
            var query =
               await _db.SoftwarePolicyGroupAssociations.Where(p => p.ProductId == id).ToListAsync();

            var result = from s in query
                         select new SoftwarePolicyGroupAssociationViewModel()
                         {
                             Id = s.ProductId,
                             GroupName = s.SoftwarePolicyAdGroupName,
                             DisplayName = s.SoftwarePolicyGroupDisplayName,
                             Description = s.SoftwarePolicyGroupDescription,
                             SoftwarePolicyId = s.SoftwarePolicyGroupAssociationId,
                             SupportLevel = s.SoftwarePolicySupportLevel.SoftwarePolicySupportLevelName
                         };

            return Json(result.ToDataSourceResult(request));
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}