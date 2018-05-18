using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class ViewJuntaAclaracionController : Controller
    {
        //
        // GET: /ViewJuntaAclaracion/
        public ActionResult Index()
        {
            return View();
        }

        DXMVCWebApplication1.Models.DB_SENASICAEntities db = new DXMVCWebApplication1.Models.DB_SENASICAEntities();

        [ValidateInput(false)]
        public ActionResult GridView3Partial()
        {
            var model = from ja in db.JuntaAclaracions
                        join l in db.Licitacions on ja.Fk_IdLicitacion__INFO equals l.Pk_IdLicitacion
                        join e in db.Estadoes on l.Fk_IdEstado__SIS equals e.Pk_IdEstado
                        join ue in db.UnidadEjecutoras on l.Fk_IdUnidadEjecutora__UE equals ue.Pk_IdUnidadEjecutora
                        select new
                        {
                             Pk_IdJuntaAclaracion = ja.Pk_IdJuntaAclaracion
                            ,Estado = e.Nombre
                            ,UnidadEjecutora = ue.Nombre
                            ,Licitacion = l.Folio
                            ,Descripcion = ja.Descripcion
                            ,FechaJuntaAclaracion = ja.FechaJuntaAclaracion
                            ,Fk_IdFiles__INFO = ja.Fk_IdFiles__INFO
                            ,Documento = ja.Documento
                       };
            return PartialView("_GridView3Partial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView3PartialAddNew(DXMVCWebApplication1.Models.JuntaAclaracion item)
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
            return PartialView("_GridView3Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView3PartialUpdate(DXMVCWebApplication1.Models.JuntaAclaracion item)
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
            return PartialView("_GridView3Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView3PartialDelete(System.Int32 Pk_IdJuntaAclaracion)
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
            return PartialView("_GridView3Partial", model.ToList());
        }
	}
}