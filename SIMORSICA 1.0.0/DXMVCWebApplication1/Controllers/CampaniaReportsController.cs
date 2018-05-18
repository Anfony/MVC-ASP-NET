using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class CampaniaReportsController : Controller
    {
        //
        // GET: /CampaniaReports/
        public ActionResult Index()
        {
            return View();
        }

        DXMVCWebApplication1.Reports.CampaniaReports report = new DXMVCWebApplication1.Reports.CampaniaReports();

        public ActionResult CampaniaReportsReportPartial()
        {
            return PartialView("_CampaniaReportsReportPartial", report);
        }

        public ActionResult CampaniaReportsReportPartialExport()
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        }
	}
}