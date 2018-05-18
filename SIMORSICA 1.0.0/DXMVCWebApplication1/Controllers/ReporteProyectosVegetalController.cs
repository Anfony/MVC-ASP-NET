using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class ReporteProyectosVegetalController : Controller
    {
        //
        // GET: /ReporteProyectosVegetal/
        public ActionResult Index()
        {
            return View();
        }

        DXMVCWebApplication1.Reports.ReporteProyectosVegetal report = new DXMVCWebApplication1.Reports.ReporteProyectosVegetal();

        public ActionResult Report4Partial()
        {
            return PartialView("_Report4Partial", report);
        }

        public ActionResult Report4PartialExport()
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        }
	}
}