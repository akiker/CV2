using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CodeVault.Models;
using CodeVault.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Net;

namespace CodeVault.Controllers
{
    public class ConfigRecordController : Controller
    {
        private readonly Cv2Context _db;

        public ConfigRecordController()
        {
                _db = new Cv2Context();
        }

        // GET: ConfigRecord
        public async Task<ActionResult> Index()
        {
            var query =
               await _db.CosmicConfigRecords.ToListAsync();
            var result = from p in query
                         select new ConfigRecordViewModel()
                         {
                             Id = p.CosmicConfigRecordId,
                             Name = p.CosmicConfigRecordName,
                             CreatedBy = p.CosmicConfigRecordCreatedBy
                         };
            return View(result);
        }

        public async Task<ActionResult> CosmicConfigRecordViewModel_Read([DataSourceRequest] DataSourceRequest request)
        {
            var query =
               await _db.CosmicConfigRecords.ToListAsync();
            var result = from p in query
                         select new ConfigRecordViewModel()
                         {
                             Id = p.CosmicConfigRecordId,
                             Name = p.CosmicConfigRecordName,
                             CreatedBy = p.CosmicConfigRecordCreatedBy
                         };
            return Json(result.ToDataSourceResult(request),JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> CosmicConfigRecordOptionalSoftware_Read([DataSourceRequest] DataSourceRequest request, int id)
        {
            var configRecord = await _db.CosmicConfigRecords.FirstOrDefaultAsync(c => c.CosmicConfigRecordId == id);
            var query = configRecord.Products.OrderBy(p => p.ProductName).ToList();
            var result = from p in query
                         select new ProductViewModel()
                         {
                             Id = p.ProductId,
                             Name = p.ProductName,
                             Manufacturer = p.ProductManufacturer,
                             Version = p.ProductVersion,
                             Description = p.ProductDescription,
                             Status = p.ProductStatus.ToString(),
                             CreatedOnDate = p.CreatedOnDate
                         };
            return Json(result.ToDataSourceResult(request),JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int? id)
        {
            var query = _db.CosmicConfigRecords.FirstOrDefault(c => c.CosmicConfigRecordId == id);
            var result = new ConfigRecordViewModel()
            {
                Id = query.CosmicConfigRecordId,
                Name = query.CosmicConfigRecordName,
                CreatedBy = query.CosmicConfigRecordCreatedBy
            };
            ViewBag.CurrentCosmicConfigRecordId = id;
            return View(result);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}