using System;
using System.Linq;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Common;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Core.Objects;
using DXMVCWebApplication1.Negocio;

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
                         + "," + RolesUsuarios.PUESTO_AUXILIAR_ADMINISTRATIVO
                         + "," + RolesUsuarios.PUESTO_AUXILIAR_CAMPO
                         + "," + RolesUsuarios.PUESTO_COOR_ADMINISTRATIVO
                         + "," + RolesUsuarios.COORDINADOR_CAMPANIA
                         + "," + RolesUsuarios.PUESTO_COOR_PROYECTO
                         + "," + RolesUsuarios.PUESTO_GERENTE
                         + "," + RolesUsuarios.PUESTO_PROF_PROYECTO
                         + "," + RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                         + "," + RolesUsuarios.PUESTO_PROF_TECNICO_CALIDAD_MEJORA_PROCESOS
                         + "," + RolesUsuarios.PUESTO_PROF_TEC_CAPACITACION_DIVULGACION
                         + "," + RolesUsuarios.PUESTO_SECRETARIA
                         + "," + RolesUsuarios.TECNICO_OPERATIVO
                         + "," + RolesUsuarios.SUP_REGIONAL
                         + "," + RolesUsuarios.REPRESENTANTE_ESTATAL
                         + "," + RolesUsuarios.SUP_ESTATAL
                         + "," + RolesUsuarios.UR_UPV
                         + "," + RolesUsuarios.IE_PROGRAMAS_U
                         + "," + RolesUsuarios.SUP_AUDITOR
   )]
    public class ReporteActividadAsignadaController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        // GET: /ReporteActividadAsignada/
        public ActionResult Index(string Estado)
        {
            ViewData["Estado"] = Estado;
            return View();
        }

        #region DataGridActividadesAsignadas
        [ValidateInput(false)]
        public ActionResult ActividadesAsignadasGridViewPartial(string Estado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_INFORME_AVANCES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            int? persona = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdPersona__SIS);
            int? IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            String rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            ViewData["Estado"] = Estado;

            return PartialView("_ActividadesAsignadasGridViewPartial", Senasica.GetActividadesAsignadasByPersona(rolusuario, IdUnidadEjecutora, persona, IdAnio, Estado));
        }
        #endregion

        #region RepActividad

        [ValidateInput(false)]
        public ActionResult RepActividadGridViewPartial(int idActividadAsignada)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_REP_ACTIVIDADES_ASIGNADAS);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            Permisos(idActividadAsignada);

            CalculaCotasFechas(idActividadAsignada);

            ViewData["idActividadAsignada"] = idActividadAsignada;
            ViewData["IdUnidadEjecutora"] = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

            return PartialView("_RepActividadGridViewPartial", Senasica.GetRepActividadesByActividad(idActividadAsignada));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult RepActividadGridViewPartialAddNew(RepActividad item, int idActividadAsignada)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_REP_ACTIVIDADES_ASIGNADAS);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            Permisos(idActividadAsignada);

            CalculaCotasFechas(idActividadAsignada);

            ViewData["idActividadAsignada"] = idActividadAsignada;
            ViewData["IdUnidadEjecutora"] = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

            var model = db.RepActividads;
            item.Fk_IdActividad = idActividadAsignada;
            item.FechaInforme = DateTime.Today;
            item.CT_User = User.Identity.GetUserId();
            item.CT_Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (item.CantidadMetas <= 0) ViewData["EditError"] = "La Cantidad debe ser mayor a cero";
                else
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
            }
            else
                ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

            ViewData["dataItem"] = item;

            return PartialView("_RepActividadGridViewPartial", Senasica.GetRepActividadesByActividad(idActividadAsignada));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult RepActividadGridViewPartialUpdate(RepActividad item, int idActividadAsignada)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_REP_ACTIVIDADES_ASIGNADAS);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            Permisos(idActividadAsignada);

            CalculaCotasFechas(idActividadAsignada);

            ViewData["idActividadAsignada"] = idActividadAsignada;
            ViewData["IdUnidadEjecutora"] = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

            var model = db.RepActividads;
            if (ModelState.IsValid)
            {
                if (item.CantidadMetas <= 0) ViewData["EditError"] = "La Cantidad debe ser mayor a cero";
                else
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
            }
            else
                ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

            ViewData["dataItem"] = item;

            return PartialView("_RepActividadGridViewPartial", Senasica.GetRepActividadesByActividad(idActividadAsignada));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult RepActividadGridViewPartialDelete(System.Int32 Pk_IdRepActividad, int idActividadAsignada)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_REP_ACTIVIDADES_ASIGNADAS);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            Permisos(idActividadAsignada);

            CalculaCotasFechas(idActividadAsignada);

            ViewData["idActividadAsignada"] = idActividadAsignada;
            ViewData["IdUnidadEjecutora"] = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

            
            var model = db.RepActividads;
            if (Pk_IdRepActividad >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdRepActividad == Pk_IdRepActividad);
                    if (item.esCerrado)
                    {
                        return JavaScript("El registro ya ha sido cerrado, por lo cual no podrá eliminar");
                    }
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
            return PartialView("_RepActividadGridViewPartial", Senasica.GetRepActividadesByActividad(idActividadAsignada));
        }
        #endregion

        #region CalculaCotasFechas
        /// <summary>
        /// Genera las cotas de fecha inicio y fecha fin, las cuales corresponden a las siguientes reglas:
        /// 
        //Si el mes actual es igual al mes que está abierto (puede que esté registrado en CierreMensual, pero aún no tiene la bandera de "esCerrado")
        //	
        //	   fecha inicio 
        //	  		fecha min = 1 enero 2017
        //	  		fecha max = hoy
        //	   fecha fin
        //	  		fecha min = 1 mes actual
        //	  		fecha max = hoy
        //	
        //Si el mes actual es mayor al mes abierto (el usuario deberá de estar todavía en los primeros 15 días para poder cerrarlo, antes de que el sistema lo haga)
        //	   fecha inicio 
        //	  		fecha min = 1 enero 2017
        //	  		fecha max = último día del mes a abierto
        //	   fecha fin
        //	  		fecha min = 1 mes abierto
        //	  		fecha max = último día del mes a abierto
        //
        //Si el mes actual es menor al mes que está abierto (el usuario cerró esta campaña antes que terminara el mes)
        //		El usuario no podrá registrar actividades ya que las debe de ejecutar con forme las va ejecutando (tendrá que esperar hasta que termine el mes)
        /// </summary>
        /// <param name="idActividadAsignada"></param>
        private void CalculaCotasFechas(int IdActividadAsignada)
        {
            int AnioActual = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            int IdMesAbierto = DateTime.Today.Day <= 15 ? DateTime.Today.Month - 1 : DateTime.Today.Month;

            IdMesAbierto = IdMesAbierto == 0 ? 12 : IdMesAbierto;

            int IdCampania = db.Actividads.Where(item => item.Pk_IdActividad == IdActividadAsignada)
                                .Select(item => item.ActividadXAccion.AccionXCampania.Fk_IdCampania__UE)
                                .First();

            var _query = db.CierreMensuals.Where(item => item.esCerrado == true
                                                        && item.Fk_IdCampania == IdCampania
                                                        && item.Fk_IdMes == IdMesAbierto);

            IdMesAbierto = _query.Count() == 0 ? IdMesAbierto : IdMesAbierto + 1;
            IdMesAbierto = IdMesAbierto == 13 ? 1 : IdMesAbierto;

            ViewData["FechaIni_CotaMin"] = "01/01/" + AnioActual;
            ViewData["FechaFin_CotaMin"] = "01/" + IdMesAbierto + "/" + AnioActual;

            if (IdMesAbierto == DateTime.Today.Month)
            {
                ViewData["FechaIni_CotaMax"] = DateTime.Today.ToString("dd/MM/yyyy");
                ViewData["FechaFin_CotaMax"] = DateTime.Today.ToString("dd/MM/yyyy");
            }
            else
            {
                string ultimoDiaMesAbierto = DateTime.DaysInMonth(AnioActual, IdMesAbierto) + "/" + IdMesAbierto + "/" + AnioActual;

                ViewData["FechaIni_CotaMax"] = ultimoDiaMesAbierto;
                ViewData["FechaFin_CotaMax"] = ultimoDiaMesAbierto;
            }

            // Entra en el caso en que el usuario haya cerrado su campaña para el mes actual antes de que termine el mes.
            // 
            // Ejemplo: Si hoy es 20-oct-2017 y el usuario quiere cerrar una cierta campaña para el mes de octubre,
            // si lo cierra el día de hoy, entonces el usuario no puede registrar nada del mes siguiente que sería noviembre
            // ya que aún no termina el mes de octubre
            ViewData["sePuedeRegistrar"] = true;

            if (DateTime.Today.Month < IdMesAbierto)
            {
                ViewData["sePuedeRegistrar"] = false;

                //esto se asigna así para que no marque error en la conversión de la View
                //estas fechas ya no los ve el usuario
                ViewData["FechaIni_CotaMin"] = DateTime.Today.ToString("dd/MM/yyyy");
                ViewData["FechaIni_CotaMax"] = DateTime.Today.ToString("dd/MM/yyyy");

                ViewData["FechaFin_CotaMin"] = DateTime.Today.ToString("dd/MM/yyyy");
                ViewData["FechaFin_CotaMax"] = DateTime.Today.ToString("dd/MM/yyyy");
            }
        }
#endregion

        #region FiltrabyEstado
        /// <summary>
        /// Filtra la Información por el estado seleccionado
        /// </summary>
        /// <param name="Estado"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult FiltrabyEstado(string Estado)
        {
            return RedirectToAction("Index", new { Estado });
        }
        #endregion

        #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña
        private void Permisos(int idActividadAsignada)
        {
            string StatusActual;
            int? IdCampania = db.Actividads.Where(i => i.Pk_IdActividad == idActividadAsignada).Select(i => i.ActividadXAccion.AccionXCampania.Fk_IdCampania__UE).First();
                    
            StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania))
            {
                ViewData["Status_Lectura"] = false;
                ViewData["Status_Escritura"] = false;
            }
            else
            {
                ViewData["Status_Lectura"] = true;
                ViewData["Status_Escritura"] = true;
            }
        }
        #endregion

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