using DevExpress.Web.Mvc;
using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.ViewModels;
using System;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize]
    public class ReporteInformeDetalleFisicoFinancieroController : Controller
    {
        // GET: ReporteInformeDetalleFisicoFinanciero

        DXMVCWebApplication1.Reports.ReporteDetalleAvanceFisicoXMes reporteDetalleFisico = new Reports.ReporteDetalleAvanceFisicoXMes();
        DXMVCWebApplication1.Reports.ReporteDetalleAvanceFinancieroXMes reporteDetalleFinanciero = new Reports.ReporteDetalleAvanceFinancieroXMes();

        static int PK_IdCampania;
        static int PK_IdMes;
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult IndexFF(ParamsRepInfoFFVM item)
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
        public ActionResult reporteAvanceFisicoFinanciero(string ComboReport)
        {
            ViewData["ComboReport"] = ComboReport;
            if (ComboReport == ConstantesGlobales.DETALLE_FISICO)
            {
                reporteDetalleFisico.Parameters["Campania"].Value = PK_IdCampania;
                reporteDetalleFisico.Parameters["Mes"].Value = PK_IdMes;
                reporteDetalleFisico.Parameters["Campania"].Visible = false;
                reporteDetalleFisico.Parameters["Mes"].Visible = false;
                return PartialView("_ReporteInformeDetalleFisicoFinancieroPartial", reporteDetalleFisico);
            }
            
            if (ComboReport == ConstantesGlobales.DETALLE_FINANCIERO)
            {
                reporteDetalleFinanciero.Parameters["Campania"].Value = PK_IdCampania;
                reporteDetalleFinanciero.Parameters["Mes"].Value = PK_IdMes;
                reporteDetalleFinanciero.Parameters["Campania"].Visible = false;
                reporteDetalleFinanciero.Parameters["Mes"].Visible = false;
                return PartialView("_ReporteInformeDetalleFisicoFinancieroPartial", reporteDetalleFinanciero);
            }
            return null;
        }

        public ActionResult ReportPartialExport(string ComboReport)
        {
            if (ComboReport == ConstantesGlobales.DETALLE_FISICO)
            {
                return DocumentViewerExtension.ExportTo(reporteDetalleFisico, Request);
            }
            if (ComboReport == ConstantesGlobales.DETALLE_FINANCIERO)
            {
                return DocumentViewerExtension.ExportTo(reporteDetalleFinanciero, Request);
            }
            return null;
        }

        public ActionResult _ComboBoxCampaniaPartial()
        {
            var IdInstanciaEjecutora = Convert.ToInt32(Request.Params["IdInstanciaEjecutora"]);
            return PartialView(Senasica.GetCampaniaByIE(IdInstanciaEjecutora));
        }
    }
}