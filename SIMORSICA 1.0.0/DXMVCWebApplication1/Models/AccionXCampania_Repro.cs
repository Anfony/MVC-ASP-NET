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
    
    public partial class AccionXCampania_Repro
    {
        public AccionXCampania_Repro()
        {
            this.ProgramaAnualAdq_Repro = new HashSet<ProgramaAnualAdq_Repro>();
            this.ActividadXAccion_Repro = new HashSet<ActividadXAccion_Repro>();
        }
    
        public int PK_IdAccionXCampania { get; set; }
        public Nullable<int> FK_IdAccionXCampania_Orig { get; set; }
        public int Fk_IdCampania__UE { get; set; }
        public int Fk_IdTipoDeAccion__SIS { get; set; }
        public int FK_IdPersona__SIS { get; set; }
        public string Justificacion { get; set; }
        public Nullable<int> FK_IdStatusAccion { get; set; }
        public string CT_User { get; set; }
        public System.DateTime CT_Date { get; set; }
    
        public virtual Campania_Repro Campania_Repro { get; set; }
        public virtual Persona Persona { get; set; }
        public virtual StatusAccion StatusAccion { get; set; }
        public virtual TipoDeAccion TipoDeAccion { get; set; }
        public virtual ICollection<ProgramaAnualAdq_Repro> ProgramaAnualAdq_Repro { get; set; }
        public virtual ICollection<ActividadXAccion_Repro> ActividadXAccion_Repro { get; set; }
    }
}
