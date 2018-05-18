using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Web;

namespace DXMVCWebApplication1.Negocio
{
    /// <summary>
    /// Obtiene los permisos de la campaña (Lectura/Esrcitura) 
    /// de acuerdo al Status en el que se encuentra y el usuario logeado
    /// </summary>
    public class PermisosFlujoCampania
    {
        private string StatusActual;
        private string RolUsuario;

        public PermisosFlujoCampania(int? IdCampania)
        {
            if(IdCampania == null) StatusActual = "";
            else
            {
                StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
                RolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();
            }
        }

        public Dictionary<string, bool> ObtienePermisos()
        {
            Dictionary<string, bool> _permisos = null;

            _permisos = GetPermisosEST();

            if (_permisos != null)
                return _permisos;

            _permisos = new Dictionary<string, bool>();
            _permisos.Add("Lectura", true);

            switch (StatusActual)
            {
                case ConstantesGlobales.SIN_ESTADO:
                    _permisos.Add("Escritura", GetPermisoEscritura_SinEstado());
                    break;
                case ConstantesGlobales.SOL_VALIDACION:
                    _permisos.Add("Escritura", GetPermisoEscritura_SolValidacion());
                    break;
                case ConstantesGlobales.CON_OBSERVACIONES:
                    _permisos.Add("Escritura", GetPermisoEscritura_ConObservaciones());
                    break;
                case ConstantesGlobales.VALIDADO:
                    _permisos.Add("Escritura", GetPermisoEscritura_Validado());
                    break;
                case ConstantesGlobales.EN_EJECUCION:
                    _permisos.Add("Escritura", GetPermisoEscritura_EnEjecucion());
                    break;
                case ConstantesGlobales.SOLICITUD_MODIFICACION:
                    _permisos.Add("Escritura", GetPermisoEscritura_SolModificacion());
                    break;
                case ConstantesGlobales.AUTORIZA_MODIFICACIONES:
                    _permisos.Add("Escritura", GetPermisoEscritura_AutorizaModificaciones());
                    break;
                case ConstantesGlobales.SOL_VALIDACION_MODIF:
                    _permisos.Add("Escritura", GetPermisoEscritura_SolValidacionModificaciones());
                    break;
                case ConstantesGlobales.CON_OBSERVACIONES_MOD:
                    _permisos.Add("Escritura", GetPermisoEscritura_ConObservacionesModificaciones());
                    break;
                case ConstantesGlobales.MOD_VALIDAS:
                    _permisos.Add("Escritura", GetPermisoEscritura_ModificacionesValidadas());
                    break;
                case ConstantesGlobales.TERMINADO:
                    _permisos.Add("Escritura", GetPermisoEscritura_Terminado());
                    break;
                case ConstantesGlobales.SOL_VISTO_BUENO:
                    _permisos.Add("Escritura", GetPermisoEscritura_SolVistoBueno());
                    break;
                case ConstantesGlobales.VISTO_BUENO:
                    _permisos.Add("Escritura", GetPermisoEscritura_VistoBueno());
                    break;
                default:
                    _permisos.Add("Escritura", false);
                    break;
            }

            return _permisos;
        }

        private Dictionary<string, bool> GetPermisosEST()
        {
            Dictionary<string, bool> _permisos = new Dictionary<string, bool>(); ;
            string controlador = HttpContext.Current.Session["CurrentController"].ToString();

            if (RolUsuario == RolesUsuarios.SUP_ESTATAL
                && (controlador == "CampaniaU" || controlador == "CampaniaN"))
            {
                _permisos.Add("Lectura", true);
                _permisos.Add("Escritura", false);
            }
            else
            {
                _permisos = null;
            }

            return _permisos;
        }

        private bool GetPermisoEscritura_SinEstado()
        {
            bool Escritura;

            if (RolUsuario == RolesUsuarios.IE_ANIMAL
                || RolUsuario == RolesUsuarios.IE_VEGETAL
                || RolUsuario == RolesUsuarios.IE_INOCUIDAD
                || RolUsuario == RolesUsuarios.IE_MOVILIZACION
                || RolUsuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || RolUsuario == RolesUsuarios.IE_PROGRAMAS_U
                || RolUsuario == RolesUsuarios.PUESTO_COOR_PROYECTO
                || RolUsuario == RolesUsuarios.COORDINADOR_CAMPANIA
                || RolUsuario == RolesUsuarios.PUESTO_COOR_REGIONAL
                || RolUsuario == RolesUsuarios.PUESTO_GERENTE
                || RolUsuario == RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                || RolUsuario == RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                || RolUsuario == RolesUsuarios.SUP_ESTATAL
                || RolUsuario == RolesUsuarios.REPRESENTANTE_ESTATAL)
            {
                Escritura = true;
            }
            else
            {
                Escritura = false;
            }

            return Escritura;
        }

