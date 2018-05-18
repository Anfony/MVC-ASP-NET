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
    public class InoSACAtendidoController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        //
        // GET: /InoSACAtendido/
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult InoCompSACGridViewPartial(int? IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoCompSACs;
            return PartialView("_InoCompSACGridViewPartial", Senasica.GetInoCompSACByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult InoCompSACGridViewPartialAddNew(InoCompSAC item, int IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoCompSACs;
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

            return PartialView("_InoCompSACGridViewPartial", Senasica.GetInoCompSACByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InoCompSACGridViewPartialUpdate(InoCompSAC item, int? IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoCompSACs;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdInoCompSAC == item.Pk_IdInoCompSAC);
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

            return PartialView("_InoCompSACGridViewPartial", Senasica.GetInoCompSACByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InoCompSACGridViewPartialDelete(System.Int32 Pk_IdInoCompSAC, int? IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoCompSACs;
            if (Pk_IdInoCompSAC >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdInoCompSAC == Pk_IdInoCompSAC);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_InoCompSAC(Pk_IdInoCompSAC, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_InoCompSACGridViewPartial", Senasica.GetInoCompSACByCampania(IdCampania));
        }
    }
}