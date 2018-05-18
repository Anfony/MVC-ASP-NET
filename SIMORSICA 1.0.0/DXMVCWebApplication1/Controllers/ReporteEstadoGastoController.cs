using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;


namespace DXMVCWebApplication1.Controllers
{
    public class ReporteEstadoGastoController : Controller
    {
        //
        // GET: /ReporteEstadoGasto/
        public ActionResult Index()
        {
            return View();
        }
        DXMVCWebApplication1.Reports.ReporteEstadoDelGasto report = new DXMVCWebApplication1.Reports.ReporteEstadoDelGasto();
        public ActionResult ReporteEstadoGasto()
        {
            return PartialView("ReporteEstadoGasto", report);
        }

        public ActionResult ReportPartialExport()
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        }
    }
}