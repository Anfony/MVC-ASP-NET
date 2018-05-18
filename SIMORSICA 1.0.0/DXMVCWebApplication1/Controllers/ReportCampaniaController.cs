using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class ReportCampaniaController : Controller
    {
        
         //GET: /ReportCampania/
        public ActionResult Index()
        {
            return View();
        }

        DXMVCWebApplication1.Reports.ReportCampania report = new DXMVCWebApplication1.Reports.ReportCampania();

        public ActionResult CampaniaReportPartial()
        {
            return PartialView("_CampaniaReportPartial", report);
        }

        public ActionResult CampaniaReportPartialExport()
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        }
	}
}