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
    
    public partial class Mov_Atendido
    {
        public int Pk_IdMov_Atendido { get; set; }
        public int Fk_IdCampania { get; set; }
        public int Fk_IdEstado { get; set; }
        public Nullable<int> NumInspecciones { get; set; }
        public Nullable<int> NumCuarentenas { get; set; }
        public Nullable<int> NumActasRetorno { get; set; }
        public Nullable<int> NumTratamientos { get; set; }
        public Nullable<int> NumDestrucciones { get; set; }
    
        public virtual Estado Estado { get; set; }
        public virtual Campania Campania { get; set; }
    }
}