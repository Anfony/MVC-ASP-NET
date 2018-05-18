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
    public class ProveedorController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        // GET: /Proveedor/
        public ActionResult Index(){ return View(); } 
     
        [ValidateInput(false)]
        public ActionResult ProveedorGridViewPartial()
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_REGISTRO_PROVEEDORES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            return PartialView("_ProveedorGridViewPartial", Senasica.GetProveedor());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ProveedorGridViewPartialAddNew(Proveedor item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_REGISTRO_PROVEEDORES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.Proveedors;
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

            return PartialView("_ProveedorGridViewPartial", Senasica.GetProveedor());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ProveedorGridViewPartialUpdate(Proveedor item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_REGISTRO_PROVEEDORES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.Proveedors;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdProveedor == item.Pk_IdProveedor);
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

            return PartialView("_ProveedorGridViewPartial", Senasica.GetProveedor());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ProveedorGridViewPartialDelete(System.Int32 Pk_IdProveedor)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_REGISTRO_PROVEEDORES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            var model = db.Proveedors;
            if (Pk_IdProveedor >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdProveedor == Pk_IdProveedor);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_Proveedor(Pk_IdProveedor, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_ProveedorGridViewPartial", Senasica.GetProveedor());
        }

        #region ComboBoxes
        //**********************************Combo de Municipios**********************************************************

        public ActionResult _ComboBoxMunicipioPartial()
        {
            int Pk_IdEstado = (Request.Params["Fk_IdEstado"] != null) ? int.Parse(Request.Params["Fk_IdEstado"]) : -1;
            return PartialView(Senasica.GetMunicipiosByEstado(Pk_IdEstado));
        }
        #endregion

        #region  GirosGridView

        //********************************************Giros X Proveedor**************************************************
        //***************************************************************************************************************
        
        [ValidateInput(false)]
        public ActionResult GirosGridViewPartial(int? IdProveedor)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_GIROS_PROVEEDORES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdProveedor"] = IdProveedor;

            return PartialView("_GirosGridViewPartial", Senasica.GetGirosByProveedor(IdProveedor));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GirosGridViewPartialAddNew(GirosXProv item, int IdProveedor)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_GIROS_PROVEEDORES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdProveedor"] = IdProveedor;
            var model = db.GirosXProvs;
            item.Fk_IdProveedor = IdProveedor;

            if (ModelState.IsValid)
            {
                try
                {
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

            return PartialView("_GirosGridViewPartial", Senasica.GetGirosByProveedor(IdProveedor));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GirosGridViewPartialUpdate(GirosXProv item, int IdProveedor)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_GIROS_PROVEEDORES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdProveedor"] = IdProveedor;
            var model = db.GirosXProvs;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.PK_IdGiroXProv == item.PK_IdGiroXProv);
                    if (modelItem != null)
                    {
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

            return PartialView("_GirosGridViewPartial", Senasica.GetGirosByProveedor(IdProveedor));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GirosGridViewPartialDelete(int PK_IdGiroXProv, int IdProveedor)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_GIROS_PROVEEDORES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdProveedor"] = IdProveedor;
            var model = db.GirosXProvs;
            if (PK_IdGiroXProv >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.PK_IdGiroXProv == PK_IdGiroXProv);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GirosGridViewPartial", Senasica.GetGirosByProveedor(IdProveedor));
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