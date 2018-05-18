using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DXMVCWebApplication1.Common;
using System;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.ViewModels;

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
                       + "," + RolesUsuarios.IE_ACUICOLA_PESQUERA
                       + "," + RolesUsuarios.IE_INOCUIDAD
                       + "," + RolesUsuarios.IE_MOVILIZACION
                       + "," + RolesUsuarios.IE_ANIMAL
                       + "," + RolesUsuarios.IE_VEGETAL
                       + "," + RolesUsuarios.PUESTO_COOR_PROYECTO
                       + "," + RolesUsuarios.PUESTO_GERENTE
                       + "," + RolesUsuarios.SUP_NACIONAL
                       + "," + RolesUsuarios.REPRESENTANTE_ESTATAL
                       + "," + RolesUsuarios.IE_PROGRAMAS_U
                       + "," + RolesUsuarios.SUP_AUDITOR
      )]
    public class ReporteInformeMensualAvanceController : Controller
    {
        DXMVCWebApplication1.Reports.ReporteCierreMes reporte = new Reports.ReporteCierreMes();
        //DXMVCWebApplication1.Reports.ReporteAvanceFisicoFinanciero reporte = new Reports.ReporteAvanceFisicoFinanciero();
        static int PK_IdCampania;
        static int PK_IdMes;
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Index2(ParamsRepInfoFFVM item)
        {
            if (ModelState.IsValid) 
            {
                PK_IdCampania = item.ComboCampania;
                PK_IdMes = item.ComboMes;
                return View();
            }
            else
            {
                ViewData["EditError"] = "Ingrese los valores solicitados";
                return View("Index");
            }
        }
        public ActionResult reporteAvanceFisicoFinanciero()
        {
            reporte.Parameters["parameterCampania"].Value = PK_IdCampania;
            reporte.Parameters["parameterMes"].Value = PK_IdMes;
            reporte.Parameters["parameterCampania"].Visible = false;
            reporte.Parameters["parameterMes"].Visible = false;
            return PartialView("_ReporteAvanceFisicoFinancieroPartial", reporte);
        }

        public ActionResult ReportPartialExport()
        {
            return DocumentViewerExtension.ExportTo(reporte, Request);
        }

        public ActionResult _ComboBoxCampaniaPartial()
        {
            var IdInstanciaEjecutora = Convert.ToInt32(Request.Params["IdInstanciaEjecutora"]);
            return PartialView(Senasica.GetCampaniaByIE(IdInstanciaEjecutora));
        }
    }
}