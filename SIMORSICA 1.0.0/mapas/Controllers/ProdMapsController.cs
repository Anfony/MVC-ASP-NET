using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;

namespace senasica.Controllers
{
    public class ProdMapsController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        // GET: ProdMaps
        public ActionResult Index()
        {
            var productors = db.Productors.Include(p => p.Estado).Include(p => p.Municipio);
            return View(productors.ToList());
        }

        // GET: ProdMaps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productor productor = db.Productors.Find(id);
            if (productor == null)
            {
                return HttpNotFound();
            }
            return View(productor);
        }

        // GET: ProdMaps/Create
        public ActionResult Create()
        {
            ViewBag.Fk_IdEstado__SIS = new SelectList(db.Estadoes, "Pk_IdEstado", "Nombre");
            ViewBag.Fk_IdMunicipio__SIS = new SelectList(db.Municipios, "Pk_IdMunicipio", "Nombre");
            return View();
        }

        // POST: ProdMaps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pk_IdProductor,RazonSocial,ApellidoPaterno,ApellidoMaterno,Nombre,Direccion,Colonia,Fk_IdEstado__SIS,Fk_IdMunicipio__SIS,Telefono,Email,Ubicacion")] Productor productor)
        {
            if (ModelState.IsValid)
            {
                db.Productors.Add(productor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Fk_IdEstado__SIS = new SelectList(db.Estadoes, "Pk_IdEstado", "Nombre", productor.Fk_IdEstado__SIS);
            ViewBag.Fk_IdMunicipio__SIS = new SelectList(db.Municipios, "Pk_IdMunicipio", "Nombre", productor.Fk_IdMunicipio__SIS);
            return View(productor);
        }

        // GET: ProdMaps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productor productor = db.Productors.Find(id);
            if (productor == null)
            {
                return HttpNotFound();
            }
            ViewBag.Fk_IdEstado__SIS = new SelectList(db.Estadoes, "Pk_IdEstado", "Nombre", productor.Fk_IdEstado__SIS);
            ViewBag.Fk_IdMunicipio__SIS = new SelectList(db.Municipios, "Pk_IdMunicipio", "Nombre", productor.Fk_IdMunicipio__SIS);
            return View(productor);
        }

        // POST: ProdMaps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pk_IdProductor,RazonSocial,ApellidoPaterno,ApellidoMaterno,Nombre,Direccion,Colonia,Fk_IdEstado__SIS,Fk_IdMunicipio__SIS,Telefono,Email,Ubicacion")] Productor productor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Fk_IdEstado__SIS = new SelectList(db.Estadoes, "Pk_IdEstado", "Nombre", productor.Fk_IdEstado__SIS);
            ViewBag.Fk_IdMunicipio__SIS = new SelectList(db.Municipios, "Pk_IdMunicipio", "Nombre", productor.Fk_IdMunicipio__SIS);
            return View(productor);
        }

        // GET: ProdMaps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productor productor = db.Productors.Find(id);
            if (productor == null)
            {
                return HttpNotFound();
            }
            return View(productor);
        }

        // POST: ProdMaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Productor productor = db.Productors.Find(id);
            db.Productors.Remove(productor);
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
