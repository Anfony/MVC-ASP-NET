using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.ViewModels;

namespace DXMVCWebApplication1.Controllers
{
    public class LicitacionController : Controller
    {
        //
        // GET: /Licitacion/
        public ActionResult Index()
        {
            var roll = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            var estado = ((UserData)Session["DataUsuario"]).FK_IdEstado__SIS;// .RolesUsuario[0].ToString();
            
            return View();
        }

        DB_SENASICAEntities db = new DB_SENASICAEntities();

        [ValidateInput(false)]
        public ActionResult GridView4Partial()
        {
            var model = db.Licitacions;
            return PartialView("_GridView4Partial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView4PartialAddNew(DXMVCWebApplication1.Models.Licitacion item)
        {
            var model = db.Licitacions;
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

            ViewData["dataItem"] = item;

            return PartialView("_GridView4Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView4PartialUpdate(DXMVCWebApplication1.Models.Licitacion item)
        {
            var model = db.Licitacions;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdLicitacion == item.Pk_IdLicitacion);
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

            ViewData["dataItem"] = item;
            return PartialView("_GridView4Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView4PartialDelete(System.Int32 Pk_IdLicitacion)
        {
            var model = db.Licitacions;
            if (Pk_IdLicitacion >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdLicitacion == Pk_IdLicitacion);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridView4Partial", model.ToList());
        }
        public ActionResult _ComboBoxComitePartial()
        {
            var IdEstado = Convert.ToInt32(Request.Params["IdEstado"]);
            return PartialView(Senasica.GetUnidadEjecutoraByEstados(IdEstado));
        }
	}
}