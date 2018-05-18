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
    public class CampaniaNController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        public ActionResult Index()
        {
            Session["CurrentController"] = "CampaniaN";
            return View();
        }
        [ValidateInput(false)]
        public ActionResult CampaniaGridViewPartial()
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(FuncionesAuxiliares.GetCurrent_RolUsuario(), ListaPantallas.UE_PROGRAMAS_TRABAJO);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            return PartialView("_CampaniaGridViewPartial", Senasica.GetCampanias());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CampaniaGridViewPartialAddNew(Campania item)
        {
            var model = db.Campanias;
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
                ViewData["EditError"] = "Por favor corrija los errores marcados.";

            ViewData["dataItem"] = item;

            return RedirectToAction("Index");
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CampaniaGridViewPartialUpdate(Campania item)
        {
            var model = db.Campanias;

            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdCampania == item.Pk_IdCampania);
                    if (modelItem != null)
                    {
                        modelItem.CT_User = User.Identity.GetUserId();
                        modelItem.CT_Date = DateTime.Now;
                        UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Por favor corrija los errores marcados.";

            ViewData["dataItem"] = item;

            return RedirectToAction("Index");
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CampaniaGridViewPartialDelete(System.Int32 Pk_IdCampania)
        {
            var model = db.Campanias;
            if (Pk_IdCampania >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdCampania == Pk_IdCampania);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_Campania(Pk_IdCampania, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            return RedirectToAction("Index");
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