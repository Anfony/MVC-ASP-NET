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
    [Authorize]
    public class InstalacionController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        public ActionResult InstalacionesGridViewPartial(int? UEID)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_PADRONES_INSTALACION);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["UEID"] = UEID;
            return PartialView("_InstalacionesGridViewPartial", Senasica.GetInstalacionesByUnidadEjecutora(UEID));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult InstalacionesGridViewPartialAddNew(Instalacion item, int UEID)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_PADRONES_INSTALACION);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["UEID"] = UEID;
            var model = db.Instalacions;
            item.Fk_IdUnidadEjecutora = UEID;
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

            return PartialView("_InstalacionesGridViewPartial", Senasica.GetInstalacionesByUnidadEjecutora(UEID));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult InstalacionesGridViewPartialUpdate(Instalacion item, int UEID)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_PADRONES_INSTALACION);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["UEID"] = UEID;
            var model = db.Instalacions;

            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdInstalacion == item.Pk_IdInstalacion);
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

            return PartialView("_InstalacionesGridViewPartial", Senasica.GetInstalacionesByUnidadEjecutora(UEID));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult InstalacionesGridViewPartialDelete(Int32 Pk_IdInstalacion, int UEID)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_PADRONES_INSTALACION);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["UEID"] = UEID;
            var model = db.Instalacions;
            if (Pk_IdInstalacion >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdInstalacion == Pk_IdInstalacion);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_Instalacion(Pk_IdInstalacion, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_InstalacionesGridViewPartial", Senasica.GetInstalacionesByUnidadEjecutora(UEID));
        }
    }
}