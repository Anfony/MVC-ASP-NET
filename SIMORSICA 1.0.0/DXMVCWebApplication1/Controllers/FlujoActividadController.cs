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
    public class FlujoActividadController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult FlujoActividadGridViewPartial()
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_FLUJO_ACTIVIDADES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.FlujoActividads;
            return PartialView("_FlujoActividadGridViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult FlujoActividadGridViewPartialAddNew(FlujoActividad item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_FLUJO_ACTIVIDADES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.FlujoActividads;
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
                ViewData["EditError"] = "Corrija los errores marcados.";

            ViewData["dataItem"] = item;

            return PartialView("_FlujoActividadGridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult FlujoActividadGridViewPartialUpdate(FlujoActividad item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_FLUJO_ACTIVIDADES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.FlujoActividads;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdFlujoActividad == item.Pk_IdFlujoActividad);
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
                ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

            ViewData["dataItem"] = item;

            return PartialView("_FlujoActividadGridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult FlujoActividadGridViewPartialDelete(System.Int32 Pk_IdFlujoActividad)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_FLUJO_ACTIVIDADES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.FlujoActividads;
            if (Pk_IdFlujoActividad >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdFlujoActividad == Pk_IdFlujoActividad);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_FlujoActividad(Pk_IdFlujoActividad, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_FlujoActividadGridViewPartial", model.ToList());
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