using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class CatalogoPartidasController : Controller
    {
        //
        // GET: /CatalogoPartidas/
        public ActionResult Index()
        {
            return View();
        }

        DXMVCWebApplication1.Models.DB_SENASICAEntities db = new DXMVCWebApplication1.Models.DB_SENASICAEntities();

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var model = db.CapituloPartidaLicitacions;
            return PartialView("_GridViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(DXMVCWebApplication1.Models.CapituloPartidaLicitacion item)
        {
            var model = db.CapituloPartidaLicitacions;
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
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(DXMVCWebApplication1.Models.CapituloPartidaLicitacion item)
        {
            var model = db.CapituloPartidaLicitacions;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdCapituloPartida == item.Pk_IdCapituloPartida);
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
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.Int32 Pk_IdCapituloPartida)
        {
            var model = db.CapituloPartidaLicitacions;
            if (Pk_IdCapituloPartida >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdCapituloPartida == Pk_IdCapituloPartida);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartial", model.ToList());
        }
	}
}