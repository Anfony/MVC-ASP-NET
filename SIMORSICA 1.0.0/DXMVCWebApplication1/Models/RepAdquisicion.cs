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
    
    public partial class RepAdquisicion
    {
        public int Pk_IdRepAdquisicion { get; set; }
        public int Fk_IdProgramaAnualAdq { get; set; }
        public System.DateTime FechaAdquisicion { get; set; }
        public string Concepto { get; set; }
        public Nullable<decimal> Importe { get; set; }
        public string Comentario { get; set; }
        public string Documento { get; set; }
        public Nullable<int> Fk_IdProveedor { get; set; }
        public string CT_User { get; set; }
        public System.DateTime CT_Date { get; set; }
        public bool esCerrado { get; set; }
    
        public virtual Proveedor Proveedor { get; set; }
        public virtual ProgramaAnualAdq ProgramaAnualAdq { get; set; }
    }
}
