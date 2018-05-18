using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class ReporteProyectosAcuicolaController : Controller
    {
        //
        // GET: /ReporteProyectosAcuicola/
        public ActionResult Index()
        {
            return View();
        }

        DXMVCWebApplication1.Reports.ReporteProyectosAcuicola report = new DXMVCWebApplication1.Reports.ReporteProyectosAcuicola();

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