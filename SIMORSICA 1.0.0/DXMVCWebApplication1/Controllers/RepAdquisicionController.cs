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
                        + "," + RolesUsuarios.IE_INOCUIDAD
                        + "," + RolesUsuarios.IE_MOVILIZACION
                        + "," + RolesUsuarios.IE_ANIMAL
                        + "," + RolesUsuarios.IE_ACUICOLA_PESQUERA
                        + "," + RolesUsuarios.IE_VEGETAL
       )] 
    public class RepAdquisicionController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        // GET: /RepAdquisicion/
        public ActionResult Index()
        {
            return View();
        }
        
        [ValidateInput(false)]
        public ActionResult AdquisicionGridViewPartial()
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_INFORME_ADQUISICIONES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            ViewBag.UEID = _Fk_IdUnidadEjecutora;

            var model = db.RepAdquisicions;
            return PartialView("_AdquisicionGridViewPartial", Senasica.GetRepAdquisicionesByUnidadEjecutora(_Fk_IdUnidadEjecutora, IdAnio));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AdquisicionGridViewPartialAddNew(RepAdquisicion item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_INFORME_ADQUISICIONES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            ViewBag.UEID = _Fk_IdUnidadEjecutora;

            var model = db.RepAdquisicions;
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

            return PartialView("_AdquisicionGridViewPartial", Senasica.GetRepAdquisicionesByUnidadEjecutora(_Fk_IdUnidadEjecutora, IdAnio));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AdquisicionGridViewPartialUpdate(RepAdquisicion item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_INFORME_ADQUISICIONES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            ViewBag.UEID = _Fk_IdUnidadEjecutora;

            var model = db.RepAdquisicions;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdRepAdquisicion == item.Pk_IdRepAdquisicion);
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

            return PartialView("_AdquisicionGridViewPartial", Senasica.GetRepAdquisicionesByUnidadEjecutora(_Fk_IdUnidadEjecutora, IdAnio));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AdquisicionGridViewPartialDelete(System.Int32 Pk_IdRepAdquisicion)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_INFORME_ADQUISICIONES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            ViewBag.UEID = _Fk_IdUnidadEjecutora;

            var model = db.RepAdquisicions;
            if (Pk_IdRepAdquisicion >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdRepAdquisicion == Pk_IdRepAdquisicion);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_RepAdquisicion(Pk_IdRepAdquisicion, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_AdquisicionGridViewPartial", Senasica.GetRepAdquisicionesByUnidadEjecutora(_Fk_IdUnidadEjecutora, IdAnio));
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
