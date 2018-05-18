using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using System;
using System.Linq;
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
    public class MOVImportanciaPVISController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        //
        // GET: /MOVImportanciaPVIS/
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ImportanciaPIV_MOVGridViewPartial(int? IdCampania)
        {
            ViewData["IdCampania"] = IdCampania;
            return PartialView("_ImportanciaPIV_MOVGridViewPartial", Senasica.GetMovImportanciaPIVByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ImportanciaPIV_MOVGridViewPartialAddNew(Mov_ImportanciaPIV item, int IdCampania)
        {
            var model = db.Mov_ImportanciaPIV;
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

            return PartialView("_ImportanciaPIV_MOVGridViewPartial", Senasica.GetMovImportanciaPIVByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ImportanciaPIV_MOVGridViewPartialUpdate(Mov_ImportanciaPIV item, int IdCampania)
        {
            var model = db.Mov_ImportanciaPIV;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdMov_ImportanciaPIV == item.Pk_IdMov_ImportanciaPIV);
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

            return PartialView("_ImportanciaPIV_MOVGridViewPartial", Senasica.GetMovImportanciaPIVByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ImportanciaPIV_MOVGridViewPartialDelete(System.Int32 Pk_IdMov_ImportanciaPIV, int IdCampania)
        {
            var model = db.Mov_ImportanciaPIV;
            if (Pk_IdMov_ImportanciaPIV >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdMov_ImportanciaPIV == Pk_IdMov_ImportanciaPIV);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_ImportanciaPIV_MOVGridViewPartial", Senasica.GetMovImportanciaPIVByCampania(IdCampania));
        }
        public ActionResult _ComboBoxMunicipioPartial()
        {
            int Pk_IdEstado = (Request.Params["Fk_IdEstado"] != null) ? int.Parse(Request.Params["Fk_IdEstado"]) : -1;
            return PartialView(Senasica.GetMunicipiosByEstado(Pk_IdEstado));
        }
    }
}