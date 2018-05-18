using System;
using System.Linq;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Common;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                         + "," + RolesUsuarios.UR_INOCUIDAD
                         + "," + RolesUsuarios.UR_MOVILIZACION
                         + "," + RolesUsuarios.UR_ANIMAL
                         + "," + RolesUsuarios.UR_ACUICOLA_PESQUERA
                         + "," + RolesUsuarios.UR_VEGETAL
        )]
    public class TipoDeAccionController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridView9Partial()
        {
            var model = db.TipoDeAccions;
            return PartialView("_GridView9Partial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView9PartialAddNew(TipoDeAccion item)
        {
            var model = db.TipoDeAccions;
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

            return PartialView("_GridView9Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView9PartialUpdate(TipoDeAccion item)
        {
            var model = db.TipoDeAccions;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IDTipoDeAccion == item.Pk_IDTipoDeAccion);
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

            return PartialView("_GridView9Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView9PartialDelete(System.Int32 Pk_IDTipoDeAccion)
        {
            var model = db.TipoDeAccions;
            if (Pk_IDTipoDeAccion >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IDTipoDeAccion == Pk_IDTipoDeAccion);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridView9Partial", model.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
