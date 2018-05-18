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
    public class ReportePersonalInstanciaEjecutoraController : Controller
    {
        //
        // GET: /ReportePersonalInstanciaEjecutora/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult IndexReporte()
        {
            return View("IndexReporte");
        }

        DXMVCWebApplication1.Reports.ReportePersonalDeInstanciaEjecutora report = new DXMVCWebApplication1.Reports.ReportePersonalDeInstanciaEjecutora();

        public ActionResult ReportPartial(string ComboUR)
        {
            ViewData["ComboUR"] = ComboUR;
            string IDUser = FuncionesAuxiliares.GetCurrent_IdUserName();
            int? IdRegion = FuncionesAuxiliares.GetRegionByUserName();
            int? IdEstado = FuncionesAuxiliares.GetCurrent_IdEstado();

            report.Parameters["parameterIdUR"].Value = ComboUR;
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