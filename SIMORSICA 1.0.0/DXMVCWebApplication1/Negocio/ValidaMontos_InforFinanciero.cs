using DXMVCWebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMVCWebApplication1.Negocio
{
    /// <summary>
    /// Valida si el avance Financiero es Menor o igual al Importe Anual * 1.2 (más un margen de tolerancia del 20 %) asignado al Bien o servicio
    /// </summary>
    public class ValidaMontos_InforFinanciero
    {
        private DB_SENASICAEntities db;

        private int Pk_IdRepAdquisicion;
        private int Fk_IdProgramaAnualAdq;
        private decimal? Importe;

        private decimal? Imp_Anual;
        private decimal? Imp_Avance;

        public ValidaMontos_InforFinanciero(int Pk_IdRepAdquisicion, int Fk_IdProgramaAnualAdq, decimal? Importe)
        {
            db = new DB_SENASICAEntities();

            this.Pk_IdRepAdquisicion = Pk_IdRepAdquisicion;
            this.Fk_IdProgramaAnualAdq = Fk_IdProgramaAnualAdq;
            this.Importe = Importe;


            var I_Anual = db.ProgramaAnualAdqs.Where(item => item.Pk_IdProgramaAnualAdq == Fk_IdProgramaAnualAdq).Select(item => item.imp_Anual);

            Imp_Anual = I_Anual.Count() == null ? 0 : Convert.ToDecimal(I_Anual.Sum()) * Convert.ToDecimal(1.2);

            if (Pk_IdRepAdquisicion > 0) // UPDATE
            {
                var I_Avances = from paa in db.ProgramaAnualAdqs
                                join _ra in db.RepAdquisicions on paa.Pk_IdProgramaAnualAdq equals _ra.Fk_IdProgramaAnualAdq
                                where paa.Pk_IdProgramaAnualAdq == Fk_IdProgramaAnualAdq
                                        && _ra.Pk_IdRepAdquisicion != Pk_IdRepAdquisicion
                                select _ra.Importe;

                Imp_Avance = I_Avances.Count() == 0 ? 0 + Convert.ToDecimal(Importe) : Convert.ToDecimal(I_Avances.Sum()) + Convert.ToDecimal(Importe);
            }
            else // NEW
            {
                var I_Avances = (from  paa in db.ProgramaAnualAdqs
                                 join _ra in db.RepAdquisicions on paa.Pk_IdProgramaAnualAdq equals _ra.Fk_IdProgramaAnualAdq 
                                where paa.Pk_IdProgramaAnualAdq == Fk_IdProgramaAnualAdq
                                select _ra.Importe);

                Imp_Avance = I_Avances.Count() == 0 ? 0 + Convert.ToDecimal(Importe) : Convert.ToDecimal(I_Avances.Sum()) + Convert.ToDecimal(Importe);
            }
        }
        public string ValidaImporte()
        {
            string Novalido;
            if (Imp_Avance <= Imp_Anual)
            {
                Novalido = "Valido";
            }
            else
            {
                Novalido = "NoValido";
            }
            return Novalido;
        }
    }

    /// <summary>
    /// Valida si el avance Físico es Menor o igual a la Meta Anual asignado de la Actividad Asignada
    /// /// ELIMINAR ESTA VALIZACION, YA QUE NO SE ESTÁ UTILIZANDO, SI SE UTILIZÓ EN EL CATÁLOGO DE iNFORME DE ACTIVIDADES, NO FUNCIONA PORQUE EL USUARIO SI PUEDE REGISTRAR METAS ANUALES, MAYOR A LO PROGRAMADO
    /// </summary>
    public class ValidaMontos_InforFisicos
    {
        private DB_SENASICAEntities db;

        private int Pk_IdRepActividad;
        private int Fk_IdActividad;
        private decimal? CantidadMetas;

        private decimal? Cantidad_Anual;
        private decimal? Cantidad_Avance;

        public ValidaMontos_InforFisicos(int Pk_IdRepActividad, int Fk_IdActividad, decimal? CantidadMetas)
        {
            db = new DB_SENASICAEntities();

            this.Pk_IdRepActividad = Pk_IdRepActividad;
            this.Fk_IdActividad = Fk_IdActividad;
            this.CantidadMetas = CantidadMetas;


            var C_Anual = db.Actividads.Where(item => item.Pk_IdActividad == Fk_IdActividad).Select(item => item.Asignado_Anual);

            Cantidad_Anual = C_Anual.Count() == null ? 0 : Convert.ToDecimal(C_Anual.Sum());

            if (Pk_IdRepActividad > 0) // UPDATE
            {
                var C_Avances = from a in db.Actividads
                                join _ra in db.RepActividads on a.Pk_IdActividad equals _ra.Fk_IdActividad
                                where a.Pk_IdActividad == Fk_IdActividad
                                        && _ra.Pk_IdRepActividad != Pk_IdRepActividad
                                select _ra.CantidadMetas;

                Cantidad_Avance = C_Avances.Count() == 0 ? 0 + Convert.ToDecimal(CantidadMetas) : Convert.ToDecimal(C_Avances.Sum()) + Convert.ToDecimal(CantidadMetas);
            }
            else // NEW
            {
                var C_Avances = from a in db.Actividads
                                join _ra in db.RepActividads on a.Pk_IdActividad equals _ra.Fk_IdActividad
                                where a.Pk_IdActividad == Fk_IdActividad
                                select _ra.CantidadMetas;

                Cantidad_Avance = C_Avances.Count() == 0 ? 0 + Convert.ToDecimal(CantidadMetas) : Convert.ToDecimal(C_Avances.Sum()) + Convert.ToDecimal(CantidadMetas);
            }
        }
        public string ValidaMetaAnual()
        {
            /// ELIMINAR ESTA VALIZACION, YA QUE NO SE ESTÁ UTILIZANDO, SI SE UTILIZÓ EN EL CATÁLOGO DE iNFORME DE ACTIVIDADES, NO FUNCIONA PORQUE EL USUARIO SI PUEDE REGISTRAR METAS ANUALES, MAYOR A LO PROGRAMADO
            string Novalido;
            if (Cantidad_Avance <= Cantidad_Anual)
            {
                Novalido = "Valido";
            }
            else
            {
                Novalido = "NoValido";
            }
            return Novalido;
        }
    }
}