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
    
    public partial class CapituloPartidaLicitacion
    {
        public CapituloPartidaLicitacion()
        {
            this.Licitacion_Capitulo = new HashSet<Licitacion_Capitulo>();
        }
    
        public int Pk_IdCapituloPartida { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
    
        public virtual ICollection<Licitacion_Capitulo> Licitacion_Capitulo { get; set; }
    }
}
