using System;
using System.ComponentModel.DataAnnotations;

namespace DXMVCWebApplication1.ViewModels
{
    public class ProgramaAnualAdqVM
    {
        public int Pk_IdProgramaAnualAdq { get; set; }

        [Required(ErrorMessage = "El Bien o Servicio es obligatorio")]
        [Display(Name = "Fk_IdBienOServ__SIS")]
        public int Fk_IdBienOServ__SIS { get; set; }

        [Required(ErrorMessage = "La Unidad de Medida es obligatoria")]
        [Display(Name = "Fk_IdUnidadDeMedida__SIS")]
        public int Fk_IdUnidadDeMedida__SIS { get; set; }

        [StringLength(500, ErrorMessage = "Por favor, introduzca no más de 500 caracteres")]
        public string Justificacion { get; set; }

        [Required(ErrorMessage ="Es necesario especificar el tipo de presupuesto")]
        public int TipoPresupuesto { get; set; }

        public decimal PresupuestoTotal { get; set; }

        public Nullable<decimal> Cant_Ene { get; set; }
        public Nullable<decimal> Cant_Feb { get; set; }
        public Nullable<decimal> Cant_Mar { get; set; }
        public Nullable<decimal> Cant_Abr { get; set; }
        public Nullable<decimal> Cant_May { get; set; }
        public Nullable<decimal> Cant_Jun { get; set; }
        public Nullable<decimal> Cant_Jul { get; set; }
        public Nullable<decimal> Cant_Ago { get; set; }
        public Nullable<decimal> Cant_Sep { get; set; }
        public Nullable<decimal> Cant_Oct { get; set; }
        public Nullable<decimal> Cant_Nov { get; set; }
        public Nullable<decimal> Cant_Dic { get; set; }
        public Nullable<decimal> imp_Ene { get; set; }
        public Nullable<decimal> imp_Feb { get; set; }
        public Nullable<decimal> imp_Mar { get; set; }
        public Nullable<decimal> imp_Abr { get; set; }
        public Nullable<decimal> imp_May { get; set; }
        public Nullable<decimal> imp_Jun { get; set; }
        public Nullable<decimal> imp_Jul { get; set; }
        public Nullable<decimal> imp_Ago { get; set; }
        public Nullable<decimal> imp_Sep { get; set; }
        public Nullable<decimal> imp_Oct { get; set; }
        public Nullable<decimal> imp_Nov { get; set; }
        public Nullable<decimal> imp_Dic { get; set; }
    }
}