using System.Linq;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using System;

namespace DXMVCWebApplication1.Controllers
{
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
    public class ReporteAnexoTecnico3Controller : Controller
    {
        DB_SENASICAEntities db = new DB_SENASICAEntities();

        Reports.ReporteAnexoTecnicoApendice3 reporte = new Reports.ReporteAnexoTecnicoApendice3();
        Reports.ReporteAnexoTecnicoApendice32018 reporte2018 = new Reports.ReporteAnexoTecnicoApendice32018();


        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult IndexReporte()
        {

            return View("IndexReporte");
        }
        public ActionResult reporteAnexoTecnicoApendice3(string ComboEstado, string ComboFecha)
        {
            int IdEstado = db.Estadoes.Where(it => it.Nombre == ComboEstado).Select(it => it.Pk_IdEstado).First();
            int IdAnio = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            ViewData["ComboEstado"] = ComboEstado;
            ViewData["ComboFecha"] = ComboFecha;

            string Dia = ComboFecha.Substring(0, 2);
            string Mes = FuncionesAuxiliares.Get_NombreMeses(Convert.ToInt32(ComboFecha.Substring(3, 2)));
            string Anio = ComboFecha.Substring(6, 4);

            if (IdAnio == 5)  //2018
            {
                reporte2018.Parameters["parameterEstado"].Visible = false;
                reporte2018.Parameters["parameterEstado"].Value = IdEstado;
                reporte2018.Parameters["parameterAnio"].Visible = false;
                reporte2018.Parameters["parameterAnio"].Value = IdAnio;
                reporte2018.Parameters["parameterFecha"].Visible = false;
                reporte2018.Parameters["parameterFecha"].Value = Dia + " DE " + Mes + " " + Anio;

                return PartialView("_ReporteAnexoTecnicoApendice3Partial", reporte2018);
            }
            else
            {
                reporte.Parameters["parameterEstado"].Visible = false;
                reporte.Parameters["parameterEstado"].Value = IdEstado;
                reporte.Parameters["parameterAnio"].Visible = false;
                reporte.Parameters["parameterAnio"].Value = IdAnio;

                return PartialView("_ReporteAnexoTecnicoApendice3Partial", reporte);
            }
        }

        public ActionResult ReportPartialExport(string ComboEstado, string ComboFecha)
        {
            int IdAnio = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            if (IdAnio == 5)
                return DocumentViewerExtension.ExportTo(reporte2018, Request);
            else
                return DocumentViewerExtension.ExportTo(reporte, Request);
        }
    }
}