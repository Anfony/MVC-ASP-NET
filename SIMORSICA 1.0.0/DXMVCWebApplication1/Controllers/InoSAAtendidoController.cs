using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                           + "," + RolesUsuarios.IE_ANIMAL
                           + "," + RolesUsuarios.UR_ANIMAL
                           + "," + RolesUsuarios.UR_INOCUIDAD
                           + "," + RolesUsuarios.COORDINADOR_CAMPANIA
                           + "," + RolesUsuarios.PUESTO_COOR_PROYECTO
                           + "," + RolesUsuarios.PUESTO_COOR_REGIONAL
                           + "," + RolesUsuarios.PUESTO_GERENTE
                           + "," + RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                           + "," + RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                           + "," + RolesUsuarios.SUP_ESTATAL
                           + "," + RolesUsuarios.SUP_REGIONAL
          )]
    public class InoSAAtendidoController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        //
        // GET: /InoSAAtendido/
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult InoCompSAGridViewPartial(int? IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_ATENDIDO_INO_SA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoCompSAs;
            return PartialView("_InoCompSAGridViewPartial", Senasica.GetInoCompSAByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult InoCompSAGridViewPartialAddNew(InoCompSA item, int IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_ATENDIDO_INO_SA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoCompSAs;
            item.FK_IdCampania = IdCampania;
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

            return PartialView("_InoCompSAGridViewPartial", Senasica.GetInoCompSAByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InoCompSAGridViewPartialUpdate(InoCompSA item, int? IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_ATENDIDO_INO_SA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoCompSAs;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdInoCompSA == item.Pk_IdInoCompSA);
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

            return PartialView("_InoCompSAGridViewPartial", Senasica.GetInoCompSAByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InoCompSAGridViewPartialDelete(System.Int32 Pk_IdInoCompSA, int? IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_ATENDIDO_INO_SA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoCompSAs;
            if (Pk_IdInoCompSA >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdInoCompSA == Pk_IdInoCompSA);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_InoCompSA(Pk_IdInoCompSA, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_InoCompSAGridViewPartial", Senasica.GetInoCompSAByCampania(IdCampania));
        }
    }
}