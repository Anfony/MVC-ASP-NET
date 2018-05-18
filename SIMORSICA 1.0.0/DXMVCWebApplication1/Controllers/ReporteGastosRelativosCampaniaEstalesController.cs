using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                       + "," + RolesUsuarios.IE_VEGETAL
                       + "," + RolesUsuarios.IE_ACUICOLA_PESQUERA
                       + "," + RolesUsuarios.IE_ANIMAL
                       + "," + RolesUsuarios.IE_MOVILIZACION
                       + "," + RolesUsuarios.SUP_ESTATAL
                       + "," + RolesUsuarios.SUP_REGIONAL
                       + "," + RolesUsuarios.SUP_NACIONAL
                       + "," + RolesUsuarios.REPRESENTANTE_ESTATAL
      )]

    public class ReporteGastosRelativosCampaniaEstalesController : Controller
    {
        DB_SENASICAEntities db = new DB_SENASICAEntities();
        //
        // GET: /ReporteGastosRelativosCampaniaEstales/
        public ActionResult Index()
        {
            int? _Fk_IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            ViewData["_Fk_IdEstado"] = _Fk_IdEstado;
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            ViewData["_Fk_IdUnidadEjecutora"] = _Fk_IdUnidadEjecutora;
            string rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            ViewData["rolusuario"] = rolusuario;
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult IndexReporte()
        {
            return View("IndexReporte");
        }
        DXMVCWebApplication1.Reports.ReporteGastosRelativosCampaniaEstatales report = new DXMVCWebApplication1.Reports.ReporteGastosRelativosCampaniaEstatales();

        public ActionResult ReportPartial(string ComboComite)
        {
            int Id_UnidadEjecutora = db.UnidadEjecutoras.Where(it => it.Nombre == ComboComite).Select(it => it.Pk_IdUnidadEjecutora).First();
            ViewData["ComboComite"] = ComboComite;
            int IdAnio = Convert.ToInt32(Session["IdAnio"]);

            report.Parameters["parameter1"].Visible = false;
            report.Parameters["parameter2"].Visible = false;
            report.Parameters["parameter1"].Value = Id_UnidadEjecutora;
            report.Parameters["parameter2"].Value = IdAnio;
            return PartialView("_ReportPartial", report);
        }

        public ActionResult ReportPartialExport()
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        }
	}
}