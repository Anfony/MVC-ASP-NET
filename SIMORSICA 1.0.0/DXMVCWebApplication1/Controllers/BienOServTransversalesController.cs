using System;
using System.Linq;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using System.Data.Entity.Core.Objects;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using DXMVCWebApplication1.Common;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize]
    public class BienOServTransversalesController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        // GET: BienOServTransversales
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_BIENES_SERVICIO_TRANSVERSALES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            return PartialView("_GridViewPartial", Senasica.GetBienesOServTransversales());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(BienOServ item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_BIENES_SERVICIO_TRANSVERSALES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.BienOServs;
            if (ModelState.IsValid)
            {
                try
                {
                    item.Fk_IdAnio = Convert.ToInt32(Session["IdAnio"]);
                    item.esTransversal = true;
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

            return PartialView("_GridViewPartial", Senasica.GetBienesOServTransversales());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(BienOServ item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_BIENES_SERVICIO_TRANSVERSALES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.BienOServs;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdBienOServ == item.Pk_IdBienOServ);
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

            return PartialView("_GridViewPartial", Senasica.GetBienesOServTransversales());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.Int32 Pk_IdBienOServ)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_BIENES_SERVICIO_TRANSVERSALES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.BienOServs;
            if (Pk_IdBienOServ >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdBienOServ == Pk_IdBienOServ);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_BienOServ(Pk_IdBienOServ, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartial", Senasica.GetBienesOServTransversales());
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