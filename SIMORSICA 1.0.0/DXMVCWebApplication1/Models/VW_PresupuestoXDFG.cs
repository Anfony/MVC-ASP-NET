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
    
    public partial class VW_PresupuestoXDFG
    {
        public int Id { get; set; }
        public int Anio { get; set; }
        public string Estado { get; set; }
        public Nullable<double> DGSA { get; set; }
        public Nullable<double> DGSV { get; set; }
        public Nullable<double> DGIAAP { get; set; }
        public Nullable<double> DGIF { get; set; }
        public double TotalEstado { get; set; }
        public Nullable<double> GastoInversion { get; set; }
        public double Restante { get; set; }
    }
}
