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
    
    public partial class StatusAccion
    {
        public StatusAccion()
        {
            this.AccionXCampanias = new HashSet<AccionXCampania>();
            this.AccionXCampania_Repro = new HashSet<AccionXCampania_Repro>();
        }
    
        public int PK_IdStatusAccion { get; set; }
        public string Nombre { get; set; }
    
        public virtual ICollection<AccionXCampania> AccionXCampanias { get; set; }
        public virtual ICollection<AccionXCampania_Repro> AccionXCampania_Repro { get; set; }
    }
}
