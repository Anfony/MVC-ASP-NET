using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.ViewsModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DevExpress.Web.ASPxGridView;
using System.Data.Entity.Core.Objects;
using System.Collections.Generic;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                             + "," + RolesUsuarios.IE_INOCUIDAD
                             + "," + RolesUsuarios.IE_MOVILIZACION
                             + "," + RolesUsuarios.IE_ANIMAL
                             + "," + RolesUsuarios.IE_ACUICOLA_PESQUERA
                             + "," + RolesUsuarios.IE_VEGETAL
                             + "," + RolesUsuarios.UR_INOCUIDAD
                             + "," + RolesUsuarios.UR_MOVILIZACION
                             + "," + RolesUsuarios.UR_ANIMAL
                             + "," + RolesUsuarios.UR_ACUICOLA_PESQUERA
                             + "," + RolesUsuarios.UR_VEGETAL
                             + "," + RolesUsuarios.PUESTO_COOR_ADMINISTRATIVO
                             + "," + RolesUsuarios.SUP_REGIONAL
                             + "," + RolesUsuarios.REPRESENTANTE_ESTATAL
                             + "," + RolesUsuarios.SUP_NACIONAL
                             + "," + RolesUsuarios.SUP_ESTATAL
                             + "," + RolesUsuarios.PUESTO_AUXILIAR_CAMPO
                             + "," + RolesUsuarios.PUESTO_COOR_REGIONAL
                             + "," + RolesUsuarios.PUESTO_PROF_PROYECTO
                             + "," + RolesUsuarios.PUESTO_PROF_TECNICO_CALIDAD_MEJORA_PROCESOS
                             + "," + RolesUsuarios.PUESTO_PROF_TEC_CAPACITACION_DIVULGACION
                             + "," + RolesUsuarios.IE_PROGRAMAS_U
           )]
    public class ProductorController : Controller
    {
        public int idProductor;
        public string nombreProductor;
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        private UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        public ActionResult Index()
        {
            return View();
        }

        DB_SENASICAEntities db1 = new DB_SENASICAEntities();

        [ValidateInput(false)]
        public ActionResult ProductorGridViewPartial()
        {
            Permisos();

            int? _Fk_IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            return PartialView("_ProductorGridViewPartial", Senasica.GetProductorByEstado(_Fk_IdEstado));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ProductorGridViewPartialAddNew(ProductorVM ProductorVM)
        {
            Permisos();

            int? _Fk_IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);

            ViewData["dataItem"] = ProductorVM;

            // se parsea el punto de forma manual
            if (ProductorVM.Latitud == null)
            {
                return PartialView("_ProductorGridViewPartial", Senasica.GetProductorByEstado(_Fk_IdEstado));
            }
            string punto = string.Format("POINT({0} {1})", ProductorVM.Longitud.Replace(",", "."), ProductorVM.Latitud.Replace(",", "."));
            System.Data.Entity.Spatial.DbGeography ubi = System.Data.Entity.Spatial.DbGeography.PointFromText(punto, 4326);

            var model = db1.Productors;
            
            if (ModelState.IsValid)
            {
                try
                {                  
                    Productor item = new Productor();
                    item.RazonSocial = ProductorVM.RazonSocial;
                    item.ApellidoPaterno = ProductorVM.ApellidoPaterno;
                    item.ApellidoMaterno = ProductorVM.ApellidoMaterno;
                    item.Nombre = ProductorVM.Nombre;
                    item.Direccion = ProductorVM.Direccion;
                    item.Colonia = ProductorVM.Colonia;
                    item.Fk_IdEstado__SIS = ProductorVM.Fk_IdEstado__SIS;
                    item.Fk_IdMunicipio__SIS = ProductorVM.Fk_IdMunicipio__SIS;
                    item.Telefono = ProductorVM.Telefono;
                    item.Email = ProductorVM.Email;
                    item.Ubicacion = ubi;

                    item.CT_User = User.Identity.GetUserId();
                    item.CT_Date = DateTime.Now;
                    model.Add(item);
                    db1.SaveChanges();
                    idProductor = item.Pk_IdProductor;
                    nombreProductor = item.RazonSocial;
                    ASPxGridView gridView = new ASPxGridView();
                    gridView.JSProperties["cpidProductor"] = item.Pk_IdProductor;
                }
                catch (DbEntityValidationException e)
                {
                    string errores = "";
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        foreach (var ve in eve.ValidationErrors)
                        {
                            errores += string.Format("Propiedad: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    ViewData["EditError"] = errores;
                }
            }
            else
                ViewData["EditError"] = "Por favor, corrija los errores marcados.";

            return PartialView("_ProductorGridViewPartial", Senasica.GetProductorByEstado(_Fk_IdEstado));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ProductorGridViewPartialUpdate(ProductorVM ProductorVM)
        {
            Permisos();

            int? _Fk_IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);

            ViewData["dataItem"] = ProductorVM;

            // se parsea el punto de forma manual
            string lng = ProductorVM.Longitud.Replace(",", ".");
            string lat = ProductorVM.Latitud.Replace(",", ".");
            string punto = string.Format("POINT({0} {1})", lng, lat);
            System.Data.Entity.Spatial.DbGeography Ubicacion = System.Data.Entity.Spatial.DbGeography.PointFromText(punto, 4326);

            var model = db1.Productors;

            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdProductor == ProductorVM.Pk_IdProductor);
                    if (modelItem != null)
                    {
                        modelItem.RazonSocial = ProductorVM.RazonSocial;
                        modelItem.ApellidoPaterno = ProductorVM.ApellidoPaterno;
                        modelItem.ApellidoMaterno = ProductorVM.ApellidoMaterno;
                        modelItem.Nombre = ProductorVM.Nombre;
                        modelItem.Direccion = ProductorVM.Direccion;
                        modelItem.Colonia = ProductorVM.Colonia;
                        modelItem.Fk_IdEstado__SIS = ProductorVM.Fk_IdEstado__SIS;
                        modelItem.Fk_IdMunicipio__SIS = ProductorVM.Fk_IdMunicipio__SIS;
                        modelItem.Telefono = ProductorVM.Telefono;
                        modelItem.Email = ProductorVM.Email;
                        modelItem.Ubicacion = Ubicacion;
                        modelItem.CT_User = User.Identity.GetUserId();
                        modelItem.CT_Date = DateTime.Now;

                        this.UpdateModel(modelItem);
                        db1.SaveChanges();
                    }
                }
                catch (DbEntityValidationException e)
                {
                    string errores = "";
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        foreach (var ve in eve.ValidationErrors)
                        {
                            errores += string.Format("Propiedad: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    ViewData["EditError"] = errores;
                }
            }
            else
                ViewData["EditError"] = "Por favor, corrija los errores marcados.";

            return PartialView("_ProductorGridViewPartial", Senasica.GetProductorByEstado(_Fk_IdEstado));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ProductorGridViewPartialDelete(System.Int32 Pk_IdProductor)
        {
            Permisos();

            int? _Fk_IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            var model = db1.Productors;
            if (Pk_IdProductor >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdProductor == Pk_IdProductor);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_Productor(Pk_IdProductor, User.Identity.GetUserId(), Error);
                    }
                    db1.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            ViewBag.idProductor = Pk_IdProductor;

            return PartialView("_ProductorGridViewPartial", Senasica.GetProductorByEstado(_Fk_IdEstado));
        }

        private void Permisos()
        {
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_PADRON_UNIDADES_PRODUCCION);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
        }

        public ActionResult _ComboBoxMunicipioPartial()
        {
            int IdEstado = (Request.Params["Fk_IdEstado__SIS"] != null) ? int.Parse(Request.Params["Fk_IdEstado__SIS"]) : -1;

            return PartialView("_ComboBoxMunicipioPartial", Senasica.GetMunicipiosByEstado(IdEstado));
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