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
    public class InoSAUnidadesACertificarController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        //
        // GET: /InoSAUnidadesACertificar/
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult InoUnidadesCertSAGridViewPartial(int? IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoUnidadesCertSAs;
            return PartialView("_InoUnidadesCertSAGridViewPartial", Senasica.GetInoUnidadesCertSAByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult InoUnidadesCertSAGridViewPartialAddNew(InoUnidadesCertSA item, int IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoUnidadesCertSAs;
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

            return PartialView("_InoUnidadesCertSAGridViewPartial", Senasica.GetInoUnidadesCertSAByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InoUnidadesCertSAGridViewPartialUpdate(InoUnidadesCertSA item, int? IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoUnidadesCertSAs;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdInoUnidadesCertSA == item.Pk_IdInoUnidadesCertSA);
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

            return PartialView("_InoUnidadesCertSAGridViewPartial", Senasica.GetInoUnidadesCertSAByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InoUnidadesCertSAGridViewPartialDelete(System.Int32 Pk_IdInoUnidadesCertSA, int? IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.InoUnidadesCertSAs;
            if (Pk_IdInoUnidadesCertSA >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdInoUnidadesCertSA == Pk_IdInoUnidadesCertSA);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_InoUnidadesCertSA(Pk_IdInoUnidadesCertSA, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_InoUnidadesCertSAGridViewPartial", Senasica.GetInoUnidadesCertSAByCampania(IdCampania));
        }
    }
}