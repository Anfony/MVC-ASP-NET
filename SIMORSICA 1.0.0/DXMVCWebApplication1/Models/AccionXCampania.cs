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
    
    public partial class AccionXCampania
    {
        public AccionXCampania()
        {
            this.NecesidadXAccions = new HashSet<NecesidadXAccion>();
            this.ProgramaAnualAdqs = new HashSet<ProgramaAnualAdq>();
            this.ActividadXAccions = new HashSet<ActividadXAccion>();
        }
    
        public int PK_IdAccionXCampania { get; set; }
        public int Fk_IdCampania__UE { get; set; }
        public int Fk_IdTipoDeAccion__SIS { get; set; }
        public int FK_IdPersona__SIS { get; set; }
        public string Justificacion { get; set; }
        public Nullable<int> FK_IdStatusAccion { get; set; }
        public string CT_User { get; set; }
        public System.DateTime CT_Date { get; set; }
        public Nullable<int> Fk_IdAccionXCampania_Repro { get; set; }
    
        public virtual StatusAccion StatusAccion { get; set; }
        public virtual ICollection<NecesidadXAccion> NecesidadXAccions { get; set; }
        public virtual TipoDeAccion TipoDeAccion { get; set; }
        public virtual Persona Persona { get; set; }
        public virtual ICollection<ProgramaAnualAdq> ProgramaAnualAdqs { get; set; }
        public virtual Campania Campania { get; set; }
        public virtual ICollection<ActividadXAccion> ActividadXAccions { get; set; }
    }
}
