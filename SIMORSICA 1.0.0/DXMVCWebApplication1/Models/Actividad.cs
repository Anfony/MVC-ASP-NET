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
    
    public partial class Actividad
    {
        public Actividad()
        {
            this.Beneficiarios = new HashSet<Beneficiario>();
            this.RepActividads = new HashSet<RepActividad>();
            this.StatusActividadKardexes = new HashSet<StatusActividadKardex>();
        }
    
        public int Pk_IdActividad { get; set; }
        public int Fk_IdPersona__SIS { get; set; }
        public Nullable<int> Fk_IdTipoActividad { get; set; }
        public Nullable<int> Fk_IdUnidadDeMedida { get; set; }
        public System.DateTime Fecha_Inicio { get; set; }
        public System.DateTime FechaFin { get; set; }
        public string Descripcion { get; set; }
        public Nullable<decimal> Costo { get; set; }
        public Nullable<int> Fk_IdStatusActividad__SIS { get; set; }
        public string Etiqueta { get; set; }
        public string Ubicacion { get; set; }
        public string Tema { get; set; }
        public int Fk_IdActividadXAccion { get; set; }
        public Nullable<decimal> Asignado_Ene { get; set; }
        public Nullable<decimal> Asignado_Feb { get; set; }
        public Nullable<decimal> Asignado_Mar { get; set; }
        public Nullable<decimal> Asignado_Abr { get; set; }
        public Nullable<decimal> Asignado_May { get; set; }
        public Nullable<decimal> Asignado_Jun { get; set; }
        public Nullable<decimal> Asignado_Jul { get; set; }
        public Nullable<decimal> Asignado_Ago { get; set; }
        public Nullable<decimal> Asignado_Sep { get; set; }
        public Nullable<decimal> Asignado_Oct { get; set; }
        public Nullable<decimal> Asignado_Nov { get; set; }
        public Nullable<decimal> Asignado_Dic { get; set; }
        public string CT_User { get; set; }
        public System.DateTime CT_Date { get; set; }
        public Nullable<decimal> Asignado_Anual { get; set; }
    
        public virtual Persona Persona { get; set; }
        public virtual StatusActividad StatusActividad { get; set; }
        public virtual TipoActividad TipoActividad { get; set; }
        public virtual UnidadDeMedida UnidadDeMedida { get; set; }
        public virtual ICollection<Beneficiario> Beneficiarios { get; set; }
        public virtual ICollection<RepActividad> RepActividads { get; set; }
        public virtual ICollection<StatusActividadKardex> StatusActividadKardexes { get; set; }
        public virtual ActividadXAccion ActividadXAccion { get; set; }
    }
}
