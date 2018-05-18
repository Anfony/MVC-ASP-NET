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
    public class VigenciaController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        public ActionResult VigenciaGridViewPartial(int? UEID)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_VIGENCIA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["UEID"] = UEID;
            return PartialView("_VigenciaGridViewPartial", Senasica.GetVigenciasByUnidadEjecutora(UEID));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult VigenciaGridViewPartialAddNew(Vigencia item, int UEID)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_VIGENCIA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["UEID"] = UEID;
            var model = db.Vigencias;
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

            return PartialView("_VigenciaGridViewPartial", Senasica.GetVigenciasByUnidadEjecutora(UEID));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult VigenciaGridViewPartialUpdate(Vigencia item, int UEID)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_VIGENCIA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["UEID"] = UEID;
            var model = db.Vigencias;
            if (ModelState.IsValid)
            {
                try
                {
                    //Actualiza detalle
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdVigencia == item.Pk_IdVigencia);
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

            return PartialView("_VigenciaGridViewPartial", Senasica.GetVigenciasByUnidadEjecutora(UEID));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult VigenciaGridViewPartialDelete(Int32 Pk_IdVigencia, int UEID)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_VIGENCIA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["UEID"] = UEID;
            var model = db.Vigencias;
            if (Pk_IdVigencia >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdVigencia == Pk_IdVigencia);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_Vigencia(Pk_IdVigencia, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_VigenciaGridViewPartial", Senasica.GetVigenciasByUnidadEjecutora(UEID));
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
