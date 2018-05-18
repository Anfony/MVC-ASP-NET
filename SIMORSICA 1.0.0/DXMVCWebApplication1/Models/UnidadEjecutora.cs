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
    
    public partial class UnidadEjecutora
    {
        public UnidadEjecutora()
        {
            this.Personas = new HashSet<Persona>();
            this.ProyectoPresupuestoes = new HashSet<ProyectoPresupuesto>();
            this.Instalacions = new HashSet<Instalacion>();
            this.Inventarios = new HashSet<Inventario>();
            this.Representantes = new HashSet<Representante1>();
            this.Vigencias = new HashSet<Vigencia>();
            this.ProgramaAnualAdq_Repro = new HashSet<ProgramaAnualAdq_Repro>();
            this.ProgramaAnualAdqs = new HashSet<ProgramaAnualAdq>();
            this.TESOFEs = new HashSet<TESOFE>();
            this.MinistracionesXComites = new HashSet<MinistracionesXComite>();
            this.Licitacions = new HashSet<Licitacion>();
        }
    
        public int Pk_IdUnidadEjecutora { get; set; }
        public int Fk_IdTipoDeUnidad__SIS { get; set; }
        public string Nombre { get; set; }
        public int Fk_IdEstado__SIS { get; set; }
        public int Fk_IdMunicipio { get; set; }
        public string Direccion { get; set; }
        public int Fk_IdUnidadResponsable__UE { get; set; }
        public string CorreoElectronico { get; set; }
        public System.Data.Entity.Spatial.DbGeography Ubicacion { get; set; }
        public string Colonia { get; set; }
        public string Telefono { get; set; }
        public string RFC { get; set; }
        public string ActaConstitutiva { get; set; }
        public string Notaria { get; set; }
        public string NombreNotario { get; set; }
        public string LugarNotario { get; set; }
        public Nullable<int> Fk_IdSubComponente { get; set; }
        public string StatusKardexUE { get; set; }
        public bool CT_LIVE { get; set; }
        public string CT_User { get; set; }
        public System.DateTime CT_Date { get; set; }
        public Nullable<int> FK_BancoCtaFederal { get; set; }
        public string CtaFederal { get; set; }
        public Nullable<int> FK_BancoCtaEstatal { get; set; }
        public string CtaEstatal { get; set; }
        public Nullable<int> FK_BancoCtaProductores { get; set; }
        public string CtaProductores { get; set; }
        public string SitioWeb { get; set; }
    
        public virtual Banco Banco { get; set; }
        public virtual Banco Banco1 { get; set; }
        public virtual Banco Banco2 { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual Municipio Municipio { get; set; }
        public virtual ICollection<Persona> Personas { get; set; }
        public virtual ICollection<ProyectoPresupuesto> ProyectoPresupuestoes { get; set; }
        public virtual SubComponente SubComponente { get; set; }
        public virtual TipoDeUnidad TipoDeUnidad { get; set; }
        public virtual ICollection<Instalacion> Instalacions { get; set; }
        public virtual ICollection<Inventario> Inventarios { get; set; }
        public virtual ICollection<Representante1> Representantes { get; set; }
        public virtual UnidadResponsable UnidadResponsable { get; set; }
        public virtual ICollection<Vigencia> Vigencias { get; set; }
        public virtual ICollection<ProgramaAnualAdq_Repro> ProgramaAnualAdq_Repro { get; set; }
        public virtual ICollection<ProgramaAnualAdq> ProgramaAnualAdqs { get; set; }
        public virtual ICollection<TESOFE> TESOFEs { get; set; }
        public virtual ICollection<MinistracionesXComite> MinistracionesXComites { get; set; }
        public virtual ICollection<Licitacion> Licitacions { get; set; }
    }
}