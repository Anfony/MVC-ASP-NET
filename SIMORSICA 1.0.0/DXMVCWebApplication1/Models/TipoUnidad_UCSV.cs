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
    
    public partial class TipoUnidad_UCSV
    {
        public TipoUnidad_UCSV()
        {
            this.UnidadesCertificarInoSVs = new HashSet<UnidadesCertificarInoSV>();
            this.UnidadesCertificarInoSV_Repro = new HashSet<UnidadesCertificarInoSV_Repro>();
        }
    
        public int Pk_IdTipoUnidad_UCSV { get; set; }
        public string Nombre { get; set; }
    
        public virtual ICollection<UnidadesCertificarInoSV> UnidadesCertificarInoSVs { get; set; }
        public virtual ICollection<UnidadesCertificarInoSV_Repro> UnidadesCertificarInoSV_Repro { get; set; }
    }
}
