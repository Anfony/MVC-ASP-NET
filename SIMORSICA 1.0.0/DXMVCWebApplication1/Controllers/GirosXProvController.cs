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
    public class GirosXProvController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        // GET: /GirosXProv/
        #region CodigoOriginal_Deprecado
        public ActionResult Index()
        {
            var girosxprovs = db.GirosXProvs.Include(g => g.TipoDeBien);
            return View(girosxprovs.ToList());
        }

        // GET: /GirosXProv/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GirosXProv girosxprov = db.GirosXProvs.Find(id);
            if (girosxprov == null)
            {
                return HttpNotFound();
            }
            return View(girosxprov);
        }

        // GET: /GirosXProv/Create
        public ActionResult Create()
        {
            ViewBag.Fk_IdProveedor = new SelectList(db.Estadoes, "Pk_IdEstado", "Nombre");
            ViewBag.Fk_IdTipoDeBien = new SelectList(db.TipoDeBiens, "Pk_IdTipoDeBien", "Nombre");
            return View();
        }

        // POST: /GirosXProv/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_IdGiroXProv,Fk_IdProveedor,Fk_IdTipoDeBien")] GirosXProv girosxprov)
        {
            if (ModelState.IsValid)
            {
                db.GirosXProvs.Add(girosxprov);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Fk_IdProveedor = new SelectList(db.Estadoes, "Pk_IdEstado", "Nombre", girosxprov.Fk_IdProveedor);
            ViewBag.Fk_IdTipoDeBien = new SelectList(db.TipoDeBiens, "Pk_IdTipoDeBien", "Nombre", girosxprov.Fk_IdTipoDeBien);
            return View(girosxprov);
        }

        // GET: /GirosXProv/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GirosXProv girosxprov = db.GirosXProvs.Find(id);
            if (girosxprov == null)
            {
                return HttpNotFound();
            }
            ViewBag.Fk_IdProveedor = new SelectList(db.Estadoes, "Pk_IdEstado", "Nombre", girosxprov.Fk_IdProveedor);
            ViewBag.Fk_IdTipoDeBien = new SelectList(db.TipoDeBiens, "Pk_IdTipoDeBien", "Nombre", girosxprov.Fk_IdTipoDeBien);
            return View(girosxprov);
        }

        // POST: /GirosXProv/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_IdGiroXProv,Fk_IdProveedor,Fk_IdTipoDeBien")] GirosXProv girosxprov)
        {
            if (ModelState.IsValid)
            {
                db.Entry(girosxprov).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Fk_IdProveedor = new SelectList(db.Estadoes, "Pk_IdEstado", "Nombre", girosxprov.Fk_IdProveedor);
            ViewBag.Fk_IdTipoDeBien = new SelectList(db.TipoDeBiens, "Pk_IdTipoDeBien", "Nombre", girosxprov.Fk_IdTipoDeBien);
            return View(girosxprov);
        }

        // GET: /GirosXProv/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GirosXProv girosxprov = db.GirosXProvs.Find(id);
            if (girosxprov == null)
            {
                return HttpNotFound();
            }
            return View(girosxprov);
        }

        // POST: /GirosXProv/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GirosXProv girosxprov = db.GirosXProvs.Find(id);
            db.GirosXProvs.Remove(girosxprov);
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
        #endregion
    }
}
