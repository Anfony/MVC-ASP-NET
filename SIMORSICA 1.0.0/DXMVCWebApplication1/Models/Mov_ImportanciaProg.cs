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
    
    public partial class Mov_ImportanciaProg
    {
        public int Pk_IdMov_ImportanciaProg { get; set; }
        public int Fk_IdCampania { get; set; }
        public int Fk_IdMunicipio { get; set; }
        public Nullable<int> Prevalencia { get; set; }
    
        public virtual Municipio Municipio { get; set; }
        public virtual Campania Campania { get; set; }
    }
}