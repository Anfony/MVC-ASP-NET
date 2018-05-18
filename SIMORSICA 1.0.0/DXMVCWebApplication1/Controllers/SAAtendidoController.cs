using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                           + "," + RolesUsuarios.IE_ANIMAL
                           + "," + RolesUsuarios.UR_ANIMAL
                           + "," + RolesUsuarios.COORDINADOR_CAMPANIA
                           + "," + RolesUsuarios.PUESTO_COOR_REGIONAL
                           + "," + RolesUsuarios.PUESTO_COOR_PROYECTO
                           + "," + RolesUsuarios.PUESTO_GERENTE
                           + "," + RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                           + "," + RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                           + "," + RolesUsuarios.SUP_ESTATAL
                           + "," + RolesUsuarios.SUP_REGIONAL
          )]
    public class SAAtendidoController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        // GET: SAAtendido
        public ActionResult Index() { return View(); }

        [ValidateInput(false)]
        public ActionResult AtendidoSAGridViewPartial(int? IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_ATENDIDO_SA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            return PartialView("_AtendidoSAGridViewPartial", Senasica.GetSAAtendidoByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AtendidoSAGridViewPartialAddNew(SA_Atendido item, int IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_ATENDIDO_SA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.SA_Atendido;
            item.Fk_IdCampania = IdCampania;
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
                ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

            ViewData["dataItem"] = item;

            return PartialView("_AtendidoSAGridViewPartial", Senasica.GetSAAtendidoByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AtendidoSAGridViewPartialUpdate(SA_Atendido item, int IdCampania, int? IdEstado)
        {            
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_ATENDIDO_SA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.SA_Atendido;
            if (ModelState.IsValid)
            {

                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdSA_Atendido == item.Pk_IdSA_Atendido);
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
                ViewData["EditError"] = "Por favor corrija los errores.";

            ViewData["dataItem"] = item;

            return PartialView("_AtendidoSAGridViewPartial", Senasica.GetSAAtendidoByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AtendidoSAGridViewPartialDelete(System.Int32 Pk_IdSA_Atendido, int IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_ATENDIDO_SA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.SA_Atendido;
            if (Pk_IdSA_Atendido >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdSA_Atendido == Pk_IdSA_Atendido);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_AtendidoSAGridViewPartial", Senasica.GetSAAtendidoByCampania(IdCampania));
        }
    }
}