using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class ReporteCampaniaESTHistoricoController : Controller
    {
        //
        // GET: /ReporteCampaniaESTHistorico/
        public ActionResult Index(string PK_IdCampania)
        {
            ViewData["PK_IdCampania"] = PK_IdCampania;
            return View();
        }

        DXMVCWebApplication1.Reports.ReporteCampaniaESTHistorico report = new DXMVCWebApplication1.Reports.ReporteCampaniaESTHistorico();

        [HttpPost]
        public ActionResult ReportPartial(string PK_IdCampania, int? Consecutivo_Repro)
        {
            ViewData["Consecutivo_Repro"] = Consecutivo_Repro;

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);

            ViewData["PK_IdCampania"] = PK_IdCampania;
            report.Parameters["parameter1"].Value = PK_IdCampania;
            report.Parameters["parameter1"].Visible = false;
            report.Parameters["parameter2"].Visible = false;
            report.Parameters["parameter2"].Value = IdAnio;
            report.Parameters["parameter3"].Visible = false;
            report.Parameters["parameter3"].Value = Consecutivo_Repro;
            return PartialView("_ReportPartial", report);
        }

        [HttpPost]
        public ActionResult ReportPartialExport(int Consecutivo_Repro)
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        }
	}
}