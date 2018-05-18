using System;
using System.Collections.Generic;

namespace DXMVCWebApplication1.ViewsModels
{
    public class ProveedorVM
    {
        public ProveedorVM() { }
        public int Pk_IdProveedor { get; set; }
        public string RazonSocial_Nombre { get; set; }
        public string RFC { get; set; }
        public string Direccion { get; set; }
        public string Colonia { get; set; }
        public int Fk_IdEstado { get; set; }
        public int Fk_IdMunicipio { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public bool PersonaMoral { get; set; }
        public string NumeroEscritura { get; set; }
        public string NombreNotario { get; set; }
        public string NumeroNotaria { get; set; }
        public string FoliodeInscripcion { get; set; }
        public string RPPyC { get; set; }
        public string EstadoNotaria { get; set; }
        public string RepresentanteLegal { get; set; }
        public string CodigoPostal { get; set; }
        public virtual Models.Estado Estado { get; set; }
        public virtual Models.Municipio Municipio { get; set; }

    }
}