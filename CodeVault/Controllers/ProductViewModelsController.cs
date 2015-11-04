using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CodeVault.Models;
using CodeVault.Models.BaseTypes;
using CodeVault.ViewModels;

namespace CodeVault.Controllers
{
    public class ProductViewModelsController : Controller
    {
        private readonly Cv2Context _db = new Cv2Context();
        private readonly IDalFacade _facade = new DalFacade(false);

        // GET: ProductViewModels
        public ActionResult Index()
        {
            var unitOfWork = _facade.GetUnitOfWork();
            var query = unitOfWork.ProductRepo.GetByQuery(p => p.ProductStatus != ProductStatus.Canceled,
                o => o.OrderBy(n => n.ProductName));
            var result = query.Select(p => new ProductViewModel(p)).ToList();
            _facade.DisposeUnitOfWork();
            return View(result);
        }

        // GET: ProductViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var unitOfWork = _facade.GetUnitOfWork();
            var query = unitOfWork.ProductRepo.GetByQuery(p => p.ProductId == id, o => o.OrderBy(n => n.ProductName));
            var result = query.Select(p => new ProductViewModel(p)).FirstOrDefault();
            _facade.DisposeUnitOfWork();
            if (result == null)
            {
                return HttpNotFound();
            }

            return View(result);
        }

        // GET: ProductViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,Name,Manufacturer,Description,Version,CreatedOnDate")] ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                _db.ProductViewModels.Add(productViewModel);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productViewModel);
        }

        // GET: ProductViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productViewModel = _db.ProductViewModels.Find(id);
            if (productViewModel == null)
            {
                return HttpNotFound();
            }
            return View(productViewModel);
        }

        // POST: ProductViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,Name,Manufacturer,Description,Version,CreatedOnDate")] ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(productViewModel).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productViewModel);
        }

        // GET: ProductViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productViewModel = _db.ProductViewModels.Find(id);
            if (productViewModel == null)
            {
                return HttpNotFound();
            }
            return View(productViewModel);
        }

        // POST: ProductViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var productViewModel = _db.ProductViewModels.Find(id);
            _db.ProductViewModels.Remove(productViewModel);
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