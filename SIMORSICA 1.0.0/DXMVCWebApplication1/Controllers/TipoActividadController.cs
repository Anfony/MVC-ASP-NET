using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using Microsoft.AspNet.Identity;
using DXMVCWebApplication1.Common;
using System.Data.Entity.Core.Objects;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                        + "," + RolesUsuarios.UR_INOCUIDAD
                        + "," + RolesUsuarios.UR_MOVILIZACION
                        + "," + RolesUsuarios.UR_ANIMAL
                        + "," + RolesUsuarios.UR_ACUICOLA_PESQUERA
                        + "," + RolesUsuarios.UR_VEGETAL
                        + "," + RolesUsuarios.UR_UPV
       )]
    public class TipoActividadController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        // GET: /TipoActividad/
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_TIPO_ACTIVIDAD);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            String rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            ViewData["rolusuario"] = rolusuario;
            return PartialView("_GridViewPartial", Senasica.GetTiposDeActividadesXSubComp(rolusuario, IdAnio));

        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(TipoActividad item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_TIPO_ACTIVIDAD);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            String rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            ViewData["rolusuario"] = rolusuario;
            var model = db.TipoActividads;
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
                ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

            ViewData["dataItem"] = item;

            return PartialView("_GridViewPartial", Senasica.GetTiposDeActividadesXSubComp(rolusuario, IdAnio));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(TipoActividad item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_TIPO_ACTIVIDAD);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            String rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            ViewData["rolusuario"] = rolusuario;
            var model = db.TipoActividads;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdTipoActividad == item.Pk_IdTipoActividad);
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

            return PartialView("_GridViewPartial", Senasica.GetTiposDeActividadesXSubComp(rolusuario, IdAnio));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.Int32 Pk_IdTipoActividad)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_TIPO_ACTIVIDAD);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            String rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            ViewData["rolusuario"] = rolusuario;
            var model = db.TipoActividads;
            if (Pk_IdTipoActividad >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdTipoActividad == Pk_IdTipoActividad);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_TipoActividad(Pk_IdTipoActividad, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartial", Senasica.GetTiposDeActividadesXSubComp(rolusuario, IdAnio));
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
