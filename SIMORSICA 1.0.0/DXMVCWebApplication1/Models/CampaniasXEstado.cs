//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DXMVCWebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CampaniasXEstado
    {
        public int Pk_IdCampania { get; set; }
        public string StatusKardex { get; set; }
        public int Fk_IdAnio { get; set; }
        public int Fk_IdEstado__SIS { get; set; }
        public string Estado { get; set; }
        public string UnidadResponsable { get; set; }
        public string UnidadEjecutora { get; set; }
        public string Proyecto { get; set; }
        public Nullable<decimal> RecFed { get; set; }
        public Nullable<decimal> RecSol_Fed { get; set; }
        public Nullable<decimal> Rec_Gastado { get; set; }
        public Nullable<decimal> SaldoDisponible { get; set; }
    }
}
