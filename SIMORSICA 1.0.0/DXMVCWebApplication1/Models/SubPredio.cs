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
    
    public partial class SubPredio
    {
        public int Pk_IDSubPredio { get; set; }
        public Nullable<int> Fk_IdPredio__GEO { get; set; }
        public string UsoPrincipal { get; set; }
        public string Comentarios { get; set; }
        public System.Data.Entity.Spatial.DbGeography poligono { get; set; }
    
        public virtual Predio Predio { get; set; }
    }
}
