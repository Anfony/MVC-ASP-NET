using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class ReporteCampaniaInocuidadSanidadAcuicolaPHistoricoController : Controller
    {
        //
        // GET: /ReporteCampaniaInocuidadSanidadAcuicolaPHistorico/
        public ActionResult Index(string PK_IdCampania)
        {
            ViewData["PK_IdCampania"] = PK_IdCampania;
            return View();
        }

        DXMVCWebApplication1.Reports.ReporteCampaniaInocuidadSanidadAcuicolaPHistorico report = new DXMVCWebApplication1.Reports.ReporteCampaniaInocuidadSanidadAcuicolaPHistorico();

        [HttpPost]
        public ActionResult Report1Partial(string PK_IdCampania, int? Consecutivo_Repro)
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
            return PartialView("_Report1Partial", report);
        }

        [HttpPost]
        public ActionResult Report1PartialExport(int Consecutivo_Repro)
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        }
    }
}