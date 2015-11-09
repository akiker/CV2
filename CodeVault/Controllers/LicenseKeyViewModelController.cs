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
    public class LicenseKeyViewModelController : Controller
    {
        private readonly Cv2Context _db;
        public LicenseKeyViewModelController()
        {
            _db = new Cv2Context();
        }

        // GET: LicenseKeyViewModel
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> LicenseViewModel_Read([DataSourceRequest] DataSourceRequest request, int id)
        {
            var query = await _db.LicenseKeys.Where(l => l.LicenseId == id).ToListAsync();
            var result = from l in query
                         select new LicenseKeyViewModel
                         {
                             Id = l.LicenseId,
                             KeyData = l.LicenseKeyData,
                             LicenseKeyOwnerPhoneNumber = l.LicenseKeyOwnerPhoneNumber,
                             OwnerEmail = l.LicenseKeyOwnerEmail,
                             OwnerName = l.LicenseKeyOwnerName
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