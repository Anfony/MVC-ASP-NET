using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                         + "," + RolesUsuarios.IE_ACUICOLA_PESQUERA
                         + "," + RolesUsuarios.UR_ACUICOLA_PESQUERA
                         + "," + RolesUsuarios.UR_ANIMAL
                         + "," + RolesUsuarios.COORDINADOR_CAMPANIA
                         + "," + RolesUsuarios.PUESTO_COOR_PROYECTO
                         + "," + RolesUsuarios.PUESTO_GERENTE
                         + "," + RolesUsuarios.PUESTO_COOR_REGIONAL
                         + "," + RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                         + "," + RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                         + "," + RolesUsuarios.SUP_ESTATAL
                         + "," + RolesUsuarios.SUP_REGIONAL
        )]
    public class SACImportanciaEconomicaController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        //
        // GET: /SACImportanciaEconomica/
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ImportanciaSACGridViewPartial(int? IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            return PartialView("_ImportanciaSACGridViewPartial", Senasica.GetSACImportanciaCultByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ImportanciaSACGridViewPartialAddNew(SACImportanciaCultivo item, int IdCampania, int? IdEstado)
        {

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.SACImportanciaCultivoes;
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
                ViewData["EditError"] = "Ppr favor corrija los errores.";

            ViewData["dataItem"] = item;

            return PartialView("_ImportanciaSACGridViewPartial", Senasica.GetSACImportanciaCultByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ImportanciaSACGridViewPartialUpdate(SACImportanciaCultivo item, int IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.SACImportanciaCultivoes;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdSACImportanciaCultivo == item.Pk_IdSACImportanciaCultivo);
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
                ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

            ViewData["dataItem"] = item;

            return PartialView("_ImportanciaSACGridViewPartial", Senasica.GetSACImportanciaCultByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ImportanciaSACGridViewPartialDelete(System.Int32 Pk_IdSACImportanciaCultivo, int IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.SACImportanciaCultivoes;
            if (Pk_IdSACImportanciaCultivo >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdSACImportanciaCultivo == Pk_IdSACImportanciaCultivo);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_ImportanciaSACGridViewPartial", Senasica.GetSACImportanciaCultByCampania(IdCampania));
        }
    }
}