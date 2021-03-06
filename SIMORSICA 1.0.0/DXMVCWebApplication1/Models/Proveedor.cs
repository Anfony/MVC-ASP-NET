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
    
    public partial class Proveedor
    {
        public Proveedor()
        {
            this.GirosXProvs = new HashSet<GirosXProv>();
            this.RepAdquisicions = new HashSet<RepAdquisicion>();
        }
    
        public int Pk_IdProveedor { get; set; }
        public string RazonSocial_Nombre { get; set; }
        public string RFC { get; set; }
        public string Direccion { get; set; }
        public string Colonia { get; set; }
        public int Fk_IdEstado { get; set; }
        public int Fk_IdMunicipio { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public System.Data.Entity.Spatial.DbGeography Ubicacion { get; set; }
        public Nullable<bool> PersonaMoral { get; set; }
        public string NumeroEscritura { get; set; }
        public Nullable<System.DateTime> FechadeAlta { get; set; }
        public string NombreNotario { get; set; }
        public string NumeroNotaria { get; set; }
        public string FoliodeInscripcion { get; set; }
        public string RPPyC { get; set; }
        public string EstadoNotaria { get; set; }
        public string RepresentanteLegal { get; set; }
        public string CodigoPostal { get; set; }
        public string CT_User { get; set; }
        public System.DateTime CT_Date { get; set; }
    
        public virtual Municipio Municipio { get; set; }
        public virtual ICollection<GirosXProv> GirosXProvs { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual ICollection<RepAdquisicion> RepAdquisicions { get; set; }
    }
}
