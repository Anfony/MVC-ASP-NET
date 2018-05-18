using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;

namespace DXMVCWebApplication1.Controllers
{
    public class Default1Controller : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        // GET: /Default1/
        public ActionResult Index()
        {
            var accions = db.Accions.Include(a => a.Persona).Include(a => a.StatusAlcance).Include(a => a.TipoDeAccion).Include(a => a.TipoDeRecurso).Include(a => a.UnidadDeMedida).Include(a => a.Campania);
            return View(accions.ToList());
        }

        // GET: /Default1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accion accion = db.Accions.Find(id);
            if (accion == null)
            {
                return HttpNotFound();
            }
            return View(accion);
        }

        // GET: /Default1/Create
        public ActionResult Create()
        {
            ViewBag.FK_IdPersona__SIS = new SelectList(db.Personas, "Pk_IdPersona", "Ap_Paterno");
            ViewBag.Fk_IdStatusAlcance__SIS = new SelectList(db.StatusAlcances, "PK_IdStatusAlcance", "Nombre");
            ViewBag.Fk_IdTipoDeAccion__SIS = new SelectList(db.TipoDeAccions, "Pk_IDTipoDeAccion", "Nombre");
            ViewBag.Fk_IdTipoDeRecurso__SIS = new SelectList(db.TipoDeRecursoes, "Pk_IdTipoDeRecurso", "Nombre");
            ViewBag.Fk_IdUnidadDeMedida__SIS = new SelectList(db.UnidadDeMedidas, "Pk_IdUnidadDeMedida", "Nombre");
            ViewBag.Fk_IdCampania__UE = new SelectList(db.Campanias, "Pk_IdCampania", "NombreCampania");
            return View();
        }

        // POST: /Default1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Pk_IdAccion,Fk_IdCampania__UE,Fk_IdTipoDeRecurso__SIS,Fk_IdTipoDeAccion__SIS,Fk_IdUnidadDeMedida__SIS,FK_IdPersona__SIS,Actividad,Indicador,Meta_Ene,Meta_Feb,Meta_Mar,Meta_Abr,Meta_May,Meta_Jun,Meta_Jul,Meta_Ago,Meta_Sep,Meta_Oct,Meta_Nov,Meta_Dic,Costo,Justificacion,Fk_IdStatusAlcance__SIS,imp_Ene,imp_Feb,imp_Mar,imp_Abr,imp_May,imp_Jun,imp_Jul,imp_Ago,imp_Sep,imp_Oct,imp_Nov,imp_Dic")] Accion accion)
        {
            if (ModelState.IsValid)
            {
                db.Accions.Add(accion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_IdPersona__SIS = new SelectList(db.Personas, "Pk_IdPersona", "Ap_Paterno", accion.FK_IdPersona__SIS);
            ViewBag.Fk_IdStatusAlcance__SIS = new SelectList(db.StatusAlcances, "PK_IdStatusAlcance", "Nombre", accion.Fk_IdStatusAlcance__SIS);
            ViewBag.Fk_IdTipoDeAccion__SIS = new SelectList(db.TipoDeAccions, "Pk_IDTipoDeAccion", "Nombre", accion.Fk_IdTipoDeAccion__SIS);
            ViewBag.Fk_IdTipoDeRecurso__SIS = new SelectList(db.TipoDeRecursoes, "Pk_IdTipoDeRecurso", "Nombre", accion.Fk_IdTipoDeRecurso__SIS);
            ViewBag.Fk_IdUnidadDeMedida__SIS = new SelectList(db.UnidadDeMedidas, "Pk_IdUnidadDeMedida", "Nombre", accion.Fk_IdUnidadDeMedida__SIS);
            ViewBag.Fk_IdCampania__UE = new SelectList(db.Campanias, "Pk_IdCampania", "NombreCampania", accion.Fk_IdCampania__UE);
            return View(accion);
        }

        // GET: /Default1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accion accion = db.Accions.Find(id);
            if (accion == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_IdPersona__SIS = new SelectList(db.Personas, "Pk_IdPersona", "Ap_Paterno", accion.FK_IdPersona__SIS);
            ViewBag.Fk_IdStatusAlcance__SIS = new SelectList(db.StatusAlcances, "PK_IdStatusAlcance", "Nombre", accion.Fk_IdStatusAlcance__SIS);
            ViewBag.Fk_IdTipoDeAccion__SIS = new SelectList(db.TipoDeAccions, "Pk_IDTipoDeAccion", "Nombre", accion.Fk_IdTipoDeAccion__SIS);
            ViewBag.Fk_IdTipoDeRecurso__SIS = new SelectList(db.TipoDeRecursoes, "Pk_IdTipoDeRecurso", "Nombre", accion.Fk_IdTipoDeRecurso__SIS);
            ViewBag.Fk_IdUnidadDeMedida__SIS = new SelectList(db.UnidadDeMedidas, "Pk_IdUnidadDeMedida", "Nombre", accion.Fk_IdUnidadDeMedida__SIS);
            ViewBag.Fk_IdCampania__UE = new SelectList(db.Campanias, "Pk_IdCampania", "NombreCampania", accion.Fk_IdCampania__UE);
            return View(accion);
        }

        // POST: /Default1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Pk_IdAccion,Fk_IdCampania__UE,Fk_IdTipoDeRecurso__SIS,Fk_IdTipoDeAccion__SIS,Fk_IdUnidadDeMedida__SIS,FK_IdPersona__SIS,Actividad,Indicador,Meta_Ene,Meta_Feb,Meta_Mar,Meta_Abr,Meta_May,Meta_Jun,Meta_Jul,Meta_Ago,Meta_Sep,Meta_Oct,Meta_Nov,Meta_Dic,Costo,Justificacion,Fk_IdStatusAlcance__SIS,imp_Ene,imp_Feb,imp_Mar,imp_Abr,imp_May,imp_Jun,imp_Jul,imp_Ago,imp_Sep,imp_Oct,imp_Nov,imp_Dic")] Accion accion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_IdPersona__SIS = new SelectList(db.Personas, "Pk_IdPersona", "Ap_Paterno", accion.FK_IdPersona__SIS);
            ViewBag.Fk_IdStatusAlcance__SIS = new SelectList(db.StatusAlcances, "PK_IdStatusAlcance", "Nombre", accion.Fk_IdStatusAlcance__SIS);
            ViewBag.Fk_IdTipoDeAccion__SIS = new SelectList(db.TipoDeAccions, "Pk_IDTipoDeAccion", "Nombre", accion.Fk_IdTipoDeAccion__SIS);
            ViewBag.Fk_IdTipoDeRecurso__SIS = new SelectList(db.TipoDeRecursoes, "Pk_IdTipoDeRecurso", "Nombre", accion.Fk_IdTipoDeRecurso__SIS);
            ViewBag.Fk_IdUnidadDeMedida__SIS = new SelectList(db.UnidadDeMedidas, "Pk_IdUnidadDeMedida", "Nombre", accion.Fk_IdUnidadDeMedida__SIS);
            ViewBag.Fk_IdCampania__UE = new SelectList(db.Campanias, "Pk_IdCampania", "NombreCampania", accion.Fk_IdCampania__UE);
            return View(accion);
        }

        // GET: /Default1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accion accion = db.Accions.Find(id);
            if (accion == null)
            {
                return HttpNotFound();
            }
            return View(accion);
        }

        // POST: /Default1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accion accion = db.Accions.Find(id);
            db.Accions.Remove(accion);
            db.SaveChanges();
            return RedirectToAction("Index");
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
