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
    public class InoSVAtendidoController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        //
        // GET: /InoSVAtendido/
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult InoCompSVGridViewPartial(int? IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoCompSVs;
            return PartialView("_InoCompSVGridViewPartial", Senasica.GetInoCompSVByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult InoCompSVGridViewPartialAddNew(InoCompSV item, int IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoCompSVs;
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

            return PartialView("_InoCompSVGridViewPartial", Senasica.GetInoCompSVByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InoCompSVGridViewPartialUpdate(InoCompSV item, int? IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoCompSVs;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdInoCompSV == item.Pk_IdInoCompSV);
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

            return PartialView("_InoCompSVGridViewPartial", Senasica.GetInoCompSVByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InoCompSVGridViewPartialDelete(System.Int32 Pk_IdInoCompSV, int? IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoCompSVs;
            if (Pk_IdInoCompSV >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdInoCompSV == Pk_IdInoCompSV);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_InoCompSV(Pk_IdInoCompSV, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_InoCompSVGridViewPartial", Senasica.GetInoCompSVByCampania(IdCampania));
        }
    }
}