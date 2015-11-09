using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CodeVault.Models;
using CodeVault.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace CodeVault.Controllers
{
    public class LicenseViewModelController : Controller
    {
        private readonly Cv2Context _db;
        public LicenseViewModelController()
        {
            _db = new Cv2Context();
        }

        // GET: LicenseViewModel
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> LicenseViewModel_Read([DataSourceRequest] DataSourceRequest request, int id)
        {
            var query = await _db.Licenses.Where(p => p.ProductId == id).ToListAsync();
            var result = from l in query
                         select new LicenseViewModel
                         {
                             Id = l.ProductId,
                             LicenseType = l.LicenseType.LicenseTypeName,
                             Notes = l.LicenseNotes,
                             Owner = l.LicenseOwner,
                             Sku = l.LicenseSku
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