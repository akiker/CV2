using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CodeVault.Models;
using CodeVault.Models.BaseTypes;
using CodeVault.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace CodeVault.Controllers
{
    public class PermissionViewModelsController : Controller
    {
        private readonly Cv2Context _db = new Cv2Context();
        private readonly IDalFacade _facade = new DalFacade(false);

        // GET: PermissionViewModels
        public ActionResult Index()
        {
            var unitOfWork = _facade.GetUnitOfWork();
            var query = unitOfWork.ProductRepo.GetByQuery(p => p.ProductStatus != ProductStatus.Canceled,
                o => o.OrderBy(n => n.ProductName));
            var result = query.Select(p => new PermissionViewModel(p)).ToList();
            _facade.DisposeUnitOfWork();
            return View(result);
        }

        public ActionResult PermissionViewModel_Read([DataSourceRequest] DataSourceRequest request)
        {
            var unitOfWork = _facade.GetUnitOfWork();
            var query = unitOfWork.ProductRepo.GetByQuery(p => p != null, o => o.OrderBy(n => n.ProductName));
            var result = query.Select(p => new PermissionViewModel(p));
            _facade.DisposeUnitOfWork();

            return Json(result.ToDataSourceResult(request));
        }

        // GET: PermissionViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var unitOfWork = _facade.GetUnitOfWork();
            var query = unitOfWork.ProductRepo.GetByQuery(p => p.ProductId == id, o => o.OrderBy(n => n.ProductName));
            var result = query.Select(p => new PermissionViewModel(p)).FirstOrDefault();
            var detail = result.PermissionDetails.ToList();
            _facade.DisposeUnitOfWork();
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        // GET: PermissionViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PermissionViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(
                Include =
                    "Id,ElevatedRightsRequired,RequiresAdminRightsBasic,RequiresAdminRightsAdvanced,RequiresAdminRightsUpdate,LaVerified,WorksWithLa,DoesNotWorkWithLa"
                )] PermissionViewModel permissionViewModel)
        {
            if (ModelState.IsValid)
            {
                _db.PermissionViewModels.Add(permissionViewModel);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(permissionViewModel);
        }

        // GET: PermissionViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            var unitOfWork = _facade.GetUnitOfWork();
            var query = unitOfWork.ProductRepo.GetByQuery(p => p.ProductId == id, o => o.OrderBy(n => n.ProductName));
            var result = query.Select(p => new PermissionViewModel(p)).FirstOrDefault();
            _facade.DisposeUnitOfWork();
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: PermissionViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(
                Include =
                    "Id,ElevatedRightsRequired,RequiresAdminRightsBasic,RequiresAdminRightsAdvanced,RequiresAdminRightsUpdate,LaVerified,WorksWithLa,DoesNotWorkWithLa"
                )] PermissionViewModel permissionViewModel)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(permissionViewModel).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(permissionViewModel);
        }

        // GET: PermissionViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var permissionViewModel = _db.PermissionViewModels.Find(id);
            if (permissionViewModel == null)
            {
                return HttpNotFound();
            }
            return View(permissionViewModel);
        }

        // POST: PermissionViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var permissionViewModel = _db.PermissionViewModels.Find(id);
            _db.PermissionViewModels.Remove(permissionViewModel);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}