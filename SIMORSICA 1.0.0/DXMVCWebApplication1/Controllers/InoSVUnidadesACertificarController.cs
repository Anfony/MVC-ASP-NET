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
    public class InoSVUnidadesACertificarController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        //
        // GET: /InoSVUnidadesACertificar/
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult InoUnidadesCertSVGridViewPartial(int? IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoUnidadesCertSVs;
            return PartialView("_InoUnidadesCertSVGridViewPartial", Senasica.GetInoUnidadesCertSVByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult InoUnidadesCertSVGridViewPartialAddNew(InoUnidadesCertSV item, int IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoUnidadesCertSVs;
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

            return PartialView("_InoUnidadesCertSVGridViewPartial", Senasica.GetInoUnidadesCertSVByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InoUnidadesCertSVGridViewPartialUpdate(InoUnidadesCertSV item, int? IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoUnidadesCertSVs;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdInoUnidadesCertSV == item.Pk_IdInoUnidadesCertSV);
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

            return PartialView("_InoUnidadesCertSVGridViewPartial", Senasica.GetInoUnidadesCertSVByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InoUnidadesCertSVGridViewPartialDelete(System.Int32 Pk_IdInoUnidadesCertSV, int? IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoUnidadesCertSVs;
            if (Pk_IdInoUnidadesCertSV >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdInoUnidadesCertSV == Pk_IdInoUnidadesCertSV);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_InoUnidadesCertSV(Pk_IdInoUnidadesCertSV, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_InoUnidadesCertSVGridViewPartial", Senasica.GetInoUnidadesCertSVByCampania(IdCampania));
        }
    }
}