using System.Linq;
using System.Collections;
using System.Data;
using System.Data.Entity.Spatial;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using DXMVCWebApplication1.Common;
using System.Collections.Generic;
using System;

namespace DXMVCWebApplication1.Models
{
    public static class Senasica
    {
        #region Acciones
        //***********************AccionesXCamp**********************************

        /// <summary>
        ///  Devuelve las Acciones de la tabla UE.AccionXCampania, filtra los registros por la Campania
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetAccionesPorCampaniaByCampania(int? IdCampania, string StatusActual)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            var StatusSinRepro = FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania);
            if (StatusActual == StatusSinRepro)
            {
                return db.AccionXCampanias.Where(item => item.Fk_IdCampania__UE == IdCampania)
                    .Select(item => new
                    {
                        item.PK_IdAccionXCampania,
                        TipoAccion = item.TipoDeAccion.Nombre,
                        Persona = item.Persona.NombreCompleto,
                        item.Justificacion,
                        item.Fk_IdTipoDeAccion__SIS,
                        item.FK_IdPersona__SIS
                    })
                    .ToList();
            }
            var StatusEnRepro = FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania);
            if (StatusActual == StatusEnRepro)
            {
                return db.AccionXCampania_Repro.Where(item => item.Fk_IdCampania__UE == IdCampania)
                    .Select(item => new
                    {
                        item.PK_IdAccionXCampania,
                        TipoAccion = item.TipoDeAccion.Nombre,
                        Persona = item.Persona.NombreCompleto,
                        item.Justificacion,
                        item.Fk_IdTipoDeAccion__SIS,
                        item.FK_IdPersona__SIS
                    })
                    .ToList();
            }
            return null;
        }
        #endregion

        #region Actividades
        /// <summary>Default2
        /// Devuelve las Actividades de la tabla UE.Actividad
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetActividadesByActividadXAccion(int Pk_IdActividadXAccion)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            return db1.Actividads.Where(item => item.Fk_IdActividadXAccion == Pk_IdActividadXAccion)
                .Select(item => new
                {
                    item.Pk_IdActividad,
                    TipoActividad = item.ActividadXAccion.TipoActividad.Nombre,
                    UnidadDeMedida = item.ActividadXAccion.TipoActividad.UnidadDeMedida.Nombre,
                    item.Descripcion,
                    item.Asignado_Anual,
                    item.Fecha_Inicio,
                    item.FechaFin,
                    Persona = item.Persona.Nombre,
                    StatusActividad = item.StatusActividad.Nombre,
                    item.Fk_IdPersona__SIS,
                    item.Fk_IdStatusActividad__SIS,
                    item.Asignado_Ene,
                    item.Asignado_Feb,
                    item.Asignado_Mar,
                    item.Asignado_Abr,
                    item.Asignado_May,
                    item.Asignado_Jun,
                    item.Asignado_Jul,
                    item.Asignado_Ago,
                    item.Asignado_Sep,
                    item.Asignado_Oct,
                    item.Asignado_Nov,
                    item.Asignado_Dic,
                }).ToList();
        }

        /// <summary>
        /// Devuelve las Actividades de la tabla UE.Actividad, filtra los registros por Campaña
        /// 
        /// --------------------------------------------------
        /// Esta función se ocupa en los controladores Actividad y Actividades2.
        /// No se sabe si se ocupan o no estos controladores
        /// --------------------------------------------------
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns>Devuelve las Actividades de la tabla UE.Actividad, filtra los registros por Campaña</returns>
        public static IEnumerable GetActividadesByCampania(int? IdCampania, int? IdUnidadEjecutora)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            if (IdCampania == 0)
            {
                var _m = from actividad in db1.Actividads
                         where actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.Fk_IdUnidadEjecutora == IdUnidadEjecutora
                         select actividad;
                return _m.ToList();
            }
            else
            {

                var _m = from actividad in db1.Actividads where actividad.ActividadXAccion.AccionXCampania.Fk_IdCampania__UE == IdCampania select actividad;
                return _m.ToList();
            }
        }

        /// <summary>
        /// Devuelve las Actividades de la tabla UE.ActividadXaccion, filtra los registros por Acción
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetActividadesParaAsignarByCampania(string userroll, int? IdUnidadEjecutora, int? IdPersona, int? IdEstado, int IdAnio)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            if (userroll == RolesUsuarios.UR_ANIMAL
                || userroll == RolesUsuarios.UR_VEGETAL
                || userroll == RolesUsuarios.UR_INOCUIDAD
                || userroll == RolesUsuarios.UR_ACUICOLA_PESQUERA
                || userroll == RolesUsuarios.UR_MOVILIZACION
                || userroll == RolesUsuarios.UR_UPV)
            {
                int IdUnidadResponsable = FuncionesAuxiliares.GetCurrent_IdUnidadResponsable();

                var _m = from actividadXaccion in db1.ActividadXAccions
                         where actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.Fk_IdUnidadResponsable == IdUnidadResponsable
                                && actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.Fk_IdAnio == IdAnio
                         orderby actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre,
                         actividadXaccion.AccionXCampania.TipoDeAccion.Nombre,
                         actividadXaccion.Persona.Nombre
                         select new
                         {
                             actividadXaccion.Pk_IdActividadXAccion,
                             IE = actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Nombre,
                             Campania = actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre,
                             Accion = actividadXaccion.AccionXCampania.TipoDeAccion.Nombre,
                             TipoActividad = actividadXaccion.TipoActividad.Nombre,
                             actividadXaccion.Actividad,
                             UnidadMedida = actividadXaccion.TipoActividad.UnidadDeMedida.Nombre,
                             actividadXaccion.Meta_Ene,
                             actividadXaccion.Meta_Feb,
                             actividadXaccion.Meta_Mar,
                             actividadXaccion.Meta_Abr,
                             actividadXaccion.Meta_May,
                             actividadXaccion.Meta_Jun,
                             actividadXaccion.Meta_Jul,
                             actividadXaccion.Meta_Ago,
                             actividadXaccion.Meta_Sep,
                             actividadXaccion.Meta_Oct,
                             actividadXaccion.Meta_Nov,
                             actividadXaccion.Meta_Dic,
                             actividadXaccion.Meta_Anual,
                             actividadXaccion.Asignadas,
                             actividadXaccion.EstadoDeAsignacion,
                             PersonaResponsable = actividadXaccion.Persona.NombreCompleto
                         };

                return _m.ToList();
            }

            if (userroll == RolesUsuarios.REPRESENTANTE_ESTATAL)
            {
                var _m = from actividadXaccion in db1.ActividadXAccions
                         where actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.Fk_IdEstado == IdEstado
                                && actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.Fk_IdAnio == IdAnio
                         orderby actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre,
                         actividadXaccion.AccionXCampania.TipoDeAccion.Nombre,
                         actividadXaccion.Persona.Nombre
                         select new
                         {
                             actividadXaccion.Pk_IdActividadXAccion,
                             IE = actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Nombre,
                             Campania = actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre,
                             Accion = actividadXaccion.AccionXCampania.TipoDeAccion.Nombre,
                             TipoActividad = actividadXaccion.TipoActividad.Nombre,
                             actividadXaccion.Actividad,
                             UnidadMedida = actividadXaccion.TipoActividad.UnidadDeMedida.Nombre,
                             actividadXaccion.Meta_Ene,
                             actividadXaccion.Meta_Feb,
                             actividadXaccion.Meta_Mar,
                             actividadXaccion.Meta_Abr,
                             actividadXaccion.Meta_May,
                             actividadXaccion.Meta_Jun,
                             actividadXaccion.Meta_Jul,
                             actividadXaccion.Meta_Ago,
                             actividadXaccion.Meta_Sep,
                             actividadXaccion.Meta_Oct,
                             actividadXaccion.Meta_Nov,
                             actividadXaccion.Meta_Dic,
                             actividadXaccion.Meta_Anual,
                             actividadXaccion.Asignadas,
                             actividadXaccion.EstadoDeAsignacion,
                             PersonaResponsable = actividadXaccion.Persona.NombreCompleto
                         };

                return _m.ToList();
            }

            if (userroll == RolesUsuarios.IE_VEGETAL
                || userroll == RolesUsuarios.IE_ANIMAL
                || userroll == RolesUsuarios.IE_INOCUIDAD
                || userroll == RolesUsuarios.IE_MOVILIZACION
                || userroll == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || userroll == RolesUsuarios.IE_PROGRAMAS_U
                || userroll == RolesUsuarios.PUESTO_GERENTE
                || userroll == RolesUsuarios.SUP_ESTATAL)
            {
                var _m = from actividadXaccion in db1.ActividadXAccions
                         where actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.Fk_IdUnidadEjecutora == IdUnidadEjecutora
                                && actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Fk_IdEstado__SIS == IdEstado
                                && actividadXaccion.TipoActividad.UnidadDeMedida.Fk_IdAnio == IdAnio
                         orderby actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre,
                         actividadXaccion.AccionXCampania.TipoDeAccion.Nombre,
                         actividadXaccion.Persona.Nombre
                         select new
                         {
                             actividadXaccion.Pk_IdActividadXAccion,
                             IE = actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Nombre,
                             Campania = actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre,
                             Accion = actividadXaccion.AccionXCampania.TipoDeAccion.Nombre,
                             TipoActividad = actividadXaccion.TipoActividad.Nombre,
                             actividadXaccion.Actividad,
                             UnidadMedida = actividadXaccion.TipoActividad.UnidadDeMedida.Nombre,
                             actividadXaccion.Meta_Ene,
                             actividadXaccion.Meta_Feb,
                             actividadXaccion.Meta_Mar,
                             actividadXaccion.Meta_Abr,
                             actividadXaccion.Meta_May,
                             actividadXaccion.Meta_Jun,
                             actividadXaccion.Meta_Jul,
                             actividadXaccion.Meta_Ago,
                             actividadXaccion.Meta_Sep,
                             actividadXaccion.Meta_Oct,
                             actividadXaccion.Meta_Nov,
                             actividadXaccion.Meta_Dic,
                             actividadXaccion.Meta_Anual,
                             actividadXaccion.Asignadas,
                             actividadXaccion.EstadoDeAsignacion,
                             PersonaResponsable = actividadXaccion.Persona.NombreCompleto
                         };

                return _m.ToList();
            }

            if (userroll == RolesUsuarios.PUESTO_COOR_ADMINISTRATIVO
                || userroll == RolesUsuarios.COORDINADOR_CAMPANIA
                || userroll == RolesUsuarios.PUESTO_COOR_REGIONAL
                || userroll == RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                || userroll == RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS)
            {
                var _m = from actividadXaccion in db1.ActividadXAccions
                         where actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.Fk_IdUnidadEjecutora == IdUnidadEjecutora
                         && actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Fk_IdEstado__SIS == IdEstado
                         && actividadXaccion.FK_IdPersona == IdPersona
                         && actividadXaccion.TipoActividad.UnidadDeMedida.Fk_IdAnio == IdAnio
                         orderby actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre,
                         actividadXaccion.AccionXCampania.TipoDeAccion.Nombre,
                         actividadXaccion.Persona.Nombre
                         select new
                         {
                             actividadXaccion.Pk_IdActividadXAccion,
                             IE = actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Nombre,
                             Campania = actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre,
                             Accion = actividadXaccion.AccionXCampania.TipoDeAccion.Nombre,
                             TipoActividad = actividadXaccion.TipoActividad.Nombre,
                             actividadXaccion.Actividad,
                             UnidadMedida = actividadXaccion.TipoActividad.UnidadDeMedida.Nombre,
                             actividadXaccion.Meta_Ene,
                             actividadXaccion.Meta_Feb,
                             actividadXaccion.Meta_Mar,
                             actividadXaccion.Meta_Abr,
                             actividadXaccion.Meta_May,
                             actividadXaccion.Meta_Jun,
                             actividadXaccion.Meta_Jul,
                             actividadXaccion.Meta_Ago,
                             actividadXaccion.Meta_Sep,
                             actividadXaccion.Meta_Oct,
                             actividadXaccion.Meta_Nov,
                             actividadXaccion.Meta_Dic,
                             actividadXaccion.Meta_Anual,
                             actividadXaccion.Asignadas,
                             actividadXaccion.EstadoDeAsignacion,
                             PersonaResponsable = actividadXaccion.Persona.NombreCompleto
                         };

                return _m.ToList();
            }

            if (userroll == RolesUsuarios.PUESTO_COOR_PROYECTO)
            {
                var _m = from actividadXaccion in db1.ActividadXAccions
                         where (actividadXaccion.AccionXCampania.Campania.Fk_IdCoordinador == IdPersona
                                    || actividadXaccion.FK_IdPersona == IdPersona)
                         && actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.Fk_IdAnio == IdAnio
                         orderby actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre,
                         actividadXaccion.AccionXCampania.TipoDeAccion.Nombre,
                         actividadXaccion.Persona.Nombre
                         select new
                         {
                             actividadXaccion.Pk_IdActividadXAccion,
                             IE = actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Nombre,
                             Campania = actividadXaccion.AccionXCampania.Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre,
                             Accion = actividadXaccion.AccionXCampania.TipoDeAccion.Nombre,
                             TipoActividad = actividadXaccion.TipoActividad.Nombre,
                             actividadXaccion.Actividad,
                             UnidadMedida = actividadXaccion.TipoActividad.UnidadDeMedida.Nombre,
                             actividadXaccion.Meta_Ene,
                             actividadXaccion.Meta_Feb,
                             actividadXaccion.Meta_Mar,
                             actividadXaccion.Meta_Abr,
                             actividadXaccion.Meta_May,
                             actividadXaccion.Meta_Jun,
                             actividadXaccion.Meta_Jul,
                             actividadXaccion.Meta_Ago,
                             actividadXaccion.Meta_Sep,
                             actividadXaccion.Meta_Oct,
                             actividadXaccion.Meta_Nov,
                             actividadXaccion.Meta_Dic,
                             actividadXaccion.Meta_Anual,
                             actividadXaccion.Asignadas,
                             actividadXaccion.EstadoDeAsignacion,
                             PersonaResponsable = actividadXaccion.Persona.NombreCompleto
                         };

                return _m.ToList();
            }

            return null;
        }

        //***********************ActividadesXAccion**********************************
        /// <summary>
        /// Devuelve las Actividades de la tabla UE.ActividadXAccion, filtra los registros por Acción
        /// </summary>
        /// <param name="IdAccion"></param>
        /// <returns></returns>

        public static IEnumerable GetActividadesByAccion(int? IdAccion, int? IdCampania, string StatusActual)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var StatusSinRepro = FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania);
            if (StatusActual == StatusSinRepro)
            {
                return db1.ActividadXAccions.Where(item => item.Fk_IdAccionXCampania == IdAccion)
                    .Select(item => new
                    {
                        item.Pk_IdActividadXAccion,
                        TipoActividad = item.TipoActividad.Nombre,
                        item.Actividad,
                        item.Meta_Anual,
                        TipoDeRecurso = item.TipoDeRecurso.Nombre,
                        UnidadDeMedida = item.TipoActividad.UnidadDeMedida.Nombre,
                        Persona = item.Persona.NombreCompleto,
                        item.FK_IdPersona,
                        item.Fk_IdTipoActividad,
                        item.Justificacion,
                        item.Indicador,
                        item.Meta_Ene,
                        item.Meta_Feb,
                        item.Meta_Mar,
                        item.Meta_Abr,
                        item.Meta_May,
                        item.Meta_Jun,
                        item.Meta_Jul,
                        item.Meta_Ago,
                        item.Meta_Sep,
                        item.Meta_Oct,
                        item.Meta_Nov,
                        item.Meta_Dic,
                    }).ToList();
            }
            var StatusEnRepro = FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania);
            if (StatusActual == StatusEnRepro)
            {
                return db1.ActividadXAccion_Repro.Where(item => item.Fk_IdAccionXCampania == IdAccion)
                        .Select(item => new
                        {
                            item.Pk_IdActividadXAccion,
                            TipoActividad = item.TipoActividad.Nombre,
                            item.Actividad,
                            item.Meta_Anual,
                            UnidadDeMedida = item.TipoActividad.UnidadDeMedida.Nombre,
                            Persona = item.Persona.NombreCompleto,
                            item.FK_IdPersona,
                            item.Fk_IdTipoActividad,
                            item.Justificacion,
                            item.Indicador,
                            item.Meta_Ene,
                            item.Meta_Feb,
                            item.Meta_Mar,
                            item.Meta_Abr,
                            item.Meta_May,
                            item.Meta_Jun,
                            item.Meta_Jul,
                            item.Meta_Ago,
                            item.Meta_Sep,
                            item.Meta_Oct,
                            item.Meta_Nov,
                            item.Meta_Dic,
                        }).ToList();
            }
            return null;
        }

        /// <summary>
        /// Devuelve las Actividades de la tabla UE.Actividad filtradas por Unidad Ejecutora
        /// </summary>
        ///    /// <param name="UEId"></param>
        /// <returns></returns>
        public static IEnumerable GetActividadesByUnidadEjecutora(int UEId)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            return db1.Actividads.Where(item => item.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.Fk_IdUnidadEjecutora == UEId)
                .Select(item => new
                {
                    item.Pk_IdActividad,
                    item.Descripcion
                }).ToList();
        }
        #endregion

        #region ActividadesAsignadas
        public static IEnumerable GetActividadesAsignadasByCierreMensual(int IdCierreMensual)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdCampania = db.CierreMensuals.Where(item => item.Pk_IdCierreMensual == IdCierreMensual)
                                .Select(item => item.Fk_IdCampania)
                                .First();

            return db.Actividads.Where(item => item.ActividadXAccion.AccionXCampania.Fk_IdCampania__UE == IdCampania)
                .Select(item => new
                {
                    item.Pk_IdActividad,
                    IE = item.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Nombre,
                    Persona = item.Persona.NombreCompleto,
                    ProyectoAutorizado = item.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre,
                    Accion = item.ActividadXAccion.AccionXCampania.TipoDeAccion.Nombre,
                    Actividad = item.ActividadXAccion.TipoActividad.Nombre,
                    UnidadDeMedida = item.ActividadXAccion.UnidadDeMedida.Nombre,
                    item.Descripcion,
                    item.Fecha_Inicio,
                    item.FechaFin,
                    item.Asignado_Anual,
                    StatusActividad = item.StatusActividad.Nombre
                }).ToList();
        }


        //Las Actividades Asignadas se estan almacenando en la tabla de UE.Actividad

        public static IEnumerable GetActividadesAsignadasByPersona(string userroll, int? Fk_IdUnidadEjecutora, int? Fk_IdPersona, int? IdAnio, string Estado)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            if (userroll == RolesUsuarios.SUPERUSUARIO
                || userroll == RolesUsuarios.SUP_AUDITOR)
            {
                if (Estado != null)
                {
                    var IdEstado = db.Estadoes.Where(item => item.Nombre == Estado).Select(item => item.Pk_IdEstado);
                    int idEst = IdEstado.Count() == null ? 0 : IdEstado.First();

                    var _m = from actividad in db.Actividads
                             where actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Fk_IdEstado__SIS == idEst
                                    && actividad.ActividadXAccion.TipoActividad.TipoDeAccion.ProyectoAutorizado.Fk_IdAnio == IdAnio
                             orderby actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Nombre
                             select new
                             {
                                 actividad.Pk_IdActividad,
                                 IE = actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Nombre,
                                 PersonaAsignada = actividad.Persona.NombreCompleto,
                                 Campania = actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre,
                                 Accion = actividad.ActividadXAccion.AccionXCampania.TipoDeAccion.Nombre,
                                 TipoActividad = actividad.ActividadXAccion.TipoActividad.Nombre,
                                 UnidadMedida = actividad.ActividadXAccion.UnidadDeMedida.Nombre,
                                 actividad.Descripcion,
                                 actividad.Fecha_Inicio,
                                 actividad.FechaFin,
                                 actividad.Asignado_Anual,
                                 StatusActividad = actividad.StatusActividad.Nombre
                             };

                    return _m.ToList();
                }
                else
                {
                    return null;
                }
            }

            if (userroll == RolesUsuarios.UR_INOCUIDAD
                   || userroll == RolesUsuarios.UR_MOVILIZACION
                   || userroll == RolesUsuarios.UR_ANIMAL
                   || userroll == RolesUsuarios.UR_ACUICOLA_PESQUERA
                   || userroll == RolesUsuarios.UR_VEGETAL
                   || userroll == RolesUsuarios.UR_UPV)
            {
                int IdUnidadResponsable = FuncionesAuxiliares.GetCurrent_IdUnidadResponsable();

                var _m = from actividad in db.Actividads
                         where actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.Fk_IdUnidadResponsable == IdUnidadResponsable
                                && actividad.ActividadXAccion.TipoActividad.TipoDeAccion.ProyectoAutorizado.Fk_IdAnio == IdAnio
                         orderby actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Nombre
                         select new
                         {
                             actividad.Pk_IdActividad,
                             IE = actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Nombre,
                             PersonaAsignada = actividad.Persona.NombreCompleto,
                             Campania = actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre,
                             Accion = actividad.ActividadXAccion.AccionXCampania.TipoDeAccion.Nombre,
                             TipoActividad = actividad.ActividadXAccion.TipoActividad.Nombre,
                             UnidadMedida = actividad.ActividadXAccion.UnidadDeMedida.Nombre,
                             actividad.Descripcion,
                             actividad.Fecha_Inicio,
                             actividad.FechaFin,
                             actividad.Asignado_Anual,
                             StatusActividad = actividad.StatusActividad.Nombre
                         };

                return _m.ToList();
            }

            if (userroll == RolesUsuarios.REPRESENTANTE_ESTATAL)
            {
                int IdEstado = FuncionesAuxiliares.GetCurrent_IdEstado();

                var _m = from actividad in db.Actividads
                         where actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Fk_IdEstado__SIS == IdEstado
                                && actividad.ActividadXAccion.TipoActividad.TipoDeAccion.ProyectoAutorizado.Fk_IdAnio == IdAnio
                         orderby actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Nombre
                         select new
                         {
                             actividad.Pk_IdActividad,
                             IE = actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Nombre,
                             PersonaAsignada = actividad.Persona.NombreCompleto,
                             Campania = actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre,
                             Accion = actividad.ActividadXAccion.AccionXCampania.TipoDeAccion.Nombre,
                             TipoActividad = actividad.ActividadXAccion.TipoActividad.Nombre,
                             UnidadMedida = actividad.ActividadXAccion.UnidadDeMedida.Nombre,
                             actividad.Descripcion,
                             actividad.Fecha_Inicio,
                             actividad.FechaFin,
                             actividad.Asignado_Anual,
                             StatusActividad = actividad.StatusActividad.Nombre
                         };

                return _m.ToList();
            }

            if (userroll == RolesUsuarios.SUP_REGIONAL)
            {
                int IdRegion = FuncionesAuxiliares.GetRegionByUserName();

                var _m = from actividad in db.Actividads
                         where actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Estado.Fk_IdRegion == IdRegion
                                && actividad.ActividadXAccion.TipoActividad.TipoDeAccion.ProyectoAutorizado.Fk_IdAnio == IdAnio
                         orderby actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Nombre
                         select new
                         {
                             actividad.Pk_IdActividad,
                             IE = actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Nombre,
                             PersonaAsignada = actividad.Persona.NombreCompleto,
                             Campania = actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre,
                             Accion = actividad.ActividadXAccion.AccionXCampania.TipoDeAccion.Nombre,
                             TipoActividad = actividad.ActividadXAccion.TipoActividad.Nombre,
                             UnidadMedida = actividad.ActividadXAccion.UnidadDeMedida.Nombre,
                             actividad.Descripcion,
                             actividad.Fecha_Inicio,
                             actividad.FechaFin,
                             actividad.Asignado_Anual,
                             StatusActividad = actividad.StatusActividad.Nombre
                         };

                return _m.ToList();
            }

            if (userroll == RolesUsuarios.IE_VEGETAL
                || userroll == RolesUsuarios.IE_ANIMAL
                || userroll == RolesUsuarios.IE_INOCUIDAD
                || userroll == RolesUsuarios.IE_MOVILIZACION
                || userroll == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || userroll == RolesUsuarios.IE_PROGRAMAS_U
                || userroll == RolesUsuarios.PUESTO_GERENTE
                || userroll == RolesUsuarios.SUP_ESTATAL)  // Al ser Administradores de Una instancia ejecutora ven todas las actividades que se han asignado en su instancia
            {
                var _m = from actividad in db.Actividads
                         where actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.Fk_IdUnidadEjecutora == Fk_IdUnidadEjecutora
                                && actividad.ActividadXAccion.TipoActividad.TipoDeAccion.ProyectoAutorizado.Fk_IdAnio == IdAnio
                         select new
                         {
                             actividad.Pk_IdActividad,
                             IE = actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Nombre,
                             PersonaAsignada = actividad.Persona.NombreCompleto,
                             Campania = actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre,
                             Accion = actividad.ActividadXAccion.AccionXCampania.TipoDeAccion.Nombre,
                             TipoActividad = actividad.ActividadXAccion.TipoActividad.Nombre,
                             UnidadMedida = actividad.ActividadXAccion.UnidadDeMedida.Nombre,
                             actividad.Descripcion,
                             actividad.Fecha_Inicio,
                             actividad.FechaFin,
                             actividad.Asignado_Anual,
                             StatusActividad = actividad.StatusActividad.Nombre
                         };

                return _m.ToList();
            }

            if (userroll == RolesUsuarios.PUESTO_COOR_PROYECTO)
            {
                var _m = from actividad in db.Actividads
                         where (actividad.ActividadXAccion.AccionXCampania.Campania.Fk_IdCoordinador == Fk_IdPersona
                                && actividad.Fk_IdPersona__SIS == Fk_IdPersona)
                                && actividad.ActividadXAccion.TipoActividad.TipoDeAccion.ProyectoAutorizado.Fk_IdAnio == IdAnio
                         select new
                         {
                             actividad.Pk_IdActividad,
                             IE = actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Nombre,
                             PersonaAsignada = actividad.Persona.NombreCompleto,
                             Campania = actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre,
                             Accion = actividad.ActividadXAccion.AccionXCampania.TipoDeAccion.Nombre,
                             TipoActividad = actividad.ActividadXAccion.TipoActividad.Nombre,
                             UnidadMedida = actividad.ActividadXAccion.UnidadDeMedida.Nombre,
                             actividad.Descripcion,
                             actividad.Fecha_Inicio,
                             actividad.FechaFin,
                             actividad.Asignado_Anual,
                             StatusActividad = actividad.StatusActividad.Nombre
                         };

                return _m.ToList();
            }
            else
            {
                var _m = from actividad in db.Actividads
                         where actividad.Fk_IdPersona__SIS == Fk_IdPersona
                                && actividad.ActividadXAccion.TipoActividad.TipoDeAccion.ProyectoAutorizado.Fk_IdAnio == IdAnio
                         select new
                         {
                             actividad.Pk_IdActividad,
                             IE = actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.UnidadEjecutora.Nombre,
                             PersonaAsignada = actividad.Persona.NombreCompleto,
                             Campania = actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre,
                             Accion = actividad.ActividadXAccion.AccionXCampania.TipoDeAccion.Nombre,
                             TipoActividad = actividad.ActividadXAccion.TipoActividad.Nombre,
                             UnidadMedida = actividad.ActividadXAccion.UnidadDeMedida.Nombre,
                             actividad.Descripcion,
                             actividad.Fecha_Inicio,
                             actividad.FechaFin,
                             actividad.Asignado_Anual,
                             StatusActividad = actividad.StatusActividad.Nombre
                         };

                return _m.ToList();
            }
        }

        #endregion
        #region Anios
        /// <summary>
        /// Devuelve el Año de la tabla SIS.Anio
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetAnios()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.Anios.OrderByDescending(item => item.Anio1)
                .Select(item => new
                {
                    item.Pk_IdAnio,
                    item.Anio1
                }).ToList();
        }

        public static int GetIdAnioByAnio(int Anio)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var IdAnio = db.Anios.Where(item => item.Anio1 == Anio)
                                  .Select(item => item.Pk_IdAnio)
                                  .First();

            return IdAnio;
        }
        #endregion

        #region BienOServ
        /// <summary>
        /// Devuelve el Bien o Servicio de la tabla SIS.BienOServ
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetBienesOServ()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            return db.BienOServs.Where(item => item.Fk_IdAnio == IdAnioPres
                                              && (item.esTransversal == false || item.esTransversal == null))
                    .Select(item => new
                    {
                        item.Pk_IdBienOServ,
                        TipoDeBien = item.TipoDeBien.Nombre,
                        item.CABMS,
                        item.Nombre,
                        item.Especificacion,
                        item.Fk_IdTipoDeBien
                    }).ToList();
        }

        public static IEnumerable GetBienesOServTransversales()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            return db.BienOServs.Where(item => item.Fk_IdAnio == IdAnioPres
                                            && item.esTransversal == true)
                    .Select(item => new
                    {
                        item.Pk_IdBienOServ,
                        TipoDeBien = item.TipoDeBien.Nombre,
                        item.CABMS,
                        item.Nombre,
                        item.Especificacion,
                        item.Fk_IdTipoDeBien,
                        item.porcentajeMaximo,
                        item.precioMaximo,
                    }).ToList();
        }

        public static IEnumerable GetBienesOServByUnidadMedida(int? Fk_IdBienOServ)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            var _m = db.BienOservByUnidadMedidas.Where(item => item.Fk_IdBienOServ == Fk_IdBienOServ)
                                                            .Select(item => new {
                                                                item.Pk_IdBienOServByUnidadMedida,
                                                                item.Fk_IdBienOServ,
                                                                Fk_IdBienOServ__SIS = item.Fk_IdBienOServ,
                                                                Pk_IdBienOServ = item.Fk_IdBienOServ,
                                                                item.Fk_IdUnidadDeMedida,
                                                                Fk_IdUnidadDeMedida__SIS = item.Fk_IdUnidadDeMedida,
                                                                Pk_IdUnidadDeMedida = item.Fk_IdUnidadDeMedida,
                                                                Nombre = item.UnidadDeMedida.Nombre,
                                                                BienOServ = item.BienOServ.Nombre
                                            }).ToList();
            return _m;
        }
        #endregion

        #region BienInventariable
        /// <summary>
        /// Devuelve el Bien o Servicio de la tabla SIS.BienOServ
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetBienesInventariables()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int IdAnio = Convert.ToInt32(HttpContext.Current.Session["IdAnio"]);

            return db.BienOServs.Where(b => b.Fk_IdAnio == IdAnio
                                            && (b.TipoDeBien.Partida.ConceptoPartida.CapituloPartida1.Pk_IdCapituloPartida == 12
                                                || b.TipoDeBien.Partida.ConceptoPartida.CapituloPartida1.Pk_IdCapituloPartida == 25)
                                            && (b.esTransversal == false || b.esTransversal == null))
                                 .Select(item => new
                                 {
                                     item.Pk_IdBienOServ,
                                     item.Nombre
                                 }).ToList();
        }
        #endregion

        #region Campania Salud Animal

        //**********************Sanidad Animal*******************************
        /// <summary>
        /// Devuelve los registros de Importancia de la enfermedad SA, de la tabla UE.SA_ImportanciaEnfermedad, lo filtra por la Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetSAImportanciaByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from importancaenfermedad in db1.ImportanciaEnfermedadSAs where importancaenfermedad.Fk_IdCampania == IdCampania select importancaenfermedad;
            return _m.ToList();
        }
        /// <summary>
        /// Devuelve los registros de Atendido SA, de la tabla UE.SA_Atendido, lo filtra por la Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetSAAtendidoByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from atendido in db1.AtendidoSAs where atendido.Fk_IdCampania == IdCampania select atendido;
            return _m.ToList();
        }

        /// <summary>
        /// Devuelve los registros de Población SA, de la tabla UE.SA_Poblacion, lo filtra por la Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetSAPoblacionByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from poblacion in db1.ImportanciaEconomicaSAs where poblacion.Fk_IdCampania == IdCampania select poblacion;
            return _m.ToList();
        }
        #endregion

        #region Importancia Económica Salud Animal
        /// <summary>
        /// Obtiene los registros de Importancia Asociada con respecto a una Campaña
        /// 
        /// Esta información se está capturando para el año 2017
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetImportanciaEconomicaSAByCampania(int? IdCampania, string StatusActual)
        {
            if (IdCampania == null) return null;
            else
            {
                DB_SENASICAEntities db = new DB_SENASICAEntities();
                var StatusSinRepro = FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania);
                if (StatusActual == StatusSinRepro)
                {
                    return db.ImportanciaEconomicaSAs.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
                var StatusEnRepro = FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania);
                if (StatusActual == StatusEnRepro)
                {
                    return db.ImportanciaEconomicaSA_Repro.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
            }
            return null;
        }
        #endregion

        #region CampaniaInocuidad

        //--------------------------------------------------------------
        //**********************Salud Animal*******************************       

        /// <summary>
        /// Devuelve los registros de Inocuidad Pecuaria, de la tabla UE.InoCompSA, lo filtra por la Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetInoCompSAByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from inocompSA in db1.AtendidoInoes where inocompSA.Fk_IdCampania == IdCampania select inocompSA;
            return _m.ToList();
        }


        /// <summary>
        /// Devuelve los registros de Inocuidad Pecuaria, de la tabla UE.InoImportanciaSA, lo filtra por la Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetInoImportanciaSAByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from inoimportanciaSA in db1.ImportanciaInoes where inoimportanciaSA.Fk_IdCampania == IdCampania select inoimportanciaSA;
            return _m.ToList();
        }

        /// <summary>
        /// Devuelve los registros de Inocuidad Pecuaria, de la tabla UE.InoUnidadesCertSA, lo filtra por la Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetInoUnidadesCertSAByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from inounidades in db1.UnidadesCertificarInoSAs where inounidades.Fk_IdCampania == IdCampania select inounidades;
            return _m.ToList();
        }




        //**********************Sanidad Acuicola*******************************       
        /// <summary>
        /// Devuelve los registros de Inocuidad Acuícola y Pesquera, de la tabla UE.InoCompSAC, lo filtra por la Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetInoCompSACByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from inocompSAC in db1.AtendidoInoes where inocompSAC.Fk_IdCampania == IdCampania select inocompSAC;
            return _m.ToList();
        }

        /// <summary>
        /// Devuelve los registros de Inocuidad Snidad Acuícola y Pesquera, de la tabla UE.InoImportanciaSAC, lo filtra por la Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetInoImportanciaSACByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from inoimportanciaSAC in db1.ImportanciaInoes where inoimportanciaSAC.Fk_IdCampania == IdCampania select inoimportanciaSAC;
            return _m.ToList();
        }

        /// <summary>
        ///  Devuelve los registros de Inocuidad Snidad Acuícola y Pesquera, de la tabla UE.InoUnidadesCertSAC, lo filtra por la Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetInoUnidadesCertSACByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from inounidadesSAC in db1.UnidadesCertificarInoSACs where inounidadesSAC.Fk_IdCampania == IdCampania select inounidadesSAC;
            return _m.ToList();
        }

        //**********************************Salud Vegetal***********************************************

        public static IEnumerable GetInoCompSVByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from inocompSV in db1.AtendidoInoes where inocompSV.Fk_IdCampania == IdCampania select inocompSV;
            return _m.ToList();
        }

        public static IEnumerable GetInoUnidadesCertSVByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from inounidadesSV in db1.UnidadesCertificarInoSVs where inounidadesSV.Fk_IdCampania == IdCampania select inounidadesSV;
            return _m.ToList();
        }
        public static IEnumerable GetInoImportanciaSVByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from inoimportanciaSV in db1.ImportanciaInoes where inoimportanciaSV.Fk_IdCampania == IdCampania select inoimportanciaSV;
            return _m.ToList();
        }

        #endregion

        #region Campania Sanidad Vegetal
        //**********************Sanidad Vegetal*******************************
        /// <summary>
        /// Devuelve los registros de Importancia del Cultivo de la Campaña Sanidad Vegetal, la tabla es UE.SVImportanciaCultivo, lo filtra por la Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetSVImportanciaCultivoByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from svimportanciaCultivo in db1.ImportanciaCultivoSVs where svimportanciaCultivo.Fk_IdCampania == IdCampania select svimportanciaCultivo;
            return _m.ToList();
        }

        /// <summary>
        /// Devuelve los registros Atendido de la Campaña Sanidad Vegetal, la tabla es UE.SVAtendido, lo filtra por la Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetSVAtendidoByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from svAtendido in db1.AtendidoSVs where svAtendido.Fk_IdCampania == IdCampania select svAtendido;
            return _m.ToList();
        }

        /// <summary>
        /// Devuelve los registros Importancia de la Plaga, de la Campaña Sanidad Vegetal, la tabla es UE.SVImportanciaPlaga, lo filtra por la Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetSVImportanciaPlagaByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from svimportanciaplaga in db1.ImportanciaPlagaSVs where svimportanciaplaga.Fk_IdCampania == IdCampania select svimportanciaplaga;
            return _m.ToList();
        }
        #endregion

        #region Campania Movilidad
        //************************** Movilidad*******************************
        /// <summary>
        /// Devuelve los registros Importancia de la Campaña Movilización, la tabla es UE.Mov_ImportanciaPIV, lo filtra por la Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetMovImportanciaPIVByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from mov_importanciapiv in db1.ImportanciaPIVMovs where mov_importanciapiv.Fk_IdCampania == IdCampania select mov_importanciapiv;
            return _m.ToList();
        }
        /// <summary>
        /// Devuelve los registros Atendido de la Campaña Movilización, la tabla es UE.Mov_Atendido, lo filtra por la Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetMovAtendidoByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from mov_atendido in db1.AtendidoMovs where mov_atendido.Fk_IdCampania == IdCampania select mov_atendido;
            return _m.ToList();
        }
        /// <summary>
        /// Devuelve los registros Importancia del  Programa de la Campaña Movilización, la tabla es UE.Mov_ImportanciaProg, lo filtra por la Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetMovImportanciaProgByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from mov_importanciapiv in db1.ImportanciaProgramaMovs where mov_importanciapiv.Fk_IdCampania == IdCampania select mov_importanciapiv;
            return _m.ToList();
        }
        #endregion

        #region Campania Sanidad Acuicola y Pesquera
        // ********************** Sanidad Acuicola y Pesquera********************
        /// <summary>
        /// Devuelve los registros, Importancia Cultivo de la Campaña Sanidad Acuícola y Pesquera, la tabla es UE.SACImportanciaCultivo, lo filtra por la Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetSACImportanciaCultByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from SACImportanciaCultivoes in db1.ImportanciaCultivoSACs where SACImportanciaCultivoes.Fk_IdCampania == IdCampania select SACImportanciaCultivoes;
            return _m.ToList();
        }
        /// <summary>
        /// Devuelve los registros, Atendido de la Campaña Sanidad Acuícola y Pesquera, la tabla es UE.SAC_Atendido, lo filtra por la Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetSACAtendidoByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from sac_atendido in db1.AtendidoSACs where sac_atendido.Fk_IdCampania == IdCampania select sac_atendido;
            return _m.ToList();
        }
        /// <summary>
        /// Devuelve los registros, Importancia de la Enfermedad - de la Campaña Sanidad Acuícola y Pesquera, la tabla es UE.SACImportanciaEnfermedad, lo filtra por la Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetSACImportanciaEnfermedadesByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from sac_importanciaenf in db1.ImportanciaEnfermedadSACs where sac_importanciaenf.Fk_IdCampania == IdCampania select sac_importanciaenf;
            return _m.ToList();
        }
        #endregion

        #region CapituloPartida

        /// <summary>
        /// Devuelve Capítulo de la Partida de la tabla SIS.CapituloPartida
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetCapitulosPartida()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            int IdAnio = Convert.ToInt32(HttpContext.Current.Session["IdAnio"]);

            var _m = from capitulopartida in db1.CapituloPartidas 
                     where capitulopartida.Fk_IdAnio == IdAnio
                     select capitulopartida;

            return _m.ToList();
        }

        #endregion

        #region Cargo
        /// <summary>
        /// Devuelve los Cargo de la tabla SIS.Cargo
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetCargo()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from cargo in db1.Cargoes where cargo.Fk_IdTipoDeUnidad == 12 select cargo;

            return _m.ToList();
        }
        #endregion

        #region Certificacion
        /// <summary>
        /// Devuelve los registros de tabla SIS.Certificacion
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetCertificacion()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from certificacion in db1.Certificacions select certificacion;

            return _m.ToList();
        }
        #endregion

        #region ConceptoPartida
        /// <summary>
        /// Devuelve los registros de Tabla SIS.ConceptosPartida
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetConceptosPartida()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from Conceptopartida in db1.ConceptoPartidas select Conceptopartida;


            return _m.ToList();
        }


        /// <summary>
        /// Devuelve los registros de Tabla SIS.ConceptosPartida concatenados con 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetConceptosPartidaConcatenado()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            int IdAnio = Convert.ToInt32(HttpContext.Current.Session["IdAnio"]);

            // var _m = from Conceptopartida in db1.ConceptoPartidas select Conceptopartida;
            var _m = from conceptopartida in db1.ConceptoPartidas
                     where conceptopartida.Fk_IdAnio == IdAnio
                     select new
                     {
                         Pk_IdConceptoPartida = conceptopartida.Pk_IdConceptoPartida,
                         Nombre = conceptopartida.CapituloPartida1.Nombre + " | " + conceptopartida.Nombre

                     };

            return _m.ToList();
        }
        #endregion

        #region EstadoBien
        /// <summary>
        /// Realiza la consulta de la tabla SIS.EstadoBien
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetEstadoBien()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from estadobien in db1.EstadoBiens select estadobien;

            return _m.ToList();
        }
        #endregion

        #region EstadoMunicipio

        public static string getEstadoNombreSpatial(double lng, double lat)
        {
            DbGeometry punto = DbGeometry.PointFromText(string.Format("POINT({0} {1})", lng, lat), 0);
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var Nombre =
                from estadosGeo in db1.EstadosGeos
                where estadosGeo.GEOM.Contains(punto)
                select estadosGeo.NOM_ENT;
            return Nombre.FirstOrDefault();
        }

        public static string getMunicipioNombreSpatial(double lng, double lat)
        {
            DbGeometry punto = DbGeometry.PointFromText(string.Format("POINT({0} {1})", lng, lat), 0);
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var Nombre =
                from municipiosGeo in db1.MunicipiosGeos
                    //where municipiosGeo.GEOM.Contains(punto)
                where punto.Within(municipiosGeo.GEOM)
                select municipiosGeo.NOM_MUN;
            return Nombre.FirstOrDefault();
        }

        public static string getMunicipioSpatial(double lng, double lat)
        {
            DbGeometry punto = DbGeometry.PointFromText(string.Format("POINT({0} {1})", lng, lat), 0);
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var Datos =
                (from municipiosGeo in db1.MunicipiosGeos
                     //where municipiosGeo.GEOM.Contains(punto)
                 where punto.Within(municipiosGeo.GEOM)
                 select new { municipiosGeo.NOM_MUN, municipiosGeo.id });

            var single = Datos.SingleOrDefault();
            string[] valor = new string[] { single.id.ToString(), single.NOM_MUN };
            //return valor;
            return string.Concat(single.id.ToString(), string.Concat(".", single.NOM_MUN));
        }

        /// <summary>
        /// Devuelve una lista de los Estados que se encuentran en la tabla SIS.Estado
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetEstados()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from estados in db1.Estadoes select estados;

            return _m.ToList();
        }

        /// <summary>
        /// Obtiene los tipos de campañas. Se utiliza para enlazar al proyecto presupuesto con un tipo de campaña
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetTiposCampanias()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.TipoCampanias.ToList();
        }

        public static IEnumerable GetEstadosByRol(string userroll, int? IdEstado)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            IQueryable<Estado> _data = null;

            if (userroll == RolesUsuarios.SUPERUSUARIO
                || userroll == RolesUsuarios.UR_ACUICOLA_PESQUERA
                || userroll == RolesUsuarios.UR_ANIMAL
                || userroll == RolesUsuarios.UR_INOCUIDAD
                || userroll == RolesUsuarios.UR_MOVILIZACION
                || userroll == RolesUsuarios.UR_VEGETAL)
            {
                _data = db.Estadoes;
            }

            if (userroll == RolesUsuarios.SUP_ESTATAL)
            {
                _data = db.Estadoes.Where(e => e.Pk_IdEstado == IdEstado);

            }

            if (userroll == RolesUsuarios.SUP_REGIONAL)
            {
                int reg = FuncionesAuxiliares.GetRegionByUserName();
                _data = db.Estadoes.Where(e => e.Fk_IdRegion == reg);

            }

            if (userroll == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || userroll == RolesUsuarios.IE_ANIMAL
                || userroll == RolesUsuarios.IE_INOCUIDAD
                || userroll == RolesUsuarios.IE_MOVILIZACION
                || userroll == RolesUsuarios.IE_VEGETAL)
            {
                _data = db.Estadoes.Where(e => e.Pk_IdEstado == IdEstado);
            }


            return _data.ToList();

        }

        /// <summary>
        /// Devuelve una lista de los Municipios que se encuentran en la tabla SIS.Municipio, se filtra por el Estado
        /// </summary>
        /// <param name="Pk_IdEstado"></param>
        /// <returns></returns>
        public static IEnumerable GetMunicipiosByEstado(int? Pk_IdEstado)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            if (Pk_IdEstado == null)
            {
                var _m = from municipio in db1.Municipios
                         select municipio;
                return _m.ToList();
            }
            else
            {
                var _m = from municipio in db1.Municipios
                         where municipio.Fk_IdEstado__SIS == Pk_IdEstado
                         select municipio;
                return _m.ToList();
            }
        }

        #endregion

        #region Combos de Reporte
        /// <summary>
        /// Obtiene la lista de reportes del sistema para getionar sus firmas autirizadas
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetReportes()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var _m = from reportes in db.Reportes select reportes;

            return _m.ToList();
        }
        #endregion

        #region Filtros de los Grids

        //AQUÍ EMPIEZAN LOS FILTROS DE LOS GRIDS ***************************************************************************
        //INSTANCIA EJECUTORA/UNIDAD EJECUTORA ***************************************************************************
        //Esta funcion se sobrecarga para alimentar el combo en las pantallas de Campaña  SA,SV,SAC,MOV
        //que pueden manejar mas de un subcomponente

        /// <summary>
        /// Filtrado la Unidad Ejecutora por el Estado, y dos tipos de Subcomponente
        /// </summary>
        /// <param name="_Fk_IdEstado"></param>
        /// <param name="subcomponente"></param>
        /// <param name="subcomponente2"></param>
        /// <returns></returns>
        public static IEnumerable GetEstadoUnidadEjecutoraBySubComponente(int? _Fk_IdEstado, int? subcomponente, int? subcomponente2, int? Fk_IdUnidadEjecutora)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            //este primer if se hizo para poder hacer el filtrado de la pantalla de Movilización.
            //no es una buena práctica, pero se hizo de esta forma para que lo demás no sufriera cambios
            if (subcomponente == null && subcomponente2 == null)
            {
                if (Fk_IdUnidadEjecutora == null || Fk_IdUnidadEjecutora == 0)
                {
                    var _m = from unidadEjecutora in db1.UnidadEjecutoras
                             where unidadEjecutora.CT_LIVE == true
                             select unidadEjecutora;

                    return _m.ToList();
                }
                else
                {
                    var _m = from unidadEjecutora in db1.UnidadEjecutoras
                             where unidadEjecutora.CT_LIVE == true && unidadEjecutora.Pk_IdUnidadEjecutora == Fk_IdUnidadEjecutora
                             select unidadEjecutora;

                    return _m.ToList();
                }
            }
            else
            {
                if (_Fk_IdEstado == null || _Fk_IdEstado == 0)
                {
                    var _m = from unidadejecutora in db1.UnidadEjecutoras
                             where unidadejecutora.CT_LIVE == true && (unidadejecutora.Fk_IdSubComponente == subcomponente || unidadejecutora.Fk_IdSubComponente == subcomponente2
                                                                        || unidadejecutora.Pk_IdUnidadEjecutora == Fk_IdUnidadEjecutora)
                             select unidadejecutora;
                    return _m.ToList();
                }
                else
                {
                    var _m = from unidadejecutora in db1.UnidadEjecutoras
                             where unidadejecutora.CT_LIVE == true
                                    && (unidadejecutora.Fk_IdEstado__SIS == _Fk_IdEstado && (unidadejecutora.Fk_IdSubComponente == subcomponente || unidadejecutora.Fk_IdSubComponente == subcomponente2)
                                        || unidadejecutora.Pk_IdUnidadEjecutora == Fk_IdUnidadEjecutora)
                             select unidadejecutora;
                    return _m.ToList();
                }
            }
        }

        //PADRON DE UNIDADES DE PRODUCCION/PRODUCTOR ***************************************************************************
        /// <summary>
        /// Devuelve los Productores de la tabla UE.Productor, lo filtra por Estado
        /// </summary>
        /// <param name="_Fk_IdEstado"></param>
        /// <returns></returns>
        public static IEnumerable GetProductorByEstado(int? _Fk_IdEstado)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            string rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();

            if (rolUsuario == RolesUsuarios.SUP_REGIONAL)
            {
                int IdRegion = FuncionesAuxiliares.GetRegionByUserName();

                return db1.Productors.Where(item => item.Estado.Fk_IdRegion == IdRegion).ToList();
            }

            if (rolUsuario == RolesUsuarios.REPRESENTANTE_ESTATAL)
            {
                int IdEstado = FuncionesAuxiliares.GetCurrent_IdEstado();

                return db1.Productors.Where(item => item.Fk_IdEstado__SIS == IdEstado).ToList();
            }

            if (_Fk_IdEstado == null || _Fk_IdEstado == 0)
            {
                var _m = from productor in db1.Productors select productor;
                return _m.ToList();
            }
            else
            {
                var _m = from productor in db1.Productors
                         where productor.Fk_IdEstado__SIS == _Fk_IdEstado
                         select productor;
                return _m.ToList();
            }
        }

        //PROGRAMAS DE TRABAJO/CAMPANIA ***************************************************************************

        //********************Comienza la Funcion**************************************
        //PERSONAL A CARGO DE LA UNIDAD ADMINISTRATIVA/PERSONA ***************************************************************************
        /// <summary>
        ///Devuelve las Personas de la tabla SIS.Persona, lo filtra por Unidad Ejecutora
        /// </summary>
        /// <param name="_Fk_IdUnidadEjecutora"></param>
        /// <returns></returns>
        public static IEnumerable GetPersonaByUnidadEjecutora(int? _Fk_IdUnidadEjecutora)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            if (_Fk_IdUnidadEjecutora == null)
            {
                return db1.Personas.Where(item => item.esActivo == true).ToList();
            }
            else
            {
                return db1.Personas.Where(item => item.esActivo == true
                                        && item.Fk_IdUnidadEjecutora__UE == _Fk_IdUnidadEjecutora).ToList();
            }
        }
        //INVENTARIO DE LA UNIDAD ADMINISTRATIVA/INVENTARIO ***************************************************************************
        /// <summary>
        /// Devuelve los Inventarios de la tabla UE.Inventario, lo filtra por Unidad Ejecutora
        /// </summary>
        /// <param name="_Fk_IdUnidadEjecutora"></param>
        /// <returns></returns>
        public static IEnumerable GetInventariosByUnidadEjecutora(int? _Fk_IdUnidadEjecutora)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            if (_Fk_IdUnidadEjecutora == null)
            {
                var _m = from inventario in db1.Inventarios select inventario;
                return _m.ToList();
            }
            else
            {
                var _m = from inventario in db1.Inventarios
                         where inventario.Fk_IdUnidadEjecutora == _Fk_IdUnidadEjecutora
                         select inventario;
                return _m.ToList();
            }
        }
        //INSTALACIONES DE LA UNIDAD ADMINISTRATIVA/INSTALACION ***************************************************************************
        /// <summary>
        /// Devuelve las Intalaciones de la tabla UE.Instalacion, lo filtra por Unidad Ejecutora
        /// </summary>
        /// <param name="_Fk_IdUnidadEjecutora"></param>
        /// <returns></returns>
        public static IEnumerable GetInstalacionByUnidadEjecutora(int? _Fk_IdUnidadEjecutora)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            if (_Fk_IdUnidadEjecutora == null)
            {
                var _m = from instalacion in db1.Instalacions select instalacion;
                return _m.ToList();
            }
            else
            {
                var _m = from instalacion in db1.Instalacions
                         where instalacion.Fk_IdUnidadEjecutora == _Fk_IdUnidadEjecutora
                         select instalacion;
                return _m.ToList();
            }
        }




        //ADMINISTRACION DE ACCIONES/ACTIVIDAD ***************************************************************************
        /// <summary>
        /// Devuelve las Actividades de la tabla UE.Actividad, lo filtra por Acción
        /// </summary>
        /// <param name="_Fk_IdAccion"></param>
        /// <returns></returns>
        public static IEnumerable GetactividadByAccion(int? _Fk_IdAccion)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            if (_Fk_IdAccion == null)
            {
                var _m = from actividad in db1.Actividads select actividad;
                return _m.ToList();
            }
            else
            {
                var _m = from actividad in db1.Actividads
                         where actividad.ActividadXAccion.Fk_IdAccionXCampania == _Fk_IdAccion
                         select actividad;
                return _m.ToList();
            }
        }

        //ADMINISTRACION DE ACCTIVIDADES/ALCANCE ***************************************************************************
        /// <summary>
        /// Devuelve las Acciones de las Campanias, los filtra por UnidadEjecutora y Acciones
        /// </summary>
        /// <param name="_Fk_IdAccion"></param>
        /// <param name="_Fk_IdUnidadEjecutora"></param>
        /// <returns></returns>
        public static IEnumerable GetCampaniaAlcanceByUnidadEjecutora(int? _Fk_IdAccion, int? _Fk_IdUnidadEjecutora)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            if (_Fk_IdAccion == null)
            {
                var _m = from accion in db1.Accions
                         where accion.Campania.ProyectoPresupuesto.Fk_IdUnidadEjecutora == _Fk_IdUnidadEjecutora
                         select accion;
                return _m.ToList();
            }
            else
            {
                var _m = from accion in db1.Accions
                         where accion.Fk_IdCampania__UE == _Fk_IdAccion
                         select accion;
                return _m.ToList();
            }
        }
        #endregion

        #region Instalaciones

        /// <summary>
        /// Devuelve las Instalaciones de la tabla UE.Instalacion, se filtra es por Unidad Ejecutora
        /// </summary>
        /// <param name="IdUnidadEjecutora"></param>
        /// <returns></returns>
        public static IEnumerable GetInstalacionesByUnidadEjecutora(int? IdUnidadEjecutora)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from instalacion in db1.Instalacions where instalacion.Fk_IdUnidadEjecutora == IdUnidadEjecutora select instalacion;

            return _m.ToList();
        }
        #endregion

        #region Inventario

        /// <summary>
        /// Devuelve los Inventarios de la tabla UE.Inventario, los filtra por Unidad Ejecutora
        /// </summary>
        /// <param name="IdUnidadEjecutora"></param>
        /// <returns></returns>
        public static IEnumerable GetInventarioByUnidadEjecutora(int? IdUnidadEjecutora)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.Inventarios.Where(item => item.Fk_IdUnidadEjecutora == IdUnidadEjecutora)
                                .OrderBy(item => item.Clave).ToList();
        }
        #endregion

        #region NecesidadesXAccion
        /// <summary>
        /// Devuelve las Necesidades de la tabla UE.NecesidadXAccion, filtrando los registros por Acción
        /// </summary>
        /// <param name="IdAccion"></param>
        /// <returns></returns>
        public static IEnumerable GetNecesidadesByAccion(int? IdAccion)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            if (IdAccion == null)
            {
                var _m = from necesidadesXaccion in db1.NecesidadXAccions
                         select necesidadesXaccion;
                return _m.ToList();
            }
            else
            {
                var _m = from necesidadesXaccion in db1.NecesidadXAccions
                         where necesidadesXaccion.Fk_IdAccionXCampania == IdAccion
                         select necesidadesXaccion;
                return _m.ToList();
            }
        }
        #endregion

        #region NecesidadesXUnidadEjecutora
        /// <summary>
        /// Devuelve las Necesidades de la tabla UE.NecesidadXAccion, filtrando los registros por Acción
        /// </summary>
        /// <param name="IdAccion"></param>
        /// <returns></returns>
        public static IEnumerable GetNecesidadesByUnidadEjecutora(int? UEId)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            if (UEId == null)
            {
                var _m = from necesidadesXaccion in db1.NecesidadXAccions
                         select necesidadesXaccion;
                return _m.ToList();
            }
            else
            {
                var _m = from necesidadesXaccion in db1.NecesidadXAccions
                         where necesidadesXaccion.AccionXCampania.Campania.ProyectoPresupuesto.Fk_IdUnidadEjecutora == UEId
                         select necesidadesXaccion;
                return _m.ToList();
            }
        }
        #endregion

        #region Programa_Componente_Incentivo

        /// <summary>
        /// Devuelve una consulta de los programas que estan en la Tabla SIS.Programa
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetProgramas()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from programas in db1.Programas select programas;

            return _m.ToList();
        }

        /// <summary>
        /// Devuelve una consulta de los Componentes que estan en la Tabla SIS.Componente
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetComponente()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            return db.Componentes.Where(item => item.Fk_IdAnio == IdAnioPres).ToList();
        }
        /// <summary>
        /// Devuelve una consulta de los Incentivos que estan en la Tabla SIS.Incentivo
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetIncentivo()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            return db.Incentivoes.Where(item => item.Componente.Fk_IdAnio == IdAnioPres).ToList();
        }

        #endregion

        #region Partida
        public static IEnumerable GetPartida()
        {
            DXMVCWebApplication1.Models.DB_SENASICAEntities db1 = new DXMVCWebApplication1.Models.DB_SENASICAEntities();
            int IdAnio = Convert.ToInt32(HttpContext.Current.Session["IdAnio"]);

            var _m =
                    from partida in db1.Partidas
                    where partida.Fk_IdAnio == IdAnio
                    select new
                    {
                        Pk_IdPartida = partida.Pk_IdPartida,
                        Descripcion = partida.ConceptoPartida.CapituloPartida1.Nombre + " | " + partida.ConceptoPartida.Nombre + " | " + partida.Nombre
                    };

            return _m.ToList();
        }
        #endregion

        #region ProyectoAutorizado

        ///// <summary>
        ///// Devuelve los Productos Autorizados de la tabla SIS.ProyectoAutorizado
        ///// </summary>
        ///// <returns></returns>
        public static IEnumerable GetProyectosAutorizados()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            return db.ProyectoAutorizadoes.Where(item => item.Fk_IdAnio == IdAnioPresupuestal)
                        .OrderBy(item => item.Fk_IdSubComponente__SIS).ThenBy(item => item.Nombre)
                        .ToList();
        }

        /// <summary>
        /// Este método es para regresar la información de los proyectos autorizados que puede ocupar el usuario logeado en cuestión
        /// en el popUp de edición/creación de Campaña.
        /// 
        /// Solo los usuarios de los comités podrán hacer modificaciones a la campaña, los demás usuarios que tengan acceso a 
        /// esta pantalla solo podrán visualizar.
        /// 
        /// Este método es para los combos escalonados, y el único que va a ocuparlo será el comité
        /// </summary>
        /// <param name="subcomponente"></param>
        /// <returns></returns>
        public static IEnumerable GetProyectosAutorizadosBySubComponente(int IdSubComponente, int? IdProyectoPresupuesto = null)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            string rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();
            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            List<ProyectoPresupuesto> _query = null;

            if (rolUsuario == RolesUsuarios.IE_VEGETAL
                || rolUsuario == RolesUsuarios.IE_ANIMAL
                || rolUsuario == RolesUsuarios.IE_INOCUIDAD
                || rolUsuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || rolUsuario == RolesUsuarios.IE_MOVILIZACION
                || rolUsuario == RolesUsuarios.PUESTO_COOR_PROYECTO
                || rolUsuario == RolesUsuarios.COORDINADOR_CAMPANIA
                || rolUsuario == RolesUsuarios.PUESTO_COOR_REGIONAL
                || rolUsuario == RolesUsuarios.PUESTO_GERENTE
                || rolUsuario == RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                || rolUsuario == RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                || rolUsuario == RolesUsuarios.SUP_ESTATAL
                || rolUsuario == RolesUsuarios.REPRESENTANTE_ESTATAL)
            {
                int IdEstado = FuncionesAuxiliares.GetCurrent_IdEstado();
                int IdIE = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

                _query = db.ProyectoPresupuestoes.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                && item.ProyectoAutorizado.Fk_IdSubComponente__SIS == IdSubComponente
                                                                && item.Fk_IdUnidadEjecutora == IdIE).ToList();
            }
            else
            {
                _query = db.ProyectoPresupuestoes.Where(item => item.Pk_IdProyectoPresupuesto == IdProyectoPresupuesto).ToList();
            }

            return _query;
        }

        public static IEnumerable GetProyectosAutorizadosByUR(int IdSubComponente)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            string rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();
            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            List<ProyectoAutorizado> _query = null;

            if (rolUsuario == RolesUsuarios.UR_ACUICOLA_PESQUERA
                || rolUsuario == RolesUsuarios.UR_ANIMAL
                || rolUsuario == RolesUsuarios.UR_INOCUIDAD
                || rolUsuario == RolesUsuarios.UR_MOVILIZACION
                || rolUsuario == RolesUsuarios.UR_VEGETAL)
            {
                int IdUR = FuncionesAuxiliares.GetCurrent_IdUnidadResponsable();

                var _query2 = from unidadRespXSub in db.UnidadResponsableXSubComponentePres
                                join proyectoAutorizado in db.ProyectoAutorizadoes on unidadRespXSub.Fk_IdSubComponente equals proyectoAutorizado.Fk_IdSubComponente__SIS
                                where unidadRespXSub.Fk_IdUnidadResponsable == IdUR
                                && proyectoAutorizado.Fk_IdAnio == IdAnioPresupuestal
                                && unidadRespXSub.Fk_IdSubComponente == IdSubComponente
                                select proyectoAutorizado;

                _query = _query2.ToList();
            }

            return _query;
        }

        public static IEnumerable GetProyectosAutorizadosById(int Id)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.ProyectoAutorizadoes.Where(item => item.Pk_IdProyectoAutorizado == Id).ToList();
        }

        /// <summary>
        /// Este método es para adquirir los proyectos que están siendo trabajados por la Unidad Responsable en cuestión
        /// y por el estado que haya elegido.
        /// 
        /// Es para poder asignar presupusto a los proyectos de cierto estado
        /// </summary>
        /// <param name="Estado"></param>
        /// <returns></returns>
        public static IEnumerable GetProyectosPresupuestalByEstado(string Estado)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            string rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();
            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            int IdUnidadResponsable = FuncionesAuxiliares.getIdUnidadResponsable(rolUsuario);

            var _data = from unidadResponsableXSubcomponente in db.UnidadResponsableXSubComponentePres
                        join SC in db.SubComponentes on unidadResponsableXSubcomponente.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                        join PA in db.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                        join PP in db.ProyectoPresupuestoes on PA.Pk_IdProyectoAutorizado equals PP.Fk_IdProyectoAutorizado
                        where unidadResponsableXSubcomponente.Fk_IdUnidadResponsable == IdUnidadResponsable
                        && PP.Fk_IdAnio == IdAnioPresupuestal
                        && PP.Estado.Nombre == Estado
                        orderby PP.Estado.Nombre ascending, SC.Nombre ascending
                        select PP;

            return _data.ToList();
        }

        /// <summary>
        /// Devuelve los el presupuesto por proyecto de acuerdo al rol del usuario logeado
        /// </summary>
        /// <param name="unidadresponsable"></param>
        /// <returns></returns>
        public static IEnumerable GetProyectosPresupuestalesByRol()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            IQueryable<ProyectoPresupuesto> _data = null;

            string rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();
            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            if (rolUsuario == RolesUsuarios.SUPERUSUARIO
                || rolUsuario == RolesUsuarios.SUP_NACIONAL)
            {
                _data = db.ProyectoPresupuestoes.Where(item => item.Fk_IdAnio == IdAnioPresupuestal);
            }

            if (rolUsuario == RolesUsuarios.SUP_REGIONAL)
            {
                int IdRegion = FuncionesAuxiliares.GetRegionByUserName();

                _data = db.ProyectoPresupuestoes.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                        && item.Estado.Fk_IdRegion == IdRegion);
            }

            if (rolUsuario == RolesUsuarios.UR_INOCUIDAD
                || rolUsuario == RolesUsuarios.UR_MOVILIZACION
                || rolUsuario == RolesUsuarios.UR_ANIMAL
                || rolUsuario == RolesUsuarios.UR_ACUICOLA_PESQUERA
                || rolUsuario == RolesUsuarios.UR_VEGETAL
                || rolUsuario == RolesUsuarios.UR_UPV)
            {

                int IdUnidadResponsable = FuncionesAuxiliares.getIdUnidadResponsable(rolUsuario);

                _data = db.ProyectoPresupuestoes.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                        && item.Fk_IdUnidadResponsable == IdUnidadResponsable);
            }

            if (rolUsuario == RolesUsuarios.SUP_ESTATAL
                || rolUsuario == RolesUsuarios.REPRESENTANTE_ESTATAL)
            {
                int IdEstado = FuncionesAuxiliares.GetCurrent_IdEstado();

                _data = db.ProyectoPresupuestoes.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                        && item.Fk_IdEstado == IdEstado);
            }

            if (rolUsuario == RolesUsuarios.IE_INOCUIDAD
                || rolUsuario == RolesUsuarios.IE_MOVILIZACION
                || rolUsuario == RolesUsuarios.IE_ANIMAL
                || rolUsuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || rolUsuario == RolesUsuarios.IE_VEGETAL
                || rolUsuario == RolesUsuarios.IE_PROGRAMAS_U)
            {
                int IdUnidadEjecutora = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

                _data = db.ProyectoPresupuestoes.Where(item => item.Fk_IdUnidadEjecutora == IdUnidadEjecutora 
                                                            && item.Fk_IdAnio == IdAnioPresupuestal);
            }

            return _data.OrderBy(item => item.Estado.Nombre).ToList();
        }

        public static IEnumerable GetProyectosPresupuestalesPorElEstado()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            IQueryable<ProyectoPresupuesto> _data = null;

            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            _data = db.ProyectoPresupuestoes.Where(item => item.Fk_IdAnio == IdAnioPresupuestal && item.Fk_IdUnidadResponsable == 25)
                    .OrderBy(item => item.Estado.Nombre);

            return _data.ToList();
        }
        #endregion

        #region PROGRAMA ANUAL DE AQUISICIONES
        public static IEnumerable GetProgramaAnualAdqByCierreMensual(int IdCierreMensual)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdCampania = db.CierreMensuals.Where(item => item.Pk_IdCierreMensual == IdCierreMensual)
                                .Select(item => item.Fk_IdCampania)
                                .First();

            var _query = db.VW_ReporteAdquisiciones.Where(item => item.Fk_IdCampania__UE == IdCampania);

            return _query.ToList();
        }

        //PROGRAMA ANUAL DE AQUISICIONES COMPLETO/POR UNIDAD EJECUTORA ******************************************************************
        /// <summary>
        /// Devuelve los Programas Anual de Adquisición de la instancia ejecutora completo incluyendo Necesidades por campaña y Necesidades por Accion de la tabla UE.ProgramaAnualAdqs; 
        /// Esta tabla esta dividida en tres niveles:
        /// Necesidades relacionads con la Instancia Ejecutora Almacenan Fk_IdAccionXCampania = null, Fk_IdActividadXAccion == null
        /// Necesidades relacionadas con la campaña Almacenan Fk_IdActividadXAccion = null
        /// Necesidades relacionadas con la Actividad Almacenan el Id de ActividadXAccion
        /// </summary>
        /// <param name="_Fk_IdUnidadEjecutora"></param>
        /// <returns></returns>
        public static IEnumerable GetProgramaAnualAdqCompletoByUnidadEjecutora(int? _Fk_IdUnidadEjecutora, int _Fk_IdAnio, string Estado)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            string rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();

            if (rolUsuario == RolesUsuarios.SUPERUSUARIO
                || rolUsuario == RolesUsuarios.SUP_AUDITOR)
            {
                if (Estado != null)
                {
                    var IdEstado = db.Estadoes.Where(item => item.Nombre == Estado).Select(item => item.Pk_IdEstado);

                    int idEst = IdEstado.Count() == null ? 0 : IdEstado.First();

                    var _m = db.VW_ReporteAdquisiciones.Where(item => item.Fk_IdAnio__SIS == _Fk_IdAnio
                                                                && item.Fk_IdEstado__SIS == idEst);
                    return _m.ToList();
                }
                else
                {
                    return null;
                }
            }

            if (rolUsuario == RolesUsuarios.IE_VEGETAL
                || rolUsuario == RolesUsuarios.IE_ANIMAL
                || rolUsuario == RolesUsuarios.IE_INOCUIDAD
                || rolUsuario == RolesUsuarios.IE_MOVILIZACION
                || rolUsuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || rolUsuario == RolesUsuarios.IE_PROGRAMAS_U
                || rolUsuario == RolesUsuarios.PUESTO_GERENTE
                || rolUsuario == RolesUsuarios.PUESTO_AUXILIAR_ADMINISTRATIVO
                || rolUsuario == RolesUsuarios.PUESTO_COOR_ADMINISTRATIVO
                || rolUsuario == RolesUsuarios.SUP_ESTATAL)
            {
                var _m = db.VW_ReporteAdquisiciones.Where(item => item.Fk_IdUnidadEjecutora__UE == _Fk_IdUnidadEjecutora
                                                            && item.Fk_IdAnio__SIS == _Fk_IdAnio);

                return _m.ToList();
            }

            if (rolUsuario == RolesUsuarios.UR_ANIMAL
                || rolUsuario == RolesUsuarios.UR_VEGETAL
                || rolUsuario == RolesUsuarios.UR_INOCUIDAD
                || rolUsuario == RolesUsuarios.UR_ACUICOLA_PESQUERA
                || rolUsuario == RolesUsuarios.UR_MOVILIZACION
                || rolUsuario == RolesUsuarios.UR_UPV)
            {
                int IdUnidadResponsable = FuncionesAuxiliares.GetCurrent_IdUnidadResponsable();

                var _m = db.VW_ReporteAdquisiciones.Where(item => item.Fk_IdUnidadResponsable__UE == IdUnidadResponsable
                                                                    && item.Fk_IdAnio__SIS == _Fk_IdAnio);

                return _m.ToList();
            }

            if (rolUsuario == RolesUsuarios.REPRESENTANTE_ESTATAL)
            {
                int IdEstado = FuncionesAuxiliares.GetCurrent_IdEstado();

                var _m = db.VW_ReporteAdquisiciones.Where(item => item.Fk_IdEstado__SIS == IdEstado
                                                                    && item.Fk_IdAnio__SIS == _Fk_IdAnio);

                return _m.ToList();
            }

            if (rolUsuario == RolesUsuarios.SUP_REGIONAL)
            {
                int IdRegion = FuncionesAuxiliares.GetRegionByUserName();

                var _m = db.VW_ReporteAdquisiciones.Where(item => item.Fk_IdRegion == IdRegion
                                                                    && item.Fk_IdAnio__SIS == _Fk_IdAnio);

                return _m.ToList();
            }

            if (rolUsuario == RolesUsuarios.PUESTO_COOR_PROYECTO)
            {
                int IdPersona = FuncionesAuxiliares.GetCurrent_IdPersona();

                var _m = db.VW_ReporteAdquisiciones.Where(item => item.Fk_IdCoordinador == IdPersona
                                                                    && item.Fk_IdAnio__SIS == _Fk_IdAnio);

                return _m.ToList();
            }

            return null;
        }


        //PROGRAMA ANUAL DE AQUISICIONES/POR UNIDAD EJECUTORA ******************************************************************
        /// <summary>
        /// Devuelve los Programas Anual de Adquisición de la tabla UE.ProgramaAnualAdqs, lo filtra SOLO por Unidad Ejecutora; 
        /// Esta tabla esta dividida en tres niveles:
        /// Necesidades relacionads con la Instancia Ejecutora Almacenan Fk_IdAccionXCampania = null, Fk_IdActividadXAccion == null
        /// Necesidades relacionadas con la campaña Almacenan Fk_IdActividadXAccion = null
        /// Necesidades relacionadas con la Actividad Almacenan el Id de ActividadXAccion
        /// </summary>
        /// <param name="_Fk_IdUnidadEjecutora"></param>
        /// <returns></returns>   
        public static IEnumerable GetProgramaAnualAdqByUnidadEjecutora(string userroll, int? _Fk_IdUnidadEjecutora, int? _Fk_IdUnidadResponsable, int _Fk_IdAnio, int? _Fk_IdEstado, bool esGastoU)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            if (userroll == RolesUsuarios.SUPERUSUARIO
                || userroll == RolesUsuarios.SUP_NACIONAL)
            {
                return db.ProgramaAnualAdqs.Where(item => item.Fk_IdAnio__SIS == _Fk_IdAnio
                                                            && item.Fk_IdActividadXAccion == null
                                                            && item.Fk_IdAccionXCampania == null
                                                            && item.Fk_IdCampania__UE == null
                                                            && item.esGastoU == esGastoU)
                                            .Select(item => new {
                                                                item.Pk_IdProgramaAnualAdq,
                                                                IE = item.UnidadEjecutora.Nombre,
                                                                BienOServ = item.BienOServ.Nombre,
                                                                UnidadDeMedida = item.UnidadDeMedida.Nombre,
                                                                item.Cant_Anual,
                                                                item.imp_Anual,
                                                                item.RecFed,
                                                                item.RecEst,
                                                                item.RecProp,
                                                                item.Fk_IdBienOServ__SIS,
                                                                item.Fk_IdUnidadDeMedida__SIS,
                                                                item.BienOServ.porcentajeMaximo,
                                                                item.BienOServ.precioMaximo,
                                                                item.Cant_Ene,
                                                                item.Cant_Feb,
                                                                item.Cant_Mar,
                                                                item.Cant_Abr,
                                                                item.Cant_May,
                                                                item.Cant_Jun,
                                                                item.Cant_Jul,
                                                                item.Cant_Ago,
                                                                item.Cant_Sep,
                                                                item.Cant_Oct,
                                                                item.Cant_Nov,
                                                                item.Cant_Dic,
                                                                item.Justificacion,
                                                                item.imp_Ene,
                                                                item.imp_Feb,
                                                                item.imp_Mar,
                                                                item.imp_Abr,
                                                                item.imp_May,
                                                                item.imp_Jun,
                                                                item.imp_Jul,
                                                                item.imp_Ago,
                                                                item.imp_Sep,
                                                                item.imp_Oct,
                                                                item.imp_Nov,
                                                                item.imp_Dic
                                            }).ToList();
            }

            if (userroll == RolesUsuarios.UR_ACUICOLA_PESQUERA
               || userroll == RolesUsuarios.UR_ANIMAL
               || userroll == RolesUsuarios.UR_INOCUIDAD
               || userroll == RolesUsuarios.UR_MOVILIZACION
               || userroll == RolesUsuarios.UR_VEGETAL
               || userroll == RolesUsuarios.UR_UPV)
            {
                var _m = (from paa in db.ProgramaAnualAdqs
                          join UEj in db.UnidadEjecutoras on paa.Fk_IdUnidadEjecutora__UE equals UEj.Pk_IdUnidadEjecutora
                          join PP in db.ProyectoPresupuestoes on UEj.Pk_IdUnidadEjecutora equals PP.Fk_IdUnidadEjecutora
                          join C in db.Campanias on PP.Pk_IdProyectoPresupuesto equals C.Fk_IdProyectoPresupuesto
                          join SC in db.SubComponentes on UEj.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                          join UR in db.UnidadResponsables on UEj.Fk_IdUnidadResponsable__UE equals UR.Pk_IdUnidadResponsable
                          join bs in db.BienOServs on paa.Fk_IdBienOServ__SIS equals bs.Pk_IdBienOServ
                          join um in db.UnidadDeMedidas on paa.Fk_IdUnidadDeMedida__SIS equals um.Pk_IdUnidadDeMedida
                          where paa.Fk_IdAnio__SIS == _Fk_IdAnio
                                && PP.Fk_IdUnidadResponsable == _Fk_IdUnidadResponsable
                                && paa.Fk_IdActividadXAccion == null
                                && paa.Fk_IdAccionXCampania == null
                                && paa.Fk_IdCampania__UE == null
                                && paa.esGastoU == esGastoU
                          group paa by new
                          {
                              paa.Pk_IdProgramaAnualAdq,
                              IE = UEj.Nombre,
                              BienOServ = bs.Nombre,
                              UnidadDeMedida = um.Nombre,
                              paa.Cant_Anual,
                              paa.imp_Anual,
                              paa.RecFed,
                              paa.RecEst,
                              paa.RecProp,
                              paa.Fk_IdBienOServ__SIS,
                              paa.Fk_IdUnidadDeMedida__SIS,
                              bs.porcentajeMaximo,
                              bs.precioMaximo,
                              paa.Cant_Ene,
                              paa.Cant_Feb,
                              paa.Cant_Mar,
                              paa.Cant_Abr,
                              paa.Cant_May,
                              paa.Cant_Jun,
                              paa.Cant_Jul,
                              paa.Cant_Ago,
                              paa.Cant_Sep,
                              paa.Cant_Oct,
                              paa.Cant_Nov,
                              paa.Cant_Dic,
                              paa.Justificacion,
                              paa.imp_Ene,
                              paa.imp_Feb,
                              paa.imp_Mar,
                              paa.imp_Abr,
                              paa.imp_May,
                              paa.imp_Jun,
                              paa.imp_Jul,
                              paa.imp_Ago,
                              paa.imp_Sep,
                              paa.imp_Oct,
                              paa.imp_Nov,
                              paa.imp_Dic,
                              PP.Fk_IdEstado,
                              UEj.Pk_IdUnidadEjecutora
                          } into paa
                          select new
                          {
                              Pk_IdProgramaAnualAdq = paa.Key.Pk_IdProgramaAnualAdq,
                              IE = paa.Key.IE,
                              BienOServ = paa.Key.BienOServ,
                              UnidadDeMedida = paa.Key.UnidadDeMedida,
                              Cant_Anual = paa.Key.Cant_Anual,
                              imp_Anual = paa.Key.imp_Anual,
                              RecFed = paa.Key.RecFed,
                              RecEst = paa.Key.RecEst,
                              RecProp = paa.Key.RecProp,
                              Fk_IdBienOServ__SIS = paa.Key.Fk_IdBienOServ__SIS,
                              Fk_IdUnidadDeMedida__SIS = paa.Key.Fk_IdUnidadDeMedida__SIS,
                              porcentajeMaximo = paa.Key.porcentajeMaximo,
                              precioMaximo = paa.Key.precioMaximo,
                              Cant_Ene = paa.Key.Cant_Ene,
                              Cant_Feb = paa.Key.Cant_Feb,
                              Cant_Mar = paa.Key.Cant_Mar,
                              Cant_Abr = paa.Key.Cant_Abr,
                              Cant_May = paa.Key.Cant_May,
                              Cant_Jun = paa.Key.Cant_Jun,
                              Cant_Jul = paa.Key.Cant_Jul,
                              Cant_Ago = paa.Key.Cant_Ago,
                              Cant_Sep = paa.Key.Cant_Sep,
                              Cant_Oct = paa.Key.Cant_Oct,
                              Cant_Nov = paa.Key.Cant_Nov,
                              Cant_Dic = paa.Key.Cant_Dic,
                              Justificacion = paa.Key.Justificacion,
                              imp_Ene = paa.Key.imp_Ene,
                              imp_Feb = paa.Key.imp_Feb,
                              imp_Mar = paa.Key.imp_Mar,
                              imp_Abr = paa.Key.imp_Abr,
                              imp_May = paa.Key.imp_May,
                              imp_Jun = paa.Key.imp_Jun,
                              imp_Jul = paa.Key.imp_Jul,
                              imp_Ago = paa.Key.imp_Ago,
                              imp_Sep = paa.Key.imp_Sep,
                              imp_Oct = paa.Key.imp_Oct,
                              imp_Nov = paa.Key.imp_Nov,
                              imp_Dic = paa.Key.imp_Dic,
                              Fk_IdEstado = paa.Key.Fk_IdEstado,
                              Pk_IdUnidadEjecutora = paa.Key.Pk_IdUnidadEjecutora
                          }).OrderBy(c => c.Fk_IdEstado)
                            .OrderBy(ca => ca.Pk_IdUnidadEjecutora);
                return _m.ToList();
            }

            if (userroll == RolesUsuarios.IE_ACUICOLA_PESQUERA
               || userroll == RolesUsuarios.IE_ANIMAL
               || userroll == RolesUsuarios.IE_INOCUIDAD
               || userroll == RolesUsuarios.IE_MOVILIZACION
               || userroll == RolesUsuarios.IE_VEGETAL
               || userroll == RolesUsuarios.IE_PROGRAMAS_U
               || userroll == RolesUsuarios.PUESTO_COOR_ADMINISTRATIVO
               || userroll == RolesUsuarios.COORDINADOR_CAMPANIA
               || userroll == RolesUsuarios.PUESTO_COOR_PROYECTO
               || userroll == RolesUsuarios.PUESTO_GERENTE
               || userroll == RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA)
            {
                return db.ProgramaAnualAdqs.Where(item => item.Fk_IdAnio__SIS == _Fk_IdAnio
                            && item.Fk_IdUnidadEjecutora__UE == _Fk_IdUnidadEjecutora
                            && item.Fk_IdActividadXAccion == null
                            && item.Fk_IdAccionXCampania == null
                            && item.Fk_IdCampania__UE == null
                            && item.esGastoU == esGastoU).ToList();
            }

            if (userroll == RolesUsuarios.SUP_ESTATAL
                || userroll == RolesUsuarios.REPRESENTANTE_ESTATAL)
            {
                return db.ProgramaAnualAdqs.Where(item => item.Fk_IdAnio__SIS == _Fk_IdAnio
                                                            && item.UnidadEjecutora.Fk_IdEstado__SIS == _Fk_IdEstado
                                                            && item.Fk_IdActividadXAccion == null
                                                            && item.Fk_IdAccionXCampania == null
                                                            && item.Fk_IdCampania__UE == null
                                                            && item.esGastoU == esGastoU)
                                            .Select(item => new {
                                                                item.Pk_IdProgramaAnualAdq,
                                                                IE = item.UnidadEjecutora.Nombre,
                                                                BienOServ = item.BienOServ.Nombre,
                                                                UnidadDeMedida = item.UnidadDeMedida.Nombre,
                                                                item.Cant_Anual,
                                                                item.imp_Anual,
                                                                item.RecFed,
                                                                item.RecEst,
                                                                item.RecProp,
                                                                item.Fk_IdBienOServ__SIS,
                                                                item.Fk_IdUnidadDeMedida__SIS,
                                                                item.BienOServ.porcentajeMaximo,
                                                                item.BienOServ.precioMaximo,
                                                                item.Cant_Ene,
                                                                item.Cant_Feb,
                                                                item.Cant_Mar,
                                                                item.Cant_Abr,
                                                                item.Cant_May,
                                                                item.Cant_Jun,
                                                                item.Cant_Jul,
                                                                item.Cant_Ago,
                                                                item.Cant_Sep,
                                                                item.Cant_Oct,
                                                                item.Cant_Nov,
                                                                item.Cant_Dic,
                                                                item.Justificacion,
                                                                item.imp_Ene,
                                                                item.imp_Feb,
                                                                item.imp_Mar,
                                                                item.imp_Abr,
                                                                item.imp_May,
                                                                item.imp_Jun,
                                                                item.imp_Jul,
                                                                item.imp_Ago,
                                                                item.imp_Sep,
                                                                item.imp_Oct,
                                                                item.imp_Nov,
                                                                item.imp_Dic
                                            }).ToList();
            }

            if (userroll == RolesUsuarios.SUP_REGIONAL)
            {
                int IdRegion = FuncionesAuxiliares.GetRegionByUserName();

                return db.ProgramaAnualAdqs.Where(item => item.Fk_IdAnio__SIS == _Fk_IdAnio
                                                                && item.UnidadEjecutora.Estado.Fk_IdRegion == IdRegion
                                                                && item.Fk_IdActividadXAccion == null
                                                                && item.Fk_IdAccionXCampania == null
                                                                && item.Fk_IdCampania__UE == null
                                                                && item.esGastoU == esGastoU)
                                            .Select(item => new {
                                                item.Pk_IdProgramaAnualAdq,
                                                IE = item.UnidadEjecutora.Nombre,
                                                BienOServ = item.BienOServ.Nombre,
                                                UnidadDeMedida = item.UnidadDeMedida.Nombre,
                                                item.Cant_Anual,
                                                item.imp_Anual,
                                                item.RecFed,
                                                item.RecEst,
                                                item.RecProp,
                                                item.Fk_IdBienOServ__SIS,
                                                item.Fk_IdUnidadDeMedida__SIS,
                                                item.BienOServ.porcentajeMaximo,
                                                item.BienOServ.precioMaximo,
                                                item.Cant_Ene,
                                                item.Cant_Feb,
                                                item.Cant_Mar,
                                                item.Cant_Abr,
                                                item.Cant_May,
                                                item.Cant_Jun,
                                                item.Cant_Jul,
                                                item.Cant_Ago,
                                                item.Cant_Sep,
                                                item.Cant_Oct,
                                                item.Cant_Nov,
                                                item.Cant_Dic,
                                                item.Justificacion,
                                                item.imp_Ene,
                                                item.imp_Feb,
                                                item.imp_Mar,
                                                item.imp_Abr,
                                                item.imp_May,
                                                item.imp_Jun,
                                                item.imp_Jul,
                                                item.imp_Ago,
                                                item.imp_Sep,
                                                item.imp_Oct,
                                                item.imp_Nov,
                                                item.imp_Dic
                                            }).ToList();
            }

            return null;
        }

        /// <summary>
        /// Los usuarios superusuario, AUR, REG... podrán elegir algún comité, de acuerdo a su rol.
        /// 
        /// Una vez elegido esto, se le mostrará la información de sus gastos transversales al usuario.
        /// </summary>
        /// <param name="IdComite"></param>
        /// <returns></returns>
        public static IEnumerable GetProgramaAnualAdqByUnidadEjecutora(int IdComite)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            return db.ProgramaAnualAdqs.Where(item => item.Fk_IdAnio__SIS == IdAnioPres
                            && item.Fk_IdUnidadEjecutora__UE == IdComite
                            && item.Fk_IdActividadXAccion == null
                            && item.Fk_IdAccionXCampania == null
                            && item.Fk_IdCampania__UE == null).ToList();
        }

        public static IEnumerable GetProgramasGastosTransversales(int? IdProgramaAnualAdq)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.ProgramasGastosTransversales.Where(item => item.Fk_IdProgramaAnualAdq == IdProgramaAnualAdq)
                                                    .ToList();
        }

        //PROGRAMA ANUAL DE AQUISICIONES/POR CAMPAÑA ***************************************************************************
        /// <summary>
        /// Devuelve los Programas Anuales de la tabla UE.ProgramaAnualAdqs, lo filtra por la Campania
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetProgramaAnualByCampania(int? IdCampania, int IdAnio, string StatusActual)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var StatusSinRepro = FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania);
            if (StatusActual == StatusSinRepro)
            {
                return db1.ProgramaAnualAdqs.Where(item => item.Fk_IdCampania__UE == IdCampania
                        && item.Fk_IdAccionXCampania == null
                        && item.Fk_IdActividadXAccion == null
                        && item.Fk_IdAnio__SIS == IdAnio)
                    .Select(item => new
                    {
                        item.Pk_IdProgramaAnualAdq,
                        BienOServ = item.BienOServ.Nombre,
                        UnidadDeMedida = item.UnidadDeMedida.Nombre,
                        item.Cant_Anual,
                        item.imp_Anual,
                        item.RecFed,
                        item.RecEst,
                        item.RecProp,
                        item.Fk_IdBienOServ__SIS,
                        item.Fk_IdUnidadDeMedida__SIS,
                        item.Justificacion,
                        item.Cant_Ene,
                        item.Cant_Feb,
                        item.Cant_Mar,
                        item.Cant_Abr,
                        item.Cant_May,
                        item.Cant_Jun,
                        item.Cant_Jul,
                        item.Cant_Ago,
                        item.Cant_Sep,
                        item.Cant_Oct,
                        item.Cant_Nov,
                        item.Cant_Dic,
                        item.imp_Ene,
                        item.imp_Feb,
                        item.imp_Mar,
                        item.imp_Abr,
                        item.imp_May,
                        item.imp_Jun,
                        item.imp_Jul,
                        item.imp_Ago,
                        item.imp_Sep,
                        item.imp_Oct,
                        item.imp_Nov,
                        item.imp_Dic,
                    }).ToList();
            }
            var StatusEnRepro = FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania);
            if (StatusActual == StatusEnRepro)
            {
                return db1.ProgramaAnualAdq_Repro.Where(item => item.Fk_IdCampania__UE == IdCampania
                        && item.Fk_IdAccionXCampania == null
                        && item.Fk_IdActividadXAccion == null
                        && item.Fk_IdAnio__SIS == IdAnio)
                    .Select(item => new
                    {
                        item.Pk_IdProgramaAnualAdq,
                        BienOServ = item.BienOServ.Nombre,
                        UnidadDeMedida = item.UnidadDeMedida.Nombre,
                        item.RecFed,
                        item.RecEst,
                        item.RecProp,
                        item.Fk_IdBienOServ__SIS,
                        item.Fk_IdUnidadDeMedida__SIS,
                        item.Justificacion,
                        item.Cant_Ene,
                        item.Cant_Feb,
                        item.Cant_Mar,
                        item.Cant_Abr,
                        item.Cant_May,
                        item.Cant_Jun,
                        item.Cant_Jul,
                        item.Cant_Ago,
                        item.Cant_Sep,
                        item.Cant_Oct,
                        item.Cant_Nov,
                        item.Cant_Dic,
                        item.imp_Ene,
                        item.imp_Feb,
                        item.imp_Mar,
                        item.imp_Abr,
                        item.imp_May,
                        item.imp_Jun,
                        item.imp_Jul,
                        item.imp_Ago,
                        item.imp_Sep,
                        item.imp_Oct,
                        item.imp_Nov,
                        item.imp_Dic,
                    }).ToList();
            }
            return null;
        }

        //PROGRAMA ANUAL DE AQUISICIONES POR ACCION ***************************************************************************
        /// <summary>
        /// Devuelve los Programas Anual de Adquisición de la tabla UE.ProgramaAnualAdqs, lo filtra SOLO por _Fk_IdAccionXCampania; 
        /// Esta tabla esta dividida en tres niveles:
        /// Necesidades relacionads con la Instancia Ejecutora Almacenan Fk_IdAccionXCampania = null, Fk_IdActividadXAccion == null
        /// Necesidades relacionadas con la campaña Almacenan Fk_IdActividadXAccion = null
        /// Necesidades relacionadas con la Actividad Almacenan el Id de ActividadXAccion
        /// </summary>
        /// <param name="_Fk_IdAccionXCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetProgramaAnualAdqByAccionXCampania(int? _Fk_IdAccionXCampania, int IdAnio, int? IdCampania, string StatusActual)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            if (_Fk_IdAccionXCampania == null) return null;
            else
            {
                var StatusSinRepro = FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania);
                if (StatusActual == StatusSinRepro)
                {
                    return db.ProgramaAnualAdqs.Where(item => item.Fk_IdAccionXCampania == _Fk_IdAccionXCampania
                        && item.Fk_IdActividadXAccion == null
                        && item.Fk_IdAnio__SIS == IdAnio)
                        .Select(item => new
                        {
                            item.Pk_IdProgramaAnualAdq,
                            BienOServ = item.BienOServ.Nombre,
                            UnidadDeMedida = item.UnidadDeMedida.Nombre,
                            item.RecFed,
                            item.RecEst,
                            item.RecProp,
                            item.Fk_IdBienOServ__SIS,
                            item.Fk_IdUnidadDeMedida__SIS,
                            item.Justificacion,
                            item.Cant_Ene,
                            item.Cant_Feb,
                            item.Cant_Mar,
                            item.Cant_Abr,
                            item.Cant_May,
                            item.Cant_Jun,
                            item.Cant_Jul,
                            item.Cant_Ago,
                            item.Cant_Sep,
                            item.Cant_Oct,
                            item.Cant_Nov,
                            item.Cant_Dic,
                            item.imp_Ene,
                            item.imp_Feb,
                            item.imp_Mar,
                            item.imp_Abr,
                            item.imp_May,
                            item.imp_Jun,
                            item.imp_Jul,
                            item.imp_Ago,
                            item.imp_Sep,
                            item.imp_Oct,
                            item.imp_Nov,
                            item.imp_Dic,

                        }).ToList();
                }
                var StatusEnRepro = FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania);
                if (StatusActual == StatusEnRepro)
                {
                    return db.ProgramaAnualAdq_Repro.Where(item => item.Fk_IdAccionXCampania == _Fk_IdAccionXCampania
                        && item.Fk_IdActividadXAccion == null
                        && item.Fk_IdAnio__SIS == IdAnio)
                        .Select(item => new
                        {
                            item.Pk_IdProgramaAnualAdq,
                            BienOServ = item.BienOServ.Nombre,
                            UnidadDeMedida = item.UnidadDeMedida.Nombre,
                            item.RecFed,
                            item.RecEst,
                            item.RecProp,
                            item.Fk_IdBienOServ__SIS,
                            item.Fk_IdUnidadDeMedida__SIS,
                            item.Justificacion,
                            item.Cant_Ene,
                            item.Cant_Feb,
                            item.Cant_Mar,
                            item.Cant_Abr,
                            item.Cant_May,
                            item.Cant_Jun,
                            item.Cant_Jul,
                            item.Cant_Ago,
                            item.Cant_Sep,
                            item.Cant_Oct,
                            item.Cant_Nov,
                            item.Cant_Dic,
                            item.imp_Ene,
                            item.imp_Feb,
                            item.imp_Mar,
                            item.imp_Abr,
                            item.imp_May,
                            item.imp_Jun,
                            item.imp_Jul,
                            item.imp_Ago,
                            item.imp_Sep,
                            item.imp_Oct,
                            item.imp_Nov,
                            item.imp_Dic,

                        }).ToList();
                }
            }
            return null;
        }

        //PROGRAMA ANUAL DE AQUISICIONES POR Actividad ************************************************************************
        /// <summary>
        /// Devuelve los Programas Anual de Adquisición de la tabla UE.ProgramaAnualAdqs, lo filtra SOLO por Fk_IdActividadXAccion; 
        /// Esta tabla esta dividida en tres niveles:
        /// Necesidades relacionads con la Instancia Ejecutora Almacenan Fk_IdAccionXCampania = null, Fk_IdActividadXAccion == null
        /// Necesidades relacionadas con la campaña Almacenan Fk_IdActividadXAccion = null
        /// Necesidades relacionadas con la Actividad Almacenan el Id de ActividadXAccion
        /// </summary>
        /// <param name="Fk_IdActividadXAccion"></param>
        /// <returns></returns>
        public static IEnumerable GetProgramaAnualAdqByActividadXAccion(int? Fk_IdActividadXAccion)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            if (Fk_IdActividadXAccion == null)
            {
                var _m = from programaanual in db1.ProgramaAnualAdqs select programaanual;
                return _m.ToList();
            }
            else
            {
                var _m = from programaanual in db1.ProgramaAnualAdqs
                         where programaanual.Fk_IdActividadXAccion == Fk_IdActividadXAccion
                         select programaanual;
                return _m.ToList();
            }
        }


        /// <summary>
        /// Concatena Anio,BienOServ,NombreCampania de la tabla UE.ProgramaAnualAdqs
        /// </summary>
        /// <returns> </returns>
        public static IEnumerable GetProgramaAnualAdq()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m =
                    from repadquisicion in db1.ProgramaAnualAdqs
                    select new
                    {
                        Pk_IdProgramaAnualAdq = repadquisicion.Pk_IdProgramaAnualAdq,
                        Descripcion = repadquisicion.Anio.Anio1 + " | " + repadquisicion.BienOServ.Nombre + " | " + repadquisicion.Campania.NombreCampania
                    };

            return _m.ToList();
        }
        #endregion

        #region Productores

        /// <summary>
        /// Devuelve los Productores de la tabla UE.Productor
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetProductores()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from productor in db1.Productors select productor;

            return _m.ToList();
        }
        #endregion

        #region Personas
        /// <summary>
        /// Devuelve las Personas de la tabla SIS.Persona
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetPersonas()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            return db1.Personas.Where(item => item.esActivo == true).ToList();
        }

        /// <summary>
        /// Devuelve las Personas de la tabla SIS.Persona, filtrando los registros por Unidad Ejecutora
        /// </summary>
        /// <param name="IdUnidadEjecutora"></param>
        /// <returns></returns>
        public static IEnumerable GetPersonasByUnidadEjecutora(int? IdUnidadEjecutora)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            return db1.Personas.Where(item => item.Fk_IdUnidadEjecutora__UE == IdUnidadEjecutora).ToList();
        }

        #endregion

        #region RepActividad
        /// <summary>
        /// Devuelve los Reportes de Actividad de la tabla UE.[RepActividad] Filtradas por la Unidad Ejecutora
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetRepActividadesByUnidadEjecutora(int? UEId, int _Fk_IdAnio)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from repactividad in db1.RepActividads
                     where repactividad.Actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.Fk_IdUnidadEjecutora == UEId
                     && repactividad.Actividad.ActividadXAccion.AccionXCampania.Campania.ProyectoPresupuesto.Fk_IdAnio == _Fk_IdAnio
                     select repactividad;

            return _m.ToList();
        }

        public static IEnumerable GetRepActividadesByActividadAbierta(int IdActividad, int IdCierreMensual)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdMes = db.CierreMensuals.Where(item => item.Pk_IdCierreMensual == IdCierreMensual)
                                        .Select(item => item.Fk_IdMes)
                                        .First();

            return db.RepActividads.Where(item => item.Fk_IdActividad == IdActividad
                                            && item.FechaFin.Month == IdMes)
                                    .ToList();
        }

        /// <summary>
        /// Devuelve los Reportes de Actividad de la tabla UE.[RepActividad] Filtradas por la Actividad Asignada
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetRepActividadesByActividad(int idActividad)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from repactividad in db1.RepActividads
                     where repactividad.Fk_IdActividad == idActividad
                     select repactividad;

            return _m.ToList();
        }

        #endregion

        #region RepAdquisicion
        /// <summary>
        /// Devuelve los Reportes de Actividad de la tabla UE.[RepActividad] Filtradas por la Unidad Ejecutora
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetRepAdquisicionesByUnidadEjecutora(int? UEId, int _Fk_IdAnio)
        {
            //DEPRECADO
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from repaadquisicion in db1.RepAdquisicions
                     where repaadquisicion.ProgramaAnualAdq.UnidadEjecutora.Pk_IdUnidadEjecutora == UEId && repaadquisicion.ProgramaAnualAdq.Fk_IdAnio__SIS == _Fk_IdAnio
                     select repaadquisicion;

            return _m.ToList();
        }

        /// <summary>
        /// Devuelve los Reportes de Actividad de la tabla UE.[RepActividad] Filtradas por la Unidad Ejecutora
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetRepAdquisicionesByNecesidad(int? Necesidad)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from repaadquisicion in db1.RepAdquisicions
                     where repaadquisicion.Fk_IdProgramaAnualAdq == Necesidad
                     select repaadquisicion;

            return _m.ToList();
        }

        public static IEnumerable GetRepAdquisicionesByCierreMensual(int? IdNecesidad, int IdCierreMensual)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdMes = db.CierreMensuals.Where(item => item.Pk_IdCierreMensual == IdCierreMensual)
                                            .Select(item => item.Fk_IdMes)
                                            .First();

            return db.RepAdquisicions.Where(item => item.Fk_IdProgramaAnualAdq == IdNecesidad
                                                && item.FechaAdquisicion.Month == IdMes).ToList();
        }
        #endregion

        #region ReporteGasto
        /// <summary>
        ///  Devuelve el Reporte de Gasto de la tabla UE.RepGasto, filtrando los registros por la Actividad
        /// </summary>
        /// <param name="IdActividad"></param>
        /// <returns></returns>
        public static IEnumerable GetRepGastosByActividad(int? IdActividad)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            var _m = from repgastos in db1.RepGastoes where repgastos.Fk_IdRepActividad == IdActividad select repgastos;
            return _m.ToList();
        }
        #endregion

        #region SubComponente
        //Posiblemente ya no se usa
        public static IEnumerable GetSubComponente()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            return db.SubComponentes.Where(item => item.Incentivo.Componente.Fk_IdAnio == IdAnioPres).ToList();
        }

        /// <summary>
        /// Devuelve los Subcomponentes de la tabla SIS.SubComponente, lo filtra para cuando la campaña solo tengan un Subcomponente
        /// </summary>
        /// <param name="subcomponente"></param>
        /// <returns></returns>
        /// 

        //Posiblemente ya no se usa
        public static IEnumerable GetSubComponenteBySubComponente(int subcomponente)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from subcomponentes in db1.SubComponentes
                     where subcomponentes.Pk_IdSubComponente == subcomponente
                     select subcomponentes;
            return _m.ToList();
        }

        //Esta funcion se sobrecarga para alimentar el combo en las pantallas de Campaña  SA,SV,SAC,MOV
        //que pueden manejar mas de un subcomponente

        /// <summary>
        /// Devuelve los Subcomponentes de la tabla SIS.SubComponente, lo filtra para cuando la campaña tenga dos Subcomponente
        /// </summary>
        /// <param name="subcomponente"></param>
        /// <param name="subcomponente2"></param>
        /// <returns></returns>
        /// 
        //Posiblemente ya no se usa
        public static IEnumerable GetSubComponente2BySubComponente(int subcomponente, int subcomponente2)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from subcomponentes in db1.SubComponentes
                     where subcomponentes.Pk_IdSubComponente == subcomponente || subcomponentes.Pk_IdSubComponente == subcomponente2
                     select subcomponentes;
            return _m.ToList();
        }

        /// <summary>
        /// Obtiene los subcomponentes para la distribución del presupuesto en los diferentes proyectos.
        /// 
        /// Este método lo ocupará las unidades responsables
        /// </summary>
        /// <param name="IdUnidadResponsable">Id de la Unidad Responsable</param>
        /// <param name="IdAnioPresupuestal">El año presupuestal que se está ocupando en el sistema</param>
        /// <returns>Una lista de subcomponentes</returns>
        public static IEnumerable GetSubComponentePresByUR(int IdUnidadResponsable, int IdAnioPresupuestal)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var query = from URXSUB in db.UnidadResponsableXSubComponentePres
                        join SC in db.SubComponentes on URXSUB.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                        where URXSUB.Fk_IdUnidadResponsable == IdUnidadResponsable && URXSUB.FK_IdAnio == IdAnioPresupuestal
                        select SC;

            return query.ToList();
        }
        #endregion

        #region StatusActividad

        /// <summary>
        /// Devuelve el Estatus de la Actividad de la tabla SIS.StatusActividad
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetStatusActividad()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            return db.StatusActividads.ToList();
        }
        #endregion

        #region StatuAlcance
        /// <summary>
        /// Devuelve el Estatus Alcance de la tabla SIS.StatusAlcance
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetStatusAlcance()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from statusalcance in db1.StatusAlcances select statusalcance;

            return _m.ToList();
        }
        #endregion

        #region StatusCampania
        /// <summary>
        /// Devuelve los Status a los cuales puede cambiar la campaña dependiendo del rol y de su estado actual
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetStatusCampania(int IdCampania)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            string statusActual = GetStatusActualCampania(IdCampania);
            string RolNormalizado = FuncionesAuxiliares.NormalizaRoles();
            string rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();

            int? IdUnidadResponsable = db.Campanias.Where(item => item.Pk_IdCampania == IdCampania)
                                                    .Select(item => item.ProyectoPresupuesto.Fk_IdUnidadResponsable)
                                                    .First();
            int IdAnio = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            //Si la campaña la tiene que revisar la UCE, entonces la UCE va a actuar como AUR
            if (IdUnidadResponsable == 25 && RolNormalizado == RolesUsuarios.SUPERUSUARIO)
                RolNormalizado = RolesUsuarios.ROL_AUR;

            if (rolUsuario == RolesUsuarios.SUP_ESTATAL)
                RolNormalizado = RolesUsuarios.ROL_AIE;

            return db.FlujoCampanias.Where(item => item.StatusCampania1.Nombre == statusActual && item.Rol == RolNormalizado && item.Fk_IdAnio == IdAnio)
                                    .Select(item => new { item.StatusCampania.PK_IdStatusCampania, item.StatusCampania.Nombre })
                                    .ToList();
        }

        public static int GetStatusCampaniaN(int IdCampania)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            string statusActual = GetStatusActualCampania(IdCampania);
            string RolNormalizado = FuncionesAuxiliares.NormalizaRoles();
            string rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();

            int? IdUnidadResponsable = db.Campanias.Where(item => item.Pk_IdCampania == IdCampania)
                                                    .Select(item => item.ProyectoPresupuesto.Fk_IdUnidadResponsable)
                                                    .First();

            //Si la campaña la tiene que revisar la UCE, entonces la UCE va a actuar como AUR
            if (IdUnidadResponsable == 25 && RolNormalizado == RolesUsuarios.SUPERUSUARIO)
                RolNormalizado = RolesUsuarios.ROL_AUR;

            if (rolUsuario == RolesUsuarios.SUP_ESTATAL)
                RolNormalizado = RolesUsuarios.ROL_AIE;

            return db.FlujoCampanias.Where(item => item.StatusCampania1.Nombre == statusActual && item.Rol == RolNormalizado)
                                    .Select(item => new { item.StatusCampania.PK_IdStatusCampania, item.StatusCampania.Nombre })
                                    .Count();
        }

        /// <summary>
        /// Devuelve el status actual de la campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static string GetStatusActualCampania(int IdCampania)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var _query = db.StatusCampaniaKardexes.Where(item => item.FK_IdCampania__UE == IdCampania)
                            .OrderByDescending(item => item.Fecha)
                            .Select(item => item.StatusCampania.Nombre);

            return _query.Count() == 0 ? ConstantesGlobales.SIN_ESTADO : _query.First();
        }

        /// <summary>
        /// Devuelve el Estatus Capania de la tabla SIS.StatusCampania, lo filtra por Status Origen y el Rol del Usuario
        /// </summary>
        /// <param name="statusorigen"></param>
        /// <param name="rol"></param>
        /// <returns></returns>
        public static IEnumerable GetStatusSiguienteCampaniaByStatusCampania(int statusorigen, string rol)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var innerJoinQuery = from statuscampania in db1.StatusCampanias
                                 join flujocampania in db1.FlujoCampanias on statuscampania.PK_IdStatusCampania equals flujocampania.Fk_IdEstatusDestino
                                 where flujocampania.Fk_IdEstatusOrigen == statusorigen && flujocampania.Rol == rol
                                 select new { Pk_IdEstado = flujocampania.Fk_IdEstatusDestino, Nombre = statuscampania.Nombre }; //produces flat sequence

            return innerJoinQuery.ToList();
        }
        //********************Funciones para poblar Detalle de Acciones en las Acciones
        //StatusCampaniaKardexes

        /// <summary>
        /// Devuelve el Estatus Capania de la tabla SIS.StatusCampania, lo filtra por la Campania
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns>Filtrado del Tabs de Status por la Campaña</returns>
        public static IEnumerable GetStatusCampaniaKardexesByCampania(int? IdCampania)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.StatusCampaniaKardexes.Where(item => item.FK_IdCampania__UE == IdCampania).OrderByDescending(item => item.Fecha).ToList();
        }
        #endregion

        #region StatusUnidadEjecutora

        /// <summary>
        /// Devuelve consulta de registros de la tabla SIS.StatusUE
        /// </summary>
        /// <returns></returns>

        public static IEnumerable GetStatusUnidadEjecutora()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from statusUE in db1.StatusUEs select statusUE;

            return _m.ToList();
        }

        #endregion

        #region TipoDeRecurso

        /// <summary>
        /// Devuelve consulta de Tipo de Recursos de la tabla SIS.TipoDeRecurso
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetTiposDeRecurso()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from tipoderecurso in db1.TipoDeRecursoes select tipoderecurso;

            return _m.ToList();
        }

        #endregion

        #region TipoDeAccions

        /// <summary>
        /// Devuelve los Tipos de Acción de la tabla SIS.TipoDeAccion
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetTiposDeAccion()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from accion in db1.TipoDeAccions select accion;

            return _m.ToList();
        }

        /// <summary>
        /// Devuelve los Tipos de Acción de la tabla SIS.TipoDeAccion, filtrando los registros por el Proyecto Autorizado
        /// </summary>
        /// <param name="Proyecto"></param>
        /// <returns></returns>
        public static IEnumerable GetTiposDeAccionByProyecto(int Proyecto)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from accion in db1.TipoDeAccions where accion.FK_IdProyectoAutorizado == Proyecto select accion;

            return _m.ToList();
        }

        #endregion

        #region TipoDeActividades

        /// <summary>
        /// Devuelve los Tipos de Actividades de la tabla SIS.TipoActividad
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetTiposDeActividad()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from actividad in db1.TipoActividads select actividad;

            return _m.ToList();
        }

        /// <summary>
        /// Devuelve los Tipos de Actividades de la tabla SIS.TipoActividad, filtrando los registros por Tipo de Acción
        /// </summary>
        /// <param name="Pk_IdTipoDeAccion"></param>
        /// <returns></returns>
        public static IEnumerable GetTiposDeActividadByTipoDeAccion(int Pk_IdTipoDeAccion)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.TipoActividads.Where(item => item.Fk_IdTipoAccion == Pk_IdTipoDeAccion).ToList();
        }
        #endregion

        #region TiposDeBien

        /// <summary>
        /// Devuelve los Tipos de Bienes de la tabla SIS.TipoDeBien
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetTiposDeBien()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            int IdAnio = Convert.ToInt32(HttpContext.Current.Session["IdAnio"]);

            var _m = from tipodebien in db1.TipoDeBiens 
                     where tipodebien.Fk_IdAnio == IdAnio
                     select tipodebien;

            return _m.ToList();
        }

        #endregion

        #region TipoDeInstalacion
        /// <summary>
        /// Devuelve los Tipos de Instalación de la tabla SIS.TipoInstalacion
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetTipoDeInstalacion()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from tipodeinstalacion in db1.TipoInstalacions select tipodeinstalacion;

            return _m.ToList();
        }
        #endregion

        #region TiposdeUnidad
        /// <summary>
        /// Devuelve los Tipos de Unidad de la tabla SIS.TipoDeUnidad
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetTiposDeUnidad()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from tipodeunidad in db1.TipoDeUnidads select tipodeunidad;

            return _m.ToList();
        }
        #endregion

        #region UnidadDeMedidas

        /// <summary>
        /// Devuelve las Unidades de Medida de la tabla SIS.UnidadDeMedida
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetUnidadesDeMedida()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int IdAnio = Convert.ToInt32(HttpContext.Current.Session["IdAnio"]);

            return db.UnidadDeMedidas.Where(u => u.Fk_IdAnio == IdAnio).ToList();
        }

        /// <summary>
        /// Devuelve las Unidades de Medida de la tabla SIS.UnidadDeMedida, filtrando por Tipo de Actividad
        /// </summary>
        /// <param name="Pk_IdTipoDeActividad"></param>
        /// <returns></returns>
        public static IEnumerable GetUnidadesDeMedidaByTipodeActividad(int Pk_IdTipoDeActividad)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var _query = (from um in db.UnidadDeMedidas
                          join ta in db.TipoActividads on um.Pk_IdUnidadDeMedida equals ta.FK_IdUnidadDeMedida
                          where ta.Pk_IdTipoActividad == Pk_IdTipoDeActividad
                          select um
                        );

            return _query.ToList();
        }
        #endregion

        #region UnidadEjecutora
        /// <summary>
        /// Devuelve las Unidades Ejecutoras de la tabla UE.UnidadEjecutora
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetUnidadesEjecutoras()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from unidadejecutora in db1.UnidadEjecutoras
                     where unidadejecutora.CT_LIVE == true
                     select unidadejecutora;
            return _m.ToList();
        }
        #endregion

        #region UnidadesResponsables
        /// <summary>
        /// Devuelve las Unidades Responsables de la tabla UE.UnidadResponsable
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetUnidadesResponsables()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from unidadresponsable in db1.UnidadResponsables select unidadresponsable;

            return _m.ToList();
        }
        #endregion

        #region Vigencia
        /// <summary>
        /// Devuelve las Vigencias de la tabla UE.Vigencia, filtrando los registros por Unidad Ejecutora
        /// </summary>
        /// <param name="IdUnidadEjecutora"></param>
        /// <returns></returns>
        public static IEnumerable GetVigenciasByUnidadEjecutora(int? IdUnidadEjecutora)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from vigencia in db1.Vigencias where vigencia.Fk_IdUnidadEjecutora == IdUnidadEjecutora select vigencia;

            return _m.ToList();
        }
        #endregion

        #region Puesto
        /// <summary>
        /// Devuelve los puestos de la tabla SIS.Puesto
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetPuestos()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from puestos in db1.Puestoes select puestos;

            return _m.ToList();
        }
        #endregion

        #region Profesion
        /// <summary>
        /// Devuelve las profesiones de SIS.Profesion
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetProfesiones()
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            var _m = from profesiones in db1.Profesions select profesiones;

            return _m.ToList();
        }
        #endregion

        #region Proveedor
        public static IEnumerable GetProveedor()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            string userRol = FuncionesAuxiliares.GetCurrent_RolUsuario();
            IQueryable<Proveedor> _datos = null;

            switch(userRol)
            {
                case RolesUsuarios.SUP_REGIONAL:
                    int IdRegion = FuncionesAuxiliares.GetRegionByUserName();

                    _datos = db.Proveedors.Where(item => item.Estado.Fk_IdRegion == IdRegion);
                    break;
                case RolesUsuarios.SUP_ESTATAL:
                    int idestado = FuncionesAuxiliares.GetCurrent_IdEstado();
                    _datos = db.Proveedors.Where(item => item.Fk_IdEstado == idestado);
                    break;
                default:
                    _datos = db.Proveedors;
                    break;
            }

            return _datos.ToList();
        }

        public static IEnumerable GetProveedorByEstado(int Estado)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var _m = from proveedor in db.Proveedors
                     where proveedor.Fk_IdEstado == Estado
                     select proveedor;

            return _m.ToList();
        }
        #endregion

        #region Giros
        public static IEnumerable GetGirosByProveedor(int? IdProveedor)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var _m = from giros in db.GirosXProvs where giros.Fk_IdProveedor == IdProveedor select giros;

            return _m.ToList();
        }
        #endregion

        public static IEnumerable GetSubComponentexUsuario(string userroll, int _Fk_IdAnio)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            List<SubComponente> _m;

            userroll = FuncionesAuxiliares.GetCurrent_RolUsuario();

            switch (userroll)
            {
                case RolesUsuarios.SUPERUSUARIO:
                    _m = db.SubComponentes.ToList();
                    break;
                default:
                    _m = null;
                    break;
            }

            return _m;
        }

        public static IEnumerable GetProyectosAutorizados(string userroll, int _Fk_IdAnio, int? IdEstado)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            if (userroll == RolesUsuarios.SUPERUSUARIO)
            {
                int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

                return db.ProyectoAutorizadoes.Where(item => item.Fk_IdAnio == IdAnioPres).ToList();
            }

            if (userroll == RolesUsuarios.UR_ANIMAL
                || userroll == RolesUsuarios.IE_ANIMAL)
            {
                var _m = from URXSub in db.UnidadResponsableXSubComponentePres
                         join SC in db.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         where URXSub.FK_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 14 // Todos los Proyectos Autorizados de ese año de DGSA
                         orderby PA.SubComponente.Nombre, PA.Nombre
                         select PA;

                return _m.ToList();
            }

            if (userroll == RolesUsuarios.UR_ACUICOLA_PESQUERA
                || userroll == RolesUsuarios.IE_ACUICOLA_PESQUERA)
            {
                var _m = from URXSub in db.UnidadResponsableXSubComponentePres
                         join SC in db.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         where URXSub.FK_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 19 // Todos los Proyectos Autorizados de ese año de Sanidad Acuicola y Pesquera
                         orderby PA.SubComponente.Nombre, PA.Nombre
                         select PA;

                return _m.ToList();
            }

            if (userroll == RolesUsuarios.UR_VEGETAL
                || userroll == RolesUsuarios.IE_VEGETAL)
            {
                var _m = from URXSub in db.UnidadResponsableXSubComponentePres
                         join SC in db.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         where URXSub.FK_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 6 // Todos los Proyectos Autorizados de ese año de DGSV
                         orderby PA.SubComponente.Nombre, PA.Nombre
                         select PA;

                return _m.ToList();
            }

            if (userroll == RolesUsuarios.UR_INOCUIDAD
                || userroll == RolesUsuarios.IE_INOCUIDAD)
            {
                var _m = from URXSub in db.UnidadResponsableXSubComponentePres
                         join SC in db.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         where URXSub.FK_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 16 // Todos los Proyectos Autorizados de ese año de DGIAAP
                         orderby PA.SubComponente.Nombre, PA.Nombre
                         select PA;

                return _m.ToList();
            }

            if (userroll == RolesUsuarios.UR_MOVILIZACION
                || userroll == RolesUsuarios.IE_MOVILIZACION)
            {
                var _m = from URXSub in db.UnidadResponsableXSubComponentePres
                         join SC in db.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         where URXSub.FK_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 17 // Todos los Proyectos Autorizados de ese año de DGIF
                         orderby PA.SubComponente.Nombre, PA.Nombre
                         select PA;

                return _m.ToList();
            }
            if (userroll == RolesUsuarios.SUP_ESTATAL)
            {
                var _m = from URXSub in db.UnidadResponsableXSubComponentePres
                         join SC in db.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         join PP in db.ProyectoPresupuestoes on PA.Pk_IdProyectoAutorizado equals PP.Fk_IdProyectoAutorizado
                         where URXSub.FK_IdAnio == _Fk_IdAnio && PP.Fk_IdEstado == IdEstado
                         orderby PA.SubComponente.Nombre, PA.Nombre
                         select PA;

                return _m.ToList();
            }

            if (userroll == RolesUsuarios.SUP_REGIONAL)
            {
                int reg = FuncionesAuxiliares.GetRegionByUserName();

                var _m = from URXSub in db.UnidadResponsableXSubComponentePres
                         join SC in db.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         join PP in db.ProyectoPresupuestoes on PA.Pk_IdProyectoAutorizado equals PP.Fk_IdProyectoAutorizado
                         where URXSub.FK_IdAnio == _Fk_IdAnio && PP.Estado.Fk_IdRegion == reg
                         orderby PA.SubComponente.Nombre, PA.Nombre
                         select PA;

                return _m.ToList();
            }

            return null;
        }

        public static IEnumerable GetProyectoAutorizadoXUsuarios_Pres(string userroll, int _Fk_IdAnio)
        {
            //Esta funcion es gemela de GetProyectoAutorizadoXUsuarios pero regresa los proyectos autorizados para presupuesto
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            List<ProyectoAutorizado> _m;
            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            switch (userroll)
            {
                case RolesUsuarios.SUPERUSUARIO:
                    _m = db1.ProyectoAutorizadoes.Where(item => item.Fk_IdAnio == IdAnioPres)
                                                .ToList();
                    break;
                default:
                    _m = null;
                    break;
            }
            return _m;
        }

        /// <summary>
        /// Se mantiene por el filtrado
        /// </summary>
        /// <param name="userroll"></param>
        /// <param name="_Fk_IdAnio"></param>
        /// <returns></returns>
        public static IEnumerable GetTiposDeAccionAux(string userroll, int _Fk_IdAnio)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            IQueryable<TipoDeAccion> _m;

            switch (userroll)
            {
                case RolesUsuarios.SUPERUSUARIO:

                    int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

                    _m = db1.TipoDeAccions.Where(item => item.ProyectoAutorizado.Fk_IdAnio == IdAnioPres)
                                            .OrderBy(item => item.ProyectoAutorizado.SubComponente.Nombre)
                                            .ThenBy(item => item.ProyectoAutorizado.Nombre)
                                            .ThenBy(item => item.Nombre);

                    break;
                case RolesUsuarios.UR_ANIMAL:
                    _m = from URXSub in db1.UnidadResponsableXSubComponentes
                         join SC in db1.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db1.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         join TA in db1.TipoDeAccions on PA.Pk_IdProyectoAutorizado equals TA.FK_IdProyectoAutorizado
                         where URXSub.FK_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 14 // Todos los Tipos de acción de ese año de DGSA 
                         orderby TA.ProyectoAutorizado.SubComponente.Nombre,
                         TA.ProyectoAutorizado.Nombre,
                         TA.Nombre
                         select TA;
                    break;
                case RolesUsuarios.UR_ACUICOLA_PESQUERA:
                    _m = from URXSub in db1.UnidadResponsableXSubComponentes
                         join SC in db1.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db1.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         join TA in db1.TipoDeAccions on PA.Pk_IdProyectoAutorizado equals TA.FK_IdProyectoAutorizado
                         where URXSub.FK_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 19 // Todos los Tipos de acción de ese año de Sanidad Acuicola y Pesquera
                         orderby TA.ProyectoAutorizado.SubComponente.Nombre,
                         TA.ProyectoAutorizado.Nombre,
                         TA.Nombre
                         select TA;
                    break;
                case RolesUsuarios.UR_VEGETAL:
                    _m = from URXSub in db1.UnidadResponsableXSubComponentes
                         join SC in db1.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db1.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         join TA in db1.TipoDeAccions on PA.Pk_IdProyectoAutorizado equals TA.FK_IdProyectoAutorizado
                         where URXSub.FK_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 6 // Todos los Tipos de acción de ese año de DGSV 
                         orderby TA.ProyectoAutorizado.SubComponente.Nombre,
                         TA.ProyectoAutorizado.Nombre,
                         TA.Nombre
                         select TA;
                    break;
                case RolesUsuarios.UR_INOCUIDAD:
                    _m = from URXSub in db1.UnidadResponsableXSubComponentes
                         join SC in db1.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db1.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         join TA in db1.TipoDeAccions on PA.Pk_IdProyectoAutorizado equals TA.FK_IdProyectoAutorizado
                         where URXSub.FK_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 16 // Todos los Tipos de acción de ese año de DGIAAP 
                         orderby TA.ProyectoAutorizado.SubComponente.Nombre,
                         TA.ProyectoAutorizado.Nombre,
                         TA.Nombre
                         select TA;
                    break;
                case RolesUsuarios.UR_MOVILIZACION:
                    _m = from URXSub in db1.UnidadResponsableXSubComponentes
                         join SC in db1.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db1.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         join TA in db1.TipoDeAccions on PA.Pk_IdProyectoAutorizado equals TA.FK_IdProyectoAutorizado
                         where URXSub.FK_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 17 // Todos los Tipos de acción de ese año de DGIF 
                         orderby TA.ProyectoAutorizado.SubComponente.Nombre,
                         TA.ProyectoAutorizado.Nombre,
                         TA.Nombre
                         select TA;
                    break;
                default:
                    _m = from tipodeaccion in db1.TipoDeAccions
                         orderby tipodeaccion.ProyectoAutorizado.SubComponente.Nombre,
                         tipodeaccion.ProyectoAutorizado.Nombre,
                         tipodeaccion.Nombre
                         select tipodeaccion;
                    break;
            }

            return _m.ToList();
        }

        /// <summary>
        /// Se mantiene por los filtrados
        /// </summary>
        /// <param name="userroll"></param>
        /// <param name="_Fk_IdAnio"></param>
        /// <returns></returns>
        public static IEnumerable GetTiposDeAccionXSubComp(string userroll, int _Fk_IdAnio)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            IQueryable<TipoDeAccion> _m;

            switch (userroll)
            {
                case RolesUsuarios.SUPERUSUARIO:

                    int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

                    _m = db1.TipoDeAccions.Where(item => item.ProyectoAutorizado.Fk_IdAnio == IdAnioPres);

                    break;
                case RolesUsuarios.UR_ANIMAL:
                    _m = from URXSub in db1.UnidadResponsableXSubComponentes
                         join SC in db1.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db1.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         join TA in db1.TipoDeAccions on PA.Pk_IdProyectoAutorizado equals TA.FK_IdProyectoAutorizado
                         where URXSub.FK_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 14 // Todos los Tipos de acción de ese año de DGSA 
                         select TA;
                    break;
                case RolesUsuarios.UR_ACUICOLA_PESQUERA:
                    _m = from URXSub in db1.UnidadResponsableXSubComponentes
                         join SC in db1.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db1.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         join TA in db1.TipoDeAccions on PA.Pk_IdProyectoAutorizado equals TA.FK_IdProyectoAutorizado
                         where URXSub.FK_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 19 // Todos los Tipos de acción de ese año de Sanidad Acuicola y Pesquera
                         select TA;
                    break;
                case RolesUsuarios.UR_VEGETAL:
                    _m = from URXSub in db1.UnidadResponsableXSubComponentes
                         join SC in db1.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db1.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         join TA in db1.TipoDeAccions on PA.Pk_IdProyectoAutorizado equals TA.FK_IdProyectoAutorizado
                         where URXSub.FK_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 6 // Todos los Tipos de acción de ese año de DGSV 
                         select TA;
                    break;
                case RolesUsuarios.UR_INOCUIDAD:
                    _m = from URXSub in db1.UnidadResponsableXSubComponentes
                         join SC in db1.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db1.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         join TA in db1.TipoDeAccions on PA.Pk_IdProyectoAutorizado equals TA.FK_IdProyectoAutorizado
                         where URXSub.FK_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 16 // Todos los Tipos de acción de ese año de DGIAAP 
                         select TA;
                    break;
                case RolesUsuarios.UR_MOVILIZACION:
                    _m = from URXSub in db1.UnidadResponsableXSubComponentes
                         join SC in db1.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db1.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         join TA in db1.TipoDeAccions on PA.Pk_IdProyectoAutorizado equals TA.FK_IdProyectoAutorizado
                         where URXSub.FK_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 17 // Todos los Tipos de acción de ese año de DGIF 
                         select TA;
                    break;
                case RolesUsuarios.UR_UPV:
                    _m = from URXSub in db1.UnidadResponsableXSubComponentes
                         join SC in db1.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db1.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         join TA in db1.TipoDeAccions on PA.Pk_IdProyectoAutorizado equals TA.FK_IdProyectoAutorizado
                         where URXSub.FK_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 26 // Todos los Tipos de acción de ese año de DGUPV 
                         select TA;
                    break;
                default:
                    _m = from tipodeaccion in db1.TipoDeAccions
                         select tipodeaccion;
                    break;
            }

            return _m.ToList();
        }

        /// <summary>
        /// Se mantiene por los filtrados
        /// </summary>
        /// <param name="userroll"></param>
        /// <param name="_Fk_IdAnio"></param>
        /// <returns></returns>
        public static IEnumerable GetTiposDeActividadesXSubComp(string userroll, int _Fk_IdAnio)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            IQueryable<TipoActividad> _m;

            switch (userroll)
            {
                case RolesUsuarios.SUPERUSUARIO:

                    int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

                    _m = db1.TipoActividads.Where(item => item.TipoDeAccion.ProyectoAutorizado.Fk_IdAnio == IdAnioPres);

                    break;
                case RolesUsuarios.UR_ANIMAL:
                    _m = from URXSub in db1.UnidadResponsableXSubComponentes
                         join SC in db1.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db1.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         join TA in db1.TipoDeAccions on PA.Pk_IdProyectoAutorizado equals TA.FK_IdProyectoAutorizado
                         join AC in db1.TipoActividads on TA.Pk_IDTipoDeAccion equals AC.Fk_IdTipoAccion
                         where TA.ProyectoAutorizado.Fk_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 14 // Todos los Tipos de Actividad de ese año de DGSA 
                         select AC;
                    break;
                case RolesUsuarios.UR_ACUICOLA_PESQUERA:
                    _m = from URXSub in db1.UnidadResponsableXSubComponentes
                         join SC in db1.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db1.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         join TA in db1.TipoDeAccions on PA.Pk_IdProyectoAutorizado equals TA.FK_IdProyectoAutorizado
                         join AC in db1.TipoActividads on TA.Pk_IDTipoDeAccion equals AC.Fk_IdTipoAccion
                         where TA.ProyectoAutorizado.Fk_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 19 // Todos los Tipos de Actividad de ese año de Sanidad Acuicola y Pesquera
                         select AC;
                    break;
                case RolesUsuarios.UR_VEGETAL:
                    _m = from URXSub in db1.UnidadResponsableXSubComponentes
                         join SC in db1.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db1.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         join TA in db1.TipoDeAccions on PA.Pk_IdProyectoAutorizado equals TA.FK_IdProyectoAutorizado
                         join AC in db1.TipoActividads on TA.Pk_IDTipoDeAccion equals AC.Fk_IdTipoAccion
                         where TA.ProyectoAutorizado.Fk_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 6 // Todos los Tipos de Actividad de ese año de DGSV 
                         select AC;
                    break;
                case RolesUsuarios.UR_INOCUIDAD:
                    _m = from URXSub in db1.UnidadResponsableXSubComponentes
                         join SC in db1.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db1.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         join TA in db1.TipoDeAccions on PA.Pk_IdProyectoAutorizado equals TA.FK_IdProyectoAutorizado
                         join AC in db1.TipoActividads on TA.Pk_IDTipoDeAccion equals AC.Fk_IdTipoAccion
                         where TA.ProyectoAutorizado.Fk_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 16 // Todos los Tipos de Actividad de ese año de DGIAAP 
                         select AC;
                    break;
                case RolesUsuarios.UR_MOVILIZACION:
                    _m = from URXSub in db1.UnidadResponsableXSubComponentes
                         join SC in db1.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db1.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         join TA in db1.TipoDeAccions on PA.Pk_IdProyectoAutorizado equals TA.FK_IdProyectoAutorizado
                         join AC in db1.TipoActividads on TA.Pk_IDTipoDeAccion equals AC.Fk_IdTipoAccion
                         where TA.ProyectoAutorizado.Fk_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 17 // Todos los Tipos de Actividad de ese año de DGIF 
                         select AC;
                    break;
                case RolesUsuarios.UR_UPV:
                    _m = from URXSub in db1.UnidadResponsableXSubComponentes
                         join SC in db1.SubComponentes on URXSub.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                         join PA in db1.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                         join TA in db1.TipoDeAccions on PA.Pk_IdProyectoAutorizado equals TA.FK_IdProyectoAutorizado
                         join AC in db1.TipoActividads on TA.Pk_IDTipoDeAccion equals AC.Fk_IdTipoAccion
                         where TA.ProyectoAutorizado.Fk_IdAnio == _Fk_IdAnio && URXSub.Fk_IdUnidadResponsable == 26 // Todos los Tipos de Actividad de ese año de DGUPV 
                         select AC;
                    break;
                default:
                    _m = from actividad in db1.TipoActividads where actividad.Pk_IdTipoActividad == 0 // NADA
                         select actividad;
                    break;
            }
            return _m.ToList();
        }

        /// <summary>
        /// Regresa una lista de instancias ejecutoras a las que tiene derecho conforme al usuario logeado 
        ///     SUPERUSUARIO => Todas las Unidades ejecutoras Sin Filtro
        ///     AUR Todas las unidades que dependan de sus subcomponentes
        ///     SUP_ESTATAL Todas las unidades del estado
        ///     SUP_REGIONAL Todas las unidades de la Region (Coleccion de Estados)
        ///     
        ///     Usuarios Internos, Solo ven a su propia Instancia Ejecutora
        ///     
        ///     AIE
        ///     PUESTO_COOR_PROYECTO
        ///     COORDINADOR_CAMPANIA
        ///     PUESTO_COOR_REGIONAL
        ///     PUESTO_GERENTE
        ///     PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
        ///     PUESTO_PROF_RESP_INFORMATICA
        /// </summary>
        /// <param name="rolusuario"></param>
        /// <param name="FK_IdUnidadResponsable"></param>
        /// <param name="_Fk_IdEstado"></param>
        /// <param name="Fk_IdUnidadEjecutora"></param>
        /// <returns></returns>
        public static IEnumerable GetUnidadEjecutoraByUsuario(string rolusuario, int? FK_IdUnidadResponsable, int? _Fk_IdEstado, int? Fk_IdUnidadEjecutora)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();
            int Fk_IdAnio = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            switch (rolusuario)
            {
                case RolesUsuarios.SUPERUSUARIO:
                case RolesUsuarios.SUP_NACIONAL:
                case RolesUsuarios.SUP_AUDITOR:
                    {
                        var _m = from unidadEjecutora in db1.UnidadEjecutoras
                                 where unidadEjecutora.CT_LIVE == true
                                 select unidadEjecutora;

                        return _m.ToList();
                    }                    

                case RolesUsuarios.SUP_REGIONAL:  // solo por cubrir el caso regresamos todas las IE 
                    {
                        if (FK_IdUnidadResponsable != 0)
                        {
                            int region = FuncionesAuxiliares.GetRegionByUserName();
                            var _m = from unidadEjecutora in db1.UnidadEjecutoras
                                     where unidadEjecutora.CT_LIVE == true && (unidadEjecutora.Estado.Fk_IdRegion == region) && (unidadEjecutora.Fk_IdUnidadResponsable__UE == FK_IdUnidadResponsable)
                                     select unidadEjecutora;
                            return _m.ToList();
                        }
                        else
                        {
                            int region = FuncionesAuxiliares.GetRegionByUserName();
                            var _m = from unidadEjecutora in db1.UnidadEjecutoras
                                     where unidadEjecutora.CT_LIVE == true && (unidadEjecutora.Estado.Fk_IdRegion == region)
                                     select unidadEjecutora;
                            return _m.ToList();
                        }

                    }
                    
                case RolesUsuarios.UR_VEGETAL:
                case RolesUsuarios.UR_UPV:
                case RolesUsuarios.UR_INOCUIDAD:
                case RolesUsuarios.UR_ANIMAL:
                case RolesUsuarios.UR_ACUICOLA_PESQUERA:
                case RolesUsuarios.UR_MOVILIZACION:
                    #region consulta
                    {
                        var _m = (from UEj in db1.UnidadEjecutoras
                                  join PP in db1.ProyectoPresupuestoes on UEj.Pk_IdUnidadEjecutora equals PP.Fk_IdUnidadEjecutora
                                  join C in db1.Campanias on PP.Pk_IdProyectoPresupuesto equals C.Fk_IdProyectoPresupuesto
                                  join SC in db1.SubComponentes on UEj.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                                  join E in db1.Estadoes on UEj.Fk_IdEstado__SIS equals E.Pk_IdEstado
                                  join M in db1.Municipios on UEj.Fk_IdMunicipio equals M.Pk_IdMunicipio
                                  join TU in db1.TipoDeUnidads on UEj.Fk_IdTipoDeUnidad__SIS equals TU.Pk_IdTipoDeUnidad
                                  join UR in db1.UnidadResponsables on UEj.Fk_IdUnidadResponsable__UE equals UR.Pk_IdUnidadResponsable
                                  where PP.Fk_IdUnidadResponsable == FK_IdUnidadResponsable
                                  group PP by new
                                  {
                                      PP.Fk_IdUnidadEjecutora,
                                      UEj.StatusKardexUE,
                                      UEj.Fk_IdTipoDeUnidad__SIS,
                                      UEj.Nombre,
                                      UEj.Fk_IdEstado__SIS,
                                      UEj.Fk_IdMunicipio,
                                      UEj.Colonia,
                                      UEj.Direccion,
                                      UEj.CorreoElectronico,
                                      UEj.Telefono,
                                      UEj.Fk_IdUnidadResponsable__UE,
                                      UEj.RFC,
                                      UEj.Fk_IdSubComponente,
                                      UEj.NombreNotario,
                                      UEj.LugarNotario,
                                      UEj.Notaria,
                                      UEj.ActaConstitutiva,
                                      UEj.SitioWeb,
                                      UEj.FK_BancoCtaFederal,
                                      UEj.CtaFederal,
                                      UEj.FK_BancoCtaEstatal,
                                      UEj.CtaEstatal,
                                      UEj.FK_BancoCtaProductores,
                                      UEj.CtaProductores,
                                      Estado = E.Nombre,
                                      Municipio = M.Nombre,
                                      TU = TU.Nombre,
                                      UR = UR.Nombre,
                                      PP.Fk_IdEstado
                                  } into PP
                                  select new
                                  {
                                      Pk_IdUnidadEjecutora = PP.Key.Fk_IdUnidadEjecutora,
                                      StatusKardexUE = PP.Key.StatusKardexUE,
                                      Fk_IdTipoDeUnidad__SIS = PP.Key.Fk_IdTipoDeUnidad__SIS,
                                      Nombre = PP.Key.Nombre,
                                      Fk_IdEstado__SIS = PP.Key.Fk_IdEstado__SIS,
                                      Fk_IdMunicipio = PP.Key.Fk_IdMunicipio,
                                      Colonia = PP.Key.Colonia,
                                      Direccion = PP.Key.Direccion,
                                      CorreoElectronico = PP.Key.CorreoElectronico,
                                      Telefono = PP.Key.Telefono,
                                      Fk_IdUnidadResponsable__UE = PP.Key.Fk_IdUnidadResponsable__UE,
                                      RFC = PP.Key.RFC,
                                      Fk_IdSubComponente = PP.Key.Fk_IdSubComponente,
                                      NombreNotario = PP.Key.NombreNotario,
                                      LugarNotario = PP.Key.LugarNotario,
                                      Notaria = PP.Key.Notaria,
                                      ActaConstitutiva = PP.Key.ActaConstitutiva,
                                      SitioWeb = PP.Key.SitioWeb,
                                      FK_BancoCtaFederal = PP.Key.FK_BancoCtaFederal,
                                      CtaFederal = PP.Key.CtaFederal,
                                      FK_BancoCtaEstatal = PP.Key.FK_BancoCtaEstatal,
                                      CtaEstatal = PP.Key.CtaEstatal,
                                      FK_BancoCtaProductores = PP.Key.FK_BancoCtaProductores,
                                      CtaProductores = PP.Key.CtaProductores,
                                      Estado = PP.Key.Estado,
                                      Municipio = PP.Key.Municipio,
                                      TipoDeUnidad = PP.Key.TU,
                                      UnidadResponsable = PP.Key.UR,
                                      Fk_IdEstado = PP.Key.Fk_IdEstado
                                  }).OrderBy(c => c.Fk_IdEstado);
                        return _m.ToList();
                    }
                    #endregion

                case RolesUsuarios.REPRESENTANTE_ESTATAL:
                    {
                        var _m = from unidadEjecutora in db1.UnidadEjecutoras
                                 where unidadEjecutora.CT_LIVE == true && unidadEjecutora.Fk_IdEstado__SIS == _Fk_IdEstado
                                 select unidadEjecutora;

                        return _m.ToList();
                    }

                case RolesUsuarios.IE_ACUICOLA_PESQUERA:
                case RolesUsuarios.IE_ANIMAL:
                case RolesUsuarios.IE_VEGETAL:
                case RolesUsuarios.IE_MOVILIZACION:
                case RolesUsuarios.IE_INOCUIDAD:
                case RolesUsuarios.IE_PROGRAMAS_U:
                case RolesUsuarios.SUP_ESTATAL:

                    // PAR LOS CATALOGOS DE UNIDADEJECUTORA, INFORME DE QDQUISICIONES, SE DEBE LLENAR POR ESTE MÉTODO.
                    {
                        var _m = from unidadEjecutora in db1.UnidadEjecutoras
                                 where unidadEjecutora.CT_LIVE == true && unidadEjecutora.Pk_IdUnidadEjecutora == Fk_IdUnidadEjecutora
                                 select unidadEjecutora;

                        return _m.ToList();
                    }

                case RolesUsuarios.PUESTO_AUXILIAR_ADMINISTRATIVO:
                case RolesUsuarios.PUESTO_AUXILIAR_INFORMATICA:
                case RolesUsuarios.PUESTO_COOR_ADMINISTRATIVO:
                case RolesUsuarios.COORDINADOR_CAMPANIA:
                case RolesUsuarios.PUESTO_COOR_PROYECTO:
                case RolesUsuarios.PUESTO_COOR_REGIONAL:
                case RolesUsuarios.PUESTO_GERENTE:
                case RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS:
                case RolesUsuarios.PUESTO_PROF_ADMINISTRATIVO:
                case RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA:
                case RolesUsuarios.PUESTO_SECRETARIA:
                case RolesUsuarios.PUESTO_AUXILIAR_CAMPO:
                case RolesUsuarios.PUESTO_PROF_PROYECTO:
                case RolesUsuarios.PUESTO_PROF_TECNICO_CALIDAD_MEJORA_PROCESOS:
                case RolesUsuarios.PUESTO_PROF_TEC_CAPACITACION_DIVULGACION:
                    {
                        var _m = from unidadEjecutora in db1.UnidadEjecutoras
                                 where unidadEjecutora.CT_LIVE == true && unidadEjecutora.Pk_IdUnidadEjecutora == Fk_IdUnidadEjecutora
                                 select unidadEjecutora;

                        return _m.ToList();
                    }
                default:
                    {
                        var _m = from unidadEjecutora in db1.UnidadEjecutoras
                                 where unidadEjecutora.CT_LIVE == true && unidadEjecutora.Pk_IdUnidadEjecutora == 0  // regresa nada
                                 select unidadEjecutora;

                        return _m.ToList();
                    }

            }


        }

        //Deprecada la sustituye GetUnidadEjecutoraByUsuario
        public static IEnumerable GetUnidadEjecutoraXUsuario(string userroll, int? Fk_IdEstado, int? Fk_IdUnidadEjecutora)
        {
            DB_SENASICAEntities db1 = new DB_SENASICAEntities();

            IQueryable<UnidadEjecutora> _m = null;
            int region = FuncionesAuxiliares.GetRegionByUserName();
            if (Fk_IdUnidadEjecutora == 0)
            {
                switch (userroll)
                {
                    case RolesUsuarios.SUPERUSUARIO:
                    case RolesUsuarios.SUP_NACIONAL:
                        _m = from unidadejecutora in db1.UnidadEjecutoras
                             where unidadejecutora.CT_LIVE == true
                             select unidadejecutora;
                        break;
                    case RolesUsuarios.UR_INOCUIDAD:
                        _m = from unidadejecutora in db1.UnidadEjecutoras
                             where unidadejecutora.CT_LIVE == true && (unidadejecutora.Fk_IdSubComponente == 1026)
                             select unidadejecutora;
                        break;
                    case RolesUsuarios.UR_MOVILIZACION:
                        _m = from unidadejecutora in db1.UnidadEjecutoras
                             where unidadejecutora.CT_LIVE == true && (unidadejecutora.Fk_IdSubComponente == 1028)
                             select unidadejecutora;
                        break;
                    case RolesUsuarios.UR_ANIMAL:
                        _m = from unidadejecutora in db1.UnidadEjecutoras
                             where unidadejecutora.CT_LIVE == true && (unidadejecutora.Fk_IdSubComponente == 1 || unidadejecutora.Fk_IdSubComponente == 2)
                             select unidadejecutora;
                        break;
                    case RolesUsuarios.UR_ACUICOLA_PESQUERA:
                        _m = from unidadejecutora in db1.UnidadEjecutoras
                             where unidadejecutora.CT_LIVE == true && (unidadejecutora.Fk_IdSubComponente == 3)
                             select unidadejecutora;
                        break;
                    case RolesUsuarios.UR_VEGETAL:
                        _m = from unidadejecutora in db1.UnidadEjecutoras
                             where unidadejecutora.CT_LIVE == true && (unidadejecutora.Fk_IdSubComponente == 4 || unidadejecutora.Fk_IdSubComponente == 5)
                             select unidadejecutora;
                        break;
                    case RolesUsuarios.UR_UPV:
                        _m = from unidadejecutora in db1.UnidadEjecutoras
                             where unidadejecutora.CT_LIVE == true && (unidadejecutora.Fk_IdSubComponente == 1050)
                             select unidadejecutora;
                        break;
                    case RolesUsuarios.SUP_ESTATAL:
                        _m = from unidadejecutora in db1.UnidadEjecutoras
                             where unidadejecutora.CT_LIVE == true && (unidadejecutora.Estado.Pk_IdEstado == Fk_IdEstado)
                             select unidadejecutora;
                        break;
                    case RolesUsuarios.REPRESENTANTE_ESTATAL:
                        _m = from unidadejecutora in db1.UnidadEjecutoras
                             where unidadejecutora.CT_LIVE == true && (unidadejecutora.Estado.Pk_IdEstado == Fk_IdEstado)
                             select unidadejecutora;
                        break;

                    case RolesUsuarios.SUP_REGIONAL:
                        _m = from unidadejecutora in db1.UnidadEjecutoras
                             where unidadejecutora.CT_LIVE == true && (unidadejecutora.Estado.Fk_IdRegion == region)
                             select unidadejecutora;
                        break;
                }
            }
            else
            {
                _m = from unidadejecutora in db1.UnidadEjecutoras
                     where unidadejecutora.CT_LIVE == true && (unidadejecutora.Pk_IdUnidadEjecutora == Fk_IdUnidadEjecutora)
                     select unidadejecutora;
            }

            return _m.ToList();
        }

        /// <summary>
        /// Este metodo solo lo podrán acceder un usuario de Tipo UR
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetUnidadEjecutoraByUR()
        {
            int IdUnidadResponsable = FuncionesAuxiliares.GetCurrent_IdUnidadResponsable();
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.UnidadEjecutoras.Where(item => item.CT_LIVE == true && item.Fk_IdUnidadResponsable__UE == IdUnidadResponsable).ToList();
        }

        public static IEnumerable GetMes()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            return db.Mes.ToList();
        }

        //Se va a habiliar los meses de enero a junio para hacer el cerrado de las campañas
        public static IEnumerable GetMesACerrar()
        {
            int IdMesACerra = FuncionesAuxiliares.GetIdMesACerrar(); //obtiene el último mes a cerrar

            DB_SENASICAEntities db = new DB_SENASICAEntities();
            return db.Mes.Where(item => item.Pk_IdMes <= IdMesACerra).ToList();
        }

        /// <summary>
        /// Obtiene las campañas que tiene asignado una IE
        /// </summary>
        /// <param name="IdInstanciaEjecutora"></param>
        /// <returns></returns>
        public static IEnumerable GetCampaniaByIE(int IdInstanciaEjecutora)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            string rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();

            switch (rolUsuario)
            {
                case RolesUsuarios.UR_MOVILIZACION:
                    {
                        return db.Campanias.Join(db.ProyectoAutorizadoes, campanias => campanias.ProyectoPresupuesto.Fk_IdProyectoAutorizado, proyectoAutorizado => proyectoAutorizado.Pk_IdProyectoAutorizado,
                                  (campanias, proyectoAutorizado) => new { campanias, proyectoAutorizado })
                                  .Join(db.SubComponentes, ASC => ASC.proyectoAutorizado.Fk_IdSubComponente__SIS, PSC => PSC.Pk_IdSubComponente,
                                  (ASC, PSC) => new { ASC, PSC })
                                  .Join(db.UnidadResponsableXSubComponentes, SCURXSC => SCURXSC.PSC.Pk_IdSubComponente, URXSC => URXSC.Fk_IdSubComponente,
                                  (SCURXSC, URXSC) => new { SCURXSC, URXSC })
                                  .Where(o => o.URXSC.Fk_IdUnidadResponsable == 17 && o.SCURXSC.ASC.campanias.ProyectoPresupuesto.Fk_IdUnidadEjecutora == IdInstanciaEjecutora && o.SCURXSC.ASC.campanias.ProyectoPresupuesto.Fk_IdAnio == IdAnioPres)
                                  .Select(item => new { item.SCURXSC.ASC.campanias.Pk_IdCampania, item.SCURXSC.ASC.proyectoAutorizado.Nombre, item.SCURXSC.ASC.campanias.RecSol_Fed, item.SCURXSC.ASC.campanias.RecSol_Est }).OrderBy(item => item.Nombre).ToList();
                    }
                case RolesUsuarios.PUESTO_COOR_PROYECTO:
                    {
                        int IdPersonaCoordinador = FuncionesAuxiliares.GetCurrent_IdPersona();

                        return db.Campanias.Where(item => item.ProyectoPresupuesto.Fk_IdUnidadEjecutora == IdInstanciaEjecutora
                                                            && item.ProyectoPresupuesto.Fk_IdAnio == IdAnioPres
                                                            && item.Fk_IdCoordinador == IdPersonaCoordinador)
                                .Select(item => new { item.Pk_IdCampania, item.ProyectoPresupuesto.ProyectoAutorizado.Nombre, item.RecSol_Fed, item.RecSol_Est })
                                .OrderBy(item => item.Nombre).ToList();
                    }
                default:
                    {
                        return db.Campanias.Where(item => item.ProyectoPresupuesto.Fk_IdUnidadEjecutora == IdInstanciaEjecutora 
                                                && item.ProyectoPresupuesto.Fk_IdAnio == IdAnioPres)
                                .Select(item => new { item.Pk_IdCampania, item.ProyectoPresupuesto.ProyectoAutorizado.Nombre, item.RecSol_Fed, item.RecSol_Est })
                                .OrderBy(item => item.Nombre).ToList();
                    }
            }
        }

        /// <summary>
        /// Obtiene las campañas que tiene asignado una IE de acuerdo al usuario AIE logeado.
        /// 
        /// Si es un usuario del tipo Estatal, verá los de su estado
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetCampaniaByIE()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            int IdInstanciaEjecutora = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

            return db.Campanias.Where(item => item.ProyectoPresupuesto.Fk_IdUnidadEjecutora == IdInstanciaEjecutora 
                                                && item.ProyectoPresupuesto.Fk_IdAnio == IdAnioPres)
                                .OrderBy(item => item.ProyectoPresupuesto.ProyectoAutorizado.Nombre).ToList();
        }

        /// <summary>
        /// Obtiene los coordinadores que existen en un comité
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetCoordinadoresByComite()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdInstanciaEjecutora = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

            return db.Personas.Where(item => item.esActivo == true
                                        && item.Fk_IdUnidadEjecutora__UE == IdInstanciaEjecutora
                                        && (item.Fk_IdCargo == 17 || item.Fk_IdCargo == 15)) //el cargo es Coordinador de Proyecto o Gerente
                                .OrderBy(item => item.Cargo.Nombre).ThenBy(item => item.NombreCompleto).ToList();
        }

        public static int? GetSubComponenteByUser(string userId)
        {
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdUnidadEjecutora = (from usuarios in userManager.Users
                                     where usuarios.Id == userId
                                     select usuarios.FK_IdUnidadEjecutora__UE).First();

            var _query = (from unidadEjecutora in db.UnidadEjecutoras
                          where unidadEjecutora.Pk_IdUnidadEjecutora == IdUnidadEjecutora
                          select unidadEjecutora.Fk_IdSubComponente);

            return _query.Count() == 0 ? 0 : _query.First();
        }

        public static string GetNombreUsuario()
        {
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            string IdUsuario = HttpContext.Current.User.Identity.GetUserId();

            string _userName = (from usuario in userManager.Users
                                where usuario.Id == IdUsuario
                                select usuario.UserName).First();

            return _userName;
        }

        /// <summary>
        /// Investiga si el usuario logeado, de acuerdo a su rol tiene permisos de accesos a cierta pantalla (tabs)
        /// </summary>
        /// <param name="rolUsuario"></param>
        /// <returns></returns>
        public static Dictionary<string, bool> GetPermisoPantallaXUsuario(string rolUsuario, string clavePantalla)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            Dictionary<string, bool> _permisos = new Dictionary<string, bool>();

            var _query = db.PrivilegiosXRols.Where(p => p.Rol.Nombre == rolUsuario && p.Pantalla.Clave == clavePantalla).Select(p => new { p.Lectura, p.Escritura });

            if (_query.Count() == 0)
            {
                _permisos.Add("Lectura", false);
                _permisos.Add("Escritura", false);
            }
            else
            {
                _permisos.Add("Lectura", _query.Select(p => p.Lectura).First());
                _permisos.Add("Escritura", _query.Select(p => p.Escritura).First());
            }

            return _permisos;
        }

        public static string GetRolUsuarioLogeado()
        {
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            return userManager.GetRoles(HttpContext.Current.User.Identity.GetUserId()).First();
        }


        /// <summary>
        /// Devuelve los usuarios que están registrados en el sistema y tienen un ID y psw de acceso
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetUsers()
        {
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.VW_Users
                .OrderBy(item => item.Rol)
                .ThenBy(item => item.UserName)
                .ToList();
        }

        /// <summary>
        /// Devuelve los usuarios que están registrados en el sistema y tienen un ID y psw de acceso
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetUsersWithID(int? Fk_IdUnidadEjecutora)
        {
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            string rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();

            if (rolUsuario == RolesUsuarios.SUPERUSUARIO)
            {
                var _queryResultAllUsers = db.Personas.ToList().Join(userManager.Users, p => p.Pk_IdPersona, u => u.FK_IdPersona__SIS, (p, u) => new { p, u })
                                               .Where(_usuarios => _usuarios.p.esActivo == true
                                                                    && _usuarios.p.UnidadEjecutora.CT_LIVE == true)
                                               .Select(_usuarios => new
                                               {
                                                   ID = _usuarios.u.Id,
                                                   Estado = _usuarios.p.UnidadEjecutora.Estado.Nombre,
                                                   InstanciaEjecutora = _usuarios.p.UnidadEjecutora.Nombre,
                                                   _usuarios.u.UserName,
                                                   NombrePersona = _usuarios.p.NombreCompleto,
                                                   _usuarios.p.Email,
                                                   Puesto = _usuarios.p.Cargo == null ? "-" : _usuarios.p.Cargo.Nombre
                                               });
                return _queryResultAllUsers;
            }

            if (Fk_IdUnidadEjecutora != 0)
            {
                var _queryResultByUnidadEjecutora = db.Personas.ToList().Join(userManager.Users, p => p.Pk_IdPersona, u => u.FK_IdPersona__SIS, (p, u) => new { p, u })
                                                           .Where(_usuarios => _usuarios.p.esActivo == true
                                                                                && _usuarios.p.UnidadEjecutora.CT_LIVE == true
                                                                                && _usuarios.u.FK_IdUnidadEjecutora__UE == Fk_IdUnidadEjecutora)
                                                           .Select(_usuarios => new
                                                           {
                                                               ID = _usuarios.u.Id,
                                                               Estado = _usuarios.p.UnidadEjecutora.Estado.Nombre,
                                                               InstanciaEjecutora = _usuarios.p.UnidadEjecutora.Nombre,
                                                               _usuarios.u.UserName,
                                                               NombrePersona = _usuarios.p.NombreCompleto,
                                                               _usuarios.p.Email,
                                                               Puesto = _usuarios.p.Cargo == null ? "-" : _usuarios.p.Cargo.Nombre
                                                           });
                return _queryResultByUnidadEjecutora;
            }
            else
            {
                var _queryResultAllUsers = db.Personas.ToList().Join(userManager.Users, p => p.Pk_IdPersona, u => u.FK_IdPersona__SIS, (p, u) => new { p, u })
                                               .Where(_usuarios => _usuarios.p.esActivo == true
                                                                    && _usuarios.p.UnidadEjecutora.CT_LIVE == true)
                                               .Select(_usuarios => new
                                               {
                                                   ID = _usuarios.u.Id,
                                                   Estado = _usuarios.p.UnidadEjecutora.Estado.Nombre,
                                                   InstanciaEjecutora = _usuarios.p.UnidadEjecutora.Nombre,
                                                   _usuarios.u.UserName,
                                                   NombrePersona = _usuarios.p.NombreCompleto,
                                                   _usuarios.p.Email,
                                                   Puesto = _usuarios.p.Cargo == null ? "-" : _usuarios.p.Cargo.Nombre
                                               });
                return _queryResultAllUsers;
            }
        }
        /// <summary>
        /// De la Tabla Especie, devuelve todos los registros que estén dados de alta
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetEspecie()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.Especies.ToList();
        }

        /// <summary>
        /// De la Tabla FuncionZootecnica, devuelve todos los registros que estén dados de alta
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetFuncionZootecnica()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.FuncionZootecnicas.ToList();
        }
        /// <summary>
        /// De la Tabla UnidadMedidaProduccion, devuelve todos los registros que estén dados de alta
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetUnidadMedidaProduccion()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.UnidadMedidaProduccions.ToList();
        }
        /// <summary>
        /// De la Tabla DestinoProduccion, devuelve todos los registros que estén dados de alta
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetDestinoProduccion()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.DestinoProduccions.ToList();
        }
        /// <summary>
        /// De la Tabla AtendidoSA, devuelve los registros que estén asociados a una Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetAtendidoSAByCampania(int? IdCampania, string StatusActual)
        {
            if (IdCampania == null) return null;
            else
            {
                DB_SENASICAEntities db = new DB_SENASICAEntities();
                var StatusSinRepro = FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania);
                if (StatusActual == StatusSinRepro)
                {
                    return db.AtendidoSAs.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
                var StatusEnRepro = FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania);
                if (StatusActual == StatusEnRepro)
                {
                    return db.AtendidoSA_Repro.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
            }
            return null;
        }
        /// <summary>
        /// De la Tabla ImportanciaEnfermedadSA, devuelve los registros que estén asociados a una Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetImportanciaEnfermedadSAByCampania(int? IdCampania, string StatusActual)
        {
            if (IdCampania == null) return null;
            else
            {
                DB_SENASICAEntities db = new DB_SENASICAEntities();
                var StatusSinRepro = FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania);
                if (StatusActual == StatusSinRepro)
                {
                    return db.ImportanciaEnfermedadSAs.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
                var StatusEnRepro = FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania);
                if (StatusActual == StatusEnRepro)
                {
                    return db.ImportanciaEnfermedadSA_Repro.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
            }
            return null;
        }
        /// <summary>
        /// De la Tabla ImportanciaCultivoSV, devuelve los registros que estén asociados a una Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetImportanciaCultivoSVByCampania(int? IdCampania, string StatusActual)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            if (IdCampania == null)
                return null;
            else
            {
                var StatusSinRepro = FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania);
                if (StatusActual == StatusSinRepro)
                {
                    return db.ImportanciaCultivoSVs.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
                var StatusEnRepro = FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania);
                if (StatusActual == StatusEnRepro)
                {
                    return db.ImportanciaCultivoSV_Repro.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
            }
            return null;
        }
        /// <summary>
        /// De la Tabla ImportanciaPlagaSV, devuelve los registros que estén asociados a una Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetImportanciaPlagaSVByCampania(int? IdCampania, string StatusActual)
        {
            if (IdCampania == null)
                return null;
            else
            {
                DB_SENASICAEntities db = new DB_SENASICAEntities();
                var StatusSinRepro = FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania);
                if (StatusActual == StatusSinRepro)
                {
                    return db.ImportanciaPlagaSVs.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
                var StatusEnRepro = FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania);
                if (StatusActual == StatusEnRepro)
                {
                    return db.ImportanciaPlagaSV_Repro.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
            }
            return null;
        }
        /// <summary>
        /// De la Tabla AtendidoSV, devuelve los registros que estén asociados a una Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetAtendidoSVByCampania(int? IdCampania, string StatusActual)
        {
            if (IdCampania == null)
                return null;
            else
            {
                DB_SENASICAEntities db = new DB_SENASICAEntities();
                var StatusSinRepro = FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania);
                if (StatusActual == StatusSinRepro)
                {
                    return db.AtendidoSVs.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
                var StatusEnRepro = FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania);
                if (StatusActual == StatusEnRepro)
                {
                    return db.AtendidoSV_Repro.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
            }
            return null;
        }
        /// <summary>
        /// De la Tabla AtendidoMov, devuelve los registros que estén asociados a una Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetAtendidoMovByCampania(int? IdCampania, string StatusActual)
        {
            if (IdCampania == null)
                return null;
            else
            {
                DB_SENASICAEntities db = new DB_SENASICAEntities();
                var StatusSinRepro = FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania);
                if (StatusActual == StatusSinRepro)
                {
                    return db.AtendidoMovs.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
                var StatusEnRepro = FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania);
                if (StatusActual == StatusEnRepro)
                {
                    return db.AtendidoMov_Repro.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
            }
            return null;
        }
        /// <summary>
        /// De la Tabla ImportanciaPIVMovs, devuelve los registros que estén asociados a una Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetImportanciaPVIMovByCampania(int? IdCampania, string StatusActual)
        {
            if (IdCampania == null)
                return null;
            else
            {
                DB_SENASICAEntities db = new DB_SENASICAEntities();
                var StatusSinRepro = FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania);
                if (StatusActual == StatusSinRepro)
                {
                    return db.ImportanciaPIVMovs.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
                var StatusEnRepro = FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania);
                if (StatusActual == StatusEnRepro)
                {
                    return db.ImportanciaPIVMov_Repro.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
            }
            return null;
        }
        /// <summary>
        /// De la Tabla ImportanciaProgramaMov, devuelve los registros que estén asociados a una Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetImportanciaProgramaMovByCampania(int? IdCampania, string StatusActual)
        {
            if (IdCampania == null)
                return null;
            else
            {
                DB_SENASICAEntities db = new DB_SENASICAEntities();
                var StatusSinRepro = FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania);
                if (StatusActual == StatusSinRepro)
                {
                    return db.ImportanciaProgramaMovs.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
                var StatusEnRepro = FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania);
                if (StatusActual == StatusEnRepro)
                {
                    return db.ImportanciaProgramaMov_Repro.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
            }
            return null;
        }
        /// <summary>
        /// De la Tabla SistemaProduccion, devuelve todos los registros que estén dados de alta
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetSistemaProduccion()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.SistemaProduccions.ToList();
        }
        /// <summary>
        /// De la Tabla AtendidoSAC, devuelve los registros que estén asociados a una Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetAtendidoSACByCampania(int? IdCampania, string StatusActual)
        {
            if (IdCampania == null) return null;
            else
            {
                DB_SENASICAEntities db = new DB_SENASICAEntities();
                var StatusSinRepro = FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania);
                if (StatusActual == StatusSinRepro)
                {
                    return db.AtendidoSACs.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
                var StatusEnRepro = FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania);
                if (StatusActual == StatusEnRepro)
                {
                    return db.AtendidoSAC_Repro.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
            }
            return null;
        }
        /// <summary>
        /// De la Tabla ImportanciaEnfermedadSAC, devuelve los registros que estén asociados a una Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetImportanciaEnfermedadSACByCampania(int? IdCampania, string StatusActual)
        {
            if (IdCampania == null) return null;
            else
            {
                DB_SENASICAEntities db = new DB_SENASICAEntities();
                var StatusSinRepro = FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania);
                if (StatusActual == StatusSinRepro)
                {
                    return db.ImportanciaEnfermedadSACs.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
                var StatusEnRepro = FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania);
                if (StatusActual == StatusEnRepro)
                {
                    return db.ImportanciaEnfermedadSAC_Repro.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
            }
            return null;
        }
        /// <summary>
        /// De la Tabla ImportanciaCultivoSAC, devuelve los registros que estén asociados a una Campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetImportanciaCultivoSACByCampania(int? IdCampania, string StatusActual)
        {
            if (IdCampania == null)
                return null;
            else
            {
                DB_SENASICAEntities db = new DB_SENASICAEntities();
                var StatusSinRepro = FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania);
                if (StatusActual == StatusSinRepro)
                {
                    return db.ImportanciaCultivoSACs.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
                var StatusEnRepro = FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania);
                if (StatusActual == StatusEnRepro)
                {
                    return db.ImportanciaCultivoSAC_Repro.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
            }
            return null;
        }

        public static IEnumerable GetTipoUnidad_Atendido()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.TipoUnidad_Atendido.ToList();
        }

        public static IEnumerable GetTipoUnidad_UCSV()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.TipoUnidad_UCSV.ToList();
        }

        public static IEnumerable GetTipoUnidad_UCAC()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.TipoUnidad_UCAC.ToList();
        }

        public static IEnumerable GetTipoProduccion()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.TipoProduccions.ToList();
        }

        public static IEnumerable GetTendenciaProduccion()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.TendenciaProduccions.ToList();
        }

        public static IEnumerable GetAtendidoInoByCampania(int? IdCampania, string StatusActual)
        {
            if (IdCampania == null) return null;
            else
            {
                DB_SENASICAEntities db = new DB_SENASICAEntities();
                var StatusSinRepro = FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania);
                if (StatusActual == StatusSinRepro)
                {
                    return db.AtendidoInoes.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
                var StatusEnRepro = FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania);
                if (StatusActual == StatusEnRepro)
                {
                    return db.AtendidoIno_Repro.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
            }
            return null;
        }

        public static IEnumerable GetUnidadesCertificarInoSVByCampania(int? IdCampania, string StatusActual)
        {
            if (IdCampania == null) return null;
            else
            {
                DB_SENASICAEntities db = new DB_SENASICAEntities();
                var StatusSinRepro = FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania);
                if (StatusActual == StatusSinRepro)
                {
                    return db.UnidadesCertificarInoSVs.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
                var StatusEnRepro = FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania);
                if (StatusActual == StatusEnRepro)
                {
                    return db.UnidadesCertificarInoSV_Repro.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
            }
            return null;
        }

        public static IEnumerable GetUnidadesCertificarInoSAByCampania(int? IdCampania, string StatusActual)
        {
            if (IdCampania == null) return null;
            else
            {
                DB_SENASICAEntities db = new DB_SENASICAEntities();
                var StatusSinRepro = FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania);
                if (StatusActual == StatusSinRepro)
                {
                    return db.UnidadesCertificarInoSAs.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
                var StatusEnRepro = FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania);
                if (StatusActual == StatusEnRepro)
                {
                    return db.UnidadesCertificarInoSA_Repro.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
            }
            return null;
        }

        public static IEnumerable GetUnidadesCertificarInoSACByCampania(int? IdCampania, string StatusActual)
        {
            if (IdCampania == null) return null;
            else
            {
                DB_SENASICAEntities db = new DB_SENASICAEntities();
                var StatusSinRepro = FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania);
                if (StatusActual == StatusSinRepro)
                {
                    return db.UnidadesCertificarInoSACs.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
                var StatusEnRepro = FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania);
                if (StatusActual == StatusEnRepro)
                {
                    return db.UnidadesCertificarInoSAC_Repro.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
            }
            return null;
        }

        public static IEnumerable GetImportanciaInoByCampania(int? IdCampania, string StatusActual)
        {
            if (IdCampania == null) return null;
            else
            {
                DB_SENASICAEntities db = new DB_SENASICAEntities();
                var StatusSinRepro = FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania);
                if (StatusActual == StatusSinRepro)
                {
                    return db.ImportanciaInoes.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
                var StatusEnRepro = FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania);
                if (StatusActual == StatusEnRepro)
                {
                    return db.ImportanciaIno_Repro.Where(item => item.Fk_IdCampania == IdCampania).ToList();
                }
            }
            return null;
        }

        public static DataSet getDataSetCubos(string nomStored, int tiporecurso)
        {
            string NomStored = nomStored;
            int Pk_IdAnio__SIS = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            int IdUR = FuncionesAuxiliares.GetCurrent_IdUnidadResponsable();
            string IdUser = FuncionesAuxiliares.GetCurrent_IdUserName();

            System.Data.SqlClient.SqlConnection _cnn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CubosOlapSIMOSICAConnection"].ConnectionString);
            System.Data.SqlClient.SqlCommand _parametro = new System.Data.SqlClient.SqlCommand(NomStored, _cnn);

            _parametro.Parameters.Add("FK_IdAnio__SIS", SqlDbType.Int).Value = Pk_IdAnio__SIS;
            _parametro.Parameters.Add("Fk_IdUnidadResponsable__UE", SqlDbType.Int).Value = IdUR;
            _parametro.Parameters.Add("TipoRecurso", SqlDbType.Int).Value = tiporecurso;
            _parametro.Parameters.Add("IdUser", SqlDbType.VarChar).Value = IdUser;
            _parametro.CommandType = CommandType.StoredProcedure;
            _cnn.Open();
            _parametro.ExecuteNonQuery();

            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(_parametro);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            return ds;
        }
        public static DataSet getDataSetCubos_(string nomStored)
        {
            string NomStored = nomStored;
            int Pk_IdAnio__SIS = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            int IdUR = FuncionesAuxiliares.GetCurrent_IdUnidadResponsable();
            string IdUser = FuncionesAuxiliares.GetCurrent_IdUserName();

            System.Data.SqlClient.SqlConnection _cnn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CubosOlapSIMOSICAConnection"].ConnectionString);
            System.Data.SqlClient.SqlCommand _parametro = new System.Data.SqlClient.SqlCommand(NomStored, _cnn);

            _parametro.Parameters.Add("FK_IdAnio__SIS", SqlDbType.Int).Value = Pk_IdAnio__SIS;
            _parametro.Parameters.Add("Fk_IdUnidadResponsable__UE", SqlDbType.Int).Value = IdUR;
            _parametro.Parameters.Add("IdUser", SqlDbType.VarChar).Value = IdUser;
            _parametro.CommandType = CommandType.StoredProcedure;
            _cnn.Open();
            _parametro.ExecuteNonQuery();

            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(_parametro);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            return ds;
        }
        /// <summary>
        /// Regresa las ministraciones que se han registrado para los comités.
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetMinistraciones()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            string rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();
            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            IQueryable<MinistracionesXComite> _data = null;

            if (rolUsuario == RolesUsuarios.IE_VEGETAL
                || rolUsuario == RolesUsuarios.IE_ANIMAL
                || rolUsuario == RolesUsuarios.IE_INOCUIDAD
                || rolUsuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || rolUsuario == RolesUsuarios.IE_MOVILIZACION
                || rolUsuario == RolesUsuarios.IE_PROGRAMAS_U
                || rolUsuario == RolesUsuarios.PUESTO_COOR_ADMINISTRATIVO
                || rolUsuario == RolesUsuarios.PUESTO_PROF_ADMINISTRATIVO
                || rolUsuario == RolesUsuarios.PUESTO_AUXILIAR_ADMINISTRATIVO
                || rolUsuario == RolesUsuarios.PUESTO_SECRETARIA
                )
            {
                int IdIE = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

                _data = db.MinistracionesXComites.Where(item => item.Fk_IdAnio == IdAnioPres
                                                                && item.Fk_IdInstanciaEjecutora == IdIE);
            }

            if(rolUsuario == RolesUsuarios.SUPERUSUARIO
                || rolUsuario == RolesUsuarios.SUP_NACIONAL
                || rolUsuario == RolesUsuarios.SUP_AUDITOR)
            {
                _data = db.MinistracionesXComites.Where(item => item.Fk_IdAnio == IdAnioPres);
            }

            if (rolUsuario == RolesUsuarios.UR_VEGETAL
                || rolUsuario == RolesUsuarios.UR_ANIMAL
                || rolUsuario == RolesUsuarios.UR_ACUICOLA_PESQUERA
                || rolUsuario == RolesUsuarios.UR_INOCUIDAD
                || rolUsuario == RolesUsuarios.UR_MOVILIZACION
                || rolUsuario == RolesUsuarios.UR_UPV)
            {
                int IdUR = FuncionesAuxiliares.GetCurrent_IdUnidadResponsable();

                _data = db.MinistracionesXComites.Where(item => item.Fk_IdAnio == IdAnioPres
                                                                && item.UnidadEjecutora.Fk_IdUnidadResponsable__UE == IdUR);
            }

            if (rolUsuario == RolesUsuarios.SUP_REGIONAL)
            {
                int IdRegion = FuncionesAuxiliares.GetRegionByUserName();

                _data = db.MinistracionesXComites.Where(item => item.Fk_IdAnio == IdAnioPres
                                                                && item.UnidadEjecutora.Estado.Region.Pk_IdRegion == IdRegion);
            }

            if (rolUsuario == RolesUsuarios.SUP_ESTATAL
                || rolUsuario == RolesUsuarios.REPRESENTANTE_ESTATAL)
            {
                int IdEstado = FuncionesAuxiliares.GetCurrent_IdEstado();

                _data = db.MinistracionesXComites.Where(item => item.Fk_IdAnio == IdAnioPres
                                                                && item.UnidadEjecutora.Estado.Pk_IdEstado == IdEstado);
            }

            return _data.OrderBy(item => item.UnidadEjecutora.Nombre).ThenBy(item => item.Fecha).ToList();
        }

        /// <summary>
        /// Obtiene la lista de bancos
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetBancos()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.Bancoes.ToList();
        }
        /// <summary>
        /// Devuelve la lista de las campañas que el usuario ha cerrado
        /// 
        /// Esta función la va a ocupar solamente los AIE
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetCierreMensual()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            string rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();
            IQueryable<CierreMensual> _elementos = null;

            if (rolUsuario == RolesUsuarios.SUPERUSUARIO
                || rolUsuario == RolesUsuarios.SUP_NACIONAL
                || rolUsuario == RolesUsuarios.SUP_AUDITOR)
            {
                _elementos = db.CierreMensuals.Where(item => item.esCerrado == true
                                                            && item.Campania.ProyectoPresupuesto.Fk_IdAnio == IdAnioPres);
            }

            if (rolUsuario == RolesUsuarios.SUP_REGIONAL)
            {
                int IdRegion = FuncionesAuxiliares.GetRegionByUserName();

                _elementos = db.CierreMensuals.Where(item => item.esCerrado == true
                                                    && item.Campania.ProyectoPresupuesto.Fk_IdAnio == IdAnioPres
                                                    && item.Campania.ProyectoPresupuesto.UnidadEjecutora.Estado.Fk_IdRegion == IdRegion);
            }

            if (rolUsuario == RolesUsuarios.UR_VEGETAL
                || rolUsuario == RolesUsuarios.UR_ANIMAL
                || rolUsuario == RolesUsuarios.UR_ACUICOLA_PESQUERA
                || rolUsuario == RolesUsuarios.UR_INOCUIDAD
                || rolUsuario == RolesUsuarios.UR_MOVILIZACION)
            {
                int IdUR = FuncionesAuxiliares.GetCurrent_IdUnidadResponsable();

                _elementos = db.CierreMensuals.Where(item => item.esCerrado == true
                                                    && item.Campania.ProyectoPresupuesto.Fk_IdAnio == IdAnioPres
                                                    && item.Campania.ProyectoPresupuesto.UnidadResponsable.Pk_IdUnidadResponsable == IdUR);
            }
            
            if (rolUsuario == RolesUsuarios.IE_VEGETAL
                || rolUsuario == RolesUsuarios.IE_ANIMAL
                || rolUsuario == RolesUsuarios.IE_INOCUIDAD
                || rolUsuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || rolUsuario == RolesUsuarios.IE_MOVILIZACION
                || rolUsuario == RolesUsuarios.IE_PROGRAMAS_U
                || rolUsuario == RolesUsuarios.SUP_ESTATAL)
            {
                int IdUnidadEjecutora = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

                _elementos = db.CierreMensuals.Where(item => item.Campania.ProyectoPresupuesto.Fk_IdUnidadEjecutora == IdUnidadEjecutora
                                                                && item.Campania.ProyectoPresupuesto.Fk_IdAnio == IdAnioPres);
            }
            if (rolUsuario == RolesUsuarios.REPRESENTANTE_ESTATAL)
            {
                int IdEstado = FuncionesAuxiliares.GetCurrent_IdEstado();

                _elementos = db.CierreMensuals.Where(item => item.esCerrado == true
                                                    && item.Campania.ProyectoPresupuesto.Fk_IdAnio == IdAnioPres
                                                    && item.Campania.ProyectoPresupuesto.Fk_IdEstado == IdEstado);
            }
            return _elementos.OrderBy(item => item.esCerrado)
                                .ThenByDescending(item => item.Fk_IdMes)
                                .ThenBy(item => item.Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre)
                                .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetCierreMensualHist(int IdCierreMensual)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            return db.CierreMensualSolicitudAperturas.Where(item => item.Fk_IdCierreMensual == IdCierreMensual
                                                            && item.CierreMensual.Campania.ProyectoPresupuesto.Fk_IdAnio == IdAnioPres)
                                                        .OrderByDescending(item => item.Pk_IdCierreMensualSolicitudApertura)
                                                        .ToList();
        }

        /// <summary>
        /// Devuelve todas las campañas que el usuario AIE pide su apertura
        /// 
        /// Esta información la verá la UCE para que decida si abre o no las campañas
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetCierreMensualSolAperturaUCE()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            IQueryable<CierreMensual> _elementos = null;

            _elementos = db.CierreMensualSolicitudAperturas
                                    .Where(item => item.estaAtendido == false
                                                    && item.CierreMensual.Campania.ProyectoPresupuesto.Fk_IdAnio == IdAnioPres)
                                    .Select(item => item.CierreMensual);


            return _elementos.OrderBy(item => item.esCerrado)
                                .ThenByDescending(item => item.Fk_IdMes)
                                .ThenBy(item => item.Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre)
                                .ToList();
        }

        /// <summary>
        /// Obtiene las campañas faltantes a cerrar
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetCampaniasFaltantesCerrarByMes(int IdMesACerrar)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            int IdIE = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();
            //int IdMesACerrar = FuncionesAuxiliares.GetIdMesACerrar();

            var _campaniasRestantes = (from campanias in db.Campanias
                                       where !(from cierreMensual in db.CierreMensuals
                                               where cierreMensual.Fk_IdMes == IdMesACerrar
                                               select cierreMensual.Fk_IdCampania).Contains(campanias.Pk_IdCampania)
                                               && campanias.ProyectoPresupuesto.Fk_IdUnidadEjecutora == IdIE
                                               && campanias.ProyectoPresupuesto.Fk_IdAnio == IdAnioPres
                                       select new { campanias.Pk_IdCampania, campanias.ProyectoPresupuesto.ProyectoAutorizado.Nombre, campanias.RecSol_Fed, campanias.RecSol_Est });

            return _campaniasRestantes.ToList();
        }

        public static IEnumerable GetDetalleMinistracionByMinistracion(int? IdMinistracionesXComite)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.MinistracionDetalles.Where(item => item.Fk_IdMinistracionesXComite == IdMinistracionesXComite).ToList();
        }

        /// <summary>
        /// La UCE va a poder revisar todas las campañas y poderles cambiar el status de:
        /// 
        /// Solicitud de Modificación > Se Autoriza hacer Modificaciones
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetCampaniasUCE()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            return db.Campanias.Where(item => item.ProyectoPresupuesto.Fk_IdAnio == IdAnioPresupuestal).ToList();
        }

        /// <summary>
        /// Devuelve las campañas correspondientes a un comité
        /// 
        /// Esta función devuelve sólo los programas "S" (item.Fk_IdPrograma__SIS == 1)
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetCampanias()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            string rolusuario = FuncionesAuxiliares.GetCurrent_RolUsuario();

            if (rolusuario == RolesUsuarios.SUPERUSUARIO
                || rolusuario == RolesUsuarios.SUP_NACIONAL
                || rolusuario == RolesUsuarios.SUP_AUDITOR)
            {
                return db.VW_CampaniasMontosTransversales.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                        && item.Fk_IdPrograma__SIS == 1).ToList();
            }

            if (rolusuario == RolesUsuarios.SUP_REGIONAL)
            {
                int IdRegion = FuncionesAuxiliares.GetRegionByUserName();
                return db.VW_CampaniasMontosTransversales.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                    && item.Fk_IdRegion == IdRegion
                                                                    && item.Fk_IdPrograma__SIS == 1).ToList();
            }

            if (rolusuario == RolesUsuarios.UR_VEGETAL
                || rolusuario == RolesUsuarios.UR_ANIMAL
                || rolusuario == RolesUsuarios.UR_ACUICOLA_PESQUERA
                || rolusuario == RolesUsuarios.UR_INOCUIDAD
                || rolusuario == RolesUsuarios.UR_MOVILIZACION
                || rolusuario == RolesUsuarios.UR_UPV)
            {
                int IdUnidadResponsable = FuncionesAuxiliares.GetCurrent_IdUnidadResponsable();

                return db.VW_CampaniasMontosTransversales.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                        && item.Fk_IdUnidadResponsable == IdUnidadResponsable
                                                                        && item.Fk_IdPrograma__SIS == 1).ToList();
            }

            if (rolusuario == RolesUsuarios.REPRESENTANTE_ESTATAL
                || rolusuario == RolesUsuarios.SUP_ESTATAL)
            {
                int IdEstado = FuncionesAuxiliares.GetCurrent_IdEstado();

                return db.VW_CampaniasMontosTransversales.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                        && item.Fk_IdEstado == IdEstado
                                                                        && item.Fk_IdPrograma__SIS == 1).ToList();
            }

            if (rolusuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || rolusuario == RolesUsuarios.IE_ANIMAL
                || rolusuario == RolesUsuarios.IE_VEGETAL
                || rolusuario == RolesUsuarios.IE_MOVILIZACION
                || rolusuario == RolesUsuarios.IE_INOCUIDAD
                || rolusuario == RolesUsuarios.IE_PROGRAMAS_U
                || rolusuario == RolesUsuarios.COORDINADOR_CAMPANIA
                || rolusuario == RolesUsuarios.PUESTO_GERENTE
                || rolusuario == RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                || rolusuario == RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                || rolusuario == RolesUsuarios.PUESTO_COOR_ADMINISTRATIVO
                || rolusuario == RolesUsuarios.PUESTO_PROF_ADMINISTRATIVO
                || rolusuario == RolesUsuarios.PUESTO_AUXILIAR_ADMINISTRATIVO
                || rolusuario == RolesUsuarios.PUESTO_SECRETARIA
                )
            {
                int IdComite = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

                return db.VW_CampaniasMontosTransversales.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                        && item.Fk_IdUnidadEjecutora == IdComite
                                                                        && item.Fk_IdPrograma__SIS == 1).ToList();
            }

            if (rolusuario == RolesUsuarios.PUESTO_COOR_PROYECTO)
            {
                int IdComite = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();
                int IdCoordinador = FuncionesAuxiliares.GetCurrent_IdPersona();

                return db.VW_CampaniasMontosTransversales.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                        && item.Fk_IdUnidadEjecutora == IdComite
                                                                        && item.Fk_IdCoordinador == IdCoordinador
                                                                        && item.Fk_IdPrograma__SIS == 1).ToList();
            }

            return null;
        }

        /// <summary>
        /// Esta función devuelve los programas "U" (&& item.Fk_IdPrograma__SIS == 38)
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetCampaniasU()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            string rolusuario = FuncionesAuxiliares.GetCurrent_RolUsuario();

            if (rolusuario == RolesUsuarios.SUPERUSUARIO
                || rolusuario == RolesUsuarios.SUP_NACIONAL
                || rolusuario == RolesUsuarios.SUP_AUDITOR)
            {
                return db.VW_CampaniasMontosTransversales.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                        && item.Fk_IdPrograma__SIS == 38).ToList();
            }

            if (rolusuario == RolesUsuarios.SUP_REGIONAL)
            {
                int IdRegion = FuncionesAuxiliares.GetRegionByUserName();
                return db.VW_CampaniasMontosTransversales.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                    && item.Fk_IdRegion == IdRegion
                                                                    && item.Fk_IdPrograma__SIS == 38).ToList();
            }

            if (rolusuario == RolesUsuarios.UR_VEGETAL
                || rolusuario == RolesUsuarios.UR_ANIMAL
                || rolusuario == RolesUsuarios.UR_ACUICOLA_PESQUERA
                || rolusuario == RolesUsuarios.UR_INOCUIDAD
                || rolusuario == RolesUsuarios.UR_MOVILIZACION
                || rolusuario == RolesUsuarios.UR_UPV)
            {
                int IdUnidadResponsable = FuncionesAuxiliares.GetCurrent_IdUnidadResponsable();

                return db.VW_CampaniasMontosTransversales.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                        && item.Fk_IdUnidadResponsable == IdUnidadResponsable
                                                                        && item.Fk_IdPrograma__SIS == 38).ToList();
            }

            if (rolusuario == RolesUsuarios.REPRESENTANTE_ESTATAL
                || rolusuario == RolesUsuarios.SUP_ESTATAL)
            {
                int IdEstado = FuncionesAuxiliares.GetCurrent_IdEstado();

                return db.VW_CampaniasMontosTransversales.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                        && item.Fk_IdEstado == IdEstado
                                                                        && item.Fk_IdPrograma__SIS == 38).ToList();
            }

            if (rolusuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || rolusuario == RolesUsuarios.IE_ANIMAL
                || rolusuario == RolesUsuarios.IE_VEGETAL
                || rolusuario == RolesUsuarios.IE_MOVILIZACION
                || rolusuario == RolesUsuarios.IE_INOCUIDAD
                || rolusuario == RolesUsuarios.IE_PROGRAMAS_U
                || rolusuario == RolesUsuarios.COORDINADOR_CAMPANIA
                || rolusuario == RolesUsuarios.PUESTO_GERENTE
                || rolusuario == RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                || rolusuario == RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                || rolusuario == RolesUsuarios.PUESTO_COOR_ADMINISTRATIVO
                || rolusuario == RolesUsuarios.PUESTO_PROF_ADMINISTRATIVO
                || rolusuario == RolesUsuarios.PUESTO_AUXILIAR_ADMINISTRATIVO
                || rolusuario == RolesUsuarios.PUESTO_SECRETARIA
                )
            {
                int IdComite = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

                return db.VW_CampaniasMontosTransversales.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                        && item.Fk_IdUnidadEjecutora == IdComite
                                                                        && item.Fk_IdPrograma__SIS == 38).ToList();
            }

            if (rolusuario == RolesUsuarios.PUESTO_COOR_PROYECTO)
            {
                int IdComite = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();
                int IdCoordinador = FuncionesAuxiliares.GetCurrent_IdPersona();

                return db.VW_CampaniasMontosTransversales.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                        && item.Fk_IdUnidadEjecutora == IdComite
                                                                        && item.Fk_IdCoordinador == IdCoordinador
                                                                        && item.Fk_IdPrograma__SIS == 38).ToList();
            }

            return null;
        }

        /// <summary>
        /// Muestra las campañas ejecutadas por el gobierno del Estado
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetCampaniasEst()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            int IdComite = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

            return db.VW_CampaniasMontosTransversales.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                        && item.Fk_IdUnidadEjecutora == IdComite
                                                                        && item.Fk_IdPrograma__SIS == 1).ToList();

            //return _query.ToList();
        }

        /// <summary>
        /// Devuelve los proyectos presupuestos asociados a un comité
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetProyectoPresupuesto(int IdProyectoPresupuesto)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            string rolusuario = FuncionesAuxiliares.GetCurrent_RolUsuario();
            List<ProyectoPresupuesto> _query = null;

            if (rolusuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || rolusuario == RolesUsuarios.IE_ANIMAL
                || rolusuario == RolesUsuarios.IE_VEGETAL
                || rolusuario == RolesUsuarios.IE_MOVILIZACION
                || rolusuario == RolesUsuarios.IE_INOCUIDAD
                || rolusuario == RolesUsuarios.IE_PROGRAMAS_U
                || rolusuario == RolesUsuarios.COORDINADOR_CAMPANIA
                || rolusuario == RolesUsuarios.PUESTO_GERENTE
                || rolusuario == RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                || rolusuario == RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                )
            {
                int IdIE = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

                _query = db.ProyectoPresupuestoes.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                && item.Fk_IdUnidadEjecutora == IdIE).ToList();
            }

            if(rolusuario == RolesUsuarios.SUP_ESTATAL)
            {
                int IdIE = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

                _query = db.ProyectoPresupuestoes.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                && item.Fk_IdUnidadEjecutora == IdIE).ToList();
            }

            if (rolusuario == RolesUsuarios.REPRESENTANTE_ESTATAL)
            {
                int IdEstado = FuncionesAuxiliares.GetCurrent_IdEstado();

                _query = db.ProyectoPresupuestoes.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                && item.Fk_IdEstado == IdEstado).ToList();
            }
            
            if(_query == null)
            {
                _query = db.ProyectoPresupuestoes.Where(item => item.Pk_IdProyectoPresupuesto == IdProyectoPresupuesto)
                            .ToList();
            }

            return _query;
        }

        public static IEnumerable GetProyectoPresupuestoS(int IdProyectoPresupuesto)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            string rolusuario = FuncionesAuxiliares.GetCurrent_RolUsuario();
            List<ProyectoPresupuesto> _query = null;

            if (rolusuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || rolusuario == RolesUsuarios.IE_ANIMAL
                || rolusuario == RolesUsuarios.IE_VEGETAL
                || rolusuario == RolesUsuarios.IE_MOVILIZACION
                || rolusuario == RolesUsuarios.IE_INOCUIDAD
                || rolusuario == RolesUsuarios.IE_PROGRAMAS_U
                || rolusuario == RolesUsuarios.COORDINADOR_CAMPANIA
                || rolusuario == RolesUsuarios.PUESTO_GERENTE
                || rolusuario == RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                || rolusuario == RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                )
            {
                int IdIE = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

                _query = db.ProyectoPresupuestoes.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                && item.Fk_IdUnidadEjecutora == IdIE
                                                                && item.ProyectoAutorizado.SubComponente.Incentivo.Componente.Fk_IdPrograma__SIS == 1).ToList();
            }

            if (rolusuario == RolesUsuarios.SUP_ESTATAL)
            {
                int IdIE = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

                _query = db.ProyectoPresupuestoes.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                && item.Fk_IdUnidadEjecutora == IdIE
                                                                && item.ProyectoAutorizado.SubComponente.Incentivo.Componente.Fk_IdPrograma__SIS == 1).ToList();
            }

            if (rolusuario == RolesUsuarios.REPRESENTANTE_ESTATAL)
            {
                int IdEstado = FuncionesAuxiliares.GetCurrent_IdEstado();

                _query = db.ProyectoPresupuestoes.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                && item.Fk_IdEstado == IdEstado
                                                                && item.ProyectoAutorizado.SubComponente.Incentivo.Componente.Fk_IdPrograma__SIS == 1).ToList();
            }

            if (_query == null)
            {
                _query = db.ProyectoPresupuestoes.Where(item => item.Pk_IdProyectoPresupuesto == IdProyectoPresupuesto
                                                                && item.ProyectoAutorizado.SubComponente.Incentivo.Componente.Fk_IdPrograma__SIS == 1).ToList();
            }

            return _query;
        }

        public static IEnumerable GetProyectoPresupuestoU(int IdProyectoPresupuesto)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            string rolusuario = FuncionesAuxiliares.GetCurrent_RolUsuario();
            List<ProyectoPresupuesto> _query = null;

            if (rolusuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || rolusuario == RolesUsuarios.IE_ANIMAL
                || rolusuario == RolesUsuarios.IE_VEGETAL
                || rolusuario == RolesUsuarios.IE_MOVILIZACION
                || rolusuario == RolesUsuarios.IE_INOCUIDAD
                || rolusuario == RolesUsuarios.IE_PROGRAMAS_U
                || rolusuario == RolesUsuarios.COORDINADOR_CAMPANIA
                || rolusuario == RolesUsuarios.PUESTO_GERENTE
                || rolusuario == RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                || rolusuario == RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                )
            {
                int IdIE = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

                _query = db.ProyectoPresupuestoes.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                && item.Fk_IdUnidadEjecutora == IdIE
                                                && item.ProyectoAutorizado.SubComponente.Incentivo.Componente.Fk_IdPrograma__SIS == 38).ToList();
            }

            if (rolusuario == RolesUsuarios.SUP_ESTATAL)
            {
                int IdIE = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

                _query = db.ProyectoPresupuestoes.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                && item.Fk_IdUnidadEjecutora == IdIE
                                                                && item.ProyectoAutorizado.SubComponente.Incentivo.Componente.Fk_IdPrograma__SIS == 38).ToList();
            }

            if (rolusuario == RolesUsuarios.REPRESENTANTE_ESTATAL)
            {
                int IdEstado = FuncionesAuxiliares.GetCurrent_IdEstado();

                _query = db.ProyectoPresupuestoes.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                && item.Fk_IdEstado == IdEstado
                                                                && item.ProyectoAutorizado.SubComponente.Incentivo.Componente.Fk_IdPrograma__SIS == 38).ToList();
            }

            if (_query == null)
            {
                _query = db.ProyectoPresupuestoes.Where(item => item.Pk_IdProyectoPresupuesto == IdProyectoPresupuesto
                                                                && item.ProyectoAutorizado.SubComponente.Incentivo.Componente.Fk_IdPrograma__SIS == 38).ToList();
            }

            return _query;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetCuadroComparativoXpantalla(int? IdCampania)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var _m = from CuadroComparativo in db.CuadroCompXPantallas
                     where CuadroComparativo.CT_LIVE == true
                     orderby CuadroComparativo.NombrePantalla ascending
                     select CuadroComparativo;
            return _m.ToList();
        }

        public static int? GetIdUnidadResponsableXCamp(int? IdCampania)
        {
            GetStatusActualCampania(Convert.ToInt32(IdCampania));

            if (IdCampania == 0) return 0;
            else
            {
                DB_SENASICAEntities db = new DB_SENASICAEntities();

                var _m = from C in db.Campanias
                         join PP in db.ProyectoPresupuestoes on C.Fk_IdProyectoPresupuesto equals PP.Pk_IdProyectoPresupuesto
                         where C.Pk_IdCampania == IdCampania
                         select PP.Fk_IdUnidadResponsable;
                return _m.First();
            }
        }

        /// <summary>
        /// Devuelve el historial de solicitudes de una campaña
        /// </summary>
        /// <param name="IdCierreMensual"></param>
        /// <returns></returns>
        public static IEnumerable GetHistorialSolicitudesCierreMensual(int IdCierreMensual)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.CierreMensualSolicitudAperturas.Where(item => item.Fk_IdCierreMensual == IdCierreMensual && item.estaAtendido == true)
                                                    .OrderByDescending(item => item.Pk_IdCierreMensualSolicitudApertura)
                                                    .ToList();
        }
        public static IEnumerable GetTipoDeAdquisicion()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var _m = from tipodeAdq in db.TipoAdquisicions select tipodeAdq;

            return _m.ToList();
        }
        /// <summary>
        /// Llena el Combo de Unidad Responsable por Usuario par el Reporte Personal de la IE
        /// </summary>
        /// <returns></returns>
        public static IEnumerable Get_ReportURByUsuario()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int? IdUR = FuncionesAuxiliares.GetCurrent_IdUnidadResponsable();
            var rolusuario = FuncionesAuxiliares.GetCurrent_RolUsuario();

            switch (rolusuario)
            {
                case RolesUsuarios.SUPERUSUARIO:
                case RolesUsuarios.SUP_NACIONAL:
                    {
                        var _m = from UR in db.UnidadResponsables
                                 select UR;
                        return _m.ToList();
                    }

                case RolesUsuarios.UR_ACUICOLA_PESQUERA:
                case RolesUsuarios.UR_ANIMAL:
                case RolesUsuarios.UR_INOCUIDAD:
                case RolesUsuarios.UR_MOVILIZACION:
                case RolesUsuarios.UR_VEGETAL:
                case RolesUsuarios.SUP_ESTATAL:
                    {
                        var _m = from UR in db.UnidadResponsables
                                 where UR.Pk_IdUnidadResponsable == IdUR
                                 select UR;
                        return _m.ToList();
                    }
                case RolesUsuarios.SUP_REGIONAL:
                case RolesUsuarios.REPRESENTANTE_ESTATAL:
                    {
                        var _m = from UR in db.UnidadResponsables
                                 where UR.Pk_IdUnidadResponsable == 6 // Vegetal
                                        || UR.Pk_IdUnidadResponsable == 14 // Animal
                                        || UR.Pk_IdUnidadResponsable == 19 // APesquera
                                        || UR.Pk_IdUnidadResponsable == 25 // UCE
                                 select UR;
                        return _m.ToList();
                    }
                default:
                    {
                        var _m = from UR in db.UnidadResponsables
                                 where UR.Pk_IdUnidadResponsable == 0 // nada
                                 select UR;
                        return _m.ToList();
                    }
            }
        }
        public static IEnumerable Get_ReportEstadoByUsuario()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            var rolusuario = FuncionesAuxiliares.GetCurrent_RolUsuario();
            int IdRegion = FuncionesAuxiliares.GetRegionByUserName();

            switch(rolusuario)
            {
                case RolesUsuarios.SUPERUSUARIO:
                case RolesUsuarios.SUP_NACIONAL:
                case RolesUsuarios.UR_ACUICOLA_PESQUERA:
                case RolesUsuarios.UR_ANIMAL:
                case RolesUsuarios.UR_INOCUIDAD:
                case RolesUsuarios.UR_MOVILIZACION:
                case RolesUsuarios.UR_VEGETAL:
                    {
                        var _m = from E in db.Estadoes
                                 select E;
                        return _m.ToList();
                    }
                case RolesUsuarios.SUP_REGIONAL:
                    {
                        var _m = from E in db.Estadoes
                                 where E.Fk_IdRegion == IdRegion
                                 select E;
                        return _m.ToList();
                    }

                case RolesUsuarios.SUP_ESTATAL:
                case RolesUsuarios.REPRESENTANTE_ESTATAL:
                    {
                        int IdEstado = FuncionesAuxiliares.GetCurrent_IdEstado();
                        var _m = from E in db.Estadoes
                                 where E.Pk_IdEstado == IdEstado
                                 select E;
                        return _m.ToList();
                    }
            }

            return null;
        }

        /// <summary>
        /// Muestra un resumen del presupuesto FEDERAL de cada una de las campañas. El desglose es el siguiente:
        /// 
        /// -- Presupuesto Autorizado: el que la UCE y AUR le autoriza
        /// -- Presupuesto solicitado: el que el comité programa
        /// -- Presupuesto Gastado: lo que el comité a reportado como ya gastado
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetPresupuestoCampanias()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            return db.VW_PresupuestoCampania.Where(item => item.Fk_IdAnio == IdAnioPres)
                        .OrderBy(item => item.Fk_IdEstado__SIS)
                        .ThenBy(item => item.UnidadEjecutora)
                        .ThenBy(item => item.UnidadResponsable)
                        .ToList();
        }

        /// <summary>
        /// Para empezar a realizar la reprogramación, el usuario podrá ver solo las campañas que estén en ejecución
        /// </summary>
        /// <param name="IdEstado"></param>
        /// <returns></returns>
        public static IEnumerable GetPresupuestoCampanias(int IdEstado)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            return db.VW_PresupuestoCampania.Where(item => item.Fk_IdAnio == IdAnioPres
                                                        && item.Fk_IdEstado__SIS == IdEstado
                                                        && item.StatusKardex == ConstantesGlobales.EN_EJECUCION)
                        .OrderBy(item => item.Fk_IdEstado__SIS)
                        .ThenBy(item => item.UnidadEjecutora)
                        .ThenBy(item => item.UnidadResponsable)
                        .ToList();
        }

        /// <summary>
        /// Obtiene el motivo del movimiento
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetMotivoMovimiento()
        {
            DB_ReprogramacionRecFed reproFed = new DB_ReprogramacionRecFed();

            return reproFed.MotivoIncremento.ToList();
        }

        /// <summary>
        /// Obtendrá los registros que indican cada movimiento realizados de acuerdo a su identificador
        /// </summary>
        /// <param name="Identificador"></param>
        /// <returns></returns>
        public static IEnumerable GetMotivoMovimiento(int Identificador)
        {
            DB_ReprogramacionRecFed reproFed = new DB_ReprogramacionRecFed();

            return reproFed.MovRepRecursoFed.Where(item => item.Identificador == Identificador)
                                            .ToList();
        }

        /// <summary>
        /// Obtiene los movimientos históricos de un estado
        /// </summary>
        /// <param name="IdEstado"></param>
        /// <returns></returns>
        public static IEnumerable GetHistMotivoMovimiento(int IdEstado)
        {
            DB_ReprogramacionRecFed reproFed = new DB_ReprogramacionRecFed();
            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            return reproFed.MovRepRecursoFed.Where(item => item.Fk_IdAnio == IdAnioPres
                                                            && item.Fk_IdEstado == IdEstado
                                                            && item.esHistorico == true).ToList();
        }

        public static IEnumerable GetCampaniasReproRecFed()
        {
            DB_ReprogramacionRecFed reproFed = new DB_ReprogramacionRecFed();

            return reproFed.CampaniasXEstado.ToList();
        }
        /// <summary>
        /// sí Existe una repro de la Campaña, a nivel de Programa de Trabajo
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static bool GetExisteHistoricoReproPT(int IdCampania)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            int Anio = db.Anios.Where(a => a.Pk_IdAnio == IdAnioPres)
                                .Select(a => a.Anio1)
                                .First();

            var existe = db.VW_HistoricoReproPT.Where(item => item.Fk_IdCampania_Orig == IdCampania);
            return (existe.Count() == 0 || existe.Count() == null) ? false : true;
        }
        /// <summary>
        /// Devuelve el Tipo de la Campaña, para generar el reporte como Histórico 
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static int GetTipoCampaniaByPTHistorico(int IdCampania)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            if (IdCampania == 0) return 0;
            else
            {
                int? IdPP = db.Campanias.Where(item => item.Pk_IdCampania == IdCampania)
                                       .Select(item => item.Fk_IdProyectoPresupuesto).First();

                var _m = from UR in db.ProyectoPresupuestoes
                         where UR.Pk_IdProyectoPresupuesto == IdPP
                         select UR.Fk_IdTipoCampania;

                return Convert.ToInt32(_m.First());
            }
        }
        /// <summary>
        /// Obtiene el total de Reprogramaciones de la Campaña, a nivel de Programa de Trabajo
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static IEnumerable GetHistoricoReproPT(int IdCampania)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            int Anio = db.Anios.Where(a => a.Pk_IdAnio == IdAnioPres)
                                .Select(a => a.Anio1)
                                .First();

            return db.VW_HistoricoReproPT.Where(item => item.Fk_IdCampania_Orig == IdCampania)

                        .OrderBy(item => item.Fk_IdCampania_Orig)
                        .ThenBy(item => item.Consecutivo_Repro)
                        .ToList();
        }

        public static IEnumerable GetPEFXEstado()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            return db.PEFXEstadoes.Where(item => item.FK_IdAnio == IdAnioPres).ToList();
        }

        public static bool GetEsRecursoCerradoPEFXEstado()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            return db.PEFXEstadoes.Where(item => item.FK_IdAnio == IdAnioPres && item.PresupuestoCerrado == true)
                                   .Count() != 0;
        }

        public static IEnumerable GetPresupuestoXDFG()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            int anioPres = db.Anios.Where(item => item.Pk_IdAnio == IdAnioPres).Select(item => item.Anio1).First();

            return db.VW_PresupuestoXDFG.Where(item => item.Anio == anioPres).ToList();
        }

        struct tipoTrimestre 
        {
            public string IdTrimestre;
            public string Descripcion;
        }
        public static IEnumerable GetTrimestre()
        {
            List<tipoTrimestre> trimestre = new List<tipoTrimestre>();
            trimestre.Add(new tipoTrimestre { IdTrimestre = "1", Descripcion = "Primer Trimestre" });
            trimestre.Add(new tipoTrimestre { IdTrimestre = "2", Descripcion = "Segundo Trimestre" });
            trimestre.Add(new tipoTrimestre { IdTrimestre = "3", Descripcion = "Tercer Trimestre" });
            trimestre.Add(new tipoTrimestre { IdTrimestre = "4", Descripcion = "Cuarto Trimestre" });

            var query = from t in trimestre select new { IdTrimestre = t.IdTrimestre, Descripcion = t.Descripcion };
            return query.ToList();
        }

        struct tipoPrograma
        {
            public string IdPrograma;
            public string Descripcion;
        }
        public static IEnumerable GetTipoPrograma()
        {
            List<tipoPrograma> programa = new List<tipoPrograma>();
            programa.Add(new tipoPrograma { IdPrograma = "1", Descripcion = "S" });
            programa.Add(new tipoPrograma { IdPrograma = "2", Descripcion = "U" });

            var query = from t in programa select new { IdPrograma = t.IdPrograma, Descripcion = t.Descripcion };
            return query.ToList();
        }

        #region CapituloPartidas
        /// <summary>
        /// Devuelve el Capitulo Partida de la tabla SIS.CapituloPartidas
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetCapituloPartidasByAnioPres()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int IdAnio = Convert.ToInt32(HttpContext.Current.Session["IdAnio"]);

            return db.CapituloPartidas.Where(b => b.Fk_IdAnio == IdAnio).ToList();
        }
        #endregion

        #region ConceptoPartidas
        /// <summary>
        /// Devuelve el Concepto Partida de la tabla SIS.ConceptoPartidas
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetConceptoPartidasByAnioPres()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int IdAnio = Convert.ToInt32(HttpContext.Current.Session["IdAnio"]);

            return db.ConceptoPartidas.Where(b => b.Fk_IdAnio == IdAnio).ToList();
        }
        #endregion

        #region Partidas
        /// <summary>
        /// Devuelve la Partida de la tabla SIS.Partidas
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetPartidasByAnioPres()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int IdAnio = Convert.ToInt32(HttpContext.Current.Session["IdAnio"]);

            return db.Partidas.Where(b => b.Fk_IdAnio == IdAnio).ToList();
        }
        #endregion

        #region TipoDeBiens
        /// <summary>
        /// Devuelve el Tipo De Bien de la tabla SIS.TipoDeBiens
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetTipoDeBiensByAnioPres()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int IdAnio = Convert.ToInt32(HttpContext.Current.Session["IdAnio"]);

            return db.TipoDeBiens.Where(b => b.Fk_IdAnio == IdAnio).ToList();
        }
        #endregion

        #region TipoDeReembolso
        /// <summary>
        /// Devuelve consulta de Tipo de Reembolso de la tabla SIS.TipoReembolso
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetTiposDeReembolsoRestante()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var _m = from tiporeembolso in db.TipoReembolsoes select tiporeembolso;
            return _m.ToList();

        }
        #endregion

        /// <summary>
        /// Regresa los Reembolsos que se han registrado por Comité.
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetTesoFe()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            string rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();
            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            IQueryable<TESOFE> _data = null;

            if (rolUsuario == RolesUsuarios.IE_VEGETAL
                || rolUsuario == RolesUsuarios.IE_ANIMAL
                || rolUsuario == RolesUsuarios.IE_INOCUIDAD
                || rolUsuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || rolUsuario == RolesUsuarios.IE_MOVILIZACION
                || rolUsuario == RolesUsuarios.IE_PROGRAMAS_U
                || rolUsuario == RolesUsuarios.SUP_ESTATAL
                )
            {
                int IdIE = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

                _data = db.TESOFEs.Where(item => item.Fk_IdAnio == IdAnioPres
                                                && item.Fk_IdUnidadEjecutora == IdIE);
            }

            if (rolUsuario == RolesUsuarios.SUPERUSUARIO
                || rolUsuario == RolesUsuarios.SUP_NACIONAL
                || rolUsuario == RolesUsuarios.SUP_AUDITOR)
            {
                _data = db.TESOFEs.Where(item => item.Fk_IdAnio == IdAnioPres);
            }

            if (rolUsuario == RolesUsuarios.UR_VEGETAL
                || rolUsuario == RolesUsuarios.UR_ANIMAL
                || rolUsuario == RolesUsuarios.UR_ACUICOLA_PESQUERA
                || rolUsuario == RolesUsuarios.UR_INOCUIDAD
                || rolUsuario == RolesUsuarios.UR_MOVILIZACION
                || rolUsuario == RolesUsuarios.UR_UPV)
            {
                int IdUR = FuncionesAuxiliares.GetCurrent_IdUnidadResponsable();

                _data = db.TESOFEs.Where(item => item.Fk_IdAnio == IdAnioPres
                                                                && item.UnidadEjecutora.Fk_IdUnidadResponsable__UE == IdUR);
            }

            if (rolUsuario == RolesUsuarios.SUP_REGIONAL)
            {
                int IdRegion = FuncionesAuxiliares.GetRegionByUserName();

                _data = db.TESOFEs.Where(item => item.Fk_IdAnio == IdAnioPres
                                                                && item.UnidadEjecutora.Estado.Region.Pk_IdRegion == IdRegion);
            }

            if (rolUsuario == RolesUsuarios.REPRESENTANTE_ESTATAL)
            {
                int IdEstado = FuncionesAuxiliares.GetCurrent_IdEstado();

                _data = db.TESOFEs.Where(item => item.Fk_IdAnio == IdAnioPres
                                                                && item.UnidadEjecutora.Estado.Pk_IdEstado == IdEstado);
            }

            return _data.OrderBy(item => item.UnidadEjecutora.Fk_IdEstado__SIS).ThenBy(item => item.Fk_IdTipodeRecurso).ToList();
        }

        public static IEnumerable GetTesoFeDetalleByTESOFE(int? IdTesofe)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.TesofeDetalles.Where(item => item.Fk_IdTesofe == IdTesofe).ToList();
        }
        public static IEnumerable GetProyectoPresupuesto()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            string rolusuario = FuncionesAuxiliares.GetCurrent_RolUsuario();
            List<ProyectoPresupuesto> _query = null;

            if (rolusuario == RolesUsuarios.SUPERUSUARIO
                || rolusuario == RolesUsuarios.SUP_NACIONAL
                || rolusuario == RolesUsuarios.SUP_AUDITOR)
            {
                _query = db.ProyectoPresupuestoes.Where(item => item.Fk_IdAnio == IdAnioPresupuestal).ToList();
            }

            if (rolusuario == RolesUsuarios.UR_VEGETAL
                || rolusuario == RolesUsuarios.UR_ANIMAL
                || rolusuario == RolesUsuarios.UR_ACUICOLA_PESQUERA
                || rolusuario == RolesUsuarios.UR_INOCUIDAD
                || rolusuario == RolesUsuarios.UR_MOVILIZACION
                || rolusuario == RolesUsuarios.UR_UPV)
            {
                int IdUR = FuncionesAuxiliares.GetCurrent_IdUnidadResponsable();

                _query = db.ProyectoPresupuestoes.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                && item.UnidadEjecutora.Fk_IdUnidadResponsable__UE == IdUR).ToList();
            }

            if (rolusuario == RolesUsuarios.IE_VEGETAL
                || rolusuario == RolesUsuarios.IE_ANIMAL
                || rolusuario == RolesUsuarios.IE_INOCUIDAD
                || rolusuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || rolusuario == RolesUsuarios.IE_MOVILIZACION
                || rolusuario == RolesUsuarios.IE_PROGRAMAS_U
                || rolusuario == RolesUsuarios.SUP_ESTATAL
                )
            {
                int IdIE = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

                _query = db.ProyectoPresupuestoes.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                && item.Fk_IdUnidadEjecutora == IdIE).ToList();
            }

            if (rolusuario == RolesUsuarios.SUP_REGIONAL)
            {
                int IdRegion = FuncionesAuxiliares.GetRegionByUserName();

                _query = db.ProyectoPresupuestoes.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                && item.UnidadEjecutora.Estado.Region.Pk_IdRegion == IdRegion).ToList();
            }
            if (rolusuario == RolesUsuarios.REPRESENTANTE_ESTATAL)
            {
                int IdEstado = FuncionesAuxiliares.GetCurrent_IdEstado();

                _query = db.ProyectoPresupuestoes.Where(item => item.Fk_IdAnio == IdAnioPresupuestal
                                                                && item.Fk_IdEstado == IdEstado).ToList();
            }

            if (_query == null)
            {
                _query = null;
            }

            return _query;
        }

        public static IEnumerable GetLicitacionesByUsuario()
        { 
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            
            var query = db.Licitacions.ToList();

            return query;
        }

        public static IEnumerable GetCapituloPartidas()
        { 
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            return db.CapituloPartidaLicitacions.ToList();
        }

        public static IEnumerable GetUnidadEjecutoraByEstados(int Pk_IdEstado)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            List<UnidadEjecutora> _query = null;
            if (Pk_IdEstado != null && Pk_IdEstado <= 0)
            {
                _query = db.UnidadEjecutoras.Where(x => x.Fk_IdEstado__SIS == Pk_IdEstado).ToList();
            }
            else
            {
                _query = db.UnidadEjecutoras.ToList();
            }
            
            return _query;
        }
        
        /// <summary>
        /// Devuelve la lista de las actas de comisión que el usuario ha registrado
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetActaComision()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            string rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();
            IQueryable<ActaComision> _elementos = null;

            if (rolUsuario == RolesUsuarios.SUPERUSUARIO
                || rolUsuario == RolesUsuarios.SUP_NACIONAL
                || rolUsuario == RolesUsuarios.SUP_AUDITOR)
            {
                _elementos = db.ActaComisions.Where(item => item.Fk_IdAnio == IdAnioPres);
            }

            if (rolUsuario == RolesUsuarios.SUP_REGIONAL)
            {
                int IdRegion = FuncionesAuxiliares.GetRegionByUserName();

                _elementos = db.ActaComisions.Where(item => item.Fk_IdAnio == IdAnioPres
                                                    && item.Estado.Fk_IdRegion == IdRegion);
            }

            if (rolUsuario == RolesUsuarios.UR_VEGETAL
                || rolUsuario == RolesUsuarios.UR_ANIMAL
                || rolUsuario == RolesUsuarios.UR_ACUICOLA_PESQUERA
                || rolUsuario == RolesUsuarios.UR_INOCUIDAD
                || rolUsuario == RolesUsuarios.UR_MOVILIZACION
                || rolUsuario == RolesUsuarios.UR_UPV)
            {
                int IdUR = FuncionesAuxiliares.GetCurrent_IdUnidadResponsable();
                var _Mes = (from AC in db.ActaComisions
                            join e in db.Estadoes on AC.Fk_IdEstado equals e.Pk_IdEstado
                            join mes in db.Mes on AC.Fk_IdMes equals mes.Pk_IdMes
                            join ue in db.UnidadEjecutoras on AC.Fk_IdEstado equals ue.Fk_IdEstado__SIS
                            join pp in db.ProyectoPresupuestoes on ue.Pk_IdUnidadEjecutora equals pp.Fk_IdUnidadEjecutora
                            where pp.Fk_IdUnidadResponsable == IdUR && AC.Fk_IdAnio == IdAnioPres && pp.Fk_IdAnio == IdAnioPres
                            group AC by new
                            {
                                AC.Pk_IdActaComision,
                                AC.Fk_IdEstado,
                                AC.Fk_IdMes,
                                AC.Fk_IdMesTrimestre,
                                AC.Documento,
                                AC.CT_Date,
                                e.Nombre,
                                Mes = mes.Descripcion,
                                Periodo = ""
                            } into AC
                            select new
                            {
                                Pk_IdActaComision = AC.Key.Pk_IdActaComision,
                                Fk_IdEstado = AC.Key.Fk_IdEstado,
                                Fk_IdMes = AC.Key.Fk_IdMes,
                                Fk_IdMesTrimestre = AC.Key.Fk_IdMesTrimestre,
                                Documento = AC.Key.Documento,
                                CT_Date = AC.Key.CT_Date,
                                Estado = AC.Key.Nombre,
                                Mes = AC.Key.Mes,
                                Periodo = AC.Key.Periodo
                            }).OrderBy(c => c.Fk_IdEstado)
                          .ThenBy(item => item.Fk_IdMes)
                          .ThenBy(item => item.Fk_IdMesTrimestre);

                var _MesT = (from AC in db.ActaComisions
                             join e in db.Estadoes on AC.Fk_IdEstado equals e.Pk_IdEstado
                             join mesT in db.MesByTrimestres on AC.Fk_IdMesTrimestre equals mesT.Pk_IdMesTrimestre
                             join ue in db.UnidadEjecutoras on AC.Fk_IdEstado equals ue.Fk_IdEstado__SIS
                             join pp in db.ProyectoPresupuestoes on ue.Pk_IdUnidadEjecutora equals pp.Fk_IdUnidadEjecutora
                             where pp.Fk_IdUnidadResponsable == IdUR && AC.Fk_IdAnio == IdAnioPres && pp.Fk_IdAnio == IdAnioPres
                             group AC by new
                             {
                                 AC.Pk_IdActaComision,
                                 AC.Fk_IdEstado,
                                 AC.Fk_IdMes,
                                 AC.Fk_IdMesTrimestre,
                                 AC.Documento,
                                 AC.CT_Date,
                                 e.Nombre,
                                 Mes = "",
                                 Mest = mesT.Descripcion
                             } into AC
                             select new
                             {
                                 Pk_IdActaComision = AC.Key.Pk_IdActaComision,
                                 Fk_IdEstado = AC.Key.Fk_IdEstado,
                                 Fk_IdMes = AC.Key.Fk_IdMes,
                                 Fk_IdMesTrimestre = AC.Key.Fk_IdMesTrimestre,
                                 Documento = AC.Key.Documento,
                                 CT_Date = AC.Key.CT_Date,
                                 Estado = AC.Key.Nombre,
                                 Mes = AC.Key.Mes,
                                 Periodo = AC.Key.Mest
                             }).OrderBy(c => c.Fk_IdEstado)
                             .ThenBy(item => item.Fk_IdMes)
                             .ThenBy(item => item.Fk_IdMesTrimestre);
                var fullOuterJoin = _Mes.Union(_MesT);

                return fullOuterJoin.OrderBy(item => item.Fk_IdEstado)
                                .ThenBy(item => item.Fk_IdMes)
                                .ThenBy(item => item.Fk_IdMesTrimestre)
                                .ToList();
            }

            if (rolUsuario == RolesUsuarios.IE_VEGETAL
                || rolUsuario == RolesUsuarios.IE_ANIMAL
                || rolUsuario == RolesUsuarios.IE_INOCUIDAD
                || rolUsuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || rolUsuario == RolesUsuarios.IE_MOVILIZACION
                || rolUsuario == RolesUsuarios.IE_PROGRAMAS_U
                || rolUsuario == RolesUsuarios.SUP_ESTATAL)
            {
                int IdUnidadEjecutora = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();

                _elementos = from AC in db.ActaComisions
                             join ue in db.UnidadEjecutoras on AC.Fk_IdEstado equals ue.Fk_IdEstado__SIS
                             where ue.Pk_IdUnidadEjecutora == IdUnidadEjecutora && AC.Fk_IdAnio == IdAnioPres
                             select AC;
            }
            if (rolUsuario == RolesUsuarios.REPRESENTANTE_ESTATAL)
            {
                int IdEstado = FuncionesAuxiliares.GetCurrent_IdEstado();

                _elementos = db.ActaComisions.Where(item => item.Fk_IdAnio == IdAnioPres
                                                    && item.Fk_IdEstado == IdEstado);
            }
            return _elementos.OrderBy(item => item.Estado.Pk_IdEstado)
                                .ThenBy(item => item.Fk_IdMes)
                                .ThenBy(item => item.Fk_IdMesTrimestre)
                                .ToList();
        }

        public static IEnumerable GetMesRestante()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int IdEstado = FuncionesAuxiliares.GetCurrent_IdEstado();
            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            int IdAnio = db.Anios.Where(i => i.Pk_IdAnio == IdAnioPres).Select(i => i.Anio1).First();

            if (IdAnio < 2018)
            {
                var Mes = (from m in db.Mes
                 where !(from ac in db.ActaComisions
                         where ac.Fk_IdEstado == IdEstado && ac.Fk_IdAnio == IdAnioPres && ac.Fk_IdMes != null
                         select ac.Fk_IdMes).Contains(m.Pk_IdMes)
                 select new { m.Pk_IdMes, m.Descripcion });

                return Mes.ToList();


            }
            else
            {
                var MesByTrimestres = (from mt in db.MesByTrimestres
                           where !(from ac in db.ActaComisions
                                   where ac.Fk_IdEstado == IdEstado && ac.Fk_IdAnio == IdAnioPres && ac.Fk_IdMesTrimestre != null
                                   select ac.Fk_IdMesTrimestre).Contains(mt.Pk_IdMesTrimestre)
                           select new { mt.Pk_IdMesTrimestre, mt.Descripcion });

                return MesByTrimestres.ToList();
            }
        }

        /// <summary>
        /// Obtiene todos los roles del sistema que se podrán crear por una cuenta superUsuario.
        /// 
        /// A continuación la lista original:
        /// 
        /// Administrador de Instancia Ejecutora de Inocuidad
        /// Administrador de Instancia Ejecutora de Movilizacion
        /// Administrador de Instancia Ejecutora de Salud Animal
        /// Administrador de Instancia Ejecutora de Sanidad Acuicola y Pesquera
        /// Administrador de Instancia Ejecutora de Sanidad Vegetal
        /// Administrador de Unidad Responsable de Inocuidad
        /// Administrador de Unidad Responsable de la Unidad de Promocion y Vinculacion
        /// Administrador de Unidad Responsable de Movilizacion
        /// Administrador de Unidad Responsable de Salud Animal
        /// Administrador de Unidad Responsable de Sanidad Acuicola y Pesquera
        /// Administrador de Unidad Responsable de Sanidad Vegetal
        /// Administrador de Unidad Responsable UCE
        /// Administrador Programas U
        /// Auditor
        /// Representante Estatal
        /// Superusuario
        /// Supervisor Estatal
        /// Supervisor Nacional
        /// Supervisor Regional
        /// Técnico Operativo
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetRoles()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.Rols.Where(item => item.Pk_IdRol == 1
            || item.Pk_IdRol == 2
            || item.Pk_IdRol == 3
            || item.Pk_IdRol == 4
            || item.Pk_IdRol == 5
            || item.Pk_IdRol == 6
            || item.Pk_IdRol == 33
            || item.Pk_IdRol == 7
            || item.Pk_IdRol == 8
            || item.Pk_IdRol == 9
            || item.Pk_IdRol == 10
            || item.Pk_IdRol == 31
            || item.Pk_IdRol == 34
            || item.Pk_IdRol == 35
            || item.Pk_IdRol == 30
            || item.Pk_IdRol == 26
            || item.Pk_IdRol == 28
            || item.Pk_IdRol == 32
            || item.Pk_IdRol == 29
            || item.Pk_IdRol == 27).OrderBy(item => item.Nombre).ToList();
        }

        public static string GetRolNameById(int IdRol)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.Rols.Where(item => item.Pk_IdRol == IdRol)
                .Select(item => item.Nombre)
                .First();
        }

        public static int GetIdRolByName(string rol)
        {
            if (rol == "")
                return 0;

            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.Rols.Where(item => item.Nombre == rol)
                .Select(item => item.Pk_IdRol)
                .First();
        }
    }
}