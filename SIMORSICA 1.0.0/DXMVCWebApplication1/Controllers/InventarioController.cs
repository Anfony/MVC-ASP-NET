using System;
using System.Linq;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Common;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using Microsoft.AspNet.Identity;
using DXMVCWebApplication1.Negocio;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize]
    public class InventarioController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        public ActionResult InventarioGridViewPartial(int? UEID)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_INVENTARIO);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["UEID"] = UEID;
            return PartialView("_InventarioGridViewPartial", Senasica.GetInventarioByUnidadEjecutora(UEID));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult InventarioGridViewPartialAddNew(Inventario item, int UEID)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_INVENTARIO);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["UEID"] = UEID;
            var model = db.Inventarios;
            item.Fk_IdUnidadEjecutora = UEID;
            if (ModelState.IsValid)
            {
                try
                {
                    item.CT_User = User.Identity.GetUserId();
                    item.CT_Date = DateTime.Now;

                    ClaveInventario nuevaClave = new ClaveInventario();

                    item.Clave = nuevaClave.GeneraClave();

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

            return PartialView("_InventarioGridViewPartial", Senasica.GetInventarioByUnidadEjecutora(UEID));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult InventarioGridViewPartialUpdate(Inventario item, int UEID)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_INVENTARIO);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["UEID"] = UEID;
            var model = db.Inventarios;
            item.Fk_IdUnidadEjecutora = UEID;

            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.PK_IdInventario == item.PK_IdInventario);
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

            return PartialView("_InventarioGridViewPartial", Senasica.GetInventarioByUnidadEjecutora(UEID));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult InventarioGridViewPartialDelete(Int32 PK_IdInventario, int UEID)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_INVENTARIO);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["UEID"] = UEID;
            var model = db.Inventarios;
            if (PK_IdInventario >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.PK_IdInventario == PK_IdInventario);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_Inventario(PK_IdInventario, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_InventarioGridViewPartial", Senasica.GetInventarioByUnidadEjecutora(UEID));
        }
    }
}