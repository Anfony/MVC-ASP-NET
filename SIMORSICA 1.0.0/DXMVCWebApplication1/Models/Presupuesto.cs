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
    
    public partial class Presupuesto
    {
        public int PK_IdPresupuesto { get; set; }
        public int Pk_IdEstado { get; set; }
        public int Pk_IdProyectoAutorizado { get; set; }
        public double Federal { get; set; }
        public double Estatal { get; set; }
        public string CT_User { get; set; }
        public System.DateTime CT_Date { get; set; }
    }
}
