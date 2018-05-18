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
    public class TipoDeUnidadController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridView5Partial()
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_TIPO_INSTANCIA_EJECUTORA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.TipoDeUnidads;
            return PartialView("_GridView5Partial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView5PartialAddNew(TipoDeUnidad item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_TIPO_INSTANCIA_EJECUTORA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.TipoDeUnidads;
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

            return PartialView("_GridView5Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView5PartialUpdate(TipoDeUnidad item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_TIPO_INSTANCIA_EJECUTORA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.TipoDeUnidads;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdTipoDeUnidad == item.Pk_IdTipoDeUnidad);
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

            return PartialView("_GridView5Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView5PartialDelete(System.Int32 Pk_IdTipoDeUnidad)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_TIPO_INSTANCIA_EJECUTORA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.TipoDeUnidads;
            if (Pk_IdTipoDeUnidad >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdTipoDeUnidad == Pk_IdTipoDeUnidad);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_TipoDeUnidad(Pk_IdTipoDeUnidad, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridView5Partial", model.ToList());
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
