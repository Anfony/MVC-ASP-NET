using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class FalloLicitacionController : Controller
    {
        //
        // GET: /FalloLicitacion/
        public ActionResult Index()
        {
            return View();
        }

        DXMVCWebApplication1.Models.DB_SENASICAEntities db = new DXMVCWebApplication1.Models.DB_SENASICAEntities();

        [ValidateInput(false)]
        public ActionResult GridView3Partial()
        {
            var model = db.Falloes;
            return PartialView("_GridView3Partial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView3PartialAddNew(DXMVCWebApplication1.Models.Fallo item)
        {
            var model = db.Falloes;
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

            return PartialView("_GridView3Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView3PartialUpdate(DXMVCWebApplication1.Models.Fallo item)
        {
            var model = db.Falloes;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdFallo == item.Pk_IdFallo);
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

            //-------------------------------------------------------------
            var model2 = db.Falloes;
            return PartialView("_GridView3Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView3PartialDelete(System.Int32 Pk_IdFallo)
        {
            var model = db.Falloes;
            if (Pk_IdFallo >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdFallo == Pk_IdFallo);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridView3Partial", model.ToList());
        }
	}
}