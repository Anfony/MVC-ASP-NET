using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using System;
using System.Web.Mvc;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize]
    public class MinistracionesXComiteController : Controller
    {
        // GET: MinistracionesXComite
        public ActionResult Index() { return View(); }

        [ValidateInput(false)]
        public ActionResult MinistracionesXComiteGridViewPartial()
        {
            Permisos();

            return PartialView("_MinistracionesXComiteGridViewPartial", Senasica.GetMinistraciones());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult MinistracionesXComiteAddNew(MinistracionesXComite item)
        {
            Permisos();

            if (ModelState.IsValid)
            {
                try
                {
                    int IdIE = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();
                    DB_SENASICAEntities db = new DB_SENASICAEntities();

                    var model = db.MinistracionesXComites;
                    int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

                    item.Fk_IdInstanciaEjecutora = IdIE;
                    item.Fk_IdAnio = IdAnioPres;
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

            return PartialView("_MinistracionesXComiteGridViewPartial", Senasica.GetMinistraciones());
        }

        public ActionResult MinistracionesXComiteUpdate(MinistracionesXComite item)
        {
            Permisos();

            if (ModelState.IsValid)
            {
                try
                {
                    DB_SENASICAEntities db = new DB_SENASICAEntities();

                    var model = db.MinistracionesXComites;
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdMinistracionesXComite == item.Pk_IdMinistracionesXComite);

                    if(modelItem != null)
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
                ViewData["EditError"] = "Por favor corrija los errores marcados.";

            ViewData["dataItem"] = item;

            return PartialView("_MinistracionesXComiteGridViewPartial", Senasica.GetMinistraciones());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult MinistracionesXComiteDelete(System.Int32 Pk_IdMinistracionesXComite)
        {
            Permisos();

            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var model = db.MinistracionesXComites;
            if (Pk_IdMinistracionesXComite >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdMinistracionesXComite == Pk_IdMinistracionesXComite);
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

            return PartialView("_MinistracionesXComiteGridViewPartial", Senasica.GetMinistraciones());
        }

        private void Permisos()
        {
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_MINISTRACION_COMITE);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];

            int Id_anioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            string anioPres = FuncionesAuxiliares.AnioPresupuestal(FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal());

            ViewData["FechaIni"] = "01/01/" + anioPres;
            if (Id_anioPres == 3)
            {
                ViewData["FechaFin"] = "31/03/2018";
            }
            else
            {
                ViewData["FechaFin"] = "31/12/" + anioPres;
            }
        }
    }
}