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
    public class StatusAccionController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        // GET: /StatusAccion/
        #region Original
        public ActionResult Index()
        {
            return View(db.StatusAccions.ToList());
        }

        // GET: /StatusAccion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusAccion statusaccion = db.StatusAccions.Find(id);
            if (statusaccion == null)
            {
                return HttpNotFound();
            }
            return View(statusaccion);
        }

        // GET: /StatusAccion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /StatusAccion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_IdStatusAccion,Nombre")] StatusAccion statusaccion)
        {
            if (ModelState.IsValid)
            {
                db.StatusAccions.Add(statusaccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statusaccion);
        }

        // GET: /StatusAccion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusAccion statusaccion = db.StatusAccions.Find(id);
            if (statusaccion == null)
            {
                return HttpNotFound();
            }
            return View(statusaccion);
        }

        // POST: /StatusAccion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_IdStatusAccion,Nombre")] StatusAccion statusaccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusaccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statusaccion);
        }

        // GET: /StatusAccion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusAccion statusaccion = db.StatusAccions.Find(id);
            if (statusaccion == null)
            {
                return HttpNotFound();
            }
            return View(statusaccion);
        }

        // POST: /StatusAccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StatusAccion statusaccion = db.StatusAccions.Find(id);
            db.StatusAccions.Remove(statusaccion);
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
