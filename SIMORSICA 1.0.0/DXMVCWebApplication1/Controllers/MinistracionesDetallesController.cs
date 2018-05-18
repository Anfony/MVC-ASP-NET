using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.ViewModels;
using System;
using System.Web.Mvc;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using DXMVCWebApplication1.Common;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize]
    public class MinistracionesDetallesController : Controller
    {
        [ValidateInput(false)]
        public ActionResult DetalleMinistracionesGridViewPartial(int? IdMinistracionesXComite)
        {
            Permisos();

            ViewData["IdMinistracionesXComite"] = IdMinistracionesXComite;

            return PartialView("_DetallesMinistracionGridViewPartial", Senasica.GetDetalleMinistracionByMinistracion(IdMinistracionesXComite));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DetalleMinistracionesGridViewPartialAddNew(MinistracionDetalleVM itemVM, int IdMinistracionesXComite)
        {
            Permisos();

            ViewData["IdMinistracionesXComite"] = IdMinistracionesXComite;

            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var model = db.MinistracionDetalles;

            if (ModelState.IsValid)
            {
                try
                {
                    MinistracionDetalle item = new MinistracionDetalle()
                    {
                        Pk_IdMinistracionDetalle = itemVM.Pk_IdMinistracionDetalle,
                        Fk_IdMinistracionesXComite = IdMinistracionesXComite,
                        Fk_IdCampania = itemVM.Fk_IdCampania,
                        Importe = itemVM.ImporteDetalle
                    };

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

            ViewData["dataItem"] = itemVM;

            return PartialView("_DetallesMinistracionGridViewPartial", Senasica.GetDetalleMinistracionByMinistracion(IdMinistracionesXComite));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DetalleMinistracionesGridViewPartialUpdate(MinistracionDetalleVM itemVM, int IdMinistracionesXComite)
        {
            Permisos();

            ViewData["IdMinistracionesXComite"] = IdMinistracionesXComite;

            DB_SENASICAEntities db = new DB_SENASICAEntities();

            if (ModelState.IsValid)
            {
                try
                {
                    var model = db.MinistracionDetalles;
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdMinistracionDetalle == itemVM.Pk_IdMinistracionDetalle);

                    if (modelItem != null)
                    {
                        modelItem.Fk_IdMinistracionesXComite = IdMinistracionesXComite;
                        modelItem.Fk_IdCampania = itemVM.Fk_IdCampania;
                        modelItem.Importe = itemVM.ImporteDetalle;
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

            ViewData["dataItem"] = itemVM;

            return PartialView("_DetallesMinistracionGridViewPartial", Senasica.GetDetalleMinistracionByMinistracion(IdMinistracionesXComite));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DetalleMinistracionesGridViewPartialDelete(System.Int32 Pk_IdMinistracionDetalle, int IdMinistracionesXComite)
        {
            Permisos();

            ViewData["IdMinistracionesXComite"] = IdMinistracionesXComite;

            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var model = db.MinistracionDetalles;
            if (Pk_IdMinistracionDetalle >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdMinistracionDetalle == Pk_IdMinistracionDetalle);
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

            return PartialView("_DetallesMinistracionGridViewPartial", Senasica.GetDetalleMinistracionByMinistracion(IdMinistracionesXComite));
        }

        private void Permisos()
        {
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_MINISTRACION_DETALLE);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
        }
    }
}