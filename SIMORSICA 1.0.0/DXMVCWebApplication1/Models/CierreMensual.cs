//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DXMVCWebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CierreMensual
    {
        public CierreMensual()
        {
            this.CierreMensualSolicitudAperturas = new HashSet<CierreMensualSolicitudApertura>();
        }
    
        public int Pk_IdCierreMensual { get; set; }
        public int Fk_IdCampania { get; set; }
        public int Fk_IdMes { get; set; }
        public string Comentarios { get; set; }
        public bool esCerrado { get; set; }
        public string NombreRepCierre { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<System.DateTime> FechaCierre { get; set; }
        public bool esSolicitadoParaApertura { get; set; }
        public string MotivosApertura { get; set; }
        public string RespuestaApertura { get; set; }
        public string NombreCampania { get; set; }
    
        public virtual Me Me { get; set; }
        public virtual ICollection<CierreMensualSolicitudApertura> CierreMensualSolicitudAperturas { get; set; }
        public virtual Campania Campania { get; set; }
    }
}