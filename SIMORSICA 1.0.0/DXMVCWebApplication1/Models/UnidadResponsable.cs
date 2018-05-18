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
    
    public partial class UnidadResponsable
    {
        public UnidadResponsable()
        {
            this.PorcentajeXDireccionGenerals = new HashSet<PorcentajeXDireccionGeneral>();
            this.UnidadResponsableXSubComponentes = new HashSet<UnidadResponsableXSubComponente>();
            this.UnidadResponsableXSubComponentePres = new HashSet<UnidadResponsableXSubComponentePre>();
            this.ProyectoPresupuestoes = new HashSet<ProyectoPresupuesto>();
            this.UnidadEjecutoras = new HashSet<UnidadEjecutora>();
            this.CuadroCompXPantallas = new HashSet<CuadroCompXPantalla>();
        }
    
        public int Pk_IdUnidadResponsable { get; set; }
        public string Nombre { get; set; }
        public int CT_CreatedBy { get; set; }
        public System.DateTime CT_CreatedDate { get; set; }
        public Nullable<int> CT_ModifiedBy { get; set; }
        public Nullable<System.DateTime> CT_ModifiedDate { get; set; }
        public bool CT_LIVE { get; set; }
        public string Siglas { get; set; }
        public string CT_User { get; set; }
        public System.DateTime CT_Date { get; set; }
    
        public virtual ICollection<PorcentajeXDireccionGeneral> PorcentajeXDireccionGenerals { get; set; }
        public virtual ICollection<UnidadResponsableXSubComponente> UnidadResponsableXSubComponentes { get; set; }
        public virtual ICollection<UnidadResponsableXSubComponentePre> UnidadResponsableXSubComponentePres { get; set; }
        public virtual ICollection<ProyectoPresupuesto> ProyectoPresupuestoes { get; set; }
        public virtual ICollection<UnidadEjecutora> UnidadEjecutoras { get; set; }
        public virtual ICollection<CuadroCompXPantalla> CuadroCompXPantallas { get; set; }
    }
}
