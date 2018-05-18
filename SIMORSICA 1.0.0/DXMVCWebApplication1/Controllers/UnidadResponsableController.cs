using System;
using System.Linq;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using Microsoft.AspNet.Identity;
using DXMVCWebApplication1.Common;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                             + "," + RolesUsuarios.UR_INOCUIDAD
                             + "," + RolesUsuarios.UR_MOVILIZACION
                             + "," + RolesUsuarios.UR_ANIMAL
                             + "," + RolesUsuarios.UR_ACUICOLA_PESQUERA
                             + "," + RolesUsuarios.UR_VEGETAL
            )]
    public class UnidadResponsableController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult UnidadResponsableGridViewPartial()
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_UNIDAD_RESPONSABLE);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.UnidadResponsables;
            return PartialView("_UnidadResponsableGridViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UnidadResponsableGridViewPartialAddNew(UnidadResponsable item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_UNIDAD_RESPONSABLE);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.UnidadResponsables;
            item.CT_CreatedBy = 1;
            item.CT_CreatedDate = System.DateTime.Now.ToLocalTime();
            item.CT_LIVE = true;
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
                ViewData["EditError"] = "Por favor Corrija los errores señalados con signo de admiración.";

            ViewData["dataItem"] = item;

            return PartialView("_UnidadResponsableGridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UnidadResponsableGridViewPartialUpdate(UnidadResponsable item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_UNIDAD_RESPONSABLE);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.UnidadResponsables;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdUnidadResponsable == item.Pk_IdUnidadResponsable);
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

            return PartialView("_UnidadResponsableGridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UnidadResponsableGridViewPartialDelete(System.Int32 Pk_IdUnidadResponsable)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_UNIDAD_RESPONSABLE);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.UnidadResponsables;
            if (Pk_IdUnidadResponsable >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdUnidadResponsable == Pk_IdUnidadResponsable);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_UnidadResponsable(Pk_IdUnidadResponsable, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_UnidadResponsableGridViewPartial", model.ToList());
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
