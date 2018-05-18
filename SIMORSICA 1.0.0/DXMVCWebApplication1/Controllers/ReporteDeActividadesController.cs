using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using DevExpress.Web.Mvc;
using DXMVCWebApplication1.Common;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                           + "," + RolesUsuarios.PUESTO_COOR_REGIONAL                        
     )]
    public class ReporteDeActividadesController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        public ActionResult Index(int? IdActividad)
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region CodigoDeReporte
        Reports.ReporteDeActividades report = new Reports.ReporteDeActividades();

        public ActionResult ActividadesReportPartial(int? IdActividad)
        {
            if (IdActividad == null)
            {
                IdActividad = 1008; // default parameter value

            }

            report.FilterString = "Pk_IdActividad = " + IdActividad.ToString();


            return PartialView("_ActividadesReportPartial", report);
        }

        public ActionResult ActividadesReportPartialExport(int? IdActividad)
        {
            if (IdActividad == null)
            {
                IdActividad = 1008; // default parameter value

            }

            report.FilterString = "Pk_IdActividad = " + IdActividad.ToString();



            return DocumentViewerExtension.ExportTo(report, Request);
        } 
        #endregion
    }
}
