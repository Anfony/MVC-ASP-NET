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
    public class SandyController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        // GET: /Sandy/
        public ActionResult Index()
        {
            var unidadejecutoras = db.UnidadEjecutoras.Include(u => u.Estado).Include(u => u.TipoDeUnidad).Include(u => u.UnidadResponsable).Include(u => u.Municipio).Include(u => u.StatusUE);
            return View(unidadejecutoras.ToList());
        }

        // GET: /Sandy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnidadEjecutora unidadejecutora = db.UnidadEjecutoras.Find(id);
            if (unidadejecutora == null)
            {
                return HttpNotFound();
            }
            return View(unidadejecutora);
        }

        // GET: /Sandy/Create
        public ActionResult Create()
        {
            ViewBag.Fk_IdEstado__SIS = new SelectList(db.Estadoes, "Pk_IdEstado", "Nombre");
            ViewBag.Fk_IdTipoDeUnidad__SIS = new SelectList(db.TipoDeUnidads, "Pk_IdTipoDeUnidad", "Nombre");
            ViewBag.Fk_IdUnidadResponsable__UE = new SelectList(db.UnidadResponsables, "Pk_IdUnidadResponsable", "Nombre");
            ViewBag.Fk_IdMunicipio = new SelectList(db.Municipios, "Pk_IdMunicipio", "Nombre");
            ViewBag.Fk_IdStatusUE = new SelectList(db.StatusUEs, "Pk_IdStatus", "Nombre");
            return View();
        }

        // POST: /Sandy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Pk_IdUnidadEjecutora,Fk_IdTipoDeUnidad__SIS,Nombre,Fk_IdEstado__SIS,Fk_IdMunicipio,Direccion,Colonia,Fk_IdUnidadResponsable__UE,CorreoElectronico,Ubicacion,Telefono,Fk_IdStatusUE")] UnidadEjecutora unidadejecutora)
        {
            if (ModelState.IsValid)
            {
                db.UnidadEjecutoras.Add(unidadejecutora);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Fk_IdEstado__SIS = new SelectList(db.Estadoes, "Pk_IdEstado", "Nombre", unidadejecutora.Fk_IdEstado__SIS);
            ViewBag.Fk_IdTipoDeUnidad__SIS = new SelectList(db.TipoDeUnidads, "Pk_IdTipoDeUnidad", "Nombre", unidadejecutora.Fk_IdTipoDeUnidad__SIS);
            ViewBag.Fk_IdUnidadResponsable__UE = new SelectList(db.UnidadResponsables, "Pk_IdUnidadResponsable", "Nombre", unidadejecutora.Fk_IdUnidadResponsable__UE);
            ViewBag.Fk_IdMunicipio = new SelectList(db.Municipios, "Pk_IdMunicipio", "Nombre", unidadejecutora.Fk_IdMunicipio);
            ViewBag.Fk_IdStatusUE = new SelectList(db.StatusUEs, "Pk_IdStatus", "Nombre", unidadejecutora.Fk_IdStatusUE);
            return View(unidadejecutora);
        }

        // GET: /Sandy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnidadEjecutora unidadejecutora = db.UnidadEjecutoras.Find(id);
            if (unidadejecutora == null)
            {
                return HttpNotFound();
            }
            ViewBag.Fk_IdEstado__SIS = new SelectList(db.Estadoes, "Pk_IdEstado", "Nombre", unidadejecutora.Fk_IdEstado__SIS);
            ViewBag.Fk_IdTipoDeUnidad__SIS = new SelectList(db.TipoDeUnidads, "Pk_IdTipoDeUnidad", "Nombre", unidadejecutora.Fk_IdTipoDeUnidad__SIS);
            ViewBag.Fk_IdUnidadResponsable__UE = new SelectList(db.UnidadResponsables, "Pk_IdUnidadResponsable", "Nombre", unidadejecutora.Fk_IdUnidadResponsable__UE);
            ViewBag.Fk_IdMunicipio = new SelectList(db.Municipios, "Pk_IdMunicipio", "Nombre", unidadejecutora.Fk_IdMunicipio);
            ViewBag.Fk_IdStatusUE = new SelectList(db.StatusUEs, "Pk_IdStatus", "Nombre", unidadejecutora.Fk_IdStatusUE);
            return View(unidadejecutora);
        }

        // POST: /Sandy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Pk_IdUnidadEjecutora,Fk_IdTipoDeUnidad__SIS,Nombre,Fk_IdEstado__SIS,Fk_IdMunicipio,Direccion,Colonia,Fk_IdUnidadResponsable__UE,CorreoElectronico,Ubicacion,Telefono,Fk_IdStatusUE")] UnidadEjecutora unidadejecutora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unidadejecutora).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Fk_IdEstado__SIS = new SelectList(db.Estadoes, "Pk_IdEstado", "Nombre", unidadejecutora.Fk_IdEstado__SIS);
            ViewBag.Fk_IdTipoDeUnidad__SIS = new SelectList(db.TipoDeUnidads, "Pk_IdTipoDeUnidad", "Nombre", unidadejecutora.Fk_IdTipoDeUnidad__SIS);
            ViewBag.Fk_IdUnidadResponsable__UE = new SelectList(db.UnidadResponsables, "Pk_IdUnidadResponsable", "Nombre", unidadejecutora.Fk_IdUnidadResponsable__UE);
            ViewBag.Fk_IdMunicipio = new SelectList(db.Municipios, "Pk_IdMunicipio", "Nombre", unidadejecutora.Fk_IdMunicipio);
            ViewBag.Fk_IdStatusUE = new SelectList(db.StatusUEs, "Pk_IdStatus", "Nombre", unidadejecutora.Fk_IdStatusUE);
            return View(unidadejecutora);
        }

        // GET: /Sandy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnidadEjecutora unidadejecutora = db.UnidadEjecutoras.Find(id);
            if (unidadejecutora == null)
            {
                return HttpNotFound();
            }
            return View(unidadejecutora);
        }

        // POST: /Sandy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UnidadEjecutora unidadejecutora = db.UnidadEjecutoras.Find(id);
            db.UnidadEjecutoras.Remove(unidadejecutora);
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
