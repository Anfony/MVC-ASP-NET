using System;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using System.Linq;
using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                       + "," + RolesUsuarios.UR_INOCUIDAD
                       + "," + RolesUsuarios.UR_MOVILIZACION
                       + "," + RolesUsuarios.UR_ANIMAL
                       + "," + RolesUsuarios.UR_ACUICOLA_PESQUERA
                       + "," + RolesUsuarios.UR_VEGETAL
                       + "," + RolesUsuarios.IE_INOCUIDAD
                       + "," + RolesUsuarios.IE_VEGETAL
                       + "," + RolesUsuarios.IE_ACUICOLA_PESQUERA
                       + "," + RolesUsuarios.IE_ANIMAL
                       + "," + RolesUsuarios.IE_MOVILIZACION
                       + "," + RolesUsuarios.SUP_ESTATAL
                       + "," + RolesUsuarios.SUP_REGIONAL
                       + "," + RolesUsuarios.SUP_NACIONAL
                       + "," + RolesUsuarios.REPRESENTANTE_ESTATAL
      )]

    public class ReporteGastosFijosDeOperacionController : Controller
    {
        DB_SENASICAEntities db = new DB_SENASICAEntities();
        //
        // GET: /ReporteGastosFijosDeOperacion/

        public ActionResult Index()
        {
            int? _Fk_IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            ViewData["_Fk_IdEstado"] = _Fk_IdEstado;
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            ViewData["_Fk_IdUnidadEjecutora"] = _Fk_IdUnidadEjecutora;
            string rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            ViewData["rolusuario"] = rolusuario;

            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult IndexReporte()
        {
            return View("IndexReporte");
        }

        Reports.ReporteProgramaIntegral report = new Reports.ReporteProgramaIntegral();
        public ActionResult Report1Partial(string ComboComite )
        {
            int Id_UnidadEjecutora = db.UnidadEjecutoras.Where(it => it.Nombre == ComboComite).Select(it => it.Pk_IdUnidadEjecutora).First();
            ViewData["ComboComite"] = ComboComite;
            int IdAnio = Convert.ToInt32(Session["IdAnio"]);


            report.Parameters["parameterPk_Campania"].Visible = false;
            report.Parameters["parameterPk_Anio"].Visible = false; 
            report.Parameters["parameterPk_Campania"].Value = Id_UnidadEjecutora;
            report.Parameters["parameterPk_Anio"].Value = IdAnio;

            return PartialView("_Report1Partial", report);
        }
        public ActionResult Report1PartialExport(string ComboComite)
        {
            int Id_Estado = db.UnidadEjecutoras.Where(it => it.Nombre == ComboComite).Select(it => it.Fk_IdEstado__SIS).First();
            string estado = Convert.ToString(Id_Estado);
            

            
            int Id_TipoUnidad = db.UnidadEjecutoras.Where(it => it.Nombre == ComboComite).Select(it => it.Fk_IdTipoDeUnidad__SIS).First();
            int Id_Anio = Convert.ToInt32(Session["IdAnio"]);
            int anio = db.Anios.Where(item => item.Pk_IdAnio == Id_Anio).Select(o => o.Anio1).First();

            //-- PI0101-2018
            report.DisplayName = "PI0"+ estado +"01-"+ Convert.ToString(anio);
            return DocumentViewerExtension.ExportTo(report, Request);
        }
    }
}