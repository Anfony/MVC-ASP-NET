using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class ViewLicitacionController : Controller
    {
        //
        // GET: /ViewLicitacion/
        public ActionResult Index()
        {
            return View();
        }

        DXMVCWebApplication1.Models.DB_SENASICAEntities db = new DXMVCWebApplication1.Models.DB_SENASICAEntities();

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {

            var model = from x in db.Licitacions 
                        join e in db.Estadoes on x.Fk_IdEstado__SIS equals e.Pk_IdEstado
                        join ue in db.UnidadEjecutoras on x.Fk_IdUnidadEjecutora__UE equals ue.Pk_IdUnidadEjecutora
                        select (new {
                                      Pk_IdLicitacion = x.Pk_IdLicitacion 
                                     ,Folio = x.Folio
                                     ,Estado = e.Nombre
                                     ,UnidadEjecutora = ue.Nombre
                                     ,Descripcion = x.Descripcion
                                     ,FechaJuntaAclaracion = x.FechaJuntaAclaracion
                                     ,FechaApertura = x.FechaApertura
                                     ,FechaFallo = x.FechaFallo
                                     ,Fk_IdFiles__INFO = x.Fk_IdFiles__INFO
                                     ,Documento = x.Documento
                                    });

            return PartialView("_GridViewPartial", model.ToList());
        }

//        [HttpPost, ValidateInput(false)]
//        public ActionResult GridViewPartialAddNew(DXMVCWebApplication1.Models.Licitacion item)
//        {
//            var model = db.Licitacions;
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    model.Add(item);
//                    db.SaveChanges();
//                }
//                catch (Exception e)
//                {
//                    ViewData["EditError"] = e.Message;
//                }
//            }
//            else
//                ViewData["EditError"] = "Please, correct all errors.";
//            return PartialView("_GridViewPartial", model.ToList());
//        }
//        [HttpPost, ValidateInput(false)]
//        public ActionResult GridViewPartialUpdate(DXMVCWebApplication1.Models.Licitacion item)
//        {
//            var model = db.Licitacions;
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    var modelItem = model.FirstOrDefault(it => it.Pk_IdLicitacion == item.Pk_IdLicitacion);
//                    if (modelItem != null)
//                    {
//                        this.UpdateModel(modelItem);
//                        db.SaveChanges();
//                    }
//                }
//                catch (Exception e)
//                {
//                    ViewData["EditError"] = e.Message;
//                }
//            }
//            else
//                ViewData["EditError"] = "Please, correct all errors.";
//            return PartialView("_GridViewPartial", model.ToList());
//        }
//        [HttpPost, ValidateInput(false)]
//        public ActionResult GridViewPartialDelete(System.Int32 Pk_IdLicitacion)
//        {
//            var model = db.Licitacions;
//            if (Pk_IdLicitacion >= 0)
//            {
//                try
//                {
//                    var item = model.FirstOrDefault(it => it.Pk_IdLicitacion == Pk_IdLicitacion);
//                    if (item != null)
//                        model.Remove(item);
//                    db.SaveChanges();
//                }
//                catch (Exception e)
//                {
//                    ViewData["EditError"] = e.Message;
//                }
//            }
//            return PartialView("_GridViewPartial", model.ToList());
//        }
	}
}