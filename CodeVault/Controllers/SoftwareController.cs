using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using CodeVault.Models;
using CodeVault.Models.BaseTypes;
using CodeVault.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using DependencyType = CodeVault.Models.DependencyType;

namespace CodeVault.Controllers
{
    public class SoftwareController : Controller
    {
        private readonly Cv2Context _db;
        private readonly IDalFacade _facade = new DalFacade(false);

        public SoftwareController()
        {
            _db = new Cv2Context();
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ProductViewModels_Read([DataSourceRequest] DataSourceRequest request)
        {
            //This is fast!!! Finally
            var query = await _db.Products.Where(p => p.ProductStatus != ProductStatus.Canceled).ToListAsync();
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
            return Json(result.ToDataSourceResult(request));
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product  = await _db.Products.Where(p => p.ProductId == id).Include(p => p.SoftwarePolicy).Include(p => p.ProductCategory).Include(p => p.ProductType).Include(p => p.LocalAccountVerification).Include(p => p.ProductsPermissions).FirstOrDefaultAsync();
            if (product == null)
            {
                return HttpNotFound("Unable to retrieve the selected product");
            }
            var productViewModel = new ProductViewModel(product);

            if (ContainsInvalid(productViewModel.Version))
            {
                ViewBag.DetailTitle = productViewModel.Name;
            }
            else if (productViewModel.Name.Contains(productViewModel.Version))
            {
                ViewBag.DetailTitle = productViewModel.Name;
            }
            else
            {
                ViewBag.DetailTitle = $"{productViewModel.Name} {productViewModel.Version}";
            }
            return View(productViewModel);
        }

        private static bool ContainsInvalid(string value)
        {
            var invalid = new List<string> {"na", "O", "0", "n/a"};
            return invalid.Contains(value.ToLower().Trim());
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            
            base.Dispose(disposing);
        }
    }
}