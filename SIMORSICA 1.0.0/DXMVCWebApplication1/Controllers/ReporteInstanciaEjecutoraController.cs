using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class ReporteInstanciaEjecutoraController : Controller
    {
        //
        // GET: /ReporteInstanciaEjecutora/
        public ActionResult Index(string PK_IdUnidadEjecutora)
        {

            ViewData["PK_IdUnidadEjecutora"] = PK_IdUnidadEjecutora;
            return View();
        }

        Reports.ReporteInstanciaEjecutora report = new Reports.ReporteInstanciaEjecutora();

        public ActionResult ReportPartial(string PK_IdUnidadEjecutora)
        {
            ViewData["PK_IdUnidadEjecutora"] = PK_IdUnidadEjecutora;
            report.Parameters["parameter1"].Value = PK_IdUnidadEjecutora;
            report.Parameters["parameter1"].Visible = false;
            return PartialView("_ReportPartial", report);
        }

        public ActionResult ReportPartialExport()
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        }
	}
}