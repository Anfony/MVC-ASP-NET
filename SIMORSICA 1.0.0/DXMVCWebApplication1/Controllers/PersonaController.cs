using System;
using System.Linq;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize]
    public class PersonaController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        public ActionResult Index(){    return View();  }
        
        public ActionResult PersonaGridViewPartial(int? UEID, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_PERSONAL_INSTANCIA_EJECUTORA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["UEID"] = UEID;
            ViewData["IdEstado"] = IdEstado;
            String rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            ViewData["rolusuario"] = rolusuario;
          

            return PartialView("_PersonaGridViewPartial", Senasica.GetPersonasByUnidadEjecutora(UEID));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PersonaGridViewPartialAddNew(Persona item, int UEID, int IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_PERSONAL_INSTANCIA_EJECUTORA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["UEID"] = UEID;
            ViewData["IdEstado"] = IdEstado;
            String rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            var model = db.Personas;
            ViewData["rolusuario"] = rolusuario;
            item.Fk_IdUnidadEjecutora__UE = UEID;

            if (ModelState.IsValid)
            {
                try
                {
                    item.CT_User = User.Identity.GetUserId();
                    item.CT_Date = DateTime.Now;
                    item.esActivo = true;

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

            return PartialView("_PersonaGridViewPartial", Senasica.GetPersonasByUnidadEjecutora(UEID));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PersonaGridViewPartialUpdate(Persona item, int UEID, int IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_PERSONAL_INSTANCIA_EJECUTORA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            ViewData["UEID"] = UEID;
            ViewData["IdEstado"] = IdEstado;
            string rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            
            var model = db.Personas;
            ViewData["rolusuario"] = rolusuario;

            //Resumen Persona Activa/Inactiva
            int CountActividadAsignada = db.Actividads.Where(a => a.Fk_IdPersona__SIS == item.Pk_IdPersona && a.FechaFin > DateTime.Today).Select(a => a.Pk_IdActividad).Count();
            int CountInformeAvances = db.RepActividads.Where(ra => ra.Fk_IdPersona == item.Pk_IdPersona && ra.esCerrado == false).Select(ra => ra.Pk_IdRepActividad).Count();
            int CountCoorCampania = db.Campanias.Where(c => c.Fk_IdCoordinador == item.Pk_IdPersona).Select(c => c.Pk_IdCampania).Count();
            
            bool esCorrecto = CountActividadAsignada == 0
                                    && CountInformeAvances == 0
                                    && CountCoorCampania == 0 ? true : false;

            if (esCorrecto == false && item.esActivo == false)
            {
                ViewData["IdPersona"] = item.Pk_IdPersona;
                ViewData["CountActividadAsignada"] = CountActividadAsignada;
                ViewData["CountInformeAvances"] = CountInformeAvances;
                ViewData["CountCoorCampania"] = CountCoorCampania;
                
                return PartialView("_ResumenPersona");
            }
            //

            item.Fk_IdUnidadEjecutora__UE = UEID;
            string cargo = db.Cargoes.Where(c => c.Pk_IdCargo == item.Fk_IdCargo).Select(c => c.Nombre).First();

            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdPersona == item.Pk_IdPersona);
                    if (modelItem != null)
                    {
                        modelItem.CT_User = User.Identity.GetUserId();
                        modelItem.CT_Date = DateTime.Now;
                        //Actualiza el rol del usuario si el puesto se ha actualizado
                        var _usuarios = (from usuario in userManager.Users
                                         where usuario.FK_IdPersona__SIS == item.Pk_IdPersona
                                         select usuario).ToList();

                        // se actualiza todos sus usuarios creados
                        foreach (ApplicationUser _user in _usuarios)
                        {
                            userManager.RemoveFromRoles(_user.Id, modelItem.Cargo.Nombre);
                            userManager.AddToRole(_user.Id, cargo);
                        }

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
            // Elimina Cuenta de usuario
            if (esCorrecto == true && item.esActivo == false)
             {
                 try
                 {
                   var modelItem = model.FirstOrDefault(it => it.Pk_IdPersona == item.Pk_IdPersona);
 
                     if (modelItem != null)
                     {
                         modelItem.CT_User = User.Identity.GetUserId();
                         modelItem.CT_Date = DateTime.Now;
                         modelItem.esActivo = false;
 
                         //Se elimina sus usuarios con lo cual accedia al sistema

                       var usuarios = userManager.Users.Where(item_ => item_.FK_IdPersona__SIS ==item.Pk_IdPersona).ToList();
                         foreach (ApplicationUser _user in usuarios) userManager.Delete(_user);
 
                         UpdateModel(modelItem);
                         db.SaveChanges();
                     }
                 }
                 catch (Exception e)
                 {
                     ViewData["EditError"] = e.Message;
                 }
             }
            //
            return PartialView("_PersonaGridViewPartial", Senasica.GetPersonasByUnidadEjecutora(UEID));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PersonaGridViewPartialDelete(Int32 Pk_IdPersona, int UEID, int IdEstado)
        {  
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_PERSONAL_INSTANCIA_EJECUTORA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["UEID"] = UEID;
            ViewData["IdEstado"] = IdEstado;
            String rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            var model = db.Personas;
            ViewData["rolusuario"] = rolusuario;

            if (Pk_IdPersona >= 0)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdPersona == Pk_IdPersona);

                    if (modelItem != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_Persona(Pk_IdPersona, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_PersonaGridViewPartial", Senasica.GetPersonasByUnidadEjecutora(UEID));
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
