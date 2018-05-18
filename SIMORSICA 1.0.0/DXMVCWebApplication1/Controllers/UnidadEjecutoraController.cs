using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                         + "," + RolesUsuarios.IE_INOCUIDAD
                         + "," + RolesUsuarios.IE_MOVILIZACION
                         + "," + RolesUsuarios.IE_ANIMAL
                         + "," + RolesUsuarios.IE_ACUICOLA_PESQUERA
                         + "," + RolesUsuarios.IE_VEGETAL
                         + "," + RolesUsuarios.UR_INOCUIDAD
                         + "," + RolesUsuarios.UR_MOVILIZACION
                         + "," + RolesUsuarios.UR_ANIMAL
                         + "," + RolesUsuarios.UR_ACUICOLA_PESQUERA
                         + "," + RolesUsuarios.UR_VEGETAL
                         + "," + RolesUsuarios.PUESTO_AUXILIAR_ADMINISTRATIVO
                         + "," + RolesUsuarios.PUESTO_AUXILIAR_INFORMATICA
                         + "," + RolesUsuarios.PUESTO_COOR_ADMINISTRATIVO
                         + "," + RolesUsuarios.COORDINADOR_CAMPANIA
                         + "," + RolesUsuarios.PUESTO_COOR_PROYECTO
                         + "," + RolesUsuarios.PUESTO_COOR_REGIONAL
                         + "," + RolesUsuarios.PUESTO_GERENTE
                         + "," + RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                         + "," + RolesUsuarios.PUESTO_PROF_ADMINISTRATIVO
                         + "," + RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                         + "," + RolesUsuarios.PUESTO_SECRETARIA
                         + "," + RolesUsuarios.SUP_ESTATAL
                         + "," + RolesUsuarios.SUP_REGIONAL
                         + "," + RolesUsuarios.REPRESENTANTE_ESTATAL
                         + "," + RolesUsuarios.SUP_NACIONAL
                         + "," + RolesUsuarios.PUESTO_AUXILIAR_CAMPO
                         + "," + RolesUsuarios.PUESTO_PROF_PROYECTO
                         + "," + RolesUsuarios.PUESTO_PROF_TECNICO_CALIDAD_MEJORA_PROCESOS
                         + "," + RolesUsuarios.PUESTO_PROF_TEC_CAPACITACION_DIVULGACION
                         + "," + RolesUsuarios.UR_UPV
                         + "," + RolesUsuarios.IE_PROGRAMAS_U
       )]
    public class UnidadEjecutoraController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        public ActionResult Index() {   return View();  }

        [ValidateInput(false)]
        public ActionResult UnidadEjecutoraGridViewPartial()
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_INSTANCIA_EJECUTORA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int? _Fk_IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            string rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            ViewData["rolusuario"] = rolusuario;

            return PartialView("_UnidadEjecutoraGridViewPartial", Senasica.GetUnidadEjecutoraByUsuario(rolusuario, FuncionesAuxiliares.GetCurrent_IdUnidadResponsable(), _Fk_IdEstado, _Fk_IdUnidadEjecutora));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UnidadEjecutoraGridViewPartialAddNew(UnidadEjecutora item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_INSTANCIA_EJECUTORA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int? _Fk_IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            string rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            ViewData["rolusuario"] = rolusuario;

            item.CT_LIVE = true;

            var model = db.UnidadEjecutoras;

            if (ModelState.IsValid)
            {
                try
                {
                    item.CT_User = User.Identity.GetUserId();
                    item.CT_Date = DateTime.Now;
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Por favor, corrija los errores marcados.";

            ViewData["dataItem"] = item;

            return PartialView("_UnidadEjecutoraGridViewPartial", Senasica.GetUnidadEjecutoraByUsuario(rolusuario, FuncionesAuxiliares.GetCurrent_IdUnidadResponsable(), _Fk_IdEstado, _Fk_IdUnidadEjecutora));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UnidadEjecutoraGridViewPartialUpdate(UnidadEjecutora item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_INSTANCIA_EJECUTORA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int? _Fk_IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            String rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            ViewData["rolusuario"] = rolusuario;

            var model = db.UnidadEjecutoras;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdUnidadEjecutora == item.Pk_IdUnidadEjecutora);
                    if (modelItem != null)
                    {
                        modelItem.CT_User = User.Identity.GetUserId();
                        modelItem.CT_Date = DateTime.Now;
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Por favor, corrija los errores marcados.";

            ViewData["dataItem"] = item;

            return PartialView("_UnidadEjecutoraGridViewPartial", Senasica.GetUnidadEjecutoraByUsuario(rolusuario, FuncionesAuxiliares.GetCurrent_IdUnidadResponsable(), _Fk_IdEstado, _Fk_IdUnidadEjecutora));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UnidadEjecutoraGridViewPartialDelete(Int32 Pk_IdUnidadEjecutora)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_INSTANCIA_EJECUTORA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int? _Fk_IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            String rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            ViewData["rolusuario"] = rolusuario;

            var model = db.UnidadEjecutoras;
            if (Pk_IdUnidadEjecutora >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdUnidadEjecutora == Pk_IdUnidadEjecutora);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_UnidadEjecutora(Pk_IdUnidadEjecutora, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_UnidadEjecutoraGridViewPartial", Senasica.GetUnidadEjecutoraByUsuario(rolusuario, FuncionesAuxiliares.GetCurrent_IdUnidadResponsable(), _Fk_IdEstado, _Fk_IdUnidadEjecutora));
        }

        public ActionResult _ComboBoxMunicipioPartial()
        {
            int Pk_IdEstado = (Request.Params["Fk_IdEstado__SIS"] != null) ? int.Parse(Request.Params["Fk_IdEstado__SIS"]) : -1;
            return PartialView(Senasica.GetMunicipiosByEstado(Pk_IdEstado));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}