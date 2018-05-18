using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DXMVCWebApplication1.Common;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO)]
    public class ReporteAnexoTecnico3_NacionalController : Controller
    {
        DXMVCWebApplication1.Reports.ReporteAnexoTecnicoApendice3_Nacional reporte = new Reports.ReporteAnexoTecnicoApendice3_Nacional();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult reporteAnexoTecnicoApendice3N()
        {
            int IdAnio = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            reporte.Parameters["parameterAnio"].Visible = false;
            reporte.Parameters["parameterAnio"].Value = IdAnio;
            return PartialView("_ReporteAnexoTecnicoApendice3N_Partial", reporte);
        }

        public ActionResult ReportPartialExport()
        {
            return DocumentViewerExtension.ExportTo(reporte, Request);
        }
    }
}