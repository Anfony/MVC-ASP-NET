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
    
    public partial class TipoDeAccion
    {
        public TipoDeAccion()
        {
            this.TipoActividads = new HashSet<TipoActividad>();
            this.AccionXCampanias = new HashSet<AccionXCampania>();
            this.Accions = new HashSet<Accion>();
            this.AccionXCampania_Repro = new HashSet<AccionXCampania_Repro>();
        }
    
        public int Pk_IDTipoDeAccion { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> Fk_IdUnidadDeMedida { get; set; }
        public Nullable<int> FK_IdProyectoAutorizado { get; set; }
        public string CT_User { get; set; }
        public System.DateTime CT_Date { get; set; }
    
        public virtual ICollection<TipoActividad> TipoActividads { get; set; }
        public virtual UnidadDeMedida UnidadDeMedida { get; set; }
        public virtual ICollection<AccionXCampania> AccionXCampanias { get; set; }
        public virtual ICollection<Accion> Accions { get; set; }
        public virtual ICollection<AccionXCampania_Repro> AccionXCampania_Repro { get; set; }
        public virtual ProyectoAutorizado ProyectoAutorizado { get; set; }
    }
}
