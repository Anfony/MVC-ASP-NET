using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using System.Linq;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO)]
    public class CierreMensualSolAperturaController : Controller
    {
        // GET: CierreMensualSolApertura
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult CierreMensualGridViewPartial()
        {
            return PartialView("_CierreMensualSolicitudApertura", Senasica.GetCierreMensualSolAperturaUCE());
        }

        [ValidateInput(false)]
        public ActionResult HistorialSolicitudesGridViewPartial(int IdCierreMensual)
        {
            ViewData["IdCierreMensual"] = IdCierreMensual;

            return PartialView("_HistorialSolicitudesGridViewPartial", Senasica.GetHistorialSolicitudesCierreMensual(IdCierreMensual));
        }

        /// <summary>
        /// Abre una ventana de confirmación con lo cual el usuario decidirá la aperura o no de la campaña
        /// </summary>
        /// <param name="IdCierreMensual"></param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public ActionResult ValidaCampaniaApertura(int IdCierreMensual)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            var _data = db.CierreMensuals.Where(item => item.Pk_IdCierreMensual == IdCierreMensual).First();

            return View("_AceptaSolApertura", _data);
        }

        /// <summary>
        /// El usuario a atendido la solicitud de apertura de la campaña
        /// 
        /// El usuario describe los motivos de su decisión y el sistema abre o no la campaña
        /// </summary>
        /// <param name="IdCierreMensual"></param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public ActionResult AperturaCampania(string RespuestaApertura, int IdCierreMensual, System.DateTime? FechaCierreAutomatico, bool esApertura)
        {
            DB_SENASICA_Storeds storeds = new DB_SENASICA_Storeds();

            storeds.SP_AperturaCampania(IdCierreMensual, RespuestaApertura, FechaCierreAutomatico, esApertura);
            storeds.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}