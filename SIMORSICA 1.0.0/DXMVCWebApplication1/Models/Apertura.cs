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
    
    public partial class Apertura
    {
        public int Pk_IdAperturas { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> Fk_IdLicitacion__INFO { get; set; }
        public Nullable<int> Fk_IdFiles__INFO { get; set; }
        public Nullable<System.DateTime> FechaApertura { get; set; }
        public string Documento { get; set; }
    
        public virtual Licitacion Licitacion { get; set; }
        public virtual FILE FILE { get; set; }
    }
}
