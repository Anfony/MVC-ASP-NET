using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DXMVCWebApplication1.ViewModels
{
    public class ParamsRepInfoFFVM
    {
        [Required(ErrorMessage = "La Campaña es Obligatoria")]
        [Display(Name = "ComboCampania")]
        public int ComboCampania { get; set; }
        public int ComboMes { get; set; }
        public int ComboPeriodo { get; set; }
    }
}