using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Core.Objects;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                        + "," + RolesUsuarios.UR_INOCUIDAD
                        + "," + RolesUsuarios.UR_MOVILIZACION
                        + "," + RolesUsuarios.UR_ANIMAL
                        + "," + RolesUsuarios.UR_ACUICOLA_PESQUERA
                        + "," + RolesUsuarios.UR_VEGETAL
                        + "," + RolesUsuarios.SUP_ESTATAL
       )] 
    public class ProyectoAutorizadoController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        private UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        // GET: /ProyectoAutorizado/
    
        public ActionResult Index()
        {
            return View();
        }        
       
        [ValidateInput(false)]
        public ActionResult ProyectoAutorizadoGridViewPartial()
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_PROGRAMA_AUTORIZADO);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            String rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            ViewData["rolusuario"] = rolusuario;
            ViewData["_FK_IdAnio"] = IdAnio;

            return PartialView("ProyectoAutorizadoGridViewPartial", Senasica.GetProyectoAutorizadoXUsuarios_Pres(rolusuario, IdAnio));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ProyectoAutorizadoGridViewPartialAddNew(ProyectoAutorizado item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_PROGRAMA_AUTORIZADO);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            String rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            ViewData["rolusuario"] = rolusuario;
            int? _FK_IdUnidadResponsable__UE = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadResponsable__UE);
            var model = db.ProyectoAutorizadoes;
            item.Fk_IdAnio = IdAnio;
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
                ViewData["EditError"] = "Por favor, corrija los errores marcados.";

            ViewData["dataItem"] = item;

            return PartialView("ProyectoAutorizadoGridViewPartial", Senasica.GetProyectoAutorizadoXUsuarios_Pres(rolusuario, IdAnio));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ProyectoAutorizadoGridViewPartialUpdate(ProyectoAutorizado item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_PROGRAMA_AUTORIZADO);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            String rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            ViewData["rolusuario"] = rolusuario;
            int? _FK_IdUnidadResponsable__UE = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadResponsable__UE);
            var model = db.ProyectoAutorizadoes;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdProyectoAutorizado == item.Pk_IdProyectoAutorizado);
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

            return PartialView("ProyectoAutorizadoGridViewPartial", Senasica.GetProyectoAutorizadoXUsuarios_Pres(rolusuario, IdAnio));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ProyectoAutorizadoGridViewPartialDelete(int Pk_IdProyectoAutorizado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_PROGRAMA_AUTORIZADO);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            String rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            ViewData["rolusuario"] = rolusuario;
            int? _FK_IdUnidadResponsable__UE = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadResponsable__UE);
            var model = db.ProyectoAutorizadoes;
            if (Pk_IdProyectoAutorizado >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdProyectoAutorizado == Pk_IdProyectoAutorizado);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_ProyectoAutorizado(Pk_IdProyectoAutorizado, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("ProyectoAutorizadoGridViewPartial", DXMVCWebApplication1.Models.Senasica.GetProyectoAutorizadoXUsuarios_Pres(rolusuario, IdAnio));
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
