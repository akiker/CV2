using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CodeVault.Models;
using CodeVault.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using DependencyType = CodeVault.Models.DependencyType;

namespace CodeVault.Controllers
{
    public class DependenciesController : Controller
    {
        private readonly Cv2Context _db;

        public DependenciesController()
        {
                _db = new Cv2Context();
        }

        // GET: Dependencies
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> PreInstallDependencyViewModel_Read([DataSourceRequest] DataSourceRequest request, int id)
        {
            var query = await _db.Dependencies.Where(d => d.BaseProductId == id && d.DependencyType == DependencyType.PreInstall).OrderBy(d => d.InstallOrder).ToListAsync();
            var result = from d in query
                select new DependencyViewModel
                {
                    Id = d.BaseProductId,
                    Version = d.Dependency.ProductVersion,
                    Name = d.Dependency.ProductName,
                    InstallOrder = d.InstallOrder
                };
            return Json(result.ToDataSourceResult(request));
        }

        public async Task<ActionResult> PostInstallDependencyViewModel_Read([DataSourceRequest] DataSourceRequest request, int id)
        {
            var query = await _db.Dependencies.Where(d => d.BaseProductId == id && d.DependencyType == DependencyType.PostInstall).OrderBy(d => d.InstallOrder).ToListAsync();
            var result = from d in query
                         select new DependencyViewModel
                         {
                             Id = d.BaseProductId,
                             Version = d.Dependency.ProductVersion,
                             Name = d.Dependency.ProductName,
                             InstallOrder = d.InstallOrder
                         };
            return Json(result.ToDataSourceResult(request));
        }
    }
}