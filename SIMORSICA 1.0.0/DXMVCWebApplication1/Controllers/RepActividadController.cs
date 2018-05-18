using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Common;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Core.Objects;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                        + "," + RolesUsuarios.IE_INOCUIDAD
                        + "," + RolesUsuarios.IE_MOVILIZACION
                        + "," + RolesUsuarios.IE_ANIMAL
                        + "," + RolesUsuarios.IE_ACUICOLA_PESQUERA
                        + "," + RolesUsuarios.IE_VEGETAL
                        + "," + RolesUsuarios.REPRESENTANTE_ESTATAL
                        + "," + RolesUsuarios.UR_UPV
       )] 

    public class RepActividadController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        // GET: /RepActividad/
        public ActionResult Index()
        {
            return View();
        }

        #region RepActividadGridView
        [ValidateInput(false)]
        public ActionResult RepActividadGridViewPartial()
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_ACCIONES_CAMPANIA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);            
            ViewBag.UEID = _Fk_IdUnidadEjecutora;

            var model = db.RepActividads;
            return PartialView("_RepActividadGridViewPartial", Senasica.GetRepActividadesByUnidadEjecutora(_Fk_IdUnidadEjecutora, IdAnio));

        }

        [HttpPost, ValidateInput(false)]
        public ActionResult RepActividadGridViewPartialAddNew(RepActividad item)
        {
            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            ViewBag.UEID = _Fk_IdUnidadEjecutora;

            var model = db.RepActividads;
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

            return PartialView("_RepActividadGridViewPartial", Senasica.GetRepActividadesByUnidadEjecutora(_Fk_IdUnidadEjecutora , IdAnio));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult RepActividadGridViewPartialUpdate(RepActividad item)
        {
            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            ViewBag.UEID = _Fk_IdUnidadEjecutora;

            var model = db.RepActividads;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdRepActividad == item.Pk_IdRepActividad);
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

            return PartialView("_RepActividadGridViewPartial", Senasica.GetRepActividadesByUnidadEjecutora(_Fk_IdUnidadEjecutora, IdAnio));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult RepActividadGridViewPartialDelete(System.Int32 Pk_IdRepActividad)
        {
            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            ViewBag.UEID = _Fk_IdUnidadEjecutora;

            var model = db.RepActividads;
            if (Pk_IdRepActividad >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdRepActividad == Pk_IdRepActividad);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_RepActividad(Pk_IdRepActividad, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_RepActividadGridViewPartial", Senasica.GetRepActividadesByUnidadEjecutora(_Fk_IdUnidadEjecutora, IdAnio));
        } 
        #endregion

        #region RepGastosGridView
        //TAB REPORTE GASTOS
        DB_SENASICAEntities db2 = new DB_SENASICAEntities();

        [ValidateInput(false)]
        public ActionResult RepGastosGridViewPartial(int? IdActividad)
        {
            ViewData["IdActividad"] = IdActividad;
            return PartialView("RepGastosGridViewPartial", Senasica.GetRepGastosByActividad(IdActividad));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult RepGastosGridViewPartialAddNew(RepGasto item, int IdActividad)
        {
            ViewData["IdActividad"] = IdActividad;
            var model = db.RepGastoes;
            item.Fk_IdRepActividad = IdActividad;
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

            return PartialView("RepGastosGridViewPartial", Senasica.GetRepGastosByActividad(IdActividad));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult RepGastosGridViewPartialUpdate(RepGasto item, int IdActividad)
        {
            ViewData["IdActividad"] = IdActividad;
            var model = db.RepGastoes;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdRepGasto == item.Pk_IdRepGasto);
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
                ViewData["EditError"] = "Por favor corrija los errores señalados.";

            ViewData["dataItem"] = item;

            return PartialView("RepGastosGridViewPartial", Senasica.GetRepGastosByActividad(IdActividad));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult RepGastosGridViewPartialDelete(System.Int32 Pk_IdRepGasto, int IdActividad)
        {
            ViewData["IdActividad"] = IdActividad;
            var model = db.RepGastoes;
            if (Pk_IdRepGasto >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdRepGasto == Pk_IdRepGasto);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_RepGasto(Pk_IdRepGasto, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("RepGastosGridViewPartial", Senasica.GetRepGastosByActividad(IdActividad));
        } 
        #endregion
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