        private bool GetPermisoEscritura_SolValidacion() { return false; }
        private bool GetPermisoEscritura_SolVistoBueno() { return false; }

        private bool GetPermisoEscritura_ConObservaciones()
        {
            bool Escritura;

            if (RolUsuario == RolesUsuarios.IE_ANIMAL
                || RolUsuario == RolesUsuarios.IE_VEGETAL
                || RolUsuario == RolesUsuarios.IE_INOCUIDAD
                || RolUsuario == RolesUsuarios.IE_MOVILIZACION
                || RolUsuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || RolUsuario == RolesUsuarios.IE_PROGRAMAS_U
                || RolUsuario == RolesUsuarios.PUESTO_COOR_PROYECTO
                || RolUsuario == RolesUsuarios.COORDINADOR_CAMPANIA
                || RolUsuario == RolesUsuarios.PUESTO_COOR_REGIONAL
                || RolUsuario == RolesUsuarios.PUESTO_GERENTE
                || RolUsuario == RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                || RolUsuario == RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                || RolUsuario == RolesUsuarios.SUP_ESTATAL
                || RolUsuario == RolesUsuarios.REPRESENTANTE_ESTATAL)
            {
                Escritura = true;
            }
            else
            {
                Escritura = false;
            }

            return Escritura;
        }

        private bool GetPermisoEscritura_Validado() { return false; }
        private bool GetPermisoEscritura_VistoBueno() { return false; }

        private bool GetPermisoEscritura_EnEjecucion() { return false; }

        private bool GetPermisoEscritura_SolModificacion() { return false; }

        private bool GetPermisoEscritura_AutorizaModificaciones()
        {
            bool Escritura;

            if (RolUsuario == RolesUsuarios.IE_ANIMAL
                || RolUsuario == RolesUsuarios.IE_VEGETAL
                || RolUsuario == RolesUsuarios.IE_INOCUIDAD
                || RolUsuario == RolesUsuarios.IE_MOVILIZACION
                || RolUsuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || RolUsuario == RolesUsuarios.IE_PROGRAMAS_U
                || RolUsuario == RolesUsuarios.PUESTO_COOR_PROYECTO
                || RolUsuario == RolesUsuarios.COORDINADOR_CAMPANIA
                || RolUsuario == RolesUsuarios.PUESTO_COOR_REGIONAL
                || RolUsuario == RolesUsuarios.PUESTO_GERENTE
                || RolUsuario == RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                || RolUsuario == RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                || RolUsuario == RolesUsuarios.SUP_ESTATAL
                || RolUsuario == RolesUsuarios.REPRESENTANTE_ESTATAL)
            {
                Escritura = true;
            }
            else
            {
                Escritura = false;
            }

            return Escritura;
        }

        private bool GetPermisoEscritura_SolValidacionModificaciones() { return false; }

        private bool GetPermisoEscritura_ConObservacionesModificaciones()
        {
            bool Escritura;

            if (RolUsuario == RolesUsuarios.IE_ANIMAL
                || RolUsuario == RolesUsuarios.IE_VEGETAL
                || RolUsuario == RolesUsuarios.IE_INOCUIDAD
                || RolUsuario == RolesUsuarios.IE_MOVILIZACION
                || RolUsuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || RolUsuario == RolesUsuarios.IE_PROGRAMAS_U
                || RolUsuario == RolesUsuarios.PUESTO_COOR_PROYECTO
                || RolUsuario == RolesUsuarios.COORDINADOR_CAMPANIA
                || RolUsuario == RolesUsuarios.PUESTO_COOR_REGIONAL
                || RolUsuario == RolesUsuarios.PUESTO_GERENTE
                || RolUsuario == RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                || RolUsuario == RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                || RolUsuario == RolesUsuarios.SUP_ESTATAL
                || RolUsuario == RolesUsuarios.REPRESENTANTE_ESTATAL)
            {
                Escritura = true;
            }
            else
            {
                Escritura = false;
            }

            return Escritura;
        }

        private bool GetPermisoEscritura_ModificacionesValidadas() { return false; }

        private bool GetPermisoEscritura_Terminado() { return false; }
    }
}