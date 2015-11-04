using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CodeVault.Models;
using CodeVault.Models.BaseTypes;
using CodeVault.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace CodeVault.Controllers
{
    public class PermissionDetailsViewModelsController : Controller
    {
        private readonly Cv2Context _db;

        public PermissionDetailsViewModelsController()
        {
               _db = new Cv2Context();
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> PermissionDetailViewModels_Read([DataSourceRequest] DataSourceRequest request, int id)
        {
            var query = await _db.ProductPermissionDetails.Where(p => p.ProductId == id).OrderBy(p => p.ProductId).ToListAsync();

            var result = from p in query
                         select new PermissionDetailViewModel()
                         {
                             Id = p.ProductId,
                             GroupUser = p.ProductPermissionGroupOrUserName,
                             Location = p.ProductPermissionLocation,
                             Permission = p.ProductPermissionDetailAcl.ToString(),
                             Type = p.ProductPermissionDetailType.ToString()
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