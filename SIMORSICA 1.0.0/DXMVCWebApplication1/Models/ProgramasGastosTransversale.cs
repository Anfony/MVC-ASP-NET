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
    
    public partial class ProgramasGastosTransversale
    {
        public int Pk_IdProgramasGastosTransversales { get; set; }
        public int Fk_IdProgramaAnualAdq { get; set; }
        public int Fk_IdProyectoPresupuesto { get; set; }
        public decimal Monto { get; set; }
    
        public virtual ProyectoPresupuesto ProyectoPresupuesto { get; set; }
        public virtual ProgramaAnualAdq ProgramaAnualAdq { get; set; }
    }
}
