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
    public class SAImportanciaEnfermedadController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        public ActionResult Index() { return View(); }

        [ValidateInput(false)]
        public ActionResult ImportanciaSAGridViewPartial(int? IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_IMPORTANCIA_SA_ENFERMEDAD);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            return PartialView("_ImportanciaSAGridViewPartial", Senasica.GetSAImportanciaByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ImportanciaSAGridViewPartialAddNew(SA_ImportanciaEnfermedad item, int IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_IMPORTANCIA_SA_ENFERMEDAD);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.SA_ImportanciaEnfermedad;
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
                ViewData["EditError"] = "Por favor Corrija los errores.";

            ViewData["dataItem"] = item;

            return PartialView("_ImportanciaSAGridViewPartial", Senasica.GetSAImportanciaByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ImportanciaSAGridViewPartialUpdate(SA_ImportanciaEnfermedad item, int IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_IMPORTANCIA_SA_ENFERMEDAD);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.SA_ImportanciaEnfermedad;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdSA_ImportanciaEnfermedad == item.Pk_IdSA_ImportanciaEnfermedad);
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

            return PartialView("_ImportanciaSAGridViewPartial", Senasica.GetSAImportanciaByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ImportanciaSAGridViewPartialDelete(System.Int32 Pk_IdSA_ImportanciaEnfermedad, int IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_IMPORTANCIA_SA_ENFERMEDAD);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.SA_ImportanciaEnfermedad;
            if (Pk_IdSA_ImportanciaEnfermedad >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdSA_ImportanciaEnfermedad == Pk_IdSA_ImportanciaEnfermedad);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_ImportanciaSAGridViewPartial", Senasica.GetSAImportanciaByCampania(IdCampania));
        }
     }
}