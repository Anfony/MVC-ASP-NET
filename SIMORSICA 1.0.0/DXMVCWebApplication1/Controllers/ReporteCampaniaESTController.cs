using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class ReporteCampaniaESTController : Controller
    {
        //
        // GET: /ReporteCampaniaEST/
        public ActionResult Index(string PK_IdCampania)
        {
            ViewData["PK_IdCampania"] = PK_IdCampania; ViewData["PK_IdCampania"] = PK_IdCampania;
            return View();
        }

        Reports.ReporteCampaniaEST report = new Reports.ReporteCampaniaEST();

        [HttpPost]
        public ActionResult ReportPartial(string PK_IdCampania)
        {
            int IdAnio = Convert.ToInt32(Session["IdAnio"]);

            ViewData["PK_IdCampania"] = PK_IdCampania;
            report.Parameters["parameter1"].Value = PK_IdCampania;
            report.Parameters["parameter1"].Visible = false;
            report.Parameters["parameter2"].Visible = false;
            report.Parameters["parameter2"].Value = IdAnio;
            return PartialView("_ReportPartial", report);
        }

         [HttpPost]
        public ActionResult ReportPartialExport()
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        }
	}
}