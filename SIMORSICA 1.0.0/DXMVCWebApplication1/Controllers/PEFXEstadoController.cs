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
    public class PEFXEstadoController : Controller
    {
        public ActionResult Index()
        {
            ViewData["esRecursoCerrado"] = Senasica.GetEsRecursoCerradoPEFXEstado();

            return View();
        }

        [ValidateInput(false)]
        public ActionResult PEFXEstadoGridViewPartial()
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_PEFXESTADO);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["esRecursoCerrado"] = Senasica.GetEsRecursoCerradoPEFXEstado();

            return PartialView("_PEFXEstadoGridViewPartial", Senasica.GetPEFXEstado());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PEFXEstadoGridViewPartialAddNew(PEFXEstado item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_PEFXESTADO);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var model = db.PEFXEstadoes;
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
                ViewData["EditError"] = "Corrija los errores marcados.";

            ViewData["dataItem"] = item;

            return PartialView("_PEFXEstadoGridViewPartial", Senasica.GetPEFXEstado());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PEFXEstadoGridViewPartialUpdate(PEFXEstado item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_PEFXESTADO);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var model = db.PEFXEstadoes;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdPEFXEstado == item.Pk_IdPEFXEstado);
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
                ViewData["EditError"] = "Corrija los errores marcados.";

            ViewData["dataItem"] = item;

            return PartialView("_PEFXEstadoGridViewPartial", Senasica.GetPEFXEstado());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PEFXEstadoGridViewPartialDelete(System.Int32 Pk_IdPEFXEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_PEFXESTADO);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var model = db.PEFXEstadoes;
            if (Pk_IdPEFXEstado >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdPEFXEstado == Pk_IdPEFXEstado);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_PEFXEstado(Pk_IdPEFXEstado, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_PEFXEstadoGridViewPartial", Senasica.GetPEFXEstado());
        }

        public ActionResult CierraDistribucionRecurso()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int IdAnio = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            var _elementos = db.PEFXEstadoes.Where(p => p.FK_IdAnio == IdAnio);

            foreach (var item in _elementos) item.PresupuestoCerrado = true;

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}