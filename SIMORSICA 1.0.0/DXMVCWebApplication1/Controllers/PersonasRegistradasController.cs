using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System;


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
                         + "," + RolesUsuarios.PUESTO_AUXILIAR_ADMINISTRATIVO
                         + "," + RolesUsuarios.PUESTO_AUXILIAR_INFORMATICA
                         + "," + RolesUsuarios.PUESTO_COOR_ADMINISTRATIVO
                         + "," + RolesUsuarios.COORDINADOR_CAMPANIA
                         + "," + RolesUsuarios.PUESTO_COOR_PROYECTO
                         + "," + RolesUsuarios.PUESTO_COOR_REGIONAL
                         + "," + RolesUsuarios.PUESTO_GERENTE
                         + "," + RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                         + "," + RolesUsuarios.PUESTO_PROF_ADMINISTRATIVO
                         + "," + RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                         + "," + RolesUsuarios.PUESTO_SECRETARIA
                         + "," + RolesUsuarios.SUP_ESTATAL
                         + "," + RolesUsuarios.IE_PROGRAMAS_U
   )]
    public class PersonasRegistradasController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult PersonasRegistradasGridViewPartial()
        {
            getPermisos();

            int _Fk_IdUnidadEjecutora = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();
            string rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();

            if(rolUsuario == RolesUsuarios.SUPERUSUARIO)
                return PartialView("PersonasRegistradasAllGridViewPartial", Senasica.GetUsers());

            return PartialView("PersonasRegistradasGridViewPartial", Senasica.GetUsersWithID(_Fk_IdUnidadEjecutora));
        }

        /// <summary>
        /// Solo el superusuario realiza esta acción
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public ActionResult PersonasRegistradasGridViewPartialAddNew(RegisterViewModel item)
        {
            getPermisos();

            try
            {
                if (ModelState.IsValid)
                {
                    DB_SENASICAEntities db = new DB_SENASICAEntities();
                    string rolName = Senasica.GetRolNameById(Convert.ToInt32(item.dropDownRoles));
                    int IdUnidadResponsable = 0;
                    int IdInstanciaEjecutora = 0;

                    switch (rolName)
                    {
                        case RolesUsuarios.SUP_REGIONAL:
                            string userNameReg = item.UserName.Substring(0, 5);

                            if (userNameReg != "REG01"
                                && userNameReg != "REG02"
                                && userNameReg != "REG03"
                                && userNameReg != "REG04"
                                && userNameReg != "REG05"
                                && userNameReg != "REG06")
                            {
                                ViewData["EditError"] = "Formato de usuario regional incorrecto: " + item.UserName;
                                return PartialView("PersonasRegistradasAllGridViewPartial", Senasica.GetUsers());
                            }
                            break;
                        case RolesUsuarios.IE_INOCUIDAD:
                        case RolesUsuarios.IE_MOVILIZACION:
                        case RolesUsuarios.IE_ANIMAL:
                        case RolesUsuarios.IE_ACUICOLA_PESQUERA:
                        case RolesUsuarios.IE_VEGETAL:

                            IdInstanciaEjecutora = item.dropDownInstanciaEjecutora;

                            IdUnidadResponsable = db.UnidadEjecutoras.Where(it => it.Pk_IdUnidadEjecutora == IdInstanciaEjecutora)
                                .Select(it => it.Fk_IdUnidadResponsable__UE)
                                .First();
                            break;
                        case RolesUsuarios.UR_INOCUIDAD:
                            IdUnidadResponsable = 16;
                            break;
                        case RolesUsuarios.UR_MOVILIZACION:
                            IdUnidadResponsable = 17;
                            break;
                        case RolesUsuarios.UR_ANIMAL:
                            IdUnidadResponsable = 14;
                            break;
                        case RolesUsuarios.UR_ACUICOLA_PESQUERA:
                            IdUnidadResponsable = 19;
                            break;
                        case RolesUsuarios.UR_VEGETAL:
                            IdUnidadResponsable = 6;
                            break;
                        case RolesUsuarios.SUP_ESTATAL:
                            IdUnidadResponsable = 25;
                            IdInstanciaEjecutora = db.UnidadEjecutoras.Where(it => it.Fk_IdTipoDeUnidad__SIS == 27  //Gobierno Estatal
                                                                                && it.Fk_IdUnidadResponsable__UE == 25  //Unidad de Coordinación y Enlace
                                                                                && it.Fk_IdEstado__SIS == item.dropDownEstado)
                                                                        .Select(it => it.Pk_IdUnidadEjecutora)
                                                                        .First();
                            break;
                        case RolesUsuarios.SUP_NACIONAL:
                            IdUnidadResponsable = 25;
                            break;
                    }

                    var user = new ApplicationUser()
                    {
                        UserName = item.UserName,
                        FK_IdEstado__SIS = item.dropDownEstado,
                        FK_IdUnidadResponsable__UE = IdUnidadResponsable,
                        FK_IdUnidadEjecutora__UE = IdInstanciaEjecutora,
                        FK_IdCampania__UE = 0,
                        FK_IdAccion__UE = 0,
                        FK_IdActividad__UE = 0,
                        FK_IdPersona__SIS = 0
                    };

                    UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                    var result = manager.Create(user, item.Password);

                    if (result.Succeeded)
                    {
                        manager.AddToRole(user.Id, rolName);
                    }
                    else
                    {
                        string lstErrores = "";

                        foreach (var itemError in result.Errors)
                        {
                            lstErrores += itemError + " ";
                        }

                        ViewData["EditError"] = lstErrores;

                        ModelState.AddModelError("", "Error en la creación, intente nuevamente");
                    }
                }
                else
                {
                    ViewData["EditError"] = "Hubo un problema, favor de verificar";
                }
            }
            catch(Exception e)
            {
                ViewData["EditError"] = "Hubo un problema, favor de verificar: " + e.Message;
            }

            ViewData["dataItem"] = item;

            return PartialView("PersonasRegistradasAllGridViewPartial", Senasica.GetUsers());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PersonasRegistradasGridViewPartialUpdate(RegisterUpdateViewModel item)
        {
            getPermisos();

            try
            {
                if (ModelState.IsValid)
                {
                    DB_SENASICAEntities db = new DB_SENASICAEntities();
                    string rolName = Senasica.GetRolNameById(Convert.ToInt32(item.dropDownRoles));
                    int IdUnidadResponsable = 0;
                    int IdInstanciaEjecutora = 0;

                    switch (rolName)
                    {
                        case RolesUsuarios.SUP_REGIONAL:
                            string userNameReg = item.UserName.Substring(0, 5);

                            if (userNameReg != "REG01"
                                && userNameReg != "REG02"
                                && userNameReg != "REG03"
                                && userNameReg != "REG04"
                                && userNameReg != "REG05"
                                && userNameReg != "REG06")
                            {
                                ViewData["EditError"] = "Formato de usuario regional incorrecto" + item.UserName;
                                return PartialView("PersonasRegistradasAllGridViewPartial", Senasica.GetUsers());
                            }
                            break;
                        case RolesUsuarios.IE_INOCUIDAD:
                        case RolesUsuarios.IE_MOVILIZACION:
                        case RolesUsuarios.IE_ANIMAL:
                        case RolesUsuarios.IE_ACUICOLA_PESQUERA:
                        case RolesUsuarios.IE_VEGETAL:

                            IdInstanciaEjecutora = item.dropDownInstanciaEjecutora;

                            IdUnidadResponsable = db.UnidadEjecutoras.Where(it => it.Pk_IdUnidadEjecutora == IdInstanciaEjecutora)
                                .Select(it => it.Fk_IdUnidadResponsable__UE)
                                .First();
                            break;
                        case RolesUsuarios.UR_INOCUIDAD:
                            IdUnidadResponsable = 16;
                            break;
                        case RolesUsuarios.UR_MOVILIZACION:
                            IdUnidadResponsable = 17;
                            break;
                        case RolesUsuarios.UR_ANIMAL:
                            IdUnidadResponsable = 14;
                            break;
                        case RolesUsuarios.UR_ACUICOLA_PESQUERA:
                            IdUnidadResponsable = 19;
                            break;
                        case RolesUsuarios.UR_VEGETAL:
                            IdUnidadResponsable = 6;
                            break;
                        case RolesUsuarios.SUP_ESTATAL:
                            IdUnidadResponsable = 25;
                            IdInstanciaEjecutora = db.UnidadEjecutoras.Where(it => it.Fk_IdTipoDeUnidad__SIS == 27  //Gobierno Estatal
                                                        && it.Fk_IdUnidadResponsable__UE == 25  //Unidad de Coordinación y Enlace
                                                        && it.Fk_IdEstado__SIS == item.dropDownEstado)
                                                .Select(it => it.Pk_IdUnidadEjecutora)
                                                .First();
                            break;
                        case RolesUsuarios.SUP_NACIONAL:
                            IdUnidadResponsable = 25;
                            break;
                    }

                    var user = new ApplicationUser()
                    {
                        Id = item.ID,
                        UserName = item.UserName,
                        FK_IdEstado__SIS = item.dropDownEstado,
                        FK_IdUnidadResponsable__UE = IdUnidadResponsable,
                        FK_IdUnidadEjecutora__UE = IdInstanciaEjecutora,
                        FK_IdCampania__UE = 0,
                        FK_IdAccion__UE = 0,
                        FK_IdActividad__UE = 0,
                        FK_IdPersona__SIS = 0
                    };

                    UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                    var _usuario = manager.FindById(item.ID);
                    var resultDelete = manager.Delete(_usuario);

                    if (!resultDelete.Succeeded)
                    {
                        ViewData["EditError"] = "No se pudo actualizar el usuario, verifíquelo nuevamente";
                        return PartialView("PersonasRegistradasAllGridViewPartial", Senasica.GetUsers());
                    }

                    var resultCreate = manager.Create(user, item.Password);

                    if (resultCreate.Succeeded)
                    {
                        manager.AddToRole(user.Id, rolName);
                    }
                    else
                    {
                        string lstErrores = "";

                        foreach (var itemError in resultCreate.Errors)
                        {
                            lstErrores += itemError + " ";
                        }

                        ViewData["EditError"] = lstErrores;

                        ModelState.AddModelError("", "Error en la creación, intente nuevamente");
                    }
                }
                else
                {
                    ViewData["EditError"] = "Hubo un problema, favor de verificar";
                }
            }
            catch (Exception e)
            {
                ViewData["EditError"] = "Hubo un problema, favor de verificar: " + e.Message;
            }

            ViewData["dataItem"] = item;

            return PartialView("PersonasRegistradasAllGridViewPartial", Senasica.GetUsers());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PersonasRegistradasGridViewPartialDelete(string ID)
        {
            getPermisos();

            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            int _Fk_IdUnidadEjecutora = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

            var _usuario = userManager.FindById(ID);
            userManager.Delete(_usuario);

            string rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();

            if (rolUsuario == RolesUsuarios.SUPERUSUARIO)
                return PartialView("PersonasRegistradasAllGridViewPartial", Senasica.GetUsers());

            return PartialView("PersonasRegistradasGridViewPartial", Senasica.GetUsersWithID(_Fk_IdUnidadEjecutora));
        }

        private void getPermisos()
        {
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_PERSONAL_SISTEMA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
        }
    }
}