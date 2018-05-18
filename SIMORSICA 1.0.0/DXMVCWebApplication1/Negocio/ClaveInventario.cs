using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using System;
using System.Linq;

namespace DXMVCWebApplication1.Negocio
{
    /// <summary>
    /// Esta clase genera la clave de inventario correspondiente al bien que se 
    /// va a generar de acuerdo al usuario que está logeado.
    /// 
    /// Esta funcionalidad solo actua al momento de generar un bien (no al actualizar)
    /// </summary>
    public class ClaveInventario
    {
        private int IdComite;
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        private const string SALUD_ANIMAL = "01";
        private const string SANIDAD_ACUICOLA_PESQUERA = "02";
        private const string SANIDAD_VEGETAL = "03";
        private const string UCE = "04";

        public ClaveInventario()
        {
            IdComite = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();
        }

        public string GeneraClave()
        {
            return GetClaveEdo() + GetConceptoApoyo() + GetConsecutivo();
        }

        /// <summary>
        /// Genera la clave del estado en string: "01" -> Aguascalientes, "02" -> Baja California
        /// </summary>
        /// <returns></returns>
        private string GetClaveEdo()
        {
            var _query = db.UnidadEjecutoras
                            .Where(item => item.Pk_IdUnidadEjecutora == IdComite)
                            .Select(item => item.Estado.Pk_IdEstado);

            int IdEstado = _query.Count() == 0 ? -1 : _query.First();

            return IdEstado < 10 ? "0" + IdEstado.ToString() : IdEstado.ToString();
        }

        /// <summary>
        /// Regresa el concepto de apoyo de acuerdo a la Unidad responsable a la cual
        /// está asociado el comité
        /// </summary>
        /// <returns></returns>
        private string GetConceptoApoyo()
        {
            var _query = db.UnidadEjecutoras
                        .Where(item => item.Pk_IdUnidadEjecutora == IdComite)
                        .Select(item => item.Fk_IdUnidadResponsable__UE);

            int IdUR = _query.Count() == 0 ? -1 : _query.First();

            string clave_ConceptoApoyo;

            switch(IdUR)
            {
                case 6: clave_ConceptoApoyo = SANIDAD_VEGETAL; break;
                case 14: clave_ConceptoApoyo = SALUD_ANIMAL; break;
                case 19: clave_ConceptoApoyo = SANIDAD_ACUICOLA_PESQUERA; break;
                case 25: clave_ConceptoApoyo = UCE; break;
                default: clave_ConceptoApoyo = "_"; break;
            }

            return clave_ConceptoApoyo;
        }

        /// <summary>
        /// Obtiene el consecutivo que le corresponde el bien que se va a crear
        /// </summary>
        /// <returns></returns>
        private string GetConsecutivo()
        {
            var _query = db.Inventarios.Where(item => item.Fk_IdUnidadEjecutora == IdComite)
                                        .OrderByDescending(item => item.Clave)
                                        .Select(item => item.Clave);

            int consecutivo = _query.Count() == 0 ? 0 : Convert.ToInt32(_query.First().Substring(4, 5)) + 1;

            string clave_consecutivo = "";

            if (0 <= consecutivo && consecutivo <= 9) clave_consecutivo = "0000" + consecutivo.ToString();
            if (10 <= consecutivo && consecutivo <= 99) clave_consecutivo = "000" + consecutivo.ToString();
            if (100 <= consecutivo && consecutivo <= 999) clave_consecutivo = "00" + consecutivo.ToString();
            if (1000 <= consecutivo && consecutivo <= 9999) clave_consecutivo = "0" + consecutivo.ToString();
            if (10000 <= consecutivo && consecutivo <= 99999) clave_consecutivo = consecutivo.ToString();

            return clave_consecutivo;
        }
    }
}