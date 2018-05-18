using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class JuntaAclaracionController : Controller
    {
        //
        // GET: /JuntaAclaracion/
        public ActionResult Index()
        {
            return View();
        }

        DXMVCWebApplication1.Models.DB_SENASICAEntities db = new DXMVCWebApplication1.Models.DB_SENASICAEntities();

        [ValidateInput(false)]
        public ActionResult GridView2Partial()
        {
            var model = db.JuntaAclaracions;
                          
            return PartialView("_GridView2Partial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView2PartialAddNew(DXMVCWebApplication1.Models.JuntaAclaracion item)
        {
            var model = db.JuntaAclaracions;
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
            return PartialView("_GridView2Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView2PartialUpdate(DXMVCWebApplication1.Models.JuntaAclaracion item)
        {
            var model = db.JuntaAclaracions;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdJuntaAclaracion == item.Pk_IdJuntaAclaracion);
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

            return PartialView("_GridView2Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView2PartialDelete(System.Int32 Pk_IdJuntaAclaracion)
        {
            var model = db.JuntaAclaracions;
            if (Pk_IdJuntaAclaracion >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdJuntaAclaracion == Pk_IdJuntaAclaracion);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridView2Partial", model.ToList());
        }
	}
}
