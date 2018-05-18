using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Common;
using DevExpress.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Core.Objects;

namespace DXMVCWebApplication1.Controllers
    {
        [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                          + "," + RolesUsuarios.IE_INOCUIDAD
                          + "," + RolesUsuarios.IE_MOVILIZACION
                          + "," + RolesUsuarios.IE_ANIMAL
                          + "," + RolesUsuarios.IE_ACUICOLA_PESQUERA
                          + "," + RolesUsuarios.IE_VEGETAL
                          + "," + RolesUsuarios.UR_INOCUIDAD
                          + "," + RolesUsuarios.UR_MOVILIZACION
                          + "," + RolesUsuarios.UR_ANIMAL
                          + "," + RolesUsuarios.UR_ACUICOLA_PESQUERA
                          + "," + RolesUsuarios.UR_VEGETAL
         )]
        public class ActividadController : Controller
        {
            private DB_SENASICAEntities db = new DB_SENASICAEntities();
            private UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            // GET: /Actividad/
            #region CodigoOriginal
            public ActionResult Index()
            {
                var actividads = db.Actividads.Include(a => a.Persona);//.Include(a => a.Alcance);
                return View(actividads.ToList());
            }

            // GET: /Actividad/Details/5
            public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Actividad actividad = db.Actividads.Find(id);
                if (actividad == null)
                {
                    return HttpNotFound();
                }
                return View(actividad);
            }

            // GET: /Actividad/Create
            public ActionResult Create()
            {
                ViewBag.Fk_IdPersona__SIS = new SelectList(db.Personas, "Pk_IdPersona", "Ap_Paterno");
                ViewBag.Pk_IdActividad = new SelectList(db.Accions, "Pk_IdAccion", "Actividad");
                return View();
            }

            // POST: /Actividad/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "Pk_IdActividad,Fk_IdAccion__UE,Fk_IdPersona__SIS,Fecha_Inicio,FechaFin,Descripcion,Costo")] Actividad actividad)
            {
                if (ModelState.IsValid)
                {
                    db.Actividads.Add(actividad);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Fk_IdPersona__SIS = new SelectList(db.Personas, "Pk_IdPersona", "Ap_Paterno", actividad.Fk_IdPersona__SIS);
                ViewBag.Pk_IdActividad = new SelectList(db.Accions, "Pk_IdAccion", "Actividad", actividad.Pk_IdActividad);
                return View(actividad);
            }

            // GET: /Actividad/Edit/5
            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Actividad actividad = db.Actividads.Find(id);
                if (actividad == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Fk_IdPersona__SIS = new SelectList(db.Personas, "Pk_IdPersona", "Ap_Paterno", actividad.Fk_IdPersona__SIS);
                ViewBag.Pk_IdActividad = new SelectList(db.Accions, "Pk_IdAccion", "Actividad", actividad.Pk_IdActividad);
                return View(actividad);
            }

            // POST: /Actividad/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "Pk_IdActividad,Fk_IdAccion__UE,Fk_IdPersona__SIS,Fecha_Inicio,FechaFin,Descripcion,Costo")] Actividad actividad)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(actividad).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Fk_IdPersona__SIS = new SelectList(db.Personas, "Pk_IdPersona", "Ap_Paterno", actividad.Fk_IdPersona__SIS);
                ViewBag.Pk_IdActividad = new SelectList(db.Accions, "Pk_IdAccion", "Actividad", actividad.Pk_IdActividad);
                return View(actividad);
            }

            // GET: /Actividad/Delete/5
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Actividad actividad = db.Actividads.Find(id);
                if (actividad == null)
                {
                    return HttpNotFound();
                }
                return View(actividad);
            }

            // POST: /Actividad/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                Actividad actividad = db.Actividads.Find(id);
                db.Actividads.Remove(actividad);
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


            #region Actividades
            DXMVCWebApplication1.Models.DB_SENASICAEntities db1 = new DXMVCWebApplication1.Models.DB_SENASICAEntities();

            [ValidateInput(false)]
            public ActionResult ActividadesGridViewPartial()
            {

                int? _Fk_IdAccion = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdAccion__UE);
                int? _Fk_IdCampania = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdCampania__UE);
                int? UEID = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);

                ViewBag.UEID = UEID;
                ViewBag.Fk_IdCampania = _Fk_IdCampania;

                return PartialView("_ActividadesGridViewPartial", DXMVCWebApplication1.Models.Senasica.GetActividadesByCampania(_Fk_IdCampania, UEID));
            }

            [HttpPost, ValidateInput(false)]
            public ActionResult ActividadesGridViewPartialAddNew(DXMVCWebApplication1.Models.Actividad item)
            {
                int? _Fk_IdAccion = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdAccion__UE);
                int? _Fk_IdCampania = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdCampania__UE);
                int? UEID = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);

                ViewBag.UEID = UEID;
                ViewBag.Fk_IdCampania = _Fk_IdCampania;

                var model = db1.Actividads;
                if (ModelState.IsValid)
                {
                    try
                    {
                        item.CT_User = User.Identity.GetUserId();
                        item.CT_Date = DateTime.Now;
                        model.Add(item);
                        db1.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                    }
                }
                else
                    ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

                ViewData["dataItem"] = item;

                return PartialView("_ActividadesGridViewPartial", DXMVCWebApplication1.Models.Senasica.GetActividadesByCampania(_Fk_IdCampania, UEID));
            }
            [HttpPost, ValidateInput(false)]
            public ActionResult ActividadesGridViewPartialUpdate(DXMVCWebApplication1.Models.Actividad item)
            {
                int? _Fk_IdAccion = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdAccion__UE);
                int? _Fk_IdCampania = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdCampania__UE);
                int? UEID = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);

                ViewBag.UEID = UEID;
                ViewBag.Fk_IdCampania = _Fk_IdCampania;

                var model = db1.Actividads;
                if (ModelState.IsValid)
                {
                    try
                    {
                        var modelItem = model.FirstOrDefault(it => it.Pk_IdActividad == item.Pk_IdActividad);
                        if (modelItem != null)
                        {
                            modelItem.CT_User = User.Identity.GetUserId();
                            modelItem.CT_Date = DateTime.Now;
                            this.UpdateModel(modelItem);
                            db1.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                    }
                }
                else
                    ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

                ViewData["dataItem"] = item;

                return PartialView("_ActividadesGridViewPartial", DXMVCWebApplication1.Models.Senasica.GetActividadesByCampania(_Fk_IdCampania, UEID));
            }
            [HttpPost, ValidateInput(false)]
            public ActionResult ActividadesGridViewPartialDelete(System.Int32 Pk_IdActividad)
            {
                int? _Fk_IdAccion = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdAccion__UE);
                int? _Fk_IdCampania = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdCampania__UE);
                int? UEID = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);

                ViewBag.UEID = UEID;
                ViewBag.Fk_IdCampania = _Fk_IdCampania;

                var model = db1.Actividads;
                if (Pk_IdActividad >= 0)
                {
                    try
                    {
                        var item = model.FirstOrDefault(it => it.Pk_IdActividad == Pk_IdActividad);
                        if (item != null)
                        {
                            ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                            db.SP_DELETE_Actividad(Pk_IdActividad, User.Identity.GetUserId(), Error);
                        }
                    db1.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                    }
                }
                return PartialView("_ActividadesGridViewPartial", DXMVCWebApplication1.Models.Senasica.GetActividadesByCampania(_Fk_IdCampania, UEID));
            } 
            #endregion
        }

    }
