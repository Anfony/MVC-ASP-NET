using DevExpress.Web.ASPxEditors;
using DXMVCWebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMVCWebApplication1.Common
{
    public static class FuncionesAuxiliares
    {
        /// <summary>
        /// Obtiene el Id del mes a cerrar
        /// </summary>
        /// <returns></returns>
        public static int GetIdMesACerrar()
        {
            int diaActual = DateTime.Now.Day;
            int IdMesACerrar;

            if (diaActual <= 15) IdMesACerrar = DateTime.Now.Month - 1;
            else IdMesACerrar = DateTime.Now.Month;

            if (IdMesACerrar == 0) IdMesACerrar = 12;

            return IdMesACerrar;
        }

        /// <summary>
        /// Obtiene el nombre del mes a cerrar
        /// </summary>
        /// <returns></returns>
        public static string GetNombreMesACerrar()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdMes = GetIdMesACerrar();

            return db.Mes.Where(item => item.Pk_IdMes == IdMes).Select(item => item.Descripcion).First();
        }

        /// <summary>
        /// Obtiene el Id del año presupuesal que está actualemente seleccionado
        /// </summary>
        /// <returns></returns>
        public static int GetCurrent_IdAnioPresupuestal()
        {
            return Convert.ToInt32(HttpContext.Current.Session["IdAnio"]);
        }

        /// <summary>
        /// Obtiene el IdSession del usuario logeado
        /// </summary>
        /// <returns></returns>
        public static string GetCurrent_IdSession()
        {
            if (HttpContext.Current.Session["DataUsuario"] == null)
                return "";

            return ((UserData)(HttpContext.Current.Session["DataUsuario"])).IdSession;
        }

        /// <summary>
        /// Obtiene el rol del usuario logeado
        /// </summary>
        /// <returns></returns>
        public static string GetCurrent_RolUsuario()
        {
            return ((UserData)(HttpContext.Current.Session["DataUsuario"])).RolesUsuario[0].ToString();
        }

        /// <summary>
        /// Obtiene el Id del Estado al cual está asociado el usuario logeado
        /// </summary>
        /// <returns></returns>
        public static int GetCurrent_IdEstado()
        {
            return ((UserData)(HttpContext.Current.Session["DataUsuario"])).FK_IdEstado__SIS;
        }

        /// <summary>
        /// Obtiene el Id de la Unidad Responsable al cual está asociado el usuario logeado
        /// </summary>
        /// <returns></returns>
        public static int GetCurrent_IdUnidadResponsable()
        {
            return ((UserData)(HttpContext.Current.Session["DataUsuario"])).FK_IdUnidadResponsable__UE;
        }

        /// <summary>
        /// Obtiene el Id de la Unidad Ejecutora al cual está asociado el usuario logeado
        /// </summary>
        /// <returns></returns>
        public static int GetCurrent_IdUnidadEjecutora()
        {
            return ((UserData)(HttpContext.Current.Session["DataUsuario"])).FK_IdUnidadEjecutora__UE;
        }

        /// <summary>
        /// Obtiene el Id de la Persona al cual está asociado el usuario logeado
        /// </summary>
        /// <returns></returns>
        public static int GetCurrent_IdPersona()
        {
            return ((UserData)(HttpContext.Current.Session["DataUsuario"])).FK_IdPersona__SIS;
        }

        /// <summary>
        /// Obtiene el UserName al cual está asociado el usuario logeado
        /// </summary>
        /// <returns></returns>
        public static string GetCurrent_UserName()
        {
            return ((UserData)(HttpContext.Current.Session["DataUsuario"])).UserName;
        }
        public static string GetCurrent_IdUserName()
        {
            return ((UserData)(HttpContext.Current.Session["DataUsuario"])).Id;
        }
        /// <summary>
        /// Normaliza los roles
        /// </summary>
        /// <returns></returns>
        public static string NormalizaRoles()
        {
            string rolUsuario = GetCurrent_RolUsuario();
            string RolNormalizado = rolUsuario;

            if (rolUsuario == RolesUsuarios.SUP_REGIONAL)
            {
                RolNormalizado = RolesUsuarios.ROL_REG;
            }

            if (rolUsuario == RolesUsuarios.UR_VEGETAL
                    || rolUsuario == RolesUsuarios.UR_ANIMAL
                    || rolUsuario == RolesUsuarios.UR_ACUICOLA_PESQUERA
                    || rolUsuario == RolesUsuarios.UR_INOCUIDAD
                    || rolUsuario == RolesUsuarios.UR_MOVILIZACION
                    || rolUsuario == RolesUsuarios.UR_UPV)
            {
                RolNormalizado = RolesUsuarios.ROL_AUR;
            }

            if (rolUsuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                    || rolUsuario == RolesUsuarios.IE_ANIMAL
                    || rolUsuario == RolesUsuarios.IE_VEGETAL
                    || rolUsuario == RolesUsuarios.IE_MOVILIZACION
                    || rolUsuario == RolesUsuarios.IE_INOCUIDAD
                    || rolUsuario == RolesUsuarios.IE_PROGRAMAS_U)
            {
                RolNormalizado = RolesUsuarios.ROL_AIE;
            }

            return RolNormalizado;
        }

        /// <summary>
        /// Obtiene el Id de la región de acuerdo al usuario regional logeado
        /// </summary>
        /// <returns></returns>
        public static int GetRegionByUserName()
        {
            int region = 0;

            string _userName = GetCurrent_UserName().Substring(0, 5);

            switch (_userName)
            {
                case "REG01": region = 1; break;
                case "REG02": region = 2; break;
                case "REG03": region = 3; break;
                case "REG04": region = 4; break;
                case "REG05": region = 5; break;
                case "REG06": region = 6; break;
            }

            return region;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////
        //(BEGIN) Combos que cargan su información sobre demanda
        public static object GetBienOServicioRange(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            var Fk_IdBienOServ__SIS = HttpContext.Current.Items["Fk_IdBienOServ__SIS"].ToString();

            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int IdAnio = GetCurrent_IdAnioPresupuestal();

            List<BienOServ> _datos;

            if (Fk_IdBienOServ__SIS == "")
            {
                _datos = db.BienOServs.Where(item => (item.esTransversal == false || item.esTransversal == null)
                                                    && (item.Nombre.Contains(args.Filter) || item.TipoDeBien.Nombre.Contains(args.Filter))
                                                    && item.Fk_IdAnio == IdAnio)
                                        .OrderBy(item => item.TipoDeBien.Nombre).ThenBy(item => item.Nombre).Skip(skip).Take(take).ToList();
            }
            else
            {
                int IdBienOServicio = Convert.ToInt32(Fk_IdBienOServ__SIS);

                _datos = db.BienOServs.Where(item => item.Pk_IdBienOServ == IdBienOServicio)
                                        .OrderBy(item => item.TipoDeBien.Nombre).ThenBy(item => item.Nombre).Skip(skip).Take(take).ToList();
            }

            return _datos;
        }

        public static object GetBienOServicioById(ListEditItemRequestedByValueEventArgs args)
        {
            int Id;
            if (args.Value == null || !int.TryParse(args.Value.ToString(), out Id)) return null;

            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.BienOServs.Where(item => item.Pk_IdBienOServ == Id).Take(1).ToList();
        }

        public static object GetBienOServicioTransversalRange(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            var Fk_IdBienOServ__SIS = HttpContext.Current.Items["Fk_IdBienOServ__SIS"].ToString();

            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int IdAnio = GetCurrent_IdAnioPresupuestal();

            List<BienOServ> _datos;

            if (Fk_IdBienOServ__SIS == "")
            {
                _datos = db.BienOServs.Where(item => item.esTransversal == true
                                                    && (item.Nombre.Contains(args.Filter) || item.TipoDeBien.Nombre.Contains(args.Filter))
                                                    && item.Fk_IdAnio == IdAnio)
                                        .OrderBy(item => item.TipoDeBien.Nombre).ThenBy(item => item.Nombre).Skip(skip).Take(take).ToList();
            }
            else
            {
                int IdBienOServicio = Convert.ToInt32(Fk_IdBienOServ__SIS);

                _datos = db.BienOServs.Where(item => item.Pk_IdBienOServ == IdBienOServicio)
                                        .OrderBy(item => item.TipoDeBien.Nombre).ThenBy(item => item.Nombre).Skip(skip).Take(take).ToList();
            }

            return _datos;
        }

        public static object GetBienOServicioTransversalById(ListEditItemRequestedByValueEventArgs args)
        {
            int Id;
            if (args.Value == null || !int.TryParse(args.Value.ToString(), out Id)) return null;

            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.BienOServs.Where(item => item.Pk_IdBienOServ == Id).Take(1).ToList();
        }


        public static object GetProveedorRange(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            var Fk_IdProveedor = HttpContext.Current.Items["Fk_IdProveedor"].ToString();

            DB_SENASICAEntities db = new DB_SENASICAEntities();

            List<Proveedor> _datos;

            if (Fk_IdProveedor == "")
            {
                _datos = db.Proveedors.Where(item => item.RazonSocial_Nombre.Contains(args.Filter) || item.Estado.Nombre.Contains(args.Filter))
                                .OrderBy(item => item.Estado.Nombre).ThenBy(item => item.RazonSocial_Nombre).Skip(skip).Take(take).ToList();
            }
            else
            {
                int IdProveedor = Convert.ToInt32(Fk_IdProveedor);

                _datos = db.Proveedors.Where(item => item.Pk_IdProveedor == IdProveedor)
                                .OrderBy(item => item.Estado.Nombre).ThenBy(item => item.RazonSocial_Nombre).Skip(skip).Take(take).ToList();
            }

            return _datos;
        }

        public static object GetProveedorById(ListEditItemRequestedByValueEventArgs args)
        {
            int Id;
            if (args.Value == null || !int.TryParse(args.Value.ToString(), out Id)) return null;

            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.Proveedors.Where(item => item.Pk_IdProveedor == Id).Take(1).ToList();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Regresa los parámetros necesarios para exportar un reporte en PDF o Excel
        /// </summary>
        /// <param name="isPDF"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public static object GetRouteValues(bool isPDF, Dictionary<string, string> parametros)
        {
            object _routeValues = null;
            switch (parametros["Clave"])
            {

                case ListaPantallas.SIS_BIENES_SERVICIO:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportBienesOServ", isPDF = isPDF };
                    break;

                case ListaPantallas.SIS_CAPITULOS_GASTO:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportCapitulosPartida", isPDF = isPDF };
                    break;

                case ListaPantallas.SIS_COMPONENTES:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportComponente", isPDF = isPDF };
                    break;

                case ListaPantallas.SIS_CONCEPTOS_GASTO:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportConceptosPartida", isPDF = isPDF };
                    break;

                case ListaPantallas.SIS_PARTIDAS_GASTO:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportPartida", isPDF = isPDF };
                    break;

                case ListaPantallas.SIS_ESTADOS:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportEstado", isPDF = isPDF };
                    break;

                case ListaPantallas.SIS_ESTADO_BIEN:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportEstadoBien", isPDF = isPDF };
                    break;

                case ListaPantallas.SIS_INCENTIVOS:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportIncentivo", isPDF = isPDF };
                    break;

                case ListaPantallas.SIS_MUNICIPIOS:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportMunicipio", isPDF = isPDF };
                    break;

                case ListaPantallas.SIS_TIPO_BIENES:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportTiposDeBien", isPDF = isPDF };
                    break;

                case ListaPantallas.SIS_PERSONAL_SISTEMA:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportUsers", isPDF = isPDF };
                    break;

                case ListaPantallas.SIS_PROGRAMAS:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportPrograma", isPDF = isPDF };
                    break;

                case ListaPantallas.SIS_REGISTRO_PROVEEDORES:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportProveedor", isPDF = isPDF };
                    break;

                case ListaPantallas.SIS_PROGRAMA_AUTORIZADO:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportProyectoAutorizado", isPDF = isPDF };
                    break;

                case ListaPantallas.SIS_PRESUPUESTO_PROYECTO:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportProyectoPresupuesto", isPDF = isPDF, estado_param = parametros["estado_param"] };
                    break;

                case ListaPantallas.SIS_SUBCOMPONENTES:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportSubComponente", isPDF = isPDF };
                    break;

                case ListaPantallas.SIS_TIPO_ACTIVIDAD:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportTipoActividad", isPDF = isPDF };
                    break;

                case ListaPantallas.SIS_TIPO_ACCION:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportTipoAccion", isPDF = isPDF };
                    break;

                case ListaPantallas.SIS_TIPO_INSTALACION:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportTipoDeInstalacion", isPDF = isPDF };
                    break;


                case ListaPantallas.SIS_TIPO_RECURSO:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportTiposDeRecurso", isPDF = isPDF };
                    break;


                case ListaPantallas.SIS_TIPO_INSTANCIA_EJECUTORA:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportTiposDeUnidad", isPDF = isPDF };
                    break;

                case ListaPantallas.SIS_UNIDAD_MEDIDA:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportUnidadesDeMedida", isPDF = isPDF };
                    break;

                case ListaPantallas.SIS_PERSONAL_INSTANCIA_EJECUTORA:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportPersonal", isPDF = isPDF, IdUnidadEjecutora = parametros["IdUnidadEjecutora"] };
                    break;

                case ListaPantallas.UE_ACTIVIDADES_ACCION:
                    _routeValues = new
                    {
                        Controller = "ExportExcelPDF",
                        Action = "exportAccionXCampania",
                        isPDF = isPDF,
                        IdCampania = parametros["IdCampania"],
                        _routeValues = new { Controller = "ExportExcelPDF", Action = "exportAccionXCampanias", isPDF = isPDF, IdCampania = parametros["IdCampania"] }
                    };
                    break;


                case ListaPantallas.UE_PROGRAMAS_TRABAJO_SV:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportCampania", isPDF = isPDF, IdCampania = parametros["IdCampania"] };

                    break;


                case ListaPantallas.UE_PROGRAMAS_TRABAJO_SA:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportCampaniaSA", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;

                case ListaPantallas.UE_PADRON_UNIDADES_PRODUCCION:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportProductores", isPDF = isPDF };
                    break;

                case ListaPantallas.UE_INFORME_DE_ADQUISICIONES:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportProgramaAnual", isPDF = isPDF, Estado = parametros["Estado"] };
                    break;

                case ListaPantallas.UE_INFORME_AVANCES:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportActividadesRealizadas", isPDF = isPDF, Estado = parametros["Estado"] };
                    break;


                case ListaPantallas.UE_INSTANCIA_EJECUTORA:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportUnidadEjecutora", isPDF = isPDF };
                    break;

                case ListaPantallas.UE_UNIDAD_RESPONSABLE:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportUnidadesResponsables", isPDF = isPDF };
                    break;

                case ListaPantallas.UE_VIGENCIA:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportVigencia", isPDF = isPDF, IdUnidadEjecutora = parametros["IdUnidadEjecutora"] };
                    break;

                case ListaPantallas.UE_PADRONES_INSTALACION:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportInstalaciones", isPDF = isPDF, IdUnidadEjecutora = parametros["IdUnidadEjecutora"] };
                    break;

                case ListaPantallas.UE_INVENTARIO:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportInventario", isPDF = isPDF, IdUnidadEjecutora = parametros["IdUnidadEjecutora"] };
                    break;

                case ListaPantallas.UE_ACTIVIDAD_ACCION:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportActividadXAccion", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;

                case ListaPantallas.UE_ACCIONES_CAMPANIA:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportActividadXAccions", isPDF = isPDF, IdAccionXCamp = parametros["IdAccionXCamp"] };
                    break;

                case ListaPantallas.UE_ATENDIDO_SA:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportSAAtendido", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;

                case ListaPantallas.UE_ATENDIDO_SV:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportSVAtendido", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;

                case ListaPantallas.UE_IMPORTANCIA_SA:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportSAImportanciaEconomica", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;

                case ListaPantallas.UE_IMPORTANCIA_SA_ENFERMEDAD:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportSAImportanciaEnfermedad", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;

                case ListaPantallas.UE_IMPORTANCIA_SV:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportSVImportanciaCultivo", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;


                case ListaPantallas.UE_IMPORTANCIA_SV_PROGRAMA:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportSVImportanciaPlaga", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;

                case ListaPantallas.UE_NECESIDADES_POR_ACCION:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportNecesidadXAccion", isPDF = isPDF, IdAccionXCamp = parametros["IdAccionXCamp"] };
                    break;

                case ListaPantallas.UE_GASTOS_FIJOS_OPERACION_INSTANCIA_EJECUTORA:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportGastosRelCampania", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;


                case ListaPantallas.UE_STATUS_CAMPANIA:
                    _routeValues = new
                    {
                        Controller = "ExportExcelPDF",
                        Action = "exportStatusCampaniaKardex",
                        isPDF = isPDF,
                        IdCampania = parametros["IdCampania"],
                        _routeValues = new { Controller = "ExportExcelPDF", Action = "exportStatusCampaniaSAKardex", isPDF = isPDF, IdCampania = parametros["IdCampania"] }
                    };
                    break;

                case ListaPantallas.UE_PEFXESTADO:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportPEFXEstado", isPDF = isPDF };
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportPEFXEstado", isPDF = isPDF };
                    break;


                case ListaPantallas.PRES_PRESUPUESTOXDIRECCIONGENERAL:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportPresupuestoXDireccionGeneral", isPDF = isPDF };
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportPresupuestoXDireccionGeneral", isPDF = isPDF };
                    break;

                case ListaPantallas.UE_PROGRAMA_ANUAL_ADQUISICIONES:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportProgramaAnualAdq_", isPDF = isPDF };
                    break;

                case ListaPantallas.UE_PROGRAMA_ANUAL_ADQUISICIONES_U:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportProgramaAnualAdq_U", isPDF = isPDF };
                    break;

                #region Export InoSA
                case ListaPantallas.UE_PROGRAMAS_TRABAJO_INO_SA:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportCampaniaInoSA", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;

                case ListaPantallas.UE_ATENDIDO_INO_SA:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportInoSAAtendido", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;


                case ListaPantallas.UE_UNIDADES_CERTIFICAR_INO_SA:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportInoSAUnidadesACertificar", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;

                case ListaPantallas.UE_IMPORTANCIA_INO_SA:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportInoSAImportanciaCampania", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;
                #endregion

                #region Export InoSAC

                case ListaPantallas.UE_PROGRAMAS_TRABAJO_INO_SAP:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportCampaniaInoSAP", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;

                case ListaPantallas.UE_ATENDIDO_INO_SAP:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportInoSAPAtendido", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;


                case ListaPantallas.UE_UNIDADES_CERTIFICAR_INO_SAP:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportInoSAPUnidadesACertificar", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;

                case ListaPantallas.UE_IMPORTANCIA_INO_SAP:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportInoSAPImportanciaCampania", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;
                #endregion

                #region Export InoSV

                case ListaPantallas.UE_PROGRAMAS_TRABAJO_INO_SV:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportCampaniaInoSV", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;

                case ListaPantallas.UE_ATENDIDO_INO_SV:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportInoSVAtendido", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;


                case ListaPantallas.UE_UNIDADES_CERTIFICAR_INO_SV:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportInoSVUnidadesACertificar", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;

                case ListaPantallas.UE_IMPORTANCIA_INO_SV:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportInoSVImportanciaCampania", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;
                #endregion

                #region Export MOV

                case ListaPantallas.UE_PROGRAMAS_TRABAJO_MOV:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportCampaniaMOV", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;

                case ListaPantallas.UE_IMPORTANCIA_MOV:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportMOVImportancia", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;


                case ListaPantallas.UE_ATENDIDO_MOV:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportMOVAtendido", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;

                case ListaPantallas.UE_IMPORTANCIA_MOV_PROGRAMA:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportMOVImportanciaPrograma", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;
                #endregion

                #region Export SAC

                case ListaPantallas.UE_PROGRAMAS_TRABAJO_SAP:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportCampaniaSAP", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;

                case ListaPantallas.UE_IMPORTANCIA_SAP:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportSAPImportancia", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;


                case ListaPantallas.UE_ATENDIDO_SAP:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportSAPAtendido", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;

                case ListaPantallas.UE_IMPORTANCIA_SAP_ENFERMEDAD:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportSAPImportanciaEnfermedad", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;
                #endregion

                #region Ministraciones
                case ListaPantallas.UE_MINISTRACION_COMITE:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportMinistracion", isPDF = isPDF };
                    break;

                case ListaPantallas.UE_MINISTRACION_DETALLE:
                    _routeValues = new
                    {
                        Controller = "ExportExcelPDF",
                        Action = "exportMinistracion_Detalle",
                        isPDF = isPDF,
                        IdMinistracionesXComite = parametros["IdMinistracionesXComite"],
                        _routeValues = new { Controller = "ExportExcelPDF", Action = "exportMinistracion_Detalle", isPDF = isPDF, IdMinistracionesXComite = parametros["IdMinistracionesXComite"] }
                    };
                    break;
                #endregion

                case ListaPantallas.UE_PROGRAMAS_TRABAJO:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportCampania_N", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;

                case ListaPantallas.UE_PROGRAMAS_TRABAJO_U:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportCampania_U", isPDF = isPDF, IdCampania = parametros["IdCampania"] };
                    break;

                case ListaPantallas.UE_CIERRE_MENSUAL:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportCierreMensual", isPDF = isPDF };

                    break;

                case ListaPantallas.UE_CIERRE_MENSUAL_SOLAPERTURA:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportCierreMensual_Apertura", isPDF = isPDF };

                    break;

                #region Reprogramación Presupuesto Federal
                case ListaPantallas.UE_REPRO_PRES_FED:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportReprogramacionPresFed", isPDF = isPDF };
                    break;

                case ListaPantallas.UE_REPRO_PRES_FED_ESTADO:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportReprogramacionPresFedByEstado", isPDF = isPDF, IdEstado = parametros["IdEstado"] };
                    break;
                    #endregion

                #region TesoFe
                case ListaPantallas.UE_TESOFE:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportTesoFe", isPDF = isPDF };
                    break;

                case ListaPantallas.UE_TESOFE_DETALLE:
                    _routeValues = new
                    {
                        Controller = "ExportExcelPDF",
                        Action = "exportTesoFe_Detalle",
                        isPDF = isPDF,
                        IdTesofe = parametros["IdTesofe"],
                        _routeValues = new { Controller = "ExportExcelPDF", Action = "exportTesoFe_Detalle", isPDF = isPDF, IdTesofe = parametros["IdTesofe"] }
                    };
                    break;
                #endregion

                case ListaPantallas.UE_ACTA_COMISION:
                    _routeValues = new { Controller = "ExportExcelPDF", Action = "exportActaComision", isPDF = isPDF };

                    break;
            }
            return _routeValues;
        }

        /// <summary>
        /// Regresa el Id de la Dirección General de Salud Animal
        /// de la tabla UE.UnidadResponsable
        /// </summary>
        /// <returns></returns>
        public static int getIdDGSA()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var _query = db.UnidadResponsables.Where(u => u.Siglas == ConstantesGlobales.DGSA).Select(u => u.Pk_IdUnidadResponsable);

            return _query.Count() == 0 ? -1 : _query.First();
        }

        /// <summary>
        /// Regresa el Id de la Dirección General de Salud Animal
        /// de la tabla UE.UnidadResponsable
        /// </summary>
        /// <returns></returns>
        public static int getIdDGSV()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var _query = db.UnidadResponsables.Where(u => u.Siglas == ConstantesGlobales.DGSV).Select(u => u.Pk_IdUnidadResponsable);

            return _query.Count() == 0 ? -1 : _query.First();
        }

        /// <summary>
        /// Regresa el Id de la Dirección General de Salud Animal
        /// de la tabla UE.UnidadResponsable
        /// </summary>
        /// <returns></returns>
        public static int getIdDGIAAP()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var _query = db.UnidadResponsables.Where(u => u.Siglas == ConstantesGlobales.DGIAAP).Select(u => u.Pk_IdUnidadResponsable);

            return _query.Count() == 0 ? -1 : _query.First();
        }

        /// <summary>
        /// Regresa el Id de la Dirección General de Salud Animal
        /// de la tabla UE.UnidadResponsable
        /// </summary>
        /// <returns></returns>
        public static int getIdDGIF()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var _query = db.UnidadResponsables.Where(u => u.Siglas == ConstantesGlobales.DGIF).Select(u => u.Pk_IdUnidadResponsable);

            return _query.Count() == 0 ? -1 : _query.First();
        }
        /// <summary>
        /// Regresa el Id de la Dirección General de la Unidad de Promoción y Enlace
        /// de la tabla UE.UnidadResponsable
        /// </summary>
        /// <returns></returns>
        public static int getIdDGUPV()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var _query = db.UnidadResponsables.Where(u => u.Siglas == ConstantesGlobales.DGUPV).Select(u => u.Pk_IdUnidadResponsable);

            return _query.Count() == 0 ? -1 : _query.First();
        }

        /// <summary>
        /// Obtiene el Id de UnidadResponsable de acuerdo al usuario logeado
        /// </summary>
        /// <returns></returns>
        public static int getIdUnidadResponsable(string rolUsuario)
        {
            int IdUnidadResponsable = -1;

            switch (rolUsuario)
            {
                case RolesUsuarios.UR_ANIMAL:
                    IdUnidadResponsable = getIdDGSA();
                    break;
                case RolesUsuarios.UR_VEGETAL:
                    IdUnidadResponsable = getIdDGSV();
                    break;
                case RolesUsuarios.UR_INOCUIDAD:
                    IdUnidadResponsable = getIdDGIAAP();
                    break;
                case RolesUsuarios.UR_MOVILIZACION:
                    IdUnidadResponsable = getIdDGIF();
                    break;
                case RolesUsuarios.UR_UPV:
                    IdUnidadResponsable = getIdDGUPV();
                    break;
            }

            return IdUnidadResponsable;
        }

        /// <summary>
        /// Devuelve a partir del Id, el año presupuestal que el usuario está trabajando en el sistema
        /// </summary>
        /// <param name="IdAnio"></param>
        /// <returns></returns>
        public static string AnioPresupuestal(int IdAnio)
        {
            string anio = "";

            switch (IdAnio)
            {
                case 2: anio = "2016"; break;
                case 3: anio = "2017"; break;
                case 5: anio = "2018"; break;
            }
            return anio;
        }
        /// <summary>
        /// De acuerdo al último Status de la Campaña, comprueba que no está en Reprogramación
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static string GetStatusCampaniaSin_Repro(int? IdCampania)
        {
            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            var Estado = "";
            switch (StatusActual)
            {
                case ConstantesGlobales.SIN_ESTADO:
                    Estado = ConstantesGlobales.SIN_ESTADO;
                    break;
                case ConstantesGlobales.SOL_VALIDACION:
                    Estado = ConstantesGlobales.SOL_VALIDACION;
                    break;
                case ConstantesGlobales.SOL_VISTO_BUENO:
                    Estado = ConstantesGlobales.SOL_VISTO_BUENO;
                    break;
                case ConstantesGlobales.CON_OBSERVACIONES:
                    Estado = ConstantesGlobales.CON_OBSERVACIONES;
                    break;
                case ConstantesGlobales.VALIDADO:
                    Estado = ConstantesGlobales.VALIDADO;
                    break;
                case ConstantesGlobales.VISTO_BUENO:
                    Estado = ConstantesGlobales.VISTO_BUENO;
                    break;
                case ConstantesGlobales.EN_EJECUCION:
                    Estado = ConstantesGlobales.EN_EJECUCION;
                    break;
                case ConstantesGlobales.SOLICITUD_MODIFICACION:
                    Estado = ConstantesGlobales.SOLICITUD_MODIFICACION;
                    break;
                case ConstantesGlobales.MOD_VALIDAS:
                    Estado = ConstantesGlobales.MOD_VALIDAS;
                    break;
            }
            return Estado;
        }
        /// <summary>
        /// De acuerdo al último Status de la Campaña, comprueba que la campaña está en el proceso de Reprogramación
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public static string GetStatusCampaniaEn_Repro(int? IdCampania)
        {
            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            var Estado = "";
            switch (StatusActual)
            {
                case ConstantesGlobales.AUTORIZA_MODIFICACIONES:
                    Estado = ConstantesGlobales.AUTORIZA_MODIFICACIONES;
                    break;
                case ConstantesGlobales.SOL_VALIDACION_MODIF:
                    Estado = ConstantesGlobales.SOL_VALIDACION_MODIF;
                    break;
                case ConstantesGlobales.CON_OBSERVACIONES_MOD:
                    Estado = ConstantesGlobales.CON_OBSERVACIONES_MOD;
                    break;
                case ConstantesGlobales.TERMINADO:
                    Estado = ConstantesGlobales.TERMINADO;
                    break;
            }
            return Estado;
        }

        /// <summary>
        /// Muestra el Botón Cancelar solo para los comités
        /// </summary>
        /// <returns></returns>
        public static string CancelarReproByRol()
        {
            string rolUsuario = GetCurrent_RolUsuario();
            string Rol = "";

            switch (rolUsuario)
            {
                case RolesUsuarios.IE_ACUICOLA_PESQUERA:
                    Rol = RolesUsuarios.IE_ACUICOLA_PESQUERA;
                    break;
                case RolesUsuarios.IE_ANIMAL:
                    Rol = RolesUsuarios.IE_ANIMAL;
                    break;
                case RolesUsuarios.IE_VEGETAL:
                    Rol = RolesUsuarios.IE_VEGETAL;
                    break;
                case RolesUsuarios.IE_MOVILIZACION:
                    Rol = RolesUsuarios.IE_MOVILIZACION;
                    break;
                case RolesUsuarios.IE_INOCUIDAD:
                    Rol = RolesUsuarios.IE_INOCUIDAD;
                    break;
                case RolesUsuarios.SUP_ESTATAL:
                    Rol = RolesUsuarios.SUP_ESTATAL;
                    break;
            }
            return Rol;
        }

        /// <summary>
        /// Convierte los meses en cadena(Nombre_Mes)
        /// </summary>
        /// <param name="Mes"></param>
        /// <returns></returns>
        public static string Get_NombreMeses(int Mes)
        {
            string Nombre_Mes = "";

            switch (Mes)
            {
                case 1:
                    Nombre_Mes = "ENERO";
                    break;
                case 2:
                    Nombre_Mes = "FEBRERO";
                    break;
                case 3:
                    Nombre_Mes = "MARZO";
                    break;
                case 4:
                    Nombre_Mes = "ABRIL";
                    break;
                case 5:
                    Nombre_Mes = "MAYO";
                    break;
                case 6:
                    Nombre_Mes = "JUNIO";
                    break;
                case 7:
                    Nombre_Mes = "JULIO";
                    break;
                case 8:
                    Nombre_Mes = "AGOSTO";
                    break;
                case 9:
                    Nombre_Mes = "SEPTIEMBRE";
                    break;
                case 10:
                    Nombre_Mes = "OCTUBRE";
                    break;
                case 11:
                    Nombre_Mes = "NOVIEMBRE";
                    break;
                case 12:
                    Nombre_Mes = "DICIEMBRE";
                    break;
            }
            return Nombre_Mes;
        }

        /// <summary>
        /// Asigna permiso para los usuarios que pueden subir Informes Trimestrales, sin importar en que Status se encuentre la Campaña
        /// </summary>
        /// <returns></returns>
        public static bool GetPermisoInfoTrimestral()
        {
            string RolUsuario = GetCurrent_RolUsuario();
            string controlador = HttpContext.Current.Session["CurrentController"].ToString();
            bool Escritura;
            
            switch (RolUsuario)
            {
                case RolesUsuarios.IE_ANIMAL:
                case RolesUsuarios.IE_VEGETAL:
                case RolesUsuarios.IE_INOCUIDAD:
                case RolesUsuarios.IE_MOVILIZACION:
                case RolesUsuarios.IE_ACUICOLA_PESQUERA:
                case RolesUsuarios.IE_PROGRAMAS_U:
                case RolesUsuarios.PUESTO_COOR_PROYECTO:
                case RolesUsuarios.COORDINADOR_CAMPANIA:
                case RolesUsuarios.PUESTO_GERENTE:
                case RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS:
                case RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA:
                case RolesUsuarios.SUPERUSUARIO:
                { 
                    Escritura = true;
                    return Escritura;
                }
                case RolesUsuarios.SUP_ESTATAL:
                {
                    if (controlador == "CampaniaU" || controlador == "CampaniaN")
                    {
                        Escritura = false;
                    }
                    else
                    {
                        Escritura = true;
                    }
                    return Escritura;
                }
                default:
                return false;
            }
        }
    }
}