using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class ReportUEController : Controller
    {
        //
        // GET: /ReportUE/
        public ActionResult Index()
        {
            return View();
        }

        DXMVCWebApplication1.Reports.ReportUE report = new DXMVCWebApplication1.Reports.ReportUE();

        public ActionResult Report2Partial()
        {
            return PartialView("_Report2Partial", report);
        }

        public ActionResult Report2PartialExport()
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        }
	}
}