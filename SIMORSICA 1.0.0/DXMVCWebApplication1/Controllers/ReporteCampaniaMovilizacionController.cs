using System;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize]
    public class ReporteCampaniaMovilizacionController : Controller
    {
        //
        // GET: /ReporteCampaniaMovilizacion/
        public ActionResult Index(string PK_IdCampania)
        {
            ViewData["PK_IdCampania"] = PK_IdCampania;
            return View();
        }

        Reports.ReporteCampaniaMovilizacion report = new Reports.ReporteCampaniaMovilizacion();

        [HttpPost]
        public ActionResult Report1Partial(string PK_IdCampania)
        {
            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            ViewData["PK_IdCampania"] = PK_IdCampania;
            report.Parameters["parameter1"].Value = PK_IdCampania;
            report.Parameters["parameter1"].Visible = false;
            report.Parameters["parameter2"].Visible = false;
            report.Parameters["parameter2"].Value = IdAnio;
            return PartialView("_Report1Partial", report);
        }

        [HttpPost]
        public ActionResult Report1PartialExport()
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        }
    }
}