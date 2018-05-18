using System;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize]
    public class ReporteCampaniaInocuidadSACController : Controller
    {
        //
        // GET: /ReporteCampaniaInocuidadSAC/
        public ActionResult Index(string PK_IdCampania)
        {
            ViewData["PK_IdCampania"] = PK_IdCampania;
            return View();
        }

        Reports.ReporteCampaniaInocuidadSAC report = new Reports.ReporteCampaniaInocuidadSAC();

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