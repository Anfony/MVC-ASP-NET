using System.Linq;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                       + "," + RolesUsuarios.UR_INOCUIDAD
                       + "," + RolesUsuarios.UR_MOVILIZACION
                       + "," + RolesUsuarios.UR_ANIMAL
                       + "," + RolesUsuarios.UR_ACUICOLA_PESQUERA
                       + "," + RolesUsuarios.UR_VEGETAL 
                       + "," + RolesUsuarios.SUP_ESTATAL
                       + "," + RolesUsuarios.SUP_REGIONAL
                       + "," + RolesUsuarios.SUP_NACIONAL
                       + "," + RolesUsuarios.REPRESENTANTE_ESTATAL
      )]
    public class DetalleApendiceController : Controller
    {
        DB_SENASICAEntities db = new DB_SENASICAEntities();
        Reports.ReporteApendice reporte = new Reports.ReporteApendice();

        // GET: /DetalleApendice/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult IndexReporte()
        {

            return View("IndexReporte");
        }

        public ActionResult reporteDetalleApendice(string ComboEstado)
        {
            int IdEstado = db.Estadoes.Where(it => it.Nombre == ComboEstado).Select(it => it.Pk_IdEstado).First();
            int IdAnio = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
                
            ViewData["ComboEstado"] = ComboEstado;
            reporte.Parameters["parameterEstado"].Visible = false;
            reporte.Parameters["parameterEstado"].Value = IdEstado;
            reporte.Parameters["parameterAnio"].Visible = false;
            reporte.Parameters["parameterAnio"].Value = IdAnio;

            return PartialView("_ReporteDetalleApendicePartial", reporte);
        }

        public ActionResult ReportPartialExport(string ComboEstado)
        {
            int IdEstado = db.Estadoes.Where(it => it.Nombre == ComboEstado).Select(it => it.Pk_IdEstado).First();

            return DocumentViewerExtension.ExportTo(reporte, Request);
        }
    }
}