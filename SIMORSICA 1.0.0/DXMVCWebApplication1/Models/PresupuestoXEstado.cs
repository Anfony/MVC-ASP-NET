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
    
    public partial class PresupuestoXEstado
    {
        public int Pk_IdPresupuestoXEstado { get; set; }
        public int FK_IdPorcentajeXDireccionGeneral { get; set; }
        public int FK_IdEstado { get; set; }
        public Nullable<decimal> CantidadSolicitada { get; set; }
        public bool PresupuestoCerrado { get; set; }
        public Nullable<double> PresupuestoAsignadoXEstado_DG { get; set; }
    
        public virtual PorcentajeXDireccionGeneral PorcentajeXDireccionGeneral { get; set; }
        public virtual Estado Estado { get; set; }
    }
}
