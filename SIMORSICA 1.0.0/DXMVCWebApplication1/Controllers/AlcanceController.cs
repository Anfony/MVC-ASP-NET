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

namespace DXMVCWebApplication1.Controllers
{
    //test
    [Authorize(Roles = "Admin")]
    public class AlcanceController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        private UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        // GET: /Alcance/
        #region CodigoOriginal
        public ActionResult Index()
        {
            var Acciones = db.Accions.Include(a => a.Persona).Include(a => a.TipoDeAccion).Include(a => a.TipoDeRecurso).Include(a => a.UnidadDeMedida).Include(a => a.Campania);
            return View(Acciones.ToList());
        }

        // GET: /Alcance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accion alcance = db.Accions.Find(id);
            if (alcance == null)
            {
                return HttpNotFound();
            }
            return View(alcance);
        }

        // GET: /Alcance/Create
        public ActionResult Create()
        {
            ViewBag.FK_IdPersona__SIS = new SelectList(db.Personas, "Pk_IdPersona", "Ap_Paterno");
            ViewBag.Fk_IdTipoDeAccion__SIS = new SelectList(db.TipoDeAccions, "Pk_IDTipoDeAccion", "Nombre");
            ViewBag.Fk_IdTipoDeRecurso__SIS = new SelectList(db.TipoDeRecursoes, "Pk_IdTipoDeRecurso", "Nombre");
            ViewBag.Fk_IdUnidadDeMedida__SIS = new SelectList(db.UnidadDeMedidas, "Pk_IdUnidadDeMedida", "Nombre");
            ViewBag.Pk_IdAccion = new SelectList(db.Actividads, "Pk_IdActividad", "Descripcion");
            ViewBag.Fk_IdCampania__UE = new SelectList(db.Campanias, "Pk_IdCampania", "NombreCampania");
            return View();
        }

        // POST: /Alcance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pk_IdAccion,Fk_IdCampania__UE,Fk_IdTipoDeRecurso__SIS,Fk_IdTipoDeAccion__SIS,Fk_IdUnidadDeMedida__SIS,FK_IdPersona__SIS,Actividad,Indicador,Meta_Ene,Meta_Feb,Meta_Mar,Meta_Abr,Meta_May,Meta_Jun,Meta_Jul,Meta_Ago,Meta_Sep,Meta_Oct,Meta_Nov,Meta_Dic,Costo,Justificacion,Fk_IdStatusAlcance__SIS")] Accion accion)
        {
            if (ModelState.IsValid)
            {
                db.Accions.Add(accion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_IdPersona__SIS = new SelectList(db.Personas, "Pk_IdPersona", "Ap_Paterno", accion.FK_IdPersona__SIS);
            ViewBag.Fk_IdTipoDeAccion__SIS = new SelectList(db.TipoDeAccions, "Pk_IDTipoDeAccion", "Nombre", accion.Fk_IdTipoDeAccion__SIS);
            ViewBag.Fk_IdTipoDeRecurso__SIS = new SelectList(db.TipoDeRecursoes, "Pk_IdTipoDeRecurso", "Nombre", accion.Fk_IdTipoDeRecurso__SIS);
            ViewBag.Fk_IdUnidadDeMedida__SIS = new SelectList(db.UnidadDeMedidas, "Pk_IdUnidadDeMedida", "Nombre", accion.Fk_IdUnidadDeMedida__SIS);
            ViewBag.Pk_IdAccion = new SelectList(db.Actividads, "Pk_IdActividad", "Descripcion", accion.Pk_IdAccion);
            ViewBag.Fk_IdCampania__UE = new SelectList(db.Campanias, "Pk_IdCampania", "NombreCampania", accion.Fk_IdCampania__UE);
            return View(accion);
        }

        // GET: /Alcance/Edit/5
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
            ViewBag.Fk_IdTipoDeAccion__SIS = new SelectList(db.TipoDeAccions, "Pk_IDTipoDeAccion", "Nombre", accion.Fk_IdTipoDeAccion__SIS);
            ViewBag.Fk_IdTipoDeRecurso__SIS = new SelectList(db.TipoDeRecursoes, "Pk_IdTipoDeRecurso", "Nombre", accion.Fk_IdTipoDeRecurso__SIS);
            ViewBag.Fk_IdUnidadDeMedida__SIS = new SelectList(db.UnidadDeMedidas, "Pk_IdUnidadDeMedida", "Nombre", accion.Fk_IdUnidadDeMedida__SIS);
            ViewBag.Pk_IdAccion = new SelectList(db.Actividads, "Pk_IdActividad", "Descripcion", accion.Pk_IdAccion);
            ViewBag.Fk_IdCampania__UE = new SelectList(db.Campanias, "Pk_IdCampania", "NombreCampania", accion.Fk_IdCampania__UE);
            return View(accion);
        }

        // POST: /Alcance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pk_IdAccion,Fk_IdCampania__UE,Fk_IdTipoDeRecurso__SIS,Fk_IdTipoDeAccion__SIS,Fk_IdUnidadDeMedida__SIS,FK_IdPersona__SIS,Actividad,Indicador,Meta_Ene,Meta_Feb,Meta_Mar,Meta_Abr,Meta_May,Meta_Jun,Meta_Jul,Meta_Ago,Meta_Sep,Meta_Oct,Meta_Nov,Meta_Dic,Costo,Justificacion,Fk_IdStatusAlcance__SIS")] Accion accion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accion).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_IdPersona__SIS = new SelectList(db.Personas, "Pk_IdPersona", "Ap_Paterno", accion.FK_IdPersona__SIS);
            ViewBag.Fk_IdTipoDeAccion__SIS = new SelectList(db.TipoDeAccions, "Pk_IDTipoDeAccion", "Nombre", accion.Fk_IdTipoDeAccion__SIS);
            ViewBag.Fk_IdTipoDeRecurso__SIS = new SelectList(db.TipoDeRecursoes, "Pk_IdTipoDeRecurso", "Nombre", accion.Fk_IdTipoDeRecurso__SIS);
            ViewBag.Fk_IdUnidadDeMedida__SIS = new SelectList(db.UnidadDeMedidas, "Pk_IdUnidadDeMedida", "Nombre", accion.Fk_IdUnidadDeMedida__SIS);
            ViewBag.Pk_IdAccion = new SelectList(db.Actividads, "Pk_IdActividad", "Descripcion", accion.Pk_IdAccion);
            ViewBag.Fk_IdCampania__UE = new SelectList(db.Campanias, "Pk_IdCampania", "NombreCampania", accion.Fk_IdCampania__UE);
            return View(accion);
        }

        // GET: /Alcance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accion alcance = db.Accions.Find(id);
            if (alcance == null)
            {
                return HttpNotFound();
            }
            return View(alcance);
        }

        // POST: /Alcance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accion alcance = db.Accions.Find(id);
            db.Accions.Remove(alcance);
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

        #region DataGrid_Acciones
        DXMVCWebApplication1.Models.DB_SENASICAEntities db2 = new DXMVCWebApplication1.Models.DB_SENASICAEntities();

        [ValidateInput(false)]
        public ActionResult AlcanceGridViewPartial()
        {
            //A esta pantalla solo tienen acceso los usuarios encargados de una campaña especifica. 
            //Por lo que el administrador de la Instancia Ejecutora tiene que darle un permiso explicito al usuario para que este pueda acceder a 
            //Las acciones planeadas para esta campaña y poder designar actividades.
            //Es posible que esta función se tenga que generar con el Id de la Instancia Ejecutora
            int? Fk_IdCampania = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdCampania__UE);
            int? Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            ViewBag.Fk_IdUnidadEjecutora = Fk_IdUnidadEjecutora;

            return PartialView("_AlcanceGridViewPartial", DXMVCWebApplication1.Models.Senasica.GetFiltradoCampaniaAlcance(Fk_IdCampania, Fk_IdUnidadEjecutora));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AlcanceGridViewPartialAddNew(DXMVCWebApplication1.Models.Accion item)
        {
            int? _Fk_IdCampania = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdCampania__UE);
            int? Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            ViewBag.Fk_IdUnidadEjecutora = Fk_IdUnidadEjecutora;

            var model = db2.Accions;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db2.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Por favor corrija los errores marcados.";
            return PartialView("_AlcanceGridViewPartial", DXMVCWebApplication1.Models.Senasica.GetFiltradoCampaniaAlcance(_Fk_IdCampania, Fk_IdUnidadEjecutora));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AlcanceGridViewPartialUpdate(DXMVCWebApplication1.Models.Accion item)
        {
            int? _Fk_IdCampania = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdCampania__UE);
            int? Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            ViewBag.Fk_IdUnidadEjecutora = Fk_IdUnidadEjecutora;

            var model = db2.Accions;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdAccion == item.Pk_IdAccion);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db2.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Por favor corrija los errores marcados.";
            return PartialView("_AlcanceGridViewPartial", DXMVCWebApplication1.Models.Senasica.GetFiltradoCampaniaAlcance(_Fk_IdCampania, Fk_IdUnidadEjecutora));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AlcanceGridViewPartialDelete(System.Int32 Pk_IdAccion)
        {
            int? _Fk_IdCampania = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdCampania__UE);
            int? Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            ViewBag.Fk_IdUnidadEjecutora = Fk_IdUnidadEjecutora;

            var model = db2.Accions;
            if (Pk_IdAccion >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdAccion == Pk_IdAccion);
                    if (item != null)
                        model.Remove(item);
                    db2.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_AlcanceGridViewPartial", DXMVCWebApplication1.Models.Senasica.GetFiltradoCampaniaAlcance(_Fk_IdCampania, Fk_IdUnidadEjecutora));
        } 
        #endregion


