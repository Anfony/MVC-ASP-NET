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
    
    public partial class SVAtendido
    {
        public int Pk_IdSV_Atendido { get; set; }
        public int Fk_IdCampania { get; set; }
        public string Cultivo { get; set; }
        public Nullable<double> Hectareas { get; set; }
        public Nullable<int> Fk_IdMunicipio { get; set; }
        public Nullable<int> NumProductores { get; set; }
        public Nullable<int> NumSitiosAAtender { get; set; }
        public Nullable<double> NivelDeInfestacionProm { get; set; }
        public Nullable<double> NivelDeInfestObjetivo { get; set; }
        public Nullable<double> NivelDeInfestAtendido { get; set; }
        public Nullable<double> PrevalenciaEnMun { get; set; }
    
        public virtual Municipio Municipio { get; set; }
        public virtual Campania Campania { get; set; }
    }
}
