using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class ReporteCierreMensualController : Controller
    {
        DXMVCWebApplication1.Reports.ReporteCierreMes reporte = new Reports.ReporteCierreMes();
        DXMVCWebApplication1.Models.DB_SENASICAEntities db = new Models.DB_SENASICAEntities();
        //
        // GET: /ReporteCierreMensual/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult reporteAvanceFisicoFinanciero(string PK_IdCierreMensual)
        {
            int IdCierreMensual =  Convert.ToInt32(PK_IdCierreMensual);
            
            int Pk_IdCampania = (from c in db.CierreMensuals where c.Pk_IdCierreMensual == IdCierreMensual select c.Fk_IdCampania).FirstOrDefault();
            int PK_IdMes = (from m in db.CierreMensuals where m.Pk_IdCierreMensual == IdCierreMensual select m.Fk_IdMes).FirstOrDefault();

            reporte.Parameters["parameterCampania"].Value = Pk_IdCampania;
            reporte.Parameters["parameterMes"].Value = PK_IdMes;
            reporte.Parameters["parameterCampania"].Visible = false;
            reporte.Parameters["parameterMes"].Visible = false;
            return PartialView("_ReporteCierreMesPartial", reporte);
        }
        public ActionResult ReportPartialExport(string ComboReport)
        {
                return DocumentViewerExtension.ExportTo(reporte, Request);            
        }
	}
}