using DevExpress.Web.Mvc;
using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class RepCuadroComparativoCampaniaController : Controller
    {
        // GET: RepCuadroComparativoCampania
        public ActionResult Index()
        {
            return View();
        }

        DXMVCWebApplication1.Reports.ReporteCuadroComp_AccionXCampania RepAccionXCampania = new Reports.ReporteCuadroComp_AccionXCampania();
        DXMVCWebApplication1.Reports.ReporteCuadroComp_ActividadXAccion_Campania RepActividad_Accion = new Reports.ReporteCuadroComp_ActividadXAccion_Campania();
        DXMVCWebApplication1.Reports.ReporteCuadroComp_NecesidadesXAccion_Campania RepNecesidades_Accion = new Reports.ReporteCuadroComp_NecesidadesXAccion_Campania();
        DXMVCWebApplication1.Reports.ReporteCuadroComparativo_GastosRelativosCampania RepGastRelCampania = new Reports.ReporteCuadroComparativo_GastosRelativosCampania();
        DXMVCWebApplication1.Reports.ReporteCuadroComp_General RepCuadroComp_General = new Reports.ReporteCuadroComp_General();

        public ActionResult RepCuadroComparativoCampania(int? PK_IdCampania, string NombreTipoReporte)
        {
            ViewData["PK_IdCampania"] = PK_IdCampania;
            ViewData["NombreTipoReporte"] = NombreTipoReporte;

            /*
             * Reporte para Acciones por Campaña
            */
            if (NombreTipoReporte == ConstantesGlobales.ACCION_CAMPANIA)
            {
                RepAccionXCampania.Parameters["parameter1"].Visible = false;//parameter1 - PK_IdCampania
                RepAccionXCampania.Parameters["parameter1"].Value = PK_IdCampania;

                return PartialView("_RepCuadroComparativoCampaniaPartial", RepAccionXCampania);
            }

            /*
             * Reporte para Acciones por Campaña -> Actividades por Acción 
            */

            if (NombreTipoReporte == ConstantesGlobales.ACTIVIDAD_ACCION)
            {
                RepActividad_Accion.Parameters["parameter1"].Visible = false;//parameter1 - PK_IdCampania
                RepActividad_Accion.Parameters["parameter1"].Value = PK_IdCampania;

                return PartialView("_RepCuadroComparativoCampaniaPartial", RepActividad_Accion);
            }

            /*
             * Reporte para Acciones por Campaña -> Necedidades por Acción 
            */

            if (NombreTipoReporte == ConstantesGlobales.NECESIDAD_ACCION)
            {
                RepNecesidades_Accion.Parameters["parameter1"].Visible = false;//parameter1 - Modificado
                RepNecesidades_Accion.Parameters["parameter1"].Value = PK_IdCampania;

                return PartialView("_RepCuadroComparativoCampaniaPartial", RepNecesidades_Accion);
            }

            /*
            * Reporte para Gastos Relativos a la Campaña
             */
            if (NombreTipoReporte == ConstantesGlobales.GASTOS_REL_CAMPANIA)
            {
                RepGastRelCampania.Parameters["parameter1"].Visible = false;
                RepGastRelCampania.Parameters["parameter1"].Value = PK_IdCampania;

                return PartialView("_RepCuadroComparativoCampaniaPartial", RepGastRelCampania);
            }
            /*
        * Reporte Cuadro Comprativo General 
         */
            if (NombreTipoReporte == ConstantesGlobales.CUADRO_COMP_GENERAL)
            {
                RepCuadroComp_General.Parameters["parameter1"].Visible = false;
                RepCuadroComp_General.Parameters["parameter1"].Value = PK_IdCampania;

                return PartialView("_RepCuadroComparativoCampaniaPartial", RepCuadroComp_General);
            }
            return null;
        }
        public ActionResult ReportPartialExport(int? PK_IdCampania, string NombreTipoReporte)
        {
            ViewData["NombreTipoReporte"] = NombreTipoReporte;

            if (NombreTipoReporte == ConstantesGlobales.ACCION_CAMPANIA)
            {
                return DocumentViewerExtension.ExportTo(RepAccionXCampania, Request);
            }
            if (NombreTipoReporte == ConstantesGlobales.ACTIVIDAD_ACCION)
            {
                return DocumentViewerExtension.ExportTo(RepActividad_Accion, Request);
            }
            if (NombreTipoReporte == ConstantesGlobales.NECESIDAD_ACCION)
            {
                return DocumentViewerExtension.ExportTo(RepNecesidades_Accion, Request);
            }
            if (NombreTipoReporte == ConstantesGlobales.GASTOS_REL_CAMPANIA)
            {
                return DocumentViewerExtension.ExportTo(RepGastRelCampania, Request);
            }
            if (NombreTipoReporte == ConstantesGlobales.CUADRO_COMP_GENERAL)
            {
                return DocumentViewerExtension.ExportTo(RepCuadroComp_General, Request);
            }
            return null;
        }
    }
}