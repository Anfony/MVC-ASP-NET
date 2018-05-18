using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize]
    public class BienOServByUMController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        // GET: BienOServByUM

        [ValidateInput(false)]
        public ActionResult BienOServByUMGridViewPartial(int? IdBienOServ)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_BIENES_SERVICIO_UNIDAMEDIDA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdBienOServ"] = IdBienOServ;

            return PartialView("_BienOServByUMGridViewPartial", Senasica.GetBienesOServByUnidadMedida(IdBienOServ));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult BienOServByUMGridViewPartialAddNew(BienOservByUnidadMedida item, int IdBienOServ)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_BIENES_SERVICIO_UNIDAMEDIDA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdBienOServ"] = IdBienOServ;
            var model = db.BienOservByUnidadMedidas;
            if (ModelState.IsValid)
            {
                try
                {
                    item.Fk_IdBienOServ = IdBienOServ;
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

            return PartialView("_BienOServByUMGridViewPartial", Senasica.GetBienesOServByUnidadMedida(IdBienOServ));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult BienOServByUMGridViewPartialUpdate(BienOservByUnidadMedida item, int IdBienOServ)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_BIENES_SERVICIO_UNIDAMEDIDA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdBienOServ"] = IdBienOServ;
            var model = db.BienOservByUnidadMedidas;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdBienOServByUnidadMedida == item.Pk_IdBienOServByUnidadMedida);
                    if (modelItem != null)
                    {
                        modelItem.Fk_IdBienOServ = IdBienOServ;
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

            return PartialView("_BienOServByUMGridViewPartial", Senasica.GetBienesOServByUnidadMedida(IdBienOServ));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult BienOServByUMGridViewPartialDelete(System.Int32 Pk_IdBienOServByUnidadMedida, int IdBienOServ)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_BIENES_SERVICIO_UNIDAMEDIDA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdBienOServ"] = IdBienOServ;
            var model = db.BienOservByUnidadMedidas;
            if (Pk_IdBienOServByUnidadMedida > 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdBienOServByUnidadMedida == Pk_IdBienOServByUnidadMedida);
                    if (item != null)
                    {
                        model.Remove(item);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_BienOServByUMGridViewPartial", Senasica.GetBienesOServByUnidadMedida(IdBienOServ));
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