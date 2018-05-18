using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class ViewFalloController : Controller
    {
        //
        // GET: /ViewFallo/
        public ActionResult Index()
        {
            return View();
        }

        DXMVCWebApplication1.Models.DB_SENASICAEntities db = new DXMVCWebApplication1.Models.DB_SENASICAEntities();

        [ValidateInput(false)]
        public ActionResult GridView1Partial()
        {
            var model = from f in db.Falloes
                        join l in db.Licitacions on f.Fk_IdLicitacion__INFO equals l.Pk_IdLicitacion
                        join e in db.Estadoes on l.Fk_IdEstado__SIS equals e.Pk_IdEstado
                        join ue in db.UnidadEjecutoras on l.Fk_IdUnidadEjecutora__UE equals ue.Pk_IdUnidadEjecutora
                        select new { Pk_IdFallo = f.Pk_IdFallo
                                    ,Estado = e.Nombre
                                    ,UnidadEjecutora = ue.Nombre
                                    ,Licitacion = l.Folio
                                    ,Descripcion = f.Descripcion
                                    ,FechaFallo = f.FechaFallo
                                    ,Fk_IdFiles__INFO = f.Fk_IdFiles__INFO
                                    ,Documento = f.Documento
                                   };
            return PartialView("_GridView1Partial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialAddNew(DXMVCWebApplication1.Models.Fallo item)
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
            return PartialView("_GridView1Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialUpdate(DXMVCWebApplication1.Models.Fallo item)
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
            return PartialView("_GridView1Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialDelete(System.Int32 Pk_IdFallo)
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
            return PartialView("_GridView1Partial", model.ToList());
        }
	}
}