using System;
using System.Linq;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Common;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using Microsoft.AspNet.Identity;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize]
    public class ReporteDeAdquisicionesCloneController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        [HttpPost, ValidateInput(false)]
        public ActionResult Index(int IdCierreMensual)
        {
            ViewData["IdCierreMensual"] = IdCierreMensual;

            ViewData["NombreCampania"] = db.CierreMensuals.Where(item => item.Pk_IdCierreMensual == IdCierreMensual)
                                                .Select(item => item.Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre)
                                                .First();

            ViewData["Mes"] = db.CierreMensuals.Where(item => item.Pk_IdCierreMensual == IdCierreMensual)
                                                            .Select(item => item.Me.Descripcion)
                                                            .First();

            return View();
        }
        #region PAA
        [ValidateInput(false)]
        public ActionResult PAAGridViewPartial(int IdCierreMensual)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_INFORME_DE_ADQUISICIONES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCierreMensual"] = IdCierreMensual;

            ViewBag.UEID = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

            return PartialView("_PAAGridViewPartial", Senasica.GetProgramaAnualAdqByCierreMensual(IdCierreMensual));
        }
        #endregion
        
        #region RepGasto
        [ValidateInput(false)]
        public ActionResult RepGastoGridViewPartial(int IdNecesidad, int IdCierreMensual)
        {
            CalculaCotasFechas(IdCierreMensual);

            ViewData["IdNecesidad"] = IdNecesidad;
            ViewData["_Fk_IdEstado"] = FuncionesAuxiliares.GetCurrent_IdEstado();
            ViewData["IdCierreMensual"] = IdCierreMensual;

            return PartialView("_RepGastoGridViewPartial", Senasica.GetRepAdquisicionesByCierreMensual(IdNecesidad, IdCierreMensual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult RepGastoGridViewPartialAddNew(RepAdquisicion item, int IdNecesidad, int IdCierreMensual)
        {
            CalculaCotasFechas(IdCierreMensual);

            ViewData["IdNecesidad"] = IdNecesidad;
            ViewData["_Fk_IdEstado"] = FuncionesAuxiliares.GetCurrent_IdEstado();
            ViewData["IdCierreMensual"] = IdCierreMensual;

            var model = db.RepAdquisicions;

            item.Fk_IdProgramaAnualAdq = IdNecesidad;

            if (ModelState.IsValid)
            {
                try
                {
                    item.CT_User = User.Identity.GetUserId();
                    item.CT_Date = DateTime.Now;
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Por favor, corrija los errores marcados.";

            ViewData["dataItem"] = item;

            return PartialView("_RepGastoGridViewPartial", Senasica.GetRepAdquisicionesByCierreMensual(IdNecesidad, IdCierreMensual));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult RepGastoGridViewPartialUpdate(RepAdquisicion item, int IdNecesidad, int IdCierreMensual)
        {
            CalculaCotasFechas(IdCierreMensual);

            ViewData["IdNecesidad"] = IdNecesidad;
            ViewData["_Fk_IdEstado"] = FuncionesAuxiliares.GetCurrent_IdEstado();
            ViewData["IdCierreMensual"] = IdCierreMensual;

            var model = db.RepAdquisicions;

            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdRepAdquisicion == item.Pk_IdRepAdquisicion);
                    if (modelItem != null)
                    {
                        if (!modelItem.esCerrado)
                        {
                            modelItem.CT_User = User.Identity.GetUserId();
                            modelItem.CT_Date = DateTime.Now;
                            this.UpdateModel(modelItem);
                            db.SaveChanges();
                        }
                        else ViewData["EditError"] = "El registro ya ha sido cerrado, por lo cual no podrá hacer modificaciones";
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            ViewData["dataItem"] = item;

            return PartialView("_RepGastoGridViewPartial", Senasica.GetRepAdquisicionesByCierreMensual(IdNecesidad, IdCierreMensual));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult RepGastoGridViewPartialDelete(System.Int32 Pk_IdRepAdquisicion, int IdNecesidad, int IdCierreMensual)
        {
            CalculaCotasFechas(IdCierreMensual);
            ViewData["IdNecesidad"] = IdNecesidad;
            var model = db.RepAdquisicions;
            int? _Fk_IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            ViewData["_Fk_IdEstado"] = _Fk_IdEstado;
            
            if (Pk_IdRepAdquisicion >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdRepAdquisicion == Pk_IdRepAdquisicion);
                    if (item != null)
                    {
                        if (!item.esCerrado)
                        {
                            ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                            db.SP_DELETE_RepAdquisicion(Pk_IdRepAdquisicion, User.Identity.GetUserId(), Error);
                            db.SaveChanges();
                        }
                        else ViewData["EditError"] = "El registro ya ha sido cerrado, por lo cual no podrá hacer modificaciones";
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_RepGastoGridViewPartial", Senasica.GetRepAdquisicionesByCierreMensual(IdNecesidad, IdCierreMensual));
        }

        public ActionResult ComboBoxPartialProveedor()
        {
            return PartialView("_ComboBoxPartialProveedor");
        }
        #endregion

        /// <summary>
        /// Las cotas estarán definidas por el mes
        /// 
        /// Fecha inicio = 1 / mes Cierre / 2017
        /// Fecha fin = último día mes cierre / mes cierre / 2017
        /// 
        /// </summary>
        /// <param name="idActividadAsignada"></param>
        private void CalculaCotasFechas(int IdCierreMensual)
        {
            int IdMes = db.CierreMensuals.Where(item => item.Pk_IdCierreMensual == IdCierreMensual)
                                .Select(item => item.Fk_IdMes)
                                .First();

            string anio = FuncionesAuxiliares.AnioPresupuestal(FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal());

            string ultimoDiaMes = DateTime.DaysInMonth(Convert.ToInt32(anio), IdMes) + "/" + IdMes + "/" + anio;

            ViewData["FechaFin_CotaMin"] = "01/" + IdMes + "/" + anio;
            ViewData["FechaFin_CotaMax"] = ultimoDiaMes;
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