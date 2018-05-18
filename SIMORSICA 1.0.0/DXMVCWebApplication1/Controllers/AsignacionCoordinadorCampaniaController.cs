using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.IE_INOCUIDAD
                     + "," + RolesUsuarios.IE_MOVILIZACION
                     + "," + RolesUsuarios.IE_ANIMAL
                     + "," + RolesUsuarios.IE_ACUICOLA_PESQUERA
                     + "," + RolesUsuarios.IE_VEGETAL
                     + "," + RolesUsuarios.SUP_ESTATAL
                     + "," + RolesUsuarios.IE_PROGRAMAS_U
   )]
    public class AsignacionCoordinadorCampaniaController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        public ActionResult Index() { return View(); }

        [ValidateInput(false)]
        public ActionResult CoordinadorCampaniaGridViewPartial()
        {
            return PartialView("_CoordinadorCampaniaGridViewPartial", Senasica.GetCampaniaByIE());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CoordinadorCampaniaUpdate(CoordinadorByCampaniaVM item)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var model = db.Campanias;

                    var modelItem = model.FirstOrDefault(it => it.Pk_IdCampania == item.Pk_IdCampania);
                    if (modelItem != null)
                    {
                        UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Por favor corrija los errores marcados.";

            ViewData["dataItem"] = item;

            return PartialView("_CoordinadorCampaniaGridViewPartial", Senasica.GetCampaniaByIE());
        }
    }
}