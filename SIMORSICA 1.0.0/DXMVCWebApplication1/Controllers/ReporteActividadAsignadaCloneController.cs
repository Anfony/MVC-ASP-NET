using System;
using System.Linq;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Common;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Core.Objects;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize]
    public class ReporteActividadAsignadaCloneController : Controller
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

        #region DataGridActividadesAsignadas
        [ValidateInput(false)]
        public ActionResult ActividadesAsignadasGridViewPartial(int IdCierreMensual)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_INFORME_AVANCES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCierreMensual"] = IdCierreMensual;

            return PartialView("_ActividadesAsignadasGridViewPartial", Senasica.GetActividadesAsignadasByCierreMensual(IdCierreMensual));
        }
        #endregion

        #region RepActividad

        [ValidateInput(false)]
        public ActionResult RepActividadGridViewPartial(int idActividadAsignada, int IdCierreMensual)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_REP_ACTIVIDADES_ASIGNADAS);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            CalculaCotasFechas(IdCierreMensual);

            ViewData["idActividadAsignada"] = idActividadAsignada;
            ViewData["IdUnidadEjecutora"] = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();
            ViewData["IdCierreMensual"] = IdCierreMensual;

            return PartialView("_RepActividadGridViewPartial", Senasica.GetRepActividadesByActividadAbierta(idActividadAsignada, IdCierreMensual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult RepActividadGridViewPartialAddNew(RepActividad item, int idActividadAsignada, int IdCierreMensual)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_REP_ACTIVIDADES_ASIGNADAS);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            CalculaCotasFechas(IdCierreMensual);

            ViewData["idActividadAsignada"] = idActividadAsignada;
            ViewData["IdUnidadEjecutora"] = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();
            ViewData["IdCierreMensual"] = IdCierreMensual;

            var model = db.RepActividads;
            item.Fk_IdActividad = idActividadAsignada;
            item.FechaInforme = DateTime.Today;
            item.CT_User = User.Identity.GetUserId();
            item.CT_Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

            ViewData["dataItem"] = item;

            return PartialView("_RepActividadGridViewPartial", Senasica.GetRepActividadesByActividadAbierta(idActividadAsignada, IdCierreMensual));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult RepActividadGridViewPartialUpdate(RepActividad item, int idActividadAsignada, int IdCierreMensual)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_REP_ACTIVIDADES_ASIGNADAS);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            CalculaCotasFechas(IdCierreMensual);

            ViewData["idActividadAsignada"] = idActividadAsignada;
            ViewData["IdUnidadEjecutora"] = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();
            ViewData["IdCierreMensual"] = IdCierreMensual;

            var model = db.RepActividads;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdRepActividad == item.Pk_IdRepActividad);
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
                ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

            ViewData["dataItem"] = item;

            return PartialView("_RepActividadGridViewPartial", Senasica.GetRepActividadesByActividadAbierta(idActividadAsignada, IdCierreMensual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult RepActividadGridViewPartialDelete(System.Int32 Pk_IdRepActividad, int idActividadAsignada, int IdCierreMensual)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_REP_ACTIVIDADES_ASIGNADAS);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            CalculaCotasFechas(IdCierreMensual);

            ViewData["idActividadAsignada"] = idActividadAsignada;
            ViewData["IdUnidadEjecutora"] = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();
            ViewData["IdCierreMensual"] = IdCierreMensual;

            var model = db.RepActividads;
            if (Pk_IdRepActividad >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdRepActividad == Pk_IdRepActividad);
                    if (item != null)
                    {
                        if (!item.esCerrado)
                        {
                            ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                            db.SP_DELETE_RepActividad(Pk_IdRepActividad, User.Identity.GetUserId(), Error);
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
            return PartialView("_RepActividadGridViewPartial", Senasica.GetRepActividadesByActividadAbierta(idActividadAsignada, IdCierreMensual));
        }
        #endregion

        /// <summary>
        /// Las cotas estarán definidas por el mes
        /// 
        /// Fecha inicio = 1 / mes Cierre / 2017
        /// Fecha fin = último día mes cierre / mes cierre / 2017
        /// 
        /// es lo mismo para la cota mínima y máxima
        /// </summary>
        /// <param name="IdCierreMensual"></param>
        private void CalculaCotasFechas(int IdCierreMensual)
        {
            int IdMes = db.CierreMensuals.Where(item => item.Pk_IdCierreMensual == IdCierreMensual)
                                            .Select(item => item.Fk_IdMes)
                                            .First();

            string anio = FuncionesAuxiliares.AnioPresupuestal(FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal());

            string ultimoDiaMes = DateTime.DaysInMonth(Convert.ToInt32(anio), IdMes) + "/" + IdMes + "/" + anio;

            ViewData["FechaIni_CotaMin"] = "01/01/" + anio;
            ViewData["FechaIni_CotaMax"] = ultimoDiaMes;

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