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
                          + "," + RolesUsuarios.UR_INOCUIDAD
                          + "," + RolesUsuarios.UR_MOVILIZACION
                          + "," + RolesUsuarios.UR_ANIMAL
                          + "," + RolesUsuarios.UR_ACUICOLA_PESQUERA
                          + "," + RolesUsuarios.UR_VEGETAL
         )]
    public class CapituloPartidaController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult CapituloPartidaGridViewPartial()
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_CAPITULOS_GASTO);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            return PartialView("_CapituloPartidaGridViewPartial", Senasica.GetCapituloPartidasByAnioPres());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CapituloPartidaGridViewPartialAddNew(CapituloPartida item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_CAPITULOS_GASTO);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.CapituloPartidas;
            if (ModelState.IsValid)
            {
                try
                {
                    item.Fk_IdAnio = Convert.ToInt32(Session["IdAnio"]);
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

            return PartialView("_CapituloPartidaGridViewPartial", Senasica.GetCapituloPartidasByAnioPres());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CapituloPartidaGridViewPartialUpdate(CapituloPartida item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_CAPITULOS_GASTO);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.CapituloPartidas;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdCapituloPartida == item.Pk_IdCapituloPartida);
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

            return PartialView("_CapituloPartidaGridViewPartial", Senasica.GetCapituloPartidasByAnioPres());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CapituloPartidaGridViewPartialDelete(System.Int32 Pk_IdCapituloPartida)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_CAPITULOS_GASTO);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.CapituloPartidas;
            if (Pk_IdCapituloPartida >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdCapituloPartida == Pk_IdCapituloPartida);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_CapituloPartida(Pk_IdCapituloPartida, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_CapituloPartidaGridViewPartial", Senasica.GetCapituloPartidasByAnioPres());
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
