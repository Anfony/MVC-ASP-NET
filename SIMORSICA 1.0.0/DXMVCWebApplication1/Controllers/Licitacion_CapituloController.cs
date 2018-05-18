using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DXMVCWebApplication1.Models;

namespace DXMVCWebApplication1.Controllers
{
    public class Licitacion_CapituloController : Controller
    {
        DB_SENASICAEntities db = new DB_SENASICAEntities();
        //
        // GET: /Licitacion_Capitulo/
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult _Licitacion_CapituloGridViewPartial(int? Pk_IdLicitacion)
        {
            
            var query = db.Licitacion_Capitulo.Where(x => x.Fk_IdLicitacion__INFO == Pk_IdLicitacion);

            ViewData["Pk_IdLicitacion"] = Pk_IdLicitacion;
            
            return PartialView("__Licitacion_CapituloGridViewPartial", query.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult _Licitacion_CapituloGridViewPartialAddNew(DXMVCWebApplication1.Models.Licitacion_Capitulo item, int? Pk_IdLicitacion)
        {
            var model = db.Licitacion_Capitulo;
            if (ModelState.IsValid)
            {
                try
                {
                    item.Fk_IdLicitacion__INFO = Pk_IdLicitacion;
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("__Licitacion_CapituloGridViewPartial", model.Where(x => x.Fk_IdLicitacion__INFO == Pk_IdLicitacion).ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult _Licitacion_CapituloGridViewPartialUpdate(DXMVCWebApplication1.Models.Licitacion_Capitulo item, int? Pk_IdLicitacion)
        {
            var model = db.Licitacion_Capitulo;
            var modelItem = model.FirstOrDefault(it => it.Pk_IdLicitacion_Capitulo == item.Pk_IdLicitacion_Capitulo);

            if (ModelState.IsValid)
            {
                try
                {
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
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("__Licitacion_CapituloGridViewPartial", model.Where(x => x.Fk_IdLicitacion__INFO == Pk_IdLicitacion).ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult _Licitacion_CapituloGridViewPartialDelete(System.Int32 Pk_IdLicitacion_Capitulo)
        {
            var model = new object[0];
            if (Pk_IdLicitacion_Capitulo >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("__Licitacion_CapituloGridViewPartial", model);
        }
	}
}