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
    
    public partial class ActaComision
    {
        public int Pk_IdActaComision { get; set; }
        public int Fk_IdEstado { get; set; }
        public int Fk_IdAnio { get; set; }
        public Nullable<int> Fk_IdMes { get; set; }
        public Nullable<int> Fk_IdMesTrimestre { get; set; }
        public string Documento { get; set; }
        public System.DateTime CT_Date { get; set; }
        public string CT_User { get; set; }
    
        public virtual Estado Estado { get; set; }
        public virtual Me Me { get; set; }
        public virtual MesByTrimestre MesByTrimestre { get; set; }
    }
}
