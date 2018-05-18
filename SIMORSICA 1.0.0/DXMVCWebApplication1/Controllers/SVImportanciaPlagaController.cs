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
    public class SVImportanciaPlagaController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        // GET: SVImportanciaPlaga
        public ActionResult Index() { return View(); }

        [ValidateInput(false)]
        public ActionResult SVImportanciaPlagaGridViewPartial(int? IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_IMPORTANCIA_SV_PROGRAMA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            return PartialView("_SVImportanciaPlagaGridViewPartial", Senasica.GetSVImportanciaPlagaByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SVImportanciaPlagaGridViewPartialAddNew(SVImportanciaPlaga item, int IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_IMPORTANCIA_SV_PROGRAMA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.SVImportanciaPlagas;
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
                ViewData["EditError"] = "Corrija los errores.";

            ViewData["dataItem"] = item;

            return PartialView("_SVImportanciaPlagaGridViewPartial", Senasica.GetSVImportanciaPlagaByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SVImportanciaPlagaGridViewPartialUpdate(SVImportanciaPlaga item, int IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_IMPORTANCIA_SV_PROGRAMA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.SVImportanciaPlagas;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdSVImportanciaPlaga == item.Pk_IdSVImportanciaPlaga);
                    if (modelItem != null)
                    {
                        UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Por Favor, Corrija los Errores.";

            ViewData["dataItem"] = item;

            return PartialView("_SVImportanciaPlagaGridViewPartial", Senasica.GetSVImportanciaPlagaByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SVImportanciaPlagaGridViewPartialDelete(int Pk_IdSVImportanciaPlaga, int IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_IMPORTANCIA_SV_PROGRAMA);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.SVImportanciaPlagas;
            if (Pk_IdSVImportanciaPlaga >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdSVImportanciaPlaga == Pk_IdSVImportanciaPlaga);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_SVImportanciaPlagaGridViewPartial", Senasica.GetSVImportanciaPlagaByCampania(IdCampania));
        }
    }
}