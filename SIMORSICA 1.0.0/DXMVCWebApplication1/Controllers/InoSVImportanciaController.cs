using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                          + "," + RolesUsuarios.IE_VEGETAL
                          + "," + RolesUsuarios.UR_VEGETAL
                          + "," + RolesUsuarios.UR_INOCUIDAD
                          + "," + RolesUsuarios.COORDINADOR_CAMPANIA
                          + "," + RolesUsuarios.PUESTO_COOR_PROYECTO
                          + "," + RolesUsuarios.PUESTO_COOR_REGIONAL
                          + "," + RolesUsuarios.PUESTO_GERENTE
                          + "," + RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                          + "," + RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                          + "," + RolesUsuarios.SUP_ESTATAL
                          + "," + RolesUsuarios.SUP_REGIONAL
   )]
    public class InoSVImportanciaController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        //
        // GET: /InoSVImportancia/
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult InoImportanciaSVGridViewPartial(int? IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoImportanciaSVs;
            return PartialView("_InoImportanciaSVGridViewPartial", Senasica.GetInoImportanciaSVByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult InoImportanciaSVGridViewPartialAddNew(InoImportanciaSV item, int IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoImportanciaSVs;
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

            return PartialView("_InoImportanciaSVGridViewPartial", Senasica.GetInoImportanciaSVByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InoImportanciaSVGridViewPartialUpdate(InoImportanciaSV item, int IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoImportanciaSVs;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdInoImportanciaSV == item.Pk_IdInoImportanciaSV);
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

            return PartialView("_InoImportanciaSVGridViewPartial", Senasica.GetInoImportanciaSVByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InoImportanciaSVGridViewPartialDelete(System.Int32 Pk_IdInoImportanciaSV, int IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoImportanciaSVs;
            if (Pk_IdInoImportanciaSV >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdInoImportanciaSV == Pk_IdInoImportanciaSV);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_InoImportanciaSV(Pk_IdInoImportanciaSV, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_InoImportanciaSVGridViewPartial", Senasica.GetInoImportanciaSVByCampania(IdCampania));
        }
    }
}