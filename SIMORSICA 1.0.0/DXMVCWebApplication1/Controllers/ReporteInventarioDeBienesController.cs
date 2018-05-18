using System.Linq;
using System;
using System.Collections.Generic;
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
                       + "," + RolesUsuarios.SUP_NACIONAL
                       + "," + RolesUsuarios.REPRESENTANTE_ESTATAL
                       + "," + RolesUsuarios.SUP_REGIONAL
                       + "," + RolesUsuarios.SUP_ESTATAL
      )]
    public class ReporteInventarioDeBienesController : Controller
    {
        DB_SENASICAEntities db = new DB_SENASICAEntities();
        //
        // GET: /ReporteInventarioDeBienes/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult IndexReporte()
        {
            return View("IndexReporte");
        }

        Reports.ReporteInventarioDeBienes report = new Reports.ReporteInventarioDeBienes();

        public ActionResult ReportPartial(string ComboUR)
        {
            int IdUnidadResponsable = db.UnidadResponsables.Where(it => it.Nombre == ComboUR).Select(it => it.Pk_IdUnidadResponsable).First();
            ViewData["ComboUR"] = ComboUR;
            string IDUser = FuncionesAuxiliares.GetCurrent_IdUserName();
            int? IdRegion = FuncionesAuxiliares.GetRegionByUserName();
            int? IdEstado = FuncionesAuxiliares.GetCurrent_IdEstado();

            report.Parameters["parameterIdUR"].Value = IdUnidadResponsable;
            report.Parameters["parameterIdUser"].Value = IDUser;
            report.Parameters["parameterIdRegion"].Value = IdRegion;
            report.Parameters["parameterIdEstado"].Value = IdEstado;
            report.Parameters["parameterIdUser"].Visible = false;
            report.Parameters["parameterIdRegion"].Visible = false;
            report.Parameters["parameterIdEstado"].Visible = false;
            report.Parameters["parameterIdUR"].Visible = false;
            return PartialView("_ReportPartial", report);
        }

        public ActionResult ReportPartialExport(string ComboUR)
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        }
	}
}