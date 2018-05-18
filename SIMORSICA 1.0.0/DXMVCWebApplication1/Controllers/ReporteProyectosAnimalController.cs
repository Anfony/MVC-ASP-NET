using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class ReporteProyectosAnimalController : Controller
    {
        //
        // GET: /ReporteProyectosAnimal/
        public ActionResult Index()
        {
            return View();
        }

        DXMVCWebApplication1.Reports.ReporteProyectosAnimal report = new DXMVCWebApplication1.Reports.ReporteProyectosAnimal();

        public ActionResult Report1Partial()
        {
            return PartialView("_Report1Partial", report);
        }

        public ActionResult Report1PartialExport()
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        }
	}
}