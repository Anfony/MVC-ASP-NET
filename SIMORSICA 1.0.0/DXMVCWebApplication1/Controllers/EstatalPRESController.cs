using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles =RolesUsuarios.ESTATAL_PRES
                + "," + RolesUsuarios.SUP_ESTATAL)]
    public class EstatalPRESController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        // GET: EstatalPRES
        public ActionResult Index(bool? esGuardado)
        {
            ViewData["esGuardado"] = esGuardado == null ? false : esGuardado;

            int? _Fk_IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            int IdAnio = Convert.ToInt32(Session["IdAnio"]);

            PresupuestoEstatalVM presupuestoEstatal = new PresupuestoEstatalVM()
            {
                SistemaInformatico = db.PEFXEstadoes.Where(p => p.FK_IdEstado == _Fk_IdEstado && p.FK_IdAnio == IdAnio).Select(p => p.SistemaInform_Est).First(),
                Capacitacion = db.PEFXEstadoes.Where(p => p.FK_IdEstado == _Fk_IdEstado && p.FK_IdAnio == IdAnio).Select(p => p.Capacitacion_Est).First(),
                Divulgacion = db.PEFXEstadoes.Where(p => p.FK_IdEstado == _Fk_IdEstado && p.FK_IdAnio == IdAnio).Select(p => p.Divulgacion_Est).First(),
                EmergenciaSanitaria = db.PEFXEstadoes.Where(p => p.FK_IdEstado == _Fk_IdEstado && p.FK_IdAnio == IdAnio).Select(p => p.Emergencias_Est).First(),
                GastosOperacion = db.PEFXEstadoes.Where(p => p.FK_IdEstado == _Fk_IdEstado && p.FK_IdAnio == IdAnio).Select(p => p.GtoOperacion_Est).First(),
                InteligenciaSanitaria = db.PEFXEstadoes.Where(p => p.FK_IdEstado == _Fk_IdEstado && p.FK_IdAnio == IdAnio).Select(p => p.InteligenciaSanitaria_Est).First()
            };
            
            return View(presupuestoEstatal);
        }

        [HttpPost]
        public ActionResult GuardaPresupuesto(PresupuestoEstatalVM item)
        {
            int? _Fk_IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            int IdAnio = Convert.ToInt32(Session["IdAnio"]);

            var model = db.PEFXEstadoes;
            var modelItem = model.FirstOrDefault(it => it.FK_IdEstado == _Fk_IdEstado && it.FK_IdAnio == IdAnio);

            modelItem.SistemaInform_Est = item.SistemaInformatico;
            modelItem.Capacitacion_Est = item.Capacitacion;
            modelItem.Divulgacion_Est = item.Divulgacion;
            modelItem.Emergencias_Est = item.EmergenciaSanitaria;
            modelItem.GtoOperacion_Est = item.GastosOperacion;
            modelItem.InteligenciaSanitaria_Est = item.InteligenciaSanitaria;

            UpdateModel(model);
            db.SaveChanges();

            return RedirectToAction("Index", new { esGuardado = true });
        }
    }
}