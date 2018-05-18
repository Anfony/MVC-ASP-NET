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
    public class TipoDeBienController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        // GET: /TipoDeBien/

        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult TipoDeBienGridViewPartial()
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_TIPO_BIENES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            return PartialView("_TipoDeBienGridViewPartial", Senasica.GetTipoDeBiensByAnioPres());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TipoDeBienGridViewPartialAddNew(TipoDeBien item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_TIPO_BIENES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.TipoDeBiens;
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
                ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

            ViewData["dataItem"] = item;

            return PartialView("_TipoDeBienGridViewPartial", Senasica.GetTipoDeBiensByAnioPres());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TipoDeBienGridViewPartialUpdate(TipoDeBien item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_TIPO_BIENES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.TipoDeBiens;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdTipoDeBien == item.Pk_IdTipoDeBien);
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

            return PartialView("_TipoDeBienGridViewPartial", Senasica.GetTipoDeBiensByAnioPres());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TipoDeBienGridViewPartialDelete(System.Int32 Pk_IdTipoDeBien)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_TIPO_BIENES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.TipoDeBiens;
            if (Pk_IdTipoDeBien >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdTipoDeBien == Pk_IdTipoDeBien);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_TipoDeBien(Pk_IdTipoDeBien, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_TipoDeBienGridViewPartial", Senasica.GetTipoDeBiensByAnioPres());
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
