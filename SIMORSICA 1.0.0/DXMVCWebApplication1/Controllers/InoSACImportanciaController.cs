using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                              + "," + RolesUsuarios.IE_ACUICOLA_PESQUERA
                              + "," + RolesUsuarios.IE_INOCUIDAD
                              + "," + RolesUsuarios.UR_INOCUIDAD
                              + "," + RolesUsuarios.UR_ACUICOLA_PESQUERA
                              + "," + RolesUsuarios.COORDINADOR_CAMPANIA
                              + "," + RolesUsuarios.PUESTO_COOR_PROYECTO
                              + "," + RolesUsuarios.PUESTO_COOR_REGIONAL
                              + "," + RolesUsuarios.PUESTO_GERENTE
                              + "," + RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                              + "," + RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                              + "," + RolesUsuarios.SUP_ESTATAL
                              + "," + RolesUsuarios.SUP_REGIONAL
             )]
    public class InoSACImportanciaController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        //
        // GET: /InoSACImportancia/
        public ActionResult Index()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult InoImportanciaSACGridViewPartial(int? IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoImportanciaSACs;
            return PartialView("_InoImportanciaSACGridViewPartial", Senasica.GetInoImportanciaSACByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult InoImportanciaSACGridViewPartialAddNew(InoImportanciaSAC item, int IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoImportanciaSACs;
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

            return PartialView("_InoImportanciaSACGridViewPartial", Senasica.GetInoImportanciaSACByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InoImportanciaSACGridViewPartialUpdate(InoImportanciaSAC item, int IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoImportanciaSACs;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdInoImportanciaSAC == item.Pk_IdInoImportanciaSAC);
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

            return PartialView("_InoImportanciaSACGridViewPartial", Senasica.GetInoImportanciaSACByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InoImportanciaSACGridViewPartialDelete(System.Int32 Pk_IdInoImportanciaSAC, int IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoImportanciaSACs;
            if (Pk_IdInoImportanciaSAC >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdInoImportanciaSAC == Pk_IdInoImportanciaSAC);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_InoImportanciaSAC(Pk_IdInoImportanciaSAC, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_InoImportanciaSACGridViewPartial", Senasica.GetInoImportanciaSACByCampania(IdCampania));
        }
    }
}