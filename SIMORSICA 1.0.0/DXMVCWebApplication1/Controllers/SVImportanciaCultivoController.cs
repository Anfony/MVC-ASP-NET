using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                            + "," + RolesUsuarios.IE_VEGETAL
                            + "," + RolesUsuarios.UR_VEGETAL
                            + "," + RolesUsuarios.PUESTO_COOR_PROYECTO
                            + "," + RolesUsuarios.COORDINADOR_CAMPANIA
                            + "," + RolesUsuarios.PUESTO_COOR_REGIONAL
                            + "," + RolesUsuarios.PUESTO_GERENTE
                            + "," + RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                            + "," + RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                            + "," + RolesUsuarios.SUP_ESTATAL
                            + "," + RolesUsuarios.SUP_REGIONAL
     )]
    public class SVImportanciaCultivoController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        public ActionResult Index() { return View(); }

        [ValidateInput(false)]
        public ActionResult SVImportanciaCultivoGridViewPartial(int? IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_IMPORTANCIA_SV);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;
            return PartialView("_SVImportanciaCultivoGridViewPartial", Senasica.GetSVImportanciaCultivoByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SVImportanciaCultivoGridViewPartialAddNew(SVImportanciaCultivo item, int IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_IMPORTANCIA_SV);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            var model = db.SVImportanciaCultivoes;
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
                ViewData["EditError"] = "Ppr favor corrija los errores.";

            ViewData["dataItem"] = item;

            return PartialView("_SVImportanciaCultivoGridViewPartial", Senasica.GetSVImportanciaCultivoByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SVImportanciaCultivoGridViewPartialUpdate(SVImportanciaCultivo item, int IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_IMPORTANCIA_SV);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            var model = db.SVImportanciaCultivoes;
            if (ModelState.IsValid)
            {

                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdSVImportanciaCultivo == item.Pk_IdSVImportanciaCultivo);
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
                ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

            ViewData["dataItem"] = item;

            return PartialView("_SVImportanciaCultivoGridViewPartial", Senasica.GetSVImportanciaCultivoByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SVImportanciaCultivoGridViewPartialDelete(int Pk_IdSVImportanciaCultivo, int IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_IMPORTANCIA_SV);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            var model = db.SVImportanciaCultivoes;
            if (Pk_IdSVImportanciaCultivo >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdSVImportanciaCultivo == Pk_IdSVImportanciaCultivo);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            return PartialView("_SVImportanciaCultivoGridViewPartial", Senasica.GetSVImportanciaCultivoByCampania(IdCampania));
        }
    }
}

