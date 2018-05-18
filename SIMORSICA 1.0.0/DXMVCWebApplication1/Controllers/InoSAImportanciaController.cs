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
    public class InoSAImportanciaController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        //
        // GET: /InoSAImportancia/
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult InoImportanciaSAGridViewPartial(int? IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoImportanciaSAs;
            return PartialView("_InoImportanciaSAGridViewPartial", Senasica.GetInoImportanciaSAByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult InoImportanciaSAGridViewPartialAddNew(InoImportanciaSA item, int IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoImportanciaSAs;
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

            return PartialView("_InoImportanciaSAGridViewPartial", Senasica.GetInoImportanciaSAByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InoImportanciaSAGridViewPartialUpdate(InoImportanciaSA item, int IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoImportanciaSAs;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdInoImportanciaSA == item.Pk_IdInoImportanciaSA);
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

            return PartialView("_InoImportanciaSAGridViewPartial", Senasica.GetInoImportanciaSAByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InoImportanciaSAGridViewPartialDelete(System.Int32 Pk_IdInoImportanciaSA, int IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoImportanciaSAs;
            if (Pk_IdInoImportanciaSA >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdInoImportanciaSA == Pk_IdInoImportanciaSA);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_InoImportanciaSA(Pk_IdInoImportanciaSA, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_InoImportanciaSAGridViewPartial", Senasica.GetInoImportanciaSAByCampania(IdCampania));
        }
    }
}