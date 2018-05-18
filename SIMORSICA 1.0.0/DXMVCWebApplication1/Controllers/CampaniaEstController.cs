using System;
using System.Linq;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Common;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using Microsoft.AspNet.Identity;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                          + "," + RolesUsuarios.IE_VEGETAL
                          + "," + RolesUsuarios.UR_VEGETAL
                          + "," + RolesUsuarios.PUESTO_COOR_PROYECTO
                          + "," + RolesUsuarios.COORDINADOR_CAMPANIA
                          + "," + RolesUsuarios.PUESTO_COOR_REGIONAL
                          + "," + RolesUsuarios.PUESTO_GERENTE
                          + "," + RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                          + "," + RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                          + "," + RolesUsuarios.SUP_ESTATAL
                          + "," + RolesUsuarios.SUP_REGIONAL
                          + "," + RolesUsuarios.REPRESENTANTE_ESTATAL
         )]
    public class CampaniaEstController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        public ActionResult Index()
        {
            Session["CurrentController"] = "CampaniaEst";
            return View();
        }

        [ValidateInput(false)]
        public ActionResult CampaniaEstGridViewPartial()
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_PROGRAMAS_TRABAJO_EST);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            return PartialView("_CampaniaEstGridViewPartial", Senasica.GetCampaniasEst());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CampaniaEstGridViewPartialAddNew(Campania item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_PROGRAMAS_TRABAJO_EST);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.Campanias;
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
                ViewData["EditError"] = "Por favor corrija los errores marcados.";

            ViewData["dataItem"] = item;

            return PartialView("_CampaniaEstGridViewPartial", Senasica.GetCampaniasEst());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CampaniaEstGridViewPartialUpdate(Campania item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_PROGRAMAS_TRABAJO_EST);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.Campanias;

            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdCampania == item.Pk_IdCampania);
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
                ViewData["EditError"] = "Por favor corrija los errores marcados.";

            ViewData["dataItem"] = item;

            return PartialView("_CampaniaEstGridViewPartial", Senasica.GetCampaniasEst());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CampaniaEstGridViewPartialDelete(System.Int32 Pk_IdCampania)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_PROGRAMAS_TRABAJO_EST);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.Campanias;
            if (Pk_IdCampania >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdCampania == Pk_IdCampania);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_Campania(Pk_IdCampania, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_CampaniaEstGridViewPartial", Senasica.GetCampaniasEst());
        }

        public ActionResult _ComboBoxProyectoPartial()
        {
            int IdSubComponente = (Request.Params["Fk_IdSubComponente__SIS"] != null) ? int.Parse(Request.Params["Fk_IdSubComponente__SIS"]) : -1;

            return PartialView("_ComboBoxProyectoPartial", Senasica.GetProyectosAutorizadosBySubComponente(IdSubComponente));
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