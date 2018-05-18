using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class ReporteProyectosMovilizacionController : Controller
    {
        //
        // GET: /ReporteProyectosMovilizacion/
        public ActionResult Index()
        {
            return View();
        }

        DXMVCWebApplication1.Reports.ReporteProyectosMovilizacion report = new DXMVCWebApplication1.Reports.ReporteProyectosMovilizacion();

        public ActionResult Report3Partial()
        {
            return PartialView("_Report3Partial", report);
        }

        public ActionResult Report3PartialExport()
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        }
	}
}