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
    
    public partial class TipoAdquisicion
    {
        public TipoAdquisicion()
        {
            this.Instalacions = new HashSet<Instalacion>();
        }
    
        public int Pk_IdTipoAdquisicion { get; set; }
        public string Nombre { get; set; }
        public string CT_User { get; set; }
        public System.DateTime CT_Date { get; set; }
    
        public virtual ICollection<Instalacion> Instalacions { get; set; }
    }
}
