﻿using DXMVCWebApplication1.Common;
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
    public class SVAtendidoController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        // GET: SVAtendido
        public ActionResult Index() { return View(); }
             

        [ValidateInput(false)]
        public ActionResult SvAtendidoGridViewPartial(int? IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_ATENDIDO_SV);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            return PartialView("_SvAtendidoGridViewPartial", Senasica.GetSVAtendidoByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SvAtendidoGridViewPartialAddNew(SVAtendido item, int IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_ATENDIDO_SV);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.SVAtendidoes;
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

            return PartialView("_SvAtendidoGridViewPartial", Senasica.GetSVAtendidoByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SvAtendidoGridViewPartialUpdate(SVAtendido item, int IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_ATENDIDO_SV);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.SVAtendidoes;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdSV_Atendido == item.Pk_IdSV_Atendido);
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
                ViewData["EditError"] = "Por Favor, Corrija los Errores.";

            ViewData["dataItem"] = item;

            return PartialView("_SvAtendidoGridViewPartial", Senasica.GetSVAtendidoByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SvAtendidoGridViewPartialDelete(int Pk_IdSV_Atendido, int IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_ATENDIDO_SV);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.SVAtendidoes;
            if (Pk_IdSV_Atendido >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdSV_Atendido == Pk_IdSV_Atendido);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_SvAtendidoGridViewPartial", Senasica.GetSVAtendidoByCampania(IdCampania));
        }

    }
}