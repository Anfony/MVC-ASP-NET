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
    
    public partial class InoCompSA
    {
        public int Pk_IdInoCompSA { get; set; }
        public int FK_IdCampania { get; set; }
        public int Fk_IdMunicipio { get; set; }
        public string Producto { get; set; }
        public Nullable<int> ProductoresAtendidos { get; set; }
        public Nullable<int> Superficie { get; set; }
        public Nullable<int> Produccion { get; set; }
        public Nullable<int> Acopio { get; set; }
        public Nullable<int> Manejo { get; set; }
        public Nullable<int> Envasado { get; set; }
        public Nullable<int> UpAnioAnterior { get; set; }
        public Nullable<int> UpAnioActual { get; set; }
        public Nullable<int> CabezasAnioAnt { get; set; }
        public Nullable<int> CabezasAnioAct { get; set; }
        public string CT_User { get; set; }
        public System.DateTime CT_Date { get; set; }
    
        public virtual Municipio Municipio { get; set; }
        public virtual Campania Campania { get; set; }
    }
}
