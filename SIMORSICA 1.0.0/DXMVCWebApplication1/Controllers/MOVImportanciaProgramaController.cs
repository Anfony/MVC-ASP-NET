﻿using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                         + "," + RolesUsuarios.IE_MOVILIZACION
                         + "," + RolesUsuarios.UR_MOVILIZACION
                         + "," + RolesUsuarios.IE_VEGETAL
                         + "," + RolesUsuarios.IE_ACUICOLA_PESQUERA
                         + "," + RolesUsuarios.IE_ANIMAL
                         + "," + RolesUsuarios.COORDINADOR_CAMPANIA
                         + "," + RolesUsuarios.PUESTO_COOR_PROYECTO
                         + "," + RolesUsuarios.PUESTO_COOR_REGIONAL
                         + "," + RolesUsuarios.PUESTO_GERENTE
                         + "," + RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                         + "," + RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                         + "," + RolesUsuarios.SUP_ESTATAL
                         + "," + RolesUsuarios.SUP_REGIONAL
    )]
    public class MOVImportanciaProgramaController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        //
        // GET: /MOVImportanciaPrograma/
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ImportanciaProgMovGridViewPartial(int? IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            return PartialView("_ImportanciaProgMovGridViewPartial", Senasica.GetMovImportanciaProgByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ImportanciaProgMovGridViewPartialAddNew(Mov_ImportanciaProg item, int IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.Mov_ImportanciaProg;
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
                ViewData["EditError"] = "Por favor, corrija los errores marcados.";

            ViewData["dataItem"] = item;

            return PartialView("_ImportanciaProgMovGridViewPartial", Senasica.GetMovImportanciaProgByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ImportanciaProgMovGridViewPartialUpdate(Mov_ImportanciaProg item, int IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.Mov_ImportanciaProg;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdMov_ImportanciaProg == item.Pk_IdMov_ImportanciaProg);
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
                ViewData["EditError"] = "Por favor, corrija los errores marcados.";

            ViewData["dataItem"] = item;

            return PartialView("_ImportanciaProgMovGridViewPartial", Senasica.GetMovImportanciaProgByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ImportanciaProgMovGridViewPartialDelete(System.Int32 Pk_IdMov_ImportanciaProg, int IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.Mov_ImportanciaProg;
            if (Pk_IdMov_ImportanciaProg >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdMov_ImportanciaProg == Pk_IdMov_ImportanciaProg);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_ImportanciaProgMovGridViewPartial", Senasica.GetMovImportanciaProgByCampania(IdCampania));
        }
    }
}