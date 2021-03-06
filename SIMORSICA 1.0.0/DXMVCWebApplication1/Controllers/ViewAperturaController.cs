﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class ViewAperturaController : Controller
    {
        //
        // GET: /ViewApertura/
        public ActionResult Index()
        {
            return View();
        }

        DXMVCWebApplication1.Models.DB_SENASICAEntities db = new DXMVCWebApplication1.Models.DB_SENASICAEntities();

        [ValidateInput(false)]
        public ActionResult GridView2Partial()
        {
            var model = from a in db.Aperturas
                        join l in db.Licitacions on a.Fk_IdLicitacion__INFO equals l.Pk_IdLicitacion
                        join e in db.Estadoes on l.Fk_IdEstado__SIS equals e.Pk_IdEstado
                        join ue in db.UnidadEjecutoras on l.Fk_IdUnidadEjecutora__UE equals ue.Pk_IdUnidadEjecutora
                        select new {
                                     Pk_IdAperturas = a.Pk_IdAperturas
                                    ,Estado = e.Nombre
                                    ,UnidadEjecutora = ue.Nombre
                                    ,Licitacion = l.Folio
                                    ,Descripcion = a.Descripcion
                                    ,FechaApertura = a.FechaApertura
                                    ,Fk_IdFiles__INFO = a.Fk_IdFiles__INFO
                                    ,Documento = a.Documento
                                   };

            return PartialView("_GridView2Partial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView2PartialAddNew(DXMVCWebApplication1.Models.Apertura item)
        {
            var model = db.Aperturas;
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
        public ActionResult GridView2PartialUpdate(DXMVCWebApplication1.Models.Apertura item)
        {
            var model = db.Aperturas;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdAperturas == item.Pk_IdAperturas);
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
        public ActionResult GridView2PartialDelete(System.Int32 Pk_IdAperturas)
        {
            var model = db.Aperturas;
            if (Pk_IdAperturas >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdAperturas == Pk_IdAperturas);
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