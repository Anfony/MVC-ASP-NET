using DevExpress.Web.Mvc;
using System;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize]
    public class ReporteProgramaTrabajo_GeneralController : Controller
    {
        //
        // GET: /ReporteProgramaTrabajo_General/
        public ActionResult Index(string PK_IdCampania)
        {
            ViewData["PK_IdCampania"] = PK_IdCampania;
            return View();
        }

        Reports.ReporteProgramaTrabajo_General report = new Reports.ReporteProgramaTrabajo_General();

        [HttpPost]
        public ActionResult ReportPartial(string PK_IdCampania)
        {
            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            ViewData["PK_IdCampania"] = PK_IdCampania;
            report.Parameters["pmtPk_IdCampania"].Visible = false;
            report.Parameters["pmtPk_IdCampania"].Value = PK_IdCampania;

            return PartialView("_ReportPartial", report);
        }

        [HttpPost]
        public ActionResult ReportPartialExport()
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        }
	}
}