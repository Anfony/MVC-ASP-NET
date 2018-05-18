using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class ReporteEjecutivoController : Controller
    {
        //
        // GET: /ReporteEjecutivo/
        public ActionResult Index()
        {
            return View();
        }

        DXMVCWebApplication1.Reports.ReporteEjecutivo report = new DXMVCWebApplication1.Reports.ReporteEjecutivo();

        public ActionResult ReportPartial()
        {
            return PartialView("_ReportPartial", report);
        }

        public ActionResult ReportPartialExport()
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        }
	}
}