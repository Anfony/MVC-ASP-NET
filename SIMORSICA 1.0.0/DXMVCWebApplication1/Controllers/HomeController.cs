using System.Web.Mvc;
using DXMVCWebApplication1.Common;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Construccion/
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        public ActionResult AsignaAnioPresupuestal(int IdAnio, string Controlador, string Accion)
        {
            Session["IdAnio"] = IdAnio;
            return RedirectToAction(Accion, Controlador);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult DocumentViewerPartial()
        {
            return PartialView("DocumentViewerPartial");
        }
    }
}