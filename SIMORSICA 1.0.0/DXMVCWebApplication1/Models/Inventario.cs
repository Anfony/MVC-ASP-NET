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
    
    public partial class Inventario
    {
        public int PK_IdInventario { get; set; }
        public int Fk_IdUnidadEjecutora { get; set; }
        public int FK_IdAnio { get; set; }
        public int FK_IdEstadoBien__SIS { get; set; }
        public int FK_IdBienOServ { get; set; }
        public string Clave { get; set; }
        public string ClaveAnt { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public string Factura { get; set; }
        public Nullable<decimal> Costo { get; set; }
        public Nullable<System.DateTime> FechaAdq { get; set; }
        public string Referencia { get; set; }
        public string CT_User { get; set; }
        public System.DateTime CT_Date { get; set; }
        public Nullable<int> Fk_TipodeRecurso_SIS { get; set; }
        public string CondicionOperativa { get; set; }
        public string UbicacionFisica { get; set; }
        public Nullable<int> Cilindros { get; set; }
    
        public virtual Anio Anio { get; set; }
        public virtual EstadoBien EstadoBien { get; set; }
        public virtual UnidadEjecutora UnidadEjecutora { get; set; }
        public virtual TipoDeRecurso TipoDeRecurso { get; set; }
        public virtual BienOServ BienOServ { get; set; }
    }
}
