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
    
    public partial class Representante1
    {
        public int Pk_IdRepresentante { get; set; }
        public int Fk_IdUnidadEjecutora__UE { get; set; }
        public string Ap_Paterno { get; set; }
        public string Ap_Materno { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Colonia { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int Fk_IdCargo { get; set; }
        public string NombreCompleto { get; set; }
    
        public virtual Cargo Cargo { get; set; }
        public virtual UnidadEjecutora UnidadEjecutora { get; set; }
    }
}