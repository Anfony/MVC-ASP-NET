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
    
    public partial class UnidadesCertificarInoSAC
    {
        public int Pk_IdUnidadesCertificarInoSAC { get; set; }
        public int Fk_IdCampania { get; set; }
        public int FK_IdTipoUnidad_UCAC { get; set; }
        public string Especie { get; set; }
        public int AntProgramado { get; set; }
        public int AntRealizado { get; set; }
        public double AntPorcentajeCumplimiento { get; set; }
        public int ActProgramado { get; set; }
        public int ActRealizado { get; set; }
        public double ActPorcentajeCumplimiento { get; set; }
    
        public virtual TipoUnidad_UCAC TipoUnidad_UCAC { get; set; }
        public virtual Campania Campania { get; set; }
    }
}
