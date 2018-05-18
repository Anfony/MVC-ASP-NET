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
    
    public partial class SACImportanciaCultivo
    {
        public int Pk_IdSACImportanciaCultivo { get; set; }
        public int Fk_IdCampania { get; set; }
        public string Cultivo { get; set; }
        public Nullable<double> SuperficieInund { get; set; }
        public Nullable<int> Fk_IdMunicipio { get; set; }
        public Nullable<int> NumProductores { get; set; }
        public Nullable<double> Superficie { get; set; }
        public Nullable<double> AnioAntProduccion { get; set; }
        public Nullable<double> AnioAntCosto { get; set; }
        public Nullable<double> AnioAntValor { get; set; }
        public Nullable<double> AnioActProd { get; set; }
        public Nullable<double> AnioActCosto { get; set; }
        public Nullable<double> AnioActValor { get; set; }
    
        public virtual Municipio Municipio { get; set; }
        public virtual Campania Campania { get; set; }
    }
}
