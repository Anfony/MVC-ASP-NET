using System.Linq;
using System.Net;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DXMVCWebApplication1.Common;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
    )]
    public class CertificadoPersonaController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        private UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        // GET: /CertificadoPersona/
        #region CodigoOriginal
        public ActionResult Index()
        {
            return View(db.CertificadoPersonas.ToList());
        }

        // GET: /CertificadoPersona/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CertificadoPersona certificadopersona = db.CertificadoPersonas.Find(id);
            if (certificadopersona == null)
            {
                return HttpNotFound();
            }
            return View(certificadopersona);
        }

        // GET: /CertificadoPersona/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /CertificadoPersona/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pk_IdCertificadoPersona,Fk_IdPersona,Fk_IdCertificado,Documento,VigenciaDocumento,FechaRegistro")] CertificadoPersona certificadopersona)
        {
            if (ModelState.IsValid)
            {
                db.CertificadoPersonas.Add(certificadopersona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(certificadopersona);
        }

        // GET: /CertificadoPersona/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CertificadoPersona certificadopersona = db.CertificadoPersonas.Find(id);
            if (certificadopersona == null)
            {
                return HttpNotFound();
            }
            return View(certificadopersona);
        }

        // POST: /CertificadoPersona/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pk_IdCertificadoPersona,Fk_IdPersona,Fk_IdCertificado,Documento,VigenciaDocumento,FechaRegistro")] CertificadoPersona certificadopersona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(certificadopersona).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(certificadopersona);
        }

        // GET: /CertificadoPersona/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CertificadoPersona certificadopersona = db.CertificadoPersonas.Find(id);
            if (certificadopersona == null)
            {
                return HttpNotFound();
            }
            return View(certificadopersona);
        }

        // POST: /CertificadoPersona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CertificadoPersona certificadopersona = db.CertificadoPersonas.Find(id);
            db.CertificadoPersonas.Remove(certificadopersona);
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
