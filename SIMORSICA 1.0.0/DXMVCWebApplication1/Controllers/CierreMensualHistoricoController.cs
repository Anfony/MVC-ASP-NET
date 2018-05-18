using DXMVCWebApplication1.Models;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class CierreMensualHistoricoController : Controller
    {
        // GET: CierreMensualHistorico
        public ActionResult Index(int IdCierreMensual, string controlador)
        {
            ViewData["IdCierreMensual"] = IdCierreMensual;
            ViewData["controlador"] = controlador;

            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CierreMensualHistGridViewPartialGeneral(int IdCierreMensual, string controlador)
        {
            ViewData["IdCierreMensual"] = IdCierreMensual;
            ViewData["controlador"] = controlador;

            return PartialView("_CierreMensualHistGridViewPartial", Senasica.GetCierreMensualHist(IdCierreMensual));
        }
    }
}