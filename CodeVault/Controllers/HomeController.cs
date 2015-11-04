using System.Collections.Generic;
using System.Web.Mvc;

namespace CodeVault.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetHowtoTopics()
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem() {Value = "0" , Text = "Please Select..."},
                new SelectListItem {Value = "1", Text = "Request Software"},
                new SelectListItem {Value = "2", Text = "Request Configuration Record"}
            };
            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}