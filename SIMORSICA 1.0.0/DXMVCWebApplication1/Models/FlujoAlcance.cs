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
    
    public partial class FlujoAlcance
    {
        public int Pk_IdFlujoAlcance { get; set; }
        public int Fk_IdEstatusOrigen { get; set; }
        public int Fk_IdEstatusDestino { get; set; }
        public string Rol { get; set; }
        public string CT_User { get; set; }
        public System.DateTime CT_Date { get; set; }
    
        public virtual StatusAlcance StatusAlcance { get; set; }
        public virtual StatusAlcance StatusAlcance1 { get; set; }
    }
}
