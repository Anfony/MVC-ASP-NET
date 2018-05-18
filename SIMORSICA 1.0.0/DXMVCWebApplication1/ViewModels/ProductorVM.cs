using System;
using System.Collections.Generic;

namespace DXMVCWebApplication1.ViewsModels
{
    public class ProductorVM
    {
        public ProductorVM() { }
        public int Pk_IdProductor { get; set; }
        public string RazonSocial { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Colonia { get; set; }
        public int Fk_IdEstado__SIS { get; set; }
        public int Fk_IdMunicipio__SIS { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public virtual Models.Estado Estado { get; set; }
        public virtual Models.Municipio Municipio { get; set; }

    }
}