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
    
    public partial class TESOFE
    {
        public TESOFE()
        {
            this.TesofeDetalles = new HashSet<TesofeDetalle>();
        }
    
        public int Pk_IdTesofe { get; set; }
        public int Fk_IdUnidadEjecutora { get; set; }
        public int Fk_IdTipodeRecurso { get; set; }
        public int Fk_IdTipoReembolso { get; set; }
        public int Fk_IdAnio { get; set; }
        public decimal Importe { get; set; }
        public Nullable<decimal> ImporteDetalle { get; set; }
        public string Justificacion { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Documento { get; set; }
        public System.DateTime CT_Date { get; set; }
        public string CT_User { get; set; }
    
        public virtual TipoDeRecurso TipoDeRecurso { get; set; }
        public virtual TipoReembolso TipoReembolso { get; set; }
        public virtual UnidadEjecutora UnidadEjecutora { get; set; }
        public virtual ICollection<TesofeDetalle> TesofeDetalles { get; set; }
    }
}