//*********************************************COMIENZAN LOS ACTIONS DE LOS DATAGRIDS HIJOS********************************************************************
//*************************************************************************************************************************************************************
        #region DataGridActividades
        public ActionResult ActividadGridViewPartial(int? Pk_IdAccion)
        {
            ViewData["Pk_IdAccion"] = Pk_IdAccion;
            return PartialView("ActividadGridViewPartial", DXMVCWebApplication1.Models.Senasica.GetActividadesByAlcance(Pk_IdAccion));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ActividadGridViewPartialAddNew(DXMVCWebApplication1.Models.Actividad item, int Pk_IdAccion)
        {
            var model = db2.Actividads;
           // item.Fk_IdAccion__UE = Pk_IdAccion;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db2.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Por favor corrija los errores marcados.";
            return PartialView("ActividadGridViewPartial", DXMVCWebApplication1.Models.Senasica.GetActividadesByAlcance(Pk_IdAccion));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ActividadGridViewPartialUpdate(DXMVCWebApplication1.Models.Actividad item, int Pk_IdAccion)
        {
            var model = db2.Actividads;
           // item.Fk_IdAccion__UE = Pk_IdAccion;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdActividad == item.Pk_IdActividad);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db2.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Por favor corrija los errores marcados.";
            return PartialView("ActividadGridViewPartial", DXMVCWebApplication1.Models.Senasica.GetActividadesByAlcance(Pk_IdAccion));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ActividadGridViewPartialDelete(System.Int32 Pk_IdActividad, System.Int32 Pk_IdAccion)
        {
            var model = db2.Actividads;
            if (Pk_IdActividad >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdActividad == Pk_IdActividad);
                    if (item != null)
                        model.Remove(item);
                    db2.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("ActividadGridViewPartial", DXMVCWebApplication1.Models.Senasica.GetActividadesByAlcance(Pk_IdAccion));
        } 
        #endregion
    }
}
