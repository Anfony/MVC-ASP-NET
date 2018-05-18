using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;

namespace DXMVCWebApplication1.Controllers
{
    public class ReporteAvancesDeCapturaController : Controller
    {
        DB_SENASICAEntities db = new DB_SENASICAEntities();
        //
        // GET: /ReporteAvancesDeCaptura/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult IndexReporte()
        {
            return View("IndexReporte");
        }

        Reports.AvancesDeCaptura report = new Reports.AvancesDeCaptura();

        public ActionResult ReportPartial(string ComboUR)
        {

            int _FK_IdUnidadResponsable = db.UnidadResponsables.Where(it => it.Nombre == ComboUR).Select(it => it.Pk_IdUnidadResponsable).First();
            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            ViewData["ComboUR"] = ComboUR;
          //  int IdEstado = db.Estadoes.Where(it => it.Nombre == ComboEstado).Select(it => it.Pk_IdEstado).First();
           // int? _FK_IdUnidadResponsable = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadResponsable__UE); //Habilitar esta linea si se pasa la unidad responsable del usuario
           
            
           // if (_FK_IdUnidadResponsable > 0)
            //{
                
           // }
            report.Parameters["parameter1"].Value = _FK_IdUnidadResponsable;
            report.Parameters["parameter2"].Value = IdAnio;
            report.Parameters["parameter2"].Visible = false;
            report.Parameters["parameter1"].Visible = false;
            return PartialView("_ReportPartial", report);
        }

        public ActionResult ReportPartialExport(string ComboUR)
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        }
	}
}