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

    //Autorización de Roles
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                       + "," + RolesUsuarios.UR_INOCUIDAD
                       + "," + RolesUsuarios.UR_MOVILIZACION
                       + "," + RolesUsuarios.UR_ANIMAL
                       + "," + RolesUsuarios.UR_ACUICOLA_PESQUERA
                       + "," + RolesUsuarios.UR_VEGETAL
                       + "," + RolesUsuarios.SUP_ESTATAL
                       + "," + RolesUsuarios.SUP_REGIONAL
                       + "," + RolesUsuarios.SUP_NACIONAL
                       + "," + RolesUsuarios.REPRESENTANTE_ESTATAL
      )]
    public class ReportePresupuestoOriginalController : Controller
    {
        DB_SENASICAEntities db = new DB_SENASICAEntities();

        //
        // GET: /ReportePresupuestoOriginal/
       Reports.ReportePresupuestoOriginal report = new Reports.ReportePresupuestoOriginal();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult IndexReporte()
        {

            return View("IndexReporte");
        }

        //public ActionResult ReportPartial()
        //{
        //    return PartialView("_ReportPartial", report);
        //}
        public ActionResult ReportPartial(string ComboEstado, string ComboFecha)
        {
           int IdEstado = db.Estadoes.Where(it => it.Nombre == ComboEstado).Select(it => it.Pk_IdEstado).First();
            
            int IdAnio = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            ViewData["ComboEstado"] = ComboEstado;
            ViewData["ComboFecha"] = ComboFecha;

            string Dia = ComboFecha.Substring(0, 2);
            string Mes = FuncionesAuxiliares.Get_NombreMeses(Convert.ToInt32(ComboFecha.Substring(3, 2)));
            string Anio = ComboFecha.Substring(6, 4);

            
                report.Parameters["parameterEstado"].Visible = false;
                report.Parameters["parameterEstado"].Value = IdEstado;
                report.Parameters["parameterAnio"].Visible = false;
                report.Parameters["parameterAnio"].Value = IdAnio;
                report.Parameters["parameterFecha"].Visible = false;
                report.Parameters["parameterFecha"].Value = Dia + " DE " + Mes + " " + Anio;

                return PartialView("_ReportPartial", report);

           
        }
        public ActionResult ReportPartialExport(string ComboEstado, string ComboFecha)
        {
            int IdAnio = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            return DocumentViewerExtension.ExportTo(report, Request);
        }
	}
}