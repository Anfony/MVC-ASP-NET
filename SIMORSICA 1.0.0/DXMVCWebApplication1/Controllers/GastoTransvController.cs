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
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO)]
    public class GastoTransvController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        // GET: /GastoTransv/
        public ActionResult Index()
        {
            return View();
        }        

        [ValidateInput(false)]
        public ActionResult GastoTransvGridViewPartial()
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_GASTO_TRANSVERSAL);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.GastoTransvs;
            return PartialView("_GastoTransvGridViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GastoTransvGridViewPartialAddNew(DXMVCWebApplication1.Models.GastoTransv item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_GASTO_TRANSVERSAL);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.GastoTransvs;
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
                ViewData["EditError"] = "Please, correct all errors.";

            ViewData["dataItem"] = item;

            return PartialView("_GastoTransvGridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GastoTransvGridViewPartialUpdate(DXMVCWebApplication1.Models.GastoTransv item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_GASTO_TRANSVERSAL);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.GastoTransvs;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdGastoTransv == item.Pk_IdGastoTransv);
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
                ViewData["EditError"] = "Please, correct all errors.";

            ViewData["dataItem"] = item;

            return PartialView("_GastoTransvGridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GastoTransvGridViewPartialDelete(System.Int32 Pk_IdGastoTransv)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_GASTO_TRANSVERSAL);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.GastoTransvs;
            if (Pk_IdGastoTransv >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdGastoTransv == Pk_IdGastoTransv);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_GastoTransv(Pk_IdGastoTransv, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GastoTransvGridViewPartial", model.ToList());
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
