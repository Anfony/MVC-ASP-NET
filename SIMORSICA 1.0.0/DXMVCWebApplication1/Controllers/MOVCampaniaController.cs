using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DXMVCWebApplication1.Common;


namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                         + "," + RolesUsuarios.IE_MOVILIZACION
                         + "," + RolesUsuarios.UR_MOVILIZACION
                         + "," + RolesUsuarios.IE_VEGETAL
                         + "," + RolesUsuarios.IE_ACUICOLA_PESQUERA
                         + "," + RolesUsuarios.IE_ANIMAL
                         + "," + RolesUsuarios.COORDINADOR_CAMPANIA
                         + "," + RolesUsuarios.PUESTO_COOR_PROYECTO
                         + "," + RolesUsuarios.PUESTO_COOR_REGIONAL
                         + "," + RolesUsuarios.PUESTO_GERENTE
                         + "," + RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                         + "," + RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                         + "," + RolesUsuarios.SUP_ESTATAL
                         + "," + RolesUsuarios.SUP_REGIONAL
    )]
    public class MOVCampaniaController : Controller
    {
        //
        // GET: /MOVCampania/
        #region CodigoDeReporte
        DXMVCWebApplication1.Reports.MOVCampania report = new DXMVCWebApplication1.Reports.MOVCampania();

        [HttpPost]
        public ActionResult Index(string PK_IdCampania)
        {
            ViewData["PK_IdCampania"] = PK_IdCampania;
            return View();
        }

        [HttpPost]
        public ActionResult MOVCampaniaReport3Partial(string PK_IdCampania)
        {
            ViewData["PK_IdCampania"] = PK_IdCampania;
            report.Parameters["parameter1"].Value = PK_IdCampania;
            report.Parameters["parameter1"].Visible = false;
            return PartialView("_MOVCampaniaReport3Partial", report);
        }

        [HttpPost]
        public ActionResult MOVCampaniaReport3PartialExport()
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        } 
        #endregion
	}
}